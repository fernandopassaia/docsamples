using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC;
using DllFuturaDataCriptografia;
using DllFuturaDataTCC.Utilitarios;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;
using DllFuturaDataTCC.Controllers;
using FuturaDataTCC.Views.Orcamento;
using FuturaDataTCC.Relatorios.Caixa;
using DllFuturaDataTCC.Models;

namespace FuturaDataTCC.Views.Caixa
{
    public partial class frmNewCaixa : Form
    {
        #region "Atributos (variaveis) do View frmNewFecharVenda"
        frmInicializacao frmInicial;
        frmTelaPrincipal frmTelaPrinc;
        clsValidacaoDeStrings valida = new clsValidacaoDeStrings();
        clsNewContasMatematicas contas = new clsNewContasMatematicas();
        iConOrcamento controlerOrcamento = new iConOrcamento();
        iConCaixa controlerCaixa = new iConCaixa();        
        clsAtivacaoSoftware ativa = new clsAtivacaoSoftware(new clsConexao().recuperaStringConexaoSQLServer());
        iModOrcamento[] arrayOrcamentos;
        bool formCarregado = false;
        string ecf = "";
        iConPlanoContas controlPlano = new iConPlanoContas();
                
        public string caixaExibir = "";
        string idCaixaPdvAtual = "1";
        #endregion

        #region Construtor (inicializador) do Form
        public frmNewCaixa(frmInicializacao frmIni, frmTelaPrincipal telaPr)
        {
            InitializeComponent();
            frmInicial = frmIni;
            frmTelaPrinc = telaPr;            
            carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
            carregaListViewPesquisaOrcamentosEVendas();

            string nomePdv = new clsInicializacao().retornaNomeMaquina(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            DataTable dt_DadosRetorno = new DataTable();


            tbxIdentificacaoCaixa.Text = nomePdv;
            idCaixaPdvAtual = "1";
            
            carregaListViewPesquisaOrcamentosEVendas();
            formCarregado = true;
            carregaCaixa();
        }
        #endregion

        #region **************MÉTODOS***************
        #region Carrega Pedidos do Caixa
        public void abaCarregaPedidosDoCaixa()
        {
            if(tbxPKIDCaixa.Text != "")
            {
                decimal valorBruto = 0;
                decimal valorLiquido = 0;
                controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                bool retorno = controlerCaixa.cObterPedidosFechadosPorCaixa();
                if (retorno)
                {
                    DataTable dt_PedidosFechados = new DataTable();
                    dt_PedidosFechados = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                    lvwVendasEfetivadasPeloCaixa.Items.Clear();
                    lvwVendasEfetivadasPeloCaixa.Columns.Clear();

                    lvwVendasEfetivadasPeloCaixa.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwVendasEfetivadasPeloCaixa.Columns.Add("CodPed", 60, HorizontalAlignment.Left);
                    lvwVendasEfetivadasPeloCaixa.Columns.Add("Data Ped", 120, HorizontalAlignment.Center);
                    
                    lvwVendasEfetivadasPeloCaixa.Columns.Add("Cliente", 225, HorizontalAlignment.Center);
                    
                    lvwVendasEfetivadasPeloCaixa.Columns.Add("ValorBruto", 80, HorizontalAlignment.Right);
                    lvwVendasEfetivadasPeloCaixa.Columns.Add("ValorFinal", 80, HorizontalAlignment.Right);
                    
                    int indiceListView = 0;

                    for (int i2 = dt_PedidosFechados.Rows.Count - 1; i2 > -1; i2--)
                    {
                        lvwVendasEfetivadasPeloCaixa.Items.Add("");
                        if (i2 % 2 == 0)
                        {
                            lvwVendasEfetivadasPeloCaixa.Items[indiceListView].BackColor = Color.White;
                        }
                        else
                        {
                            lvwVendasEfetivadasPeloCaixa.Items[indiceListView].BackColor = Color.WhiteSmoke;
                        }

                        lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["CODIGO"].ToString());
                        lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["DATACAD"].ToString());
                        lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["NOMECLIENTE"].ToString());
                        //lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["NOMEVENDEDOR"].ToString());
                        lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["VALORBRUTOORCT"].ToString());
                        lvwVendasEfetivadasPeloCaixa.Items[indiceListView].SubItems.Add(dt_PedidosFechados.Rows[i2]["VALORFINALPAGO"].ToString());

                        valorBruto = valorBruto + Convert.ToDecimal(dt_PedidosFechados.Rows[i2]["VALORBRUTOORCT"].ToString());
                        valorLiquido = valorLiquido + Convert.ToDecimal(dt_PedidosFechados.Rows[i2]["VALORFINALPAGO"].ToString());
                        indiceListView++;
                    }//fim do FOR
                }//fim do if retorno
                tbxFechtVendasValorBruto.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorBruto.ToString());
                tbxFechtVendasValorLiquido.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorLiquido.ToString());
            }//fim do tbxPKIDCaixa
        }
        #endregion

        #region Carrega Produtos Vendidos no Caixa
        public void abaCarregaProdutosDoCaixa()
        {
            if (tbxPKIDCaixa.Text != "")
            {
                controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                bool retorno = controlerCaixa.cObterMovimentacaoEstoquePorIDCaixa();
                if (retorno)
                {
                    DataTable dt_ProdutosVendidos = new DataTable();
                    dt_ProdutosVendidos = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                    lvwMovimentacaoEstoque.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwMovimentacaoEstoque.Columns.Clear();

                    lvwMovimentacaoEstoque.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwMovimentacaoEstoque.Columns.Add("IDMov", 60, HorizontalAlignment.Left);
                    lvwMovimentacaoEstoque.Columns.Add("Cod.Ped", 70, HorizontalAlignment.Left);
                    lvwMovimentacaoEstoque.Columns.Add("Cod.Prod", 70, HorizontalAlignment.Left);
                    lvwMovimentacaoEstoque.Columns.Add("CodFabr", 80, HorizontalAlignment.Left);
                    lvwMovimentacaoEstoque.Columns.Add("Descricao", 315, HorizontalAlignment.Center);
                    lvwMovimentacaoEstoque.Columns.Add("Qtd Vendida", 74, HorizontalAlignment.Right);
                    lvwMovimentacaoEstoque.Columns.Add("ES", 30, HorizontalAlignment.Right);                    
                    lvwMovimentacaoEstoque.Columns.Add("Valor Total", 70, HorizontalAlignment.Right);                    

                    int indiceListView = 0;

                    for (int i2 = dt_ProdutosVendidos.Rows.Count - 1; i2 > -1; i2--)
                    {
                        lvwMovimentacaoEstoque.Items.Add("");
                        if (dt_ProdutosVendidos.Rows[i2]["TIPO_ES"].ToString() == "1")
                        {
                            lvwMovimentacaoEstoque.Items[indiceListView].BackColor = Color.LightCoral;
                        }
                        else
                        {
                            if (i2 % 2 == 0)
                            {
                                lvwMovimentacaoEstoque.Items[indiceListView].BackColor = Color.White;
                            }
                            else
                            {
                                lvwMovimentacaoEstoque.Items[indiceListView].BackColor = Color.WhiteSmoke;
                            }
                        }

                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["PK_ID"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["FK_CODIGOPEDIDO"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["FK_CODIGOPRODUTO"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["CODIGOFABRICANTE"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["DESCRICAOPROD"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["QUANTIDADE"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["TIPO_ES"].ToString());
                        lvwMovimentacaoEstoque.Items[indiceListView].SubItems.Add(dt_ProdutosVendidos.Rows[i2]["VALORTOTAL"].ToString());
                        indiceListView++;
                    }//fim do FOR
                }//fim do if retorno
            }//fim do tbxPKIDCaixa
        }
        #endregion

        #region Carrega Aba de movimentacao financeira do Caixa
        public void abaCarregaMovimentacaoFinanceiraDoCaixa()
        {
            if (tbxPKIDCaixa.Text != "")
            {
                DataTable dt_VendasAVista = new DataTable();
                DataTable dt_Estornos = new DataTable();

                DataTable dt_TotaisPorPlanoContas = new DataTable();
                dt_TotaisPorPlanoContas.Columns.Add("FormaPagto");
                dt_TotaisPorPlanoContas.Columns.Add("TipoForma");
                dt_TotaisPorPlanoContas.Columns.Add("ValorTotal");

                controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                bool retornoVista = controlerCaixa.cObterMovimentacaoFinanceiraPorIDCaixa();
                dt_VendasAVista = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                bool retornoEstorno = controlerCaixa.cObterMovimentacaoEstoquePorIDCaixa();
                dt_Estornos = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                
                if (retornoVista && retornoEstorno)
                {   
                    lvwEntradasAVista.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwEntradasAVista.Columns.Clear();
                    lvwEstornosCaixa.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwEstornosCaixa.Columns.Clear();

                    lvwValoresTotaisPorPlanoDeContas2.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwValoresTotaisPorPlanoDeContas2.Columns.Clear();
                    
                    lvwEntradasAVista.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwEntradasAVista.Columns.Add("IDMov", 60, HorizontalAlignment.Left);
                    lvwEntradasAVista.Columns.Add("Cod.Ped", 70, HorizontalAlignment.Left);
                    lvwEntradasAVista.Columns.Add("Cod.Cli", 70, HorizontalAlignment.Left);
                    lvwEntradasAVista.Columns.Add("Forma Pagto", 140, HorizontalAlignment.Left);
                    lvwEntradasAVista.Columns.Add("Numero Doc", 80, HorizontalAlignment.Left);
                    lvwEntradasAVista.Columns.Add("Data Emissao", 115, HorizontalAlignment.Right);
                    lvwEntradasAVista.Columns.Add("Data Vecto", 115, HorizontalAlignment.Right);
                    lvwEntradasAVista.Columns.Add("Valor", 74, HorizontalAlignment.Right);
                    lvwEntradasAVista.Columns.Add("Mais Info", 280, HorizontalAlignment.Left);

                    lvwEstornosCaixa.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwEstornosCaixa.Columns.Add("IDMov", 60, HorizontalAlignment.Left);
                    lvwEstornosCaixa.Columns.Add("IDVenda", 70, HorizontalAlignment.Left);
                    lvwEstornosCaixa.Columns.Add("DataEstorno", 110, HorizontalAlignment.Left);
                    lvwEstornosCaixa.Columns.Add("Qtd", 70, HorizontalAlignment.Left);
                    lvwEstornosCaixa.Columns.Add("Valor Estorno", 80, HorizontalAlignment.Left);
                    lvwEstornosCaixa.Columns.Add("Motivo Estorno", 180, HorizontalAlignment.Right);
                    lvwEstornosCaixa.Columns.Add("IDProd", 80, HorizontalAlignment.Right);
                    lvwEstornosCaixa.Columns.Add("CodFabr", 110, HorizontalAlignment.Right);
                    lvwEstornosCaixa.Columns.Add("Descricao", 180, HorizontalAlignment.Right);                    
                    int indiceListView = 0;
                    decimal valorAVista = 0;
                    decimal valorEstornado = 0;


                    lvwValoresTotaisPorPlanoDeContas2.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwValoresTotaisPorPlanoDeContas2.Columns.Add("Plano de Contas", 120, HorizontalAlignment.Left);
                    lvwValoresTotaisPorPlanoDeContas2.Columns.Add("Valor Vendido", 80, HorizontalAlignment.Right);


                    for (int i2 = dt_VendasAVista.Rows.Count - 1; i2 > -1; i2--)
                    {
                        lvwEntradasAVista.Items.Add("");
                        if (i2 % 2 == 0)
                        {
                            lvwEntradasAVista.Items[indiceListView].BackColor = Color.White;
                        }
                        else
                        {
                            lvwEntradasAVista.Items[indiceListView].BackColor = Color.WhiteSmoke;
                        }

                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["PK_ID"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["FK_CODIGOPEDIDO"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["FK_CODIGOCLIENTE"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["DESCRICAO_SUBCATEGORIA"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["NUMERO_FATURA"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["DATAEMISSAO"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["DATAVENCIMENTO"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["VALORFATURA"].ToString());
                        lvwEntradasAVista.Items[indiceListView].SubItems.Add(dt_VendasAVista.Rows[i2]["MAIS_INFORMACOES"].ToString());
                        indiceListView++;
                        valorAVista = valorAVista + Convert.ToDecimal(dt_VendasAVista.Rows[i2]["VALORFATURA"].ToString());

                        
                        //ADICIONA O VALOR DESSE ITEM NO DATATABLE DE TOTAIS POR PLANO DE CONTAS
                        bool encontradoDt_PlanoContas = false;
                        int indiceEncontrado = 0;
                        for (int i = 0; i < dt_TotaisPorPlanoContas.Rows.Count; i++)
                        {
                            if (dt_TotaisPorPlanoContas.Rows[i]["FormaPagto"].ToString() == dt_VendasAVista.Rows[i2]["DESCRICAO_SUBCATEGORIA"].ToString())
                            {
                                encontradoDt_PlanoContas = true;
                                indiceEncontrado = i;
                            }
                        }


                        if (encontradoDt_PlanoContas)
                        {
                            decimal valorAdicionar = Convert.ToDecimal(dt_VendasAVista.Rows[i2]["VALORFATURA"].ToString());
                            decimal valorNoDataTable = Convert.ToDecimal(dt_TotaisPorPlanoContas.Rows[indiceEncontrado]["ValorTotal"].ToString());
                            decimal novoValor = valorAdicionar + valorNoDataTable;
                            dt_TotaisPorPlanoContas.Rows[indiceEncontrado]["ValorTotal"] = contas.newValidaAjustaArredonda4CasasDecimais(novoValor.ToString());
                        }
                        else
                        {
                            DataRow DR = dt_TotaisPorPlanoContas.NewRow();
                            DR["FormaPagto"] = dt_VendasAVista.Rows[i2]["DESCRICAO_SUBCATEGORIA"].ToString();
                            DR["TipoForma"] = "VISTA";
                            DR["ValorTotal"] = dt_VendasAVista.Rows[i2]["VALORFATURA"].ToString();
                            dt_TotaisPorPlanoContas.Rows.Add(DR);
                            DR = null;
                        }
                    }

                    int indiceListView4 = 0;                    
                    for (int i2 = dt_Estornos.Rows.Count - 1; i2 > -1; i2--)
                    {
                        if (dt_Estornos.Rows[i2]["TIPO_ES"].ToString() == "1")
                        {
                            lvwEstornosCaixa.Items.Add("");
                            if (i2 % 2 == 0)
                            {
                                lvwEstornosCaixa.Items[indiceListView4].BackColor = Color.White;
                            }
                            else
                            {
                                lvwEstornosCaixa.Items[indiceListView4].BackColor = Color.WhiteSmoke;
                            }

                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["PK_ID"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["PK_ID"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["DATAMOVIMENTACAO"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["QUANTIDADE"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["VALORVENDA"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["MAISINFO"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["FK_CODIGOPRODUTO"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["CODIGOFABRICANTE"].ToString());
                            lvwEstornosCaixa.Items[indiceListView4].SubItems.Add(dt_Estornos.Rows[i2]["DESCRICAOPROD"].ToString());
                            valorEstornado = valorEstornado + Convert.ToDecimal(dt_Estornos.Rows[i2]["VALORTOTAL"].ToString());
                            indiceListView4++;
                        }
                    }//fim do FOR
                    tbxAbaMovValorAVista.Text = contas.newValidaAjustaArredonda4CasasDecimais(valorAVista.ToString());
                    tbxAbaMovValorTotalEstornos.Text = contas.newValidaAjustaArredonda4CasasDecimais(valorEstornado.ToString());
                    
                    //TEXTBOX que estão no ultimo quadro do fechamento...
                    tbxFechtVendasAVista.Text = tbxAbaMovValorAVista.Text;                    
                    tbxFechtEstornos.Text = tbxAbaMovValorTotalEstornos.Text;


                    int indiceListView5 = 0;

                    for (int i2 = dt_TotaisPorPlanoContas.Rows.Count - 1; i2 > -1; i2--)
                    {                        
                        lvwValoresTotaisPorPlanoDeContas2.Items.Add("");
                        if (i2 % 2 == 0)
                        {                            
                            lvwValoresTotaisPorPlanoDeContas2.Items[indiceListView5].BackColor = Color.White;
                        }
                        else
                        {                            
                            lvwValoresTotaisPorPlanoDeContas2.Items[indiceListView5].BackColor = Color.WhiteSmoke;
                        }
                        
                        lvwValoresTotaisPorPlanoDeContas2.Items[indiceListView5].SubItems.Add(dt_TotaisPorPlanoContas.Rows[i2]["FormaPagto"].ToString());
                        lvwValoresTotaisPorPlanoDeContas2.Items[indiceListView5].SubItems.Add(dt_TotaisPorPlanoContas.Rows[i2]["ValorTotal"].ToString());
                        indiceListView5++;
                    }


                    grfTotaisPlanoContasPizza2.Visible = true;
                    grfTotaisPlanoContasPizza2.DataSource = dt_TotaisPorPlanoContas;
                    grfTotaisPlanoContasPizza2.DataBind();
                }//fim do if retorno
            }//fim do tbxPKIDCaixa
        }
        #endregion
        
        #region Método que Carrega a ABA Fodástica do Fechamento de Caixa
        //que comece a programação cabulosa By Fer Chuck Norris 04/11/2013 10:58
        public void abaCarregaFechamentoCaixa()
        {
            tbxFechtEstornos.Text = "0,0000";
            tbxFechtQuantidadeItensVendidos.Text = "0,0000";
            tbxFechtReforcos.Text = "0,0000";
            tbxFechtSangrias.Text = "0,0000";
            tbxFechtValorAbertura.Text = "0,0000";
            tbxFechtValorFechamentoParcial.Text = "0,0000";
            tbxFechtVendasAVista.Text = "0,0000";

            tbxFechtValorAbertura.Text = tbxValorAbertura.Text;

            #region Carrega os Valores Vendidos por Forma de Pagamento (A Vista, a Receber, a Faturar)
            decimal valorTotalAVista = 0;

            controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
            DataTable dt_VendasAVista = new DataTable();            
            bool retornoVista = controlerCaixa.cObterMovimentacaoFinanceiraPorIDCaixa();
            dt_VendasAVista = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
            

            for (int i = 0; i < dt_VendasAVista.Rows.Count; i++)
            {
                valorTotalAVista = valorTotalAVista + Convert.ToDecimal(dt_VendasAVista.Rows[i]["VALORBRUTO"].ToString());
            }
            
            //atribui todos os valores as TextBox...
            tbxFechtVendasAVista.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorTotalAVista.ToString());            
            #endregion

            #region Carrega as Quantidades De Itens Vendidos e Estornados
            controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
            bool retornoEst = controlerCaixa.cObterMovimentacaoEstoquePorIDCaixa();
            if (retornoEst)
            {
                DataTable dt_ProdutosVendidos = new DataTable();
                dt_ProdutosVendidos = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                decimal quantidadeVendida = 0;
                decimal quantidadeEstornada = 0;

                for (int i = 0; i < dt_ProdutosVendidos.Rows.Count; i++)
                {
                    int entradaSaida = Convert.ToInt32(dt_ProdutosVendidos.Rows[i]["TIPO_ES"].ToString());
                    if (entradaSaida == 1) //1 = ENTRADA (estorno)
                    {
                        quantidadeEstornada = quantidadeEstornada + Convert.ToDecimal(dt_ProdutosVendidos.Rows[i]["QUANTIDADE"].ToString());
                    }//fim do IF
                    else
                    {
                        quantidadeVendida = quantidadeVendida + Convert.ToDecimal(dt_ProdutosVendidos.Rows[i]["QUANTIDADE"].ToString());
                    }//fim do else
                }//fim do for

                tbxFechtQuantidadeItensEstornados.Text = contas.newValidaAjustaArredonda2CasasDecimais(quantidadeEstornada.ToString());
                tbxFechtQuantidadeItensVendidos.Text = contas.newValidaAjustaArredonda2CasasDecimais(quantidadeVendida.ToString());
            }//fim retornoEst
            #endregion

            abaCarregaMovimentacaoFinanceiraDoCaixa(); //carrega as tbx de Venda a Vista, Prazo, Receber e Estornados...

            //if (formCarregado2) //fiz essa gambi firmezinha por uqe estava entrando em loop
            //{
            //    carregaCaixa(); //carrega pra verificar se já não houve valor de FECHAMENTO do caixa... se foi fechado...
            //}
            //calcula os TOTAIS do Fechamento do Caixa...
            decimal abertura = Convert.ToDecimal(tbxFechtValorAbertura.Text);
            decimal reforcos = Convert.ToDecimal(tbxFechtReforcos.Text);
            decimal sangrias = Convert.ToDecimal(tbxFechtSangrias.Text);
            decimal estornos = Convert.ToDecimal(tbxFechtEstornos.Text);
            decimal valorLiquido = Convert.ToDecimal(tbxFechtVendasValorLiquido.Text);
            
            decimal valorFinalFecht = abertura + reforcos - sangrias - estornos + valorLiquido;
            tbxFechtValorFechamentoParcial.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorFinalFecht.ToString());

            if (tbxFechtValorFechado.Text == "0,00" || tbxFechtValorFechado.Text == "0,0000")
            {
                tbxFechtValorFechado.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorFinalFecht.ToString());
            }
            //salva as informacoes na Tabela de Caixa...
        }//fim metodo
        #endregion

        #region Carrega Caixa por Periodo
        public void carregaCaixa()
        {
            if (formCarregado)
            {
                if (tbxPKIDCaixa.Text == "")
                {
                    tbxPKIDCaixa.Text = controlerCaixa.cObterIDUltimoCaixa();
                }
                //carrega as informações do Caixa de Acordo com o PK do Caixa...
                if (tbxPKIDCaixa.Text != "")
                {
                    controlerCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                    bool retorno = controlerCaixa.cObterInformacoesSobreCaixaPelaPK();
                    DataTable dt_DadosCaixa = new DataTable();
                    if (retorno)
                    {
                        dt_DadosCaixa = controlerCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
                    }

                    if (dt_DadosCaixa.Rows.Count != 0)
                    {
                        tbxPKIDCaixa.Text = dt_DadosCaixa.Rows[0]["PK"].ToString();
                        tbxIdentificacaoCaixa.Text = dt_DadosCaixa.Rows[0]["MASCARA_CAIXA_INTEIRA"].ToString();
                        tbxHorarioAbertura.Text = Convert.ToDateTime(dt_DadosCaixa.Rows[0]["DIA_HORAABERTURA"].ToString()).ToString("dd/MM/yyyy hh:mm");
                        tbxValorAbertura.Text = dt_DadosCaixa.Rows[0]["VALOR_ABERTURA"].ToString();
                        string status = dt_DadosCaixa.Rows[0]["STATUS"].ToString();

                        tbxFechtVendasValorLiquido.Text = dt_DadosCaixa.Rows[0]["VENDAS_EFETUADAS_LIQ"].ToString();
                        tbxFechtVendasValorBruto.Text = dt_DadosCaixa.Rows[0]["VENDAS_EFETUADAS_BRU"].ToString();
                        tbxFechtQuantidadeItensVendidos.Text = dt_DadosCaixa.Rows[0]["QTD_ITENSVENDIDOS"].ToString();
                        tbxFechtQuantidadeItensEstornados.Text = dt_DadosCaixa.Rows[0]["QTD_ITENSESTORNADOS"].ToString();
                        tbxFechtEstornos.Text = dt_DadosCaixa.Rows[0]["VALORESTORNOS"].ToString();
                        tbxFechtReforcos.Text = dt_DadosCaixa.Rows[0]["REFORCOS"].ToString();
                        tbxFechtSangrias.Text = dt_DadosCaixa.Rows[0]["SANGRIAS"].ToString();
                        tbxFechtVendasAVista.Text = dt_DadosCaixa.Rows[0]["VENDA_AVISTA"].ToString();                        
                        tbxFechtValorFechamentoParcial.Text = dt_DadosCaixa.Rows[0]["FECHAMENTO"].ToString();
                        tbxFechtValorFechado.Text = dt_DadosCaixa.Rows[0]["FECHAMENTO"].ToString();

                        if (status == "CAIXA ABERTO")
                        {
                            lblInformacoesDeCaixa.Text = "O Caixa Atual Encontra-se Em Aberto. Você pode efetuar Movimentações, Receber Pedido, Estornar Itens e outros...";
                            lblInformacoesDeCaixa.ForeColor = Color.DeepSkyBlue;
                            btnAberturaCaixa.Enabled = false;
                            btnFechamentoCaixa.Enabled = true;

                            btnEstorno.Enabled = true;
                            btnFechamentoCaixa.Enabled = true;
                        }
                        else
                        {
                            lblInformacoesDeCaixa.Text = "O Caixa Atual Encontra-se Fechado. Utilize as informações apenas para Visualização...";
                            lblInformacoesDeCaixa.ForeColor = Color.Orange;
                            btnAberturaCaixa.Enabled = true;
                            btnFechamentoCaixa.Enabled = false;

                            btnEstorno.Enabled = true;
                            btnFechamentoCaixa.Enabled = false;
                            //btnRelatorioAnaliticoTotal.Enabled = true;
                        }

                        abaCarregaPedidosDoCaixa();
                        abaCarregaProdutosDoCaixa();
                        abaCarregaMovimentacaoFinanceiraDoCaixa();
                        abaCarregaFechamentoCaixa();
                    }
                }
                else
                {
                    MessageBox.Show("Desculpe mas não há informações de nenhum Caixa para ser carregada. Inicie um Novo e Primeiro Caixa!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }//fim formCarregado
        }
        #endregion

        #region Carrega Orcamentos e Ordens de Servico em Aberto

        public void carregaOrcamentosEOrdensETrataEmAbertoEVendidos()
        {
            arrayOrcamentos = controlerOrcamento.cObterOrcamentosApenasEmAberto();
        }

        #endregion Carrega Orcamentos e Ordens de Servico em Aberto
        #endregion
        
        #region **************EVENTOS***************

        #region Método ListView de PESQUISA

        public void carregaListViewPesquisaOrcamentosEVendas()
        {
            #region Cria e Formata as Colunas dentro do ListView
            lvwCotacoesEmAberto.Items.Clear(); //limpo o ListView para mostrar nova consulta
            lvwCotacoesEmAberto.Columns.Clear();

            lvwCotacoesEmAberto.Columns.Add("", 0, HorizontalAlignment.Center);
            lvwCotacoesEmAberto.Columns.Add("NumCot", 80, HorizontalAlignment.Left);
            lvwCotacoesEmAberto.Columns.Add("Data", 150, HorizontalAlignment.Center);
            lvwCotacoesEmAberto.Columns.Add("IdCliente", 80, HorizontalAlignment.Left);
            lvwCotacoesEmAberto.Columns.Add("Nome do Cliente", 280, HorizontalAlignment.Left);
            lvwCotacoesEmAberto.Columns.Add("Valor Final", 100, HorizontalAlignment.Right);
            lvwCotacoesEmAberto.Columns.Add("Status", 0, HorizontalAlignment.Left);            
            #endregion Cria e Formata as Colunas dentro do ListView

            #region Faz o Update Dentro do ListView

            lvwCotacoesEmAberto.BeginUpdate();
            int indiceListView = 0;
            foreach (iModOrcamento item in arrayOrcamentos)
            {
                lvwCotacoesEmAberto.Items.Add("");
                if (indiceListView % 2 == 0)
                {
                    lvwCotacoesEmAberto.Items[indiceListView].BackColor = Color.White;
                }
                else
                {
                    lvwCotacoesEmAberto.Items[indiceListView].BackColor = Color.WhiteSmoke;
                }
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.PkCodigo.ToString());                
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.DataOrc.ToString());
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.cliente.Pk_Codigo.ToString());
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.cliente.Nome.ToString());
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.ValorFinal.ToString());
                lvwCotacoesEmAberto.Items[indiceListView].SubItems.Add(item.Status.ToString());
                indiceListView++;
            }
            lvwCotacoesEmAberto.EndUpdate();

            #endregion Faz o Update Dentro do ListView
        }

        #endregion Método ListView de PESQUISA

        #region Evento do Botao Abertura de Caixa
        private void btnAberturaCaixa_Click(object sender, EventArgs e)
        {
            frmNewAberturaCaixa abertCaixa = new frmNewAberturaCaixa(this.frmInicial, false, "");
            abertCaixa.ShowDialog();
            tbxPKIDCaixa.Text = "";
            carregaCaixa();
        }
        #endregion

        #region Evento das DataPick de Data Inicial e Final das Cotacoes e Vendas
        private void dtpCotacoesDataInicial_ValueChanged(object sender, EventArgs e)
        {
            carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
            carregaListViewPesquisaOrcamentosEVendas();
        }

        private void dtpCotacoesDataFinal_ValueChanged(object sender, EventArgs e)
        {
            carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
            carregaListViewPesquisaOrcamentosEVendas();
        }
        #endregion

        #region Evento SelectedIndexChanged e ValueChanged das ComboBox de terminal e data
        private void cbbTerminalPDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCaixa();
        }

        private void dtpDataMovimento_ValueChanged(object sender, EventArgs e)
        {
            carregaCaixa();
        }
        #endregion

        #region Evento cbbPeriodoCaixa Selected Index Changed (abre outro caixa na tela)
        private void cbbPeriodoCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbbPeriodoCaixa.Text != "")
            //{
            //    tbxPKIDCaixa.Text = cbbPeriodoCaixa.SelectedValue.ToString();
            //    carregaInformacoesCaixa();
            //}
        }
        #endregion

        #region Evento DoubleClick que chama o Form de Fechar Vendas ou Abre uma Cotação
        private void lvwCotacoesEmAberto_DoubleClick(object sender, EventArgs e)
        {
            if (tbxPKIDCaixa.Text != "")
            {                
                string cod_Orc = lvwCotacoesEmAberto.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                string nomeCliente = lvwCotacoesEmAberto.SelectedItems[0].SubItems[4].Text.ToString().Trim();
                string idCliente = lvwCotacoesEmAberto.SelectedItems[0].SubItems[3].Text.ToString().Trim();
                string dataOrc = lvwCotacoesEmAberto.SelectedItems[0].SubItems[2].Text.ToString().Trim();
                string valorFinal = lvwCotacoesEmAberto.SelectedItems[0].SubItems[5].Text.ToString().Trim();
                frmNewFecharVenda fecht = new frmNewFecharVenda(this.frmInicial, Convert.ToInt32(cod_Orc), tbxPKIDCaixa.Text, tbxIdentificacaoCaixa.Text, nomeCliente, valorFinal, dataOrc, idCliente);
                fecht.ShowDialog();
                carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
                carregaListViewPesquisaOrcamentosEVendas();
                //}
                //catch(Exception erro)
                //{
                //    MessageBox.Show(erro.Message.ToString());
                //}
            }
            else
            {
                MessageBox.Show("Não é possível fechar uma venda sem um Caixa Aberto.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lvwVendasEfetivadasPeloCaixa_DoubleClick(object sender, EventArgs e)
        {
            if (tbxPKIDCaixa.Text != "")
            {
                //try
                //{
                string cod_Orc = lvwVendasEfetivadasPeloCaixa.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, frmTelaPrinc, frmInicial.cnpjEmpresa, cod_Orc);
                orct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Não é possível visualizar a Venda.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lvwMovimentacaoEstoque_DoubleClick(object sender, EventArgs e)
        {
            if (tbxPKIDCaixa.Text != "")
            {
                //try
                //{
                string cod_Orc = lvwMovimentacaoEstoque.SelectedItems[0].SubItems[2].Text.ToString().Trim();
                frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, frmTelaPrinc, frmInicial.cnpjEmpresa, cod_Orc);
                orct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Não é possível visualizar a Venda.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lvwEntradasAVista_DoubleClick(object sender, EventArgs e)
        {
            if (tbxPKIDCaixa.Text != "")
            {
                //try
                //{
                string cod_Orc = lvwEntradasAVista.SelectedItems[0].SubItems[2].Text.ToString().Trim();
                frmGestaoOrcamentos orct = new frmGestaoOrcamentos(this.frmInicial, frmTelaPrinc, frmInicial.cnpjEmpresa, cod_Orc);
                orct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Não é possível visualizar a Venda.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Evento do Botao Fechamento de Caixa
        private void btnFechamentoCaixa_Click(object sender, EventArgs e)
        {
            frmNewFechtCaixa fecht = new frmNewFechtCaixa(this.frmInicial, tbxPKIDCaixa.Text, tbxIdentificacaoCaixa.Text, tbxFechtValorFechamentoParcial.Text, ecf);
            fecht.ShowDialog();
            this.Close();
            carregaCaixa();
        }
        #endregion

        #region Evento do SelectedIndexChanged do TabControle
        private void tbcControleCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcControleCaixa.SelectedTab == tbpPedidosFechados)
            {
                abaCarregaPedidosDoCaixa();
            }

            if (tbcControleCaixa.SelectedTab == tbpMovimentoEstoque)
            {
                abaCarregaProdutosDoCaixa();                
            }

            if (tbcControleCaixa.SelectedTab == tbpEntradaFinanceiro)
            {
                abaCarregaMovimentacaoFinanceiraDoCaixa();
            }

            if (tbcControleCaixa.SelectedTab == tbpFechamentos)
            {
                abaCarregaMovimentacaoFinanceiraDoCaixa();
                abaCarregaPedidosDoCaixa();
                abaCarregaFechamentoCaixa();
            }
        }
        #endregion                

        #region Evento do Botão Estorno
        private void btnEstorno_Click(object sender, EventArgs e)
        {
            if (tbxPKIDCaixa.Text != "")
            {
                frmNewEstornoProduto estorno = new frmNewEstornoProduto(this.frmInicial, Convert.ToInt32(tbxPKIDCaixa.Text), tbxIdentificacaoCaixa.Text);
                estorno.ShowDialog();
                abaCarregaProdutosDoCaixa();
            }
        }
        #endregion

        #region Evento das RadioButtons de Mudar o tipo de Gráfico
        private void rdbGraficoPizza_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }

        private void rdbGraficoBarras_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }

        private void rdbGraficoPizza2_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }

        private void rdbGraficoBarras2_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }

        private void rdbGraficoPizzaVistaReceberFaturar_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }

        private void rdbGraficoBarrasVistaReceberFaturar_CheckedChanged(object sender, EventArgs e)
        {
            abaCarregaMovimentacaoFinanceiraDoCaixa();
        }
        #endregion

        #region F5 dentro do Form
        private void tbcControleCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
                carregaListViewPesquisaOrcamentosEVendas();
            }
        }

        private void frmNewCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                carregaOrcamentosEOrdensETrataEmAbertoEVendidos();
                carregaListViewPesquisaOrcamentosEVendas();
            }
        }
        #endregion

        #region Evento do Botao Relatório Analítico
        private void btnRelatorioAnaliticoTotal_Click(object sender, EventArgs e)
        {
            tbcControleCaixa.SelectedTab = tbpFechamentos;
            tbcControleCaixa.Refresh();

            //damw i`m good - God of Thunder - Mr.Gambi - make it possible
            string nomeArquivoGrafico = @"c:\FuturaData\TCC\Exportados\graf" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";
            grfTotaisPlanoContasPizza2.SaveImage(nomeArquivoGrafico, ChartImageFormat.Png);
            Thread.Sleep(500);
            
            frmNewRelatAnalCaixa newRelat = new frmNewRelatAnalCaixa(this.frmInicial, Convert.ToInt32(tbxPKIDCaixa.Text), tbxFechtValorAbertura.Text, tbxFechtEstornos.Text, tbxFechtQuantidadeItensVendidos.Text, tbxFechtQuantidadeItensEstornados.Text, tbxFechtVendasAVista.Text, tbxFechtVendasValorBruto.Text, tbxFechtVendasValorLiquido.Text, tbxFechtValorFechamentoParcial.Text, tbxFechtValorFechado.Text, nomeArquivoGrafico);
            newRelat.ShowDialog();

            try
            {
                Thread.Sleep(500);
                File.Delete(nomeArquivoGrafico);
            }
            catch
            {

            }
        }
        #endregion
        #endregion
    }//fim classe
}//fim namespace