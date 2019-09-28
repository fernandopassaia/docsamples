using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Controllers;

namespace FuturaDataTCC.Views.Caixa
{
    public partial class frmNewFechtCaixa : Form
    {
        #region Construtor e Variaveis Internas do Form
        frmInicializacao frmInicial;
        iConCaixa controlCaixa = new iConCaixa();
        public frmNewFechtCaixa(frmInicializacao frmInicial, string idCaixa, string serialCaixa, string valorFechamento, string marcaECF)
        {
            InitializeComponent();
            this.frmInicial = frmInicial;
            tbxPKIDCaixa.Text = idCaixa;
            tbxIdentificacaoCaixa.Text = serialCaixa;
            tbxValorFechamento.Text = valorFechamento;
            tbxECF.Text = marcaECF;
        }
        #endregion

        #region Evento do Botao Fechamento de Caixa
        private void btnFechamentoCaixa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você confirma realmente o fechamento desse Caixa?", "FuturaData Business", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                controlCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                controlCaixa.modCaixa.ValorFechtCaixa = Convert.ToDecimal(tbxValorFechamento.Text);
                bool retorno = controlCaixa.cEfetuaFechamentoCaixa();
                if (retorno)
                {
                    MessageBox.Show(null, "Caixa Finalizado com Sucesso!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show(null, "Problemas na Finalização do Caixa!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//fim if
        }
        #endregion
    }//fim classe
}//fim namespace
