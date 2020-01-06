using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataContrValidacoes;
using FuturaDataTCC.Iniciar;
using System.IO;
using System.Diagnostics;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;
using DllInfoSigaUtil.Grafico;

namespace FuturaDataTCC.Views.Gestoes
{
    public partial class frmGestaoProdutos : Form
    {
        #region Atributos e Variaveis do View frmGestaoProdutos
        frmTelaPrincipal telaPrincipal;
        frmInicializacao frmInicial;
        //clsProduto produto = new clsProduto();
        iConProduto controlProduto = new iConProduto();
        iConEstoque controlEstoque = new iConEstoque();
        private string flagStatus; //usava para saber se a opção é salvar ou excluir

        public int indice = 0; //controlará o indice dentro do DataDrigView
        clsImagem imagem = new clsImagem();
        
        string caminhoDaAplicacao = System.Environment.CurrentDirectory.ToString();
        iModProduto[] arrayProdutos;
        Byte[] imagemEmBytes;
        string caminhoImagem = "";
        int codPesquisa = 2;

        clsNewContasMatematicas contasMat = new clsNewContasMatematicas();
        #endregion

        #region Construtor (inicializador) do Form
        public frmGestaoProdutos(frmInicializacao frmInicia, frmTelaPrincipal telaPrincipal)
        {
            InitializeComponent();
            this.telaPrincipal = telaPrincipal;
            this.frmInicial = frmInicia;
            
            carregarCampos();
            travarBotoes();
            travarCampos();

            tbcProdutos.SelectedTab = tbpPesquisa;
            tbxPalavraChaveBusca_TextChanged(null, null);
            tbxPalavraChaveBusca.Focus();
        }
        #endregion

        #region **************MÉTODOS***************

        #region Método Carrega Cadastro de Produtos
        public void carregarCadProdutos()
        {
            arrayProdutos = controlProduto.cObterProduto();            
        }//fim método
        #endregion

        #region Método Carrega Campos nas TextBoxs e Outros
        public void carregarCampos()
        {            
            if (arrayProdutos == null)
            {
                this.carregarCadProdutos();
                if (arrayProdutos.Length == 0)
                {
                    MessageBox.Show("Esse é o seu primeiro cadastro de produto. Dica: Clique em 'NOVO' e use a tecla 'TAB' de seu teclado para se locomover de um campo ao outro. (TAB avança entre os campos, CTRL+TAB retrocede entre os campos)", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbxCodigoProdutoSistema.Text = "";
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = false;
                }
            }
            if (arrayProdutos.Length > 0)
            {
                iModProduto item = arrayProdutos[indice];
                tbxCodigoProdutoSistema.Text = item.Pk_Codigo.ToString();
                tbxCodigoFabricante.Text = item.CodigoFabric;
                
                //tbxNCM.Text = dt_DadosProdutos.Rows[indice]["NCM"].ToString();
                tbxDescricaoProduto.Text = item.Descricao;
                tbxAplicacaoProduto.Text = item.Aplicacao;

                tbxPrecoCustoConsumidor.Text = item.PrecoCusto.ToString();
                tbxMargemLucro.Text = item.MargemLucro.ToString();
                tbxPrecoVendaNormal.Text = item.PrecoVenda.ToString();
                
                tbxMaisInfo.Text = item.MaisInfo;
                tbxQuantidadeAtual.Text = item.QtdAtual.ToString();
                cbbUnidade.Text = item.Unidade;

                tbxPorcentagemMediaImpostoPago.Text = item.PorcImpPago.ToString();
                
                cbbIcms.Text = item.Icms;

                if (cbbIcms.Text == "")
                {
                    cbbIcms.Text = "18,0000";
                }

                
                tbxCodigoOriginal.Text = item.CodigoOriginal;
                tbxCorredorSetor.Text = item.CorredorSetor;
                tbxLocalCaixa.Text = item.LocalCaixa;
                bool possuiImagem = item.PossuiImagem;

                if (possuiImagem)
                {
                    try
                    {
                        controlProduto.modProduto.Pk_Codigo = Convert.ToInt32(tbxCodigoProdutoSistema.Text);
                        pctImagemCliente.Image =  controlProduto.cObterImagem();
                        pctImagemCliente.Refresh();
                    }
                    catch
                    {
                        pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.produtoSemFoto;
                        pctImagemCliente.Refresh();
                    }
                }
                else
                {
                    pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.produtoSemFoto;
                    pctImagemCliente.Refresh();
                }

                flagStatus = "Carregado";
                tbcProdutos.SelectedTab = tbpInformacoesProduto;
            }
        }
        #endregion
        
        #region Método Carregar Campos nos atributos da Classe clsProduto
        public void carregarCamposNaClasseProduto()
        {
            if (caminhoImagem != "")
            {
                long tamanhoDaImagem = 0;
                FileInfo imagem = new FileInfo(caminhoImagem);
                tamanhoDaImagem = imagem.Length;
                imagemEmBytes = new byte[Convert.ToInt32(tamanhoDaImagem)];
                FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read, FileShare.Read);
                fs.Read(imagemEmBytes, 0, Convert.ToInt32(tamanhoDaImagem));
                fs.Close();
                controlProduto.modProduto.ImagemProduto = imagemEmBytes;
                controlProduto.modProduto.PossuiImagem = true;
            }
            else
            {
                controlProduto.modProduto.PossuiImagem = false;
            }

            controlProduto.modProduto.Aplicacao = tbxAplicacaoProduto.Text;
            controlProduto.modProduto.CodigoFabric = tbxCodigoFabricante.Text;
            controlProduto.modProduto.CodigoOriginal = tbxCodigoOriginal.Text;
            controlProduto.modProduto.CorredorSetor = tbxCorredorSetor.Text;
            controlProduto.modProduto.Descricao = tbxDescricaoProduto.Text;            
            controlProduto.modProduto.Icms = cbbIcms.Text;
            controlProduto.modProduto.LocalCaixa = tbxLocalCaixa.Text;
            controlProduto.modProduto.MaisInfo = tbxMaisInfo.Text;
            controlProduto.modProduto.MargemLucro = Convert.ToDecimal(tbxMargemLucro.Text);
            controlProduto.modProduto.Pk_Codigo = Convert.ToInt32(tbxCodigoProdutoSistema.Text);
            controlProduto.modProduto.PorcImpPago = Convert.ToDecimal(tbxPorcentagemMediaImpostoPago.Text);
            controlProduto.modProduto.PrecoCusto = Convert.ToDecimal(tbxPrecoCustoConsumidor.Text);
            controlProduto.modProduto.PrecoVenda = Convert.ToDecimal(tbxPrecoVendaNormal.Text);
            controlProduto.modProduto.QtdAtual = Convert.ToDecimal(tbxQuantidadeAtual.Text);
            controlProduto.modProduto.Status = "ATIVO";
            controlProduto.modProduto.Unidade = cbbUnidade.Text;
        }
        #endregion

        #region Método Destravar Botões
        public void destravarBotoes()
        {
            this.btnNovo.Enabled = false;
            this.btnAlterar.Enabled = false;
            this.btnCancelar.Enabled = true;
            this.btnExcluir.Enabled = false;
            this.btnSalvar.Enabled = true;

        }
        #endregion

        #region Método Travar Botões
        public void travarBotoes()
        {
            this.btnNovo.Enabled = true;
            this.btnAlterar.Enabled = true;
            this.btnCancelar.Enabled = false;
            this.btnExcluir.Enabled = true;
            this.btnSalvar.Enabled = false;
        }
        #endregion

        #region Método Travar Campos
        //rotina que trava os campos de codigos do produto e o fornecedor do mesmo
        //pois em uma alteraçao somente alguns dados sao alterados
        public void travarCampos()
        {
            clsFuncoes.TravaControles(tbpInformacoesProduto);
        }
        #endregion

        #region Método Destravar Campos
        public void destravarCampos()
        {
            clsFuncoes.DestravaControles(tbpInformacoesProduto);
            clsFuncoes.DestravaControles(tbpPesquisa);
            
            tbxCodigoProdutoSistema.ReadOnly = true;
        }
        #endregion

        #region Método Validar Campos
        //verifica se os campos estao preenchidos, se achar algum campo em branco
        //a funcao retorna true
        private bool validarCampos()
        {
            if (tbxCodigoFabricante.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Nº Produto(Fabric) é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxCodigoFabricante.Focus();
                return false;
            }//fim If Codigo Produto Fabricante == ""

            if (tbxDescricaoProduto.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Descrição do Produto é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxDescricaoProduto.Focus();
                return false;
            }//fim if descriçãoProduto == ""

            if (cbbIcms.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Alíquota é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbIcms.Focus();
                return false;
            }


            clsNewContasMatematicas matematica = new clsNewContasMatematicas();

            if (!matematica.verificaSeEDecimal(tbxPrecoCustoConsumidor.Text))
            {
                MessageBox.Show("O valor inserido no campo Preço de Custo precisa ter ',', exemplo: 10,50 ou 12,00 !!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxPrecoCustoConsumidor.Focus();
                return false;
            }//Fim if Verifica se o Preço Custo Consumidor != Numero

            if (tbxPrecoCustoConsumidor.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Preço de Custo do Produto é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxPrecoCustoConsumidor.Focus();
                return false;
            }//fim if preço Custo == ""

            
            if (tbxPrecoCustoConsumidor.Text.Trim().ToString() == "")
            {
                tbxPrecoCustoConsumidor.Text = "0,0000";
            }

            decimal aux_Custo = Convert.ToDecimal(tbxPrecoCustoConsumidor.Text.Trim().ToString());

            if (tbxPrecoVendaNormal.Text.Trim().ToString() == "")
            {
                tbxPrecoVendaNormal.Text = "0,0000";
            }

            decimal aux_Venda = Convert.ToDecimal(tbxPrecoVendaNormal.Text.Trim().ToString());

            if (aux_Custo > aux_Venda)
            {
                MessageBox.Show("O Preço de Custo é menor do que o preço de venda", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxPrecoCustoConsumidor.Focus();
                return false;
            }//fim if Preco de Custo > que Preço Venda

            if (tbxMargemLucro.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Margem do Produto é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxMargemLucro.Focus();
                return false;
            }// Fim If margem LucroProdutoConsumidor == ""

            
            if (cbbUnidade.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Unidade do Produto é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbUnidade.Focus();
                return false;
            }//fim if Cbb Unidade == ""

            if (tbxQuantidadeAtual.Text.Trim() == "")
            {
                MessageBox.Show("O Campo Qtd. Atual é Obrigatório!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxQuantidadeAtual.Focus();
                return false;
            }//fim if quantidade atual == ""
            return true;
        }
        #endregion
        
        #region Método Alterar Cadastro
        public void alterarCadastro()
        {
            if (arrayProdutos.Length != 0)
            {
                this.flagStatus = "Alterar";
                this.destravarCampos();
                this.destravarBotoes();
            }
        }
        #endregion

        #region Método Cancelar Operação
        public void cancelarOperacao()
        {
            this.destravarBotoes();
            this.travarCampos();
            this.travarBotoes();
            this.indice = 0;
            this.carregarCampos();
        }
        #endregion

        #region Método Salvar ou Alterar Produto
        public void salvarProduto()
        {
            if (validarCampos())
            {
                this.carregarCamposNaClasseProduto();
                if (this.flagStatus == "Salvar")
                {
                    if (this.controlProduto.cInsereProduto())
                    {

                        MessageBox.Show("Produto inserido com sucesso!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao tentar inserir o Produto, contate o suporte!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (this.flagStatus == "Alterar")
                    {
                        if (this.controlProduto.cAlteraProduto())
                        {

                            MessageBox.Show("Produto alterado com sucesso!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao tentar alterar o Produto, contate o suporte!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                this.travarCampos();
                this.travarBotoes();
                this.carregarCadProdutos();

                if (this.flagStatus == "Salvar")
                {
                    indice = arrayProdutos.Length - 1;
                }
                flagStatus = "";
                this.carregarCampos();
            }
        }
        #endregion

        #region Método Excluir Produto
        private void excluirProduto()
        {
            if (arrayProdutos.Length > 0)
            {
                this.carregarCamposNaClasseProduto();
                if (controlProduto.cExcluiProduto())
                {
                    MessageBox.Show("Produto excluído com sucesso!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao tentar excluir o Produto, contate o suporte!!!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.carregarCadProdutos();
                this.indice = 0;
                this.carregarCampos();
            }
        }
        #endregion

        #region Método Calcular Margem de Lucro
        public double calcularMargemLucro(double preco, double margem)
        {
            clsNewContasMatematicas matematica = new clsNewContasMatematicas();
            double margemLucro;

            margemLucro = (preco * margem) / 100;
            return Convert.ToDouble(matematica.newValidaAjustaArredonda4CasasDecimais((preco + margemLucro).ToString()));
        }
        #endregion

        #region Método Inicia calculo da margem de lucro
        public void calcularMargemLucro()
        {
            if ((tbxPrecoCustoConsumidor.Text.Trim() != "") && (tbxMargemLucro.Text.Trim() != ""))
            {
                if (tbxPrecoVendaNormal.Text == "0,0000" || tbxPrecoVendaNormal.Text == "0" || tbxPrecoVendaNormal.Text == "")
                {
                    decimal porcentagemLucro = Convert.ToDecimal(contasMat.newMultiplicaCampos4CasasDecimais(tbxPrecoCustoConsumidor.Text, tbxMargemLucro.Text)) / 100;
                    porcentagemLucro = Convert.ToDecimal(tbxPrecoCustoConsumidor.Text) + porcentagemLucro;
                    tbxPrecoVendaNormal.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(porcentagemLucro.ToString());
                }
            }
        }
        #endregion

        #endregion **************MÉTODOS***************

        #region **************EVENTOS***************

        #region Leave da TextBox do NCM
        private void tbxNCM_Leave(object sender, EventArgs e)
        {
            if (tbxNCM.Text != "")
            {
                DataSet dsDadosIBPT = new DataSet();
                dsDadosIBPT.ReadXml(@"c:\FuturaData\TCC\ibpt.xml");
                DataTable dt_MayMayTheAcesSpadesHãm = dsDadosIBPT.Tables[0];
                bool achado = false;
                for (int i = 0; i < dt_MayMayTheAcesSpadesHãm.Rows.Count; i++)
                {
                    if (!achado)
                    {
                        if (dt_MayMayTheAcesSpadesHãm.Rows[i]["codigo"].ToString().Trim() == tbxNCM.Text)
                        {
                            tbxPorcentagemMediaImpostoPago.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(dt_MayMayTheAcesSpadesHãm.Rows[i]["aliqNac"].ToString().Trim());
                            achado = true;
                            //tbxEAN.Focus();
                        }//fim do if tbxNCM.Text
                    }//fim do if exclamação achado
                }//fim do for

                if (tbxPorcentagemMediaImpostoPago.Text == "")
                {
                    tbxPorcentagemMediaImpostoPago.Text = "0,0000";
                }
            }
        }
        #endregion
        
        #region Evento Click Botão Novo Cadastro
        private void btnNovo_Click(object sender, EventArgs e)
        {

            clsFuncoes.LimpaControles(tbpInformacoesProduto);
            tbxCodigoProdutoSistema.Text = "0";

            this.flagStatus = "Salvar";
            this.destravarCampos();
            this.destravarBotoes();
            caminhoImagem = "";

            cbbUnidade.Text = "PC";
            tbxQuantidadeAtual.Text = "1,0000";

            cbbIcms.SelectedIndex = 5;
            tbxPrecoCustoConsumidor.Text = "0,0000";

            tbxMargemLucro.Text = "0,0000";
            tbxPrecoVendaNormal.Text = "0,0000";

            cbbLocalProduto.SelectedIndex = 0;
            tbxPorcentagemMediaImpostoPago.Text = "0,0000";
            tbxQuantidadeAtual.Text = "1,0000";

            tbxCorredorSetor.Clear();
            tbxLocalCaixa.Clear();
            pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
            pctImagemCliente.Refresh();
            caminhoImagem = "";

            tbcProdutos.SelectedTab = tbpInformacoesProduto;
            tbxCodigoFabricante.Focus();
        }
        #endregion

        #region Evento Click Botão Alterar Cadastro
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            this.alterarCadastro();
            this.btnAnterior.Enabled = false;
            this.btnUltimo.Enabled = false;
            this.btnPrimeiro.Enabled = false;
            this.btnProximo.Enabled = false;
            tbxCodigoFabricante.Focus();
        }
        #endregion

        #region Evento Click Botão Salvar Cadastro
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            this.salvarProduto();
            this.btnAnterior.Enabled = true;
            this.btnUltimo.Enabled = true;
            this.btnPrimeiro.Enabled = true;
            this.btnProximo.Enabled = true;

            tbxPalavraChaveBusca.Clear();
            tbxPalavraChaveBusca_TextChanged(null, null);
        }
        #endregion

        #region Evento Click Botão Excluir Cadastro
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você está prestes a excluir o registro, deseja realmente continuar?", "FuturaData - Gestão de Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.excluirProduto();
                this.btnAnterior.Enabled = true;
                this.btnUltimo.Enabled = true;
                this.btnPrimeiro.Enabled = true;
                this.btnProximo.Enabled = true;
            }
        }
        #endregion

        #region Evento Click Botão Cancelar Operação
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.cancelarOperacao();
            this.btnAnterior.Enabled = true;
            this.btnUltimo.Enabled = true;
            this.btnPrimeiro.Enabled = true;
            this.btnProximo.Enabled = true;
            tbcProdutos.SelectedTab = tbpPesquisa;
            tbxPalavraChaveBusca.Focus();
        }
        #endregion

        #region Botões Proximo, Anterior, Primeiro e Ultimo Cadastro

        #region Evento Click ToolStrip Botão Ultimo
        private void toolult_Click(object sender, EventArgs e)
        {
            this.indice = arrayProdutos.Length - 1;
            carregarCampos();
        }
        #endregion

        #region Evento Click ToolStrip Botão Primeiro
        private void toolprim_Click(object sender, EventArgs e)
        {
            indice = 0;
            carregarCampos();
        }
        #endregion

        #region Evento Click ToolStrip Botão Proximo
        private void toolprox_Click(object sender, EventArgs e)
        {
            if (indice != arrayProdutos.Length - 1)
            {
                indice++;
                carregarCampos();
            }
            else
                MessageBox.Show("Você está no ultimo Cadastro!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Evento Click Toolstrip Botão anterior
        private void toolant_Click(object sender, EventArgs e)
        {
            if (indice > 0)
            {
                indice--;
                carregarCampos();
            }
            else
            {
                MessageBox.Show("Você está no primeiro Cadastro!", "FuturaData - Gestão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #endregion

        #region Evento da Pesquisa de Produtos (textChanged da TbxPesquisa)
        private void tbxPalavraChaveBusca_TextChanged(object sender, EventArgs e)
        {
            lvwPesquisaProdutos.Items.Clear(); //limpo o ListView para mostrar nova consulta
            lvwPesquisaProdutos.Columns.Clear();

            #region Cria e Formata as Colunas dentro do ListView
            lvwPesquisaProdutos.Columns.Add("", 0, HorizontalAlignment.Center);
            lvwPesquisaProdutos.Columns.Add("Código Loja", 0, HorizontalAlignment.Center);
            lvwPesquisaProdutos.Columns.Add("Código Fabr", 110, HorizontalAlignment.Center);
            lvwPesquisaProdutos.Columns.Add("Descricao", 238, HorizontalAlignment.Center);
            lvwPesquisaProdutos.Columns.Add("Aplicacao", 238, HorizontalAlignment.Center);
            #endregion

            if (tbxPalavraChaveBusca.Text != "")
            {
                #region Pesquisa == 1 (Codigo do Fabricante)
                if (codPesquisa == 1)
                {
                    #region Monta o Filtro de Dados e a Pesquisa
                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxPalavraChaveBusca.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxPalavraChaveBusca.Text.ToString().Substring(0, tamanhoFiltro);
                    #endregion

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("Código Loja");
                    dt_DadosFiltrados.Columns.Add("Código Fabr");
                    dt_DadosFiltrados.Columns.Add("Código Orig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    dt_DadosFiltrados.Columns.Add("Preço Venda");
                    dt_DadosFiltrados.Columns.Add("Estoque");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    
                    foreach (iModProduto item in arrayProdutos)
                    {
                        if (item.CodigoFabric.Length >= tamanhoFiltro)
                        {
                            if (item.CodigoFabric.Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["Código Loja"] = item.Pk_Codigo;
                                DR["Código Fabr"] = item.CodigoFabric;
                                DR["Descricao"] = item.Descricao;
                                DR["Aplicacao"] = item.Aplicacao;
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

                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Código Loja"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Código Fabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    lblProdutosListados.Text = arrayProdutos.Length.ToString() + " Produtos Cadastrados ao total. " + dt_DadosFiltrados.Rows.Count.ToString() + " produtos com a palavra chave pesquisada.";
                    lblProdutosListados.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==4
                #endregion

                #region Pesquisa == 2 (Pesquisa por Descricao)
                if (codPesquisa == 2)//descricao
                {
                    #region Monta o Filtro de Dados e a Pesquisa
                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxPalavraChaveBusca.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxPalavraChaveBusca.Text.ToString().Substring(0, tamanhoFiltro);
                    #endregion

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada
                    dt_DadosFiltrados.Columns.Add("Código Loja");
                    dt_DadosFiltrados.Columns.Add("Código Fabr");
                    dt_DadosFiltrados.Columns.Add("Código Orig");
                    dt_DadosFiltrados.Columns.Add("Descricao");
                    dt_DadosFiltrados.Columns.Add("Aplicacao");
                    #endregion

                    #region Varre o DataTable Atual para fazer o Filtro
                    //cria o DataTable com o Filtro de Pesquisa
                    foreach (iModProduto item in arrayProdutos)
                    {
                        if (item.Descricao.Length >= tamanhoFiltro)
                        {
                            if (item.Descricao.Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["Código Loja"] = item.Pk_Codigo;
                                DR["Código Fabr"] = item.CodigoFabric;
                                DR["Descricao"] = item.Descricao;
                                DR["Aplicacao"] = item.Aplicacao;
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

                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Código Loja"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Código Fabr"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Descricao"].ToString());
                        lvwPesquisaProdutos.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["Aplicacao"].ToString());
                    }

                    #endregion

                    lvwPesquisaProdutos.EndUpdate();
                    lblProdutosListados.Text = arrayProdutos.Length.ToString() + " Produtos Cadastrados ao total. " + dt_DadosFiltrados.Rows.Count.ToString() + " produtos com a palavra chave pesquisada.";
                    lblProdutosListados.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==2
                #endregion
            }//fim if(tbx=="")
            else
            {
                int indiceLinhaColorida = 0;
                foreach (iModProduto item in arrayProdutos)
                {
                    lvwPesquisaProdutos.BeginUpdate();
                    lvwPesquisaProdutos.Items.Add("");
                    if (indiceLinhaColorida % 2 == 0)
                    {
                        lvwPesquisaProdutos.Items[indiceLinhaColorida].BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lvwPesquisaProdutos.Items[indiceLinhaColorida].BackColor = Color.White;
                    }

                    lvwPesquisaProdutos.Items[indiceLinhaColorida].SubItems.Add(item.Pk_Codigo.ToString());
                    lvwPesquisaProdutos.Items[indiceLinhaColorida].SubItems.Add(item.CodigoFabric);
                    lvwPesquisaProdutos.Items[indiceLinhaColorida].SubItems.Add(item.Descricao);
                    lvwPesquisaProdutos.Items[indiceLinhaColorida].SubItems.Add(item.Aplicacao);
                    indiceLinhaColorida++;
                }
                lvwPesquisaProdutos.EndUpdate();
                lblProdutosListados.Text = arrayProdutos.Length.ToString() + " Produtos Cadastrados ao total.";
                lblProdutosListados.Refresh();                
            }
        }
        #endregion

        #region Evento Text Changed Da Textbox maisInfo
        private void txtMaisInfo_TextChanged(object sender, EventArgs e)
        {
            string aux;
            aux = tbxMaisInfo.Text.ToString();
            Label20.Text = "Mais Info (Máximo 250 Caracters) - Restam" + Convert.ToString(250 - aux.Length);
        }
        #endregion

        #region Evento Double Click no ListView
        private void lvwPesquisaProdutos_DoubleClick(object sender, EventArgs e)
        {
            string cod_Sist = lvwPesquisaProdutos.SelectedItems[0].SubItems[1].Text.ToString();

            int indiceEncontrado = 0;
            foreach (iModProduto item in arrayProdutos)
            {
                if (item.Pk_Codigo.ToString() == cod_Sist)
                {
                    indice = indiceEncontrado;                    
                }
                indiceEncontrado++;
            }

            this.carregarCampos();
            travarCampos();
            travarBotoes();
            flagStatus = "Carregado";
            this.tbcProdutos.SelectedTab = this.tbpInformacoesProduto;
        }
        #endregion

        #region Evento Radio Buttons de Busca
        private void rdbBuscaCodFabr_CheckedChanged(object sender, EventArgs e)
        {
            tbxPalavraChaveBusca.Clear();
            tbxPalavraChaveBusca.Focus();
            codPesquisa = 1;
        }

        #region radio button busca aplicação
        private void rdbBuscaDescricao_CheckedChanged(object sender, EventArgs e)
        {
            tbxPalavraChaveBusca.Clear();
            tbxPalavraChaveBusca.Focus();
            codPesquisa = 2;
        }// Fim codigo de pesquisa = 2
        #endregion
        #endregion

        #region Eventos de Teclado

        #region KeyDown do Form
        /// <summary>
        /// Utilizado para as teclas de atalho: Novo, Salvar, Alterar, Pesquisar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCadProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            this.teclasAtalho(e);
        }
        #endregion

        #region ATALHOS
        public bool teclasAtalho(KeyEventArgs e)
        {
            #region Atalho F4 e Ctrl+S no botão(Salvar Cadastro de Clientes)

            if ((e.KeyCode == Keys.F4) || ((Control.ModifierKeys == Keys.Control) && (e.KeyCode == Keys.S)))
            {
                this.btnSalvar_Click(null, null);
                return true;
            }
            #endregion

            #region Atalho F5 no botão(Atualizar Tela)

            if (e.KeyCode == Keys.F5)
            {
                this.carregarCampos();
                return true;
            }
            #endregion

            #region Atalho F6 e DEL no botão(Excluir Cadastro)

            if ((e.KeyCode == Keys.F6))
            {
                this.btnExcluir_Click(null, null);
                return true;
            }
            #endregion

            #region Atalho F7 e ESC no botão(Cancelar Operação)

            if ((e.KeyCode == Keys.F7))
            {
                if (tbxCodigoFabricante.ReadOnly.Equals(true))
                {
                    if (MessageBox.Show(null, "Todos os dados digitados não foram salvos, deseja realmente CANCELAR a operação?", "FuturaData - Gestão de Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    {
                        this.btnCancelar_Click(null, null);
                        return true;
                    }
                }
                else
                {
                    if (MessageBox.Show(null, "Todos os dados digitados não foram salvos, deseja realmente FECHAR a tela?", "FuturaData - Gestão de Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    {
                        this.Close();
                        return true;
                    }
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

        #endregion

        #region Evento tbxPalavraChaveBusca TextChanged
        private void tbxPalavraChaveBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lvwPesquisaProdutos.Items.Count == 0)
                {
                    tbxPalavraChaveBusca.Clear();
                    tbxPalavraChaveBusca.Focus();
                }
                else
                {
                    this.lvwPesquisaProdutos.Focus();
                    this.lvwPesquisaProdutos.Select();
                    this.lvwPesquisaProdutos.Items[0].Selected = true;
                    this.lvwPesquisaProdutos.Activation = ItemActivation.OneClick;
                    //lvwPesquisaProdutos_PreviewKeyDown(null, null);
                }
            }

            if (e.KeyCode == Keys.F2)
            {
                rdbBuscaCodigoFabric.Checked = true;
                rdbBuscaDescricao.Checked = false;
            }

            if (e.KeyCode == Keys.F3)
            {
                rdbBuscaCodigoFabric.Checked = false;
                rdbBuscaDescricao.Checked = true;
            }
        }
        #endregion

        #region Evento do Botao Alterar Imagem
        private void btnAlterarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileProcurarImagem = new OpenFileDialog();
            openFileProcurarImagem.Title = "Selecione uma imagem para o seu logo";
            openFileProcurarImagem.InitialDirectory = @"C:\";
            openFileProcurarImagem.RestoreDirectory = true;
            openFileProcurarImagem.Filter = "Imagens JPG (apenas formato JPG) (*.jpg)|*.jpg;";

            if (openFileProcurarImagem.ShowDialog() == DialogResult.OK)
            {
                caminhoImagem = @"c:\FuturaData\retorno.jpg";
                bool retorno = new clsImagem().gerarNovaImagem(160, 160, openFileProcurarImagem.FileName.ToString(), caminhoImagem);

                if (retorno)//se ele conseguir gerar ele pega o novo caminho da nova imagem
                {
                    Image imgObj = Image.FromFile(caminhoImagem);
                    pctImagemCliente.Image = imgObj;
                    pctImagemCliente.Refresh();
                }
                else//senão pega a normal mesmo sem compactacao
                {
                    caminhoImagem = openFileProcurarImagem.FileName.ToString();
                }
            }
        }
        #endregion

        #region Evento tbxDescricaoProduto, Produtos, Valores, Arredondamentos e outros
        private void tbxDescricaoProduto_Leave(object sender, EventArgs e)
        {
            if (flagStatus == "Salvar" && tbxAplicacaoProduto.Text == "")
            {
                tbxAplicacaoProduto.Text = tbxDescricaoProduto.Text;
            }
        }
        
        private void tbxPrecoCustoConsumidor_Leave(object sender, EventArgs e)
        {
            tbxPrecoCustoConsumidor.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(tbxPrecoCustoConsumidor.Text);
        }

        private void tbxMargemLucroProdutoConsumidor_Leave(object sender, EventArgs e)
        {
            tbxMargemLucro.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(tbxMargemLucro.Text);
            calcularMargemLucro();
        }

        private void tbxQuantidadeAtual_Leave(object sender, EventArgs e)
        {
            tbxQuantidadeAtual.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(tbxQuantidadeAtual.Text);
        }
        #endregion

        #region Evento com a Porcentagem Media do Imposto Pago
        private void tbxPorcentagemMediaImpostoPago_Leave(object sender, EventArgs e)
        {
            tbxPorcentagemMediaImpostoPago.Text = contasMat.newValidaAjustaArredonda4CasasDecimais(tbxPorcentagemMediaImpostoPago.Text);
        }
        #endregion
        
        #region Evento que Abre a Tabela TIPI
        private void tabelaNCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\FuturaData\TIPI.pdf");
        }
        #endregion        
        #endregion **************EVENTOS***************
    }//Fim Classe     
}//fim namespace
