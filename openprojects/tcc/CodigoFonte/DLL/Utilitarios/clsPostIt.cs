using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsPostIt
    {
        #region Variaveis Internas da Classe
        string erroClasse { get; set; }
        private clsConexao clsConexao = new clsConexao();
        private DataSet ds_DadosRetorno = new DataSet();

        /// <summary>
        /// Método de Acesso para Atribuir um DataSet ao DataSet privado da Classe
        /// </summary>
        /// <param name="ds_DadosRet">DataSet que será atribuido</param>
        public void setDs_DadosRetorno(DataSet ds_DadosRet)
        {
            ds_DadosRetorno = ds_DadosRet;
        }

        /// <summary>
        /// Método de Acesso para Retornar um DataSet Consultado através de um Método
        /// </summary>
        /// <returns>Retorna o DataSet preenchido por um método de pesquisa</returns>
        public DataSet getDs_DadosRetorno()
        {
            return this.ds_DadosRetorno;
        }
        #endregion

        #region Incluir PostIt
        public bool incluirPostIt(string tela, int fk, string criador, string cor, string mensagem, bool urgencia, int usuario, int x, int y, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("INSERT INTO TB_POSTITS_MAYMAY ");
            SqlConcatenada.Append("(TELA ");
            SqlConcatenada.Append(",FK ");
            SqlConcatenada.Append(",DATAHORA ");
            SqlConcatenada.Append(",CRIADOR ");
            SqlConcatenada.Append(",COR ");
            SqlConcatenada.Append(",MENSAGEM ");
            SqlConcatenada.Append(",URGENCIA ");
            SqlConcatenada.Append(",FK_USUARIO ");
            SqlConcatenada.Append(",X ");
            SqlConcatenada.Append(",Y ");
            SqlConcatenada.Append(",STATUS) ");
            SqlConcatenada.Append("VALUES ");
            SqlConcatenada.Append("(@TELA ");
            SqlConcatenada.Append(",@FK ");
            SqlConcatenada.Append(",@DATAHORA ");
            SqlConcatenada.Append(",@CRIADOR ");
            SqlConcatenada.Append(",@COR ");
            SqlConcatenada.Append(",@MENSAGEM ");
            SqlConcatenada.Append(",@URGENCIA ");
            SqlConcatenada.Append(",@FK_USUARIO ");
            SqlConcatenada.Append(",@X ");
            SqlConcatenada.Append(",@Y ");
            SqlConcatenada.Append(",@STATUS) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@TELA", tela);
            SqlParameter parametro2 = new SqlParameter("@FK", fk);
            SqlParameter parametro3 = new SqlParameter("@DATAHORA", DateTime.Now);
            SqlParameter parametro4 = new SqlParameter("@CRIADOR", criador);
            SqlParameter parametro5 = new SqlParameter("@COR", cor);
            SqlParameter parametro6 = new SqlParameter("@MENSAGEM", mensagem);
            SqlParameter parametro7 = new SqlParameter("@URGENCIA", urgencia);
            SqlParameter parametro8 = new SqlParameter("@FK_USUARIO", usuario);
            SqlParameter parametro9 = new SqlParameter("@X", x);
            SqlParameter parametro10 = new SqlParameter("@Y", y);
            SqlParameter parametro11 = new SqlParameter("@STATUS", "ATIVO");

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
            
            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPostIt", "incluirPostIt()", erro.Message.ToString(), "Incluir o PostIt");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                SqlConcatenada = null;
            }
        }//fim do método de incluir
        #endregion

        #region Alterar Local PostIt
        public bool alterarLocalPostIt(int pkPost, int x, int y, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE TB_POSTITS_MAYMAY ");
            SqlConcatenada.Append("SET X = @X ");
            SqlConcatenada.Append(",Y = @Y ");            
            SqlConcatenada.Append("WHERE PK = @PK ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@X", x);
            SqlParameter parametro2 = new SqlParameter("@Y", y);
            SqlParameter parametro3 = new SqlParameter("@PK", pkPost);

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            
            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPostIt", "alterarLocalPostIt()", erro.Message.ToString(), "Altera o Local do PostIt");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                SqlConcatenada = null;
            }
        }//fim do método de incluir
        #endregion

        #region Alterar Local PostIt
        public bool excluirPostIt(int pkPost, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE TB_POSTITS_MAYMAY ");
            SqlConcatenada.Append("SET STATUS = 'EXCLUIDO' ");            
            SqlConcatenada.Append("WHERE PK = @PK ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);
                        
            SqlParameter parametro1 = new SqlParameter("@PK", pkPost);

            comando.Parameters.Add(parametro1);
            
            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPostIt", "excluirPostIt()", erro.Message.ToString(), "Excluir Post IT");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                SqlConcatenada = null;
            }
        }//fim do método de incluir
        #endregion

        #region Obter PostIts Por Tela e FK
        public bool obterPostItsPorTelaEFK(string tela, int fk, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("SELECT PK ");
            SqlConcatenada.Append(",TELA");
            SqlConcatenada.Append(",FK");
            SqlConcatenada.Append(",DATAHORA");
            SqlConcatenada.Append(",CRIADOR");
            SqlConcatenada.Append(",COR");
            SqlConcatenada.Append(",MENSAGEM");
            SqlConcatenada.Append(",URGENCIA");
            SqlConcatenada.Append(",FK_USUARIO");
            SqlConcatenada.Append(",X");
            SqlConcatenada.Append(",Y");
            SqlConcatenada.Append(",STATUS ");
            SqlConcatenada.Append("FROM TB_POSTITS_MAYMAY WHERE STATUS = 'ATIVO' AND FK = @FK AND TELA = @TELA");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@FK", fk);
            SqlParameter parametro2 = new SqlParameter("@TELA", tela);            

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);

            SqlDataAdapter Adap = new SqlDataAdapter(comando);
            DataSet dsDados = new DataSet();
            try
            {
                Adap.Fill(dsDados);
                setDs_DadosRetorno(dsDados);
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPostIt", "obterPostsItsPorTelaEFK()", erro.Message.ToString(), "Obter Post Its por FK e Tela");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                SqlConcatenada = null;
            }
        }//fim do método de incluir
        #endregion
    }//fim classe
}//fim namespace
