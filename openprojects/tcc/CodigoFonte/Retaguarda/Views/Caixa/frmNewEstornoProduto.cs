using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Controllers;

namespace FuturaDataTCC.Views.Caixa
{
    public partial class frmNewEstornoProduto : Form
    {
        #region Construtor do Form e Variaveis Internas
        frmInicializacao frmInicial;
        clsNewContasMatematicas contas = new clsNewContasMatematicas();
        iConEstoque controlerEstoque = new iConEstoque();
        DataTable dt_ProdutosEstorno = new DataTable();
        int idCaixa;
        string identificacaoCaixa = "";        
        public frmNewEstornoProduto(frmInicializacao frmInicia, int idCaix, string identCaixa)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            idCaixa = idCaix;
            identificacaoCaixa = identCaixa;            
        }
        #endregion

        #region Evento da PictureBox e Label da Ajuda
        private void pctPFAjudaSubProduto_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "Você pode informar dois números: O Primeiro (item) efetuará o estorno de APENAS 1 item da venda. O Segundo (pedido) efetuará o Estorno de Todos os Itens da Venda. No caso do Item, Esse número é seqüencial e cada item da venda, possui seu numero identificando-o. Esse número vem no cupom balcão ou fiscal antes do Código do Fabricante (balcão) ou antes da descrição (fiscal).", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pctPFAjudaSubProduto_Click(null, null);
        }
        #endregion

        #region Evento do Botao Estornar
        private void btnEstornar_Click(object sender, EventArgs e)
        {
            if (tbxInformacoesDevolucao.Text == "")
            {
                MessageBox.Show("Por favor insira um motivo e documente a devolução do produto.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxInformacoesDevolucao.Focus();
            }
            else
            {
                if (tbxNumeroIDProduto.Text != "")
                {
                    string mensagem = "";
                    if (rdbItemVenda.Checked)
                    {
                        mensagem = "Você selecionou o estorno de 1 único produto. Após estornado, o valor será subtraido ao total vendido no caixa.";
                    }
                    else
                    {
                        mensagem = "Você selecionou o estorno de um PEDIDO INTEIRO. Após estornado, o valor será subtraido ao total vendido no caixa.";
                    }


                    if (rdbProdutoVoltaEstoque.Checked)
                    {
                        mensagem = mensagem + " A quantidade dos produtos vendidos será devolvida ao estoque automaticamente. Confirma Operação?";
                    }
                    else
                    {
                        mensagem = mensagem + " A quantidade dos produtos vendidos NÃO SERÁ devolvida ao Estoque. Os Itens ficarão pendentes no Módulo de Compras Aguardando Troca pelo Fornecedor. Confirma Operação?";
                    }



                    if (MessageBox.Show(null, mensagem, "FuturaData Business", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool retorno = true;
                        for (int i = 0; i < dt_ProdutosEstorno.Rows.Count; i++)
                        {
                            int codigoProduto = Convert.ToInt32(dt_ProdutosEstorno.Rows[i]["PK_CODIGOSIST"].ToString());
                            int codigoItemVenda = Convert.ToInt32(dt_ProdutosEstorno.Rows[i]["PK_IDVENDA"].ToString());
                            decimal quantidade = Convert.ToDecimal(dt_ProdutosEstorno.Rows[i]["QUANTIDADE"].ToString());
                            decimal valorTotal = Convert.ToDecimal(dt_ProdutosEstorno.Rows[i]["VALORTOTALITEM"].ToString());

                            controlerEstoque.modEstoque.produto.Pk_Codigo = codigoProduto;
                            controlerEstoque.modEstoque.EntradaOuSaida = 1;
                            controlerEstoque.modEstoque.Qtd = quantidade;
                            if (rdbProdutoVoltaEstoque.Checked)
                            {
                                bool retorno2 = controlerEstoque.cEfetuaMovimentoDoEstoque();
                            }
                            

                            //lancar na tabela movimentacao
                            controlerEstoque.modEstoque.orcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                            controlerEstoque.modEstoque.caixa.IdCaixa = idCaixa;
                            controlerEstoque.modEstoque.cliente.Pk_Codigo = Convert.ToInt32(tbxIDCliente.Text);
                            controlerEstoque.modEstoque.caixa.IdentificacaoCaixa = identificacaoCaixa;
                            controlerEstoque.modEstoque.ValorCusto = 0;
                            controlerEstoque.modEstoque.MargemLucro = 0;
                            controlerEstoque.modEstoque.ValorVenda = valorTotal;
                            controlerEstoque.modEstoque.Qtd = quantidade;
                            controlerEstoque.modEstoque.ValorTotal = valorTotal;
                            controlerEstoque.modEstoque.EntradaOuSaida = 1;
                            controlerEstoque.modEstoque.MaisInfo = "Estorno efetuado pelo Caixa " + idCaixa.ToString() + ": " + tbxInformacoesDevolucao.Text;
                            controlerEstoque.modEstoque.produto.Pk_Codigo = codigoProduto;
                            bool retornoMov = controlerEstoque.cIncluirMovimentacaoEstoque();
                            
                        }//fim for
                        if (retorno)
                        {
                            MessageBox.Show("Estorno do(s) Produto(s) efetuado com Sucesso! Estorno lançado no relatório de movimentação do caixa e lançado ao estoque!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Falha no Estorno do(s) Produto(s)! Verifique no Movimento do Estoque pelo Caixa e eventualmente efetue a baixa manual dos itens não estornados.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }//fim if DialogResult = Yes
                }
            }
        }
        #endregion

        #region Evento Leave da TextBox Numero ID Produto
        private void tbxNumeroIDProduto_Leave(object sender, EventArgs e)
        {            
            if(contas.verificaSeEInteiro(tbxNumeroIDProduto.Text))
            {                
                string tipoPesquisa = "VENDA";
                if (rdbItemVenda.Checked)
                {
                    tipoPesquisa = "PRODUTO";
                }

                controlerEstoque.modEstoque.produto.Pk_Codigo = Convert.ToInt32(tbxNumeroIDProduto.Text);
                bool retorno = controlerEstoque.cObterDadosDeUmProdutoParaEstorno();
                dt_ProdutosEstorno.Rows.Clear();
                dt_ProdutosEstorno.Columns.Clear();
                if (retorno)
                {
                    dt_ProdutosEstorno = controlerEstoque.daoEstoque.Ds_DadosRetorno.Tables[0];
                }

                if (retorno && dt_ProdutosEstorno.Rows.Count != 0)
                {

                    tbxNumeroOrcamento.Text = dt_ProdutosEstorno.Rows[0]["CODIGOORC"].ToString();
                    tbxCliente.Text = dt_ProdutosEstorno.Rows[0]["NOME"].ToString();
                    tbxIDCliente.Text = dt_ProdutosEstorno.Rows[0]["CODIGOCLI"].ToString();
                    
                    tbxValorTotalVenda.Text = dt_ProdutosEstorno.Rows[0]["VALORFINAL"].ToString();
                    if (rdbItemVenda.Checked)
                    {
                        tbxQuantidade.Text = dt_ProdutosEstorno.Rows[0]["QUANTIDADE"].ToString();
                        //tbxQuantidade.ReadOnly = false;
                    }
                    else
                    {
                        //tbxQuantidade.ReadOnly = true;
                    }
                    tbxInformacoesDevolucao.Text = ""; //dt_ProdutosEstorno.Rows[0][""].ToString();
                    tbxInformacoesDevolucao.Focus();
                    tbxValorEstorno.Text = dt_ProdutosEstorno.Rows[0]["VALORFINAL"].ToString();

                    lvwItensOrcamento.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwItensOrcamento.Columns.Clear();

                    lvwItensOrcamento.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("IdVenda", 60, HorizontalAlignment.Left);
                    lvwItensOrcamento.Columns.Add("IdProd", 60, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("CodProdFabr", 60, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("Descricao", 160, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("Aplicacao", 170, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("Qtde", 60, HorizontalAlignment.Center);
                    lvwItensOrcamento.Columns.Add("Valor Final", 70, HorizontalAlignment.Center);

                    decimal valorDosProdutosAEstornar = 0;

                    for (int i = 0; i < dt_ProdutosEstorno.Rows.Count; i++)
                    {
                        lvwItensOrcamento.Items.Add("");
                        if (i % 2 == 0)
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.White;
                        }
                        else
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.WhiteSmoke;
                        }

                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["PK_IDVENDA"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["PK_CODIGOSIST"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["CODIGOPRODFABRIC"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["DESCRICAO"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["APLICACAO"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["QUANTIDADE"].ToString());
                        lvwItensOrcamento.Items[i].SubItems.Add(dt_ProdutosEstorno.Rows[i]["VALORTOTALITEM"].ToString());

                        valorDosProdutosAEstornar = valorDosProdutosAEstornar + Convert.ToDecimal(dt_ProdutosEstorno.Rows[i]["VALORTOTALITEM"].ToString());
                    }//fim do FOR
                    tbxValorEstorno.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorDosProdutosAEstornar.ToString());
                }//fim if retorno     
                else
                {
                    MessageBox.Show("Não foram localizadas informações para efetuar o Estorno", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }//fim if contas
        }

        private void tbxQuantidade_Leave(object sender, EventArgs e)
        {
           
        }

        private void rdbItemVenda_CheckedChanged(object sender, EventArgs e)
        {
            if (tbxNumeroIDProduto.Text != "")
            {
                tbxNumeroIDProduto_Leave(sender, e);
            }
        }

        private void rdbVendaInteira_CheckedChanged(object sender, EventArgs e)
        {
            if (tbxNumeroIDProduto.Text != "")
            {
                tbxNumeroIDProduto_Leave(sender, e);
            }
        }//fim metodo
        #endregion
    }//fim classe
}//fim namespace
