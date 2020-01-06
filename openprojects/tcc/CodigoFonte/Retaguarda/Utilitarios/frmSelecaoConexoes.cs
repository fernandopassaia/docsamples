using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DllFuturaDataCriptografia;
using System.Data.SqlClient;
using System.IO;

namespace FuturaDataTCC.Utilitarios
{
    public partial class frmSelecaoConexoes : Form
    {
        #region Construtor e Variaveis Internas do Form
        string diretorio = System.Environment.CommandLine.ToString().Replace("vshost", "");
        public frmSelecaoConexoes()
        {
            InitializeComponent();

            //Verifica Conexao Dos Dois Bancos
            DataSet dsDadosXML = new DataSet();
            clsCriptografia crip = new clsCriptografia();
            dsDadosXML.ReadXml(@"c:\FuturaData\TCC\conexoes\conexao1.xml");
            string servidor1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVIDOR"].ToString());
            string servico1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SERVICO"].ToString());
            string baseDados1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["BASEDADOS"].ToString());
            string aceitaConexoesSeguras1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
            string usuario1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["USUARIO"].ToString());
            string senha1 = crip.Descriptografar(dsDadosXML.Tables[0].Rows[0]["SENHA"].ToString());
            string descricaoBanco1 = dsDadosXML.Tables[0].Rows[0]["NOMEEMPRESA"].ToString();
            string conexao1 = "Data Source=" + servidor1 + @"\" + servico1 + ";Initial Catalog=" + baseDados1 + ";Integrated Security=" + aceitaConexoesSeguras1 + ";";
            if (aceitaConexoesSeguras1.ToUpper() == "FALSE")
            {
                 conexao1 = conexao1 + "User ID=" + usuario1 + ";Password=" + senha1 + ";";
            }

            lblBancoEmpresa1.Text = lblBancoEmpresa1.Text + " " + baseDados1;
            lblDescricaoEmpresa1.Text = lblDescricaoEmpresa1.Text + " " + descricaoBanco1;
            lblInstanciaBanco1.Text = lblInstanciaBanco1.Text + " " + servico1;
            lblServidorEmpresa1.Text = lblServidorEmpresa1.Text + " " + servidor1;
            
            DataSet dsDadosXML2 = new DataSet();            
            dsDadosXML2.ReadXml(@"c:\FuturaData\TCC\conexoes\conexao2.xml");
            string servidor2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["SERVIDOR"].ToString());
            string servico2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["SERVICO"].ToString());
            string baseDados2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["BASEDADOS"].ToString());
            string aceitaConexoesSeguras2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
            string usuario2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["USUARIO"].ToString());
            string senha2 = crip.Descriptografar(dsDadosXML2.Tables[0].Rows[0]["SENHA"].ToString());
            string descricaoBanco2 = dsDadosXML2.Tables[0].Rows[0]["NOMEEMPRESA"].ToString();
            string conexao2 = "Data Source=" + servidor2 + @"\" + servico2 + ";Initial Catalog=" + baseDados2 + ";Integrated Security=" + aceitaConexoesSeguras2 + ";";
            if (aceitaConexoesSeguras2.ToUpper() == "FALSE")
            {
                conexao2 = conexao2 + "User ID=" + usuario2 + ";Password=" + senha2 + ";";
            }

            lblBancoEmpresa2.Text = lblBancoEmpresa2.Text + " " + baseDados2;
            lblDescricaoEmpresa2.Text = lblDescricaoEmpresa2.Text + " " + descricaoBanco2;
            lblInstanciaBanco2.Text = lblInstanciaBanco2.Text + " " + servico2;
            lblServidorEmpresa2.Text = lblServidorEmpresa2.Text + " " + servidor2;

            SqlConnection ConexaoBd1 = new SqlConnection(conexao1);
            try
            {
                ConexaoBd1.Open();
                btnIniciarSistemaEmpresa1.Enabled = true;
                pcbBanco1.Image = FuturaDataTCC.Properties.Resources.bancoAtivo;
                lblStatusEmpresa1.Text = lblStatusEmpresa1.Text + " Banco Ativo!";
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                btnIniciarSistemaEmpresa1.Enabled = false;
                pcbBanco1.Image = FuturaDataTCC.Properties.Resources.bancoOff;
                lblStatusEmpresa1.Text = lblStatusEmpresa1.Text + " Banco Offline!";
            }

            SqlConnection ConexaoBd2 = new SqlConnection(conexao2);
            try
            {
                ConexaoBd2.Open();
                btnIniciarEmpresaSistema2.Enabled = true;
                pcbBanco2.Image = FuturaDataTCC.Properties.Resources.bancoAtivo;
                lblStatusEmpresa2.Text = lblStatusEmpresa2.Text + " Banco Ativo!";
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                btnIniciarEmpresaSistema2.Enabled = false;
                pcbBanco2.Image = FuturaDataTCC.Properties.Resources.bancoOff;
                lblStatusEmpresa2.Text = lblStatusEmpresa2.Text + " Banco Offline!";
            }


            if (File.Exists(@"c:\FuturaData\TCC\conexoes\conexao3.xml"))
            {
                DataSet dsDadosXML3 = new DataSet();
                dsDadosXML3.ReadXml(@"c:\FuturaData\TCC\conexoes\conexao3.xml");
                string servidor3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["SERVIDOR"].ToString());
                string servico3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["SERVICO"].ToString());
                string baseDados3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["BASEDADOS"].ToString());
                string aceitaConexoesSeguras3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
                string usuario3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["USUARIO"].ToString());
                string senha3 = crip.Descriptografar(dsDadosXML3.Tables[0].Rows[0]["SENHA"].ToString());
                string descricaoBanco3 = dsDadosXML3.Tables[0].Rows[0]["NOMEEMPRESA"].ToString();
                string conexao3 = "Data Source=" + servidor3 + @"\" + servico3 + ";Initial Catalog=" + baseDados3 + ";Integrated Security=" + aceitaConexoesSeguras3 + ";";
                if (aceitaConexoesSeguras3.ToUpper() == "FALSE")
                {
                    conexao3 = conexao3 + "User ID=" + usuario3 + ";Password=" + senha3 + ";";
                }

                lblBancoEmpresa3.Text = lblBancoEmpresa3.Text + " " + baseDados3;
                lblDescricaoEmpresa3.Text = lblDescricaoEmpresa3.Text + " " + descricaoBanco3;
                lblInstanciaBanco3.Text = lblInstanciaBanco3.Text + " " + servico3;
                lblServidorEmpresa3.Text = lblServidorEmpresa3.Text + " " + servidor3;

                SqlConnection ConexaoBd3 = new SqlConnection(conexao3);
                try
                {
                    ConexaoBd3.Open();
                    btnIniciarEmpresaSistema3.Enabled = true;
                    pcbBanco3.Image = FuturaDataTCC.Properties.Resources.bancoAtivo;
                    lblStatusEmpresa3.Text = lblStatusEmpresa3.Text + " Banco Ativo!";
                }
                //caindo no CATCH chama as rotinas que geram os logs de erro
                catch (SqlException erro)
                {
                    btnIniciarEmpresaSistema3.Enabled = false;
                    pcbBanco3.Image = FuturaDataTCC.Properties.Resources.bancoOff;
                    lblStatusEmpresa3.Text = lblStatusEmpresa3.Text + " Banco Offline!";
                }
            }
            else
            {
                lblServidorEmpresa3.Text = "Sem empresa 3 configurada!";
            }


            if (File.Exists(@"c:\FuturaData\TCC\conexoes\conexao4.xml"))
            {
                DataSet dsDadosXML4 = new DataSet();
                dsDadosXML4.ReadXml(@"c:\FuturaData\TCC\conexoes\conexao4.xml");
                string servidor4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["SERVIDOR"].ToString());
                string servico4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["SERVICO"].ToString());
                string baseDados4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["BASEDADOS"].ToString());
                string aceitaConexoesSeguras4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["ACEITACONEXOESSEGURAS"].ToString());
                string usuario4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["USUARIO"].ToString());
                string senha4 = crip.Descriptografar(dsDadosXML4.Tables[0].Rows[0]["SENHA"].ToString());
                string descricaoBanco4 = dsDadosXML4.Tables[0].Rows[0]["NOMEEMPRESA"].ToString();
                string conexao4 = "Data Source=" + servidor4 + @"\" + servico4 + ";Initial Catalog=" + baseDados4 + ";Integrated Security=" + aceitaConexoesSeguras4 + ";";
                if (aceitaConexoesSeguras4.ToUpper() == "FALSE")
                {
                    conexao4 = conexao4 + "User ID=" + usuario4 + ";Password=" + senha4 + ";";
                }

                lblBancoEmpresa4.Text = lblBancoEmpresa4.Text + " " + baseDados4;
                lblDescricaoEmpresa4.Text = lblDescricaoEmpresa4.Text + " " + descricaoBanco4;
                lblInstanciaBanco4.Text = lblInstanciaBanco4.Text + " " + servico4;
                lblServidorEmpresa4.Text = lblServidorEmpresa4.Text + " " + servidor4;

                SqlConnection ConexaoBd4 = new SqlConnection(conexao4);
                try
                {
                    ConexaoBd4.Open();
                    btnIniciarEmpresaSistema4.Enabled = true;
                    pcbBanco4.Image = FuturaDataTCC.Properties.Resources.bancoAtivo;
                    lblStatusEmpresa4.Text = lblStatusEmpresa4.Text + " Banco Ativo!";
                }
                //caindo no CATCH chama as rotinas que geram os logs de erro
                catch (SqlException erro)
                {
                    btnIniciarEmpresaSistema4.Enabled = false;
                    pcbBanco4.Image = FuturaDataTCC.Properties.Resources.bancoOff;
                    lblStatusEmpresa4.Text = lblStatusEmpresa4.Text + " Banco Offline!";
                }
            }
            else
            {
                lblServidorEmpresa4.Text = "Sem empresa 4 configurada!";
            }

        }
        #endregion

        private void btnIniciarSistemaEmpresa1_Click(object sender, EventArgs e)
        {
            if(File.Exists(@"c:\FuturaData\TCC\conexoes\conexao.xml"))
            {
                File.Delete(@"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            if (File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                File.Move(@"c:\FuturaData\TCC\conexao.xml", @"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            File.Copy(@"c:\FuturaData\TCC\conexoes\conexao1.xml", @"c:\FuturaData\TCC\conexao.xml", true);
            this.Close();
        }

        private void btnIniciarEmpresaSistema2_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"c:\FuturaData\TCC\conexoes\conexao.xml"))
            {
                File.Delete(@"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            if (File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                File.Move(@"c:\FuturaData\TCC\conexao.xml", @"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            File.Copy(@"c:\FuturaData\TCC\conexoes\conexao2.xml", @"c:\FuturaData\TCC\conexao.xml", true);
            this.Close();
        }

        private void btnIniciarEmpresaSistema3_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"c:\FuturaData\TCC\conexoes\conexao.xml"))
            {
                File.Delete(@"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            if (File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                File.Move(@"c:\FuturaData\TCC\conexao.xml", @"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            File.Copy(@"c:\FuturaData\TCC\conexoes\conexao3.xml", @"c:\FuturaData\TCC\conexao.xml", true);
            this.Close();
        }

        private void btnIniciarEmpresaSistema4_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"c:\FuturaData\TCC\conexoes\conexao.xml"))
            {
                File.Delete(@"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            if (File.Exists(@"c:\FuturaData\TCC\conexao.xml"))
            {
                File.Move(@"c:\FuturaData\TCC\conexao.xml", @"c:\FuturaData\TCC\conexoes\conexao.xml");
            }
            File.Copy(@"c:\FuturaData\TCC\conexoes\conexao4.xml", @"c:\FuturaData\TCC\conexao.xml", true);
            this.Close();
        }
    }//fim classe
}//fim namespace
