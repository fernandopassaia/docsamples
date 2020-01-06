//C# WinForms APP to Monitor and Automatic-Backup SQL Server DataBase.
//You can install this application on computer with a MS-SQL Server instance, and program it to make automatic backups of your database.
//Exemple: If you have a Software Developed with MS-SQL Server, you can install it on your clients to make the Data-Backup of your Software.
//Just inform the DataBase config (Server name, instance, name of Database) in database.xml (root of this program) and let it works. This
//program will not use the "Query" Backup of SQL and .bkp files, BUT copy all Database files (.mdf and .lfd(log)) to a folder. Then, will
//"ZIP" the both files to compress the final file.

//You can configure a Pendrive or Removible Device (Like an External Hard Disk) to copy the final ".zip" backup to a second disk, and make
//it more secure. You can program this Tool to Work on a specific time (like 2am) when the server is not in use. The tool will stop the
//MS-SQL Service, copy the files, start the Service Again (to up-again the Server) and do the backup-work.

//It have options to "Automatic Backup" on specific time (like 02am, 04am, 06am, 18pm), days of backup (you can program for example, just one
//back per week, or every day), you have options to delete old backups (you can inform the backuper, how many backups it should keep - just
//the last 6 for example), and you have options to delete old files from the removable devices (to keep the PenDrive with enought space).
//You can also program the Tool to ShutDown the machine after the work.

//It also have some tools to connect, execute querys, get information about the Server and Open MS - SQL to Network(open Windows Firewall).
//This Software was Tested on SQL-Server 2005 - 2014 and works good.The Source Code is in Portuguese, but, i insert an option to translate all
//labels to ENGLISH(so the program will appears to user in ENG). It's a simple translate for a small program and little number of labels, so i
//don't create a class or xml, json, but just translate labels direct on code.

//Note: The program need 4 DLLS.They're included in the "Library" folder, "ionic.zip" and "WindowsFirewallHelper" can be found on Nuget. The other
//two was developed by me (CSOBRF_Criptografia and CSOBRF_Validacoes). If you want to see the source, they're open in my repo.This program will 
//compile/build on C:\MsSqlServerBackup.This program can be "adapted" to backup other type of DataBases, or even other type of files and etc. 
//Don't forget to run always as 'administrator' because this program needs access to Windows Services.

//Note: This Software was developed by me too many years ago, a tool for backup a C# ERP WinForms application. Then i decide to open the source
//and just adapt it to be an opensource. This source has too many years, so maybe all the currently design patters are not nowdays/updated.

//To run this program: Create a folder c:\MsSqlServerBackup (where it will be build). Put inside the folder, the 3 files in "Library" path on this
//solution.Configure your database in "database.xml". Run it, configure on screen and enjoy.

//________________________________________
//C# Windows forms - aplicativo para monitorar e efetuar backups automáticos do SQL Server.
//Você pode instalar esse aplicativo em um computador com uma instância do SQL Server, e programar ele para fazer backups automáticos do seu banco.
//Exemplo: Se você tem um Software feito com SQL Server, você pode instalar esse programa nos seus clientes para fazer backup do banco de dados do
//seu software. Apenas informe a configuração do banco (Nome Servidor, instância, nome banco) no database.xml (na raiz desse programa) e deixe que
//ele trabalhe. Esse programa não irá usar o “Query” backup do SQL e arquivos .bkp, mas copiar os arquivos do banco (.mdf e .lfd(log)) para uma pasta.
//Então irá “zipar” e comprimir o arquivo final.

//Você pode configurar um Pendrive ou Disco Removível (como um disco externo) para copiar o ".zip" final para um segundo disco, e tornar o backup 
//mais seguro. Você pode programar essa ferramenta trabalhar em uma hora específica (como 2 da manhã) quando o servidor não estiver em uso. Essa 
//ferramenta irá parar o serviço do SQL Server, copiar os arquivos, iniciar o Serviço Novamente (para liberar o servidor) e fazer o trabalho.

//Tem opções de "Backup Automático" em alguma hora específica (como 02, 04, 06, 18h), dias do backup (você pode programar backup só uma vez por semana,
//ou todo dia), você tem opções para deletar backups antigos (você pode informar quantos backups deve manter, apenas os últimos 6 por exemplo), e tem 
//opções para deletar arquivos antigos do disco removível (para manter o pendrive com espaço suficiente). Você também pode programar a ferramenta para
//desligar a máquina após o trabalho.

//Também existem algumas opções para conectar, executar querys, pegar informações sobre o servidor, abrir a Rede (liberar o Firewall). Esse Software foi
//testado sobre SQL-Server 2005-2014 e funcionou bem. O código fonte está em português, mas, eu inseri opções para traduzir todas labels para Inglês 
//(então o programa ficará em inglês). É uma tradução simples para um pequeno programa e algumas labels, então não criei classes ou xml, json, mas apenas
//traduzi as lables direto no código.

//Nota: O programa precisa de 3 DLLS. Elas estão incluidas na pasta "Library", "ionic.zip" e "WindowsFirewallHelper" podem ser encontrados no nuget. As 
//outras duas foram desenvolvidas por mim (CSOBRF_Criptografia and CSOBRF_Validacoes). Você pode olhar o fonte, elas são abertas no meu repositório. Esse 
//programa irá compilar em C:\MsSqlServerBackup . Esse programa pode ser "adaptado" para fazer backup de outros tipos de banco de dados, ou mesmo outro 
//tipos de arquivos e etc. Não esqueça de rodar como 'administrador' sempre, pois esse programa precisa de acesso a Serviços do Windows.

//Nota: Esse software foi desenvolvido por mim a muitos anos atrás, uma ferramenta pra backup de um ERP Windows Forms C#. Então eu decidi abrir o fonte e
//apenas adaptar pra opensource. Esse código tem muitos anos, então talvez não esteja de acordo com os design patterns atuais.

//Para rodar esse programa: Crie uma pasta c:\MsSqlServerBackup (onde será compilado). Coloque dentro da pasta os 3 arquivos na pasta "Library" nessa 
//solução. Configure seu banco de dados no arquivo "database.xml". Rode, configure na tela e aproveite.

using CSOBRF_Validacoes;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WindowsFirewallHelper;

namespace MsSqlBackup
{
    public partial class MsSqlBackup : Form
    {
        #region Import da API Kernel do Windows e das Variaveis que receberão Informações do HardDisk da Máquina (S/N)
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

        #region Construtor e Variaveis Internas do Form
        bool formLoaded = false;
        public MsSqlBackup()
        {
            InitializeComponent();
            abrirConexaoBd();
            retornaTamanhoBaseDeDados();
            rodarComoAdmin();
            DriversRemoviveisDsponiveis();
            carregaArquivoConfiguracao();
            tbxCaminhoBackupLocal.Text = Directory.GetCurrentDirectory().ToString() + "\\Backups\\" + DateTime.Now.ToString("MMyyyy");
            carregaGridHistoricoBackups();
            recuperaStringConexaoSQLServerMaster();
            formLoaded = true;
        }
        #endregion
                
        #region Aba de Backup e Restauração do Sistema

        #region Parte dos Arquivos de Configuração, e Histórico de Backup (XMLs)
        public void recriaAtualizaArquivoConfiguracao()
        {
            if(File.Exists(Directory.GetCurrentDirectory() + "\\config.xml"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\config.xml");
            }

            XmlTextWriter escritorXML = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\config.xml", Encoding.UTF8);
            escritorXML.Formatting = Formatting.Indented;

            escritorXML.WriteStartDocument();
            escritorXML.WriteStartElement("configSyncServer");

            escritorXML.WriteElementString("caminhoBackupLocal", tbxCaminhoBackupLocal.Text);
            escritorXML.WriteElementString("caminhoBackupRemovivel", cbbDriversRemoviveis.Text.ToString());
            escritorXML.WriteElementString("gerenciarEspacoDiscoRem", chkApagarBackupsMaisAntigosAutomaticamente.Checked.ToString());
            escritorXML.WriteElementString("ativarBackupAutomatico", chkAtivarBackupAutomatico.Checked.ToString());
            escritorXML.WriteElementString("horarioBackupAutomatico", tbxHorarioBackup.Text);
            escritorXML.WriteElementString("diasBackupAutomatico", nudBackupDeQuantosEmQuantosDias.Text);
            escritorXML.WriteElementString("desligarAoConcluirBackupAutomatico", chkDesligarPCAoConcluir.Checked.ToString());
            escritorXML.WriteElementString("numeroBackupsAManter", nudLimiteBackups.Text);
            escritorXML.WriteElementString("translateEng", chkTranslateToENG.Checked.ToString());
            escritorXML.WriteEndElement();//finaliza o configSyncServer    
            escritorXML.WriteEndDocument();
            escritorXML.Close();
        }

        public void carregaArquivoConfiguracao()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\config.xml"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Directory.GetCurrentDirectory() + "\\config.xml");
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/configSyncServer");                
                foreach (XmlNode node in nodeList)
                {
                    tbxCaminhoBackupLocal.Text = node.SelectSingleNode("caminhoBackupLocal").InnerText;                    
                    cbbDriversRemoviveis.SelectedText = node.SelectSingleNode("caminhoBackupRemovivel").InnerText;
                    cbbDriversRemoviveis.Text = node.SelectSingleNode("caminhoBackupRemovivel").InnerText;
                    tbxHorarioBackup.Text = node.SelectSingleNode("horarioBackupAutomatico").InnerText;
                    chkApagarBackupsMaisAntigosAutomaticamente.Checked = Convert.ToBoolean(node.SelectSingleNode("gerenciarEspacoDiscoRem").InnerText);
                    chkAtivarBackupAutomatico.Checked = Convert.ToBoolean(node.SelectSingleNode("ativarBackupAutomatico").InnerText);
                    chkDesligarPCAoConcluir.Checked = Convert.ToBoolean(node.SelectSingleNode("desligarAoConcluirBackupAutomatico").InnerText);
                    nudBackupDeQuantosEmQuantosDias.Value = Convert.ToDecimal(node.SelectSingleNode("diasBackupAutomatico").InnerText);
                    nudLimiteBackups.Value = Convert.ToDecimal(node.SelectSingleNode("numeroBackupsAManter").InnerText);
                    chkTranslateToENG.Checked = Convert.ToBoolean(node.SelectSingleNode("translateEng").InnerText);
                    
                    #region Tradução de Labels para Inglês (translation to ENG)
                    if(chkTranslateToENG.Checked)
                    {
                        lblCaminhoDoBackup.Text = "Path to Local Backup:";
                        lblCaminhoRemovivel.Text = "Path Removable Devide (PenDrive/HD):";
                        chkAtivarBackupAutomatico.Text = "Automatic Backup";
                        lblPeridiocidade.Text = "Frequency (1 = backup everyday)";
                        lblNumeroBackupsManter.Text = "Number of Backups to keep (0 = never delete olders)";
                        chkApagarBackupsMaisAntigosAutomaticamente.Text = "If Removable Device out of space, auto-delete olders backup";
                        chkDesligarPCAoConcluir.Text = "ShutDown computer when done";
                        lnkEntendaBackup.Text = "Understand Backup System";
                        btnSalvarBackupAgora.Text = "Make Backup Now";
                        btnRestaurarBackup.Text = "Restore Backup";
                        tbpBackupRestauracao.Text = "Backup and Restore";
                        tbpGerenciadorBancoDados.Text = "DataBase Manager and Firewall Access";
                        btnLiberarFirewall.Text = "Execute auto the Steps (Open Ports and Programs) to SQL Server works on Network";
                        btnExecutarSQL.Text = "Execute SQL";
                        grpConexao.Text = "Connection and information about DataBase:";
                        grpExecutorSQL.Text = "SQL-Executor and Querys on DataBase (Operations Query Executor)";
                    }
                    #endregion
                }   
            }//fim if File.Exists()
            else
            {
                recriaAtualizaArquivoConfiguracao();
            }
        }

        public void carregaGridHistoricoBackups()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\backups.xml"))
            {
                DataSet dsDados = new DataSet();
                dsDados.ReadXml(Directory.GetCurrentDirectory() + "\\backups.xml");
                lvwBackupsLocais.Items.Clear(); //limpo o ListView para mostrar nova consulta
                lvwBackupsLocais.Columns.Clear();

                if (chkTranslateToENG.Checked)
                {
                    lvwBackupsLocais.Columns.Add("", 0, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Local Backup Path", 420, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Available", 110, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Removible Backup Path", 420, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Available", 110, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Date of Backup", 140, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Size", 85, HorizontalAlignment.Center);                    
                }
                else
                {
                    lvwBackupsLocais.Columns.Add("", 0, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Caminho Backup Local", 420, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Disponível", 110, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Caminho Backup Removível", 420, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Disponível", 110, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Data do Backup", 140, HorizontalAlignment.Left);
                    lvwBackupsLocais.Columns.Add("Tamanho", 85, HorizontalAlignment.Center);
                }
                
                int indiceListView = 0;
                for (int i = dsDados.Tables[0].Rows.Count-1; i >= 0; i--) //desenho ao contrário sendo o backup mais novo em cima
                {
                    lvwBackupsLocais.Items.Add("");
                    if (i % 2 == 0)
                    {
                        lvwBackupsLocais.Items[indiceListView].BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lvwBackupsLocais.Items[indiceListView].BackColor = Color.White;
                    }
                    lvwBackupsLocais.Items[indiceListView].SubItems.Add(dsDados.Tables[0].Rows[i]["caminhoBackupLocal"].ToString());
                    if(File.Exists(dsDados.Tables[0].Rows[i]["caminhoBackupLocal"].ToString()))
                    {
                        lvwBackupsLocais.Items[indiceListView].SubItems.Add("OK");
                    }
                    else
                    {
                        lvwBackupsLocais.Items[indiceListView].SubItems.Add(" -- ");
                    }

                    lvwBackupsLocais.Items[indiceListView].SubItems.Add(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString());
                    if (File.Exists(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString()))
                    {
                        lvwBackupsLocais.Items[indiceListView].SubItems.Add("OK");
                    }
                    else
                    {
                        lvwBackupsLocais.Items[indiceListView].SubItems.Add(" -- ");
                    }

                    lvwBackupsLocais.Items[indiceListView].SubItems.Add(dsDados.Tables[0].Rows[i]["dataBackup"].ToString());
                    lvwBackupsLocais.Items[indiceListView].SubItems.Add(dsDados.Tables[0].Rows[i]["tamanhoBackup"].ToString());
                    indiceListView++;
                }//fim for
            }//fim if (File.Exists(Directory.GetCurrentDirectory() + "\\config.xml"))            
        }
        #endregion

        #region Carrega Drivers Removíveis do Windows
        public void DriversRemoviveisDsponiveis()
        {
            List<DriveInfo> drivers = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Removable).ToList();
            DataTable dtDados = new DataTable();
            dtDados.Columns.Add("ID");
            dtDados.Columns.Add("Descricao");

            int indice = 1;
            foreach (DriveInfo driver in drivers)
            {
                DataRow DR = dtDados.NewRow();
                DR["ID"] = indice.ToString();

                string nomeDispositivo = driver.Name.ToString();
                string volumeLabel = driver.VolumeLabel;
                long tamanhoTotal = driver.TotalSize;
                long espacoLivre = driver.TotalFreeSpace;

                tamanhoTotal = tamanhoTotal / 1024; //bytes
                tamanhoTotal = tamanhoTotal / 1024; //kbytes
                espacoLivre = espacoLivre / 1024; //bytes
                espacoLivre = espacoLivre / 1024; //kbytes

                DR["Descricao"] = nomeDispositivo + " " + volumeLabel + " " + tamanhoTotal.ToString() + "MB (" + espacoLivre.ToString() + "MB livres)";
                dtDados.Rows.Add(DR);
                indice++;
            }

            if (dtDados.Rows.Count != 0)
            {                
                cbbDriversRemoviveis.DataSource = dtDados;
                cbbDriversRemoviveis.DisplayMember = "Descricao".Trim().ToString();
                cbbDriversRemoviveis.ValueMember = "ID".Trim().ToString();
                btnSalvarBackupAgora.Enabled = true;                
                lblProgressoBackup.Refresh();
                lblProgressoBackup.ForeColor = Color.DarkBlue;
            }
            else
            {
                btnSalvarBackupAgora.Enabled = false;
                lblProgressoBackup.Text = "BACKUP PARADO! Não há unidades removíveis disponíveis. Insira um PenDrive e configure.";
                if(chkTranslateToENG.Checked)
                {
                    lblProgressoBackup.Text = "Backup Stoped! There's not a removible device available. Please insert a pendrive and configure.";
                }
                lblProgressoBackup.ForeColor = Color.DarkRed;
            }
        }
        
        private void btnSelecionarPenDriveBackup_Click(object sender, EventArgs e)
        {
            DriversRemoviveisDsponiveis();
        }
        #endregion

        #region Rodar Programa como Administrador
        private void rodarComoAdmin()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool administrativeMode = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!administrativeMode)
            {
                MessageBox.Show(null, "Esse aplicativo necessita rodar como Administrador: Clique com o Botão Direito e selecione 'Executar como Administrador', e confirme SIM na tela. Não será possível continuar, encerramos agora." + Environment.NewLine + Environment.NewLine + "This applications needs to run as Administrator: Right click on it and 'run as administrator'. Impossible to run, will finish now.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                
                FilaProcessosWindows.finalizarPrograma();
                //throw new Exception("Não foi possível conceder acesso como Admin" + Environment.NewLine + "As operações realizadas poderão ter Acesso Negado !");                
            }
        }
        #endregion

        #region String de Conexão para o SQL Server, Abre Conexão, Retorna tamanho Base, Caminho e outros métodos pro Banco de Dados
        public SqlConnection abrirConexaoBd()
        {
            if(!File.Exists(Directory.GetCurrentDirectory().ToString() + @"\database.xml"))
            {
                MessageBox.Show(null, "Não foi possível encontrar o Arquivo 'database.xml' na raíz desse aplicativo. Impossível determinar banco. Verifique a documentação desse projeto e tente novamente." + Environment.NewLine + Environment.NewLine + "Can't find file 'database.xml' on the root of this app. Impossible to find DataBase. Verify readme and try again", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilaProcessosWindows.finalizarPrograma();
            }

            SqlConnection ConexaoBd = new SqlConnection(recuperaStringConexaoSQLServer());
            try
            {
                ConexaoBd.Open();
                return ConexaoBd;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                #region Aviso o Usuário que não foi possível conectar ao banco informado
                MessageBox.Show(null, "Não foi possível conectar ao banco de dados. Por favor configure sua conexão corretamente o database.xml na raíz desse programa. Verifique se o Banco e Serviço do SQL estão disponíveis." + Environment.NewLine + Environment.NewLine + "Can't connect to the database. Please configure your connection on 'database.xml' in the root of this program. Check if Database and Service are running.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FilaProcessosWindows.finalizarPrograma();
                return ConexaoBd;
                #endregion
            }//fim catch
        }
                
        public string recuperaStringConexaoSQLServer()
        {
            DataSet dsDadosXML = new DataSet();
            string caminho = Directory.GetCurrentDirectory().ToString() + @"\database.xml";
            dsDadosXML.ReadXml(caminho);
            string servidor = dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString();
            string instancia = dsDadosXML.Tables[0].Rows[0]["INSTANCIA"].ToString();
            string servico = dsDadosXML.Tables[0].Rows[0]["SERVICOSQL"].ToString();
            string baseDados = dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString();
            string aceitaConexoesSeguras = dsDadosXML.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString();
            string usuario = dsDadosXML.Tables[0].Rows[0]["USUARIO"].ToString();
            string senha = dsDadosXML.Tables[0].Rows[0]["SENHA"].ToString();
            string conexao = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + baseDados + ";Integrated Security=" + aceitaConexoesSeguras + ";";
            if (aceitaConexoesSeguras.ToUpper() == "FALSE")
            {
                conexao = conexao + "User ID=" + usuario + ";Password=" + senha + ";";
            }
            return conexao;
        }//fim classe

        public SqlConnection abrirConexaoBdMaster()
        {
            SqlConnection ConexaoBd = new SqlConnection(recuperaStringConexaoSQLServerMaster());
            try
            {
                ConexaoBd.Open();
                return ConexaoBd;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                #region Aviso o Usuário que não foi possível conectar ao banco informado
                MessageBox.Show(null, "Não foi possível conectar ao banco de dados. Por favor configure sua conexão corretamente o database.xml na raíz desse programa. Verifique se o Banco e Serviço do SQL estão disponíveis." + Environment.NewLine + Environment.NewLine + "Can't connect to the database. Please configure your connection on 'database.xml' in the root of this program. Check if Database and Service are running.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FilaProcessosWindows.finalizarPrograma();
                return ConexaoBd;
                #endregion
            }
        }

        public string recuperaStringConexaoSQLServerMaster()
        {
            DataSet dsDadosXML = new DataSet();
            string caminho = Directory.GetCurrentDirectory().ToString() + @"\database.xml";
            dsDadosXML.ReadXml(caminho);
            string servidor = dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString();
            string instancia = dsDadosXML.Tables[0].Rows[0]["INSTANCIA"].ToString();
            string servico = dsDadosXML.Tables[0].Rows[0]["SERVICOSQL"].ToString();
            string baseDados = "master"; //dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString();
            string aceitaConexoesSeguras = dsDadosXML.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString();
            string usuario = dsDadosXML.Tables[0].Rows[0]["USUARIO"].ToString();
            string senha = dsDadosXML.Tables[0].Rows[0]["SENHA"].ToString();            

            DataTable dtDados = retornaInformacoesSobreBancoDeDados();
            string nomeMaquina = System.Environment.MachineName.ToString();

            if(nomeMaquina != servidor)
            {
                if(chkTranslateToENG.Checked)
                {
                    MessageBox.Show(null, "Esse programa só pode rodar no SERVIDOR onde está o banco de dados. Não é possível rodar em terminais de rede.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(null, "This Program can Only Run Local on the Server of DataBase. It's not possible to run on a network.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                FilaProcessosWindows.finalizarPrograma();
            }

            if (!chkTranslateToENG.Checked)
            {
                lblDBNomeServidorBancoDados.Text = "Server name of DataBase: " + servidor;
                lblDBServicoBancoDados.Text = "Nome Serviço Banco de Dados: " + servico;                
                lblDBNomeBaseDados.Text = "Nome Base de Dados: " + dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString();
                lblDBNomeInstancia.Text = "Nome Instância: " + instancia;
                lblDBVersaoBancoDados.Text = "Versão Servidor Banco de Dados: " + dtDados.Rows[0]["Versao"].ToString();
                lblDBServicePackBancoDados.Text = "ServicePack do Banco de Dados: " + dtDados.Rows[0]["ServicePack"].ToString();
                lblDBEdicaoBancoDados.Text = "Edição do Banco de Dados: " + dtDados.Rows[0]["Edicao"].ToString();
                lblBDNomeMaquina.Text = "Nome da Máquina: " + nomeMaquina;
                lblDBSerialMaquina.Text = "Serial da Máquina: " + retornaNumeroSerieHD();
            }
            else
            {
                lblDBNomeServidorBancoDados.Text = "Name of the DataBase Server: " + servidor;
                lblDBServicoBancoDados.Text = "Name of the Service: " + servico;                
                lblDBNomeBaseDados.Text = "Name of the DataBase: " + dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString();
                lblDBNomeInstancia.Text = "Name of the Instance: " + instancia;
                lblDBVersaoBancoDados.Text = "Version of the DataBase Service: " + dtDados.Rows[0]["Versao"].ToString();
                lblDBServicePackBancoDados.Text = "SQL Server ServicePack: " + dtDados.Rows[0]["ServicePack"].ToString();
                lblDBEdicaoBancoDados.Text = "SQL Server Edition: " + dtDados.Rows[0]["Edicao"].ToString();
                lblBDNomeMaquina.Text = "Name of Machine: " + nomeMaquina;
                lblDBSerialMaquina.Text = "Serial Number of Machine: " + retornaNumeroSerieHD();
            }

            string conexao = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + baseDados + ";Integrated Security=" + aceitaConexoesSeguras + ";";
            if (aceitaConexoesSeguras.ToUpper() == "FALSE")
            {
                conexao = conexao + "User ID=" + usuario + ";Password=" + senha + ";";
            }
            return conexao;
        }//fim classe

        /// <summary>
        /// Retorna o Tamanho da Base de Dados (124MB por exemplo)
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna o tamanho da base, "ERRO" no caso de falha na consulta</returns>
        public string retornaTamanhoBaseDeDados()
        {
            SqlConnection conexao = abrirConexaoBd();
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
                return "ERRO";
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

        /// <summary>
        /// Retorna o Tamanho da Base de Dados (124MB por exemplo)
        /// </summary>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna o tamanho da base, "ERRO" no caso de falha na consulta</returns>
        public string retornaCaminhoBaseDeDados()
        {
            SqlConnection conexao = abrirConexaoBdMaster();
            SqlCommand comando = new SqlCommand(); //cria um objeto do tipo comando

            comando.CommandText = "SELECT * FROM sys.database_files"; //define a Stored que será chamada
            comando.CommandType = CommandType.Text; //define que é uma StoredProcedure
            comando.Connection = conexao; //abre a conexão
            comando.ExecuteNonQuery(); //executa a query
            //cria um adaptador de dados para ler os dados que voltam
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dadosOrcamento = new DataSet();

            try
            {
                //preenche o DATASET com os dados retornados
                dataAdapter.Fill(dadosOrcamento);
                string caminho = dadosOrcamento.Tables[0].Rows[0]["physical_name"].ToString().Replace("master.mdf", "Business.mdf");
                return caminho;
            }

            catch (Exception erro)
            {
                return "ERRO";
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

        public DataTable retornaInformacoesSobreBancoDeDados()
        {
            StringBuilder sqlConcatenada = new StringBuilder();
            sqlConcatenada.Append("SELECT ");
            sqlConcatenada.Append("SERVERPROPERTY ('MachineName') as Servidor, ");
            sqlConcatenada.Append("SERVERPROPERTY('productversion') as Versao, ");
            sqlConcatenada.Append("SERVERPROPERTY ('productlevel')as ServicePack,  ");
            sqlConcatenada.Append("SERVERPROPERTY ('edition')as Edicao ");

            SqlConnection conexao = abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(sqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet);
                return DsDataSet.Tables[0];
            }//fim TRY

            catch (Exception erro)
            {
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {                
                conexao = null;
            }
        }

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

        #region Executa uma Operação SQL no banco        
        public DataTable executaOperacaoSQL(string operacaoSQL)
        {
            SqlConnection conexao = abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.Text;
            ComandoSQL.CommandText = operacaoSQL;
            SqlDataAdapter DataAdap = new SqlDataAdapter(operacaoSQL.ToString(), conexao);
            DataSet dsDados = new DataSet();
            try
            {
                ComandoSQL.ExecuteNonQuery();
                DataAdap.Fill(dsDados);
                return dsDados.Tables[0];
            }
            catch (Exception erro)
            {
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                conexao = null;
                ComandoSQL = null;
            }
        }
        #endregion

        #region Métodos e Eventos da Parte de Backup e Restauração do Sistema

        #region Método Efetuar Backup do Sistema
        public void efetuaBackupDoSistema()
        {
            #region Verifico se o PenDrive não encontra-se cheio e apago backups mais antigos
            List<DriveInfo> drivers = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Removable).ToList();            
            foreach (DriveInfo driver in drivers)
            {
                string nomeDispositivo = driver.Name.ToString();
                string volumeLabel = driver.VolumeLabel;
                long tamanhoTotal = driver.TotalSize;
                long espacoLivre = driver.TotalFreeSpace;

                tamanhoTotal = tamanhoTotal / 1024; //bytes
                tamanhoTotal = tamanhoTotal / 1024; //kbytes
                espacoLivre = espacoLivre / 1024; //bytes
                espacoLivre = espacoLivre / 1024; //kbytes

                if (File.Exists(Directory.GetCurrentDirectory() + "\\backups.xml"))
                {
                    if (cbbDriversRemoviveis.Text.StartsWith(nomeDispositivo)) //achei o Drive, agora preciso verificar se ainda há espaço para armazenar o backup
                    {
                        //pego o tamanho do último backup armazenado no drive pra ter um "parametro" de quanto espaço preciso
                        DataSet dsDados = new DataSet();
                        dsDados.ReadXml(Directory.GetCurrentDirectory() + "\\backups.xml");
                        if (dsDados.Tables[0].Rows.Count != 0)
                        {
                            long tamanhoUltimoBackup = Convert.ToInt32(dsDados.Tables[0].Rows[dsDados.Tables[0].Rows.Count - 1]["tamanhoBackup"].ToString().Replace("MB", ""));
                            tamanhoUltimoBackup = tamanhoUltimoBackup + 100; //aumento em 100MB, considerando que o backup de hoje pode ser um pouco maior que o último

                            if (tamanhoUltimoBackup > espacoLivre)
                            {
                                if (chkApagarBackupsMaisAntigosAutomaticamente.Checked)
                                {
                                    //apago o mais antigo automaticamente no pendrive
                                    DataSet dsDadosGrid = new DataSet();
                                    dsDadosGrid.ReadXml(Directory.GetCurrentDirectory() + "\\backups.xml");

                                    int apagaDoisUltimos = 0;

                                    for (int i = 0; i < dsDados.Tables[0].Rows.Count; i++) //desenho ao contrário sendo o backup mais novo em cima
                                    {
                                        if (apagaDoisUltimos < 2)
                                        {
                                            if (File.Exists(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString()))
                                            {
                                                File.Delete(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString());
                                                apagaDoisUltimos++;
                                            }
                                        }
                                    }
                                    carregaGridHistoricoBackups();
                                }
                                else
                                {
                                    btnSalvarBackupAgora.Enabled = false;
                                    lblProgressoBackup.Text = "BACKUP PARADO! A Unidade Removível está cheia. Apague Backups antigos ou ative a opção 'Apagar Backups Antigos automaticamente'.";
                                    if (chkTranslateToENG.Checked)
                                    {
                                        lblProgressoBackup.Text = "Backup stoped! The Removible device is full. Delete old backups or activate the 'Automatic Delete Old Backups' option.";
                                    }
                                    lblProgressoBackup.ForeColor = Color.DarkRed;
                                }
                            }//fim if(tamanhoUltimoBackup > espacoLivre)
                        }//fim if (dsDados.Tables[0].Rows.Count != 0)
                    }//fim if cbbDriversRemoviveis
                }
            }
            #endregion

            if (btnSalvarBackupAgora.Enabled) //se não tem pendrive disponível o botão fica desabilitado, ai não pode fazer backup!
            {
                #region Finalizo o Processo do Business e crio as variaveis do caminho do banco
                tmrBackup.Enabled = false; //paro o ticker do relógio pra não atrapalhar
                recriaAtualizaArquivoConfiguracao();
                //Fernando 27/11/2018 02:07 da manhã, Győr - HU, madrugada, ontem eu peguei o Peugeot 206, agora estou trabalhando no gás pra terminar as coisas do Business,
                //e podermos mudar pra Budapest em breve! Trabalho firme e forte Fernando, deus abençoe a gente meu velho! Vai na luta.
                lblProgressoBackup.Text = "Iniciando Backup, parando servidor banco e sistema...";
                if (chkTranslateToENG.Checked)
                {
                    lblProgressoBackup.Text = "Starting Backup, stoping database server...";
                }

                lblProgressoBackup.Refresh();
                prgAndamentoBackup.Value = 1;
                prgAndamentoBackup.Refresh();

                //FilaProcessosWindows.finalizarProcessoPorNome("FuturaData Business"); //Se você quiser que esse aplicativo feche o seu, coloque o nome do processo aqui
                
                DateTime dataRegistroBackup = DateTime.Now; //vou registrar a data que o backup foi feito (copiado pro disco) pra não gerar um LOG com segundos a mais...
                Thread.Sleep(1000);

                string caminhoBancoDadosMdf = retornaCaminhoBaseDeDados();
                string caminhoBancoDadosLdf = caminhoBancoDadosMdf.Replace(".mdf", "_log.ldf");

                //NOTA FERNANDO: SE FOR SQL LOCAL DB, NÃO VAI TER SERVIÇO, AI EU NÃO PRECISO PARAR, ADOTAR OUTRA ESTRATÉGIA.
                int statusServicoSQL = 1;
                #endregion

                #region Para Serviço do Windows
                ServiceController[] services = ServiceController.GetServices();
                DataSet dsDadosXML = new DataSet();                
                string caminho = Directory.GetCurrentDirectory().ToString() + @"\database.xml";
                dsDadosXML.ReadXml(caminho);
                string servico = dsDadosXML.Tables[0].Rows[0]["SERVICOSQL"].ToString();

                foreach (ServiceController service in services)
                {
                    if (service.DisplayName == servico)
                    {
                        statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                        if (statusServicoSQL == 1) //rodando
                        {
                            FilaProcessosWindows.PararServico(service.DisplayName, 10000);
                            Thread.Sleep(6000); //paro e espero o serviço parar...
                        }
                        statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);

                        if (statusServicoSQL == 1) //1 é sinal que ainda está rodando, ai eu vou tentar PARAR ele de novo, vou fazer uma segunda tentativa
                        {
                            FilaProcessosWindows.PararServico(service.DisplayName, 10000);
                            Thread.Sleep(6000); //paro e espero o serviço parar...
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                        }//sim status ==1                    
                    }//fim if servico.Dislayname
                }//fim foreach
                #endregion

                #region Efetua o Backup
                if (statusServicoSQL == 0)//se estiver 0 está parado, ai posso continuar
                {
                    lblProgressoBackup.Text = "Serviço parado, iniciando backup do banco...";
                    if(chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Service stoped, starting database backup...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 2;
                    prgAndamentoBackup.Refresh();

                    #region Verifico e Crio os Diretórios para Backup
                    if (!Directory.Exists(tbxCaminhoBackupLocal.Text))
                    {
                        Directory.CreateDirectory(tbxCaminhoBackupLocal.Text);
                    }

                    string caminhoBackupPenDrive = cbbDriversRemoviveis.Text.Substring(0, 3); //irá pegar algo como e:\
                    caminhoBackupPenDrive = caminhoBackupPenDrive + "MsSqlBackups\\" + DateTime.Now.ToString("MMyyyy");

                    if (!Directory.Exists(caminhoBackupPenDrive))
                    {
                        Directory.CreateDirectory(caminhoBackupPenDrive);
                    }
                    #endregion

                    #region Copio os Arquivos

                    //Estratégia Fernando: Copio os dois arquivos pro Disco C:\MsSqlServer\Backups, feito isso, eu subo o banco de novo e libero o sistema...
                    //ai então eu vou zipar os arquivos (tratar) e só depois vou copiar o backup já pronto pro PenDrive

                    string caminhoMdfNoDiscoC = tbxCaminhoBackupLocal.Text + "\\" + lblDBNomeBaseDados.Text.Replace("Nome Base de Dados: ", "").Replace("Name of the DataBase: ", "") + ".mdf";
                    string caminhoLdfNoDiscoC = tbxCaminhoBackupLocal.Text + "\\" + lblDBNomeBaseDados.Text.Replace("Nome Base de Dados: ", "").Replace("Name of the DataBase: ", "") + "_log.ldf";
                    string caminhoDestinoZipDiscoLocal = tbxCaminhoBackupLocal.Text + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".zip";
                    string caminhoDestinoZipPenDrive = caminhoBackupPenDrive + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".zip";
                    dataRegistroBackup = DateTime.Now;
                    if (File.Exists(caminhoMdfNoDiscoC))
                    {
                        File.Delete(caminhoMdfNoDiscoC);
                    }
                    if (File.Exists(caminhoLdfNoDiscoC))
                    {
                        File.Delete(caminhoLdfNoDiscoC);
                    }

                    File.Copy(caminhoBancoDadosMdf, caminhoMdfNoDiscoC);
                    File.Copy(caminhoBancoDadosLdf, caminhoLdfNoDiscoC);

                    lblProgressoBackup.Text = "Banco copiado, iremos subir o sistema e liberar o uso...";
                    if(chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Bank Copied, will start the service again...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 3;
                    prgAndamentoBackup.Refresh();
                    Thread.Sleep(3000);
                    #endregion

                    #region Subo de Novo o Serviço do SQL Server e Libero o Uso do Sistema
                    statusServicoSQL = 0;
                    foreach (ServiceController service in services)
                    {
                        if (service.DisplayName == servico)
                        {
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            if (statusServicoSQL != 1) //se for diferente de 1 ainda não está rodando
                            {
                                FilaProcessosWindows.IniciarServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                            }
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName);

                            if (statusServicoSQL != 1) //se for diferente de 1 ainda não está rodando
                            {
                                FilaProcessosWindows.IniciarServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                                statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            }//sim status ==1       

                            if (statusServicoSQL != 1)
                            {
                                if (chkTranslateToENG.Checked)
                                {
                                    MessageBox.Show(null, "The MsSqlServer can't start database service again. Maybe you'll need to restart this server.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBox.Show(null, "O MsSqlServer não conseguiu subir novamente o Serviço do Banco de Dados. Talvez seja necessário reiniciar esse Servidor para poder usar o sistema.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }

                        }//fim if servico.Dislayname
                    }//fim foreach

                    if (statusServicoSQL == 1)
                    {
                        lblProgressoBackup.Text = "Pronto! Sistema Online e liberado para uso, agora aguarde o fim do backup...";
                        if(chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "AlReady! System Online an you can use it, now wait for the end of backup process...";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 4;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(3000);
                    }
                    #endregion

                    #region Agora zipo o arquivo                
                    using (ZipFile zip = new ZipFile())
                    {
                        // add this map file into the "images" directory in the zip archive
                        //zip.AddFile("c:\\images\\personal\\7440-N49th.png", "images");
                        // add the report into a different directory in the archive
                        //zip.AddFile("c:\\Reports\\2008-Regional-Sales-Report.pdf", "files");
                        //zip.AddFile("ReadMe.txt");
                        //zip.Save("MyZipFile.zip");
                        lblProgressoBackup.Text = "Compactando banco de dados em zip e preparando arquivos...";
                        if(chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "Compacting the database in zip format, preparing files...";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 5;
                        prgAndamentoBackup.Refresh();
                        zip.AddFile(caminhoBancoDadosMdf, "");
                        zip.AddFile(caminhoBancoDadosLdf, "");
                        zip.Save(caminhoDestinoZipDiscoLocal);
                    }
                    #endregion

                    #region Apago Arquivos MDF e LDF após zipar e copio .zip pro pendrive
                    try
                    {
                        lblProgressoBackup.Text = "Apagando arquivos temporários e copiando para disp.removível...";
                        if(chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "Deleting temp files and copying to external drive...";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 6;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(2000);
                        File.Copy(caminhoDestinoZipDiscoLocal, caminhoDestinoZipPenDrive);
                        File.Delete(caminhoMdfNoDiscoC);
                        File.Delete(caminhoLdfNoDiscoC);
                    }
                    catch
                    {

                    }
                    #endregion

                    #region Registro o Backup no XML de histórico
                    lblProgressoBackup.Text = "Registrando LOG do backup e gerando informações finais...";
                    if(chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Generating LOG and generating final information...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 7;
                    prgAndamentoBackup.Refresh();
                    Thread.Sleep(2000);
                    string caminhoXmlRegistroBackups = Directory.GetCurrentDirectory() + "\\backups.xml";
                    if (File.Exists(caminhoXmlRegistroBackups))
                    {
                        //create new instance of XmlDocument
                        XmlDocument doc = new XmlDocument();
                        doc.Load(caminhoXmlRegistroBackups);
                        long tamanhoArquivo = new System.IO.FileInfo(caminhoDestinoZipDiscoLocal).Length;
                        tamanhoArquivo = tamanhoArquivo / 1024; //bytes
                        tamanhoArquivo = tamanhoArquivo / 1024; //kbytes
                                                                //create node and add value
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "registroBackup", null);

                        XmlNode nodeCaminhoBackupLocal = doc.CreateElement("caminhoBackupLocal");
                        nodeCaminhoBackupLocal.InnerText = caminhoDestinoZipDiscoLocal;
                        node.AppendChild(nodeCaminhoBackupLocal);

                        XmlNode nodeCaminhoDestinoZipPenDrive = doc.CreateElement("caminhoBackupPenDrive");
                        nodeCaminhoDestinoZipPenDrive.InnerText = caminhoDestinoZipPenDrive;
                        node.AppendChild(nodeCaminhoDestinoZipPenDrive);

                        XmlNode nodeDataBackup = doc.CreateElement("dataBackup");
                        nodeDataBackup.InnerText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        node.AppendChild(nodeDataBackup);

                        XmlNode nodeTamanhoBackup = doc.CreateElement("tamanhoBackup");
                        nodeTamanhoBackup.InnerText = tamanhoArquivo.ToString() + "MB";
                        node.AppendChild(nodeTamanhoBackup);

                        doc.DocumentElement.AppendChild(node);
                        doc.Save(caminhoXmlRegistroBackups);

                        lblProgressoBackup.Text = "Pronto, processo finalizado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ". Backup 100% Efetuado!";
                        if(chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "Well done, process finished in " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ". Backup 100% done!";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 8;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        XmlTextWriter escritorXML = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\backups.xml", Encoding.UTF8);
                        escritorXML.Formatting = Formatting.Indented;
                        long tamanhoArquivo = new System.IO.FileInfo(caminhoDestinoZipDiscoLocal).Length;
                        tamanhoArquivo = tamanhoArquivo / 1024; //bytes
                        tamanhoArquivo = tamanhoArquivo / 1024; //kbytes
                                                                //tamanhoArquivo = tamanhoArquivo / 1024; //mbytes

                        escritorXML.WriteStartDocument();
                        escritorXML.WriteStartElement("logBackups");
                        escritorXML.WriteStartElement("registroBackup");
                        escritorXML.WriteElementString("caminhoBackupLocal", caminhoDestinoZipDiscoLocal);
                        escritorXML.WriteElementString("caminhoBackupPenDrive", caminhoDestinoZipPenDrive);
                        escritorXML.WriteElementString("dataBackup", dataRegistroBackup.ToString("dd/MM/yyyy HH:mm:ss"));
                        escritorXML.WriteElementString("tamanhoBackup", tamanhoArquivo.ToString() + "MB");
                        escritorXML.WriteEndElement();//finaliza o configSyncServer    
                        escritorXML.WriteEndElement();//finaliza o configSyncServer    
                        escritorXML.WriteEndDocument();
                        escritorXML.Close();

                        lblProgressoBackup.Text = "Pronto, processo finalizado e Backup 100% efetuado!";
                        if (chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "Well done, finished process and Backup 100% done!";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 8;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(2000);
                    }                    
                    #endregion

                    #region Verifico se opção de limitar backups está habilitada, se estiver eu apago os backups mais antigos
                    if(nudLimiteBackups.Value > 0)
                    {
                        int numeroBackupsManter = Convert.ToInt32(nudLimiteBackups.Value);
                        int indiceBackupAtual = 0;
                        DataSet dsDados = new DataSet();
                        dsDados.ReadXml(Directory.GetCurrentDirectory() + "\\backups.xml");
                        

                        for (int i = dsDados.Tables[0].Rows.Count - 1; i >= 0; i--) //desenho ao contrário sendo o backup mais novo em cima
                        {
                            if(indiceBackupAtual >= numeroBackupsManter)
                            {
                                if (File.Exists(dsDados.Tables[0].Rows[i]["caminhoBackupLocal"].ToString()))
                                {
                                    File.Delete(dsDados.Tables[0].Rows[i]["caminhoBackupLocal"].ToString());
                                }
                                if (File.Exists(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString()))
                                {
                                    File.Delete(dsDados.Tables[0].Rows[i]["caminhoBackupPenDrive"].ToString());
                                }
                            }
                            indiceBackupAtual++;
                        }//fim for
                    }//fim if nudLimiteBackups

                    DriversRemoviveisDsponiveis();
                    carregaGridHistoricoBackups();

                    if(chkDesligarPCAoConcluir.Checked) //faz computador desligar
                    {
                        var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;
                        Process.Start(psi);
                    }
                    #endregion
                }//fim status==0
                else
                {
                    if (chkTranslateToENG.Checked)
                    {
                        MessageBox.Show(null, "This application can't stop the Service of DataBase. Impossible to continue." + Environment.NewLine + Environment.NewLine + "(1) Be sure that systems are closed and the networks terminals." + Environment.NewLine + "(2) Ensure that you have clicked with Right Button and selected 'Run as Administrator'." + Environment.NewLine + "(3) If problem persist, reboot your machine e without opening any program, try to run it again.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(null, "Esse aplicativo não conseguiu parar o Serviço do Banco de Dados para efetuar um Backup. É impossível continuar." + Environment.NewLine + Environment.NewLine + "(1) Certifique-se que o sistema encontra-se fechado nesse Servidor e nos Terminais de Rede." + Environment.NewLine + "(2) Certifique-se de ter clicado com o botão direito nesse aplicativo e selecionado 'Executar como Administrador'." + Environment.NewLine + "(3) Caso o problema persista, reinicie esse computador, e SEM ABRIR o seu sistema e tente executar esse aplicativo e fazer o backup.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }//fim else
                tmrBackup.Enabled = true; //volto o ticker
                #endregion
            }
        }//fim efetuaBackupSistema
        #endregion

        #region Evento do Botão Salvar Backup Agora
        private void btnSalvarBackupAgora_Click(object sender, EventArgs e)
        {
            if (chkTranslateToENG.Checked)
            {
                if (MessageBox.Show(null, "MsSqlBackup will do a Full-Backup of your DataBase Now. To continue, we will stop the DataBase." + Environment.NewLine + Environment.NewLine + "Close all Systems and Terminals before continue. Follow-up the messages and this system will notify when done. Allow to continue?", "MsSqlServer Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    efetuaBackupDoSistema();
                }
            }
            else
            {
                if (MessageBox.Show(null, "O Syncserver efetuará um Backup Completo de seu Banco de Dados agora. Para continuar, o Sistema será finalizado nesse terminal e iremos para o Serviço do Banco de Dados para Cópia." + Environment.NewLine + Environment.NewLine + "Feche os outros terminais do sistema em rede antes de continuar. Acompanhe a mensagem de progresso azul que avisará quando você já pode voltar a abrir o sistema. Deseja continuar?", "MsSqlServer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    efetuaBackupDoSistema();
                }
            }
        }
        #endregion        

        #region Link Entenda Backup

        private void lnkEntendaBackup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chkTranslateToENG.Checked)
            {
                MessageBox.Show(null, "Every Backup will be saved in 2 different places: " + Environment.NewLine + Environment.NewLine + "(1) In the Same Directory of this Applicattion." + Environment.NewLine +
                    "(2) In a Pendrive/Hard Disk (external) pluged to this Server (we recomend at last a 8GB device plugged to this server all the time)." + Environment.NewLine + Environment.NewLine +
                    "The backups will be saved in the format DD/MM/YYYY/HH/MM and compacted (ziped) automatic." + Environment.NewLine + Environment.NewLine + "You can program this applicattion to do the backups automatic" +
                    ", just activate the option, and tell how many days it should do, and in what hour it should do." + Environment.NewLine + Environment.NewLine + "Note: MsSqlBackup will always shutdown the database system in the moment of backup," +
                    " for some seconds, while it's make. Terminals of network may be out." + Environment.NewLine + Environment.NewLine + "This application also allows you to restore backups." +
                    "This tools should Run only on the Server Machine (where the DataBase is installed)." + Environment.NewLine + Environment.NewLine + "You can also activate the option to Automatic remove older backups. This way it will " +
                    " manage automatic the space in the external device, deleting the old backups when it becames full (without space)." + Environment.NewLine + Environment.NewLine + "Note: If your Business closes at 18h:00pm, you can program this application " +
                    " to 18:15pm to do the backup and then 'Shutdown when done'. On next day, turn it on and work!", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Cada Backup Efetuado é armazenado em 2 locais diferentes: " + Environment.NewLine + Environment.NewLine + "(1) No Diretório Backup dentro da Pasta desse Aplicativo." + Environment.NewLine +
                    "(2) Em um PenDrive/HD Externo ligado ao Servidor (Recomendamos um dispositivo exclusivo superior a 8GB ligado atrás do Servidor em tempo integral)." + Environment.NewLine + Environment.NewLine +
                    "Os backups são salvos no formato Dia/Mês/Ano/Hora/Minuto e compactado (zipado) automáticamente." + Environment.NewLine + Environment.NewLine + "Você pode programar esse aplicativo para fazer backups regulares de modo" +
                    " automático, bastando ativar a opção, informar de quantos em quantos dias, e em qual horário ele deve trabalhar." + Environment.NewLine + Environment.NewLine + "Note que o MsSqlBackup sempre irá 'derrubar' o sistema no momento de backup," +
                    " por alguns segundos, enquanto o backup é feito. Terminais poderão ficar fora do ar temporariamente." + Environment.NewLine + Environment.NewLine + "Esse aplicativo também permite selecionar um backup anterior, e restaurar o mesmo. " +
                    "Essa ferramenta deve rodar APENAS no Servidor de Sistema (Banco de Dados)." + Environment.NewLine + Environment.NewLine + "Você pode ativar a opção para que o Syncserver apague backups mais antigos automaticamente. Dessa maneira ele irá" +
                    " gerenciar automaticamente o espaço do Pendrive/HDExterno, apagando os backups mais antigos e impedindo que o dispositivo fique cheio." + Environment.NewLine + Environment.NewLine + "Nota: Se sua empresa fecha as 18h, você pode programar a" +
                    " opção 'Desligar ao concluir' para que o sistema faça o backup as 18:15 (já com a empresa fechada) e após isso desligue o computador. No outro dia - basta ligar e usar.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Evento Tick do Backup
        private void tmrBackup_Tick(object sender, EventArgs e)
        {
            if (chkAtivarBackupAutomatico.Checked)
            {
                string horarioAgora = DateTime.Now.ToString("HH:mm");
                if (tbxHorarioBackup.Text == horarioAgora)
                {
                    //preciso verificar SE o backup não tem intervalo de dias, se houver, verificar se já devo fazer
                    bool fazerBackup = true;
                    if(nudBackupDeQuantosEmQuantosDias.Value > 1)
                    {
                        DataSet dsDados = new DataSet();
                        dsDados.ReadXml(Directory.GetCurrentDirectory() + "\\backups.xml");
                        if (dsDados.Tables[0].Rows.Count != 0)
                        {
                            string tamanhoUltimoBackup = dsDados.Tables[0].Rows[dsDados.Tables[0].Rows.Count-1]["tamanhoBackup"].ToString();
                            string dataUltimoBackup = dsDados.Tables[0].Rows[dsDados.Tables[0].Rows.Count-1]["dataBackup"].ToString();
                            int numeroDiasProximoBackup = Convert.ToInt32(nudBackupDeQuantosEmQuantosDias.Value);

                            DateTime dataProximoBackup = Convert.ToDateTime(dataUltimoBackup);
                            dataProximoBackup = dataProximoBackup.AddDays(numeroDiasProximoBackup);

                            if(dataProximoBackup.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                fazerBackup = false;
                            }
                        }
                    }

                    if (fazerBackup)
                    {
                        lblProgressoBackup.Text = "Deu a hora do backup automático! Estamos iniciando seu backup...";
                        if(chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "It's time to backup automatic! We are starting...";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 0;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(2000);
                        DriversRemoviveisDsponiveis();
                        efetuaBackupDoSistema();
                    }//fim if fazerBackup
                }//fim tbxHorarioBackup
            }//fim chlAtivarAutomatico
        }
        #endregion

        #endregion

        #region Ao Fechar Form eu Salvo o Arquivo de Configurações Automáticamente
        private void SyncServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            recriaAtualizaArquivoConfiguracao();
        }
        #endregion

        #region Evento do Botão Restaurar Backup
        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string caminhoBackupSelecionado = lvwBackupsLocais.SelectedItems[0].SubItems[1].Text.ToString();
                string data = lvwBackupsLocais.SelectedItems[0].SubItems[6].Text.ToString();
                string tamanho = lvwBackupsLocais.SelectedItems[0].SubItems[7].Text.ToString();

                string disponibilidade = lvwBackupsLocais.SelectedItems[0].SubItems[2].Text.ToString();
                if(disponibilidade == "Removido!")
                {
                    if (chkTranslateToENG.Checked)
                    {
                        MessageBox.Show(null, "The Backup is not on the selected folder!", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(null, "O Backup selecionado não está disponível no diretório!", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                bool continua = false;
                if (chkTranslateToENG.Checked)
                {
                    if (MessageBox.Show(null, "Você solicitou a Restauração de um Backup do Banco de Dados. O banco atual será SUBSTITUIDO pelo backup selecionado: " + Environment.NewLine + Environment.NewLine + "Arquivo: " + caminhoBackupSelecionado + Environment.NewLine + "Data: " + data + Environment.NewLine + "Tamanho: " + tamanho + Environment.NewLine + Environment.NewLine + "Faça um Backup do banco ATUAL antes de restaurar o backup. Informações poderão ser perdidas pois o sistema voltará os dados para a Data Selecionada acima. Deseja mesmo continuar?", "MsSqlBackup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        continua = true;
                    }//fim if MessageBox
                }
                else
                {
                    if (MessageBox.Show(null, "You have asked to restore a backup. The Database will be overwrited by the Selected Backup: " + Environment.NewLine + Environment.NewLine + "File: " + caminhoBackupSelecionado + Environment.NewLine + "Date: " + data + Environment.NewLine + "Size: " + tamanho + Environment.NewLine + Environment.NewLine + "Do a Backup of the actual DataBase before restore an older. Information may be losted and the system will return to the Data Selected. Are you sure you want to continue?", "MsSqlBackup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        continua = true;
                    }//fim if MessageBox
                }

                if (continua)
                {
                    #region Passo 1: Recupero o caminho do Banco de Dados lá no SQL Server (banco que irei substituir)

                    DataSet dsDadosXML = new DataSet();
                    string caminho = Directory.GetCurrentDirectory().ToString() + @"\conexao.xml";
                    string servico = dsDadosXML.Tables[0].Rows[0]["SERVICOSQL"].ToString();
                    string baseDados = dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString();

                    int statusServicoSQL = 1;
                    string caminhoBancoDadosMdf = retornaCaminhoBaseDeDados();
                    string caminhoBancoDadosLdf = caminhoBancoDadosMdf.Replace(".mdf", "_log.ldf");
                    string caminhoDiretorioDestino = tbxCaminhoBackupLocal.Text;
                    string diretorioSqlServerPraDarPermissao = retornaCaminhoBaseDeDados().Replace("\\" + baseDados + ".mdf", "");
                    

                    //Libero o diretorio do SQL Server e dou acesso geral pra todo mundo...
                    DirectoryInfo dInfo = new DirectoryInfo(diretorioSqlServerPraDarPermissao);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(
                        new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                        FileSystemRights.FullControl,
                        InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                        PropagationFlags.NoPropagateInherit,
                        AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);

                    string caminhoBancoDadosMdfBackupDescompactado = "";
                    string caminhoBancoDadosLdfBackupDescompactado = "";

                    lblProgressoBackup.Text = "Iniciando Restauração do Backup, por favor aguarde...";
                    if (chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Starting Backup Restore, please wait...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 2;
                    prgAndamentoBackup.Refresh();
                    Thread.Sleep(2000);
                    #endregion

                    #region Passo 2: Paro o Serviço do SQL Server
                    lblProgressoBackup.Text = "Parando o Servidor de Banco de dados para retorno do Backup... aguarde...";

                    if (chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Stoping the DataBase Server to Restore Backup, please wait...";
                    }

                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 4;
                    prgAndamentoBackup.Refresh();


                    ServiceController[] services = ServiceController.GetServices();
                    foreach (ServiceController service in services)
                    {
                        if (service.DisplayName == servico)
                        {
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            if (statusServicoSQL == 1) //rodando
                            {
                                FilaProcessosWindows.PararServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                            }
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);

                            if (statusServicoSQL == 1) //1 é sinal que ainda está rodando, ai eu vou tentar PARAR ele de novo, vou fazer uma segunda tentativa
                            {
                                FilaProcessosWindows.PararServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                                statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            }//sim status ==1                    
                        }//fim if servico.Dislayname
                    }//fim foreach
                    #endregion

                    #region Passo 3: Descompacto o Arquivo ZIP do Backup
                    lblProgressoBackup.Text = "Descompactando o ZIP do backup e preparando para substituir...";
                    if(chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Unpacking the zip file and preparing to replace...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 6;
                    prgAndamentoBackup.Refresh();
                    Thread.Sleep(2000);

                    ZipFile zip = ZipFile.Read(caminhoBackupSelecionado);
                    //Directory.CreateDirectory(caminhoDiretorioDestino);
                    foreach (ZipEntry entry in zip)
                    {
                        // check if you want to extract e or not
                        if (entry.FileName.Contains(".mdf"))
                        {
                            caminhoBancoDadosMdfBackupDescompactado = caminhoDiretorioDestino + "\\" + entry.FileName;
                        }
                        if (entry.FileName.Contains(".ldf"))
                        {
                            caminhoBancoDadosLdfBackupDescompactado = caminhoDiretorioDestino + "\\" + entry.FileName;
                        }
                        entry.Extract(caminhoDiretorioDestino, ExtractExistingFileAction.OverwriteSilently);
                    }
                    #endregion

                    #region Passo 4: Movo o Arquivo pra Pasta do SQL Server e substituo os arquivos anteriores
                    if (File.Exists(caminhoBancoDadosLdfBackupDescompactado) && File.Exists(caminhoBancoDadosMdfBackupDescompactado))
                    {
                        File.Delete(caminhoBancoDadosLdf);
                        File.Delete(caminhoBancoDadosMdf);
                        Thread.Sleep(1000);
                        File.Move(caminhoBancoDadosLdfBackupDescompactado, caminhoBancoDadosLdf);
                        File.Move(caminhoBancoDadosMdfBackupDescompactado, caminhoBancoDadosMdf);
                    }
                    #endregion

                    #region Passo 5: Subo novamente o SQL Server e finalizo o processo
                    lblProgressoBackup.Text = "Pronto, aguarde que estamos subindo o sistema de banco de dados novamente...";
                    if (chkTranslateToENG.Checked)
                    {
                        lblProgressoBackup.Text = "Well done, wait while we Start the DataBase again...";
                    }
                    lblProgressoBackup.Refresh();
                    prgAndamentoBackup.Value = 7;
                    prgAndamentoBackup.Refresh();
                    Thread.Sleep(2000);

                    //Libero o diretorio do SQL Server e dou acesso geral pra todo mundo...                    
                    dSecurity.AddAccessRule(new FileSystemAccessRule(
                        new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                        FileSystemRights.FullControl,
                        InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                        PropagationFlags.NoPropagateInherit,
                        AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);

                    #region Subo de Novo o Serviço do SQL Server e Libero o Uso do Sistema
                    statusServicoSQL = 0;
                    foreach (ServiceController service in services)
                    {
                        if (service.DisplayName == servico)
                        {
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            if (statusServicoSQL != 1) //se for diferente de 1 ainda não está rodando
                            {
                                FilaProcessosWindows.IniciarServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                            }
                            statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName);

                            if (statusServicoSQL != 1) //se for diferente de 1 ainda não está rodando
                            {
                                FilaProcessosWindows.IniciarServico(service.DisplayName, 10000);
                                Thread.Sleep(6000); //paro e espero o serviço parar...
                                statusServicoSQL = FilaProcessosWindows.RetornaStatusServico(service.DisplayName, 10000);
                            }//sim status ==1       

                            if (statusServicoSQL != 1)
                            {
                                if (chkTranslateToENG.Checked)
                                {
                                    MessageBox.Show(null, "The MsSqlServer can't start database service again. Maybe you'll need to restart this server.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBox.Show(null, "O MsSqlServer não conseguiu subir novamente o Serviço do Banco de Dados. Talvez seja necessário reiniciar esse Servidor para poder usar o sistema.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }

                        }//fim if servico.Dislayname
                    }//fim foreach

                    if (statusServicoSQL == 1)
                    {
                        lblProgressoBackup.Text = "Pronto! Sistema Online e liberado para uso, backup restaurado com sucesso!";
                        if (chkTranslateToENG.Checked)
                        {
                            lblProgressoBackup.Text = "AlReady! Online and ready for use, backup restore with success!";
                        }
                        lblProgressoBackup.Refresh();
                        prgAndamentoBackup.Value = 8;
                        prgAndamentoBackup.Refresh();
                        Thread.Sleep(3000);
                    }
                    #endregion


                    #endregion
                }
            }
            catch(Exception erro)
            {
                if (chkTranslateToENG.Checked)
                {
                    MessageBox.Show(null, "Selecione um Backup na Lista abaixo para Restauração.", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(null, "Please select one Backup in List Below for Restoration.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #endregion

        #region Aba de Gerenciamento de Banco de Dados

        #region Efetua Liberação das Portas de Firewall
        private void btnLiberarFirewall_Click(object sender, EventArgs e)
        {
            //WindowsFirewallHelper
            //A class library to manage the Windows Firewall as well as 
            //        adding your program to the Windows Firewall Exception list

            //=========================================================================

            //USAGE
            // Get an instance of the active firewall  using FirewallManager class and
            //  use the properties to get the list of firewall rules and profiles. 
            //  You can also  use the methods  on this class  to add a new rule  to the 
            //  firewall.

            //CODE SAMPLE FOR ADDING AN APPLICATION RULE TO THE ACTIVE PROFILE:
            //  var rule = FirewallManager.Instance.CreateApplicationRule(
            //        FirewallManager.Instance.GetProfile().Type, @"MyApp Rule", 
            //        FirewallAction.Allow, @"C:\MyApp.exe");
            //  rule.Direction = FirewallDirection.Outbound;
            //  FirewallManager.Instance.Rules.Add(rule);

            //CODE SAMPLE FOR ADDING A PORT RULE TO THE ACTIVE PROFILE:
            //  var rule = FirewallManager.Instance.CreatePortRule(
            //      FirewallManager.Instance.GetProfile().Type,
            //      @"Port 80 - Any Protocol", FirewallAction.Allow, 80, 
            //      FirewallProtocol.Any);
            //  FirewallManager.Instance.Rules.Add(rule);

            //CODE SAMPLE TO GET A LIST OF ALL REGISTERED RULES:
            //  var allRules = FirewallManager.Instance.Rules.ToArray();

            //MORE SAMPLES:
            //  Check the Project's Github page at: 
            //              https://github.com/falahati/WindowsFirewallHelper

            string ArquivosProgramas = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string ArquivosProgramasX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            
            var ruleFuturaDataSQLServer = FirewallManager.Instance.CreateApplicationRule(
                  FirewallManager.Instance.GetProfile().Type, @"MsSqlBackup SQL Server",
                  FirewallAction.Allow, ArquivosProgramas + @"\Microsoft SQL Server\MSSQL12.FUTURADATA2014\MSSQL\Binn\sqlservr.exe"); //você precisa colocar a pasta do SQL instalado na sua máquina aqui
            ruleFuturaDataSQLServer.Direction = FirewallDirection.Outbound;
            FirewallManager.Instance.Rules.Add(ruleFuturaDataSQLServer);

            var ruleFuturaDataSQLBrowser = FirewallManager.Instance.CreateApplicationRule(
                  FirewallManager.Instance.GetProfile().Type, @"MsSqlBackup SQL Server",
                  FirewallAction.Allow, ArquivosProgramasX86 + @"\Microsoft SQL Server\90\Shared\sqlbrowser.exe");
            ruleFuturaDataSQLBrowser.Direction = FirewallDirection.Outbound;
            FirewallManager.Instance.Rules.Add(ruleFuturaDataSQLBrowser);

            var ruleFuturaDataSQLServerIn = FirewallManager.Instance.CreateApplicationRule(
                  FirewallManager.Instance.GetProfile().Type, @"MsSqlBackup SQL Server",
                  FirewallAction.Allow, ArquivosProgramas + @"\Microsoft SQL Server\MSSQL12.FUTURADATA2014\MSSQL\Binn\sqlservr.exe"); //you should check the folder o SQL and put it here
            ruleFuturaDataSQLServerIn.Direction = FirewallDirection.Inbound;
            FirewallManager.Instance.Rules.Add(ruleFuturaDataSQLServerIn);

            var ruleFuturaDataSQLBrowserIn = FirewallManager.Instance.CreateApplicationRule(
                  FirewallManager.Instance.GetProfile().Type, @"MsSqlBackup SQL Server",
                  FirewallAction.Allow, ArquivosProgramasX86 + @"\Microsoft SQL Server\90\Shared\sqlbrowser.exe");
            ruleFuturaDataSQLBrowserIn.Direction = FirewallDirection.Inbound;
            FirewallManager.Instance.Rules.Add(ruleFuturaDataSQLBrowserIn);

            var rule = FirewallManager.Instance.CreatePortRule(
                  FirewallManager.Instance.GetProfile().Type,
                  @"MsSqlBackup Porta SQL", FirewallAction.Allow, 1433, 
                  FirewallProtocol.TCP);
              FirewallManager.Instance.Rules.Add(rule);

            if (chkTranslateToENG.Checked)
            {
                MessageBox.Show(null, "Ports Opened and Windows Firewall configured. You need to restart this server to efect.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Portas Liberadas e Firewall do Windows configurado. Você precisa reiniciar esse Servidor para que as configurações entrem em vigor.", "MsSqlServer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Executor do SQL
        private void btnExecutarSQL_Click(object sender, EventArgs e)
        {            
            DataTable retorno = executaOperacaoSQL(tbxComandoSqlExecutar.Text);
            if (retorno.Rows.Count != 0)
            {
                dgwRetornoSQL.DataSource = retorno;
                dgwRetornoSQL.Refresh();
            }            
        }
        #endregion

        #endregion

        #region Evento da CheckBox de Tradução
        private void chkTranslateToENG_CheckedChanged(object sender, EventArgs e)
        {
            if (formLoaded)
            {
                recriaAtualizaArquivoConfiguracao();
                MessageBox.Show(null, "Reinicie o programa / Restart App", "MsSqlBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Evento do Notifier
        private void MsSqlBackup_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notNotificador.Visible = true;
                if (chkTranslateToENG.Checked)
                {
                    notNotificador.Text = "MsSqlBackup is working in background.";
                    notNotificador.BalloonTipText = "MsSqlBackup is working in background.";
                }
                else
                {
                    notNotificador.Text = "MsSqlBackup está rodando no background.";
                    notNotificador.BalloonTipText = "MsSqlBackup está rodando no background.";
                }
                notNotificador.ShowBalloonTip(2000);
            }
        }
        
        private void notNotificador_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notNotificador.Visible = false;
        }
        #endregion
    }//fim classe
}//fim namespace