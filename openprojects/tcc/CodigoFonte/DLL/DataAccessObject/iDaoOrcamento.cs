using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.DataAccessObject
{
    public class iDaoOrcamento
    {
        #region Variaveis Internas da Classe
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }
        #endregion

        #region Método Insere Orcamento
        public int dInsereOrcamento(iModOrcamento objOrcamento)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.StoredProcedure;
            ComandoSQL.CommandText = "sp_GravaOrcamento";

            SqlParameter parametro = new SqlParameter("@fk_numcliente", objOrcamento.cliente.Pk_Codigo);
            ComandoSQL.Parameters.Add(parametro);

            SqlParameter parametro3 = new SqlParameter("@infoAdicional", objOrcamento.InfoAdicional);
            ComandoSQL.Parameters.Add(parametro3);

            SqlParameter parametro4 = new SqlParameter("@valorFinal", objOrcamento.ValorFinal);
            ComandoSQL.Parameters.Add(parametro4);

            clsNewContasMatematicas contas = new clsNewContasMatematicas();

            //passo o parametro de Retorno que está no construtor da SP
            SqlParameter parametroRetorno = new SqlParameter("@ultimoOrcamento", 0);
            //defino que ele é um parametro de Retorno, ou seja, OUTPUT
            ComandoSQL.Parameters.Add(parametroRetorno).Direction = ParameterDirection.Output;
            try
            {
                //Executo a Query                
                ComandoSQL.ExecuteNonQuery();

                //esse inteiro recebe o valor gerado pelo Identity no banco de dados
                int codigoOrcamentoGerado = Convert.ToInt32(ComandoSQL.Parameters["@ultimoOrcamento"].Value.ToString());

                clsValidacaoDeStrings validarString = new clsValidacaoDeStrings();

                foreach (iModItensOrcamento item in objOrcamento.itensOrcamento)
                {
                    if (item != null)
                    {
                        StringBuilder SqlConcatenada2 = new StringBuilder();

                        SqlConcatenada2.Append("INSERT INTO ITEMSORCAMENTO ");
                        SqlConcatenada2.Append("(FK_NUMORCAMENTO ");
                        SqlConcatenada2.Append(",FK_NUMPRODUTO ");
                        SqlConcatenada2.Append(",ITEM ");
                        SqlConcatenada2.Append(",CODIGOFABRIC ");
                        SqlConcatenada2.Append(",DESCRICAOAPLICACAO ");
                        SqlConcatenada2.Append(",PRECOVENDABANCO ");
                        SqlConcatenada2.Append(",VALORTOTAL_SEMDESC_OU_ACRE ");
                        SqlConcatenada2.Append(",VALORUNITARIO ");
                        SqlConcatenada2.Append(",QUANTIDADE ");
                        SqlConcatenada2.Append(",VALORTOTAL ");
                        SqlConcatenada2.Append(",DESCONTO ");
                        SqlConcatenada2.Append(",ACRESCIMO ) ");
                        SqlConcatenada2.Append("VALUES ");
                        SqlConcatenada2.Append("(@FK_NUMORCAMENTO ");
                        SqlConcatenada2.Append(",@FK_NUMPRODUTO ");
                        SqlConcatenada2.Append(",@ITEM ");
                        SqlConcatenada2.Append(",@CODIGOFABRIC ");
                        SqlConcatenada2.Append(",@DESCRICAOAPLICACAO ");
                        SqlConcatenada2.Append(",@PRECOVENDABANCO ");
                        SqlConcatenada2.Append(",@VALORTOTAL_SEMDESC_OU_ACRE ");
                        SqlConcatenada2.Append(",@VALORUNITARIO ");
                        SqlConcatenada2.Append(",@QUANTIDADE ");
                        SqlConcatenada2.Append(",@VALORTOTAL ");
                        SqlConcatenada2.Append(",@DESCONTO ");
                        SqlConcatenada2.Append(",@ACRESCIMO) ");



                        SqlCommand ComandoSQL2 = new SqlCommand();
                        ComandoSQL2.Connection = conexao;
                        ComandoSQL2.CommandType = CommandType.Text;
                        ComandoSQL2.CommandText = SqlConcatenada2.ToString();


                        SqlParameter parametro15 = new SqlParameter("@FK_NUMORCAMENTO", codigoOrcamentoGerado);
                        ComandoSQL2.Parameters.Add(parametro15);
                        SqlParameter parametro16 = new SqlParameter("@FK_NUMPRODUTO", item.produto.Pk_Codigo);
                        ComandoSQL2.Parameters.Add(parametro16);
                        SqlParameter parametro18 = new SqlParameter("@ITEM", item.NumeroItem);
                        ComandoSQL2.Parameters.Add(parametro18);
                        SqlParameter parametro19 = new SqlParameter("@PRECOVENDABANCO", item.PrecoVendaBanco);
                        ComandoSQL2.Parameters.Add(parametro19);
                        SqlParameter parametro20 = new SqlParameter("@VALORTOTAL_SEMDESC_OU_ACRE", item.ValorTotalSemDescAcre);
                        ComandoSQL2.Parameters.Add(parametro20);
                        SqlParameter parametro21 = new SqlParameter("@VALORUNITARIO", item.ValorUnit);
                        ComandoSQL2.Parameters.Add(parametro21);
                        SqlParameter parametro22 = new SqlParameter("@QUANTIDADE", item.Quantidade);
                        ComandoSQL2.Parameters.Add(parametro22);
                        SqlParameter parametro23 = new SqlParameter("@VALORTOTAL", item.ValorTotal);
                        ComandoSQL2.Parameters.Add(parametro23);
                        SqlParameter parametro24 = new SqlParameter("@DESCONTO", item.Desconto);
                        ComandoSQL2.Parameters.Add(parametro24);
                        SqlParameter parametro25 = new SqlParameter("@ACRESCIMO", item.Acrescimo);
                        ComandoSQL2.Parameters.Add(parametro25);
                        
                        SqlParameter parametro31 = new SqlParameter("@CODIGOFABRIC", item.CodigoFabric);
                        ComandoSQL2.Parameters.Add(parametro31);

                        SqlParameter parametro32 = new SqlParameter("@DESCRICAOAPLICACAO", item.DescricaoAplicacao);
                        ComandoSQL2.Parameters.Add(parametro32);

                        ComandoSQL2.ExecuteNonQuery();

                        SqlConcatenada2 = null;
                        ComandoSQL2 = null;

                        parametro15 = null;
                        parametro16 = null;
                        parametro18 = null;
                        parametro19 = null;
                        parametro20 = null;
                        parametro21 = null;
                        parametro22 = null;
                        parametro23 = null;
                        parametro24 = null;
                        parametro25 = null;
                        parametro31 = null;
                        parametro32 = null;
                    }
                }//fim while

                return codigoOrcamentoGerado;
            }//fim try


        //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "gravarOrcamento()", erro.Message.ToString(), "Inclui um Orcamento na Base de Dados");
                return 0;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                ComandoSQL = null;
                parametro = null;

                parametro3 = null;
                parametro4 = null;
                parametroRetorno = null;
            }
        }
        #endregion

        #region Metodo Altera Orcamento
        public bool dAlteraOrcamento(iModOrcamento objOrcamento)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            //cria os objetos de conexao e de transação         

            #region Faz o Update na tabela de Orcamentos
            StringBuilder SqlConcatenada = new StringBuilder();

            SqlConcatenada.Append("UPDATE ORCAMENTOS SET ");
            SqlConcatenada.Append("FK_NUMCLIENTE = @FK_NUMCLIENTE ");
            SqlConcatenada.Append(",INFOADICIONAL = @INFOADICIONAL ");
            SqlConcatenada.Append(",STATUSVENDIDO = @STATUSVENDIDO ");
            SqlConcatenada.Append(",VALORFINAL = @VALORFINAL ");
            SqlConcatenada.Append("WHERE CODIGO = @CODIGOORCAMENTO ");


            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = SqlConcatenada.ToString();

            SqlParameter parametro = new SqlParameter("@CODIGOORCAMENTO", objOrcamento.PkCodigo);
            ComandoSQL.Parameters.Add(parametro);

            SqlParameter parametro1 = new SqlParameter("@fk_numcliente", objOrcamento.cliente.Pk_Codigo);
            ComandoSQL.Parameters.Add(parametro1);

            SqlParameter parametro3 = new SqlParameter("@infoAdicional", objOrcamento.InfoAdicional);
            ComandoSQL.Parameters.Add(parametro3);

            SqlParameter parametro4 = new SqlParameter("@valorFinal", objOrcamento.ValorFinal);
            ComandoSQL.Parameters.Add(parametro4);

            clsNewContasMatematicas contas = new clsNewContasMatematicas();

            SqlParameter newParametro5 = new SqlParameter("@STATUSVENDIDO", "EM ABERTO");
            ComandoSQL.Parameters.Add(newParametro5);

            #endregion

            try
            {
                ComandoSQL.ExecuteNonQuery();

                #region Faz o Delete dos antigos itens da tabela de Itens do Orcamento
                SqlCommand ComandoSQL3 = new SqlCommand();
                ComandoSQL3.Connection = conexao;
                ComandoSQL3.CommandType = CommandType.Text;
                ComandoSQL3.CommandText = "DELETE FROM ITEMSORCAMENTO WHERE FK_NUMORCAMENTO = @CODIGOORCAMENTO";

                SqlParameter parametroSelect = new SqlParameter("@CODIGOORCAMENTO", objOrcamento.PkCodigo);
                ComandoSQL3.Parameters.Add(parametroSelect);

                ComandoSQL3.ExecuteNonQuery();
                #endregion

                #region Grava novamente os itens do orcamento novos e atualizados
                foreach (iModItensOrcamento item in objOrcamento.itensOrcamento)
                {
                    if (item != null)
                    {
                        StringBuilder SqlConcatenada2 = new StringBuilder();

                        SqlConcatenada2.Append("INSERT INTO ITEMSORCAMENTO ");
                        SqlConcatenada2.Append("(FK_NUMORCAMENTO ");
                        SqlConcatenada2.Append(",FK_NUMPRODUTO ");
                        SqlConcatenada2.Append(",ITEM ");
                        SqlConcatenada2.Append(",CODIGOFABRIC ");
                        SqlConcatenada2.Append(",DESCRICAOAPLICACAO ");
                        SqlConcatenada2.Append(",PRECOVENDABANCO ");
                        SqlConcatenada2.Append(",VALORTOTAL_SEMDESC_OU_ACRE ");
                        SqlConcatenada2.Append(",VALORUNITARIO ");
                        SqlConcatenada2.Append(",QUANTIDADE ");
                        SqlConcatenada2.Append(",VALORTOTAL ");
                        SqlConcatenada2.Append(",DESCONTO ");
                        SqlConcatenada2.Append(",ACRESCIMO) ");
                        SqlConcatenada2.Append("VALUES ");
                        SqlConcatenada2.Append("(@FK_NUMORCAMENTO ");
                        SqlConcatenada2.Append(",@FK_NUMPRODUTO ");
                        SqlConcatenada2.Append(",@ITEM ");
                        SqlConcatenada2.Append(",@CODIGOFABRIC ");
                        SqlConcatenada2.Append(",@DESCRICAOAPLICACAO ");
                        SqlConcatenada2.Append(",@PRECOVENDABANCO ");
                        SqlConcatenada2.Append(",@VALORTOTAL_SEMDESC_OU_ACRE ");
                        SqlConcatenada2.Append(",@VALORUNITARIO ");
                        SqlConcatenada2.Append(",@QUANTIDADE ");
                        SqlConcatenada2.Append(",@VALORTOTAL ");
                        SqlConcatenada2.Append(",@DESCONTO ");
                        SqlConcatenada2.Append(",@ACRESCIMO) ");

                        SqlCommand ComandoSQL2 = new SqlCommand();
                        ComandoSQL2.Connection = conexao;
                        ComandoSQL2.CommandType = CommandType.Text;
                        ComandoSQL2.CommandText = SqlConcatenada2.ToString();


                        SqlParameter parametro15 = new SqlParameter("@FK_NUMORCAMENTO", objOrcamento.PkCodigo);
                        ComandoSQL2.Parameters.Add(parametro15);
                        SqlParameter parametro16 = new SqlParameter("@FK_NUMPRODUTO", item.produto.Pk_Codigo);
                        ComandoSQL2.Parameters.Add(parametro16);
                        SqlParameter parametro18 = new SqlParameter("@ITEM", item.NumeroItem);
                        ComandoSQL2.Parameters.Add(parametro18);
                        SqlParameter parametro19 = new SqlParameter("@PRECOVENDABANCO", item.PrecoVendaBanco);
                        ComandoSQL2.Parameters.Add(parametro19);
                        SqlParameter parametro20 = new SqlParameter("@VALORTOTAL_SEMDESC_OU_ACRE", item.ValorTotalSemDescAcre);
                        ComandoSQL2.Parameters.Add(parametro20);
                        SqlParameter parametro21 = new SqlParameter("@VALORUNITARIO", item.ValorUnit);
                        ComandoSQL2.Parameters.Add(parametro21);
                        SqlParameter parametro22 = new SqlParameter("@QUANTIDADE", item.Quantidade);
                        ComandoSQL2.Parameters.Add(parametro22);
                        SqlParameter parametro23 = new SqlParameter("@VALORTOTAL", item.ValorTotal);
                        ComandoSQL2.Parameters.Add(parametro23);
                        SqlParameter parametro24 = new SqlParameter("@DESCONTO", item.Desconto);
                        ComandoSQL2.Parameters.Add(parametro24);
                        SqlParameter parametro25 = new SqlParameter("@ACRESCIMO", item.Acrescimo);
                        ComandoSQL2.Parameters.Add(parametro25);

                        SqlParameter parametro31 = new SqlParameter("@CODIGOFABRIC", item.CodigoFabric);
                        ComandoSQL2.Parameters.Add(parametro31);

                        SqlParameter parametro32 = new SqlParameter("@DESCRICAOAPLICACAO", item.DescricaoAplicacao);
                        ComandoSQL2.Parameters.Add(parametro32);

                        ComandoSQL2.ExecuteNonQuery();

                        SqlConcatenada2 = null;
                        ComandoSQL2 = null;

                        parametro15 = null;
                        parametro16 = null;
                        parametro18 = null;
                        parametro19 = null;
                        parametro20 = null;
                        parametro21 = null;
                        parametro22 = null;
                        parametro23 = null;
                        parametro24 = null;
                        parametro25 = null;
                        parametro31 = null;
                        parametro32 = null;
                    }
                }//fim while
                #endregion
                return true;
            }//fim try

            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "atualizarOrcamento()", erro.Message.ToString(), "Atualizando Orcamento no Sistema");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                parametro = null;

                parametro3 = null;
                parametro4 = null;
            }
        }
        #endregion

        #region Obter Orcamento
        public iModOrcamento[] dObterOrcamento()
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            StringBuilder SqlConcatenada = new StringBuilder();

            SqlConcatenada.Append("SELECT  ");
            SqlConcatenada.Append("ORC.CODIGO ");
            SqlConcatenada.Append(",ORC.FK_NUMCLIENTE ");

            SqlConcatenada.Append(",ORC.INFOADICIONAL ");
            SqlConcatenada.Append(",ORC.STATUSVENDIDO ");
            SqlConcatenada.Append(",ORC.VALORFINAL ");
            SqlConcatenada.Append(",ORC.DATA ");

            SqlConcatenada.Append(",CLI.NOME AS NOMECLIENTE ");

            SqlConcatenada.Append("FROM ORCAMENTOS ORC ");
            SqlConcatenada.Append("INNER JOIN CLIENTES CLI ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");

            SqlConcatenada.Append("WHERE ORC.STATUSVENDIDO <> 'ANAMAO' "); //WTF?
            SqlConcatenada.Append("ORDER BY ORC.CODIGO ");

            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet, "ORCAMENTOS");

                iModOrcamento[] orcamentos = new iModOrcamento[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset
                
                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    orcamentos[i] = new iModOrcamento(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    orcamentos[i].PkCodigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["CODIGO"].ToString());
                    orcamentos[i].cliente = new iModCliente(); //crio um novo objeto cliente dentro deste objeto orcamento
                    orcamentos[i].cliente.Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["FK_NUMCLIENTE"].ToString());
                    orcamentos[i].cliente.Nome = DsDataSet.Tables[0].Rows[i]["NOMECLIENTE"].ToString();
                    orcamentos[i].InfoAdicional = DsDataSet.Tables[0].Rows[i]["INFOADICIONAL"].ToString();
                    orcamentos[i].Status = DsDataSet.Tables[0].Rows[i]["STATUSVENDIDO"].ToString();
                    orcamentos[i].ValorFinal = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORFINAL"].ToString());                    
                    orcamentos[i].DataOrc = Convert.ToDateTime(DsDataSet.Tables[0].Rows[i]["DATA"].ToString());
                }
                return orcamentos;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "newObterOrcamentosOSAberto()", erro.Message.ToString(), "Obter Orcamentos e OS em Aberto");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                DataAdap = null;
                SqlConcatenada = null;
                DsDataSet = null;
            }
        }
        #endregion
        
        #region Carrega Dados dos Produtos de um Orcamento/Ordem de Servico
        public iModItensOrcamento[] dObterProdutosDeUmOrcamento(iModOrcamento objOrcamento)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            StringBuilder SqlConcatenada = new StringBuilder();

            SqlConcatenada.Append("SELECT ");
            SqlConcatenada.Append("PK_ID = ITO.PK_ID, ");
            SqlConcatenada.Append("FK_NUMPRODUTO = ITO.FK_NUMPRODUTO, ");
            SqlConcatenada.Append("ITEM = ITO.ITEM, ");
            SqlConcatenada.Append("CODIGOFABRIC = ITO.CODIGOFABRIC, ");
            SqlConcatenada.Append("DESCRICAOAPLICACAO = ITO.DESCRICAOAPLICACAO, ");
            SqlConcatenada.Append("PRECOVENDABANCO = ITO.PRECOVENDABANCO, ");
            SqlConcatenada.Append("VALORTOTAL_SEMDESC_OU_ACRE = ITO.VALORTOTAL_SEMDESC_OU_ACRE, ");
            SqlConcatenada.Append("VALORUNITARIO = ITO.VALORUNITARIO, ");
            SqlConcatenada.Append("QUANTIDADE = ITO.QUANTIDADE, ");
            SqlConcatenada.Append("VALORTOTAL = ITO.VALORTOTAL, ");
            SqlConcatenada.Append("DESCONTO = ITO.DESCONTO, ");
            SqlConcatenada.Append("ACRESCIMO = ITO.ACRESCIMO ");
            SqlConcatenada.Append("FROM ITEMSORCAMENTO ITO ");
            SqlConcatenada.Append("WHERE ITO.FK_NUMORCAMENTO = @CODIGOORCT ");
            SqlConcatenada.Append("ORDER BY ITO.ITEM");

            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataAdap.SelectCommand.Parameters.AddWithValue("@CODIGOORCT", objOrcamento.PkCodigo);

            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet, "ITENSORCAMENTO");

                iModItensOrcamento[] itensOrcamentos = new iModItensOrcamento[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset

                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    itensOrcamentos[i] = new iModItensOrcamento(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    itensOrcamentos[i].PkIdItemVenda = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["PK_ID"].ToString());
                    itensOrcamentos[i].produto.Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["FK_NUMPRODUTO"].ToString());
                    itensOrcamentos[i].NumeroItem = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["ITEM"].ToString());
                    itensOrcamentos[i].CodigoFabric = DsDataSet.Tables[0].Rows[i]["CODIGOFABRIC"].ToString();
                    itensOrcamentos[i].DescricaoAplicacao = DsDataSet.Tables[0].Rows[i]["DESCRICAOAPLICACAO"].ToString();
                    itensOrcamentos[i].PrecoVendaBanco = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["PRECOVENDABANCO"].ToString());
                    itensOrcamentos[i].ValorTotalSemDescAcre = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORTOTAL_SEMDESC_OU_ACRE"].ToString());
                    itensOrcamentos[i].ValorUnit = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORUNITARIO"].ToString());
                    itensOrcamentos[i].Quantidade = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["QUANTIDADE"].ToString());
                    itensOrcamentos[i].ValorTotal = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORTOTAL"].ToString());
                    itensOrcamentos[i].Desconto = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["DESCONTO"].ToString());
                    itensOrcamentos[i].Acrescimo = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["ACRESCIMO"].ToString());
                }
                return itensOrcamentos;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "newObterProdutosDeUmOrcamentoOS()", erro.Message.ToString(), "Obter Itens de um Orcamento e OS");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                DataAdap = null;
                SqlConcatenada = null;
                DsDataSet = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método Obter Todos Orçamentos em Aberto
        public iModOrcamento[] dObterOrcamentosApenasEmAberto()
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            StringBuilder SqlConcatenada = new StringBuilder();

            SqlConcatenada.Append("SELECT  ");
            SqlConcatenada.Append("ORC.CODIGO ");
            SqlConcatenada.Append(",ORC.FK_NUMCLIENTE ");
            SqlConcatenada.Append(",ORC.INFOADICIONAL ");
            SqlConcatenada.Append(",ORC.STATUSVENDIDO ");
            SqlConcatenada.Append(",ORC.VALORFINAL ");
            SqlConcatenada.Append(",ORC.DATA ");
            SqlConcatenada.Append(",CLI.NOME AS NOMECLIENTE ");
            SqlConcatenada.Append("FROM ORCAMENTOS ORC ");
            SqlConcatenada.Append("INNER JOIN CLIENTES CLI ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");
            SqlConcatenada.Append(" WHERE ORC.STATUSVENDIDO = 'EM ABERTO' AND ORC.DATA BETWEEN @DATAINICIAL AND @DATAFINAL ");
            SqlConcatenada.Append("ORDER BY ORC.CODIGO ");

            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametroData1 = new SqlParameter("@DATAINICIAL", DateTime.Now.AddDays(-7));
            SqlParameter parametroData2 = new SqlParameter("@DATAFINAL", DateTime.Now); //retorna orçamentos em aberto de até uma semana

            DataAdap.SelectCommand.Parameters.Add(parametroData1);
            DataAdap.SelectCommand.Parameters.Add(parametroData2);

            try
            {
                DataAdap.Fill(DsDataSet);

                iModOrcamento[] orcamentos = new iModOrcamento[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset

                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    orcamentos[i] = new iModOrcamento(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    orcamentos[i].PkCodigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["CODIGO"].ToString());
                    orcamentos[i].cliente = new iModCliente(); //crio um novo objeto cliente dentro deste objeto orcamento
                    orcamentos[i].cliente.Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["FK_NUMCLIENTE"].ToString());
                    orcamentos[i].cliente.Nome = DsDataSet.Tables[0].Rows[i]["NOMECLIENTE"].ToString();
                    orcamentos[i].InfoAdicional = DsDataSet.Tables[0].Rows[i]["INFOADICIONAL"].ToString();
                    orcamentos[i].Status = DsDataSet.Tables[0].Rows[i]["STATUSVENDIDO"].ToString();
                    orcamentos[i].ValorFinal = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORFINAL"].ToString());                    
                    orcamentos[i].DataOrc = Convert.ToDateTime(DsDataSet.Tables[0].Rows[i]["DATA"].ToString());
                }
                return orcamentos;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "newObterOrcamentosOSAberto()", erro.Message.ToString(), "Obter Orcamentos e OS em Aberto");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                DataAdap = null;
                SqlConcatenada = null;
                DsDataSet = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Obter informacoes sobre um Orcamento
        public iModOrcamento[] dObterOrcamentoPorID(iModOrcamento objOrcamento)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            StringBuilder SqlConcatenada = new StringBuilder();

            SqlConcatenada.Append("SELECT  ");
            SqlConcatenada.Append("ORC.CODIGO ");
            SqlConcatenada.Append(",ORC.FK_NUMCLIENTE ");
            SqlConcatenada.Append(",ORC.INFOADICIONAL ");
            SqlConcatenada.Append(",ORC.STATUSVENDIDO ");
            SqlConcatenada.Append(",ORC.VALORFINAL ");
            SqlConcatenada.Append(",ORC.DATA ");
            SqlConcatenada.Append(",CLI.NOME AS NOMECLIENTE ");

            SqlConcatenada.Append("FROM ORCAMENTOS ORC ");
            SqlConcatenada.Append("INNER JOIN CLIENTES CLI ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");

            SqlConcatenada.Append("WHERE ORC.CODIGO = @CODIGO ");
            SqlConcatenada.Append("ORDER BY ORC.CODIGO ");

            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametroData1 = new SqlParameter("@CODIGO", objOrcamento.PkCodigo);
            DataAdap.SelectCommand.Parameters.Add(parametroData1);

            try
            {
                DataAdap.Fill(DsDataSet);

                iModOrcamento[] orcamentos = new iModOrcamento[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset

                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    orcamentos[i] = new iModOrcamento(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    orcamentos[i].PkCodigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["FK_CODIGO"].ToString());
                    orcamentos[i].cliente.Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["FK_NUMCLIENTE"].ToString());
                    orcamentos[i].InfoAdicional = DsDataSet.Tables[0].Rows[i]["INFOADICIONAL"].ToString();
                    orcamentos[i].Status = DsDataSet.Tables[0].Rows[i]["STATUSVENDIDO"].ToString();
                    orcamentos[i].ValorFinal = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["VALORFINAL"].ToString());
                    orcamentos[i].cliente.Nome = DsDataSet.Tables[0].Rows[i]["NOMECLIENTE"].ToString();
                }
                return orcamentos;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "newObterOrcamentosOSAberto()", erro.Message.ToString(), "Obter Orcamentos e OS em Aberto");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                DataAdap = null;
                SqlConcatenada = null;
                DsDataSet = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Metodo Retorna Dados Orcamento para Relatorio Vendas
        public DataSet dRetornaDadosOrcamentoRelatorio(int codigo)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            SqlCommand comando = new SqlCommand(); //cria um objeto do tipo comando
            //adiciona o parametro que chama a stored procedure, e o código que será enviado

            SqlParameter parametro2 = new SqlParameter("@CODIGO", codigo);
            comando.Parameters.Add(parametro2);

            comando.CommandText = "sp_RELATORIO_DADOS_CLIENTE_PEDIDO"; //define a Stored que será chamada
            comando.CommandType = CommandType.StoredProcedure; //define que é uma StoredProcedure
            comando.Connection = conexao; //abre a conexão
            comando.ExecuteNonQuery(); //executa a query

            //cria um adaptador de dados para ler os dados que voltam
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dadosClientePedido = new DataSet();
            try
            {
                dataAdapter.Fill(dadosClientePedido);
                return dadosClientePedido; //sou obrigado a retornar DataSet - é o único tipo de objeto que o Relatório Lê...
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsOrcamento", "retornaDadosClientePedido()", erro.Message.ToString(), "Retorna os dados do Pedido e também os Itens deste Produto");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
                dadosClientePedido = null;
                dataAdapter = null;
            }
        }
        #endregion
    }
}
