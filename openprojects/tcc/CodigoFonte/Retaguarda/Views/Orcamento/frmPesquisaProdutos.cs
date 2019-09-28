using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DllFuturaDataContrValidacoes;
using System.Threading;
using DllFuturaDataTCC.Gestoes;
using FuturaDataTCC.Iniciar;

namespace FuturaDataTCC.Views.Orcamento
{
    public partial class frmPesquisaProdutos : Form
    {
        #region Construtor da Classe e Variaveis Internas 
        frmGestaoOrcamentos frmPDV;
        bool aux_Enter = false;
        int codPesquisa = 0;
        bool selctListeView = false;
        bool selectTbxQuant = false;
        bool selectTbxCodigoProd = false;
        bool selectTbxValor = false;
        public string codigoProdutoSimilarSelecionar = "";

        private System.Windows.Forms.Timer m_timer;
        private const int SELECTION_DELAY = 50;       
        clsNewContasMatematicas contas = new clsNewContasMatematicas();
        frmInicializacao frmInicial;
        //DataView Dv;
        //CurrencyManager CM; // " "  
        /// <summary>
        /// Construtor do Form - Precisa passar o Parametro da Pesquisa, se passar 2, é pesquisa por Descrição,
        /// 3 é pesquisa Codigo da Loja, 4 por Código Fabricante, 5 por Código Original
        /// </summary>
        /// <param name="codigoPesq">Codigo (Filtro) de Pesquisa</param>
        /// <param name="frmPdv"></param>

        DataTable dt_FabricantesFiltrados = new DataTable();
        DataTable dt_ModelosFiltrados = new DataTable();
        DataTable dt_FamiliasFiltrados = new DataTable();
        DataTable dt_GruposFiltrados = new DataTable();
        bool formCarregado = false;

        public frmPesquisaProdutos(frmGestaoOrcamentos frmPdv, frmInicializacao frmInicia)
        {
            InitializeComponent();
            frmPDV = frmPdv;
            frmInicial = frmInicia;
            carregaConfigForm();
            //carregaDadosProdutos();
            #region Cria e Formata as Colunas dentro do ListView
            lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
            lvwPesquisaProdutos.Columns.Add("CodigoFabr", 100, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("CodigoOrig", 100, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("Descricao", 340, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("PrecoVenda", 80, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("QdtEstoque", 80, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("Aplicacao", 264, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
            lvwPesquisaProdutos.Columns.Add("DescricaoFilho", 180, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
            lvwPesquisaProdutos.Columns.Add("PrVendaParc", 100, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("PrReVenda", 100, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("PrReVendaParc", 100, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("LocalEstoque", 100, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("CorredorCaixa", 100, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("CodigoOrig2", 100, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("CodigoParal", 100, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("PrecoMinimo", 1, HorizontalAlignment.Right);
            lvwPesquisaProdutos.Columns.Add("PkProduto", 1, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("PkFilho", 60, HorizontalAlignment.Left);
            lvwPesquisaProdutos.Columns.Add("IMG", 40, HorizontalAlignment.Left);
            #endregion
            codPesquisa = 3;            
        }
        #endregion
                
        #region Carrega Config Form
        private void carregaConfigForm()
        {
            
            if (codPesquisa == 3)//descricao
            {
                lblTipoPesquisa.Text = "Pesquisa Por Descrição(F2):";
                lblTipoPesquisa.Refresh();
                pctTipoPesquisa.Image = FuturaDataTCC.Properties.Resources.PesqDescr;
                pctTipoPesquisa.Refresh();
            }

            if (codPesquisa == 4)
            {
                lblTipoPesquisa.Text = "Pesquisa Por Aplicação(F3):";
                lblTipoPesquisa.Refresh();
                pctTipoPesquisa.Image = FuturaDataTCC.Properties.Resources.PesqDescr;
                pctTipoPesquisa.Refresh();
            }

            if (codPesquisa == 5)
            {
                lblTipoPesquisa.Text = "Pesquisa Por Cod.Fabr(F4):";
                lblTipoPesquisa.Refresh();
                pctTipoPesquisa.Image = FuturaDataTCC.Properties.Resources.CodFabr;
                pctTipoPesquisa.Refresh();
            }

            if (codPesquisa == 6)
            {
                lblTipoPesquisa.Text = "Pesquisa Por Cod.Orig(F5):";
                lblTipoPesquisa.Refresh();
                pctTipoPesquisa.Image = FuturaDataTCC.Properties.Resources.CodOrig;
                pctTipoPesquisa.Refresh();
            }

            if (codPesquisa == 7)
            {
                lblTipoPesquisa.Text = "Pesquisa Por C.Barras(F6):";
                lblTipoPesquisa.Refresh();
                pctTipoPesquisa.Image = FuturaDataTCC.Properties.Resources.CodLoja;
                pctTipoPesquisa.Refresh();
            }
        }
        #endregion

        #region Carrega o Filtro de Pesquisa em um DataTable para Melhorar Performance do ListView
        public void carregaFiltroDePesquisa()
        {
            //AI CARALHO - 02/09/2013 19:02 - LÁ VOU EU MEXER NESSE FILTRO CABULOSO (VIDA LOKA) E BOTAR
            //FABRICANTES, MODELOS, FAMILIAS E MARCAS - FERNANDO. BEM VAMOS LÁ!
            //coloquei as comboBox == 0 (TODOS) aqui pra não mexer no filtro antigo... deixa ele como está...
            if (tbxFiltroPesquisa.Text != "")
            {
                lblProdutoAdicionado.Text = "";
                lblProdutoAdicionado.Refresh();
                lvwPesquisaProdutos.Items.Clear(); //limpo o ListView para mostrar nova consulta
                lvwPesquisaProdutos.Columns.Clear();

                #region Cria e Formata as Colunas dentro do ListView
                lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
                lvwPesquisaProdutos.Columns.Add("CodigoFabr", 100, HorizontalAlignment.Left);
                lvwPesquisaProdutos.Columns.Add("CodigoOrig", 100, HorizontalAlignment.Left);
                lvwPesquisaProdutos.Columns.Add("Descricao", 340, HorizontalAlignment.Left);
                lvwPesquisaProdutos.Columns.Add("PrecoVenda", 80, HorizontalAlignment.Right);
                lvwPesquisaProdutos.Columns.Add("QdtEstoque", 80, HorizontalAlignment.Right);
                lvwPesquisaProdutos.Columns.Add("Aplicacao", 264, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
                lvwPesquisaProdutos.Columns.Add("PrecoMinimo", 1, HorizontalAlignment.Right);
                lvwPesquisaProdutos.Columns.Add("PkProduto", 1, HorizontalAlignment.Left);                
                lvwPesquisaProdutos.Columns.Add("IMG", 40, HorizontalAlignment.Left);
                //NA LABEL ABAIXO DO LISTVIEW
                #endregion

                #region Pesquisa == 3 (Descricao)
                if (codPesquisa == 3)//descricao
                {
                    #region Monta o Filtro de Dados e a Pesquisa
                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                    #endregion

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("CodigoFabr");
                    dt_DadosFiltrados.Columns.Add("CodigoOrig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("PrecoVenda");
                    dt_DadosFiltrados.Columns.Add("QtdEstoque");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                    dt_DadosFiltrados.Columns.Add("PkProduto");
                    dt_DadosFiltrados.Columns.Add("IMG");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    //cria o DataTable com o Filtro de Pesquisa
                    for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                    {
                        if (frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString().Length >= tamanhoFiltro)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();                              
                                DR["PrecoMinimo"] = "0,00";
                                DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();                                

                                if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                {
                                    DR["IMG"] = "SIM";
                                }
                                else
                                {
                                    DR["IMG"] = "NAO";
                                }

                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;
                            }
                        }
                    }
                    #endregion

                    //inicia a população do ListView
                    #region Faz o Update Dentro do ListView
                    lvwPesquisaProdutos.BeginUpdate();
                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaProdutos.Items.Add("");
                       
                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }
                      
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());                       
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());                        
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                    gbpResultadoPesquisa.Refresh();
                    // dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==2
                #endregion

                #region Pesquisa == 4 (Aplicacao)
                if (codPesquisa == 4)//loja
                {
                    #region Monta o Filtro de Dados e a Pesquisa
                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                    #endregion

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("CodigoFabr");
                    dt_DadosFiltrados.Columns.Add("CodigoOrig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("PrecoVenda");
                    dt_DadosFiltrados.Columns.Add("QtdEstoque");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                    dt_DadosFiltrados.Columns.Add("PkProduto");
                    dt_DadosFiltrados.Columns.Add("IMG");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    //cria o DataTable com o Filtro de Pesquisa
                    for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                    {
                        if (frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString().Length >= tamanhoFiltro)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                DR["PrecoMinimo"] = "0,00";
                                DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                {
                                    DR["IMG"] = "SIM";
                                }
                                else
                                {
                                    DR["IMG"] = "NAO";
                                }
                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;
                            }
                        }
                    }
                    #endregion

                    #region Faz o Update Dentro do ListView
                    lvwPesquisaProdutos.BeginUpdate();
                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaProdutos.Items.Add("");

                        if (i2 % 2 == 0)
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                        }

                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                    gbpResultadoPesquisa.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==3
                #endregion

                #region Pesquisa == 5 (Codigo do Fabricante)
                if (codPesquisa == 5)//loja
                {
                    #region Monta o Filtro de Dados e a Pesquisa
                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                    #endregion

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("CodigoFabr");
                    dt_DadosFiltrados.Columns.Add("CodigoOrig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("PrecoVenda");
                    dt_DadosFiltrados.Columns.Add("QtdEstoque");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                    dt_DadosFiltrados.Columns.Add("PkProduto");
                    dt_DadosFiltrados.Columns.Add("IMG");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    //cria o DataTable com o Filtro de Pesquisa
                    for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                    {
                        if (frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString().Length >= tamanhoFiltro)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                DR["PrecoMinimo"] = "0,00";
                                DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                {
                                    DR["IMG"] = "SIM";
                                }
                                else
                                {
                                    DR["IMG"] = "NAO";
                                }
                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;

                            }
                        }
                    }
                    #endregion

                    #region Faz o Update Dentro do ListView
                    lvwPesquisaProdutos.BeginUpdate();
                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaProdutos.Items.Add("");

                        if (i2 % 2 == 0)
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                        }

                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                    gbpResultadoPesquisa.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==4
                #endregion             
            }//fim if(tbx=="")

            else //ELSE... Ai quer dizer que selecionaram alguma coisa - ou Fabricante, ou Modelo, ou Família, ou Grupo
            {
                string idFabricante = "0";
                string idModelo = "0";
                string idFamilia = "0";
                string idGrupo = "0";
                //primeiro vamos captar o ID de cada comboBox...
               
                if (tbxFiltroPesquisa.Text != "")
                {
                    lblProdutoAdicionado.Text = "";
                    lblProdutoAdicionado.Refresh();
                    lvwPesquisaProdutos.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwPesquisaProdutos.Columns.Clear();

                    #region Cria e Formata as Colunas dentro do ListView
                    lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwPesquisaProdutos.Columns.Add("CodigoFabr", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("CodigoOrig", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("Descricao", 340, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("PrecoVenda", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("QdtEstoque", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("Aplicacao", 264, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
                    lvwPesquisaProdutos.Columns.Add("PrecoMinimo", 1, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("PkProduto", 1, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("IMG", 40, HorizontalAlignment.Left);
                    //NA LABEL ABAIXO DO LISTVIEW
                    #endregion

                    #region Pesquisa == 3 (Descricao)
                    if (codPesquisa == 3)//descricao
                    {
                        #region Monta o Filtro de Dados e a Pesquisa
                        DataTable dt_DadosFiltrados = new DataTable();
                        int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                        //filtro (palavras digitadas na textbox para o filtro)
                        string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                        #endregion

                        #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                        dt_DadosFiltrados.Columns.Add("CodigoFabr");
                        dt_DadosFiltrados.Columns.Add("CodigoOrig");
                        dt_DadosFiltrados.Columns.Add("Descricao");
                        dt_DadosFiltrados.Columns.Add("PrecoVenda");
                        dt_DadosFiltrados.Columns.Add("QtdEstoque");
                        dt_DadosFiltrados.Columns.Add("Aplicacao");
                        dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                        dt_DadosFiltrados.Columns.Add("PkProduto");
                        dt_DadosFiltrados.Columns.Add("IMG");
                        #endregion

                        #region Varre o DataTable Atual para fazer o Filtro
                        //cria o DataTable com o Filtro de Pesquisa
                        for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString().Length >= tamanhoFiltro)
                            {
                                if (frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                                {
                                    //mais um filtro pra verificar se o produto está na relação de Fabricantes ou Famílias
                                    //Fernando 03/09/2013 08:18 - que treta vishi...kkk
                                    bool produtoLocalizadoEInserir = false;

                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)
                                    string pkIDProdutoAtual = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();
                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)


                                    if (produtoLocalizadoEInserir)
                                    {
                                        DataRow DR = dt_DadosFiltrados.NewRow();
                                        DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                        DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                        DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                        DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                        DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                        DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                        DR["PrecoMinimo"] = "0,00";
                                        DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                        if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                        {
                                            DR["IMG"] = "SIM";
                                        }
                                        else
                                        {
                                            DR["IMG"] = "NAO";
                                        }

                                        dt_DadosFiltrados.Rows.Add(DR);
                                        DR = null;

                                        produtoLocalizadoEInserir = true; //seleciono que o produto já foi inserido pra não inserir novamente!
                                    }//fim do IF Produto Localizado e Inserido
                                }
                            }//FIM DO IF TAMANHO DO FILTRO
                        }
                        #endregion

                        //inicia a população do ListView
                        #region Faz o Update Dentro do ListView
                        lvwPesquisaProdutos.BeginUpdate();
                        for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                        {
                            lvwPesquisaProdutos.Items.Add("");

                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }

                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                        }

                        #endregion

                        lvwPesquisaProdutos.EndUpdate();
                        gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                        gbpResultadoPesquisa.Refresh();
                        // dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                    }//fim if==2
                    #endregion

                    #region Pesquisa == 4 (Aplicacao)
                    if (codPesquisa == 4)//loja
                    {
                        #region Monta o Filtro de Dados e a Pesquisa
                        DataTable dt_DadosFiltrados = new DataTable();
                        int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                        //filtro (palavras digitadas na textbox para o filtro)
                        string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                        #endregion

                        #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                        dt_DadosFiltrados.Columns.Add("CodigoFabr");
                        dt_DadosFiltrados.Columns.Add("CodigoOrig");
                        dt_DadosFiltrados.Columns.Add("Descricao");
                        dt_DadosFiltrados.Columns.Add("PrecoVenda");
                        dt_DadosFiltrados.Columns.Add("QtdEstoque");
                        dt_DadosFiltrados.Columns.Add("Aplicacao");
                        dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                        dt_DadosFiltrados.Columns.Add("PkProduto");
                        dt_DadosFiltrados.Columns.Add("IMG");
                        #endregion

                        #region Varre o DataTable Atual para fazer o Filtro
                        //cria o DataTable com o Filtro de Pesquisa
                        for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString().Length >= tamanhoFiltro)
                            {
                                if (frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                                {
                                    //mais um filtro pra verificar se o produto está na relação de Fabricantes ou Famílias
                                    //Fernando 03/09/2013 08:18 - que treta vishi...kkk
                                    bool produtoLocalizadoEInserir = false;

                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)
                                    string pkIDProdutoAtual = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();
                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)


                                    if (produtoLocalizadoEInserir)
                                    {
                                        DataRow DR = dt_DadosFiltrados.NewRow();
                                        DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                        DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                        DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                        DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                        DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                        DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                        DR["PrecoMinimo"] = "0,00";
                                        DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                        if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                        {
                                            DR["IMG"] = "SIM";
                                        }
                                        else
                                        {
                                            DR["IMG"] = "NAO";
                                        }
                                        dt_DadosFiltrados.Rows.Add(DR);
                                        DR = null;


                                        produtoLocalizadoEInserir = true; //seleciono que o produto já foi inserido pra não inserir novamente!
                                    }//fim do IF Produto Localizado e Inserido
                                }
                            }//FIM DO IF TAMANHO DO FILTRO
                        }
                        #endregion

                        #region Faz o Update Dentro do ListView
                        lvwPesquisaProdutos.BeginUpdate();
                        for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                        {
                            lvwPesquisaProdutos.Items.Add("");

                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }

                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                        }

                        #endregion

                        lvwPesquisaProdutos.EndUpdate();
                        gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                        gbpResultadoPesquisa.Refresh();
                        dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                    }//fim if==3
                    #endregion

                    #region Pesquisa == 5 (Codigo do Fabricante)
                    if (codPesquisa == 5)//loja
                    {
                        #region Monta o Filtro de Dados e a Pesquisa
                        DataTable dt_DadosFiltrados = new DataTable();
                        int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                        //filtro (palavras digitadas na textbox para o filtro)
                        string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                        #endregion

                        #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                        dt_DadosFiltrados.Columns.Add("CodigoFabr");
                        dt_DadosFiltrados.Columns.Add("CodigoOrig");
                        dt_DadosFiltrados.Columns.Add("Descricao");
                        dt_DadosFiltrados.Columns.Add("PrecoVenda");
                        dt_DadosFiltrados.Columns.Add("QtdEstoque");
                        dt_DadosFiltrados.Columns.Add("Aplicacao");
                        dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                        dt_DadosFiltrados.Columns.Add("PkProduto");
                        dt_DadosFiltrados.Columns.Add("IMG");
                        #endregion

                        #region Varre o DataTable Atual para fazer o Filtro
                        //cria o DataTable com o Filtro de Pesquisa
                        for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                        {
                            if (frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString().Length >= tamanhoFiltro)
                            {
                                if (frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString().Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                                {
                                    //mais um filtro pra verificar se o produto está na relação de Fabricantes ou Famílias
                                    //Fernando 03/09/2013 08:18 - que treta vishi...kkk
                                    bool produtoLocalizadoEInserir = false;

                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)
                                    string pkIDProdutoAtual = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();
                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)

                                    if (produtoLocalizadoEInserir)
                                    {
                                        DataRow DR = dt_DadosFiltrados.NewRow();
                                        DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                        DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                        DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                        DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                        DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                        DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                        DR["PrecoMinimo"] = "0,00";
                                        DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                        if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                        {
                                            DR["IMG"] = "SIM";
                                        }
                                        else
                                        {
                                            DR["IMG"] = "NAO";
                                        }
                                        dt_DadosFiltrados.Rows.Add(DR);
                                        DR = null;

                                        produtoLocalizadoEInserir = true; //seleciono que o produto já foi inserido pra não inserir novamente!
                                    }//fim do IF Produto Localizado e Inserido
                                }
                            }//FIM DO IF TAMANHO DO FILTRO
                        }
                        #endregion

                        #region Faz o Update Dentro do ListView
                        lvwPesquisaProdutos.BeginUpdate();
                        for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                        {
                            lvwPesquisaProdutos.Items.Add("");

                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }

                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                        }

                        #endregion

                        lvwPesquisaProdutos.EndUpdate();
                        gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                        gbpResultadoPesquisa.Refresh();
                        dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                    }//fim if==5
                    #endregion
                }//fim if (tbxFiltroPesquisa.Text == "")
                else
                {
                    //primeiro vamos captar o ID de cada comboBox...

                    lblProdutoAdicionado.Text = "";
                    lblProdutoAdicionado.Refresh();
                    lvwPesquisaProdutos.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwPesquisaProdutos.Columns.Clear();

                    #region Cria e Formata as Colunas dentro do ListView
                    lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwPesquisaProdutos.Columns.Add("CodigoFabr", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("CodigoOrig", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("Descricao", 340, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("PrecoVenda", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("QdtEstoque", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("Aplicacao", 264, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
                    lvwPesquisaProdutos.Columns.Add("PrecoMinimo", 1, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("PkProduto", 1, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("IMG", 40, HorizontalAlignment.Left);
                    //NA LABEL ABAIXO DO LISTVIEW
                    #endregion

                    #region Pesquisa == 3 (Descricao)
                    if (null == null)//essa gambiarra foi de propósito - Fer. 07/09/2013 10:21 - é que não queria desfazer a estrutura - talvez seja por que estou ouvindo BobMarley também...
                    {
                        #region Monta o Filtro de Dados e a Pesquisa
                        DataTable dt_DadosFiltrados = new DataTable();
                        int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                        //filtro (palavras digitadas na textbox para o filtro)
                        string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
                        #endregion

                        #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                        dt_DadosFiltrados.Columns.Add("CodigoFabr");
                        dt_DadosFiltrados.Columns.Add("CodigoOrig");
                        dt_DadosFiltrados.Columns.Add("Descricao");
                        dt_DadosFiltrados.Columns.Add("PrecoVenda");
                        dt_DadosFiltrados.Columns.Add("QtdEstoque");
                        dt_DadosFiltrados.Columns.Add("Aplicacao");
                        dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                        dt_DadosFiltrados.Columns.Add("PkProduto");
                        dt_DadosFiltrados.Columns.Add("IMG");
                        #endregion

                        #region Varre o DataTable Atual para fazer o Filtro
                        //cria o DataTable com o Filtro de Pesquisa
                        for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                        {
                            if (1 == 1) //what the hell
                            {
                                if (true == true) //fire gambi, Fire, fire, Fire!
                                {
                                    //mais um filtro pra verificar se o produto está na relação de Fabricantes ou Famílias
                                    //Fernando 03/09/2013 08:18 - que treta vishi...kkk
                                    bool produtoLocalizadoEInserir = false;
                                    string pkIDProdutoAtual = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();
                                    //preciso localizar o produto apenas 1 única vez, em qualquer um dos 4 datatables que estejam
                                    //selecionados, localizando-o... basta inserir no Grid e já era (Preety Cool!)

                                    if (produtoLocalizadoEInserir)
                                    {
                                        DataRow DR = dt_DadosFiltrados.NewRow();
                                        DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                        DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                        DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                        DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                        DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                        DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                        DR["PrecoMinimo"] = "0,00";
                                        DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();

                                        if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                        {
                                            DR["IMG"] = "SIM";
                                        }
                                        else
                                        {
                                            DR["IMG"] = "NAO";
                                        }

                                        dt_DadosFiltrados.Rows.Add(DR);
                                        DR = null;
                                        produtoLocalizadoEInserir = true; //seleciono que o produto já foi inserido pra não inserir novamente!
                                    }//fim do IF Produto Localizado e Inserido
                                }
                            }//FIM DO IF TAMANHO DO FILTRO
                        }
                        #endregion

                        //inicia a população do ListView
                        #region Faz o Update Dentro do ListView
                        lvwPesquisaProdutos.BeginUpdate();
                        for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                        {
                            lvwPesquisaProdutos.Items.Add("");

                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }

                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                            lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                        }

                        #endregion

                        lvwPesquisaProdutos.EndUpdate();
                        gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                        gbpResultadoPesquisa.Refresh();
                        // dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                    }//fim if==2
                    #endregion
                }//fim do else do if (tbxFiltroPesquisa.Text == "")
            }//fim else
        }//fim metodo
        #endregion

        #region Calcula Quantidade vs Valor e Valida Informacoes
        public bool calculaQuantidadeVsValorEValidaInformacoes()
        {
            decimal valorDigitado = Convert.ToDecimal(tbxValorDigitado.Text.Replace(".", ","));
            decimal valorMinimo = 0;
            decimal valorNormal = Convert.ToDecimal(tbxPrecoNormal.Text.Replace(".", ","));
            if (valorDigitado < valorMinimo)
            {
                MessageBox.Show("Você digitou um valor de preço ABAIXO do valor minimo de venda. Não será possível adicionar esse produto a venda. Altere seu valor por um MAIOR que o minimo permitido...", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
                tbxValorDigitado.Focus();
            }
            else
            {
                tbxTipoDescontoAcrescimo.Text = "";
                tbxValorDescontoAcrescimo.Text = "";
                lblProdutoAdicionado.Text = "";

                //lblTotalCedidoDescontosOuAcrescimos.Text = "";
                //lblTotalCedidoDescontosOuAcrescimos.Refresh();
                //chkIgnorarMudancasPrecos.Enabled = false;
                //chkIgnorarMudancasPrecos.Checked = false;
                //chkIgnorarMudancasPrecos.Refresh();
                
                if (!chkIgnorarMudancasPrecos.Checked)
                {
                    if (valorNormal > valorDigitado)
                    {
                        decimal valorNormalTotal = valorNormal * Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                        decimal valorDigitadoTotal = valorDigitado * Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                        decimal mayMay = valorNormalTotal - valorDigitadoTotal;
                        tbxTipoDescontoAcrescimo.Text = "D";
                        tbxValorDescontoAcrescimo.Text = contas.newValidaAjustaArredonda2CasasDecimais(mayMay.ToString());
                        //lblTotalCedidoDescontosOuAcrescimos.ForeColor = Color.Red;
                        //lblTotalCedidoDescontosOuAcrescimos.Text = "Nota: Cedido DESCONTO TOTAL de R$ " + contas.newValidaAjustaArredonda2CasasDecimais(mayMay.ToString()) + ". Para Ignorar Tick 'Ignorar Mudança de Preços'";
                        chkIgnorarMudancasPrecos.Enabled = true;
                        chkIgnorarMudancasPrecos.Checked = false;
                        chkIgnorarMudancasPrecos.Refresh();
                    }
                    if (valorNormal < valorDigitado)
                    {
                        decimal valorNormalTotal = valorNormal * Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                        decimal valorDigitadoTotal = valorDigitado * Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                        decimal mayMay = valorDigitadoTotal - valorNormalTotal;
                        tbxTipoDescontoAcrescimo.Text = "A";
                        tbxValorDescontoAcrescimo.Text = contas.newValidaAjustaArredonda2CasasDecimais(mayMay.ToString());
                        //lblTotalCedidoDescontosOuAcrescimos.ForeColor = Color.Blue;
                        //lblTotalCedidoDescontosOuAcrescimos.Text = "Nota: Inserido Acrescimo TOTAL de R$ " + contas.newValidaAjustaArredonda2CasasDecimais(mayMay.ToString()) + ". Para Ignorar Tick 'Ignorar Mudança de Preços'";
                        chkIgnorarMudancasPrecos.Enabled = true;
                        chkIgnorarMudancasPrecos.Checked = false;
                        chkIgnorarMudancasPrecos.Refresh();
                    }
                }

                decimal valorTotal = valorDigitado * Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                tbxValorTotal.Text = contas.newValidaAjustaArredonda4CasasDecimais(valorTotal.ToString());


                if(contas.verificaSeEDecimal(tbxQuantidade.Text) && contas.verificaSeEDecimal(tbxQuantidadeEstoque.Text))
                {
                    decimal quantidadeDigitada = Convert.ToDecimal(tbxQuantidade.Text.Replace(".", ","));
                    decimal quantidadeEstoque = Convert.ToDecimal(tbxQuantidadeEstoque.Text.Replace(".", ","));

                    if (quantidadeDigitada > quantidadeEstoque)
                    {
                        lblProdutoAdicionado.ForeColor = Color.Red;
                        lblProdutoAdicionado.Text = "Nota: Quantidade digitada maior estoque atual. ";
                        lblProdutoAdicionado.Refresh();
                    }
                }

                return true;
            }
        }
        #endregion

        #region Textbox evento Text Changed
        private void tbxFiltroPesquisa_TextChanged(object sender, EventArgs e)
        {
            //lblStatusProdutoAdd.Text = "";
            carregaFiltroDePesquisa();
        }
        #endregion

        #region ATALHOS FORM 
        public bool teclasAtalho(KeyEventArgs e)
        {
            #region Atalho F2 (Pesquisa por Descricao)

            if ((e.KeyCode == Keys.F2))
            {
                this.tbxFiltroPesquisa.Focus();
                rdbBuscaDescricao.Checked = true;
                rdbBuscaDescricao_CheckedChanged(null, null);
                return true;
            }
            #endregion

            #region Atalho F3 (Pesquisa por Aplicacao)
            if ((e.KeyCode == Keys.F3))
            {
                this.tbxFiltroPesquisa.Focus();
                rdbBuscaCodFabricante.Checked = true;
                rdbBuscaCodFabricante_CheckedChanged(null, null);
                return true;
            }
            #endregion

            #region Atalho F4 (Pesquisa por Fabricante)
            if ((e.KeyCode == Keys.F4))
            {
                this.tbxFiltroPesquisa.Focus();
                rdbBuscaAplicacao.Checked = true;
                rdbBuscaAplicacao_CheckedChanged(null, null);
                return true;
            }
            #endregion
            
            #region Atalho F8 (Finalizar Pesquisa)

            if ((e.KeyCode == Keys.F8))
            {
                this.btnFinalizarPesquisa_Click(null, null);
                return true;
            }
            #endregion

            #region Atalho F9 (Ignorar Desconto Acrescimo)

            if ((e.KeyCode == Keys.F9))
            {
                if (tbxDescricaoAplicacaoEFilho.ReadOnly == true)
                {
                    tbxDescricaoAplicacaoEFilho.ReadOnly = false;
                    tbxDescricaoAplicacaoEFilho.Focus();
                }
                return true;                
            }
            #endregion

            #region Atalho F10 (Foco na TbxQuantidade)

            if ((e.KeyCode == Keys.F10))
            {
                tbxQuantidade.Focus();
                tbxQuantidade.ReadOnly = false;
                tbxValorTotal.ReadOnly = true;
                return true;
            }
            #endregion

            #region Atalho F11 (Chk Ignorar Desconto ou Acrescimo)

            if ((e.KeyCode == Keys.F12))
            {
                if (chkIgnorarMudancasPrecos.Enabled)
                {
                    if (chkIgnorarMudancasPrecos.Checked)
                    {
                        chkIgnorarMudancasPrecos.Checked = false;
                    }
                    else
                    {
                        chkIgnorarMudancasPrecos.Checked = true;
                    }
                }
                return true;
            }
            #endregion

            #region Atalho F12 (Adiciona o Produto)

            if ((e.KeyCode == Keys.F11))
            {
                if (tbxValorDigitado.Text != "" && tbxValorDigitado.Text != "0,00" && tbxValorDigitado.Text != "0,0000")
                {
                    tbxQuantidade.ReadOnly = true;
                    tbxValorTotal.ReadOnly = false;
                    tbxValorTotal.Focus();
                }
                return true;
            }
            #endregion
            
            #region Atalho ENTER função de Tab
            if (aux_Enter.Equals(false))
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                    return true;
                }
            }
            #endregion

            #region Atalho ESC (Finalizar Pesquisa)
            //if ((e.KeyCode == Keys.F8))
            //{
            //    this.btnFinalizarPesquisa_Click(null, null);
            //    return true;
            //}
            #endregion

            return false;
        }
        #endregion

        #region Botoes de Atalho 
        private void btnPesquisaPorCodLoja_Click(object sender, EventArgs e)
        {

        }

        private void frmPesquisaProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            this.teclasAtalho(e);
        }

        private void tbxFiltroPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.lvwPesquisaProdutos.Focus();
                    this.lvwPesquisaProdutos.Select();
                    this.lvwPesquisaProdutos.Items[0].Selected = true;
                    this.lvwPesquisaProdutos.Activation = ItemActivation.OneClick;
                    //lvwPesquisaProdutos_PreviewKeyDown(null, null);
                }


                if (e.KeyCode == Keys.Down)
                {
                    this.lvwPesquisaProdutos.Focus();
                    this.lvwPesquisaProdutos.Select();
                    this.lvwPesquisaProdutos.Items[0].Selected = true;
                    this.lvwPesquisaProdutos.Activation = ItemActivation.OneClick;
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch
            {
                tbxFiltroPesquisa.Focus();
            }
        }
        #endregion

        #region Busca (Radio Buttons Checked Changed 
        
        #endregion

        #region Metodo Valida Campos 
        public bool validarCampos()
        {
            clsNewContasMatematicas validaCampo = new clsNewContasMatematicas();
            bool retorno = true;

            if (tbxCodigoProduto.Text.Equals("") || tbxCodigoProduto.Text.Equals(null))
            {
                MessageBox.Show("Por favor informe um valor válido","FuturaData Business - Pesquisa de Produtos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxQuantidade.Text.Equals("") || tbxQuantidade.Text.Equals(null))
            {
                MessageBox.Show("Por Favor, Informe um valor válido","FuturaData Business - Pesquisa de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (validaCampo.verificaSeEInteiro(tbxCodigoProduto.Text.ToString()).Equals(false))
            {
                MessageBox.Show("Por Favor, Informe Somente Numeros","FuturaData Business - Pesquisa de Produtos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                tbxCodigoProduto.Focus();
                retorno = false;
            }


            return retorno;

        }
        #endregion

        #region Metodo Limpar Campos 
        public void limparCampos()
        {
            tbxCodigoProduto.Text = "";
            tbxQuantidade.Text = "1";
            tbxValorDigitado.Text = "";
            tbxFiltroPesquisa.Text = "";
            lvwPesquisaProdutos.Clear();

        }
        #endregion

        #region Controles Tbx Filtro Pesquisa 
        private void tbxFiltroPesquisa_Enter(object sender, EventArgs e)
        {
            this.aux_Enter = true;
            //lvwPesquisaProdutos_PreviewKeyDown(null, null);
        }

        private void tbxFiltroPesquisa_Leave(object sender, EventArgs e)
        {
            this.aux_Enter = false;
            //lvwPesquisaProdutos_PreviewKeyDown(null, null);
        }
        #endregion

        #region Controles ListView 
        private void lvwPesquisaProdutos_Enter(object sender, EventArgs e)
        {            
            this.selctListeView = true;
            //lvwPesquisaProdutos_DoubleClick(sender, e);
            //AAAAAAHHHH
        }

        private void lvwPesquisaProdutos_Leave(object sender, EventArgs e)
        {
            this.selctListeView = false;
        }

        private void lvwPesquisaProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if(e.KeyCode == Keys.Enter)
            {
                lvwPesquisaProdutos_DoubleClick(null, null);
            }
        }
        #endregion

        #region Controles Tbx Quant 
        private void tbxQuantidade_Enter(object sender, EventArgs e)
        {
            //tbxValorDigitado.Text = "0,0000";
            this.selectTbxQuant = true;
            //tbxQuantidade.Clear();
        }

        private void tbxQuantidade_Leave(object sender, EventArgs e)
        {   
            this.selectTbxQuant = false;

            tbxQuantidade.Text = contas.newValidaAjustaArredonda4CasasDecimais(tbxQuantidade.Text);

            if (contas.verificaSeEDecimal(tbxQuantidade.Text) && contas.verificaSeEDecimal(tbxValorDigitado.Text))
            {
                calculaQuantidadeVsValorEValidaInformacoes();
            }            
        }

        private void tbxQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            //tbxQuantidade_KeyDown
            //se o Cursor estiver no list View
            //if (this.selectTbxQuant.Equals(true))
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (tbxQuantidade.Text != "")
            //        {
            //            this.procuraProcDT();
            //            tbxValorDigitado.Focus();
            //        }
            //    }
            //}
        }
        #endregion

        #region Controles Tbx Valor 
        private void tbxValor_Enter(object sender, EventArgs e)
        {
            this.selectTbxValor = true;
        }

        private void tbxValor_Leave(object sender, EventArgs e)
        {
            this.selectTbxValor = true;
            tbxValorDigitado.Text = contas.newValidaAjustaArredonda4CasasDecimais(tbxValorDigitado.Text);
            calculaQuantidadeVsValorEValidaInformacoes();

        }

        private void tbxValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
        
        #region finalizar pesquisa
        private void btnFinalizarPesquisa_Click(object sender, EventArgs e)
        {
            if (tbxCodigoProduto.Text != "" && contas.verificaSeEDecimal(tbxValorDigitado.Text) && contas.verificaSeEDecimal(tbxQuantidade.Text))
            {
                if (calculaQuantidadeVsValorEValidaInformacoes())
                {
                    DataRow DR = frmPDV.dt_ItensOrcamento.NewRow();

                    //dt_ItensOrcamento.Columns.Add("PK_ID");
                    //dt_ItensOrcamento.Columns.Add("FK_NUMPRODUTO");
                    //dt_ItensOrcamento.Columns.Add("FK_NUMPRODUTOFILHO");
                    //dt_ItensOrcamento.Columns.Add("ITEM");
                    //dt_ItensOrcamento.Columns.Add("CODIGOFABRIC");
                    //dt_ItensOrcamento.Columns.Add("DESCRICAOAPLICACAO");
                    //dt_ItensOrcamento.Columns.Add("LOCALESTOQUE");
                    //dt_ItensOrcamento.Columns.Add("PRECOVENDABANCO");
                    //dt_ItensOrcamento.Columns.Add("VALORTOTAL_SEMDESC_OU_ACRE");
                    //dt_ItensOrcamento.Columns.Add("VALORUNITARIO");
                    //dt_ItensOrcamento.Columns.Add("QUANTIDADE");
                    //dt_ItensOrcamento.Columns.Add("VALORTOTAL");
                    //dt_ItensOrcamento.Columns.Add("DESCONTO");
                    //dt_ItensOrcamento.Columns.Add("ACRESCIMO");
                    //dt_ItensOrcamento.Columns.Add("IGNORAR_MUDANCA_PRECOS");
                    //dt_ItensOrcamento.Columns.Add("DESCONTOPDV");
                    //dt_ItensOrcamento.Columns.Add("ACRESCIMOPDV");

                    int indiceItem = frmPDV.dt_ItensOrcamento.Rows.Count + 1;
                    decimal totalSemDescontosAcrescimos = Convert.ToDecimal(tbxQuantidade.Text) * Convert.ToDecimal(tbxPrecoNormal.Text);

                    DR["PK_ID"] = indiceItem.ToString();
                    DR["FK_NUMPRODUTO"] = tbxCodigoProduto.Text;
                    
                    DR["ITEM"] = indiceItem.ToString();

                    DR["CODIGOFABRIC"] = tbxCodigoFabricante.Text;
                    DR["DESCRICAOAPLICACAO"] = tbxDescricaoAplicacaoEFilho.Text;
                    //DR["LOCALESTOQUE"] = "";

                    DR["PRECOVENDABANCO"] = contas.newValidaAjustaArredonda2CasasDecimais(tbxPrecoNormal.Text);
                    DR["VALORTOTAL_SEMDESC_OU_ACRE"] = contas.newValidaAjustaArredonda2CasasDecimais(totalSemDescontosAcrescimos.ToString());
                    DR["VALORUNITARIO"] = contas.newValidaAjustaArredonda2CasasDecimais(tbxValorDigitado.Text);

                    DR["QUANTIDADE"] = contas.newValidaAjustaArredonda4CasasDecimais(tbxQuantidade.Text);
                    DR["VALORTOTAL"] = contas.newValidaAjustaArredonda2CasasDecimais(tbxValorTotal.Text);
                    
                    if (tbxTipoDescontoAcrescimo.Text == "A" && chkIgnorarMudancasPrecos.Checked == false)
                    {
                        DR["DESCONTO"] = "0,00";
                        DR["ACRESCIMO"] = contas.newValidaAjustaArredonda2CasasDecimais(tbxValorDescontoAcrescimo.Text);                        
                    }

                    if (tbxTipoDescontoAcrescimo.Text == "D" &&  chkIgnorarMudancasPrecos.Checked == false)
                    {
                        DR["DESCONTO"] = contas.newValidaAjustaArredonda2CasasDecimais(tbxValorDescontoAcrescimo.Text);
                        DR["ACRESCIMO"] = "0,00";                        
                    }

                    if (tbxTipoDescontoAcrescimo.Text == "")
                    {
                        DR["DESCONTO"] = "0,00";
                        DR["ACRESCIMO"] = "0,00";                        
                    }
                    else
                    {
                        if (chkIgnorarMudancasPrecos.Checked)
                        {
                            DR["DESCONTO"] = "0,00";
                            DR["ACRESCIMO"] = "0,00";                            
                        }
                    }

                    if (frmPDV.dt_ItensOrcamento.Rows.Count < 99)
                    {
                        frmPDV.dt_ItensOrcamento.Rows.Add(DR);
                    }
                    else
                    {
                        MessageBox.Show("Desculpe, você não pode inserir mais de 100 itens em uma única cotação.", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    tbxQuantidade.Text = "1,0000";
                    tbxValorDigitado.Text = "0,0000";

                    lblProdutoAdicionado.ForeColor = Color.DarkBlue;                    
                    
                    lblProdutoAdicionado.Text = "Produto " + tbxDescricaoAplicacaoEFilho.Text + " adicionado com Sucesso a Cotação!";
                    lblProdutoAdicionado.Refresh();
                    //lblTotalCedidoDescontosOuAcrescimos.Text = "";
                    tbxValorDescontoAcrescimo.Text = "";
                    tbxTipoDescontoAcrescimo.Text = "";
                    chkIgnorarMudancasPrecos.Enabled = false;
                    chkIgnorarMudancasPrecos.Checked = false;
                    chkIgnorarMudancasPrecos.Refresh();
                    tbxDescricaoAplicacaoEFilho.ReadOnly = true;

                    tbxQuantidade.ReadOnly = false;
                    tbxValorTotal.ReadOnly = true;
                    //tbxFiltroPesquisa.Text = "";
                    //tbxFiltroPesquisa.Focus();
                    this.lvwPesquisaProdutos.Focus();
                    this.lvwPesquisaProdutos.Select();
                    this.lvwPesquisaProdutos.Items[0].Selected = true;

                    
                    rdbBuscaDescricao_CheckedChanged(null, null);
                    tbxFiltroPesquisa.Text = "";
                    tbxFiltroPesquisa.Focus();
                    

                    frmPDV.desenhaProdutosDeUmOrcamento();
                }
            }//fim método
            else
            {
                MessageBox.Show("Por favor selecione um produto e informe quantidade e valor antes de adicionar!", "FuturaData PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion                       

        #region LvwPesquisaProdutos
        private void lvwPesquisaProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int tamanhoFiltro = tbxFiltroPesquisa.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
        //    //filtro (palavras digitadas na textbox para o filtro)
        //    string filtro = tbxFiltroPesquisa.Text.ToString().Substring(0, tamanhoFiltro);
        //   for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
        //    {
        //          //string codigoPegou = frmPDV.dt_DadosProdutos.Rows[i]["CODIGO_SIST"].ToString();
        //          //if (codigoPegou.ToUpper() == filtro.ToUpper())
        //            {
        //                    //precoVenda = Convert.ToDecimal(frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString());
        //                    //precoFinal = (precoVenda * Convert.ToDecimal(tbxQuantidade.Text.ToString()));
        //                    //tbxValor.Text = contas.newValidaAjustaArredonda4CasasDecimais(precoFinal.ToString());
        //                    descricao = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
        //                    //codigoSistema = frmPDV.dt_DadosProdutos.Rows[i]["CODIGO_SIST"].ToString();
        //                    //codigoFabric = frmPDV.dt_DadosProdutos.Rows[i]["CODIGO_FABR"].ToString();
        //                    //codigoOrigem = frmPDV.dt_DadosProdutos.Rows[i]["CODIGO_ORIG"].ToString();
        //                    //qtdAtual = Convert.ToDecimal(frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString());
        //                    //aplicacao = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
        //                    //quantidade = tbxQuantidade.Text.ToString();
        //                    //tipo = frmPDV.dt_DadosProdutos.Rows[i]["TIPOPRODUTO"].ToString();
        //                    MessageBox.Show(descricao);
        //            }
        //    }
        }
        #endregion

        #region Evento KeyDown da TbxFiltroPesquisa
        private void tbxFiltroPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {                      
                if (tbxFiltroPesquisa.Text == "")
                {
                    this.tbxFiltroPesquisa.Focus();
                    return;
                }
                else
                {
                    string tmpCodigo = tbxFiltroPesquisa.Text.PadLeft(13);
                    tmpCodigo = tmpCodigo.Trim();
                    tmpCodigo = tmpCodigo.TrimEnd();
                    tmpCodigo = tmpCodigo.TrimStart();
                    tbxFiltroPesquisa.Text = tmpCodigo;                    
                }
            }
        }
        #endregion

        #region Evento Double click no Lvw (seleciona o Produto)
        public void lvwPesquisaProdutos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tbxCodigoProduto.Text = this.lvwPesquisaProdutos.SelectedItems[0].SubItems[8].Text.ToString();                
                tbxCodigoFabricante.Text = this.lvwPesquisaProdutos.SelectedItems[0].SubItems[1].Text.ToString();
                
                tbxDescricaoAplicacaoEFilho.Text = this.lvwPesquisaProdutos.SelectedItems[0].SubItems[3].Text.ToString();// +"-" + this.lvwPesquisaProdutos.SelectedItems[0].SubItems[6].Text.ToString();// +"-" + this.lvwPesquisaProdutos.SelectedItems[0].SubItems[7].Text.ToString(); 
                                
                tbxPrecoNormal.Text = this.lvwPesquisaProdutos.SelectedItems[0].SubItems[4].Text.ToString();                
                tbxQuantidadeEstoque.Text = this.lvwPesquisaProdutos.SelectedItems[0].SubItems[5].Text.ToString();
                                
                tbxQuantidade.Clear();
                tbxQuantidade.Text = "1,0000";
                tbxQuantidade.ReadOnly = false;
                tbxValorTotal.ReadOnly = true;               
                tbxValorDigitado.Text = tbxPrecoNormal.Text;
                tbxQuantidade.Focus();
                lblProdutoAdicionado.Text = "";
            }
            catch
            {

            }
        }
        #endregion

        #region Evento das RdbBusca CheckedChenged May May (escrevi errado de propósito)
        private void rdbBuscaDescricao_CheckedChanged(object sender, EventArgs e)
        {
            codPesquisa = 3;
            tbxFiltroPesquisa.Focus();
            tbxFiltroPesquisa.Clear();
            carregaConfigForm();     
        }

        private void rdbBuscaAplicacao_CheckedChanged(object sender, EventArgs e)
        {
            codPesquisa = 4;
            tbxFiltroPesquisa.Focus();
            tbxFiltroPesquisa.Clear();
            carregaConfigForm();     
        }

        private void rdbBuscaCodFabricante_CheckedChanged(object sender, EventArgs e)
        {
            codPesquisa = 5;
            tbxFiltroPesquisa.Focus();
            tbxFiltroPesquisa.Clear();
            carregaConfigForm();     
        }

        private void rdbBuscaOriginal1_CheckedChanged(object sender, EventArgs e)
        {
            codPesquisa = 6;
            tbxFiltroPesquisa.Focus();
            tbxFiltroPesquisa.Clear();
            carregaConfigForm();     
        }

        private void rdbBuscaCodBarras_CheckedChanged(object sender, EventArgs e)
        {
            codPesquisa = 7;
            tbxFiltroPesquisa.Focus();
            tbxFiltroPesquisa.Clear();
            carregaConfigForm();     
        }
        #endregion

        #region Evento que Procura Produtos Similares
        private void verificarProdutosSimilaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                codigoProdutoSimilarSelecionar = "";
                int codigoProduto = Convert.ToInt32(lvwPesquisaProdutos.SelectedItems[0].SubItems[16].Text.ToString());
                string fabricante = lvwPesquisaProdutos.SelectedItems[0].SubItems[1].Text.ToString();
                string descricao = lvwPesquisaProdutos.SelectedItems[0].SubItems[3].Text.ToString() + "-" + this.lvwPesquisaProdutos.SelectedItems[0].SubItems[6].Text.ToString() + "-" + this.lvwPesquisaProdutos.SelectedItems[0].SubItems[7].Text.ToString();

                if (codigoProdutoSimilarSelecionar != "") // se for diferente de null, significa que o cara SELECIONOU na Tela de Similares...
                {
                    DataTable dt_DadosFiltrados = new DataTable();
                    lvwPesquisaProdutos.Items.Clear(); //limpo o ListView para mostrar nova consulta
                    lvwPesquisaProdutos.Columns.Clear();

                    #region Cria e Formata as Colunas dentro do ListView
                    lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
                    lvwPesquisaProdutos.Columns.Add("CodigoFabr", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("CodigoOrig", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("Descricao", 340, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("PrecoVenda", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("QdtEstoque", 80, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("Aplicacao", 264, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
                    lvwPesquisaProdutos.Columns.Add("DescricaoFilho", 180, HorizontalAlignment.Left);//COLUNA INVISIVEL PARA MOSTRAR A DESCRIÇÃO                 
                    lvwPesquisaProdutos.Columns.Add("PrVendaParc", 100, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("PrReVenda", 100, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("PrReVendaParc", 100, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("LocalEstoque", 100, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("CorredorCaixa", 100, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("CodigoOrig2", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("CodigoParal", 100, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("PrecoMinimo", 1, HorizontalAlignment.Right);
                    lvwPesquisaProdutos.Columns.Add("PkProduto", 1, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("PkFilho", 60, HorizontalAlignment.Left);
                    lvwPesquisaProdutos.Columns.Add("IMG", 40, HorizontalAlignment.Left);
                    //NA LABEL ABAIXO DO LISTVIEW
                    #endregion
                    
                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("CodigoFabr");
                    dt_DadosFiltrados.Columns.Add("CodigoOrig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("PrecoVenda");
                    dt_DadosFiltrados.Columns.Add("QtdEstoque");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    dt_DadosFiltrados.Columns.Add("DescricaoFilho");
                    dt_DadosFiltrados.Columns.Add("PrVendaParc");
                    dt_DadosFiltrados.Columns.Add("PrReVenda");
                    dt_DadosFiltrados.Columns.Add("PrReVendaParc");
                    dt_DadosFiltrados.Columns.Add("LocalEstoque");
                    dt_DadosFiltrados.Columns.Add("CorredorCaixa");
                    dt_DadosFiltrados.Columns.Add("CodigoOrig2");
                    dt_DadosFiltrados.Columns.Add("CodigoParal");
                    dt_DadosFiltrados.Columns.Add("HabSubProd");
                    dt_DadosFiltrados.Columns.Add("PrecoMinimo");
                    dt_DadosFiltrados.Columns.Add("PkProduto");
                    dt_DadosFiltrados.Columns.Add("PkFilho");
                    dt_DadosFiltrados.Columns.Add("IMG");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    //cria o DataTable com o Filtro de Pesquisa
                    for (int i = 0; i < frmPDV.dt_DadosProdutos.Rows.Count; i++)
                    {                            
                            if (frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString() == codigoProdutoSimilarSelecionar)
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CodigoFabr"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOFABRIC"].ToString();
                                DR["CodigoOrig"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL"].ToString();
                                DR["Descricao"] = frmPDV.dt_DadosProdutos.Rows[i]["DESCRICAO"].ToString();
                                DR["PrecoVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDA"].ToString();
                                DR["QtdEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["QTD_ATUAL"].ToString();
                                DR["Aplicacao"] = frmPDV.dt_DadosProdutos.Rows[i]["APLICACAO"].ToString();
                                DR["DescricaoFilho"] = "";
                                DR["PrVendaParc"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOVENDAPARCELADO"].ToString();
                                DR["PrReVenda"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOREVENDANORMAL"].ToString();
                                DR["PrReVendaParc"] = frmPDV.dt_DadosProdutos.Rows[i]["PRECOREVENDAPARCELADO"].ToString();
                                DR["LocalEstoque"] = frmPDV.dt_DadosProdutos.Rows[i]["LOCALESTOQUE"].ToString();
                                DR["CorredorCaixa"] = frmPDV.dt_DadosProdutos.Rows[i]["CORREDORCAIXA"].ToString();
                                //DR["CodigoOrig2"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOORIGINAL2"].ToString();
                                DR["CodigoParal"] = frmPDV.dt_DadosProdutos.Rows[i]["CODIGOPARALELO"].ToString();
                                DR["HabSubProd"] = frmPDV.dt_DadosProdutos.Rows[i]["HABSUBPROD"].ToString();
                                DR["PrecoMinimo"] = "0,00";
                                DR["PkProduto"] = frmPDV.dt_DadosProdutos.Rows[i]["PK_CODIGOSIST"].ToString();
                                DR["PkFilho"] = "";

                                if (frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString().ToUpper() == "TRUE" || frmPDV.dt_DadosProdutos.Rows[i]["POSSUI_IMAGEM"].ToString() == "1")
                                {
                                    DR["IMG"] = "SIM";
                                }
                                else
                                {
                                    DR["IMG"] = "NAO";
                                }

                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;

                            }
                        }//fim do FOR                        
                        #endregion

                    //inicia a população do ListView
                    #region Faz o Update Dentro do ListView
                    lvwPesquisaProdutos.BeginUpdate();
                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaProdutos.Items.Add("");
                        bool pintadinho = false;
                        if (dt_DadosFiltrados.Rows[i2]["HABSUBPROD"].ToString().ToUpper() == "TRUE" || dt_DadosFiltrados.Rows[i2]["HABSUBPROD"].ToString().ToUpper() == "1")
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.SteelBlue; //para produtos filho = DarkSlateGray
                            pintadinho = true;
                        }

                        if (dt_DadosFiltrados.Rows[i2]["PkFilho"].ToString().ToUpper() != "")
                        {
                            lvwPesquisaProdutos.Items[i2].BackColor = Color.DarkTurquoise; //para produtos filho = DarkSlateGray
                            pintadinho = true;
                        }

                        if (!pintadinho)
                        {
                            if (i2 % 2 == 0)
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lvwPesquisaProdutos.Items[i2].BackColor = Color.White;
                            }
                        }

                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoFabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoVenda"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["QtdEstoque"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["DescricaoFilho"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrVendaParc"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrReVenda"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrReVendaParc"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["LocalEstoque"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CorredorCaixa"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoOrig2"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CodigoParal"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PrecoMinimo"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkProduto"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["PkFilho"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["IMG"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    gbpResultadoPesquisa.Text = "Resultado Pesquisa: " + dt_DadosFiltrados.Rows.Count.ToString() + " Produtos Exibidos!";
                    gbpResultadoPesquisa.Refresh();
                    // dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==2                
                this.lvwPesquisaProdutos.Activation = ItemActivation.OneClick;
            }            
            catch
            {
                MessageBox.Show("Impossível localizar Produtos Similares.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        
        #region Evento Leave da Tbx Valor Total
        private void tbxValorTotal_Leave(object sender, EventArgs e)
        {
            if (tbxValorTotal.ReadOnly == false)
            {
                tbxValorTotal.Text = contas.newValidaAjustaArredonda4CasasDecimais(tbxValorTotal.Text);
                if (contas.verificaSeEDecimal(tbxValorTotal.Text))
                {
                    decimal quantidade = Convert.ToDecimal(tbxValorTotal.Text) / Convert.ToDecimal(tbxValorDigitado.Text);
                    tbxQuantidade.Text = contas.newValidaAjustaArredonda4CasasDecimais(quantidade.ToString());
                    btnFinalizarPesquisa.Focus();
                }
            }
        }
        #endregion
    }//fim classe
}//fim namespace