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
    public class iDaoEstoque
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }

        DataSet ds_DadosRetorno;

        public DataSet Ds_DadosRetorno
        {
            get { return ds_DadosRetorno; }
            set { ds_DadosRetorno = value; }
        }
        #endregion


        #region Efetua Baixa do Estoque
        public bool dEfetuaMovimentoDoEstoque(iModEstoque objEstoque)
        {
            string vSql = "";

            if (objEstoque.EntradaOuSaida == 1)
            {
                vSql = "UPDATE PRODUTOS SET QTD_ATUAL=QTD_ATUAL+@QTD WHERE PK_CODIGOSIST = @CODIGOPRODUTO";
            }
            else
            {
                vSql = "UPDATE PRODUTOS SET QTD_ATUAL=QTD_ATUAL-@QTD WHERE PK_CODIGOSIST = @CODIGOPRODUTO";
            }


            SqlConnection vConexao = new clsConexao().abrirConexaoBd();
            SqlCommand vComando = new SqlCommand(vSql, vConexao);

            SqlParameter param1 = new SqlParameter("@QTD", objEstoque.Qtd);
            SqlParameter param2 = new SqlParameter("@CODIGOPRODUTO", objEstoque.produto.Pk_Codigo);
            vComando.Parameters.Add(param2);
            vComando.Parameters.Add(param1);

            try
            {
                vComando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsEstoque", "efetuaBaixaDeEstoqueNaVendaProduto()", erro.Message.ToString(), "Efetua Baixa do Estoque na Venda de um Produto");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                vConexao.Close();
                vComando.Dispose();
                vConexao.Dispose();
            }
        }
        #endregion

        #region Incluir Movimentacao Estoque
        public bool dIncluirMovimentacaoEstoque(iModEstoque objEstoque)
        {
            StringBuilder incluir = new StringBuilder();
            incluir.Append("INSERT INTO MOVIMENTOESTOQUE ");
            incluir.Append("(FK_CODIGOPEDIDO ");
            incluir.Append(",FK_CODIGOPRODUTO ");
            incluir.Append(",FK_CODIGOCAIXA ");
            incluir.Append(",FK_CODIGOCLIENTE ");
            incluir.Append(",IDENTIFICACAO_CAIXA ");
            incluir.Append(",DATAMOVIMENTACAO ");
            incluir.Append(",VALORCUSTO ");
            incluir.Append(",MARGEM_LUCRO ");
            incluir.Append(",VALORVENDA ");
            incluir.Append(",QUANTIDADE ");
            incluir.Append(",VALORTOTAL ");
            incluir.Append(",TIPO_ES ");
            incluir.Append(",MAISINFO) ");
            incluir.Append("VALUES ");
            incluir.Append("(@FK_CODIGOPEDIDO ");
            incluir.Append(",@FK_CODIGOPRODUTO ");
            incluir.Append(",@FK_CODIGOCAIXA ");
            incluir.Append(",@FK_CODIGOCLIENTE ");
            incluir.Append(",@IDENTIFICACAO_CAIXA ");
            incluir.Append(",@DATAMOVIMENTACAO ");
            incluir.Append(",@VALORCUSTO ");
            incluir.Append(",@MARGEM_LUCRO ");
            incluir.Append(",@VALORVENDA ");
            incluir.Append(",@QUANTIDADE ");
            incluir.Append(",@VALORTOTAL ");
            incluir.Append(",@TIPO_ES ");
            incluir.Append(",@MAISINFO) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(incluir.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@FK_CODIGOPEDIDO", objEstoque.orcamento.PkCodigo);
            SqlParameter parametro2 = new SqlParameter("@FK_CODIGOCAIXA", objEstoque.caixa.IdCaixa);
            SqlParameter parametro3 = new SqlParameter("@FK_CODIGOCLIENTE", objEstoque.cliente.Pk_Codigo);
            SqlParameter parametro4 = new SqlParameter("@IDENTIFICACAO_CAIXA", objEstoque.caixa.IdentificacaoCaixa);
            SqlParameter parametro5 = new SqlParameter("@DATAMOVIMENTACAO", DateTime.Now);
            SqlParameter parametro6 = new SqlParameter("@VALORCUSTO", objEstoque.ValorCusto);
            SqlParameter parametro7 = new SqlParameter("@MARGEM_LUCRO", objEstoque.MargemLucro);
            SqlParameter parametro8 = new SqlParameter("@VALORVENDA", objEstoque.ValorVenda);
            SqlParameter parametro9 = new SqlParameter("@QUANTIDADE", objEstoque.Qtd);
            SqlParameter parametro10 = new SqlParameter("@VALORTOTAL", objEstoque.ValorTotal);
            SqlParameter parametro11 = new SqlParameter("@TIPO_ES", objEstoque.EntradaOuSaida);
            SqlParameter parametro12 = new SqlParameter("@MAISINFO", objEstoque.MaisInfo);
            SqlParameter parametro13 = new SqlParameter("@FK_CODIGOPRODUTO", objEstoque.produto.Pk_Codigo);

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro6);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);
            comando.Parameters.Add(parametro9);
            comando.Parameters.Add(parametro10);
            comando.Parameters.Add(parametro11);
            comando.Parameters.Add(parametro12);
            comando.Parameters.Add(parametro13);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }


            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "incluirMovimentacaoEstoque()", erro.Message.ToString(), "Incluir Movimentação do Estoque");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                comando = null;
                incluir = null;

                parametro1 = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
                parametro5 = null;
                parametro6 = null;
                parametro7 = null;
                parametro8 = null;
                parametro9 = null;
                parametro10 = null;
                parametro11 = null;
                parametro12 = null;
            }
        }
        //fim da função GravarProduto()
        #endregion

        #region Obter movimentação de estoque por produto
        public bool dObterMovimentacaoEstoquePorIDProduto(iModEstoque objEstoque)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT EST.PK_ID ");
            consultarPedidos.Append(",EST.FK_CODIGOPEDIDO ");
            consultarPedidos.Append(",EST.FK_CODIGOPRODUTO ");
            consultarPedidos.Append(",PRO.DESCRICAO + '-' + PRO.APLICACAO AS DESCRICAOPROD ");
            consultarPedidos.Append(",PRO.CODIGOFABRIC AS CODIGOFABRICANTE ");
            consultarPedidos.Append(",EST.FK_CODIGOCAIXA ");
            consultarPedidos.Append(",EST.FK_CODIGOCLIENTE ");
            consultarPedidos.Append(",EST.IDENTIFICACAO_CAIXA ");
            consultarPedidos.Append(",EST.DATAMOVIMENTACAO ");
            consultarPedidos.Append(",EST.VALORCUSTO ");
            consultarPedidos.Append(",EST.MARGEM_LUCRO ");
            consultarPedidos.Append(",EST.VALORVENDA ");
            consultarPedidos.Append(",EST.QUANTIDADE ");
            consultarPedidos.Append(",EST.VALORTOTAL ");
            consultarPedidos.Append(",EST.TIPO_ES ");
            consultarPedidos.Append(",EST.MAISINFO ");
            consultarPedidos.Append("FROM MOVIMENTOESTOQUE EST ");
            consultarPedidos.Append("INNER JOIN dbo.PRODUTOS PRO ");
            consultarPedidos.Append("ON PRO.PK_CODIGOSIST = EST.FK_CODIGOPRODUTO ");
            consultarPedidos.Append("WHERE EST.FK_CODIGOPRODUTO = @ID ORDER BY PK_ID DESC ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@ID", objEstoque.produto.Pk_Codigo);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);

                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterMovimentacaoEstoquePorIDCaixa()", erro.Message.ToString(), "Obter Movimentação de Estoque Fechados por um Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Obter Dados de um Produto para Estorno
        public bool dObterDadosDeUmProdutoParaEstorno(iModEstoque objEstoque)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  ");
            builder.Append("ORC.CODIGO AS CODIGOORC,  ");
            builder.Append("CLI.NOME,  ");
            builder.Append("CLI.CODIGO AS CODIGOCLI,  ");
            builder.Append("ORC.VALORFINAL,  ");
            
            builder.Append("ITEMORC.QUANTIDADE,  ");
            builder.Append("ITEMORC.VALORTOTAL AS VALORTOTALITEM,  ");
            builder.Append("ITEMORC.PK_ID AS PK_IDVENDA, ");
            builder.Append("PRO.PK_CODIGOSIST,  ");
            builder.Append("PRO.DESCRICAO,  ");
            builder.Append("PRO.APLICACAO,  ");
            builder.Append("PRO.CODIGOFABRIC AS CODIGOPRODFABRIC ");
            builder.Append("FROM ITEMSORCAMENTO ITEMORC  ");
            builder.Append("INNER JOIN ORCAMENTOS ORC  ");
            builder.Append("ON ORC.CODIGO = ITEMORC.FK_NUMORCAMENTO  ");
            builder.Append("INNER JOIN CLIENTES CLI  ");
            builder.Append("ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");
            builder.Append("INNER JOIN PRODUTOS PRO  ");
            builder.Append("ON PRO.PK_CODIGOSIST = ITEMORC.FK_NUMPRODUTO  ");
            builder.Append("WHERE ITEMORC.PK_ID = @CODIGOIDVENDA ");

            SqlConnection conexao = new clsConexao().abrirConexaoBd();

            SqlParameter parametro = new SqlParameter("@CODIGOIDVENDA", objEstoque.produto.Pk_Codigo);

            SqlDataAdapter DataAdap = new SqlDataAdapter(builder.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsEstoque", "obterDadosDeUmProdutoParaEstorno()", erro.Message.ToString(), "Obter ID de um Produto para Efetuar o Estorno");
                return false;
            }
            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion
    }
}
