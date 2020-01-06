#region Using......

using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using DllFuturaDataContrValidacoes;
using DllFuturaDataCriptografia;
using DllFuturaDataTCC;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataTCC.Utilitarios;
using FuturaDataTCC.Iniciar;
using FuturaDataTCC.Utilitarios;
using Microsoft.Win32;
using System.Threading;
using DllFuturaDataUtil.FTP;
using System.Collections.Generic;
using FuturaDataTCC.Views.Gestoes;
using FuturaDataTCC.Views.Orcamento;
using FuturaDataTCC.Views.Caixa;
using FuturaDataTCC.Views.PlanoDeContas;

#endregion Using......

namespace FuturaDataTCC
{
    public partial class frmTelaPrincipal : Form
    {
        #region Variaveis de Inicialização do Form
        frmInicializacao frmInicial;
        //public frmCarregando frmCarregando = new frmCarregando();
        public frmTelaPrincipal minhaInstacia;
        
        const int WM_CLOSE = 16;
        Byte[] imagemEmBytes;
        /// <summary>
        /// Variaveis com informações do LOGON para serem usados em outras classes
        /// </summary>
        public string nomeEmpresa;
        public string cnpjempresa;
        public string chave;
        public string nomeUsuario;
        public string horaLogon = DateTime.Now.ToString();
        public string tipoMaquina = null;
        public DateTime dataUltimoBackup;
        //public int nivelUsuario = 0;
        bool jaApareceuIconeNotificacaoMovePainel = false;
        bool sistemaIniciado = false;
        clsInicializacao iniciar = new clsInicializacao();
        DataTable dt_MensagensInicializacao;
        bool primeiroUso = false;

        bool fecharImpressoraAutomaticamente = false;
        bool fecharCaixaAutomaticamente = false;
        DateTime HorarioFechamento;
        bool usuarioJaNotificadoFechamento = false;
        string modeloImpressora;

        private Point ptStartPosition; // the start position of the postit when moving
        private Point ptEndPosition; // the end position of the postit when moving
        bool formCarregado = false;
        #endregion

        #region Construtor do Form
        public frmTelaPrincipal(frmInicializacao formInicial)
        {
            InitializeComponent();
            frmInicial = formInicial;
            frmInicial.Hide();
            trataResolucaoTela();
            mostrarInfoConfig();

            bool retorno = iniciar.retornaMensagensInicialização(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            if (retorno == true)
            {
                dt_MensagensInicializacao = iniciar.getDs_DadosRetorno().Tables[0];
            }

            #region verifica se resolucao nao é menor que 1024 por 768

            int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            int deskWidth = Screen.PrimaryScreen.Bounds.Width;

            if (deskWidth < 820 || deskHeight < 620)
            {
                MessageBox.Show(null, "Resolução Minima de 1024X768. O software necessita de uma resolução maior para que possa exibir todos seus recursos visuais. Não será possível continuar com a resolução atual. Aumente a Resolução e Reinicie o Sistema! O sistema será fechado automáticamente agora!", "FuturaData - Principal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                clsFilaProcessosWindows.finalizarPrograma();
            }

            #endregion verifica se resolucao nao é menor que 1024 por 768

            #region Verifica a Formatacao da Data Está correta (01/01/2008 por exemplo) - se tiver 1/1/2008 já dá o erro)

            clsValidacaoDeStrings validacao = new clsValidacaoDeStrings();
            string data = DateTime.Now.ToShortDateString();
            if (validacao.verificaSeEData(data) == false)
            {
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\International", true);                    
                    key.SetValue("sShortDate", "dd/MM/yyyy");
                    //MessageBox.Show("Chave alterada com sucesso! O sistema fechará automáticamente, basta rodar novamente...", "FuturaDataTCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception erro)
                {
                      Thread.Sleep(3000);
                      MessageBox.Show(null, "O Formato de Data do seu Windows está incompatível com o sistema - no sistema é necessário estar no formado dd/MM/yyyy (ex: 05/05/2011). O sistema tentou alterar as configurações do Windows mas não teve sucesso!" + Environment.NewLine + Environment.NewLine +"Caso você esteja usando Windows 7/Vista, abra o sistema novamente clicando com o botão direito no icone e selecionando 'Executar como Administrador' (run as administrator). Caso o sistema não obtenha êxito, você precisará alterar manualmente as configurações de seu Windows." + Environment.NewLine + Environment.NewLine + "Para corrigir o problema, vá manualmente até 'Painel de Controle > Opcoes Regionais e de Idioma > Personalizar > Data' e configure o 'Formato de Data Abreviada' para dd/MM/aaaa, após isso, reinicie o sistema. O sistema não pode continuar com a configuração atual e será fechado automáticamente agora. Informações sobre como corrigir esse problema estão na página 52 do manual." + Environment.NewLine + Environment.NewLine + "Caso tenha dúvidas, procure o Manual do usuário capítulo 11, onde há dicas e imagens de como resolver esse problema. Mensagem erro compilador:" + Environment.NewLine + Environment.NewLine + erro.Message.ToString(), "FuturaDataTCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      clsFilaProcessosWindows.finalizarPrograma();
                }
            }

            #endregion Verifica a Formatacao da Data Está correta (01/01/2008 por exemplo) - se tiver 1/1/2008 já dá o erro)

            sistemaIniciado = true;

            //string mensagemLabelFormPrincipal = this.Text + " " + nomeEmpresa + " " + cnpjempresa.Replace(",", ".") + ".";

            frmInicial.nomeEmpresa = nomeEmpresa;
            frmInicial.cnpjEmpresa = cnpjempresa;
            
            //this.Text = mensagemLabelFormPrincipal;
            //string cnpjEmpresaSemPont = cnpjempresa.Replace(".", "").Replace("-", "").Replace(",", "").Replace("-", "").Replace("/", "");

            formCarregado = true;           
            pctMarcaDaguaLogoFuturaData.Parent = pnlPapelParede;
        }

        #endregion Construtor e Variaveis Inicializacao

        #region ********** Métodos **********

        #region Método Trata Redimensionamento de Tela (Tratamento)

        /// <summary>
        /// Se houver mudança na resolução de tela (maximização, redimensionamento da tela) ele trata
        /// os campos, textbox, gruop e etc, para que seja redimensionado junto com o FORM
        /// </summary>
        private void trataResolucaoTela()
        {
            //string horizontal = Screen.PrimaryScreen.WorkingArea.Width.ToString();
            //string vertical = Screen.PrimaryScreen.WorkingArea.Height.ToString();

            string horizontal = this.Width.ToString();
            string vertical = this.Height.ToString();

            pnlPainelSuperior.Width = Convert.ToInt32(horizontal) - 10;
            pnlPainelSuperior.Refresh();

            //            pnlPainelEsquerda.Height = Convert.ToInt32(vertical) - 152;
            //          pnlPainelEsquerda.Refresh();

            pnlPapelParede.Width = Convert.ToInt32(horizontal) - 20;
            pnlPapelParede.Height = Convert.ToInt32(vertical) - 185;
            tbpControlesDesktop.Width = Convert.ToInt32(horizontal) - 22;
            tbpControlesDesktop.Height = Convert.ToInt32(vertical) - 160;
            pnlPapelParede.Refresh();
        }

        #endregion Método Trata Redimensionamento de Tela (Tratamento)

        #region Método Mostrar Informações de Configurações na Tela e Carrega Configuracoes Usuario

        public void mostrarInfoConfig()
        {            
            carregaPapelParede();
        }

        #endregion Método Mostrar Informações de Configurações na Tela e Carrega Configuracoes Usuario

        #region Método Carrega o Papel de Parede do Usuario e Evento TICK do Relógio que troca ele de 15 em 15 minutos

        public void carregaPapelParede()
        {
            string testeImagem = "";
            try
            {
                testeImagem = frmInicial.Dt_ConfigUsuario.Rows[0]["IMAGEM_BINARIO"].ToString();
            }
            catch
            {

            }
            if (testeImagem != "") //carrega a imagem
            {
                Byte[] imagemCarregada = new Byte[0];
                imagemCarregada = (Byte[])(frmInicial.Dt_ConfigUsuario.Rows[0]["IMAGEM_BINARIO"]);
                MemoryStream streamMemory = new MemoryStream(imagemCarregada);
                pnlPapelParede.Image = Image.FromStream(streamMemory);
                pnlPapelParede.Refresh();
                imagemEmBytes = imagemCarregada; //transfere para o objeto que será usado no caso de uma inserção ou atualização
                testeImagem = null;
            }

            else
            {
                string usarRandom = "TRUE";

                //recebe o numero do Papel de Parede nas configurações do Usuario
                int numeroPapelParede = 1; //Convert.ToInt32(frmInicial.Dt_ConfigUsuario.Rows[0]["NUMEROPAPELPAREDE"].ToString().Trim());

                if (usarRandom.ToUpper() == "TRUE" || usarRandom == "1")
                {
                    Random rnd = new Random();
                    numeroPapelParede = rnd.Next(1, 66);
                }


                //seta o background para esse papel de parede
                if (numeroPapelParede == 1)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w1;
                }

                if (numeroPapelParede == 2)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w2;
                }

                if (numeroPapelParede == 3)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w3;
                }

                if (numeroPapelParede == 4)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w4;
                }

                if (numeroPapelParede == 5)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w5;
                }

                if (numeroPapelParede == 6)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w6;
                }

                if (numeroPapelParede == 7)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w7;
                }

                if (numeroPapelParede == 8)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w8;
                }

                if (numeroPapelParede == 9)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w9;
                }

                if (numeroPapelParede == 10)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w10;
                }

                if (numeroPapelParede == 11)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w11;
                }

                if (numeroPapelParede == 12)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w12;
                }

                if (numeroPapelParede == 13)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w13;
                }

                if (numeroPapelParede == 14)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w14;
                }

                if (numeroPapelParede == 15)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w15;
                }

                if (numeroPapelParede == 16)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w16;
                }

                if (numeroPapelParede == 17)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w17;
                }

                if (numeroPapelParede == 18)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w18;
                }

                if (numeroPapelParede == 18)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w19;
                }

                if (numeroPapelParede == 20)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w20;
                }

                if (numeroPapelParede == 21)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w21;
                }

                if (numeroPapelParede == 22)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w22;
                }

                if (numeroPapelParede == 22)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w23;
                }

                if (numeroPapelParede == 24)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w24;
                }

                if (numeroPapelParede == 25)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w25;
                }

                if (numeroPapelParede == 26)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w26;
                }

                if (numeroPapelParede == 27)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w27;
                }

                if (numeroPapelParede == 28)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w28;
                }

                if (numeroPapelParede == 29)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w29;
                }

                if (numeroPapelParede == 30)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w30;
                }

                if (numeroPapelParede == 31)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w31;
                }

                if (numeroPapelParede == 32)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w32;
                }

                if (numeroPapelParede == 33)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w33;
                }

                if (numeroPapelParede == 34)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w34;
                }

                if (numeroPapelParede == 35)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w35;
                }

                if (numeroPapelParede == 36)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w36;
                }

                if (numeroPapelParede == 37)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w37;
                }

                if (numeroPapelParede == 38)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w38;
                }

                if (numeroPapelParede == 39)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w39;
                }

                if (numeroPapelParede == 40)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w40;
                }

                if (numeroPapelParede == 41)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w41;
                }

                if (numeroPapelParede == 42)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w42;
                }

                if (numeroPapelParede == 43)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w43;
                }

                if (numeroPapelParede == 44)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w44;
                }

                if (numeroPapelParede == 45)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w45;
                }

                if (numeroPapelParede == 46)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w46;
                }

                if (numeroPapelParede == 47)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w47;
                }

                if (numeroPapelParede == 48)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w48;
                }

                if (numeroPapelParede == 49)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w49;
                }

                if (numeroPapelParede == 50)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w5;
                }

                if (numeroPapelParede == 51)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w51;
                }

                if (numeroPapelParede == 52)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w52;
                }

                if (numeroPapelParede == 53)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w53;
                }

                if (numeroPapelParede == 54)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w54;
                }

                if (numeroPapelParede == 55)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w55;
                }

                if (numeroPapelParede == 56)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w56;
                }

                if (numeroPapelParede == 57)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w57;
                }

                if (numeroPapelParede == 58)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w58;
                }

                if (numeroPapelParede == 59)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w59;
                }

                if (numeroPapelParede == 60)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w60;
                }

                if (numeroPapelParede == 61)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w61;
                }

                if (numeroPapelParede == 62)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w62;
                }

                if (numeroPapelParede == 63)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w63;
                }

                if (numeroPapelParede == 64)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w64;
                }

                if (numeroPapelParede == 65)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w65;
                }

                if (numeroPapelParede == 66)
                {
                    this.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w66;
                }
            }
        }

        private void tmrPapelParede_Tick(object sender, EventArgs e)
        {
            string usarRandom = frmInicial.Dt_ConfigUsuario.Rows[0]["EXIBIRALEATORIO"].ToString().Trim();

            if (usarRandom.ToUpper() == "TRUE" || usarRandom == "1")
            {
                carregaPapelParede();
            }            
        }
        #endregion Método Carrega o Papel de Parede do Usuario

        #region Métodos que Carregam os forms Clientes, Produtos, Orçamentos e Caixa
        public void abreFormClientes()
        {
            frmGestaoClientes gestao = new frmGestaoClientes(this.frmInicial, this);
            gestao.ShowDialog();
        }

        public void abreFormProdutos()
        {
            frmGestaoProdutos frmPro = new frmGestaoProdutos(frmInicial, this);
            frmPro.ShowDialog();
        }

        public void abreFormCotacoes()
        {
            frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, this, this.nomeEmpresa, "");
            orct.ShowDialog();
            btnVendas.Checked = false;
        }

        public void carregarFormCadUsuarios()
        {
            frmGestaoUsuarios cadUsuarios = new frmGestaoUsuarios();
            cadUsuarios.ShowDialog();
        }
        public void carregarFormOrcamento()
        {
            frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, this, cnpjempresa + " - " + nomeEmpresa, "0");
            orct.ShowDialog();
            btnVendas.Checked = false;
        }

        public void carregaFormProdutos()
        {
            frmGestaoProdutos produtos = new frmGestaoProdutos(this.frmInicial, this);
            produtos.ShowDialog();
            btnProdutos.Checked = false;
        }

        public void carregaGestaoUsuario()
        {
            frmGestaoUsuarios gestao = new frmGestaoUsuarios();
            gestao.ShowDialog();
        }

        public void carregaPapeisParede()
        {
            frmPapelParede papel = new frmPapelParede(this.frmInicial, this);
            papel.ShowDialog();
        }
        #endregion Método Carrega Form Cadastro de Clientes
        
        #region Método Carrega Form Papel De Parede

        public void carregarFormPapelParede()
        {
            frmPapelParede papel = new frmPapelParede(this.frmInicial, this);
            papel.ShowDialog();
        }

        #endregion Método Carrega Form Papel De Parede

        #region Método Sair do Sistema

        public void fecharSistema()
        {
            if (MessageBox.Show(null, "Você tem certeza que deseja sair do Sistema?", "FuturaData - Principal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion Método Sair do Sistema

        #endregion ********** Métodos **********

        #region ********** Eventos **********

        #region Controla o Evento OnClose do Form se tentar desligar o Windows com o Sist Aberto

        private void frmTelaPrincipal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Environment.HasShutdownStarted)
            {
                e.Cancel = true;
                this.Hide();
                MessageBox.Show(null, "Por favor finalize o sistema FuturaData antes de desligar ou reiniciar o computador!", "FuturaData - Principal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion Controla o Evento OnClose do Form se tentar desligar o Windows com o Sist Aberto

        #region Captura o Evento Fechar da Aplicação no Windows e Fecha a Aplicação

        /// <summary>
        /// Captura o Evento de Fechamento do Form no Windows para finalizar a aplicação
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (WM_CLOSE == m.Msg)
            {
                Application.Exit();
            }
        }

        #endregion Captura o Evento Fechar da Aplicação no Windows e Fecha a Aplicação

        #region Evento Captura o Evento de Maximizacao do FORM

        private void frmTelaPrincipal_SizeChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                trataResolucaoTela();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                trataResolucaoTela();
            }
        }

        #endregion Evento Captura o Evento de Maximizacao do FORM
        
        #region Evento Click Botão Cadastro de Clientes !  ****

        private void btnCadClientes_Click(object sender, EventArgs e)
        {
            abreFormClientes();
        }

        #endregion Evento Click Botão Cadastro de Clientes !  ****
        
        #region Evento Click Botão Papel de Parede

        private void btnPapelParede_Click(object sender, EventArgs e)
        {
            this.carregarFormPapelParede();
        }

        #endregion Evento Click Botão Papel de Parede

        #region Evento Click Botão Orçamentos
        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            this.carregarFormOrcamento();
        }
        #endregion Evento Click Botão Orçamentos ( Evento Antigo )
        
        #region Evento Click Botão Caixa

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            frmNewCaixa caixa = new frmNewCaixa(this.frmInicial, this);
            caixa.ShowDialog();
            btnCaixa.Checked = false;
        }

        #endregion Evento Click Botão Caixa

        #region Evento Click Cadastro de Usuarios

        private void cadastroDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.carregarFormCadUsuarios();
        }

        #endregion Evento Click Cadastro de Usuaráios
        
        #region Evento Botao Configuração do Sistema

        private void configuraçãoDoSistemaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConfigSistema config = new frmConfigSistema(this.frmInicial);
            config.ShowDialog();
        }

        #endregion Evento Botao Configuração do Sistema
        
        #region Alguns atalhos da ToolStrip
        private void clientesToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {            
            frmGestaoClientes clientes = new frmGestaoClientes(this.frmInicial, this);
            clientes.ShowDialog();         
        }

        private void cotaçãoVendaConToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, this, this.nomeEmpresa, "");
            orct.ShowDialog();
        }
        
        #endregion Alguns atalhos da ToolStrip

        #region Eventos que Abrem os Forms CLIENTES, PRODUTOS, PEDIDOS, VENDAS, PAPEL PAREDE
        private void btnClientes_Click(object sender, EventArgs e)
        {        
            frmGestaoClientes clientes = new frmGestaoClientes(this.frmInicial, this);
            clientes.ShowDialog();
            btnClientes.Checked = false;
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            frmGestaoProdutos produtos = new frmGestaoProdutos(this.frmInicial, this);
            produtos.ShowDialog();
            btnProdutos.Checked = false;
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, this, this.nomeEmpresa, "");
            orct.ShowDialog();
            btnVendas.Checked = false;
        }

        private void btnPapelParede_Click_1(object sender, EventArgs e)
        {
            this.carregarFormPapelParede();
            btnPapelParede.Checked = false;
        }
        
        private void btnSairSistema_Click(object sender, EventArgs e)
        {
           this.fecharSistema();
        }

        private void frmTelaPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.fecharSistema();
        }
        #endregion

        #region Novos Atalhos da Barra de Atalhos do Sistema
        private void clientesToolStripMenuItem2_Click_2(object sender, EventArgs e)
        {
            frmGestaoClientes clientes = new frmGestaoClientes(this.frmInicial, this);
            clientes.ShowDialog();

            btnClientes.Checked = false;
        }
        #endregion

        #region Evento Click que Abre o Form de Usuários

        private void gestãoUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            carregaGestaoUsuario();
        }

        #endregion

        #region Métodos do ECF
        private void cancelaUltimoCupomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsECF ecf = new clsECF();
            ecf.cancelaUltimoCupom();
        }        

        private void leituraXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsECF ecf = new clsECF();
            ecf.leituraX();
        }
        
        private void reducaoZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsECF ecf = new clsECF();
            ecf.reducaoZ();
        }        

        private void gravarAliquotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsECF ecf = new clsECF();
            ecf.reducaoZ();
        }
        #endregion

        #region Evento que Abre o Plano de Contas
        private void formasDePagamentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmGestaoPlanoContas planCon = new frmGestaoPlanoContas(this.frmInicial);
            planCon.ShowDialog();
        }
        #endregion
        #endregion
    }//fim namespace
}//fim classe