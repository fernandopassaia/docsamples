using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using DllFuturaDataCriptografia;

namespace DllFuturaDataTCC.DataAccessObject
{
    public class iDaoUsuario
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }
        #endregion

        #region DAO Inserir Usuario
        public bool dInsereUsuario(iModUsuario objUsuario)
        {
            StringBuilder sqlConcatenada = new StringBuilder();
            sqlConcatenada.Append("INSERT INTO USUARIOS ");
            sqlConcatenada.Append("(NOMEUSUARIO ");
            sqlConcatenada.Append(",LOGINUSUARIO ");
            sqlConcatenada.Append(",SENHAUSUARIO ");
            sqlConcatenada.Append(",LEMBRETE ");
            sqlConcatenada.Append(",STATUS ");
            sqlConcatenada.Append(",FUNCAO) ");

            sqlConcatenada.Append("VALUES ");

            sqlConcatenada.Append("(@NOMEUSUARIO ");
            sqlConcatenada.Append(",@LOGINUSUARIO ");
            sqlConcatenada.Append(",@SENHAUSUARIO ");
            sqlConcatenada.Append(",@LEMBRETE ");
            sqlConcatenada.Append(",@STATUS ");
            sqlConcatenada.Append(",@FUNCAO) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(sqlConcatenada.ToString(), conexao);

                                                                        
            SqlParameter parametro = new SqlParameter("@NOMEUSUARIO", objUsuario.NomeUsuario);
            SqlParameter parametro1 = new SqlParameter("@LOGINUSUARIO", objUsuario.LoginUsuario);
            SqlParameter parametro2 = new SqlParameter("@SENHAUSUARIO", objUsuario.Senha);
            SqlParameter parametro3 = new SqlParameter("@LEMBRETE", objUsuario.Lembrete);
            SqlParameter parametro4 = new SqlParameter("@STATUS", "ATIVO");
            SqlParameter parametro5 = new SqlParameter("@FUNCAO", objUsuario.Funcao);


            comando.Parameters.Add(parametro);
            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);

            try
            {
                comando.ExecuteNonQuery();
                //caindo no CATCH chama as rotinas que geram os logs de erro
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
                comando = null;
                parametro = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
                parametro5 = null;
            }
        }
        #endregion

        #region Método Alterar Usuario
        public bool dAlteraUsuario(iModUsuario objUsuario)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE USUARIOS ");
            SqlConcatenada.Append("SET NOMEUSUARIO = @NOMEUSUARIO ");
            SqlConcatenada.Append(",LOGINUSUARIO = @LOGINUSUARIO ");
            SqlConcatenada.Append(",SENHAUSUARIO = @SENHAUSUARIO ");
            SqlConcatenada.Append(",LEMBRETE = @LEMBRETE ");
            SqlConcatenada.Append("WHERE CODIGO = @CODIGO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@NOMEUSUARIO", objUsuario.NomeUsuario);
            SqlParameter parametro1 = new SqlParameter("@LOGINUSUARIO", objUsuario.LoginUsuario);
            SqlParameter parametro2 = new SqlParameter("@SENHAUSUARIO", objUsuario.Senha);
            SqlParameter parametro3 = new SqlParameter("@LEMBRETE", objUsuario.Lembrete);
            SqlParameter parametro4 = new SqlParameter("@CODIGO", objUsuario.Pk_Codigo);
            comando.Parameters.Add(parametro);
            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString();
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
            }
        }
        #endregion

        #region Método Exclui Produto
        public bool dExcluiUsuario(iModUsuario objUsuario)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE USUARIOS ");
            SqlConcatenada.Append("SET STATUS = 'EXCLUIDO' ");
            SqlConcatenada.Append("WHERE CODIGO = @CODIGO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@CODIGO", objUsuario.Pk_Codigo);
            comando.Parameters.Add(parametro);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString();
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
            }
        }
        #endregion

        #region Método Obtem Usuario
        public iModUsuario[] dObterUsuario()
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("SELECT CODIGO ");
            SqlConcatenada.Append(",NOMEUSUARIO ");
            SqlConcatenada.Append(",LOGINUSUARIO ");
            SqlConcatenada.Append(",SENHAUSUARIO ");
            SqlConcatenada.Append(",LEMBRETE ");
            SqlConcatenada.Append(",STATUS ");
            SqlConcatenada.Append(",FUNCAO ");
            SqlConcatenada.Append("FROM USUARIOS WHERE STATUS = 'ATIVO' ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet, "USUARIOS");

                iModUsuario[] usuarios = new iModUsuario[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset


                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    usuarios[i] = new iModUsuario(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    usuarios[i].Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["CODIGO"].ToString());
                    usuarios[i].NomeUsuario = DsDataSet.Tables[0].Rows[i]["NOMEUSUARIO"].ToString();
                    usuarios[i].LoginUsuario = DsDataSet.Tables[0].Rows[i]["LOGINUSUARIO"].ToString();
                    usuarios[i].Senha = DsDataSet.Tables[0].Rows[i]["SENHAUSUARIO"].ToString();
                    usuarios[i].Lembrete = DsDataSet.Tables[0].Rows[i]["LEMBRETE"].ToString();
                    usuarios[i].Funcao = DsDataSet.Tables[0].Rows[i]["FUNCAO"].ToString();
                   
                }

                return usuarios;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsusuario", "obterCad()", erro.Message.ToString(), "Alterar o Usuario");
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

        #region Obter informacoes de Terminado Usuario Logado
        public iModUsuario[] dObterInformacoesUsuario(iModUsuario objUsuario)//Carrega um DataSet com o banco de dados
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("Select * ");
            SqlConcatenada.Append("From CONFIGUSUARIO WHERE LOGINUSUARIO = @NOME");
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@NOME", objUsuario.NomeUsuario);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet, "USUARIOS");

                iModUsuario[] usuarios = new iModUsuario[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset


                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    usuarios[i] = new iModUsuario(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    usuarios[i].Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["CODIGO"].ToString());
                    usuarios[i].NomeUsuario = DsDataSet.Tables[0].Rows[i]["NOMEUSUARIO"].ToString();
                    usuarios[i].LoginUsuario = DsDataSet.Tables[0].Rows[i]["LOGINUSUARIO"].ToString();
                    usuarios[i].Senha = DsDataSet.Tables[0].Rows[i]["SENHAUSUARIO"].ToString();
                    usuarios[i].Lembrete = DsDataSet.Tables[0].Rows[i]["LEMBRETE"].ToString();
                    usuarios[i].Funcao = DsDataSet.Tables[0].Rows[i]["FUNCAO"].ToString();

                }

                return usuarios;
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

        #region Efetuar Logon
        public bool dEfetuarLogon(iModUsuario objUsuario)//Carrega um DataSet com o banco de dados
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("Select SENHAUSUARIO ");
            SqlConcatenada.Append("From USUARIOS WHERE LOGINUSUARIO = @USUARIO AND STATUS = 'ATIVO' ");
            SqlCommand ComandoSQL = new SqlCommand();
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = SqlConcatenada.ToString();
            SqlParameter parametro = new SqlParameter("@USUARIO", objUsuario.LoginUsuario);
            ComandoSQL.Parameters.Add(parametro);
            SqlDataReader reader;

            try
            {
                ComandoSQL.ExecuteNonQuery();
                reader = ComandoSQL.ExecuteReader(); //retorna um objeto do tipo dataReader

                reader.Read();
                if (reader.HasRows != false)
                {
                    clsCriptografia DescriptografarSenha = new clsCriptografia();
                    string SenhaRecebidaCriptografada = DescriptografarSenha.Criptografar(objUsuario.Senha);
                    string SenhaQueVemDoBanco = reader.GetString(0).Trim(); //lê a string senhausuario do bd
                    //int nivelPermissao = reader.GetInt32(1); //lê o inteiro com nivel de permissão

                    if (SenhaRecebidaCriptografada == SenhaQueVemDoBanco)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsUsuario", "efetuarLogon()", erro.Message.ToString(), "Efetuar Logon no Sistema");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                ComandoSQL = null;
                SqlConcatenada = null;
                reader = null;
            }
        }
        #endregion
    }//fim classe
}//fim objeto
