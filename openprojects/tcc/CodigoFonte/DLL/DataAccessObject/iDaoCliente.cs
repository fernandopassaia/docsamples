using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.DataAccessObject
{
    public class iDaoCliente
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

        #region Model Inserir Cliente
        public bool dInsereCliente(iModCliente objCliente)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("INSERT INTO CLIENTES ");
            SqlConcatenada.Append("(PESSOAFISICAJURIDICA ");
            SqlConcatenada.Append(",CPFCNPJ ");
            SqlConcatenada.Append(",RG ");
            SqlConcatenada.Append(",NOME ");
            SqlConcatenada.Append(",RAZAOSOCIAL ");
            SqlConcatenada.Append(",INSCRESTADUAL ");
            SqlConcatenada.Append(",ESTADO ");
            SqlConcatenada.Append(",CEP ");
            SqlConcatenada.Append(",LOGRADOURO ");
            SqlConcatenada.Append(",NUMERO ");
            SqlConcatenada.Append(",BAIRRO ");
            SqlConcatenada.Append(",CIDADE ");
            SqlConcatenada.Append(",COMPLEMENTO ");
            SqlConcatenada.Append(",TELEFONE1 ");
            SqlConcatenada.Append(",TELEFONE2 ");
            SqlConcatenada.Append(",FAX ");
            SqlConcatenada.Append(",CELULAR1 ");
            SqlConcatenada.Append(",OPERADORA1 ");
            SqlConcatenada.Append(",CELULAR2 ");
            SqlConcatenada.Append(",OPERADORA2 ");
            SqlConcatenada.Append(",EMAIL ");
            SqlConcatenada.Append(",SITE ");
            SqlConcatenada.Append(",MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM ");
            if (objCliente.ImagemCliente != null)
            {
                SqlConcatenada.Append(",IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",STATUS) ");
            SqlConcatenada.Append("VALUES ");
            SqlConcatenada.Append("(@PESSOAFISICAJURIDICA ");
            SqlConcatenada.Append(",@CPFCNPJ ");
            SqlConcatenada.Append(",@RG ");
            SqlConcatenada.Append(",@NOME ");
            SqlConcatenada.Append(",@RAZAOSOCIAL ");
            SqlConcatenada.Append(",@INSCRESTADUAL ");
            SqlConcatenada.Append(",@ESTADO ");
            SqlConcatenada.Append(",@CEP ");
            SqlConcatenada.Append(",@LOGRADOURO ");
            SqlConcatenada.Append(",@NUMERO ");
            SqlConcatenada.Append(",@BAIRRO ");
            SqlConcatenada.Append(",@CIDADE ");
            SqlConcatenada.Append(",@COMPLEMENTO ");
            SqlConcatenada.Append(",@TELEFONE1 ");
            SqlConcatenada.Append(",@TELEFONE2 ");
            SqlConcatenada.Append(",@FAX ");
            SqlConcatenada.Append(",@CELULAR1 ");
            SqlConcatenada.Append(",@OPERADORA1 ");
            SqlConcatenada.Append(",@CELULAR2 ");
            SqlConcatenada.Append(",@OPERADORA2 ");
            SqlConcatenada.Append(",@EMAIL ");
            SqlConcatenada.Append(",@SITE ");
            SqlConcatenada.Append(",@MAISINFO ");
            SqlConcatenada.Append(",@POSSUI_IMAGEM ");
            if (objCliente.ImagemCliente != null)
            {
                SqlConcatenada.Append(",@IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",@STATUS) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@CODIGO", objCliente.Pk_Codigo);
            SqlParameter parametro2 = new SqlParameter("@PESSOAFISICAJURIDICA", objCliente.PessoaFisicaJuridica);
            SqlParameter parametro3 = new SqlParameter("@CPFCNPJ", objCliente.CpfCnpj);
            SqlParameter parametro4 = new SqlParameter("@RG", objCliente.Rg);
            SqlParameter parametro5 = new SqlParameter("@NOME", objCliente.Nome);
            SqlParameter parametro6 = new SqlParameter("@RAZAOSOCIAL", objCliente.RazaoSocial);
            SqlParameter parametro7 = new SqlParameter("@INSCRESTADUAL", objCliente.InscrEstadual);

            SqlParameter parametro8 = new SqlParameter("@ESTADO", objCliente.Estado);
            SqlParameter parametro9 = new SqlParameter("@CEP", objCliente.Cep);
            SqlParameter parametro10 = new SqlParameter("@LOGRADOURO", objCliente.Logradouro);
            SqlParameter parametro11 = new SqlParameter("@NUMERO", objCliente.Numero);
            SqlParameter parametro12 = new SqlParameter("@BAIRRO", objCliente.Bairro);
            SqlParameter parametro13 = new SqlParameter("@CIDADE", objCliente.Cidade);
            SqlParameter parametro14 = new SqlParameter("@COMPLEMENTO", objCliente.Complemento);
            SqlParameter parametro15 = new SqlParameter("@TELEFONE1", objCliente.Telefone1);
            SqlParameter parametro16 = new SqlParameter("@TELEFONE2", objCliente.Telefone2);

            SqlParameter parametro17 = new SqlParameter("@EMAIL", objCliente.Email);

            SqlParameter parametro18 = new SqlParameter("@SITE", objCliente.Site);
            SqlParameter parametro19 = new SqlParameter("@MAISINFO", objCliente.MaisInfo);
            SqlParameter parametro20 = new SqlParameter("@STATUS", objCliente.Status);

            if (objCliente.ImagemCliente != null)
            {
                SqlParameter parametroImagem = new SqlParameter("@IMAGEM_BINARIO", objCliente.ImagemCliente);
                comando.Parameters.Add(parametroImagem);
                SqlParameter parametroImagem2 = new SqlParameter("@POSSUI_IMAGEM", true);
                comando.Parameters.Add(parametroImagem2);
            }
            else
            {
                SqlParameter parametroImagem = new SqlParameter("@POSSUI_IMAGEM", false);
                comando.Parameters.Add(parametroImagem);
            }


            SqlParameter parametro21 = new SqlParameter("@CELULAR1", objCliente.Celular1);
            SqlParameter parametro22 = new SqlParameter("@OPERADORA1", objCliente.Operadora1);
            SqlParameter parametro23 = new SqlParameter("@CELULAR2", objCliente.Celular2);
            SqlParameter parametro24 = new SqlParameter("@OPERADORA2", objCliente.Operadora2);
            SqlParameter parametro25 = new SqlParameter("@FAX", objCliente.Fax);

            comando.Parameters.Add(parametro);
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
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);
            comando.Parameters.Add(parametro17);
            comando.Parameters.Add(parametro18);
            comando.Parameters.Add(parametro19);
            comando.Parameters.Add(parametro20);
            comando.Parameters.Add(parametro21);
            comando.Parameters.Add(parametro22);
            comando.Parameters.Add(parametro23);
            comando.Parameters.Add(parametro24);
            comando.Parameters.Add(parametro25);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "incluir()", erro.Message.ToString(), "Incluir o Cliente");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                parametro = null;
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
                parametro13 = null;
                parametro14 = null;
                parametro15 = null;
                parametro16 = null;
                parametro17 = null;
                parametro18 = null;
                parametro19 = null;
                parametro20 = null;
                parametro21 = null;
                parametro22 = null;
                parametro23 = null;
                parametro24 = null;
                parametro25 = null;
            }
        }
        #endregion

        #region Método Alterar Cliente
        public bool dAlteraCliente(iModCliente objCliente)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE CLIENTES ");
            SqlConcatenada.Append("SET PESSOAFISICAJURIDICA = @PESSOAFISICAJURIDICA ");
            SqlConcatenada.Append(",CPFCNPJ = @CPFCNPJ ");
            SqlConcatenada.Append(",RG = @RG ");
            SqlConcatenada.Append(",NOME = @NOME ");
            SqlConcatenada.Append(",RAZAOSOCIAL = @RAZAOSOCIAL ");
            SqlConcatenada.Append(",INSCRESTADUAL = @INSCRESTADUAL ");
            SqlConcatenada.Append(",ESTADO = @ESTADO ");
            SqlConcatenada.Append(",CEP = @CEP ");
            SqlConcatenada.Append(",LOGRADOURO = @LOGRADOURO ");
            SqlConcatenada.Append(",NUMERO = @NUMERO ");
            SqlConcatenada.Append(",BAIRRO = @BAIRRO ");
            SqlConcatenada.Append(",CIDADE = @CIDADE ");
            SqlConcatenada.Append(",COMPLEMENTO = @COMPLEMENTO ");
            SqlConcatenada.Append(",TELEFONE1 = @TELEFONE1 ");
            SqlConcatenada.Append(",TELEFONE2 = @TELEFONE2 ");
            SqlConcatenada.Append(",FAX = @FAX ");
            SqlConcatenada.Append(",CELULAR1 = @CELULAR1 ");
            SqlConcatenada.Append(",OPERADORA1 = @OPERADORA1 ");
            SqlConcatenada.Append(",CELULAR2 = @CELULAR2 ");
            SqlConcatenada.Append(",OPERADORA2 = @OPERADORA2 ");
            SqlConcatenada.Append(",EMAIL = @EMAIL ");
            SqlConcatenada.Append(",SITE = @SITE ");
            SqlConcatenada.Append(",MAISINFO = @MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM = @POSSUI_IMAGEM ");
            if (objCliente.ImagemCliente != null)
            {
                SqlConcatenada.Append(",IMAGEM_BINARIO = @IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",STATUS = @STATUS ");
            SqlConcatenada.Append("WHERE CODIGO = @CODIGO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@CODIGO", objCliente.Pk_Codigo);
            SqlParameter parametro2 = new SqlParameter("@PESSOAFISICAJURIDICA", objCliente.PessoaFisicaJuridica);
            SqlParameter parametro3 = new SqlParameter("@CPFCNPJ", objCliente.CpfCnpj);
            SqlParameter parametro4 = new SqlParameter("@RG", objCliente.Rg);
            SqlParameter parametro5 = new SqlParameter("@NOME", objCliente.Nome);
            SqlParameter parametro6 = new SqlParameter("@RAZAOSOCIAL", objCliente.RazaoSocial);
            SqlParameter parametro7 = new SqlParameter("@INSCRESTADUAL", objCliente.InscrEstadual);

            SqlParameter parametro8 = new SqlParameter("@ESTADO", objCliente.Estado);
            SqlParameter parametro9 = new SqlParameter("@CEP", objCliente.Cep);
            SqlParameter parametro10 = new SqlParameter("@LOGRADOURO", objCliente.Logradouro);
            SqlParameter parametro11 = new SqlParameter("@NUMERO", objCliente.Numero);
            SqlParameter parametro12 = new SqlParameter("@BAIRRO", objCliente.Bairro);
            SqlParameter parametro13 = new SqlParameter("@CIDADE", objCliente.Cidade);
            SqlParameter parametro14 = new SqlParameter("@COMPLEMENTO", objCliente.Complemento);
            SqlParameter parametro15 = new SqlParameter("@TELEFONE1", objCliente.Telefone1);
            SqlParameter parametro16 = new SqlParameter("@TELEFONE2", objCliente.Telefone2);

            SqlParameter parametro17 = new SqlParameter("@EMAIL", objCliente.Email);

            SqlParameter parametro18 = new SqlParameter("@SITE", objCliente.Site);
            SqlParameter parametro19 = new SqlParameter("@MAISINFO", objCliente.MaisInfo);

            SqlParameter parametro20 = new SqlParameter("@STATUS", objCliente.Status);

            if (objCliente.ImagemCliente != null)
            {
                SqlParameter parametroImagem = new SqlParameter("@IMAGEM_BINARIO", objCliente.ImagemCliente);
                comando.Parameters.Add(parametroImagem);
                SqlParameter parametroImagem2 = new SqlParameter("@POSSUI_IMAGEM", true);
                comando.Parameters.Add(parametroImagem2);
            }
            else
            {
                SqlParameter parametroImagem = new SqlParameter("@POSSUI_IMAGEM", false);
                comando.Parameters.Add(parametroImagem);
            }


            SqlParameter parametro21 = new SqlParameter("@CELULAR1", objCliente.Celular1);
            SqlParameter parametro22 = new SqlParameter("@OPERADORA1", objCliente.Operadora1);
            SqlParameter parametro23 = new SqlParameter("@CELULAR2", objCliente.Celular2);
            SqlParameter parametro24 = new SqlParameter("@OPERADORA2", objCliente.Operadora2);
            SqlParameter parametro25 = new SqlParameter("@FAX", objCliente.Fax);

            comando.Parameters.Add(parametro);
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
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);
            comando.Parameters.Add(parametro17);
            comando.Parameters.Add(parametro18);
            comando.Parameters.Add(parametro19);
            comando.Parameters.Add(parametro20);
            comando.Parameters.Add(parametro21);
            comando.Parameters.Add(parametro22);
            comando.Parameters.Add(parametro23);
            comando.Parameters.Add(parametro24);
            comando.Parameters.Add(parametro25);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "alterar()", erro.Message.ToString(), "Alterar o Cliente");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                parametro = null;
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
                parametro13 = null;
                parametro14 = null;
                parametro15 = null;
                parametro16 = null;
                parametro17 = null;
                parametro18 = null;
                parametro19 = null;
                parametro20 = null;
                parametro21 = null;
                parametro22 = null;
                parametro23 = null;
                parametro24 = null;
                parametro25 = null;
            }
        }
        #endregion

        #region Método Exclui Cliente
        public bool dExcluiCliente(iModCliente objCliente)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append(" UPDATE CLIENTES ");
            SqlConcatenada.Append(" SET ");
            SqlConcatenada.Append(" STATUS = 'EXCLUIDO'");
            SqlConcatenada.Append(" WHERE ");
            SqlConcatenada.Append(" CODIGO = @codigo");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@codigo", objCliente.Pk_Codigo);
            comando.Parameters.Add(parametro);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }


        //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "excluir()", erro.Message.ToString(), "Excluir o Cliente");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                parametro = null;
                comando = null;
                SqlConcatenada = null;
            }
        }
        #endregion

        #region Método Obtem Cliente
        public iModCliente[] dObterCliente()
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("SELECT CODIGO ");
            SqlConcatenada.Append(",PESSOAFISICAJURIDICA ");
            SqlConcatenada.Append(",CPFCNPJ ");
            SqlConcatenada.Append(",RG ");
            SqlConcatenada.Append(",NOME ");
            SqlConcatenada.Append(",RAZAOSOCIAL ");
            SqlConcatenada.Append(",INSCRESTADUAL ");
            SqlConcatenada.Append(",ESTADO ");
            SqlConcatenada.Append(",CEP ");
            SqlConcatenada.Append(",LOGRADOURO ");
            SqlConcatenada.Append(",NUMERO ");
            SqlConcatenada.Append(",BAIRRO ");
            SqlConcatenada.Append(",CIDADE ");
            SqlConcatenada.Append(",COMPLEMENTO ");
            SqlConcatenada.Append(",TELEFONE1 ");
            SqlConcatenada.Append(",TELEFONE2 ");
            SqlConcatenada.Append(",FAX ");
            SqlConcatenada.Append(",CELULAR1 ");
            SqlConcatenada.Append(",OPERADORA1 ");
            SqlConcatenada.Append(",CELULAR2 ");
            SqlConcatenada.Append(",OPERADORA2 ");
            SqlConcatenada.Append(",EMAIL ");
            SqlConcatenada.Append(",SITE ");
            SqlConcatenada.Append(",MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM ");
            SqlConcatenada.Append(",IMAGEM_BINARIO ");
            SqlConcatenada.Append(",STATUS ");
            SqlConcatenada.Append("FROM CLIENTES WHERE STATUS='ATIVO' ORDER BY CODIGO");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet, "CLIENTES");
                
                iModCliente[] clientes = new iModCliente[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset
                

                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    clientes[i] = new iModCliente(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    clientes[i].Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["CODIGO"].ToString());
                    clientes[i].PessoaFisicaJuridica = DsDataSet.Tables[0].Rows[i]["PESSOAFISICAJURIDICA"].ToString();
                    clientes[i].CpfCnpj = DsDataSet.Tables[0].Rows[i]["CPFCNPJ"].ToString();
                    clientes[i].Rg = DsDataSet.Tables[0].Rows[i]["RG"].ToString();
                    clientes[i].Nome = DsDataSet.Tables[0].Rows[i]["NOME"].ToString();
                    clientes[i].RazaoSocial = DsDataSet.Tables[0].Rows[i]["RAZAOSOCIAL"].ToString();
                    clientes[i].InscrEstadual = DsDataSet.Tables[0].Rows[i]["INSCRESTADUAL"].ToString();
                    clientes[i].Estado = DsDataSet.Tables[0].Rows[i]["ESTADO"].ToString();
                    clientes[i].Cep = DsDataSet.Tables[0].Rows[i]["CEP"].ToString();
                    clientes[i].Logradouro = DsDataSet.Tables[0].Rows[i]["LOGRADOURO"].ToString();
                    clientes[i].Numero = DsDataSet.Tables[0].Rows[i]["NUMERO"].ToString();
                    clientes[i].Bairro = DsDataSet.Tables[0].Rows[i]["BAIRRO"].ToString();
                    clientes[i].Cidade = DsDataSet.Tables[0].Rows[i]["CIDADE"].ToString();
                    clientes[i].Complemento = DsDataSet.Tables[0].Rows[i]["COMPLEMENTO"].ToString();
                    clientes[i].Telefone1 = DsDataSet.Tables[0].Rows[i]["TELEFONE1"].ToString();
                    clientes[i].Telefone2 = DsDataSet.Tables[0].Rows[i]["TELEFONE2"].ToString();
                    clientes[i].Fax = DsDataSet.Tables[0].Rows[i]["FAX"].ToString();
                    clientes[i].Celular1 = DsDataSet.Tables[0].Rows[i]["CELULAR1"].ToString();
                    clientes[i].Operadora1 = DsDataSet.Tables[0].Rows[i]["OPERADORA1"].ToString();
                    clientes[i].Celular2 = DsDataSet.Tables[0].Rows[i]["CELULAR2"].ToString();
                    clientes[i].Operadora2 = DsDataSet.Tables[0].Rows[i]["OPERADORA2"].ToString();
                    clientes[i].Email = DsDataSet.Tables[0].Rows[i]["EMAIL"].ToString();
                    clientes[i].Site = DsDataSet.Tables[0].Rows[i]["SITE"].ToString();
                    clientes[i].MaisInfo = DsDataSet.Tables[0].Rows[i]["MAISINFO"].ToString();
                    clientes[i].PossuiImagem = Convert.ToBoolean(DsDataSet.Tables[0].Rows[i]["POSSUI_IMAGEM"].ToString());      
                }

                return clientes;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "obterCad()", erro.Message.ToString(), "Alterar o Cliente");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DsDataSet = null;
                DataAdap = null;
                SqlConcatenada = null;
            }
        }
        #endregion

        #region Método para Recuperar a Imagem do Cliente no Banco
        public Image dRecuperarImagemClienteNoBanco(iModCliente objCliente)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand k = new SqlCommand("SELECT IMAGEM_BINARIO FROM CLIENTES WHERE CODIGO = @CODIGO", conexao);
            SqlParameter param = new SqlParameter("@CODIGO", objCliente.Pk_Codigo);
            k.Parameters.Add(param);
            try
            {
                byte[] imagemEmBytes = (byte[])k.ExecuteScalar();
                MemoryStream ms = new MemoryStream();
                ms.Write(imagemEmBytes, 0, imagemEmBytes.Length);
                return Image.FromStream(ms);
            }
            catch (Exception erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "recuperarImagemClienteBanco()", erro.Message.ToString(), "Recuperar Imagem do Cliente no Banco");
                return null;
            }
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }
        #endregion

        #region Obter Orçamentos e Vendas do Cliente no Banco de Dados
        public bool dObterOrcamentosEVendasDoCliente(iModCliente objCliente)//Carrega um DataSet com o banco de dados
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("SELECT  ");
            SqlConcatenada.Append("CODIGO = ORCT.CODIGO, ");
            SqlConcatenada.Append("DATACADASTRO = RTRIM(LTRIM(ORCT.LOG_DATACRIACAO)), ");
            SqlConcatenada.Append("NOMEVENDEDOR = RTRIM(LTRIM(USU.NOMEFANTASIA)), ");
            SqlConcatenada.Append("VALORVENDA = RTRIM(LTRIM(ORCT.VALORFINAL)), ");
            SqlConcatenada.Append("STATUSVENDA = RTRIM(LTRIM(ORCT.STATUSVENDIDO)) ");
            SqlConcatenada.Append("FROM ORCAMENTOS ORCT ");
            SqlConcatenada.Append("INNER JOIN TB_VENDEDORES222 USU ");
            SqlConcatenada.Append("ON ORCT.FK_NUMVENDEDOR = USU.PK_CODIGO ");
            SqlConcatenada.Append("WHERE ORCT.FK_NUMCLIENTE=@CODIGO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();

            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@CODIGO", objCliente.Pk_Codigo);
            comando.Parameters.Add(parametro);

            SqlDataAdapter DataAdap = new SqlDataAdapter(comando);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet);
                //ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "obterOrcamentosEVendasDoCliente()", erro.Message.ToString(), "Obter Orcamentos e Vendas do Cliente");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DsDataSet = null;
                DataAdap = null;
                SqlConcatenada = null;
                parametro = null;
            }
        }//fim do método de ObterCadastro
        #endregion
    }//fim classe
}//fim namespace
