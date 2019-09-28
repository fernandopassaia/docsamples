using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DllFuturaDataTCC.Gestoes;
using FuturaDataTCC.Utilitarios;
using DllFuturaDataContrValidacoes;
using DllFuturaDataCriptografia;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;

namespace FuturaDataTCC.Iniciar
{
    public partial class frmLoginSistema : Form
    {
        const int WM_CLOSE = 16; //variavel para captar o fechamento do FORM de login e fechar a aplicação

        #region Inicializacao do Form
        //cria uma variavel interna do tipo clsForm para controlar o form
        frmInicializacao frmInicial;

        //recebe como parametro a Tela Principal do Sistema
        public frmLoginSistema(frmInicializacao formInicial)
        {
            InitializeComponent();
            frmInicial = formInicial; //associa variavel inerna ao parametro que esta receb. no constr
            tbxLoginUsuario.Focus();

            DataSet dsDadosXML = new DataSet();
            clsCriptografia crip = new clsCriptografia();
            dsDadosXML.ReadXml(@"c:\FuturaData\TCC\conexao.xml");
            string servidor1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString());
            string servico1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVICO"].ToString());
            string baseDados1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString());
            
            lblBancoEmpresa1.Text = lblBancoEmpresa1.Text + " " + baseDados1;
            lblInstanciaBanco1.Text = lblInstanciaBanco1.Text + " " + servico1;
            lblServidorEmpresa1.Text = lblServidorEmpresa1.Text + " " + servidor1;
            lblStatusEmpresa1.Text = "Servidor e Banco de Dados Ativos!";
        }
        #endregion

        #region botao entrar
        private void btnentrar_Click(object sender, EventArgs e)
        {
            iConUsuario PassarParametros = new iConUsuario();

            PassarParametros.modUsuario.LoginUsuario = tbxLoginUsuario.Text.ToString().Trim();
            PassarParametros.modUsuario.Senha = tbxSenhaUsuario.Text.ToString().Trim();

            //recebe o inteiro que retorna dessa função com o nível do usuario
            //se retornar 0, é por que não achou usuario algum
            //se retornar de 1 a 8, é o nível de acesso do usuario no sistema
            bool Login = PassarParametros.cEfetuarLogon();

            if (Login == true) //se login for diferente de zero, quer dizer que usuario deu logon
            {
                frmInicial.usuarioEfetuouLogon = true;
                frmInicial.loginUsuarioLogado = tbxLoginUsuario.Text;
                //frmInicial.nivelAcesso = Login.GetInt32(2);
                this.Close();                
            }

            else //se retornar 0, é por que não encontrou login
            {
                MessageBox.Show("Nome do Usuario ou senha Inválidos! O Sistema diferencia Maiuscula e Minuscula! Caso você tenha esquecido a senha, contacte o Administrador!", "FuturaData - Login no Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSenhaUsuario.Clear();
                tbxSenhaUsuario.Focus();
                frmInicial.usuarioEfetuouLogon = false;
                frmInicial.ptbEfetuandoLogon.Image = FuturaDataTCC.Properties.Resources.btnStatusRuimPeq;
                frmInicial.ptbEfetuandoLogon.Refresh();
            }

        }//fim btnentrar
        #endregion

        #region KeyDown do Form
        public void frmLoginSistema_KeyPress(object sender, KeyEventArgs e)
        {
            #region Atalho ENTER no botão(Botão Enter de Login)

            if (e.KeyCode == Keys.Enter)
            {
                btnentrar_Click(null, null);
            }
            #endregion

            #region Atalho F12 no botão(Botão F12 de FrmLog)

            if (e.KeyCode == Keys.F12)
            {
                
            }
            #endregion
        }
        #endregion

        #region tbxSenha
        private void txtsenha_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        #endregion

        #region LoginSistema
        private void loginSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnentrar_Click(null, null);
        }//fim btnLogin
        #endregion

        #region botaoentrar
        private void btnentrar_Click_1(object sender, EventArgs e)
        {
            btnentrar_Click(null, null);
        }
        #endregion
        
        #region Evento dos Menus Configurar Conexão e Configurar Sistema
        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConfigConexaoServidor config = new frmConfigConexaoServidor();
            config.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmConfigSistema config = new frmConfigSistema(this.frmInicial);
            config.ShowDialog();
        }
        #endregion
        
        #region Evento Tick do Relógio
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDataEHoraSist.Text = "Data/Hora Sistema: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm.ss");
            lblDataEHoraSist.Refresh();
        }
        #endregion

        #region Evento Label Hora Sistema Errada
        private void label3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(null, "Caso sua data/hora esteja errada, o sistema pode apresentar diversos problemas! O sistema usa a hora do computador para efetuar lançamento de informações. Caso a hora esteja errada, todas as informações computadas serão computadas com datas/horas erradas também e automaticamente causando grande confusão no sistema. Para resolver o problema, dê dois cliques no relógio ao lado direito inferior do sistema, ajuste o horário e entre novamente. Caso seu computador esteja perdendo data/hora com frequencia, procure uma assistência técnica! Não utilize em ipotese alguma o sistema com horário incorreto!", "FuturaData - Login no Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        #region Botao Sair do Sistema
        private void button1_Click(object sender, EventArgs e)
        {
            clsFilaProcessosWindows.finalizarPrograma();
        }
        #endregion

        #region Evento Label Primeiro Acesso
        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "Esse é o seu primeiro acesso? A senha padrão do sistema é:" + Environment.NewLine + Environment.NewLine + "Usuario: adm" + Environment.NewLine + "Senha: 1234fd" + Environment.NewLine + Environment.NewLine + "Essa é a senha padrão do sistema que já vem pre-configurada para o primeiro acesso. Caso o administrador do sistema ainda não tenha efetuado a troca, será possível fazer logon com a mesma. Caso você ainda não tenha um login no sistema, faça a requisição junto do administrador do sistema." + Environment.NewLine + Environment.NewLine + "Administrador: Efetue a troca da senha padrão após o primeiro acesso do sistema.", "FuturaData - Login no Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion
    }//fim classe
}//fim namespace

