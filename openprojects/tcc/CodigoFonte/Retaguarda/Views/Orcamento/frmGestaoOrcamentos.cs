using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Gestoes;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Utilitarios;
using System.Drawing;
using System.Collections.Generic;
using DllFuturaDataTCC;
using DllFuturaDataCriptografia;
using System.Threading;
using DllFuturaDataTCC.Controllers;
using System.Collections;
using FuturaDataTCC.Views.Gestoes;
using FuturaDataTCC.Relatorios.Orcamento;
using DllFuturaDataTCC.Models;

namespace FuturaDataTCC.Views.Orcamento
{
    public partial class frmGestaoOrcamentos : Form
    {
        #region Atributos (Variaveis) do Form (View) frmOrcamento

        frmInicializacao frmInicial;
        public int indice = 0;
        int opcao = 0;

        iConOrcamento controllerOrcamento = new iConOrcamento();
        iConCliente controllerCliente = new iConCliente();
        iConProduto controllerProduto = new iConProduto();        
        public bool permitirPrecoRevendaCliente = true;

        clsValidacaoDeStrings valida = new clsValidacaoDeStrings();
        clsNewContasMatematicas contas = new clsNewContasMatematicas();

        public DataTable dt_DadosProdutos = new DataTable(); //usado apenas aqui dentro da view, para pesquisa de produtos

        frmTelaPrincipal princ;
        string nomeECnpjEmpresa;        
        iModOrcamento[] arrayOrcamentos;        
        iModProduto[] arrayProdutos;
        public DataTable dt_ItensOrcamento = new DataTable();
        #endregion

        #region Construtor (inicializador) do Form
        public frmGestaoOrcamentos(frmInicializacao frmInicia, frmTelaPrincipal prin, string nomeECnpjEmpres, string orcamentoMostrarEntrada)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            
            //esse DataTable será usado APENAS para armazenar os itens de um novo orçamento, não será enviado a lugar algum,
            //será convertido pra objeto iModItensOrcamento ao ser enviado para Controller/Model/Dao
            dt_ItensOrcamento.Columns.Add("PK_ID");
            dt_ItensOrcamento.Columns.Add("FK_NUMPRODUTO");
            dt_ItensOrcamento.Columns.Add("ITEM");
            dt_ItensOrcamento.Columns.Add("CODIGOFABRIC");
            dt_ItensOrcamento.Columns.Add("DESCRICAOAPLICACAO");
            dt_ItensOrcamento.Columns.Add("PRECOVENDABANCO");
            dt_ItensOrcamento.Columns.Add("VALORTOTAL_SEMDESC_OU_ACRE");
            dt_ItensOrcamento.Columns.Add("VALORUNITARIO");
            dt_ItensOrcamento.Columns.Add("QUANTIDADE");
            dt_ItensOrcamento.Columns.Add("VALORTOTAL");
            dt_ItensOrcamento.Columns.Add("DESCONTO");
            dt_ItensOrcamento.Columns.Add("ACRESCIMO");

            carregaListViewItensOrcamento();
            recarregaDadosPrincipais();
            indice = arrayOrcamentos.Length-1;
            
            carregarDados();
            princ = prin;
            nomeECnpjEmpresa = nomeECnpjEmpres;
            carregaListViewPesquisa();
            if (orcamentoMostrarEntrada.Trim() != "")
            {
                tbcVendas.SelectedTab = tbpDadosGerais;
            }           
            
            carregaListViewItensOrcamento();            
            carregaOrcamentosEmAberto();
            carregarDados();            
        }
        #endregion Variaveis Internas e Construtor da Classe

        #region **************MÉTODOS***************
                
        #region Desenha Produtos de um Orcamento
        public void desenhaProdutosDeUmOrcamento()
        {
            carregaListViewItensOrcamento();
            lvwItensOrcamento.BeginUpdate();
            decimal valorTotalVenda = 0;
            
            for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
            {
                lvwItensOrcamento.Items.Add("");
                if (i % 2 == 0)
                {
                    lvwItensOrcamento.Items[i].BackColor = Color.WhiteSmoke;
                }
                else
                {
                    lvwItensOrcamento.Items[i].BackColor = Color.White;
                }
                
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["FK_NUMPRODUTO"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ITEM"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                lvwItensOrcamento.Items[i].SubItems.Add(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());
                valorTotalVenda = valorTotalVenda + Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString());                
            }//fim for
            lvwItensOrcamento.EndUpdate();

            lblQuantidadeProdutos.Text = "Quantidade de Produtos Inseridos: " + dt_ItensOrcamento.Rows.Count;
            tbxValorFinal.Text = contas.newValidaAjustaArredonda2CasasDecimais(valorTotalVenda.ToString());
        }
        #endregion
        
        #region Método Carrega DataTable Produtos

        public void carregaDtProdutos()
        {            
            arrayProdutos = controllerProduto.cObterProduto();

            //O DataTable dt_DadosProdutos é usado apenas para pesquisa de produtos na tela adicionar itens, não é enviado a nenhum lugar nem sai da view
            dt_DadosProdutos.Columns.Add("DESCRICAO");
            dt_DadosProdutos.Columns.Add("CODIGOFABRIC");
            dt_DadosProdutos.Columns.Add("CODIGOORIGINAL");            
            dt_DadosProdutos.Columns.Add("PRECOVENDA");
            dt_DadosProdutos.Columns.Add("QTD_ATUAL");
            dt_DadosProdutos.Columns.Add("APLICACAO");
            dt_DadosProdutos.Columns.Add("PK_CODIGOSIST");
            dt_DadosProdutos.Columns.Add("POSSUI_IMAGEM");

            for (int i = 0; i < arrayProdutos.Length; i++)
            {
                //preencho o DataTable com os Produtos
                DataRow DR = dt_DadosProdutos.NewRow();
                DR["DESCRICAO"] = arrayProdutos[i].Descricao;
                DR["CODIGOFABRIC"] = arrayProdutos[i].CodigoFabric;
                DR["CODIGOORIGINAL"] = arrayProdutos[i].CodigoOriginal;                
                DR["PRECOVENDA"] = arrayProdutos[i].PrecoVenda.ToString();
                DR["QTD_ATUAL"] = arrayProdutos[i].QtdAtual.ToString();
                DR["APLICACAO"] = arrayProdutos[i].Aplicacao.ToString();
                DR["PK_CODIGOSIST"] = arrayProdutos[i].Pk_Codigo.ToString();
                DR["POSSUI_IMAGEM"] = arrayProdutos[i].PossuiImagem.ToString();
                dt_DadosProdutos.Rows.Add(DR);
                DR = null;
            }
        }

        #endregion Método Carrega DataTable Produtos

        #region Carrega Orcamentos em Aberto

        public void carregaOrcamentosEmAberto()
        {
            arrayOrcamentos = controllerOrcamento.cObterOrcamentos();
        }

        #endregion Carrega Orcamentos e Ordens de Servico em Aberto

        #region Método Trava Campos

        public void travaCampos()
        {
            clsFuncoes.TravaControles(tbpDadosGerais);            
        }

        #endregion Método Trava Campos

        #region Método Destrava Campos

        public void destravaCampos()
        {
            clsFuncoes.DestravaControles(tbpDadosGerais);
            
            tbxNumeroOrcamento.ReadOnly = true;
        }

        #endregion Método Destrava Campos

        #region Método "travaBotaoOpcoes"

        //
        private void travaBotaoOpcoes()
        {
            btnAlterar.Enabled = false;
            btnCadAnterior.Enabled = false;

            btnNovo.Enabled = false;
            btnProximoCad.Enabled = false;
            btnPrimeiroCad.Enabled = false;
            btnUltimoCad.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;            
        }

        //fim travaBotaoOpcoes

        #endregion Método "travaBotaoOpcoes"

        #region Método "destravaBotaoOpcoes"
        //
        private void DestravaBotaoOpcoes()
        {
            btnAlterar.Enabled = true;
            btnCadAnterior.Enabled = true;

            btnNovo.Enabled = true;
            btnProximoCad.Enabled = true;
            btnPrimeiroCad.Enabled = true;
            btnUltimoCad.Enabled = true;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
        }

        //fim código DEstravaBotaoOpcoes

        #endregion Método "destravaBotaoOpcoes"

        #region Método Limpa Controles

        public void limpaControles()
        {
            clsFuncoes.LimpaControles(tbpDadosGerais);            
            
            //int numeroUltimoOrct = Convert.ToInt32(orct.retornaNumeroUltimoOrcamento(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost));
            //numeroUltimoOrct++;
            tbxNumeroOrcamento.Text = "";                        
            tbxValorFinal.Text = "0,0000";            
        }

        #endregion Método Limpa Controles

        #region Método ListView de Itens do Orcamento

        public void carregaListViewItensOrcamento()
        {
            #region Cria e Formata as Colunas dentro do ListView
            lvwItensOrcamento.Items.Clear();
            lvwItensOrcamento.Columns.Clear();
            lvwItensOrcamento.Refresh();
            lvwItensOrcamento.Columns.Add("", 0, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("IDProd", 50, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("Item", 50, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("Codigo Fabric", 110, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("Descricao-Aplicacao", 380, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("PrecoSistema", 80, HorizontalAlignment.Left);
            lvwItensOrcamento.Columns.Add("PrecoVendido", 80, HorizontalAlignment.Center);
            lvwItensOrcamento.Columns.Add("Quant.", 60, HorizontalAlignment.Center);
            lvwItensOrcamento.Columns.Add("PrecoFinal", 100, HorizontalAlignment.Center);
            lvwItensOrcamento.Columns.Add("Desconto", 80, HorizontalAlignment.Center);
            lvwItensOrcamento.Columns.Add("Acrescimo", 80, HorizontalAlignment.Center);
            #endregion Cria e Formata as Colunas dentro do ListView
        }

        #endregion Método ListView de Itens do Orcamento

        #region Método ListView de PESQUISA

        public void carregaListViewPesquisa()
        {
            #region Cria e Formata as Colunas dentro do ListView
            lvwPesquisaClientes.Items.Clear(); //limpo o ListView para mostrar nova consulta
            lvwPesquisaClientes.Columns.Clear();

            lvwPesquisaClientes.Columns.Add("", 0, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("NumCot", 60, HorizontalAlignment.Left);
            lvwPesquisaClientes.Columns.Add("Data", 110, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Nome do Cliente", 260, HorizontalAlignment.Left);
            lvwPesquisaClientes.Columns.Add("Valor Final", 100, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Status", 80, HorizontalAlignment.Left);            
            #endregion Cria e Formata as Colunas dentro do ListView

            #region Faz o Update Dentro do ListView

            lvwPesquisaClientes.BeginUpdate();
            int indiceListView = 0;
            foreach (iModOrcamento orca in arrayOrcamentos)
            {
                lvwPesquisaClientes.Items.Add("");
                if (indiceListView % 2 == 0)
                {
                    lvwPesquisaClientes.Items[indiceListView].BackColor = Color.White;
                }
                else
                {
                    lvwPesquisaClientes.Items[indiceListView].BackColor = Color.WhiteSmoke;
                }
                lvwPesquisaClientes.Items[indiceListView].SubItems.Add(orca.PkCodigo.ToString());
                lvwPesquisaClientes.Items[indiceListView].SubItems.Add(orca.DataOrc.ToString());
                lvwPesquisaClientes.Items[indiceListView].SubItems.Add(orca.cliente.Nome); //retorna o nome do cliente que será apenas 1 dentro do objeto Orcamento
                lvwPesquisaClientes.Items[indiceListView].SubItems.Add(orca.ValorFinal.ToString());
                lvwPesquisaClientes.Items[indiceListView].SubItems.Add(orca.Status);
                indiceListView++;
            }
            lvwPesquisaClientes.EndUpdate();

            #endregion Faz o Update Dentro do ListView
        }

        #endregion Método ListView de PESQUISA

        #region Método Recarrega Dados Principais

        public void recarregaDadosPrincipais()
        {
            carregaDtProdutos();
            carregaComboBoxClientes();
            carregaOrcamentosEmAberto();
        }

        #endregion Método Recarrega Dados Principais

        #region Método Carrega Dados e Verifica se pode Fechar, Emitir NFe ou Visualizar

        public void carregarDados()
        {
            if (arrayOrcamentos.Length > 0)
            {
                if (indice == -1)
                {
                    indice = 0;
                }

                tbxNumeroOrcamento.Text = arrayOrcamentos[indice].PkCodigo.ToString();

                cbbNomeCliente.SelectedValue = Convert.ToInt32(arrayOrcamentos[indice].cliente.Pk_Codigo.ToString());

                tbxMaisInformacoes.Text = arrayOrcamentos[indice].InfoAdicional;
                tbxValorFinal.Text = arrayOrcamentos[indice].ValorFinal.ToString();

                carregaItensDoOrcamento();
                btnAlterar.Enabled = true;
                DestravaBotaoOpcoes();
                travaCampos(); //ei, asswipe - o travaCampos irá travar inclusive todos os buttons
            }
            else
            {
                tbxNumeroOrcamento.Clear();
                DestravaBotaoOpcoes();
                travaCampos();
                btnAlterar.Enabled = false;
            }
        }

        #endregion Método Carrega Dados e Verifica se pode Fechar, Emitir NFe ou Visualizar

        #region Método Carrega Itens do Respectivo Orcamento/OS e Verifica se Pode Fechar e emitir NFe ou Visualizar

        public void carregaItensDoOrcamento()
        {
            if (contas.verificaSeEInteiro(tbxNumeroOrcamento.Text))
            {
                controllerOrcamento.modOrcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                iModItensOrcamento[] itensOrc = controllerOrcamento.cObterProdutosDeUmOrcamento();
                dt_ItensOrcamento.Rows.Clear(); //limpa o DataTable para armazenar os novos itens do Orcamento
                
                foreach (iModItensOrcamento itemOrc in itensOrc)
                {
                    DataRow DR = dt_ItensOrcamento.NewRow();
                    DR["PK_ID"] = itemOrc.PkIdItemVenda;
                    DR["FK_NUMPRODUTO"] = itemOrc.produto.Pk_Codigo;
                    DR["ITEM"] = itemOrc.NumeroItem;
                    DR["CODIGOFABRIC"] = itemOrc.CodigoFabric;
                    DR["DESCRICAOAPLICACAO"] = itemOrc.DescricaoAplicacao;                    
                    DR["PRECOVENDABANCO"] = itemOrc.PrecoVendaBanco;
                    DR["VALORTOTAL_SEMDESC_OU_ACRE"] = itemOrc.ValorTotalSemDescAcre;
                    DR["VALORUNITARIO"] = itemOrc.ValorUnit;
                    DR["QUANTIDADE"] = itemOrc.Quantidade;
                    DR["VALORTOTAL"] = itemOrc.ValorTotal;
                    DR["DESCONTO"] = itemOrc.Desconto;
                    DR["ACRESCIMO"] = itemOrc.Acrescimo;
                    dt_ItensOrcamento.Rows.Add(DR);
                    DR = null;
                }

                #region Faz o Update Dentro do ListView do Form PDV
                decimal totalFinal = 0;
                lvwItensOrcamento.BeginUpdate();
                lvwItensOrcamento.Items.Clear();
                int i2 = 0;

                foreach(iModItensOrcamento itemOrc in itensOrc)
                {
                    lvwItensOrcamento.Items.Add("");
                    if (i2 % 2 == 0)
                    {
                        lvwItensOrcamento.Items[i2].BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lvwItensOrcamento.Items[i2].BackColor = Color.White;
                    }

                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.produto.Pk_Codigo.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.NumeroItem.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.CodigoFabric);
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.DescricaoAplicacao);
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.PrecoVendaBanco.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.ValorUnit.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.Quantidade.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.ValorTotal.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.Desconto.ToString());
                    lvwItensOrcamento.Items[i2].SubItems.Add(itemOrc.Acrescimo.ToString());
                    totalFinal = totalFinal + Convert.ToDecimal(itemOrc.ValorTotal);
                    i2++;
                }
                lvwItensOrcamento.EndUpdate();
                lvwItensOrcamento.Refresh();
                tbxValorFinal.Text = totalFinal.ToString();
                tbxValorFinal.Refresh();
                lblQuantidadeProdutos.Text = "Quantidade de Produtos Inseridos: " + itensOrc.Length.ToString();

                #endregion Faz o Update Dentro do ListView do Form PDV
            }
        }

        #endregion Método Carrega Itens do Respectivo Orcamento/OS e Verifica se Pode Fechar e emitir NFe ou Visualizar
        
        #region Método Carrega Dados das ComboBox
        
        public void carregaComboBoxClientes()
        {
            cbbNomeCliente.DataSource = null;
            cbbNomeCliente.Items.Clear();
            //rezando pelo amor de deus para que a ComboBox aceite um objeto do tipo classe como Datasource, era DataTable antes
            iModCliente[] arrayClientes = controllerCliente.cObterCliente();            
            if (arrayClientes != null)
            {
                //dt_Clientes = controllerCliente.modCliente.Ds_DadosRetorno.Tables[0]; //contem os dados de todos os clientes
                cbbNomeCliente.DataSource = arrayClientes;
                cbbNomeCliente.DisplayMember = "nome".Trim().ToString();
                cbbNomeCliente.ValueMember = "pk_Codigo".Trim().ToString();
            }
        }
        #endregion Método Carrega Dados das ComboBox

        #region Método Executa Operacao

        public void executaOperacao()
        {
            if (validaOrcamento())
            {
                #region Opcao = 1 (Insere novo Orçamento ou Comanda)

                if (opcao == 1)
                {
                    iConOrcamento controllerOrcamento = new iConOrcamento();
                    controllerOrcamento.modOrcamento.cliente.Pk_Codigo = Convert.ToInt32(cbbNomeCliente.SelectedValue);
                    controllerOrcamento.modOrcamento.InfoAdicional = tbxMaisInformacoes.Text;
                    controllerOrcamento.modOrcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                    controllerOrcamento.modOrcamento.ValorFinal = Convert.ToDecimal(tbxValorFinal.Text);
                    //montar o array de itens de orcamento no momento de enviar

                    iModItensOrcamento[] itensOrcamento = new iModItensOrcamento[dt_ItensOrcamento.Rows.Count];

                    for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                    {
                        itensOrcamento[i] = new iModItensOrcamento();
                        itensOrcamento[i].produto.Pk_Codigo = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["FK_NUMPRODUTO"].ToString());
                        itensOrcamento[i].PkIdItemVenda = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["PK_ID"].ToString());
                        itensOrcamento[i].NumeroItem = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["ITEM"].ToString());
                        itensOrcamento[i].CodigoFabric = dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString();
                        itensOrcamento[i].DescricaoAplicacao = dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString();
                        itensOrcamento[i].PrecoVendaBanco = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString());
                        itensOrcamento[i].ValorUnit = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString());
                        itensOrcamento[i].Quantidade = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString());
                        itensOrcamento[i].ValorTotal = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString());
                        itensOrcamento[i].Desconto = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                        itensOrcamento[i].Acrescimo = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());
                    }

                    controllerOrcamento.modOrcamento.itensOrcamento = itensOrcamento;
                    int numeroOrcamentoGerado = controllerOrcamento.cInsereOrcamento();
                    if (numeroOrcamentoGerado == 0)
                    {
                        MessageBox.Show("Erro ao inserir o Orçamento. Um LOG de erros foi gerado, se o problema persistir procure a equipe Futuradata!", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                    
                    else
                    {
                        MessageBox.Show("Orçamento inserido com sucesso sobre o número: " + numeroOrcamentoGerado.ToString(), "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//fim if opcao = 0

                #endregion Opcao = 1 (Insere novo Orçamento ou Comanda)

                #region Opcao = 2 (Atualiza Orcamento)

                // alterar
                if (opcao == 2)
                {
                    #region Alterar Dados Orcamento
                    iConOrcamento controllerOrcamento = new iConOrcamento();                    
                    controllerOrcamento.modOrcamento.cliente.Pk_Codigo = Convert.ToInt32(cbbNomeCliente.SelectedValue);
                    controllerOrcamento.modOrcamento.InfoAdicional = tbxMaisInformacoes.Text;
                    controllerOrcamento.modOrcamento.PkCodigo = Convert.ToInt32(tbxNumeroOrcamento.Text);
                    controllerOrcamento.modOrcamento.ValorFinal = Convert.ToDecimal(tbxValorFinal.Text);
                    
                    iModItensOrcamento[] itensOrcamento = new iModItensOrcamento[dt_ItensOrcamento.Rows.Count];

                    for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                    {
                        itensOrcamento[i] = new iModItensOrcamento();
                        itensOrcamento[i].produto.Pk_Codigo = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["FK_NUMPRODUTO"].ToString());
                        itensOrcamento[i].PkIdItemVenda = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["PK_ID"].ToString());
                        itensOrcamento[i].NumeroItem = Convert.ToInt32(dt_ItensOrcamento.Rows[i]["ITEM"].ToString());
                        itensOrcamento[i].CodigoFabric = dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString();
                        itensOrcamento[i].DescricaoAplicacao = dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString();
                        itensOrcamento[i].PrecoVendaBanco = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString());
                        itensOrcamento[i].ValorUnit = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString());
                        itensOrcamento[i].Quantidade = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString());
                        itensOrcamento[i].ValorTotal = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString());
                        itensOrcamento[i].Desconto = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                        itensOrcamento[i].Acrescimo = Convert.ToDecimal(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());
                    }

                    controllerOrcamento.modOrcamento.itensOrcamento = itensOrcamento;
                    bool retorno = controllerOrcamento.cAlteraOrcamento();
                    if (retorno == true)
                    {                        
                        MessageBox.Show("Orçamento alterado com sucesso", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    }
                    else
                    {                        
                            MessageBox.Show("Ocorreu uma falha no Sistema. O orçamento não foi alterado, um LOG foi gerado automaticamente. Caso o problema persista, contacte a equipe FuturaData!", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.carregarDados();
                    }

                    #endregion Alterar Dados Orcamento
                }//fim if opcao = 2

                #endregion Opcao = 2 (Atualiza Orcamento)

                carregaOrcamentosEmAberto();
                carregarDados();
            }//fim valida orcamento
        }//fim método

        #endregion Método Executa Operacao
        
        #region Método Valida Orcamento antes de gravar

        public bool validaOrcamento()
        {
            bool retorno = true;
            clsNewContasMatematicas contas = new clsNewContasMatematicas();
            clsValidacaoDeCampos valida = new clsValidacaoDeCampos();

            #region Validacoes Gerais

            if (dt_ItensOrcamento.Rows.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos 1 item a cotação antes de tentar salvar.", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }        

            #endregion Validacoes Gerais

            #region Validacoes para Orçamento
            if (cbbNomeCliente.Text == "")
            {
                MessageBox.Show("Por favor, é necessário escolher o cliente para efetuar a operação", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbNomeCliente.Focus();
                retorno = false;
            }
            
            #endregion Validacoes para Orçamento

            return retorno;
        }

        #endregion Método Valida Orcamento antes de gravar

        #endregion

        #region **************EVENTOS***************

        #region Evento das ComboBox

        private void cbbNomeCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // carregaComboBoxEquipCliente();
        }

        #endregion Evento das ComboBox       

        #region Evento dos Botoes Novo, Alterar, Salvar, Próximo, Anterior, Relatórios e etc (ToolStrip)

        private void btnNovo_Click(object sender, EventArgs e)
        {            
            limpaControles();
            destravaCampos();
            opcao = 1;
            travaBotaoOpcoes();
            carregaItensDoOrcamento();
            carregaListViewItensOrcamento();
            tbxNumeroOrcamento.Text = "0";           
            tbcVendas.SelectedTab = tbpDadosGerais;
            lblQuantidadeProdutos.Text = "Clique Adicionar Produto para Adicionar Itens...";
            lblQuantidadeProdutos.Refresh();
            btnAdicionarProduto.Focus();
            dt_ItensOrcamento.Rows.Clear(); //limpa o datatable para armazenar os itens do novo orcamento
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {            
            destravaCampos();
            opcao = 2;
            travaBotaoOpcoes();
            tbcVendas.SelectedTab = tbpDadosGerais;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaOrcamento())
            {
                if (MessageBox.Show("Deseja realmente salvar as alterações?", "FuturaData - Orçamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pedidoAtual = tbxNumeroOrcamento.Text;
                    executaOperacao();
                    carregaOrcamentosEmAberto();
                    
                    carregarDados();
                    carregaListViewItensOrcamento();
                    carregaItensDoOrcamento();

                    lvwPesquisaClientes.Items.Clear();
                    lvwPesquisaClientes.Refresh();
                    carregaListViewPesquisa();
                    opcao = 0;
                }//fim primeira messagebox
            }//fim if validaorcamento
        }//fim método

        #endregion Evento dos Botoes Novo, Alterar, Salvar, Próximo, Anterior, Relatórios e etc (ToolStrip)

        #region Evento dos Botoes Adicionar e Remover Produto/Servico

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            frmPesquisaProdutos pesq = new frmPesquisaProdutos(this, frmInicial);
            pesq.ShowDialog();

            //frmAdicItemOrctOs adic = new frmAdicItemOrctOs(this.frmInicial, this, princ);
            //adic.ShowDialog();
        }

        #endregion Evento dos Botoes Adicionar e Remover Produto/Servico
        
        #region Evento dos Botoes de Correr (Primeiro, Ultimo, Próximo, Anterior) e outros da ToolStrip

        private void btnPrimeiroCad_Click(object sender, EventArgs e)
        {
            this.indice = 0;
            this.carregarDados();
        }

        private void btnCadAnterior_Click(object sender, EventArgs e)
        {
            if (indice > 0)
            {
                this.indice--;
                carregarDados();
            }
            else
                MessageBox.Show("Você está no primeiro Cadastro!", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProximoCad_Click(object sender, EventArgs e)
        {
            if (indice != arrayOrcamentos.Length -1)
            {
                this.indice++;
                carregarDados();
            }
            else
                MessageBox.Show(null, "Você está no ultimo Cadastro!", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUltimoCad_Click(object sender, EventArgs e)
        {
            this.indice = arrayOrcamentos.Length - 1;
            carregarDados();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            indice = 0;
            carregarDados();
        }

        #endregion Evento dos Botoes de Correr (Primeiro, Ultimo, Próximo, Anterior) e outros da ToolStrip

        #region Evento Botao Excluir Produto

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir esse produto do orçamento?", "FuturaData - Orçamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string codigoProdutoSelecionado = lvwItensOrcamento.SelectedItems[0].SubItems[13].Text.ToString().Trim();

                for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                {
                    if (dt_ItensOrcamento.Rows[i]["PK_ID"].ToString() == codigoProdutoSelecionado)
                    {
                        dt_ItensOrcamento.Rows[i].Delete();
                    }
                }
                carregaListViewItensOrcamento();
            }
        }

        #endregion Evento Botao Excluir Produto

        #region Botao Imprimir cotação e Cadastro de Clientes
        private void btnImprimir_Click(object sender, EventArgs e)
        {            
                if (contas.verificaSeEInteiro(tbxNumeroOrcamento.Text) && opcao == 0)
                {
                    string emailCliente = "";

                    string tipoFrete = "Frete: ";
                    string prazoEntrega = "Prazo Entr: ";
                    string garantia = "Garantia Até: ";
                    string validadeProposta = "Valid.Proposta: ";
                    string formaPagto = "Forma Pagto: ";
                    string qtdItens = "Quantidade Produtos:";

                    frmImpressaoRelPedido impr = new frmImpressaoRelPedido(Convert.ToInt32(tbxNumeroOrcamento.Text), this.frmInicial, emailCliente, false, tipoFrete, prazoEntrega, garantia, validadeProposta, formaPagto, qtdItens, tbxValorFinal.Text);
                    impr.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Você precisa estar com a Cotação Salva para Imprimir!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }           
        }

        private void btnCadClientes_Click(object sender, EventArgs e)
        {
            frmGestaoClientes gestao = new frmGestaoClientes(this.frmInicial, princ);
            gestao.ShowDialog();
            carregaComboBoxClientes();
        }

        #endregion Botao Imprimir cotação e Cadastro de Clientes

        #region Evento do Botao Fechar

        private void btnFecharVenda_Click(object sender, EventArgs e)
        {
            
            string pedidoAtual = tbxNumeroOrcamento.Text;
            carregaOrcamentosEmAberto();
            indice = arrayOrcamentos.Length - 1;
            carregarDados();
            carregaListViewItensOrcamento();
            carregaItensDoOrcamento();

            lvwPesquisaClientes.Items.Clear();
            lvwPesquisaClientes.Refresh();
            carregaListViewPesquisa();
        }

        #endregion Evento do Botao Fechar

        #region Evento do Botao Fechar Pedido

        private void btnFecharPedido_Click(object sender, EventArgs e)
        {
            if (tbxNumeroOrcamento.Text != "")
            {
                
            }
            
            #region Antigo Código Comentado em 26112013 - Antes do Caixa e agora é botão de conferência e não mais fecha pedido - Fer
            //if (tbxNumeroOrcamento.Text != "")
            //{
            //    if (MessageBox.Show("Efetuando a venda, ela será liberada para fatura e emissão da Nota Fiscal Eletrônica e não será mais possível alterar a mesma. Deseja realmente fechar a venda?", "FuturaData - Orçamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        clsBancos banco = new clsBancos();
            //        bool retorno = banco.ObterContasBancarias(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            //        if (retorno == true)
            //        {
            //            DataTable dt_Bancos = new DataTable();
            //            bool retorno2 = banco.ObterContasBancarias(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            //            if (retorno2 == true)
            //            {
            //                dt_Bancos = banco.getDs_DadosRetorno().Tables[0];
            //            }
            //            if (dt_Bancos.Rows.Count > 0)
            //            {
            //                DataTable dt_DadosPagamento = new DataTable();

            //                dt_DadosPagamento.Columns.Add("FormaPagto");
            //                dt_DadosPagamento.Columns.Add("Parcela");
            //                dt_DadosPagamento.Columns.Add("Vencimento");
            //                dt_DadosPagamento.Columns.Add("ValorParcela");
            //                dt_DadosPagamento.Columns.Add("Info");

            //                DataRow DR = dt_DadosPagamento.NewRow();
            //                DR["FormaPagto"] = "Faturado";
            //                DR["Parcela"] = "1";
            //                DR["Vencimento"] = "01/01/2009 11:11:11";
            //                DR["ValorParcela"] = tbxValorFinal.Text;
            //                DR["Info"] = "";

            //                dt_DadosPagamento.Rows.Add(DR);
            //                clsVendas venda = new clsVendas();
            //                int numeroPedido = venda.fechaVenda(Convert.ToInt32(tbxNumeroOrcamento.Text), false, Convert.ToDecimal(tbxValorFinal.Text), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(tbxValorFinal.Text), "", Convert.ToDecimal(tbxValorFinal.Text), Convert.ToDecimal(0), tbxMaisInformacoes.Text, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost, dt_DadosPagamento, Convert.ToInt32(1));

            //                if (numeroPedido == 0)
            //                {
            //                    MessageBox.Show("Pedido não gerado com sucesso! A operação foi abordada devido a um erro. Por favor verifique, caso o problema persista contate a equipe Futuradata.", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //                else
            //                {
            //                    //MessageBox.Show(null, "Pedido " + numeroPedido.ToString() + " gerado com sucesso! A Venda foi Efetivada e já está liberada para emissão da Nf-e!" + Environment.NewLine + Environment.NewLine + "O respectivo valor dessa venda já foi somado ao caixa do dia atual e irá constar nos relatórios de fechamento de caixa. Essa janela será fechada automáticamente.", "FuturaData Business - Fechar Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("Você não possui conta bancária cadastrada! Não será possível fechar o orçamento sem efetuar antes o cadastro! Por favor vá até o Controle Financeiro do Sistema e efetue o Cadastro de uma conta bancária", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //        carregaListViewItensOrcamento();
            //        //recarregaDadosPrincipais();
            //        //indice = dt_OrcamentosEOSCarregadosPraExibicao.Rows.Count - 1;
            //        carregarDados();
            //    }
            //}
            #endregion

            #region Código comentado, esse código servia para NFE e afins, mas para o comércio o tratamento é diferente
            /*
            if (tbxNumOrcOS.Text != "")
            {
                if (MessageBox.Show("Efetuando a venda, ela será liberada para fatura e emissão da Nota Fiscal Eletrônica e não será mais possível alterar a mesma. Deseja realmente fechar a venda?", "FuturaData - Orçamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    clsBancos banco = new clsBancos();
                    bool retorno = banco.ObterContasBancarias(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
                    if (retorno == true)
                    {
                        DataTable dt_Bancos = new DataTable();
                        bool retorno2 = banco.ObterContasBancarias(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
                        if (retorno2 == true)
                        {
                            dt_Bancos = banco.getDs_DadosRetorno().Tables[0];
                        }
                        if (dt_Bancos.Rows.Count > 0)
                        {
                            DataTable dt_DadosPagamento = new DataTable();

                            dt_DadosPagamento.Columns.Add("FormaPagto");
                            dt_DadosPagamento.Columns.Add("Parcela");
                            dt_DadosPagamento.Columns.Add("Vencimento");
                            dt_DadosPagamento.Columns.Add("ValorParcela");
                            dt_DadosPagamento.Columns.Add("Info");

                            DataRow DR = dt_DadosPagamento.NewRow();
                            DR["FormaPagto"] = "Faturado";
                            DR["Parcela"] = "1";
                            DR["Vencimento"] = "01/01/2009 11:11:11";
                            DR["ValorParcela"] = tbxValorFinal.Text;
                            DR["Info"] = "";

                            dt_DadosPagamento.Rows.Add(DR);
                            clsVendas venda = new clsVendas();
                            int numeroPedido = venda.fechaVenda(Convert.ToInt32(tbxNumOrcOS.Text), false, Convert.ToDecimal(tbxValorFinal.Text), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(tbxValorFinal.Text), "", Convert.ToDecimal(tbxValorFinal.Text), Convert.ToDecimal(0), tbxMaisInformacoes.Text, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost, dt_DadosPagamento, Convert.ToInt32(1));

                            if (numeroPedido == 0)
                            {
                                MessageBox.Show("Pedido não gerado com sucesso! A operação foi abordada devido a um erro. Por favor verifique, caso o problema persista contate a equipe Futuradata.", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //MessageBox.Show(null, "Pedido " + numeroPedido.ToString() + " gerado com sucesso! A Venda foi Efetivada e já está liberada para emissão da Nf-e!" + Environment.NewLine + Environment.NewLine + "O respectivo valor dessa venda já foi somado ao caixa do dia atual e irá constar nos relatórios de fechamento de caixa. Essa janela será fechada automáticamente.", "FuturaData Business Comercio - Fechar Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Você não possui conta bancária cadastrada! Não será possível fechar o orçamento sem efetuar antes o cadastro! Por favor vá até o Controle Financeiro do Sistema e efetue o Cadastro de uma conta bancária", "FuturaData - Orçamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    carregaListViewItensOrcamento();
                    recarregaDadosPrincipais();
                    //indice = dt_OrcamentosEOSCarregadosPraExibicao.Rows.Count - 1;
                    carregarDados();
                }
            }
             */
            #endregion Código comentado, esse código servia para NFE e afins, mas para o comércio o tratamento é diferente
        }

        #endregion Evento do Botao Fechar Pedido

        #region Evento do Botao Emitir Cotacao

        private void btnEmitirCotacao_Click(object sender, EventArgs e)
        {
            if (contas.verificaSeEInteiro(tbxNumeroOrcamento.Text))
            {
                btnImprimir_Click(sender, e);
            }
        }

        #endregion Evento do Botao Emitir Cotacao
        
        #region Listview Pesquisa de Clientes
        private void lvwPesquisaClientes_DoubleClick(object sender, EventArgs e)
        {
            string cod_Sist = lvwPesquisaClientes.SelectedItems[0].SubItems[1].Text.ToString().Trim();

            for (int i = 0; i < arrayOrcamentos.Length; i++)
            {
                if (arrayOrcamentos[i].PkCodigo.ToString() == cod_Sist)
                {
                    indice = i;
                }
            }

            this.carregarDados();
            this.tbcVendas.SelectedTab = this.tbpDadosGerais;
        }        

        private void lvwPesquisaClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lvwPesquisaClientes_DoubleClick(null, null);
            }
        }
        #endregion
        
        #region Evento do Botao Cadastrar Venda
        private void btnCadastraVendedor_Click(object sender, EventArgs e)
        {            
                //frmVendedoresRepresentantes frmVen = new frmVendedoresRepresentantes(frmInicial);
                //frmVen.ShowDialog();
                //frmVen.Dispose();
                //carregaComboBoxVendedor();
        }
        #endregion
        #endregion
    }//fim classe
}//fim namespace