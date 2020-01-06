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
using System.Threading;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Utilitarios;

namespace FuturaDataTCC.Views.Caixa
{
    public partial class frmNewFecharVenda : Form
    {
        #region "Atributos (variaveis) do View frmNewFecharVenda"
        frmInicializacao frmInicial;
        clsNewContasMatematicas contas = new clsNewContasMatematicas();        
        public DataTable dt_FormasPagamento = new DataTable();
        DataGridViewComboBoxColumn gridFormaPagto = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn gridBanco = new DataGridViewComboBoxColumn();
        CalendarColumn gridColunaData = new CalendarColumn(); //DATAPICKER NO DATAGRID VIEW DESENVOLVIDA POR FERNANDO, CLASSE LÁ NO NAMESPACE UTILITARIOS

        public DataTable dt_DadosFormasPagto = new DataTable();
        iConOrcamento controlOrcamento = new iConOrcamento();
        iConCaixa controlCaixa = new iConCaixa();        
        iConEstoque controlEstoque = new iConEstoque();
        iConPlanoContas controlPlanoContas = new iConPlanoContas();
        DataTable dt_Bancos = new DataTable();
        public DataTable dt_ItensOrcamento = new DataTable();

        string formaPagtoPrincipal = "";

        public decimal descontoTotal = 0;
        public decimal acrescimoTotal = 0;
        
        int formaPagtoAVista1 = 0;
        int formaPagtoAVista2 = 0;
        int formaPagtoAVista3 = 0;
        int formaPagtoAVista4 = 0;
        int formaPagtoAVista5 = 0;
        int formaPagtoAVista6 = 0;
        int formaPagtoAVista7 = 0;
        int formaPagtoAVista8 = 0;
        int formaPagtoAVistaSelecionada = 0;
        
        //Campos Novos para Crédito do Cliente, Prazos, Bloquear cliente Entre outros...
        DataTable dt_DadosOrcamento = new DataTable();
        public decimal descontoPorFormaPagamento = 0;
        public decimal acrescimoPorFormaPagamento = 0;
        #endregion

        #region Construtor (inicializador) do Form
        public frmNewFecharVenda(frmInicializacao frmInicia, int numeroPedido, string numeroCaixa, string serialCaixa, string nomeCliente, string valorVenda, string dataOrc, string idCliente)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            tbxIdentificacaoCaixa.Text = serialCaixa;
            tbxPKIDCaixa.Text = numeroCaixa;


            //DataTable que contem os items Default voltados do banco            
            dt_DadosOrcamento.Columns.Add("PK_ID");
            dt_DadosOrcamento.Columns.Add("FK_NUMPRODUTO");
            dt_DadosOrcamento.Columns.Add("DESCRICAOAPLICACAO");
            dt_DadosOrcamento.Columns.Add("PRECOVENDABANCO");
            dt_DadosOrcamento.Columns.Add("DESCONTO");
            dt_DadosOrcamento.Columns.Add("ACRESCIMO");
            dt_DadosOrcamento.Columns.Add("VALORUNIT");
            dt_DadosOrcamento.Columns.Add("QUANTIDADE");
            dt_DadosOrcamento.Columns.Add("VALORTOTAL");

            tbxNumeroOrcamento.Text = numeroPedido.ToString();
            tbxNomeCliente.Text = nomeCliente;
            tbxValorVenda.Text = valorVenda;
            
            tbxDataOrcamento.Text = dataOrc;
            tbxIDCliente.Text = idCliente;

            bool retornoForma = controlPlanoContas.cObterTodasSubCategoriasDeEntradaDosPlanosDeContasCadastrados();
            if (retornoForma)
            {
                dt_DadosFormasPagto = controlPlanoContas.daoPlanCont.Ds_DadosRetorno.Tables[0];

                DataTable dt_DadosFiltradosFormasAVista = new DataTable();
                dt_DadosFiltradosFormasAVista.Columns.Add("PK");
                dt_DadosFiltradosFormasAVista.Columns.Add("DESCRICAO");
                dt_DadosFiltradosFormasAVista.Columns.Add("TIPOPLANO");
                for (int i = 0; i < dt_DadosFormasPagto.Rows.Count; i++)
                {                    
                    if (dt_DadosFormasPagto.Rows[i]["NUM_PARCELAS"].ToString().ToUpper() == "0")//Define que o pagamento é a VISTA!
                    {
                        DataRow DR = dt_DadosFiltradosFormasAVista.NewRow();
                        DR["PK"] = dt_DadosFormasPagto.Rows[i]["ID_CATEGORIAPLANO"].ToString();
                        DR["DESCRICAO"] = dt_DadosFormasPagto.Rows[i]["DESC_CBB"].ToString();
                        DR["TIPOPLANO"] = "1";
                        dt_DadosFiltradosFormasAVista.Rows.Add(DR);
                        DR = null;
                    }                    
                }//fim do for que preenche as Formas de Pagamento


                //cores: Plano a Vista Normal, a Receber Azulzinho e a Faturar (Vales) Vermelhinho

                try
                {
                    formaPagtoAVista1 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[0]["PK"].ToString());
                    btnFormaPagto1.Text = dt_DadosFiltradosFormasAVista.Rows[0]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista1 = 0;
                    btnFormaPagto1.Visible = false;
                }

                try
                {
                    formaPagtoAVista2 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[1]["PK"].ToString());
                    btnFormaPagto2.Text = dt_DadosFiltradosFormasAVista.Rows[1]["DESCRICAO"].ToString();                    
                }
                catch
                {
                    formaPagtoAVista2 = 0;
                    btnFormaPagto2.Visible = false;
                }

                try
                {
                    formaPagtoAVista3 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[2]["PK"].ToString());
                    btnFormaPagto3.Text = dt_DadosFiltradosFormasAVista.Rows[2]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista3 = 0;
                    btnFormaPagto3.Visible = false;
                }

                try
                {
                    formaPagtoAVista4 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[3]["PK"].ToString());
                    btnFormaPagto4.Text = dt_DadosFiltradosFormasAVista.Rows[3]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista4 = 0;
                    btnFormaPagto4.Visible = false;
                }

                try
                {
                    formaPagtoAVista5 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[4]["PK"].ToString());
                    btnFormaPagto5.Text = dt_DadosFiltradosFormasAVista.Rows[4]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista5 = 0;
                    btnFormaPagto5.Visible = false;
                }

                try
                {
                    formaPagtoAVista6 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[5]["PK"].ToString());
                    btnFormaPagto6.Text = dt_DadosFiltradosFormasAVista.Rows[5]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista6 = 0;
                    btnFormaPagto6.Visible = false;
                }

                try
                {
                    formaPagtoAVista7 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[6]["PK"].ToString());
                    btnFormaPagto7.Text = dt_DadosFiltradosFormasAVista.Rows[6]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista7 = 0;
                    btnFormaPagto7.Visible = false;
                }

                try
                {
                    formaPagtoAVista8 = Convert.ToInt32(dt_DadosFiltradosFormasAVista.Rows[7]["PK"].ToString());
                    btnFormaPagto8.Text = dt_DadosFiltradosFormasAVista.Rows[7]["DESCRICAO"].ToString();
                }
                catch
                {
                    formaPagtoAVista8 = 0;
                    btnFormaPagto8.Visible = false;
                }
            }
            carregaDataGridView();
            recarregaProdutosComDescontosAcrescimos();
        }
        #endregion
        
        #region **************MÉTODOS***************
        #region Carrega DataGridView
        public void carregaDataGridView()
        {
            dgwParcelas.DataSource = null;
            dgwParcelas.Rows.Clear();
            dgwParcelas.Columns.Clear();
            dgwParcelas.Refresh();

            gridFormaPagto.Name = "FORMA_PAGTO";
            gridFormaPagto.ValueType = typeof(int);
            gridFormaPagto.DataSource = dt_DadosFormasPagto;
            gridFormaPagto.DisplayMember = "DESC_CBB".Trim().ToString();
            gridFormaPagto.ValueMember = "ID_CATEGORIAPLANO".Trim().ToString();
            gridFormaPagto.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridFormaPagto.Resizable = DataGridViewTriState.False;           

            gridColunaData.Name = "DATA_VENCIMENTO";

            dgwParcelas.Columns.Add("NUMEROFATURA", "Num.Fat");            
            dgwParcelas.Columns.Add(gridFormaPagto);            
            dgwParcelas.Columns.Add("DATAEMISSAO", "Data.Emissão");
            dgwParcelas.Columns.Add(gridColunaData);
            dgwParcelas.Columns.Add("NUMEROPARCELA", "Num.Parcela");            
            dgwParcelas.Columns.Add("VALORFINAL", "ValorFinal");
            dgwParcelas.Columns.Add("CAIXAATUAL", "CaixaAtual");
            dgwParcelas.Columns.Add("ARECEBER", "AReceber");
            dgwParcelas.Columns.Add("AFATURAR", "AFaturar");
            dgwParcelas.Columns.Add("MAISINFO", "Mais Informações...");
            dgwParcelas.Refresh();
            calculaValores();
            
        }
        #endregion

        #region Valida Dados
        public bool validaDados()
        {
            bool retorno = true;
                        
            if (dgwParcelas.Rows.Count == 0)
            {
                MessageBox.Show("Por favor insira ao menos uma forma de Pagamento com o valor total da Venda!");
                retorno = false;
            }

            return retorno;
        }
        #endregion

        #region Calcula Descontos, Acrescimos, Troco, Falta e tudo mais
        public void calculaValores()
        {
            if (dgwParcelas.Rows.Count == 0)
            {
                decimal valorFinal = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text.Replace(".", ","));
                tbxValorFaltaParcelas.Text = valorFinal.ToString();
                tbxSomaParcelasInseridas.Text = "0,0000";
                //tbxValorFinalComDescIncargos.Text = valorFinal.ToString();
                tbxTroco.Text = "0,0000";
            }
            else
            {
                decimal encargosPagos = 0;
                decimal descontosCedidos = 0;
                decimal somaParcelasInseridas = 0;
                decimal falta = 0;
                decimal troco = 0;                
                decimal valorFinalAPagar = 0;
                string dataAgora = DateTime.Now.ToString("dd/MM/yyyy");
                int indiceParcela = 0; ;
                formaPagtoPrincipal = "";

                for (int i = 0; i <= dgwParcelas.Rows.Count-1; i++)
                {
                    string formaPagamentoSelecionada = "";
                    for (int i2 = 0; i2 < dt_DadosFormasPagto.Rows.Count; i2++)
                    {
                        if (dt_DadosFormasPagto.Rows[i2]["ID_CATEGORIAPLANO"].ToString() == dgwParcelas.Rows[i].Cells["FORMA_PAGTO"].Value.ToString())
                        {

                            bool emiteCF = Convert.ToBoolean(dt_DadosFormasPagto.Rows[i2]["CHAMAR_ECF"]);
                            bool emiteTEF = Convert.ToBoolean(dt_DadosFormasPagto.Rows[i2]["CHAMAR_TEF"]);


                            if (dt_DadosFormasPagto.Rows[i2]["FORMA_VENDA_AVISTA"].ToString().ToUpper() == "TRUE")
                            {
                                formaPagamentoSelecionada = "hastalavistababy"; //flag que sei quando é a vista - é, eu sei... rs
                            }
                            if (dt_DadosFormasPagto.Rows[i2]["FORMA_VENDA_ARECEBER"].ToString().ToUpper() == "TRUE")
                            {
                                formaPagamentoSelecionada = "receber";
                            }
                            if (dt_DadosFormasPagto.Rows[i2]["FORMA_VENDA_AFATURAR"].ToString().ToUpper() == "TRUE")
                            {
                                formaPagamentoSelecionada = "faturar";
                            }
                        }
                    }


                    indiceParcela++;
                    dgwParcelas.Rows[i].Cells["NUMEROPARCELA"].Value = indiceParcela.ToString();
                    encargosPagos = 0;
                    descontosCedidos = 0;
                    somaParcelasInseridas = somaParcelasInseridas + Convert.ToDecimal(dgwParcelas.Rows[i].Cells["VALORFINAL"].Value.ToString().Replace(".", ","));
                    falta = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text.Replace(".", ",")) - somaParcelasInseridas;
                    troco = somaParcelasInseridas - Convert.ToDecimal(tbxValorFinalComDescIncargos.Text.Replace(".", ","));

                    if (troco.ToString() == "0,01" || troco.ToString() == "-0,01")
                    {
                        troco = 0;
                    }

                    valorFinalAPagar = somaParcelasInseridas + encargosPagos - descontosCedidos;
                    if (dataAgora == dgwParcelas.Rows[i].Cells["DATA_VENCIMENTO"].Value.ToString().Substring(0,10))
                    {
                        if (formaPagamentoSelecionada == "hastalavistababy")
                        {
                            dgwParcelas.Rows[i].Cells["CAIXAATUAL"].Value = "SIM";
                            dgwParcelas.Rows[i].Cells["ARECEBER"].Value = "NAO";
                            dgwParcelas.Rows[i].Cells["AFATURAR"].Value = "NAO";
                        }
                        else
                        {
                            if (formaPagamentoSelecionada == "faturar")
                            {
                                dgwParcelas.Rows[i].Cells["CAIXAATUAL"].Value = "NAO";
                                dgwParcelas.Rows[i].Cells["ARECEBER"].Value = "NAO";
                                dgwParcelas.Rows[i].Cells["AFATURAR"].Value = "SIM";
                            }
                            else
                            {
                                dgwParcelas.Rows[i].Cells["CAIXAATUAL"].Value = "NAO";
                                dgwParcelas.Rows[i].Cells["ARECEBER"].Value = "SIM";
                                dgwParcelas.Rows[i].Cells["AFATURAR"].Value = "NAO";
                            }
                        }
                    }
                    else
                    {                        
                        dgwParcelas.Rows[i].Cells["CAIXAATUAL"].Value = "NAO";
                        dgwParcelas.Rows[i].Cells["ARECEBER"].Value = "SIM";
                        dgwParcelas.Rows[i].Cells["AFATURAR"].Value = "NAO";
                    }

                    if(formaPagtoPrincipal.Contains(dgwParcelas.Rows[i].Cells["FORMA_PAGTO"].Value.ToString()))
                    {
                        
                    }
                    else
                    {
                        for (int i2 = 0; i2 < dt_DadosFormasPagto.Rows.Count; i2++)
                        {
                            if (dgwParcelas.Rows[i].Cells["FORMA_PAGTO"].Value.ToString() == dt_DadosFormasPagto.Rows[i2]["ID_CATEGORIAPLANO"].ToString())
                            {
                                if (formaPagtoPrincipal == "")
                                {
                                    formaPagtoPrincipal = dt_DadosFormasPagto.Rows[i2]["DESC_CBB"].ToString();
                                }
                                else
                                {
                                    if (!formaPagtoPrincipal.Contains(dt_DadosFormasPagto.Rows[i2]["DESC_CBB"].ToString()))
                                    {
                                        formaPagtoPrincipal = formaPagtoPrincipal + " - " + dt_DadosFormasPagto.Rows[i2]["DESC_CBB"].ToString();
                                    }
                                }
                            }
                        }
                        
                    }
                }//fim for
                                
                tbxSomaParcelasInseridas.Text = contas.newValidaAjustaArredonda4CasasDecimais(somaParcelasInseridas.ToString());

                if (somaParcelasInseridas > Convert.ToDecimal(tbxValorFinalComDescIncargos.Text.Replace(".", ",")))
                {
                    tbxValorFaltaParcelas.Text = "0,0000";
                    tbxTroco.Text = contas.newValidaAjustaArredonda4CasasDecimais(troco.ToString());
                }
                else
                {
                    tbxValorFaltaParcelas.Text = contas.newValidaAjustaArredonda4CasasDecimais(falta.ToString());
                    tbxTroco.Text = "0,0000";
                }                
            }//fim else
        }
        #endregion

        #region Evento que Fecha a Venda
        public void efetuarFechamentoVenda()
        {
            if (MessageBox.Show("Fechando essa Venda você irá lançar esse movimento no caixa diario, lançar as parcelas futuras (se houver) no contas a receber, e baixar a quantidade dos itens do estoque. Deseja realmente fechar essa venda agora?", "FuturaData Business", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (validaDados())
                {
                    #region Fecha a Venda no Caixa
                    int codigoPedido = Convert.ToInt32(tbxNumeroOrcamento.Text);
                    int codigoCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                    int codigoCliente = Convert.ToInt32(tbxIDCliente.Text);
                    string identificacaoCaixa = tbxIdentificacaoCaixa.Text;
                                        
                    //passa os parametros para a classe de caixa
                    controlCaixa.modCaixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                    controlCaixa.modCaixa.orcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);                    
                    controlCaixa.modCaixa.FormaPagtoPrincipal = formaPagtoPrincipal;
                    controlCaixa.modCaixa.IdentificacaoCaixa = tbxIdentificacaoCaixa.Text;
                    controlCaixa.modCaixa.planoContas.IdCategoriaMestre = formaPagtoAVistaSelecionada; //CONFIRMAR SE É ESSA VARIAVEL MESMO...
                    controlCaixa.modCaixa.InfoAdicional = tbxInfoAdicional.Text;
                    controlCaixa.modCaixa.NumeroCupomFiscal = "";
                    controlCaixa.modCaixa.Troco = Convert.ToDecimal(tbxTroco.Text);
                    controlCaixa.modCaixa.ValorAcrescimo = Convert.ToDecimal(tbxEncargos.Text);
                    controlCaixa.modCaixa.ValorBrutoOrc = Convert.ToDecimal(tbxValorVenda.Text);
                    controlCaixa.modCaixa.ValorDadoCliente = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
                    controlCaixa.modCaixa.ValorDesconto = Convert.ToDecimal(tbxDescontoValor.Text);
                    controlCaixa.modCaixa.ValorPago = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);

                    bool retornoVenda = controlCaixa.cFecharVenda();
                    #endregion

                    if (retornoVenda)
                    {
                        #region Insere a forma de pagamento na tabela
                        for (int i = 0; i <= dgwParcelas.Rows.Count - 1; i++)
                        {
                            controlCaixa.modCaixa.ParFormaPagtoPlanoConta = Convert.ToInt32(dgwParcelas.Rows[i].Cells["FORMA_PAGTO"].Value);
                            controlCaixa.modCaixa.ParNumeroFatura = dgwParcelas.Rows[i].Cells["NUMEROFATURA"].Value.ToString();
                            controlCaixa.modCaixa.ParNumeroParcela = Convert.ToInt32(dgwParcelas.Rows[i].Cells["NUMEROPARCELA"].Value);
                            controlCaixa.modCaixa.cliente.Pk_Codigo = Convert.ToInt32(tbxIDCliente.Text);
                            controlCaixa.modCaixa.ParValorBruto = Convert.ToDecimal(dgwParcelas.Rows[i].Cells["VALORFINAL"].Value);
                            controlCaixa.modCaixa.ParValorFatura = Convert.ToDecimal(dgwParcelas.Rows[i].Cells["VALORFINAL"].Value);
                            controlCaixa.modCaixa.ParValorPago = Convert.ToDecimal(dgwParcelas.Rows[i].Cells["VALORFINAL"].Value);
                            controlCaixa.modCaixa.ParValorRemanescente = 0;
                            string maisInfo = dgwParcelas.Rows[i].Cells["MAISINFO"].Value.ToString();
                            retornoVenda = controlCaixa.cIncluirRecebimentoAVistaCaixa();
                        }
                        #endregion

                        #region Efetua a Baixa de Estoque os Itens do Orçamento
                        controlOrcamento.modOrcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);                        
                       
                        string ccoCupom = "";
                        for (int i = 0; i < dt_DadosOrcamento.Rows.Count; i++)
                        {
                            int codigoProdutoVendido = Convert.ToInt32(dt_DadosOrcamento.Rows[i]["FK_NUMPRODUTO"].ToString());                         
                            decimal qtdVendida = Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["QUANTIDADE"].ToString().Replace(".", ","));
                            string descricao = dt_DadosOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Replace(".", ",");
                            string aplicacao = dt_DadosOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Replace(".", ",");                           

                            decimal valorCusto = 0;
                            decimal margemLucro = 0;
                            decimal valorVenda = Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["VALORUNIT"].ToString().Replace(".", ",")); ;
                            decimal valorTotal = Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["VALORTOTAL"].ToString().Replace(".", ",")); ;                            
                            string maisInfoMovProd = "Movimentacao feita pelo Caixa Momento Venda Pedido " + codigoPedido.ToString() + ". Codigo Caixa ID: " + codigoCaixa.ToString();

                            controlEstoque.modEstoque.caixa.IdCaixa = Convert.ToInt32(tbxPKIDCaixa.Text);
                            controlEstoque.modEstoque.cliente.Pk_Codigo = Convert.ToInt32(tbxIDCliente.Text);
                            controlEstoque.modEstoque.orcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                            controlEstoque.modEstoque.EntradaOuSaida = 2;
                            controlEstoque.modEstoque.caixa.IdentificacaoCaixa = tbxIdentificacaoCaixa.Text;
                            controlEstoque.modEstoque.MaisInfo = maisInfoMovProd;
                            controlEstoque.modEstoque.MargemLucro = margemLucro;
                            controlEstoque.modEstoque.produto.Pk_Codigo = codigoProdutoVendido;
                            controlEstoque.modEstoque.Qtd = qtdVendida;
                            controlEstoque.modEstoque.ValorCusto = valorCusto;
                            controlEstoque.modEstoque.ValorTotal = valorTotal;
                            controlEstoque.modEstoque.ValorVenda = valorVenda;

                            controlEstoque.cEfetuaMovimentoDoEstoque();
                            controlEstoque.cIncluirMovimentacaoEstoque();                    
                        }
                        #endregion

                        string cpfCnpj = "";
                        if (chkIncluirCPFNaNota.Checked)
                        {
                            cpfCnpj = tbxCpfCnpj.Text;
                        }        

                        //EMISSAO DO CUPOM FISCAL
                        iModItensOrcamento[] itens = controlOrcamento.cObterProdutosDeUmOrcamento();
                        ccoCupom = new clsECF().emitirCF(itens, cpfCnpj, formaPagtoPrincipal, tbxValorFinalComDescIncargos.Text);

                        MessageBox.Show("Venda e Operações Efetivadas com Sucesso!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();                        
                    }
                    else
                    {
                        MessageBox.Show("Houve uma Falha ao Fechar essa Venda. Caso o problema persista procure o suporte técnico.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//fim ifValidaDados()
            }//fim if MessageBox.Show
        }
        #endregion
        
        #region Método que Puxa todos os Produtos da Venda e calcula Descontos e Acrescimos
        public void recarregaProdutosComDescontosAcrescimos()
        {
            #region Carrega Produtos do Orcamento (Para Os que ainda não foram calculados... Fire!
                        
            if (dt_ItensOrcamento.Rows.Count == 0)
            {
                controlOrcamento.modOrcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                bool retorno = true; //converter ARRAY objetos controlOrcamento.cObterProdutosDeUmOrcamento(); //orc.retornaOrcamentos(Convert.ToInt32(tbxNumeroOrcamento.Text), frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
                iModItensOrcamento[] itens = controlOrcamento.cObterProdutosDeUmOrcamento();
                
                dt_ItensOrcamento.Rows.Clear();
                dt_ItensOrcamento.Columns.Clear();
                dt_ItensOrcamento.Clear();
                dt_ItensOrcamento.Columns.Add("Id_ProdVend");
                dt_ItensOrcamento.Columns.Add("DescricaoAplicacao");
                dt_ItensOrcamento.Columns.Add("PrecoVendaBanco");
                dt_ItensOrcamento.Columns.Add("PodeTerDesc");
                dt_ItensOrcamento.Columns.Add("DescontoPDV");
                dt_ItensOrcamento.Columns.Add("DescontoCaixa");
                dt_ItensOrcamento.Columns.Add("DescTotal");
                dt_ItensOrcamento.Columns.Add("Acrescimo");
                dt_ItensOrcamento.Columns.Add("ValorUnit");
                dt_ItensOrcamento.Columns.Add("Quantidade");
                dt_ItensOrcamento.Columns.Add("ValorTotal");
                dt_ItensOrcamento.Columns.Add("ValorFinal");

                decimal somaProdutosPodeTerDescontos = 0;
                decimal valorTotalDescontos = 0;
                decimal valorTotalAcrescimos = 0;

                //PREENCHE O DATATABLE DE ORCAMENTO COM O RETORNO DO BANCO
                foreach(iModItensOrcamento item in itens)
                {
                    DataRow DR = dt_DadosOrcamento.NewRow();
                    DR["PK_ID"] = item.PkIdItemVenda;
                    DR["FK_NUMPRODUTO"] = item.produto.Pk_Codigo;
                    DR["DESCRICAOAPLICACAO"] = item.DescricaoAplicacao;
                    DR["PRECOVENDABANCO"] = item.PrecoVendaBanco;
                    DR["DESCONTO"] = item.Desconto;
                    DR["ACRESCIMO"] = item.Acrescimo;
                    DR["VALORUNIT"] = item.ValorUnit;
                    DR["QUANTIDADE"] = item.Quantidade;
                    DR["VALORTOTAL"] = item.ValorTotal;                    
                    dt_DadosOrcamento.Rows.Add(DR);
                    DR = null;
                }

                for (int i = 0; i < dt_DadosOrcamento.Rows.Count; i++)
                {
                    DataRow DR = dt_ItensOrcamento.NewRow();
                    DR["Id_ProdVend"] = dt_DadosOrcamento.Rows[i]["PK_ID"].ToString().Trim();

                    DR["DescricaoAplicacao"] = dt_DadosOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Trim();

                    DR["PrecoVendaBanco"] = dt_DadosOrcamento.Rows[i]["PRECOVENDABANCO"].ToString().Trim().Replace(".", ",");
                    bool produtoSemDesconto = true;
                    DR["PodeTerDesc"] = "NÃO";

                    DR["DescontoPDV"] = dt_DadosOrcamento.Rows[i]["DESCONTO"].ToString().Trim().Replace(".", ",");
                    DR["DescontoCaixa"] = "0,0000"; //calcular
                    DR["DescTotal"] = dt_DadosOrcamento.Rows[i]["DESCONTO"].ToString().Trim().Replace(".", ",");
                    DR["Acrescimo"] = dt_DadosOrcamento.Rows[i]["ACRESCIMO"].ToString().Trim().Replace(".", ",");

                    valorTotalDescontos = valorTotalDescontos + Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["DESCONTO"].ToString());
                    valorTotalAcrescimos = valorTotalAcrescimos + Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["ACRESCIMO"].ToString());

                    DR["ValorUnit"] = dt_DadosOrcamento.Rows[i]["VALORUNIT"].ToString().Trim().Replace(".", ",");
                    DR["Quantidade"] = dt_DadosOrcamento.Rows[i]["QUANTIDADE"].ToString().Trim();                    
                    DR["ValorTotal"] = dt_DadosOrcamento.Rows[i]["VALORTOTAL"].ToString().Trim();                    
                    DR["ValorFinal"] = dt_DadosOrcamento.Rows[i]["VALORTOTAL"].ToString().Trim();

                    if (!produtoSemDesconto)
                    {
                        somaProdutosPodeTerDescontos = somaProdutosPodeTerDescontos + Convert.ToDecimal(dt_DadosOrcamento.Rows[i]["VALORTOTAL"].ToString());
                    }

                    dt_ItensOrcamento.Rows.Add(DR);
                    DR = null;
                }


                lvwItensOrcamento.Items.Clear();
                lvwItensOrcamento.Columns.Clear();

                lvwItensOrcamento.Columns.Add("", 0, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("IdItem", 52, HorizontalAlignment.Left);
                lvwItensOrcamento.Columns.Add("Desc_Prod", 145, HorizontalAlignment.Left);
                lvwItensOrcamento.Columns.Add("VendaBanco", 67, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("PodeTerDesc", 1, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescontoPDV", 78, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescontoCaixa", 82, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescTotal", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("Acrescimo", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("ValorUnit", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("Quantidade", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("ValorTotal", 74, HorizontalAlignment.Right);
                lvwItensOrcamento.Columns.Add("ValorFinal", 74, HorizontalAlignment.Right);
                lvwItensOrcamento.BeginUpdate();

                for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                {
                    lvwItensOrcamento.Items.Add("");
                    if (dt_ItensOrcamento.Rows[i]["DescontoPDV"].ToString() == "0,0000" && dt_ItensOrcamento.Rows[i]["DescontoCaixa"].ToString() == "0,0000" && dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,0000")
                    {
                        if (i % 2 == 0)
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,0000" || dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,00")
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.LightBlue;
                        }
                        else
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.LightPink;
                        }
                    }

                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Id_ProdVend"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescricaoAplicacao"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["PrecoVendaBanco"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["PodeTerDesc"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescontoPDV"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescontoCaixa"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescTotal"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorUnit"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Quantidade"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorTotal"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorFinal"].ToString());
                }//fim for
                lvwItensOrcamento.EndUpdate();

                decimal descontoTotal = 0;
                decimal acrescimoTotal = 0;
                decimal valorTotalFor = 0;
                decimal valorFinalFor = 0;

                //Carrega Totalizadores...
                for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                {
                    descontoTotal = descontoTotal + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["DescTotal"].ToString());
                    acrescimoTotal = acrescimoTotal + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString());
                    valorTotalFor = valorTotalFor + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ValorTotal"].ToString());
                    valorFinalFor = valorFinalFor + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ValorTotal"].ToString());
                }//fim do FOR que carrega os Totalizadores

                tbxDescontoValor.Text = contas.newValidaAjustaArredonda4CasasDecimais(descontoTotal.ToString());
                tbxEncargos.Text = contas.newValidaAjustaArredonda2CasasDecimais(acrescimoTotal.ToString());
                tbxValorFinalComDescIncargos.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorFinalFor.ToString());


                if (tbxValorVenda.Text != "" && tbxValorVenda.Text != "0,00" && tbxValorVenda.Text != "0,0000")
                {//previne que ele recalcule o valor da venda por uma segunda vez, calcula apenas na primeira o valor bruto da venda...
                    //depois se algum outro método chamar esse método, o valor não será recalculado evitando desconto sobre desconto
                    //verifica se houve algum desconto vindo do PDV... se houve, altera os valores do pedido
                    if (valorTotalDescontos != 0)
                    {
                        decimal valorTotalVenda = Convert.ToDecimal(tbxValorVenda.Text);
                        valorTotalVenda = valorTotalVenda + valorTotalDescontos;
                        tbxValorVenda.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorTotalVenda.ToString());
                    }
                    else
                    {
                        tbxValorVenda.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorTotalFor.ToString());
                    }
                }
                calculaValores();
            }//fim if dt_DadosOrcamento
            #endregion

            #region Faz o Update Dentro do ListView do Form PDV
            else
            {
                //decimal valorTotalDescontos = 0;
                //decimal valorTotalAcrescimos = 0;

                lvwItensOrcamento.Items.Clear();
                lvwItensOrcamento.Columns.Clear();

                lvwItensOrcamento.Columns.Add("", 0, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("IdItem", 52, HorizontalAlignment.Left);
                lvwItensOrcamento.Columns.Add("Desc_Prod", 145, HorizontalAlignment.Left);
                lvwItensOrcamento.Columns.Add("VendaBanco", 67, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("PodeTerDesc", 74, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescontoPDV", 78, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescontoCaixa", 82, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("DescTotal", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("Acrescimo", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("ValorUnit", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("Quantidade", 69, HorizontalAlignment.Center);
                lvwItensOrcamento.Columns.Add("ValorTotal", 74, HorizontalAlignment.Right);
                lvwItensOrcamento.Columns.Add("ValorFinal", 74, HorizontalAlignment.Right);
                lvwItensOrcamento.BeginUpdate();

                for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                {
                    lvwItensOrcamento.Items.Add("");
                    if (dt_ItensOrcamento.Rows[i]["DescontoPDV"].ToString() == "0,0000" && dt_ItensOrcamento.Rows[i]["DescontoCaixa"].ToString() == "0,0000" && dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,0000")
                    {
                        if (i % 2 == 0)
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,0000" || dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString() == "0,00")
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.LightBlue;
                        }
                        else
                        {
                            lvwItensOrcamento.Items[i].BackColor = Color.LightPink;
                        }
                    }

                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Id_ProdVend"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescricaoAplicacao"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["PrecoVendaBanco"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["PodeTerDesc"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescontoPDV"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescontoCaixa"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DescTotal"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorUnit"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["Quantidade"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorTotal"].ToString());
                    lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ValorFinal"].ToString());
                }//fim for
                lvwItensOrcamento.EndUpdate();

                decimal descontoTotal = 0;
                decimal acrescimoTotal = 0;
                decimal valorTotalFor = 0;
                decimal valorFinalFor = 0;


                //Carrega Totalizadores...
                for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                {
                    descontoTotal = descontoTotal + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["DescTotal"].ToString());
                    acrescimoTotal = acrescimoTotal + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["Acrescimo"].ToString());
                    valorTotalFor = valorTotalFor + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ValorTotal"].ToString());
                    valorFinalFor = valorFinalFor + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ValorFinal"].ToString());
                    //valorTotalDescontos = valorTotalDescontos + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["DescTotal"].ToString());
                }//fim do FOR que carrega os Totalizadores


                tbxDescontoValor.Text = contas.newValidaAjustaArredonda4CasasDecimais(descontoTotal.ToString());
                tbxEncargos.Text = contas.newValidaAjustaArredonda2CasasDecimais(acrescimoTotal.ToString());
                tbxValorFinalComDescIncargos.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorFinalFor.ToString());
                calculaValores();
            }//fim if dt_DadosOrcamento
            #endregion
        }
        #endregion
        #endregion

        #region **************EVENTOS***************
        #region Evento do Botao Limpar Formas de Pagamento
        private void btnRemoverFormaPagamento_Click(object sender, EventArgs e)
        {
            carregaDataGridView();
            recarregaProdutosComDescontosAcrescimos();
            calculaValores();
        }
        #endregion
        
        #region Evento do Botao Fechar Venda
        private void btnFecharVenda_Click(object sender, EventArgs e)
        {
            efetuarFechamentoVenda();
        }
        #endregion

        #region Evento do Duplo Clique sobre o Produto no Grid para Customizar o Produto
        private void lvwItensOrcamento_DoubleClick(object sender, EventArgs e)
        {
            string indiceProduto = lvwItensOrcamento.SelectedItems[0].SubItems[1].Text.ToString().Trim();
            for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
            {
                if (indiceProduto == dt_ItensOrcamento.Rows[i]["Id_ProdVend"].ToString())
                {
                    recarregaProdutosComDescontosAcrescimos();
                }//fim do if indiceProduto == dt_ItensOrcamento
            }//fim do For
        }//fim private void
        #endregion        
        
        #region Eventos dos Botões de Forma de Pagamento a Vista (seleção rápida)
        private void btnFormaPagto1_Click(object sender, EventArgs e)
        {
            carregaDataGridView();
            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            for (int i = 0; i < dt_DadosFormasPagto.Rows.Count; i++)
            {
                if (formaPagtoAVista1.ToString() == dt_DadosFormasPagto.Rows[i]["ID_CATEGORIAPLANO"].ToString())
                {
                    descontoFixoForma = Convert.ToDecimal(dt_DadosFormasPagto.Rows[i]["DESCONTO_FIXO"].ToString());
                    acrescimoFixoForma = Convert.ToDecimal(dt_DadosFormasPagto.Rows[i]["ACRESCIMO_FIXO"].ToString());
                }
            }

            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto1.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista1;

            //insere a formade pagamento

            int contadorParcelas = 1;
            
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            
            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();
            
            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto2_Click(object sender, EventArgs e)
        {
            carregaDataGridView();

            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto2.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista2;

            //insere a formade pagamento

            int contadorParcelas = 1;           
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            
            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto3_Click(object sender, EventArgs e)
        {
            carregaDataGridView();

            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto3.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista3;

            //insere a formade pagamento

            int contadorParcelas = 1;           
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            //if (nudNumeroParcelas.Value > 1)
            //{
            //    valorBrutoParcela = valorBrutoParcela / Convert.ToInt32(contadorParcelas);
            //}

            ////PORRA 30/06/2012 E EU AQUI NESSA MERDA, SÃO 07:16 DA MANHÃ...

            //if (porcentagemAcrescimoParcelas > 0)
            //{
            //    if (nudNumeroParcelas.Value > Convert.ToInt32(tbxIsentaAcrescimosAte.Text))
            //    {
            //        acrescimoPorParcela = valorBrutoParcela * porcentagemAcrescimoParcelas / 100;
            //    }
            //}
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcelaPodeTerDesc * porcentagemDescontoParcelas / 100;
            //}


            ////Antes desconto 20/02/2012 - Fernando
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcela * porcentagemDescontoParcelas / 100;
            //}

            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto4_Click(object sender, EventArgs e)
        {
            carregaDataGridView();


            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto4.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista4;

            //insere a formade pagamento

            int contadorParcelas = 1;          
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            //if (nudNumeroParcelas.Value > 1)
            //{
            //    valorBrutoParcela = valorBrutoParcela / Convert.ToInt32(contadorParcelas);
            //}

            ////PORRA 30/06/2012 E EU AQUI NESSA MERDA, SÃO 07:16 DA MANHÃ...

            //if (porcentagemAcrescimoParcelas > 0)
            //{
            //    if (nudNumeroParcelas.Value > Convert.ToInt32(tbxIsentaAcrescimosAte.Text))
            //    {
            //        acrescimoPorParcela = valorBrutoParcela * porcentagemAcrescimoParcelas / 100;
            //    }
            //}
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcelaPodeTerDesc * porcentagemDescontoParcelas / 100;
            //}


            ////Antes desconto 20/02/2012 - Fernando
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcela * porcentagemDescontoParcelas / 100;
            //}

            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto5_Click(object sender, EventArgs e)
        {
            carregaDataGridView();


            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto5.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista5;

            //insere a formade pagamento

            int contadorParcelas = 1;          
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            
            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto6_Click(object sender, EventArgs e)
        {
            carregaDataGridView();


            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto6.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista6;

            //insere a formade pagamento

            int contadorParcelas = 1;
          
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            //if (nudNumeroParcelas.Value > 1)
            //{
            //    valorBrutoParcela = valorBrutoParcela / Convert.ToInt32(contadorParcelas);
            //}

            ////PORRA 30/06/2012 E EU AQUI NESSA MERDA, SÃO 07:16 DA MANHÃ...

            //if (porcentagemAcrescimoParcelas > 0)
            //{
            //    if (nudNumeroParcelas.Value > Convert.ToInt32(tbxIsentaAcrescimosAte.Text))
            //    {
            //        acrescimoPorParcela = valorBrutoParcela * porcentagemAcrescimoParcelas / 100;
            //    }
            //}
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcelaPodeTerDesc * porcentagemDescontoParcelas / 100;
            //}


            ////Antes desconto 20/02/2012 - Fernando
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcela * porcentagemDescontoParcelas / 100;
            //}

            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto7_Click(object sender, EventArgs e)
        {
            carregaDataGridView();


            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto7.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista7;

            //insere a formade pagamento

            int contadorParcelas = 1;
            
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            //if (nudNumeroParcelas.Value > 1)
            //{
            //    valorBrutoParcela = valorBrutoParcela / Convert.ToInt32(contadorParcelas);
            //}

            ////PORRA 30/06/2012 E EU AQUI NESSA MERDA, SÃO 07:16 DA MANHÃ...

            //if (porcentagemAcrescimoParcelas > 0)
            //{
            //    if (nudNumeroParcelas.Value > Convert.ToInt32(tbxIsentaAcrescimosAte.Text))
            //    {
            //        acrescimoPorParcela = valorBrutoParcela * porcentagemAcrescimoParcelas / 100;
            //    }
            //}
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcelaPodeTerDesc * porcentagemDescontoParcelas / 100;
            //}


            ////Antes desconto 20/02/2012 - Fernando
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcela * porcentagemDescontoParcelas / 100;
            //}

            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }

        private void btnFormaPagto8_Click(object sender, EventArgs e)
        {
            carregaDataGridView();


            decimal descontoFixoForma = 0;
            decimal acrescimoFixoForma = 0;
            
            recarregaProdutosComDescontosAcrescimos();
            string descricaoForma = btnFormaPagto8.Text;
            lblFormaPagamentoSelecionada.Text = "Você selecionou a Forma de Pagamento " + descricaoForma;
            formaPagtoAVistaSelecionada = formaPagtoAVista8;

            //insere a formade pagamento

            int contadorParcelas = 1;           
            decimal acrescimoPorParcela = 0;
            decimal descontoPorParcela = 0;

            decimal valorBrutoParcela = Convert.ToDecimal(tbxValorFinalComDescIncargos.Text);
            decimal valorBrutoParcelaPodeTerDesc = Convert.ToDecimal(0);
            //if (nudNumeroParcelas.Value > 1)
            //{
            //    valorBrutoParcela = valorBrutoParcela / Convert.ToInt32(contadorParcelas);
            //}

            ////PORRA 30/06/2012 E EU AQUI NESSA MERDA, SÃO 07:16 DA MANHÃ...

            //if (porcentagemAcrescimoParcelas > 0)
            //{
            //    if (nudNumeroParcelas.Value > Convert.ToInt32(tbxIsentaAcrescimosAte.Text))
            //    {
            //        acrescimoPorParcela = valorBrutoParcela * porcentagemAcrescimoParcelas / 100;
            //    }
            //}
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcelaPodeTerDesc * porcentagemDescontoParcelas / 100;
            //}


            ////Antes desconto 20/02/2012 - Fernando
            //if (porcentagemDescontoParcelas > 0)
            //{
            //    descontoPorParcela = valorBrutoParcela * porcentagemDescontoParcelas / 100;
            //}

            DateTime dataAgora = DateTime.Now;

            for (int i = 0; i < contadorParcelas; i++)
            {

                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contasMatem.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                int indiceDataGrid = dgwParcelas.Rows.Count;
                dgwParcelas.Rows.Add();
                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROFATURA"].Value = tbxNumeroOrcamento.Text + "-P1";
                if (contadorParcelas == 1)
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = descricaoForma;
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = descricaoForma;
                }
                else
                {
                    //dgwParcelas.Rows[indiceDataGrid].Cells["NUMERODOCUMENTO"].Value = "";
                    //dgwParcelas.Rows[indiceDataGrid].Cells["INFODOCUMENTO"].Value = "";
                }
                dgwParcelas.Rows[indiceDataGrid].Cells["FORMA_PAGTO"].Value = formaPagtoAVistaSelecionada;
                //frmNewFecharVenda.dgwParcelas.Rows[indiceDataGrid].Cells["BANCO_DESTINO"].Value = 1;
                dgwParcelas.Rows[indiceDataGrid].Cells["DATAEMISSAO"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                //A PARTIR DAQUI PRECISA FAZER AS CONTAS DE DATA DE VENCIMENTO, DE VALOR BRUTO, ACRESCIMO, DESCONTOS E VALOR FINAL
                //ESCREVI ISSO NO DIA 29/06/2012 AS 20:55 DA NOITE, ESTOU VIOLENTAMENTE CANSADO... AFF, CONTAS PRA AMANHÃ! FER.

                //if (i == 0)
                //{
                //    if (!chkPrimeiraParcelaAVista.Checked)
                //    {
                //        dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));
                //    }
                //}

                dgwParcelas.Rows[indiceDataGrid].Cells["DATA_VENCIMENTO"].Value = dataAgora;
                //dataAgora = dataAgora.AddDays(Convert.ToDouble(tbxIntervaloParcelas.Text));

                dgwParcelas.Rows[indiceDataGrid].Cells["NUMEROPARCELA"].Value = "1";
                //dgwParcelas.Rows[indiceDataGrid].Cells["VALORBRUTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorBrutoParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["ACRESCIMO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(acrescimoPorParcela.ToString());
                //dgwParcelas.Rows[indiceDataGrid].Cells["DESCONTO"].Value = contas.newValidaAjustaArredonda4CasasDecimais(descontoPorParcela.ToString());

                decimal valorFinalParcela = valorBrutoParcela + acrescimoPorParcela - descontoPorParcela;

                dgwParcelas.Rows[indiceDataGrid].Cells["VALORFINAL"].Value = contas.newValidaAjustaArredonda4CasasDecimais(valorFinalParcela.ToString());
                dgwParcelas.Rows[indiceDataGrid].Cells["MAISINFO"].Value = "";
            }//fim For
            dgwParcelas.Refresh();

            //if (tbxPorcentagemDesconto.Text != "0,00")
            //{
            //    if (!jaFoiInseridoPorcentagem)
            //    {
            //        frmNewFecharVenda.descontoTotal = frmNewFecharVenda.descontoTotal + Convert.ToDecimal(tbxPorcentagemDesconto.Text);
            //    }
            //}

            //if (tbxValorAcrescimoNaForma.Text != "0,00")
            //{
            //    frmNewFecharVenda.acrescimoTotal = frmNewFecharVenda.acrescimoTotal + Convert.ToDecimal(tbxValorAcrescimoNaForma.Text);
            //}
            calculaValores();
        }
        #endregion
        #endregion
    }//fim classe
}//fim namespace
