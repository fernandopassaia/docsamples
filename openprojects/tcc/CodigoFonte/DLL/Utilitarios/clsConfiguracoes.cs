using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DllFuturaDataCriptografia;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Management.Instrumentation;
using System.Xml;
using DllFuturaDataUtil.FTP;
using System.Threading;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsConfiguracoes
    {
        bool primeiroCadastro = false;
        string erroClasse { get; set; }
        public bool getPrimeiroCadastro()
        {
            return primeiroCadastro;
        }
                
        #region Atributos da classe
        private DataSet ds_DadosRetorno = new DataSet();
        private clsConexao clsConexao = new clsConexao();
        clsCriptografia crip = new clsCriptografia();
        clsAtivacaoSoftware ativa = new clsAtivacaoSoftware(new clsConexao().recuperaStringConexaoSQLServer());
        string conexaoBanco = new clsConexao().recuperaStringConexaoSQLServer();
        #endregion

        #region  Método de Acesso aos atributos
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

        #region Obter Numero do Cliente FuturaData No Banco de Dados Online
        public int retornaNumeroIDClienteFuturaData(string cnpj)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = "SELECT NUMEROCLIENTEFUTURADATA AS CODIGO FROM DADOSEMPRESA where CNPJ LIKE '%" + cnpj + "%'";
            SqlDataReader reader;
            int numeroClienteFD = 0;
            try
            {
                ComandoSQL.ExecuteNonQuery();
                reader = ComandoSQL.ExecuteReader(); //retorna um objeto do tipo dataReader
                reader.Read();
                if (reader.IsDBNull(0) == false)
                {
                    numeroClienteFD = reader.GetInt32(0);
                    return numeroClienteFD;
                }
                else
                {
                    return numeroClienteFD;
                }
            }
            catch
            {
                return numeroClienteFD;
            }
        }
        #endregion

        #region Grava configuracoes do Sistema na Base de Dados
        
        public bool newGravaInformacoes(string nome, string cnpj, string razaoSocial, string ie, string im, string ramoAtividade,
        string cep, string estado, string rua, string numero, string bairro, string cidade, string complemento, string telefone1, string telefone2, 
        string telefone3, string fax, string telefoneGratuito, string celular1, string operadoraCelular1,string celular2, string operadoraCelular2,
        string celular3, string operadoraCelular3, string nextelID, string email, string emailFinanceiro, string emailCobranca, string msn, string skype,
        string redeSocial1, string redeSocial2, string site, string pessoasESetores, Byte[] imagemSistemas, int idClienteFD, string senhaPortal,
        DateTime dataInstalacaoSistema, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {   
            SqlCommand comandoSQLDestroiDados = new SqlCommand("DELETE FROM DADOSEMPRESA", new clsConexao().abrirConexaoBd());
            comandoSQLDestroiDados.ExecuteNonQuery();

            //verifica se já existe o numero do Cadastro no Cliente, no Banco FuturaData, se não houver tenta enviar...
            
            StringBuilder gravarInfo = new StringBuilder();
            gravarInfo.Append("INSERT INTO DADOSEMPRESA ");
            gravarInfo.Append("(NOME, CNPJ ,RAZAOSOCIAL ,IE ,IM ,RAMOATIVIDADE ,CEP ,ESTADO ,RUA ,NUMERO ");
            gravarInfo.Append(",BAIRRO ,CIDADE ,COMPLEMENTO ,TELEFONE1 ,TELEFONE2 ,TELEFONE3 ,FAX ");
            gravarInfo.Append(",TELEFONEGRATUITO ,CELULAR1 ,OPERADORA1 ,CELULAR2 ,OPERADORA2 ,CELULAR3 ");
            gravarInfo.Append(",OPERADORA3 ,NEXTELID ,EMAIL ,EMAILFINANCEIRO ,EMAILCOBRANCA ,MSN ,SKYPE ");
            gravarInfo.Append(",REDE_SOCIAL1, REDE_SOCIAL2 ,SITE ,PESSOASESETORES ");
            if (imagemSistemas != null)
            {
                gravarInfo.Append(",IMAGEM_BINARIO ");
            }
            gravarInfo.Append(",IDCLIENTE_FD ,SENHA_PORTAL, DATAINSTALACAOSISTEMA) ");
            
            gravarInfo.Append("VALUES (@NOME, @CNPJ ,@RAZAOSOCIAL ,@IE ,@IM ,@RAMOATIVIDADE ,@CEP ,@ESTADO ,@RUA ,@NUMERO ");
            gravarInfo.Append(",@BAIRRO ,@CIDADE ,@COMPLEMENTO ,@TELEFONE1 ,@TELEFONE2 ,@TELEFONE3 ,@FAX ,@TELEFONEGRATUITO ");
            gravarInfo.Append(",@CELULAR1 ,@OPERADORA1 ,@CELULAR2 ,@OPERADORA2 ,@CELULAR3 ,@OPERADORA3 ,@NEXTELID ,@EMAIL ");
            gravarInfo.Append(",@EMAILFINANCEIRO ,@EMAILCOBRANCA ,@MSN ,@SKYPE ,@REDE_SOCIAL1 ,@REDE_SOCIAL2 ,@SITE ,@PESSOASESETORES  ");
            if (imagemSistemas != null)
            {
                gravarInfo.Append(",@IMAGEM_BINARIO ");
            }
            gravarInfo.Append(",@IDCLIENTE_FD ,@SENHA_PORTAL ,@DATAINSTALACAOSISTEMA) ");
            
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            SqlCommand comando = new SqlCommand(gravarInfo.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@NOME", nome);
            comando.Parameters.Add(parametro);
            
            SqlParameter parametro1 = new SqlParameter("@CNPJ", cnpj);
            comando.Parameters.Add(parametro1);

            SqlParameter parametro2 = new SqlParameter("@RAZAOSOCIAL", razaoSocial);
            comando.Parameters.Add(parametro2);

            SqlParameter parametro3 = new SqlParameter("@IE", ie);
            comando.Parameters.Add(parametro3);

            SqlParameter parametro4 = new SqlParameter("@IM", im);
            comando.Parameters.Add(parametro4);

            SqlParameter parametro5 = new SqlParameter("@RAMOATIVIDADE", ramoAtividade);
            comando.Parameters.Add(parametro5);
            
            SqlParameter parametro7 = new SqlParameter("@CEP", cep);
            comando.Parameters.Add(parametro7);

            SqlParameter parametro6 = new SqlParameter("@ESTADO", estado);
            comando.Parameters.Add(parametro6);

            SqlParameter parametro8 = new SqlParameter("@RUA", rua);
            comando.Parameters.Add(parametro8);

            SqlParameter parametro9 = new SqlParameter("@NUMERO", numero);
            comando.Parameters.Add(parametro9);

            SqlParameter parametro10 = new SqlParameter("@BAIRRO", bairro);
            comando.Parameters.Add(parametro10);

            SqlParameter parametro11 = new SqlParameter("@CIDADE", cidade);
            comando.Parameters.Add(parametro11);

            SqlParameter parametro12 = new SqlParameter("@COMPLEMENTO", complemento);
            comando.Parameters.Add(parametro12);

            SqlParameter parametro13 = new SqlParameter("@TELEFONE1", telefone1);
            comando.Parameters.Add(parametro13);

            SqlParameter parametro14 = new SqlParameter("@TELEFONE2", telefone2);
            comando.Parameters.Add(parametro14);
            
            SqlParameter parametro15 = new SqlParameter("@TELEFONE3", telefone3);
            comando.Parameters.Add(parametro15);

            SqlParameter parametro16 = new SqlParameter("@FAX", fax);
            comando.Parameters.Add(parametro16);
                        
            SqlParameter parametro17 = new SqlParameter("@TELEFONEGRATUITO", telefoneGratuito);
            comando.Parameters.Add(parametro17);
                        
            SqlParameter parametro18 = new SqlParameter("@CELULAR1", celular1);
            comando.Parameters.Add(parametro18);

            SqlParameter parametro19 = new SqlParameter("@OPERADORA1", operadoraCelular1);
            comando.Parameters.Add(parametro19);

            SqlParameter parametro20 = new SqlParameter("@CELULAR2", celular2);
            comando.Parameters.Add(parametro20);
            
            SqlParameter parametro21 = new SqlParameter("@OPERADORA2", operadoraCelular2);
            comando.Parameters.Add(parametro21);

            SqlParameter parametro22 = new SqlParameter("@CELULAR3", celular3);
            comando.Parameters.Add(parametro22);

            SqlParameter parametro23 = new SqlParameter("@OPERADORA3", operadoraCelular3);
            comando.Parameters.Add(parametro23);

            SqlParameter parametro24 = new SqlParameter("@NEXTELID", nextelID);
            comando.Parameters.Add(parametro24);

            SqlParameter parametro25 = new SqlParameter("@EMAIL", email);
            comando.Parameters.Add(parametro25);
            
            SqlParameter parametro26 = new SqlParameter("@EMAILFINANCEIRO", emailFinanceiro);
            comando.Parameters.Add(parametro26);

            SqlParameter parametro27 = new SqlParameter("@EMAILCOBRANCA", emailCobranca);
            comando.Parameters.Add(parametro27);

            SqlParameter parametro28 = new SqlParameter("@MSN", msn);
            comando.Parameters.Add(parametro28);

            SqlParameter parametro29 = new SqlParameter("@SKYPE", skype);
            comando.Parameters.Add(parametro29);

            SqlParameter parametro30 = new SqlParameter("@REDE_SOCIAL1", redeSocial1);
            comando.Parameters.Add(parametro30);

            SqlParameter parametro31 = new SqlParameter("@REDE_SOCIAL2", redeSocial2);
            comando.Parameters.Add(parametro31);
            
            SqlParameter parametro32 = new SqlParameter("@SITE", site);
            comando.Parameters.Add(parametro32);

            SqlParameter parametro33 = new SqlParameter("@PESSOASESETORES", pessoasESetores);
            comando.Parameters.Add(parametro33);

            if (imagemSistemas != null)
            {
                SqlParameter parametro34 = new SqlParameter("@IMAGEM_BINARIO", imagemSistemas);
                comando.Parameters.Add(parametro34);
            }

            SqlParameter parametro35 = new SqlParameter("@IDCLIENTE_FD", idClienteFD);
            comando.Parameters.Add(parametro35);

            SqlParameter parametro36 = new SqlParameter("@SENHA_PORTAL", senhaPortal);
            comando.Parameters.Add(parametro36);

            SqlParameter parametro37 = new SqlParameter("@DATAINSTALACAOSISTEMA", dataInstalacaoSistema);
            comando.Parameters.Add(parametro37);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "gravaInformacoes", "gravaInformacoes()", erro.Message.ToString(), "Grava Informações");
                return false;
            }

    //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                comando = null;
            }
        }//fim metodo
        #endregion

        #region Alterar Papel Parede do Usuario
        /// <summary>
        /// Grava o Papel de Parede do Usuario no banco de dados de acordo com o clique no papel de parede
        /// </summary>
        /// <param name="numeroPapelParede">Numero do Papel a ser gravado</param>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna true se conseguir gravar, false se não</returns>
        public bool alterarPapelParedeUsuario(int numeroPapelParede, bool exibirAleatorio, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append(" UPDATE CONFIGUSUARIO SET ");
            SqlConcatenada.Append(" NUMEROPAPELPAREDE = @numeroPapelParede,"); //se for checada é fisica, senão, juridica
            SqlConcatenada.Append(" EXIBIRALEATORIO = @exibirAleatorio, ");
            SqlConcatenada.Append(" IMAGEM_BINARIO = null "); //se for checada é fisica, senão, juridica
            SqlConcatenada.Append(" WHERE  ");
            SqlConcatenada.Append("FK_NUMUSUARIO = @fk_NumUsuario"); //se for checada é fisica, senão, juridica

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@fk_NumUsuario", numeroUsuarioLogado);
            comando.Parameters.Add(parametro);

            SqlParameter parametro1 = new SqlParameter("@numeroPapelParede", numeroPapelParede); //se for checada é fisica, senão, juridica
            comando.Parameters.Add(parametro1);

            SqlParameter parametro2 = new SqlParameter("@exibirAleatorio", exibirAleatorio);
            comando.Parameters.Add(parametro2);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }

                    //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsConfiguracoes", "alteraPapelParedeUsuário()", erro.Message.ToString(), "Altera Papel de Parede do Usuário");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                comando = null;
                parametro = null;
                parametro1 = null;
            }
        }//fim do método de incluir
        #endregion

        #region Personaliza Papel de Parede para o Usuário (papel que ele seleciona)
        /// <summary>
        /// Personaliza o Papel de Parede do Usuário Para Imagem que ele mesmo seleciona
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se Conseguir Alterar, False se não</returns>
        public bool personalizaPapelParedeUsuarioEscolhe(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost, Byte[] imagem)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append(" UPDATE CONFIGUSUARIO SET ");
            SqlConcatenada.Append(" IMAGEM_BINARIO = @IMAGEM ");
            
            SqlConcatenada.Append(" WHERE ");

            SqlConcatenada.Append(" FK_NUMUSUARIO = @CODIGO ");
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro24 = new SqlParameter("@CODIGO", numeroUsuarioLogado);
            comando.Parameters.Add(parametro24);

            SqlParameter parametro25 = new SqlParameter("@IMAGEM", imagem);
            comando.Parameters.Add(parametro25);


            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsConfiguracoes", "personalizaPapelParedeUsuarioEscolhe()", erro.Message.ToString(), "Personaliza o Papel de Parede do Usuário");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;                
                comando = null;
                SqlConcatenada = null;
            }
        }//fim do método de Alterar
        #endregion

        #region Executa uma Operação SQL no banco
        /// <summary>
        /// Retorna a Quantidade de Hosts que estão sendo usados atualmente pelo Sistema.
        /// <param name="numeroUsuarioLogado">numero do Usuario logado no momento</param>
        /// <param name="nomeHost">nome do host que gerou o erro</param>
        /// </summary>
        /// <returns>Retorna MAX da tabela - Se retornar 0, deu erro</returns>
        public bool executaOperacaoSQL(string operacaoSQL, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = operacaoSQL;
            SqlDataAdapter DataAdap = new SqlDataAdapter(operacaoSQL.ToString(), conexao);
            DataSet dsDados = new DataSet();
            try
            {
                ComandoSQL.ExecuteNonQuery();
                DataAdap.Fill(dsDados, "CLIENTES");
                setDs_DadosRetorno(dsDados);
                return true;
            }
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsConfiguracoes", "Executa uma Operação SQL no banco", erro.Message.ToString(), "Executa uma Operação SQL no banco");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;                
                ComandoSQL = null;
            }
        }
        #endregion

        #region Retorna Dados Empresa Relatorio
        /// <summary>
        /// Retorna Orcamentos Efetuados por um cliente (apenas orçamentos em aberto). O Filtro opcional é por data
        /// e/ou por numero do cliente (código do cliente)
        /// </summary>
        /// <param name="dataInicio">Data de Inicio no formado 11/11/1111 11:11 - "" para sem data</param>
        /// <param name="dataFim">Data de Inicio no formado 11/11/1111 11:11 - "" para sem data</param>
        /// <param name="codigoCliente">Código do cliente (pk)</param>        
        /// <param name="numeroUsuarioLogado">Número do Usuário logado no Sistema</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema</param>
        /// <returns>Retorna True se conseguir - usar getDsRetorno() pra pegar, false se não</returns>
        public bool retornaDadosEmpresaRel( int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            SqlCommand comando = new SqlCommand(); //cria um objeto do tipo comando
            //adiciona o parametro que chama a stored procedure, e o código que será enviado

            comando.CommandText = "sp_DADOS_EMPRESA_REL"; //define a Stored que será chamada
            comando.CommandType = CommandType.StoredProcedure; //define que é uma StoredProcedure
            comando.Connection = conexao; //abre a conexão
            comando.ExecuteNonQuery(); //executa a query

            //cria um adaptador de dados para ler os dados que voltam
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dadosOrcamento = new DataSet();
            try
            {
                dataAdapter.Fill(dadosOrcamento);
                setDs_DadosRetorno(dadosOrcamento);
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsConfiguracoes", "retornaImpostosNfePorData()", erro.Message.ToString(), "Retorna Impostos Nfe de acordo com data");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
                dadosOrcamento = null;
                dataAdapter = null;
            }
        }
        #endregion
    }//fim classe
}//fim namespace
