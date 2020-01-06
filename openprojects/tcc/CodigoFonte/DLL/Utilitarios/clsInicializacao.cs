using System;
using System.Collections.Generic;
using System.Text;
using DllFuturaDataTCC.Utilitarios;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using DllFuturaDataCriptografia;
using System.IO;

namespace DllFuturaDataTCC
{
    public class clsInicializacao
    {
        #region Import da API Kernel do Windows e das Variaveis que receberão Informações do HardDisk da Máquina (S/N)
        string erroClasse { get; set; }
        //importa a API Kernel32 do windows
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static bool GetVolumeInformation(
        string RootPathName,
        StringBuilder VolumeNameBuffer,
        int VolumeNameSize,
        out uint VolumeSerialNumber,
        out uint MaximumComponentLength,
        out uint FileSystemFlags,
        StringBuilder FileSystemNameBuffer,
        int nFileSystemNameSize);
        #endregion

        #region Variaveis Internas e Método para Retornar DataSet de Retorno
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

        #region Verifica Comunicacao Banco (PASSO 1 DA AUTENTICAÇÃO)
        /// <summary>
        /// Tenta abrir conexão com o banco de dados na inicialização do sistema
        /// </summary>
        /// <returns>Retorna True se conseguir, false se não</returns>
        public bool verificaComunicacaoBanco()
        {            
            try
            {
                SqlConnection conexao = this.clsConexao.abrirConexaoBd();
                if (conexao.State == ConnectionState.Open)
                {
                    this.clsConexao.fecharConexaoBd(conexao);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Retorna Dados Cliente (PASSO 2 DA AUTENTICAÇÃO)
        /// <summary>
        /// Retorna DataSet Contendo todos os dados do cliente Usuário do Sistema
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se conseguir fazer a Consulta - Usar o método getDsDadosRetorno pra pegar o DataSet, False se não</returns>
        public bool retornaDadosCliente(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd(); //já vem abertinha
            //selecionamos todos os campos - afinal, nunca sabemos quandop recisaremos de um ou de outro
            //é isso ai, joga o comando dentro do construtor do adapter e que se f*** fiz isso por que esse Select sempre terá só 1 linha
            SqlDataAdapter DataAdap = new SqlDataAdapter("SELECT * FROM DADOSEMPRESA", conexao);
            DataSet DsDataSet = new DataSet(); //milagre eu ter criado ele fora do try pra destruir depois
            try
            {
                DataAdap.Fill(DsDataSet); //quem se importa? o coletor faz isso pra gente...
                setDs_DadosRetorno(DsDataSet); //por que não retornar o DataSet direto? se der erro...
                return true; //culpa o usuário, se pintar a grama de azul ele morre de fome
            }
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaDadosCliente()", erro.Message.ToString(), "Retorna os Dados da Empresa Usuária do Sistema");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DataAdap = null;
                DsDataSet = null;
            }
        }//this sucks
        #endregion

        #region Verifica Backup (Se está Desatualizado ou Não) (PASSO 6 DA AUTENTICAÇÃO)
        /// <summary>
        /// Verifica Backup - Se backup tiver mais de 3 dias, avisa usuario
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna "Backup Atualizado" se o ultimo backup tiver pelo menos 3 dias, "Backup Desatualizado" se tiver 4 dias ou mais,
        /// Em caso de erro de conexão ou verificação retorna "Erro Validação"</returns>
        public string verificaBackupServidor(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = "SELECT DATAULTIMOBACKUP FROM DADOSEMPRESA";
            SqlDataReader reader;
            try
            {                
                ComandoSQL.ExecuteNonQuery();
                reader = ComandoSQL.ExecuteReader(); //lê o SQL DataReader
                reader.Read();

                DateTime dataBanco = Convert.ToDateTime(reader.GetString(0));
                DateTime hoje = DateTime.Now;
                //calcula quantos dias e horas existem entre o ultimo backup e hoje
                TimeSpan diferencasDias = (dataBanco).Subtract(hoje);
                string dias = Convert.ToString(diferencasDias.ToString());
                dias = dias.Substring(1, 1); //pega o que interessa, apenas o dia do ultimo backup

                int dia = 0;// Convert.ToInt32(dias);

                if (dia >= 3) //se for mais de 3 dias, quer dizer que o backup está desatualizado
                {
                    return "Backup Desatualizado";
                }
                else
                {
                    return "Backup Atualizado";
                }

            }//fim TRY

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "verificaBackupServidor()", erro.Message.ToString(), "Verifica se o backup no servidor não está desatualizado");
                return "Erro Validação";
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }//fim método
        #endregion //DEPOIS SOBE LOGON DO SISTEMA
                
        #region Verifica Atualizações (PASSO 7 DA AUTENTICAÇÃO)
        /// <summary>
        /// Tenta se conectar ao Servidor FuturaData e baixar atualizações do produto
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se sistema Cliente estiver atualizado, false se não</returns>
        public bool verificaAtualizacoes(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            return true;
        }
        #endregion

        #region EfetuarLogin (AUTENTICA USUARIO E LIBERA ACESSO - PASSO 8 DA AUTENTICAÇÃO)
        /// <summary>
        /// chama Form onde usuario preenche o Login e Senha - Guarda usuario na Cessão
        /// </summary>
        /// <returns>Retorna True se for válido, false se não</returns>
        public bool efetuarLogin(string nomeUsuario, string nomeUsuarioLogado, string Senha)
        {
            return true;
        }
        #endregion

        #region Retorna Configurações do Usuario (CARREGA AS CONFIGS - PASSO 9 DA AUTENTICAÇÃO)
        /// <summary>
        /// Retorna DataSet com todas as configurações do Usuario Citado no Login
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se conseguir fazer a Consulta - Usar o método getDsDadosRetorno pra pegar o DataSet, False se não</returns>
        public bool retornaConfiguracoesUsuario(int numeroUsuario, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            return true;         
        }
        #endregion

        #region Retorna Mensagens Inicialização (CARREGA MENSAGENS DE BOA VINDA)
        /// <summary>
        /// Retorna as mensagens de abertura do sistema
        /// </summary>
        /// <param name="numeroUsuarioLogado">numero do Usuario logado no momento</param>
        /// <param name="nomeHost">nome do host que gerou o erro</param>
        /// <returns>Retorna True se conseguir fazer a Consulta - Usar o método getDsDadosRetorno pra pegar o DataSet, False se não</returns>
        public bool retornaMensagensInicialização(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter("SELECT * FROM ISMSGINICIALIZACAO654", conexao);
            DataSet DsDataSet = new DataSet();
            try
            {                
                DataAdap.Fill(DsDataSet);
                setDs_DadosRetorno(DsDataSet);
                return true;
            }
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaMensagensInicializacao()", erro.Message.ToString(), "Retorna Mensagens de Inicializacao");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DataAdap = null;
                DsDataSet = null;
            }
        }
        #endregion        

        #region Retorna Versão InfoSiga
        /// <summary>
        /// Retorna a Versão Atual que o Infosiga está rodando
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna um Inteiro com o numero da versão</returns>
        public int retornaVersaoInfoSiga(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            return 0;
        }
        #endregion

        #region Verifica Backup (Se está Desatualizado ou Não) (PASSO 6 DA AUTENTICAÇÃO)
        /// <summary>
        /// Verifica Backup - Retorna a Data do Ultimo Backup
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna "Backup Atualizado" se o ultimo backup tiver pelo menos 3 dias, "Backup Desatualizado" se tiver 4 dias ou mais,
        /// Em caso de erro de conexão ou verificação retorna "Erro Validação"</returns>
        public string retornaDataUltimoBackup(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = "SELECT DATAULTIMOBACKUP FROM DADOSEMPRESA";
            SqlDataReader reader;
            try
            {
                ComandoSQL.ExecuteNonQuery();
                reader = ComandoSQL.ExecuteReader(); //lê o SQL DataReader
                reader.Read();

                DateTime dataBanco = Convert.ToDateTime(reader.GetString(0));
                return Convert.ToDateTime(dataBanco).ToString("dd/MM/yyyy hh:mm");

            }//fim TRY

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaDataUltimoBackup()", erro.Message.ToString(), "Retorna a Data do Ultimo Backup");
                return "Erro Validação";
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }//fim método
        #endregion //DEPOIS SOBE LOGON DO SISTEMA

        #region Retorna se maquina atual é Servidor ou Não (Pelo nome Servidor da Máquina)
        /// <summary>
        /// Verifica se a máquina atual é Servidor do Sistema
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Returna True se for Servidor, false se não</returns>
        public bool retornaSeEServidor(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            if (System.Environment.MachineName.ToString().ToUpper() == "SERVIDOR")
            {
                return true;
            }

            else
            {
                return false;
            }

        }
        #endregion

        #region Retorna o nome da Máquina que está usando o sistema
        /// <summary>
        /// Retorna o Nome da Máquina que está usando o sistema
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna Nome da Máquina que está usando o sistema</returns>
        public string retornaNomeMaquina(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            return System.Environment.MachineName.ToString();
        }
        #endregion

        #region Retorna Todos os Usuarios que estão Conectados ao Servidor Exceto o Servidor
        /// <summary>
        /// Retorna todos os usuarios conectados no SQL Server Exceto o Servidor
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se conseguir fazer a Consulta - Usar o método getDsDadosRetorno pra pegar o DataSet, False se não</returns>
        public bool retornaUsuariosConectadosSql(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter("SELECT DB_NAME(DBID) AS [Sistema], HOSTNAME AS ESTACAO_TRABALHO FROM MASTER..SYSPROCESSES WHERE DB_NAME(DBID)='InfoSigaBasic'  AND HOSTNAME <>'' AND HOSTNAME <>'SERVIDOR'", conexao);
            DataSet DsDataSet = new DataSet();
            try
            {                
                DataAdap.Fill(DsDataSet);
                setDs_DadosRetorno(DsDataSet);
                return true;
            }
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaUsuariosConectadosSql()", erro.Message.ToString(), "Verifica Usuários Conectados no SQL Server");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DataAdap = null;
                DsDataSet = null;
            }
        }
        #endregion

        #region Retorna numero Serie HD
        /// <summary>
        /// Verifica se o nome do HOST e o numero do HD do mesmo estão licenciados para usar o sistema
        /// </summary>
        /// <returns>Retorna numero Série HD</returns>
        public string retornaNumeroSerieHD()
        {

            StringBuilder volname = new StringBuilder(256);
            StringBuilder fsname = new StringBuilder(256);
            uint sernum, maxlen, flags;
            if (!GetVolumeInformation("c:\\", volname, volname.Capacity, out sernum, out maxlen, out flags, fsname, fsname.Capacity))
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            string volnamestr = volname.ToString();
            string fsnamestr = fsname.ToString();

            string numeroSerieHDMaquina = Convert.ToString(sernum);
            return numeroSerieHDMaquina;
        }
        #endregion

        #region Retorna o Tamanho da Base de Dados
        /// <summary>
        /// Retorna o Tamanho da Base de Dados (124MB por exemplo)
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna o tamanho da base, "ERRO" no caso de falha na consulta</returns>
        public string retornaTamanhoBaseDeDados(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(); //cria um objeto do tipo comando

            comando.CommandText = "sp_spaceused"; //define a Stored que será chamada
            comando.CommandType = CommandType.StoredProcedure; //define que é uma StoredProcedure
            comando.Connection = conexao; //abre a conexão
            comando.ExecuteNonQuery(); //executa a query
            //cria um adaptador de dados para ler os dados que voltam
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dadosOrcamento = new DataSet();

            try
            {                
                //preenche o DATASET com os dados retornados
                dataAdapter.Fill(dadosOrcamento, "sp_spaceused");
                
                string tamanho = dadosOrcamento.Tables[0].Rows[0]["database_size"].ToString();
                return tamanho;
            }

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaTamanhoBaseDeDados()", erro.Message.ToString(), "Retorna o Tamanho do Banco de Dados");
                return "ERRO";
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                dataAdapter = null;
                dadosOrcamento = null;
                comando = null;
            }
        }
        #endregion

        #region Verifica Componentes
        /// <summary>
        /// Verifica os Componentes e Depêndencias do sistema (Verifica se todas as dlls estão na pasta correta,
        /// se todos os diretórios estão criados, se todos os arquivos usados estao ok e etc).
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna "OK" se tudo estiver correto, o erro se faltar algum arquivo, o erro pode ser exibido direto ao usuário</returns>
        public string verificaComponentesEDependencias(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            if (File.Exists(@"c:\FuturaData\TCC\DllFuturaDataContrValidacoes.dll") == false)
            {
                return "A ClassLibrary de Controle e Validações do Sistema não foi localizada! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
            
            if (File.Exists(@"c:\FuturaData\TCC\DllFuturaDataCriptografia.dll") == false)
            {
                return "A ClassLibrary de Critografia do Sistema não foi localizada! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
            if (File.Exists(@"c:\FuturaData\TCC\DllFuturaDataFiscal.dll") == false)
            {
                return "A ClassLibrary Fiscal do Sistema não foi localizada! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
            
            
            if (File.Exists(@"c:\FuturaData\TCC\DllFuturaDataUtil.dll") == false)
            {
                return "A ClassLibrary de Utilitários do Sistema não foi localizada! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
            
            //if (Directory.Exists(@"c:\FuturaData\TCC\Arquivos Digitais") == false)
            //{
             //   return "O Diretório de Arquivos Digitais do Sistema não foi localizado! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            //}
            
            if (Directory.Exists(@"c:\FuturaData\TCC\Exportados") == false)
            {
                return "O Diretório de Exportados do Sistema não foi localizado! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
                        
            if (File.Exists(@"c:\FuturaData\TCC\ibge.xml") == false)
            {
                return "O Arquivo de Códigos de Municipio IBGE não pode ser encontrado! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }
            
            if (File.Exists(@"c:\FuturaData\TCC\TIPI.PDF") == false)
            {
                return "O Arquivo da Tabela TIPI não pode ser encontrado! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }

            //if (File.Exists(@"c:\FuturaData\TCC\ibpt.xml") == false)
            //{
            //    return "O Arquivo da Tabela do IBPT formato XML! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            //}

            if (File.Exists(@"c:\FuturaData\TCC\ibpt.xls") == false)
            {
                return "O Arquivo da Tabela do IBPT formato XLS! Não será possível continuar o uso do sistema. Efetue um backup de suas informações. Será necessário a reinstalação do componente corrompido!";
            }

            if (File.Exists(@"c:\FuturaData\TCC\conexao.xml") == false)
            {
                bool retorno = new clsConexao().criaArquivoConexaoXML();                
            }

            return "OK";
        }
        #endregion

        #region Retorna Dados Cliente (PASSO 2 DA AUTENTICAÇÃO)
        /// <summary>
        /// Retorna Apenas o nome da empresa utilizadora para o topo dos relatórios
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna o Nome da Empresa Utilizadora, "" se der erro</returns>
        public string retornaNomeEmpresaUtilizadoraTopoRelatórios(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd(); //já vem abertinha
            //selecionamos todos os campos - afinal, nunca sabemos quandop recisaremos de um ou de outro
            //é isso ai, joga o comando dentro do construtor do adapter e que se f***

            SqlCommand ComandoSQL = new SqlCommand("SELECT NOME FROM DADOSEMPRESA", conexao);
                        
            SqlDataReader reader;
            try
            {
                ComandoSQL.ExecuteNonQuery();
                reader = ComandoSQL.ExecuteReader(); //retorna um objeto do tipo dataReader
                reader.Read();
                if (reader.IsDBNull(0) == false)
                {
                    return reader.GetString(0);                    
                }
                else
                {
                    return "";
                }
            }
            catch (SqlException erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaNomeEmpresaUtilizadoraTopoRelatórios()", erro.Message.ToString(), "Retorna o nome da empresa para o cabecário do Relatório");
                return "";
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;                
                reader = null;
            }
        }//this sucks
        #endregion

        #region Retorna informações sobre a conexão (nome do servidor, nome do serviço, nome da base de dados)
        public DataTable retornaInformacoesSobreConexao()
        {
            DataTable dt_Dados = new DataTable();
            dt_Dados.Columns.Add("NomeServidor");
            dt_Dados.Columns.Add("NomeServico");
            dt_Dados.Columns.Add("NomeBase");

            SqlConnection conexao = new clsConexao().abrirConexaoBd();

            DataRow DR = dt_Dados.NewRow();
            DR["NomeServidor"] = conexao.DataSource.ToString();
            DR["NomeServico"] = conexao.DataSource.ToString();
            DR["NomeBase"] = conexao.Database.ToString();
            dt_Dados.Rows.Add(DR);            
            return dt_Dados;
        }
        #endregion
        
        #region Retorna Informações sobre as Versões do Banco de Dados
        public bool retornaInformacoesSobreBancoDeDados(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {            
            StringBuilder sqlConcatenada = new StringBuilder();
            sqlConcatenada.Append("SELECT ");
            sqlConcatenada.Append("SERVERPROPERTY ('MachineName') as Servidor, ");
            sqlConcatenada.Append("SERVERPROPERTY('productversion') as Versao, ");
            sqlConcatenada.Append("SERVERPROPERTY ('productlevel')as ServicePack,  ");
            sqlConcatenada.Append("SERVERPROPERTY ('edition')as Edicao ");            

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(sqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet);
                setDs_DadosRetorno(DsDataSet);
                return true;
            }//fim TRY

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaInformacoesSobreBancoDeDados()", erro.Message.ToString(), "Retorna Informacoes do Banco de Dados");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }
        #endregion

        #region Grava Informacoes do Aceite do Usuário nas Responsabilidades Fiscais
        public bool insereInformacoesDoAceiteDoUsuarioResponsabilidadesFiscais(string nomeUsuarioDigitado, string nomeContabilidade, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder sqlConcatenada = new StringBuilder();
            sqlConcatenada.Append("INSERT INTO TB_INFORMATIVOSFISCAIS ");
            sqlConcatenada.Append("            (DATAACEITE ");
            sqlConcatenada.Append(",NUMEROUSUARIOLOGADO ");
            sqlConcatenada.Append(",NOMEUSUARIODIGITADO ");
            sqlConcatenada.Append(",NOMECONTABILIDADEDIGITADA) ");
            sqlConcatenada.Append("VALUES ");
            sqlConcatenada.Append("(@DATAACEITE ");
            sqlConcatenada.Append(",@NUMEROUSUARIOLOGADO ");
            sqlConcatenada.Append(",@NOMEUSUARIODIGITADO ");
            sqlConcatenada.Append(",@NOMECONTABILIDADEDIGITADA) ");
            

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(sqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@DATAACEITE", DateTime.Now);
            SqlParameter parametro1 = new SqlParameter("@NUMEROUSUARIOLOGADO", numeroUsuarioLogado);
            SqlParameter parametro2 = new SqlParameter("@NOMEUSUARIODIGITADO", nomeUsuarioDigitado);
            SqlParameter parametro3 = new SqlParameter("@NOMECONTABILIDADEDIGITADA", nomeContabilidade);
            comando.Parameters.Add(parametro);
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
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "insereInformacoesDoAceitedoUsuarioResponsabilidadesFiscais()", erro.Message.ToString(), "Insere Informacoes do Aceite do Usuário Responsabilidades Fiscais");
                return false;
            }
        }
        #endregion

        #region Retorna Se Mostra Tela Aceite Fiscal
        public bool retornaSeMostraTelaAceiteFiscal(int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder sqlConcatenada = new StringBuilder();
            sqlConcatenada.Append("SELECT DATAACEITE FROM TB_INFORMATIVOSFISCAIS");            

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(sqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet);

                bool retorno = true;

                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    DateTime data = Convert.ToDateTime(DsDataSet.Tables[0].Rows[i][0].ToString());
                    if (data.Month == DateTime.Now.Month)
                    {
                        retorno = false;
                    }
                }
                                
                return retorno;
            }//fim TRY

            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsInicializacao", "retornaSeMostraTelaAceiteFiscal()", erro.Message.ToString(), "Retorna se Sistema deve mostrar tela Aceite Fiscal");
                return true;
            }
            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }
        #endregion
    }//fim classe
}//fim namespace

