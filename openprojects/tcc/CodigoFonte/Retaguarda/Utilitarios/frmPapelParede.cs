using System;
using System.Drawing;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using System.IO;
using DllFuturaDataTCC.Utilitarios;
using DllInfoSigaUtil.Grafico;

namespace FuturaDataTCC.Utilitarios
{
    public partial class frmPapelParede : Form
    {
        frmInicializacao frmInicial;
        frmTelaPrincipal frmTelaPrinc;

        string caminhoImagem = "";
        Byte[] imagemEmBytes;

        #region Construtor do Form
        public frmPapelParede(frmInicializacao frmInicia, frmTelaPrincipal alterarPapelParede)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            frmTelaPrinc = alterarPapelParede;

            pctPapelProprio.Image = frmTelaPrinc.pnlPapelParede.Image;
            pctPapelProprio.Refresh();



            //BACKUP DO CÓDIGO FONTE PRADEFINIR PAPEL DE PAREDE - FERNANDO 01072013
            ////frmTelaPrinc.picPapelParede.Image = global::InfoSiga_Basic.Properties.Resources.infosiga1;
            //frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w1;
            //clsConfiguracoes gravarPapel = new clsConfiguracoes();
            //if (gravarPapel.alterarPapelParedeUsuario(1, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            //{
            //    //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        #endregion

        #region ATALHOS
        public bool teclasAtalho(KeyEventArgs e)
        {          
            #region Atalho F7 e ESC no botão(Cancelar Operação)

            if ((e.KeyCode == Keys.F7) || (e.KeyCode == Keys.Escape))
            {
                if (MessageBox.Show(null, "Todos os dados digitados não foram salvos, deseja realmente FECHAR a tela?", "FuturaData Business - Fechamento de Janela", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                {
                    this.Close();
                    return true;
                }
            }
            #endregion

            #region Atalho ENTER função de Tab
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            return false;
            #endregion
        }
        #endregion

        #region Evento KeyDown do Form
        private void frmPapelParede_KeyDown(object sender, KeyEventArgs e)
        {
            this.teclasAtalho(e);
        }
        #endregion

        #region Evento do Botao Seleciona Próprio Papel de Parede
        private void btnSelecioneSeuProprioPapel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "Atenção: É aconselhável selecionar uma imagem com resolução igual ou superior a 1000x620. Caso sua imagem seja inferior a esse tamanho, o sistema automaticamente irá tratar a mesma para aumentar o tamanho, porém irá perder em qualidade de imagem!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OpenFileDialog openFileProcurarImagem = new OpenFileDialog();
            openFileProcurarImagem.Title = "Selecione uma imagem para o seu Papel de Parede...";
            //openFileProcurarImagem.InitialDirectory = @"C:\";
            openFileProcurarImagem.RestoreDirectory = true;
            openFileProcurarImagem.Filter = "Imagens JPG (apenas formato JPG) (*.jpg)|*.jpg;";
            if (openFileProcurarImagem.ShowDialog() == DialogResult.OK)
            {
                clsImagem img = new clsImagem();
                caminhoImagem = @"c:\FuturaData\retorno.jpg";
                bool retorno = new clsImagem().gerarNovaImagem(1000, 620, openFileProcurarImagem.FileName.ToString(),caminhoImagem);
                if (retorno)//se ele conseguir gerar ele pega o novo caminho da nova imagem
                {
                    //caminhoImagem = retorno;
                    Image imgObj = Image.FromFile(caminhoImagem);
                    pctPapelProprio.Image = imgObj;

                    long tamanhoDaImagem = 0;
                    FileInfo imagem = new FileInfo(caminhoImagem);
                    tamanhoDaImagem = imagem.Length;
                    imagemEmBytes = new byte[Convert.ToInt32(tamanhoDaImagem)];
                    FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read, FileShare.Read);
                    fs.Read(imagemEmBytes, 0, Convert.ToInt32(tamanhoDaImagem));
                    fs.Close();

                    frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w33;
                    clsConfiguracoes gravarPapel = new clsConfiguracoes();
                    if (gravarPapel.alterarPapelParedeUsuario(33, false, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
                    {
                        //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    clsConfiguracoes config = new clsConfiguracoes();
                    bool retorno3 = config.personalizaPapelParedeUsuarioEscolhe(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost, imagemEmBytes);

                    if (retorno)
                    {
                        frmTelaPrinc.pnlPapelParede.Image = imgObj;
                        frmTelaPrinc.pnlPapelParede.Refresh();
                        MessageBox.Show("Papel alterado com sucesso!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Falha na alteração do papel de parede. Verifique sua conexão, caso o problema persista contate a FuturaData.", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else//senão pega a normal mesmo sem compactacao
                {
                    caminhoImagem = openFileProcurarImagem.FileName.ToString();
                }                
            }
        }
        #endregion

        #region Evento da Troca dos Papeis de Parede (66 Eventos)
        private void pct1_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w1;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(1, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct2_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w2;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(2, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct3_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w3;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(3, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct4_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w4;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(4, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct5_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w5;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(5, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct6_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w6;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(6, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct7_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w7;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(7, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct8_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w8;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(8, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct9_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w9;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(9, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct10_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w10;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(10, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct11_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w11;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(11, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct12_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w12;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(12, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct13_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w13;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(13, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct14_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w14;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(14, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct15_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w15;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(15, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct16_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w16;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(16, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct17_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w17;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(17, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct18_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w18;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(18, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct19_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w19;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(19, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct20_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w20;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(20, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct21_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w21;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(21, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct22_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w22;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(22, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct23_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w23;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(23, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct24_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w24;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(24, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct25_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w25;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(25, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct26_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w26;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(26, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct27_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w27;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(27, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct28_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w28;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(28, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct29_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w29;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(29, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct30_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w30;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(30, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct31_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w31;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(31, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct32_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w32;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(32, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct33_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w33;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(33, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct34_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w34;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(34, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct35_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w35;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(35, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct36_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w36;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(36, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct37_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w37;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(37, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct38_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w38;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(38, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct39_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w39;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(39, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct40_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w40;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(40, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct41_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w41;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(41, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct42_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w42;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(42, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct43_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w43;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(43, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct44_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w44;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(44, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct45_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w45;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(45, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct46_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w46;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(46, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct47_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w47;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(47, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct48_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w48;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(48, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct49_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w49;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(49, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct50_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w50;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(50, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct51_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w51;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(51, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct52_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w52;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(52, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct53_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w53;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(53, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct54_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w54;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(54, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct55_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w55;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(55, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct56_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w56;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(56, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct57_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w57;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(57, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct58_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w58;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(58, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct59_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w59;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(59, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct60_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w60;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(60, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct61_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w61;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(61, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct62_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w62;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(62, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct63_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w63;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(63, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct64_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w64;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(64, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct65_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w65;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(65, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pct66_Click(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w66;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(66, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                //MessageBox.Show("Papel parede Registrado em suas configurações com Sucesso!", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkHabilitarModoRandom_CheckedChanged(object sender, EventArgs e)
        {
            frmTelaPrinc.pnlPapelParede.Image = global::FuturaDataTCC.Properties.Resources.w6;
            clsConfiguracoes gravarPapel = new clsConfiguracoes();
            if (gravarPapel.alterarPapelParedeUsuario(6, chkHabilitarModoRandom.Checked, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost) == true)
            {
                if (chkHabilitarModoRandom.Checked)
                {
                    MessageBox.Show("Modo Random Habilitado! O sistema irá trocar seu Papel de Parede Automaticamente a cada 15 minutos... além disso a Cada Inicialização você terá um Papel de Parede diferente! Essa escolha é feita de modo aleatório podendo ser qualquer um dos mais de 60 papeis de parede disponíveis.", "Papel Gravado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Erro ao Alterar o Papel de Parede!", "FuturaData - Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }//fim classe
}//fim namespace
