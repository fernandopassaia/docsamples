using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DllFuturaDataGestaoInfoSigaStandart.Utilitarios;
using System.Data;

namespace DllFuturaDataGestaoInfoSigaStandart
{
    public class clsConfiguracoes
    {
        // Essa é a String de Conexão convertida pelo Settings da Aplicação
        private string ConexaoBanco = new clsConexao().recuperaStringConexaoSQLServer();

        #region Abrir Conexao BD
        /// <summary>
        /// Esse método é apenas usado interno a Classe, ele tenta abrir conexão com banco
        /// </summary>
        /// <returns>Retorna objeto de conexão aberto</returns>
        private SqlConnection AbrirConexaoBd()
        {
            try
            {
                SqlConnection ConexaoBd = new SqlConnection(ConexaoBanco);
                ConexaoBd.Open();
                return ConexaoBd;
            }
            catch (SqlException erro)
            {
                throw erro;
            }
        }
        #endregion

        #region Fechar Conexao BD
        /// <summary>
        /// Esse método é usado interno a Classe, ele fecha a conexão após uma operação
        /// </summary>
        /// <returns>Retorna objeto de conexão fechado</returns>
        private SqlConnection FecharConexaoBd()
        {
            SqlConnection ConexaoBd = new SqlConnection(ConexaoBanco);
            ConexaoBd.Close();
            return ConexaoBd;
        }
        #endregion

        #region Gravar Papel Parede do Usuario
        /// <summary>
        /// Grava o Papel de Parede do Usuario no banco de dados de acordo com o clique no papel de parede
        /// </summary>
        /// <param name="numeroPapelParede">Numero do Papel a ser gravado</param>
        /// <param name="numeroUsuarioLogado">Numero do Usuario logado no sistema</param>
        /// <param name="nomeHost">Nome do Host em que está logado</param>
        /// <returns>retorna true se conseguir gravar, false se não</returns>
        public bool alterarPapelParedeUsuario(int numeroPapelParede, int numeroUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append(" UPDATE ISCONFIGUSUA889 SET ");
            SqlConcatenada.Append(" NUMEROPAPELPAREDE = @numeroPapelParede"); //se for checada é fisica, senão, juridica

            SqlConcatenada.Append(" WHERE  ");

            SqlConcatenada.Append("FK_NUMUSUARIO = @fk_NumUsuario"); //se for checada é fisica, senão, juridica

            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), AbrirConexaoBd());

            SqlParameter parametro = new SqlParameter("@fk_NumUsuario", numeroUsuarioLogado);
            comando.Parameters.Add(parametro);

            SqlParameter parametro1 = new SqlParameter("@numeroPapelParede", numeroPapelParede); //se for checada é fisica, senão, juridica
            comando.Parameters.Add(parametro1);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco
                FecharConexaoBd();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (Exception erro)
            {
                clsControleLog gerarLog = new clsControleLog();
                gerarLog.gravaLOGnoServidor(numeroUsuarioLogado, nomeHost, "clsConfiguracoes", "GravarPapelParedeUsuario()", erro.Message.ToString(), "Gravar Papel Parede Usuario");
                gerarLog.gravaLOGnoClient(numeroUsuarioLogado, nomeHost, "clsConfiguracoes", "GravarPapelParedeUsuario()", erro.Message.ToString(), "Gravar Papel Parede Usuario");

                FecharConexaoBd();
                return false;
            }

        }//fim do método de incluir
        #endregion

        #region Retorna Modelo da Impressora Gravada no Banco
        /// <summary>
        /// Retorna o Modelo da Impressora que está salvo e Configurado no Banco
        /// </summary>
        /// <returns>Retorna "Bematech", ou "Sweda", ou "Daruma", ou "Epson", etc</returns>
        public string retornaModeloImpressora(int numeroUsuarioLogado, string nomeHost)
        {
            SqlDataReader modeloImpressora;
            try
            {
                SqlCommand ComandoSQL = new SqlCommand();
                ComandoSQL.Connection = AbrirConexaoBd();
                ComandoSQL.CommandType = CommandType.Text;
                ComandoSQL.CommandText = "SELECT MODELOIMPRESSORA FROM ISCONFIGSIST635";
                ComandoSQL.ExecuteNonQuery();
                FecharConexaoBd();

                modeloImpressora = ComandoSQL.ExecuteReader(); //retorna um objeto do tipo dataReader
                modeloImpressora.Read();
                return modeloImpressora.GetString(0).Trim();
            }

            catch (SqlException erro)
            {
                clsControleLog gerarLog = new clsControleLog();
                gerarLog.gravaLOGnoServidor(numeroUsuarioLogado, nomeHost, "clsOperacoesFiscais", "retornaModeloImpressora()", erro.Message.ToString(), "Retorna Modelo da Impressora Gravado no banco");
                gerarLog.gravaLOGnoClient(numeroUsuarioLogado, nomeHost, "clsOperacoesFiscais", "retornaModeloImpressora()", erro.Message.ToString(), "Retorna Modelo da Impressora Gravado no banco");

                FecharConexaoBd();
                return "";
            }
        }
        #endregion        
    }//fim classe
}//fim namespace
