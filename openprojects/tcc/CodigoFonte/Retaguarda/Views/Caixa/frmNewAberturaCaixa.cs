using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC;
using DllFuturaDataCriptografia;
using DllFuturaDataTCC.Utilitarios;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Controllers;

namespace FuturaDataTCC.Views.Caixa
{
    public partial class frmNewAberturaCaixa : Form
    {
        #region Construtor e Variaveis Internas do Form
        frmInicializacao frmInicial;
        clsNewContasMatematicas newContas = new clsNewContasMatematicas();
        iConCaixa controlCaixa = new iConCaixa();
        public frmNewAberturaCaixa(frmInicializacao frmIni, bool mostrarLabel, string valorAbertura)
        {
            InitializeComponent();
            frmInicial = frmIni;
            
            tbxNomePDV.Text = new clsInicializacao().retornaNomeMaquina(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            tbxSerialPDV.Text = new clsInicializacao().retornaNumeroSerieHD();
            
            tbxNumeroUsuario.Text = frmInicial.numeroUsuarioLogado.ToString();
            tbxHorarioAbertura.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            DataTable dt_DadosRetorno = new DataTable();
            //tenta retornar informações do PDV de acordo com o número desse PDV
            tbxIDPDV.Text = "1"; //chumbei ID do PDV para 1 - por que no futuro podemos ter multicaixa...
            DataTable dt_DadosUltimoCaixa = new DataTable();
            controlCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxIDPDV.Text);
            bool retorno = controlCaixa.cObterInformacoesUltimoCaixa();
            if (retorno)
            {
                dt_DadosUltimoCaixa = controlCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
            }

            if (dt_DadosUltimoCaixa.Rows.Count != 0)
            {
                if (dt_DadosUltimoCaixa.Rows[0]["STATUS"].ToString() == "EM ABERTO")
                {
                    lblLabelEmbaixoBotaoAbrirCaixa.Text = "O Ultimo Caixa Aberto nesse PDV " + dt_DadosUltimoCaixa.Rows[0]["MASCARA_CAIXA_INTEIRA"].ToString() + " de " + Convert.ToDateTime(dt_DadosUltimoCaixa.Rows[0]["DIA_HORAABERTURA"].ToString()).ToString("dd/MM/yyyy hh:mm") + " ainda encontra-se em aberto. Não será possível abrir novo caixa até fechar este!";
                    btnAberturaCaixa.Enabled = false;
                }
                else
                {
                    //carrega realmente os Dados do Caixa que será Aberto!
                    string dataCaixa = dt_DadosUltimoCaixa.Rows[0]["DATACAIXA"].ToString();
                    string dataHoje = DateTime.Now.ToString("ddMMyyyy");
                    string sequencialCaixaDiario = "01";
                    int numeroCaixa = Convert.ToInt32(dt_DadosUltimoCaixa.Rows[0]["SEQUENCIALDIARIO"].ToString());
                    if (dataCaixa == dataHoje)
                    {
                        numeroCaixa++;
                    }
                    tbxDataAberturaCaixa.Text = dataHoje;
                    sequencialCaixaDiario = numeroCaixa.ToString();
                    if (sequencialCaixaDiario.Length == 1)
                    {
                        sequencialCaixaDiario = "0" + sequencialCaixaDiario.ToString();
                    }
                    string sequencialGeral = controlCaixa.cObterUltimoSequencialDosCaixas();

                    int seqGeral = Convert.ToInt32(sequencialGeral);
                    seqGeral++;

                    sequencialGeral = seqGeral.ToString();
                    //enfia 0 na frente até dar 
                    while (sequencialGeral.Length < 8)
                    {
                        sequencialGeral = "0" + sequencialGeral;
                    }
                    tbxNumeroCaixaSequencialDiario.Text = sequencialCaixaDiario;
                    tbxNumeroCaixaSequencialGeral.Text = sequencialGeral;

                }
            }//fim do if dt_DAdosUltimoCaixa.Rows.Count !=
            else
            {
                //else quer dizer que NUNCA FOI ABERTO NENHUM CAIXA NESSE PDV... ai gera primeiras informações...
                tbxDataAberturaCaixa.Text = DateTime.Now.ToString("ddMMyyyy");
                tbxNumeroCaixaSequencialDiario.Text = "01";
                tbxNumeroCaixaSequencialGeral.Text = "00000001";
            }



            if (mostrarLabel)
            {
                lblInfo.Visible = true;
                lblInfo.Refresh();
            }

            if (valorAbertura != "")
            {
                tbxValorAbertura.Text = valorAbertura;
            }

        }
        #endregion

        #region Evento do Botao Abertura de Caixa
        private void btnAberturaCaixa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma ter conferido as informações e proceder com Abertura de Caixa?", "FuturaData Business", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                controlCaixa.modCaixa.DataCaixa = tbxDataAberturaCaixa.Text;
                controlCaixa.modCaixa.SeqDiario = tbxNumeroCaixaSequencialDiario.Text;
                controlCaixa.modCaixa.SeqGeral = tbxNumeroCaixaSequencialGeral.Text;
                controlCaixa.modCaixa.Mascara_Caixa_Inteira = tbxIDPDV.Text + "." + tbxDataAberturaCaixa.Text + "." + tbxNumeroCaixaSequencialDiario.Text;
                controlCaixa.modCaixa.ValorAberturaCaixa = Convert.ToDecimal(tbxValorAbertura.Text);

                bool retorno = controlCaixa.cIncluirAbrirCaixa();
                if (retorno)
                {
                    MessageBox.Show("Caixa Aberto com Sucesso. Você pode fazer movimento, recebimentos, estornos e tudo mais. Ao finalizar o movimento hoje, feche o caixa ao fim do dia.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Não foi possível efetuar a Abertura do Caixa.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion        

        #region Evento Leave da TbxValorAbertura
        private void tbxValorAbertura_Leave(object sender, EventArgs e)
        {
            tbxValorAbertura.Text = newContas.newValidaAjustaArredonda4CasasDecimais(tbxValorAbertura.Text);
        }
        #endregion
    }//fim classe
}//fim namespace
