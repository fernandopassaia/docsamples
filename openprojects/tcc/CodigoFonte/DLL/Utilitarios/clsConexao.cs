using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using DllFuturaDataCriptografia;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsConexao
    {
        string erroClasse { get; set; }
        #region Antigos Métodos de Acesso ao Access Comentados!
        //public string ConexaoBancoAccess = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\FuturaData\TCC\log\info.mdb;;Persist Security Info=True;Jet OLEDB:Database Password=143031si";

        //#region Abrir Conexao BD Access
        ///// <summary>
        ///// Esse método é apenas usado interno a Classe, ele tenta abrir conexão com banco
        ///// </summary>
        ///// <returns>Retorna objeto de conexão aberto</returns>
        //private OleDbConnection AbrirConexaoBdAccess()
        //{
        //    try
        //    {
        //        OleDbConnection ConexaoBd = new OleDbConnection(ConexaoBancoAccess);
        //        ConexaoBd.Open();
        //        return ConexaoBd;
        //    }
        //    catch (OleDbException erro)
        //    {
        //        clsControleLog geracao = new clsControleLog();
        //        geracao.efetuarTratamentoDoErroInfoSiga(0,"Inicializando Sistema - Sem usuário Identificado","Inicializando Sistema - Sem usuário Identificado","clsConexao","AbrirConexaoBdAccess",erro.Message.ToString(),"Abrir Conexão Banco de Dados");
        //        return null;
        //    }
        //}
        //#endregion

        //#region Fechar Conexao BD Access
        ///// <summary>
        ///// Esse método é usado interno a Classe, ele fecha a conexão após uma operação
        ///// </summary>
        ///// <returns>Retorna objeto de conexão fechado</returns>
        //private OleDbConnection FecharConexaoBdAccess()
        //{
        //    OleDbConnection ConexaoBd = new OleDbConnection(ConexaoBancoAccess);
        //    ConexaoBd.Close();
        //    return ConexaoBd;
        //}
        //#endregion
        #endregion        
        
        #region Abrir Conexao BD
        /// <summary>
        /// Método para Abertura de Conexão com o Banco de Dados - Retorna o Objeto de
        /// Conexão Já aberto e pronto pra uso (.Open()).
        /// </summary>
        /// <returns>Retorna objeto de conexão aberto</returns>
        public SqlConnection abrirConexaoBd()
        {
            SqlConnection ConexaoBd = new SqlConnection(recuperaStringConexaoSQLServer());
            try
            {                
                ConexaoBd.Open();
                return ConexaoBd;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(1, "", "", "clsConexao", "abrirConexaoBD()", erro.Message.ToString(), "Abertura da Conexão com o Banco de Dados");
                return ConexaoBd;
            }            
        }
        #endregion

        #region Fechar Conexao BD
        /// <summary>
        /// Recebe um objeto de Conexão com o Banco de Dados e Fecha-o
        /// </summary>
        /// <returns>Retorna objeto de conexão fechado</returns>
        public void fecharConexaoBd(SqlConnection conexaoAFechar)
        {
            conexaoAFechar.Close();                   
        }
        #endregion               

        #region Obter String Conexão (SQL Server) (XML)
        /// <summary>
        /// Recupera a String de Conexão do SQL Server que está gravada no banco Access
        /// </summary>
        /// <returns>Retorna a String de Conexão</returns>
        public string recuperaStringConexaoSQLServer()
        {
            //Data Source=SERVIDOR\FUTURADATA;Initial Catalog=FUTURADATA;Integrated Security=False;User ID=SA;Password=fdtech03
            DataSet dsDadosXML = new DataSet();
            clsCriptografia crip = new clsCriptografia();

            if(!File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                criaArquivoConexaoXML();
            }


            dsDadosXML.ReadXml(@"c:\FuturaData\TCC\conexao.xml");
            string servidor = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString());
            string servico = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVICO"].ToString());
            string baseDados = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString());
            string aceitaConexoesSeguras = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
            string usuario = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["USUARIO"].ToString());
            string senha = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SENHA"].ToString());

            try
            {
                string conexao = "Data Source=" + servidor + @"\" + servico + ";Initial Catalog=" + baseDados + ";Integrated Security=" + aceitaConexoesSeguras + ";";
                if (aceitaConexoesSeguras.ToUpper() == "FALSE")
                {
                    conexao = conexao + "User ID=" + usuario + ";Password=" + senha + ";";
                }
                return conexao;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(0, 0, true, "Inicializando Sistema - Sem usuário Identificado", "Inicializando Sistema - Sem usuário Identificado", "clsConexao()", "recuperaStringConexaoSQLServer()", erro.Message.ToString(), "Recuperando a String de Conexão do Sql Server Durante a Inicialização do Sistema do Access");
                return null;
            }
        }//fim classe
        #endregion

        #region Cria arquivo de Conexão com o Banco (XML)
        public bool criaArquivoConexaoXML()
        {
            if (!File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                clsCriptografia crip = new clsCriptografia();
                XmlDocument doc = new XmlDocument();
                XmlNode raiz = doc.CreateElement("ConexaoBDFuturaData");
                doc.AppendChild(raiz);
                doc.Save(@"c:\FuturaData\TCC\conexao.xml");
                doc.Load(@"c:\FuturaData\TCC\conexao.xml");

                XmlNode linha1 = doc.CreateElement("SERVIDOR");
                XmlNode linha2 = doc.CreateElement("SERVICO");
                XmlNode linha3 = doc.CreateElement("BASEDADOS");
                XmlNode linha4 = doc.CreateElement("ACEITACONEXOESSEGURAS");
                XmlNode linha5 = doc.CreateElement("USUARIO");
                XmlNode linha6 = doc.CreateElement("SENHA");

                linha1.InnerText = crip.Criptografar(System.Environment.MachineName.ToString().ToUpper());
                linha2.InnerText = crip.Criptografar("FUTURADATA");
                linha3.InnerText = crip.Criptografar("FDBUSINESS");
                linha4.InnerText = crip.Criptografar("FALSE");
                linha5.InnerText = crip.Criptografar("sa");
                linha6.InnerText = crip.Criptografar("1234fd");
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha1);
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha2);
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha3);
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha4);
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha5);
                doc.SelectSingleNode("/ConexaoBDFuturaData").AppendChild(linha6);
                doc.Save(@"c:\FuturaData\TCC\conexao.xml");
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Altera a Conexão com o Banco (XML)
        public bool alteraConexaoBancoXML(string servidor, string servico, string baseDados, string aceitaConexoesSeguras, string usuario, string senha)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"c:\FuturaData\TCC\conexao.xml");
                XmlNode no;
                clsCriptografia crip = new clsCriptografia();
                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./SERVIDOR").InnerText = crip.Criptografar(servidor);

                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./SERVICO").InnerText = crip.Criptografar(servico);

                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./BASEDADOS").InnerText = crip.Criptografar(baseDados);

                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./ACEITACONEXOESSEGURAS").InnerText = crip.Criptografar(aceitaConexoesSeguras);

                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./USUARIO").InnerText = crip.Criptografar(usuario);

                no = doc.SelectSingleNode("/ConexaoBDFuturaData");
                no.SelectSingleNode("./SENHA").InnerText = crip.Criptografar(senha);

                doc.Save(@"c:\FuturaData\TCC\conexao.xml");
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Retorna a Conexão com o Banco XML
        public DataTable retornaConexao()
        {                                        
            DataSet dsDadosXML = new DataSet();
            clsCriptografia crip = new clsCriptografia();
            dsDadosXML.ReadXml(@"c:\FuturaData\TCC\conexao.xml");
            string servidor = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString());
            string servico = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVICO"].ToString());
            string baseDados = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString());
            string aceitaConexoesSeguras = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
            string usuario = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["USUARIO"].ToString());
            string senha = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SENHA"].ToString());

            DataTable dt_DadosConexao = new DataTable();
            dt_DadosConexao.Columns.Add("Servidor");
            dt_DadosConexao.Columns.Add("Servico");
            dt_DadosConexao.Columns.Add("BaseDados");
            dt_DadosConexao.Columns.Add("AceitaConexoesSeguras");
            dt_DadosConexao.Columns.Add("Usuario");
            dt_DadosConexao.Columns.Add("Senha");

            DataRow DR = dt_DadosConexao.NewRow();
            DR["Servidor"] = servidor;
            DR["Servico"] = servico;
            DR["BaseDados"] = baseDados;
            DR["AceitaConexoesSeguras"] = aceitaConexoesSeguras;
            DR["Usuario"] = usuario;
            DR["Senha"] = senha;
            dt_DadosConexao.Rows.Add(DR);
            return dt_DadosConexao;
        }
        #endregion

        #region Verifica se banco de dados FDcorporateerp já existe
        public bool verificaSeBancoDeDadosFDcorporateerpJaExiste(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = abrirConexaoBd();
            SqlCommand comando = new SqlCommand(); //cria um objeto do tipo comando

            comando.CommandText = "sys.sp_databases"; //define a Stored que será chamada
            comando.CommandType = CommandType.StoredProcedure; //define que é uma StoredProcedure
            comando.Connection = conexao; //abre a conexão
            comando.ExecuteNonQuery(); //executa a query
            //cria um adaptador de dados para ler os dados que voltam
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dadosOrcamento = new DataSet();

            try
            {
                //preenche o DATASET com os dados retornados
                dataAdapter.Fill(dadosOrcamento, "sys.sp_databases");
                bool existe = false;
                DataTable dt_Dados = dadosOrcamento.Tables[0];
                for (int i = 0; i < dt_Dados.Rows.Count; i++)
                {
                    if (dt_Dados.Rows[i]["DATABASE_NAME"].ToString().Trim() == "FDcorporateerp")
                    {
                        existe = true;
                    }
                }

                return existe;
            }

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsConexao", "verificaSeBancoDeDadosFDcorporateerpJaExiste()", erro.Message.ToString(), "Verifica se o banco de dados FDcorporateerp já existe!");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {                
                conexao = null;
                dataAdapter = null;
                dadosOrcamento = null;
                comando = null;
            }
        }
        #endregion
    }//fim classe
}//fim namespace