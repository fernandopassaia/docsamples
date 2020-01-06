using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using DllFuturaDataContrValidacoes;
using FuturaDataTCC.Utilitarios;
using System.Reflection;
//using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using System.IO;
using System.Diagnostics;
using System.Threading;
using DllFuturaDataTCC.Utilitarios;

namespace FuturaDataTCC.Iniciar
{
    public partial class frmConfigConexaoServidor : Form
    {
        #region Construtor do Form e Variaveis Internas
        //public string ConexaoBancoAccess = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\FuturaData\TCC\\log\info.mdb;;Persist Security Info=True;Jet OLEDB:Database Password=143031si";        
        string conexao = "";
        bool conexaoJaVerificada = false;
        public frmConfigConexaoServidor()
        {
            InitializeComponent();

            string conexao = new clsConexao().recuperaStringConexaoSQLServer().Trim().ToUpper();
            #region Antigo Método para Montar conexão usando o Access
            //int indice6 = 0;
            //#region Gambiarra Nervosa
            //if (conexao != "")
            //{
            //    for (int i = 0; i < conexao.Length; i++)
            //    {
            //        if (conexao.Substring(i, 1) == "=")
            //        {
            //            int contadorAteAPrimeiraBarra = 0;
            //            for (int i2 = i; i2 < conexao.Length - i; i2++)
            //            {
            //                if (conexao.Substring(i2, 1) == "\\")
            //                {
            //                    #region Gambi pra Achar o nome do Servidor/Nome Serviço
            //                    contadorAteAPrimeiraBarra = i2;
            //                    string nomeServidorEServico = conexao.Substring(i, i2);
            //                    nomeServidorEServico = nomeServidorEServico.Replace("=", "");

            //                    bool jaAchouABarra = false;
            //                    for (int i3 = 0; i3 < nomeServidorEServico.Length; i3++)
            //                    {
            //                        if (nomeServidorEServico.Substring(i3, 1) != "\\" && jaAchouABarra == false)
            //                        {
            //                            tbxNomeServidor.Text = tbxNomeServidor.Text + nomeServidorEServico.Substring(i3, 1);
            //                        }
            //                        else
            //                        {
            //                            jaAchouABarra = true;
            //                            tbxNomeServico.Text = tbxNomeServico.Text + nomeServidorEServico.Substring(i3, 1);
            //                        }
            //                    }
            //                    tbxNomeServico.Text = tbxNomeServico.Text.Replace("\\", "");
            //                    tbxNomeServidor.Text = tbxNomeServidor.Text.Replace("\\", "");
            //                    #endregion
            //                    //metodologia de programação da marmota louca

            //                    #region Gambi para Achar o nome do Banco de Dados
            //                    for (int i4 = 0; i4 < conexao.Length; i4++)
            //                    {
            //                        bool jaAchouPrimeiroPontoEVirgula = false;
            //                        if (conexao.Substring(i4, 1) == ";")
            //                        {
            //                            int numeroPrimeiroPontoEVirgula = i4;
            //                            int numeroSegundoPontoEVirgula = 0;

            //                            for (int i5 = i4; i5 < conexao.Length - i4; i5++)
            //                            {
            //                                if (conexao.Substring(i5, 1) == "=")
            //                                {
            //                                    bool jaAchou = false;
            //                                    for (int i6 = i5; i6 < conexao.Length - i5; i6++)
            //                                    {
            //                                        if (conexao.Substring(i6, 1) != ";" && jaAchou == false)
            //                                        {
            //                                            indice6 = i6;
            //                                            tbxNomeBaseDados.Text = tbxNomeBaseDados.Text + conexao.Substring(i6, 1);
            //                                        }
            //                                        else
            //                                        {
            //                                            jaAchou = true;
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                            tbxNomeBaseDados.Text = tbxNomeBaseDados.Text.Replace(";", "");
            //                            tbxNomeBaseDados.Text = tbxNomeBaseDados.Text.Replace("=", "");
            //                            tbxNomeBaseDados.Text = tbxNomeBaseDados.Text.Replace("INITIAL CATALOG=", "");
            //                        }
            //                    }
            //                    #endregion

            //                    #region Gambi Caso tenha que achar a Senha!
            //                    string tipoConexao = conexao.Substring(conexao.Length - 5, 4);
            //                    int pontoEVirgula = indice6 + 1;
            //                    if (tipoConexao != "TRUE")
            //                    {
            //                        rdbServidorSemAutentIntegrada.Checked = true;
            //                    bool jaAchouIndiceUsuario = false;
            //                    int aondeTaOUsuario = 0;
            //                    for (int i7 = 0; i7 < conexao.Length; i7++)
            //                    {
            //                        #region Antigo Código Marmota da Morte
            //                        //int indiceUsuario = 0;
            //                        //if (tipoConexao != "TRUE")
            //                        //{
            //                        //    rdbServidorSemAutentIntegrada.Checked = true;
            //                        //    for (int i8 = indice6+2; i8 < conexao.Length; i8++)
            //                        //    {
            //                        //        if (conexao.Substring(i8, 8) != "USER ID=" && jaAchouIndiceUsuario == false)
            //                        //        {
            //                        //            indiceUsuario = i8;
            //                        //            tbxNomeUsuario.Text = tbxNomeUsuario.Text + conexao.Substring(i8, 1);
            //                        //        }
            //                        //        else
            //                        //        {
            //                        //            jaAchouIndiceUsuario = true;
            //                        //        }
            //                        //    }
                                                                                
            //                        //    tbxNomeUsuario.Text = tbxNomeUsuario.Text.Replace("USER ID=", "");
            //                        #endregion
                                    
            //                            bool jaAchouUsuario = false;
            //                            bool jaAchouSenha = false;
                                        
            //                            #region Acha o nome do Usuario Fail
            //                            if (i7 < conexao.Length - 8)
            //                            {
            //                                if (conexao.Substring(i7, 8) == "USER ID=")
            //                                {
            //                                    int i8 = i7;

            //                                    while (i8 < conexao.Length)
            //                                    {
            //                                        if (conexao.Substring(i8, 1) != ";" && jaAchouUsuario == false)
            //                                        {
            //                                            tbxNomeUsuario.Text = tbxNomeUsuario.Text + conexao.Substring(i8, 1);
            //                                        }
            //                                        else
            //                                        {
            //                                            if (jaAchouUsuario == false)
            //                                            {
            //                                                aondeTaOUsuario = i8;
            //                                            }
            //                                            jaAchouUsuario = true;                                                        
            //                                        }
            //                                        i8++;                                                    
            //                                    }
            //                                    tbxNomeUsuario.Text = tbxNomeUsuario.Text.Replace("USER ID=", "");                                                
            //                                }
            //                            }//fim if conexao.lentgt - 8
            //                            #endregion                                        
            //                    }//fim if tipoconexao=true
            //                    tbxSenha.Text = conexao.Substring(aondeTaOUsuario, conexao.Length - aondeTaOUsuario);
            //                    tbxSenha.Text = tbxSenha.Text.Replace("PASSWORD=", "");
            //                    tbxSenha.Text = tbxSenha.Text.Replace(";", "");                                
            //                    tbxSenha.Text = tbxSenha.Text.ToLower();
            //                    }//fim for
            //                    #endregion
            //                }//fim for que procura a primeira barra
            //            }
            //        }//fim for que varre a conexão inteira
            //    }//fim if que monta a conexao
            //}//fim construtor
            //    #endregion
            #endregion

            DataTable dt_Dados = new clsConexao().retornaConexao();

            tbxNomeServidor.Text = dt_Dados.Rows[0]["SERVIDOR"].ToString();
            tbxNomeServico.Text = dt_Dados.Rows[0]["SERVICO"].ToString();
            tbxNomeBaseDados.Text = dt_Dados.Rows[0]["BASEDADOS"].ToString();
            
            string servidorAceitaCS = dt_Dados.Rows[0]["ACEITACONEXOESSEGURAS"].ToString();

            if (servidorAceitaCS.ToUpper() == "TRUE")
            {
                rdbServidorComAutentIntegrada.Checked = true;
            }
            else
            {
                rdbServidorSemAutentIntegrada.Checked = true;
                tbxNomeUsuario.Text = dt_Dados.Rows[0]["USUARIO"].ToString();
                tbxSenha.Text = dt_Dados.Rows[0]["SENHA"].ToString();
            }
            btnTestarConexao_Click(null, null);

            if (File.Exists(@"c:\FuturaData\TCC\\sqlb.bat"))
            {
                if (MessageBox.Show(null, "Foi Detectada uma nova instalação do Sistema. Deseja que o Sistema Crie uma Base de Dados Nova para você? (se essa for sua primeira instalação e esse for seu servidor clique em SIM)", "FuturaData - Configuração do Servidor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(@"c:\FuturaData\TCC\\sqlb.bat");
                    Thread.Sleep(8000);
                    MessageBox.Show("Operação realizada. O sistema será reiniciado automaticamente", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    clsFilaProcessosWindows.finalizarPrograma();
                }
            }
            else
            {
                lblPrimeiraInstalacao.Enabled = false;
                lblPrimeiraInstalacao.Text = "";
                lblPrimeiraInstalacao.Refresh();
            }
        }//fim construtor
        #endregion

        #region Método Monta String Conexao
        public void montarStringConexao()
        {
            conexao = "";
            //Data Source=FERNANDO-NOTE\SQLEXPRESS;Initial Catalog=FDSTCOMERCIO;Integrated Security=True;
            if (rdbServidorComAutentIntegrada.Checked == true)
            {
                conexao = "Data Source="+tbxNomeServidor.Text + @"\" + tbxNomeServico.Text + ";Initial Catalog=" + tbxNomeBaseDados.Text + ";Integrated Security=True;";
            }
            if (rdbServidorSemAutentIntegrada.Checked == true)
            {
                //Data Source=SERVIDOR\FUTURADATA;Initial Catalog=INFOSIGASTANDART;Integrated Security=True;User ID=sa;Password=143031si
                conexao = "Data Source=" + tbxNomeServidor.Text + @"\" + tbxNomeServico.Text + ";Initial Catalog=" + tbxNomeBaseDados.Text + ";Integrated Security=False;User ID="+tbxNomeUsuario.Text+";Password="+tbxSenha.Text;
            }
        }
        #endregion

        #region Evento Botao Salvar Configuração
        private void btnSalvarConfig_Click(object sender, EventArgs e)
        {
            btnTestarConexao_Click(null, null);
            if (conexaoJaVerificada == true)
            {
                try
                {
                    string status = "TRUE";
                    if(rdbServidorSemAutentIntegrada.Checked)
                    {
                        status = "FALSE";
                    }
                    bool retorno = new clsConexao().alteraConexaoBancoXML(tbxNomeServidor.Text, tbxNomeServico.Text, tbxNomeBaseDados.Text, status, tbxNomeUsuario.Text, tbxSenha.Text);
                    if (retorno)
                    {
                        MessageBox.Show("Configuração Salva com Sucesso! O sistema será finalizado automaticamente, basta abri-lo novamente!", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clsFilaProcessosWindows.finalizarPrograma();
                    }
                    else
                    {
                        MessageBox.Show("Houve uma falha ao alterar a conexão. Por favor verifique. Caso o problema persista, contate o Suporte FuturaData!", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (OleDbException erro)
                {
                    MessageBox.Show("Erro na configuração da string, a configuração não foi efetuada! Erro do compilado: " + erro.Message.ToString(), "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }//fim do if conexao verificada
        }
        #endregion

        #region Evento Botao Testar Conexão
        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            montarStringConexao();

            SqlConnection conexaoBanco = new SqlConnection(conexao);
            try
            {
                conexaoBanco.Open();
                MessageBox.Show("Conexão efetuada com Sucesso!", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexaoJaVerificada = true;
                lblNaoConectado.Text = "Conexão efetuada com sucesso! Clique em salvar!";
                lblNaoConectado.ForeColor = Color.Blue;

                tbxNomeBaseDados.ReadOnly = true;
                tbxNomeServico.ReadOnly = true;
                tbxNomeServidor.ReadOnly = true;
                tbxNomeUsuario.ReadOnly = true;
                tbxSenha.ReadOnly = true;
                rdbServidorComAutentIntegrada.Enabled = false;
                rdbServidorSemAutentIntegrada.Enabled = false;

                pctImagem.Image = FuturaDataTCC.Properties.Resources.comconexao;
                pctImagem.Refresh();
                if (File.Exists(@"c:\FuturaData\TCC\sqlb.bat"))
                {
                    File.Delete(@"c:\FuturaData\TCC\sqlb.bat");
                }
            }
            catch (SqlException erro)
            {
                if (File.Exists(@"c:\FuturaData\TCC\sqlb.bat") == false)
                {
                    MessageBox.Show("Conexão não efetuada, erro na conexão a base de dados. Erro do compilador: " + erro.Message.ToString(), "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conexaoJaVerificada = false;
            }
        }
        #endregion

        #region Evento Radio Button Servidor Com Conexao Integrada
        private void rdbServidorComAutentIntegrada_CheckedChanged(object sender, EventArgs e)
        {
            tbxNomeUsuario.ReadOnly = true;
            tbxSenha.ReadOnly = true;
            tbxNomeUsuario.Clear();
            tbxSenha.Clear();
        }
        #endregion

        #region Evento Radio Button Servidor Com Conexao Não Integrada
        private void rdbServidorSemAutentIntegrada_CheckedChanged(object sender, EventArgs e)
        {
         
            tbxNomeUsuario.Clear();
            tbxSenha.Clear();
            tbxSenha.ReadOnly = false;
            tbxNomeUsuario.ReadOnly = false;

            tbxSenha.BackColor = Color.White;
            tbxNomeUsuario.BackColor = Color.White;
        }
        #endregion

        #region Evento KeyDown
        private void frmConfigConexaoServidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                tbxNomeBaseDados.Enabled = true;
                tbxNomeServico.Enabled = true;
                tbxNomeServidor.Enabled = true;
                tbxNomeUsuario.Enabled = true;
                tbxSenha.Enabled = true;
                tbxSenha.ReadOnly = false;
                tbxNomeUsuario.ReadOnly = false;
                tbxNomeServidor.ReadOnly = false;
                tbxNomeServico.ReadOnly = false;
                tbxNomeBaseDados.ReadOnly = false;
                rdbServidorComAutentIntegrada.Enabled = true;
                rdbServidorSemAutentIntegrada.Enabled = true;
            }
        }

        private void tbxNomeServidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                tbxNomeBaseDados.Enabled = true;
                tbxNomeServico.Enabled = true;
                tbxNomeServidor.Enabled = true;
                tbxNomeUsuario.Enabled = true;
                tbxSenha.Enabled = true;
                tbxSenha.ReadOnly = false;
                tbxNomeUsuario.ReadOnly = false;
                tbxNomeServidor.ReadOnly = false;
                tbxNomeServico.ReadOnly = false;
                tbxNomeBaseDados.ReadOnly = false;
                rdbServidorComAutentIntegrada.Enabled = true;
                rdbServidorSemAutentIntegrada.Enabled = true;
            }
        }
        #endregion

        #region Evento da Label Tente achar a conexao para mim
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void btnDicasEnvio_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "Dica rápida de como configurar sua conexão de modo simples, siga as dicas abaixo e verifique se consegue se conectar:" + Environment.NewLine + Environment.NewLine + "Nome do Servidor: SERVIDOR*" + Environment.NewLine + "Nome Serviço da Base de Dados: FUTURADATA" + Environment.NewLine + "Nome Base de Dados: FDCORPORATEERP" + Environment.NewLine + "Servidor sem autenticação integrada (com senha)" + Environment.NewLine + "Nome Usuário: sa" + Environment.NewLine + "Senha: 1234fd" + Environment.NewLine + Environment.NewLine + "* Onde SERVIDOR é o nome da máquina, na rede, que possui o banco de dados de sua empresa. (por exemplo: RENATO-PC)" + Environment.NewLine + Environment.NewLine + "Caso as configurações ainda estejam padrão e seu administrador não tenha trocado nenhuma senha ou configuração, os parametros acima devem funcionar. Após isso, basta clicar em SALVAR CONFIGURAÇÃO e seu sistema estará configurado para o acesso!" + Environment.NewLine + Environment.NewLine + "Caso mesmo assim você não consiga localizar, você ainda pode clicar sobre 'Clique aqui para tentar localizar servidores disponíveis em sua rede' para que o sistema tenta localizar o Servidor automaticamente. Caso o mesmo não seja localizado, peça para que seu administrador verifique o Serviço e se não há um Firewall ou Antivirus bloqueando o acesso. (essas instruções encontram-se no manual do sistema)", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(null, "Dica rápida de como configurar sua conexão de modo simples, siga as dicas abaixo e verifique se consegue se conectar:" + Environment.NewLine + Environment.NewLine + "Nome do Servidor: SERVIDOR*" + Environment.NewLine + "Nome Serviço da Base de Dados: FUTURADATA" + Environment.NewLine + "Nome Base de Dados: FDCORPORATEERP" + Environment.NewLine + "Servidor sem autenticação integrada (com senha)" + Environment.NewLine + "Nome Usuário: sa" + Environment.NewLine + "Senha: 1234fd" + Environment.NewLine + Environment.NewLine + "* Onde SERVIDOR é o nome da máquina, na rede, que possui o banco de dados de sua empresa. (por exemplo: RENATO-PC)" + Environment.NewLine + Environment.NewLine + "Caso as configurações ainda estejam padrão e seu administrador não tenha trocado nenhuma senha ou configuração, os parametros acima devem funcionar. Após isso, basta clicar em SALVAR CONFIGURAÇÃO e seu sistema estará configurado para o acesso!" + Environment.NewLine + Environment.NewLine + "Caso mesmo assim você não consiga localizar, você ainda pode clicar sobre 'Clique aqui para tentar localizar servidores disponíveis em sua rede' para que o sistema tenta localizar o Servidor automaticamente. Caso o mesmo não seja localizado, peça para que seu administrador verifique o Serviço e se não há um Firewall ou Antivirus bloqueando o acesso. (essas instruções encontram-se no manual do sistema)", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblPrimeiraInstalacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(File.Exists(@"c:\FuturaData\TCC\sqlb.bat"))
            {
                if (MessageBox.Show(null, "Deseja que o Sistema Crie uma Base de Dados Nova para você? (se essa for sua primeira instalação e esse for seu servidor clique em SIM)", "FuturaData - Configuração do Servidor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(@"c:\FuturaData\TCC\sqlb.bat");
                    Thread.Sleep(8000);
                    MessageBox.Show("Operação realizada. O sistema será reiniciado automaticamente", "FuturaData - Configuração do Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    clsFilaProcessosWindows.finalizarPrograma();
                }
            }
        }
    }//fim classe
}//fim namespace
