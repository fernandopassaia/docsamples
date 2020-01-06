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

namespace FuturaDataTCC.Views.PlanoDeContas
{
    public partial class frmCadastroCategoriaPlanoContas : Form
    {
        #region Construtor e Variaveis Internas da Classe
        iConPlanoContas controlPlanoContas = new iConPlanoContas();
        frmInicializacao frmInicial;        
        public frmCadastroCategoriaPlanoContas(frmInicializacao frmIni)
        {
            InitializeComponent();
            frmInicial = frmIni;
            cbbTipoDoMovimento.Text = "1 - Entrada/Receita";
            carregaMascaraDaContaMestre();
        }
        #endregion

        #region Carrega Mascará da Conta Mestre
        public void carregaMascaraDaContaMestre()
        {
            string primeiroNumero = cbbTipoDoMovimento.Text.Substring(0,1);
            DataTable dt_PlanosDeContasExistentes = new DataTable();
            bool retorno = controlPlanoContas.cObterUltimoPlanosDeContaMestresCadastrado();
            if (retorno)
            {
                dt_PlanosDeContasExistentes = controlPlanoContas.daoPlanCont.Ds_DadosRetorno.Tables[0];
            }


            if (dt_PlanosDeContasExistentes.Rows.Count == 0)
            {
                if (primeiroNumero == "1")
                {
                    primeiroNumero = "1.01";
                }
                if (primeiroNumero == "2")
                {
                    primeiroNumero = "2.01";
                }
                tbxMascara.Text = primeiroNumero;
            }//fim do else que verifica se o primeiro numero está vazio...
            else
            {
                int ultimoNumeroCadastrado = Convert.ToInt32(dt_PlanosDeContasExistentes.Rows[0]["MASCARA_PLANO"].ToString().Substring(2,2));
                ultimoNumeroCadastrado++;

                if (ultimoNumeroCadastrado > 99)
                {
                    MessageBox.Show(null, "Seu Plano de Contas Chegou ao Limite Máximo (99 itens). Será necessário procurar um Consultor FuturaData para editar algum número anterior não utilizado - Não é possível inserir mais de 99 Planos de Conta Mestres.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnInserirPlanoContas.Enabled = false;
                }

                string ultimoNumCadastr = ultimoNumeroCadastrado.ToString();
                if (ultimoNumCadastr.Length == 1)
                {
                    ultimoNumCadastr = "0" + ultimoNumCadastr;
                }
                primeiroNumero = primeiroNumero + "." + ultimoNumCadastr;
                tbxMascara.Text = primeiroNumero;
            }
        }
        #endregion

        #region Evento do Botao Inserir Contas
        private void btnInserirPlanoContas_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Atenção: Ao inserir esse plano de Contas, não será possível mais excluir nem alterar o mesmo, visto que contas, pedidos, compras, recebimentos e muitas informações posteriores - estarão amarradas a esse plano. Deseja realmente cadastrar e confirmar todas informações fornecidas?", "FuturaData Business", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                controlPlanoContas.modPlanCont.MascaraPlanoMestre = tbxMascara.Text;
                controlPlanoContas.modPlanCont.DescricaoCategoriaMestre = tbxDescricaoCategoria.Text;
                bool retorno = controlPlanoContas.cIncluirPlanoDeContasMestre();
                if (retorno)
                {
                    MessageBox.Show(null, "Plano de Contas Mestre Inserido com Sucesso!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
        #endregion

        #region Evento SelectedIndexChanged da CbbTipoDoMovimento
        private void cbbTipoDoMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMascaraDaContaMestre();
        }
        #endregion
    }//fim classe
}//fim namespace
