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
    public partial class frmCadastroItemPlanoContas : Form
    {
        #region Construtor e Variaveis Internas do Form
        iConPlanoContas controlerPlanCont = new iConPlanoContas();
        frmInicializacao frmInicial;
        bool formCarregado = false;
        DataTable dt_PlanosMestre = new DataTable();
        string mascaraPlanoAlterar = "";
        public frmCadastroItemPlanoContas(frmInicializacao frmIni, string mascaraPlanoAlter)
        {
            InitializeComponent();
            frmInicial = frmIni;

            bool retorno = controlerPlanCont.cObterTodosPlanosDeContaMestres();
            
            if (retorno == true)
            {
                dt_PlanosMestre = controlerPlanCont.daoPlanCont.Ds_DadosRetorno.Tables[0];
                if (dt_PlanosMestre.Rows.Count > 0)
                {
                    cbbCategoriaMestre.DataSource = dt_PlanosMestre;
                    cbbCategoriaMestre.DisplayMember = "DESC_CBB".Trim().ToString();// +" - " + "DESCRICAO_CATEGORIA".Trim().ToString();
                    cbbCategoriaMestre.ValueMember = "PK_IDPLANO_CONTAS_MESTRE".Trim().ToString();
                    cbbCategoriaMestre.SelectedValue = Convert.ToInt32(1);
                }
                else
                {
                    MessageBox.Show(null, "Você ainda não informou Categorias (Mestre) de Planos de Contas. Por favor informe antes de inserir SubCategorias!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            mascaraPlanoAlterar = mascaraPlanoAlter;
            cbbCategoriaMestre.Refresh();
            cbbCategoriaMestre_SelectedIndexChanged(null, null);
            formCarregado = true;
            if (mascaraPlanoAlterar != "")
            {
                //ai eu enviei algum plano de contas... eu carro as informações e mudo o método pra alterar
                controlerPlanCont.modPlanCont.MascaraSubPlano = tbxMascara.Text;
                bool retornoPlan = controlerPlanCont.cObterSubCategoriaPlanoContasPorCodigo();
                if (retornoPlan)
                {
                    DataTable dt_DadosAlterar = controlerPlanCont.daoPlanCont.Ds_DadosRetorno.Tables[0];
                    if (dt_DadosAlterar.Rows.Count != 0)
                    {
                        btnInserirPlanoContas.Text = "Alterar SubCategoria Plano Conta Entrada/Receita";
                        cbbCategoriaMestre.Enabled = false;

                        cbbCategoriaMestre.SelectedValue = Convert.ToInt32(dt_DadosAlterar.Rows[0]["FK_CATEGORIAMESTRE"]);
                        tbxDescricaoSubCategoria.Text = dt_DadosAlterar.Rows[0]["DESCRICAO_SUBCATEGORIA"].ToString();
                        tbxNomeECF.Text = dt_DadosAlterar.Rows[0]["NOME_ECF"].ToString();
                        tbxResumoNomeECF.Text = dt_DadosAlterar.Rows[0]["NOME_ECFRESUMIDO"].ToString();
                        tbxMascara.Text = dt_DadosAlterar.Rows[0]["MASCARA_SUBCATEGORIA"].ToString();
                        //tbxSt dt_DadosAlterar.Rows[0]["STATUS"].ToString();
                        chkChamarTEF.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["CHAMAR_TEF"].ToString());
                        chkChamarECF.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["CHAMAR_ECF"].ToString());
                        chkChamarNFe.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["CHAMAR_NFE"].ToString());
                        rdbFormaPagamentoAVista.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["FORMA_VENDA_AVISTA"].ToString());
                        rdbFormaPagamentoAPrazo.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["FORMA_VENDA_ARECEBER"].ToString());
                        rdbFormaPagamentoAFaturar.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["FORMA_VENDA_AFATURAR"].ToString());
                        nudNumeroParcelas.Value = Convert.ToDecimal(dt_DadosAlterar.Rows[0]["NUM_PARCELAS"].ToString());
                        nudIntervaloDias.Value = Convert.ToDecimal(dt_DadosAlterar.Rows[0]["INTERVALO_DIAS"].ToString());
                        chkPrimeiraParcelaAVista.Checked = Convert.ToBoolean(dt_DadosAlterar.Rows[0]["PRIMEIRA_PARC_AVISTA"].ToString());
                        nudQuantidadeParcelaSemJuros.Value = Convert.ToDecimal(dt_DadosAlterar.Rows[0]["QTD_PARC_SEMJUROS"].ToString());
                        tbxValorAcrescimoNaForma.Text = dt_DadosAlterar.Rows[0]["ACRESCIMO_FIXO"].ToString();
                        tbxDescontoFixo.Text = dt_DadosAlterar.Rows[0]["DESCONTO_FIXO"].ToString();
                        tbxDiaVencimentoFixo.Text = dt_DadosAlterar.Rows[0]["DIA_VENCT_FIXO"].ToString();
                        string tipoDoc = dt_DadosAlterar.Rows[0]["TIPO_DOC"].ToString();
                    }//fim do if dt_.Rows.Count != 0
                }//fm do IF retornoPlan
            }//fim do numeroPlanoContasCarregar
        }
        #endregion

        #region Carrega Mascará da Conta Mestre
        public void carregaMascaraDaContaSubCategoria()
        {
            if (formCarregado || !formCarregado) //what the hell... Gambi Fire!
            {
                string primeiroNumero = cbbCategoriaMestre.Text.Substring(0, 4);

                if (!formCarregado)
                {
                    primeiroNumero = dt_PlanosMestre.Rows[0]["DESC_CBB"].ToString().Substring(0, 4);
                }

                if (primeiroNumero.StartsWith("1"))
                {
                    lblEntradaOuSaida.Text = "Categoria do tipo Entrada/Receita.";
                    lblEntradaOuSaida.ForeColor = Color.ForestGreen;
                    lblEntradaOuSaida.Refresh();

                    grpOpcoesPagamento.Enabled = true;
                    grpPagamentosPrazo.Enabled = true;
                }

                if (primeiroNumero.StartsWith("2"))
                {
                    lblEntradaOuSaida.Text = "Categoria do tipo Saida/Despesa.  ";
                    lblEntradaOuSaida.ForeColor = Color.DarkRed;
                    lblEntradaOuSaida.Refresh();

                    grpOpcoesPagamento.Enabled = false;
                    grpPagamentosPrazo.Enabled = false;
                }

                DataTable dt_PlanosDeContasExistentes = new DataTable();

                Int32 selectedValue = 1;
                if (!formCarregado)
                {
                    selectedValue = Convert.ToInt32(dt_PlanosMestre.Rows[0]["PK_IDPLANO_CONTAS_MESTRE"].ToString());
                }
                else
                {
                    selectedValue = Convert.ToInt32(cbbCategoriaMestre.SelectedValue);
                }

                controlerPlanCont.modPlanCont.IdCategoriaMestre = selectedValue;
                bool retorno = controlerPlanCont.cObterUltimoPlanosDeSubCategoriaCadastrado();
                if (retorno)
                {
                    dt_PlanosDeContasExistentes = controlerPlanCont.daoPlanCont.Ds_DadosRetorno.Tables[0];
                }


                if (dt_PlanosDeContasExistentes.Rows.Count == 0)
                {
                    primeiroNumero = primeiroNumero + ".001";
                    tbxMascara.Text = primeiroNumero;
                }//fim do else que verifica se o primeiro numero está vazio...

                else
                {
                    int ultimoNumeroCadastrado = Convert.ToInt32(dt_PlanosDeContasExistentes.Rows[0]["MASCARA_SUBCATEGORIA"].ToString().Substring(5, 3));
                    ultimoNumeroCadastrado++;

                    if (ultimoNumeroCadastrado > 999)
                    {
                        MessageBox.Show(null, "Seu Plano de Contas Chegou ao Limite Máximo (99 itens). Será necessário procurar um Consultor FuturaData para editar algum número anterior não utilizado - Não é possível inserir mais de 99 Planos de Conta Mestres.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnInserirPlanoContas.Enabled = false;
                    }

                    string ultimoNumCadastr = ultimoNumeroCadastrado.ToString();
                    if (ultimoNumCadastr.Length == 1)
                    {
                        ultimoNumCadastr = "00" + ultimoNumCadastr;
                    }

                    if (ultimoNumCadastr.Length == 2)
                    {
                        ultimoNumCadastr = "0" + ultimoNumCadastr;
                    }

                    primeiroNumero = primeiroNumero + "." + ultimoNumCadastr;
                    tbxMascara.Text = primeiroNumero;
                }
            }//fim do formCarregado == true
        }
        #endregion

        #region Evento do Botao Inserir Plano de Contas
        private void btnInserirPlanoContas_Click(object sender, EventArgs e)
        {
            string tipoForma = "";
            if (rdbTipoDocBoleto.Checked)
            {
                tipoForma = "BOLETO";
            }

            if (rdbTipoDocCarne.Checked)
            {
                tipoForma = "CARNE";
            }

            if (rdbTipoDocCartaoCredito.Checked)
            {
                tipoForma = "CARTAO_CREDITO";
            }

            if (rdbTipoDocCartaoDebito.Checked)
            {
                tipoForma = "CARTAO_DEBITO";
            }

            if (rdbTipoDocChequeAPrazo.Checked)
            {
                tipoForma = "CHEQUE_PRAZO";
            }

            if (rdbTipoDocChequeAVista.Checked)
            {
                tipoForma = "CHEQUE_VISTA";
            }

            if (rdbTipoDocDinheiro.Checked)
            {
                tipoForma = "DINHEIRO";
            }

            if (rdbTipoDocDuplicata.Checked)
            {
                tipoForma = "DUPLICATA";
            }

            if (rdbTipoDocOutros.Checked)
            {
                tipoForma = "OUTROS";
            }

            if (rdbTipoDocPromissoria.Checked)
            {
                tipoForma = "PROMISSORIA";
            }

            if (rdbTipoDocRecibo.Checked)
            {
                tipoForma = "RECIBO";
            }

            if (rdbTipoDocVales.Checked)
            {
                tipoForma = "VALES";
            }

            bool retorno = true;
            if (mascaraPlanoAlterar == "")
            {
                controlerPlanCont.modPlanCont.DescricaoSubCategoria = tbxDescricaoSubCategoria.Text;
                controlerPlanCont.modPlanCont.MascaraSubPlano = tbxMascara.Text;
                controlerPlanCont.modPlanCont.PlanoMestreSubPlano = Convert.ToInt32(cbbCategoriaMestre.SelectedValue);
                retorno = controlerPlanCont.cIncluirPlanoDeContasSubCategoria();
            }
            

            if (retorno)
            {
                if (mascaraPlanoAlterar == "")
                {
                    MessageBox.Show("Item do Plano de Conta Inserido com Sucesso!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }               
                this.Close();
            }
            else
            {
                MessageBox.Show("Falha ao inserir Item do Plano de Conta!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Evento SelectedIndexChanged da CbbMascara da Conta SubCategoria
        private void cbbCategoriaMestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMascaraDaContaSubCategoria();
        }
        #endregion
    }//fim classe
}//fim namespace
