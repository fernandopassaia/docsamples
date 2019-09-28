using System;
using System.Data;
using System.Windows.Forms;
using DllFuturaDataTCC;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataTCC.Utilitarios;
using DllFuturaDataContrValidacoes;
using DllFuturaDataCriptografia;
using FuturaDataTCC.Utilitarios;
using System.Threading;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;

namespace FuturaDataTCC.Iniciar
{
    public partial class frmInicializacao : Form
    {
        #region Inicializador do Form e Variaveis principais
        
        public frmInicializacao frmInicial; //cria uma variavel dessa mesma classe,
        //usado para passar ela mesmo como parametro para outra classe (form)

        //recebe as informações referentes ao cliente que está usando o sistema
        public DataTable Dt_DadosCliente = null;

        //recebe as informações de configurações do Usuario
        public DataTable Dt_ConfigUsuario = null;

        //carrega as Mensagens de Inicialização do Sistema
        public DataTable Dt_MensagensInicializacao = null;

        //DATATABLE SERÁ USADO PRA ACHAR O NOME DO USUÁRIO DURANTE O CARREGAMENTO DE LOGS DO SISTEMA
        public DataTable dt_TodosOsUsuarios = new DataTable();

        public string nomeHost = "Ainda não identificado, inicializando o sistema...";

        public bool primeiroUsoSistema = false;
        public string cnpjEmpresa = "";
        public string nomeEmpresa = "";

        public string loginUsuarioLogado = "";
        public string nomeUsuarioLogado = "";
        public int numeroUsuarioLogado = 1;
        public string usuarioPerfilLogado = "";
        public bool usuarioEfetuouLogon = false;
        public frmInicializacao()
        {
           
            //todo código ficará no inicializador para abrir junto com o form
            InitializeComponent();
            
            this.Show();
            this.Focus();
            this.Refresh();
            this.PerformLayout();
            
            //inicializa variaveis globais            
            btnIniciarSistema.Focus();            
        }
        #endregion

        #region Carrega Sistema
        public void carregarSistema()
        {
            clsInicializacao iniciarSistema = new clsInicializacao();            
            string razaoSocial = "";
            string cnpj = "";

            //carrega a inicialização do sistema...
            if (iniciarSistema.verificaComunicacaoBanco() == true) //passo 1 - verifica se há conexão com o database
            {
                prgProgressoInicialização.Value = 30;
                prgProgressoInicialização.Refresh();
                tbxMensagens.Text = "Conexão Estabelecida com o banco! Obtendo informações e Verificando Chave/Licença Software...";
                tbxMensagens.Refresh();

                ptbVerifComunServ.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                ptbVerifComunServ.Refresh();

                //passo 2 - retorna dados da empresa
                bool retorno = iniciarSistema.retornaDadosCliente(1, 0, true, "", nomeHost);                
                if (retorno == true)
                {
                    Dt_DadosCliente = iniciarSistema.getDs_DadosRetorno().Tables[0];
                    razaoSocial = Dt_DadosCliente.Rows[0]["RAZAOSOCIAL"].ToString().Trim();
                    cnpj = Dt_DadosCliente.Rows[0]["CNPJ"].ToString().Trim();

                    ptbCarregandoInfoCliente.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                    ptbCarregandoInfoCliente.Refresh();
                    prgProgressoInicialização.Value = 50;
                    prgProgressoInicialização.Refresh();
                    //Thread.Sleep(1000);
                }

                //passo 3 Efetua o Login do Usuário
                prgProgressoInicialização.Value = 60;
                tbxMensagens.Text = "Aguardando Identificação e Login do Usuario... Comunicação com Servidor e Autenticação OK!";
                tbxMensagens.Refresh();
                ptbIniciandoIdentUsuario.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                ptbIniciandoIdentUsuario.Refresh();                
                prgProgressoInicialização.Value = 70;
                prgProgressoInicialização.Refresh();

                frmLoginSistema login = new frmLoginSistema(this);
                login.ShowDialog();
                
                //verifica se o usuario está logado, se não estiver, fecha a aplicação
                if (usuarioEfetuouLogon == false) // quer dizer que usuário não se autenticou - fecha aplicação
                {
                    Application.Exit();
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    ptbEfetuandoLogon.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                    ptbEfetuandoLogon.Refresh();
                    
                    if (usuarioEfetuouLogon)
                    {   
                        //captura as informações do usuário, como nome, numero e perfil
                        iConUsuario controlUsuario = new iConUsuario();
                        iModUsuario[] usuarios = controlUsuario.cObterUsuario();
                        foreach(iModUsuario usu in usuarios)
                        {
                            if(usu.LoginUsuario == loginUsuarioLogado)
                            {
                                numeroUsuarioLogado = usu.Pk_Codigo;
                                usuarioPerfilLogado = usu.Funcao;
                                nomeUsuarioLogado = usu.NomeUsuario;
                            }
                        }

                        ptbRetornaConfigUsu.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                        ptbRetornaConfigUsu.Refresh();                     
                        ptbLogonOKAbrTelaPrinc.Image = FuturaDataTCC.Properties.Resources.btnStatusOKMin;
                        ptbLogonOKAbrTelaPrinc.Refresh();
                    }

                    prgProgressoInicialização.Value = 100;
                    prgProgressoInicialização.Refresh();                    
                    frmTelaPrincipal telaPrincipal = new frmTelaPrincipal(this);
                    telaPrincipal.minhaInstacia = telaPrincipal;
                    telaPrincipal.Show();
                }
            }//fim do if verificaComunicacaoBanco
            else
            {
                //quando não acha comunicação com o banco de dados
                MessageBox.Show("Houve uma falha no Login ao Banco de Dados. Verifique sua conexão!", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmConfigConexaoServidor config = new frmConfigConexaoServidor();
                config.ShowDialog();
            }            
        }//FIM CARREGAR SISTEMA
        #endregion

        #region Botao Carrega Sistema
        private void btnIniciarSistema_Click(object sender, EventArgs e)
        {
            carregarSistema();
        }
        #endregion

        #region Label que abre tela de config
        private void lblAbrirTelaConfig_Click(object sender, EventArgs e)
        {
            frmConfigSistema config = new frmConfigSistema(this.frmInicial);
            config.ShowDialog();
        }
        #endregion

        #region Evento do Botao Alterar Configuracao do Sistema
        private void btnAlterarConfigSist_Click(object sender, EventArgs e)
        {
            frmConfigSistema config = new frmConfigSistema(this.frmInicial);
            config.ShowDialog();
        }
        #endregion

        #region Botao Sair do Sistema
        private void button1_Click(object sender, EventArgs e)
        {
            clsFilaProcessosWindows.finalizarPrograma();
        }
        #endregion
    }//fim classe
}//fim namespace