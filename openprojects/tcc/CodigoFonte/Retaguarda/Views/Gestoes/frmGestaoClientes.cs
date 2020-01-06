using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataTCC.Utilitarios;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;
using DllInfoSigaUtil.Grafico;

namespace FuturaDataTCC.Views.Gestoes
{
    public partial class frmGestaoClientes : Form
    {
        #region "Atributos (variaveis) do View frmCadClientes"
        //Flag utilizada para chave busca!!!
        string idClienteSelecionado = "";
        frmInicializacao frmInicial;
        frmTelaPrincipal frmTelaPrincipal;
        public int opcao; 
        public int indice = 0;
        int cod_Pesq = 2;
        iConCliente controlCliente = new iConCliente();
        clsImagem imagem = new clsImagem();        
        clsArquivos arquivos = new clsArquivos();
        string caminhoDaAplicacao = System.Environment.CurrentDirectory.ToString();
        string caminhoImagem = "";
        clsNewContasMatematicas contas = new clsNewContasMatematicas();
        Byte[] imagemEmBytes;
        iModCliente[] arrayClientes;
        #endregion

        #region Construtor (inicializador) do Form
        public frmGestaoClientes(frmInicializacao formInicial, frmTelaPrincipal telaPrincipal)
        {
            InitializeComponent();            
            this.frmTelaPrincipal = telaPrincipal;
            frmInicial = formInicial;
                        
            this.DestravaBotaoOpcoes();
            this.carregarDados("campos");
            indice = arrayClientes.Length-1; //.Rows.Count - 1;
            this.carregarDados("campos e DataGridView");
            
            this.tbcCadastroCliente.SelectedTab = this.tbpCadastros;
            this.tbxPalavraChaveBusca.Focus();
            tbxPalavraChaveBusca_TextChanged(null, null);
            this.travarControles();            
        }

        #endregion Construtor da classe

        #region **************MÉTODOS***************
        
        #region Método travarControles (desabilita textbox, combobox, etc - readonly modo visualização)
        public void travarControles()
        {
            clsFuncoes.TravaControles(tbcCadastroCliente);
            clsFuncoes.TravaControles(tbpCadastros);
            clsFuncoes.TravaControles(tbpInformacoesCliente);
            
            tbxPalavraChaveBusca.ReadOnly = false;
            rdbCnpjCpf.Enabled = true;
            rdbNomeCliente.Enabled = true;
        }
        #endregion

        #region Método destravarControles (habilita textbox, combobox para edição ou novo)
        private void destravarControles()
        {
            //trava tudo mas destrava a pesquisa
            clsFuncoes.DestravaControles(tbcCadastroCliente);
            clsFuncoes.DestravaControles(tbpCadastros);
            clsFuncoes.DestravaControles(tbpInformacoesCliente);            
            tbxCodigo.ReadOnly = true;
        }
        #endregion
        
        #region Método travaBotaoOpcoes
        private void travaBotaoOpcoes()
        {
            btnAlterar.Enabled = false;
            btnCadAnterior.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnProximoCad.Enabled = false;
            btnPrimeiroCad.Enabled = false;
            btnUltimoCad.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;

            tbxPalavraChaveBusca.ReadOnly = true;
        }

        //fim travaBotaoOpcoes

        #endregion Método travaBotaoOpcoes

        #region Método destravaBotaoOpcoes

        private void DestravaBotaoOpcoes()
        {
            btnAlterar.Enabled = true;
            btnCadAnterior.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnProximoCad.Enabled = true;
            btnPrimeiroCad.Enabled = true;
            btnUltimoCad.Enabled = true;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = true;
            tbxPalavraChaveBusca.ReadOnly = false;
        }
        #endregion Método destravaBotaoOpcoes

        #region Método Limpar Controles para Novo Cliente
        public void limparControles()
        {
            clsFuncoes.LimpaControles(tbpCadastros);
            clsFuncoes.LimpaControles(tbpInformacoesCliente);
            cbbOperadoraCelular1.Text = "Vivo";
            cbbOperadoraCelular2.Text = "Vivo";

            rdbPessoaFisica.Checked = false;
            rdbPessoaJuridica.Checked = true;
            cbbEstado.Text = "SP";
            pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
            pctImagemCliente.Refresh();
        }
        #endregion Método Limpar Controles para Novo Cliente

        #region Método Executar Operacao
        //Para passar os Parametros para a Camada de Negócio, recebe um parametro,
        //Se for 0, a opção é gravar um Novo Cadastro, se for 1, a opção é Alterar
        //O cadastro, se for 3 é excluir o Cadastro Atual!
        private void executarOperacao()
        {
            #region SET dos parâmetros
            controlCliente.modCliente.Bairro = tbxBairro.Text;
            controlCliente.modCliente.Celular1 = tbxCelular1.Text;
            controlCliente.modCliente.Celular2 = tbxCelular2.Text;
            controlCliente.modCliente.Cep = tbxCep.Text;
            controlCliente.modCliente.Cidade = tbxCidade.Text;
            controlCliente.modCliente.Complemento = tbxComplemento.Text;
            controlCliente.modCliente.CpfCnpj = tbxCpfCnpj.Text;
            controlCliente.modCliente.Email = tbxEmail.Text;
            controlCliente.modCliente.Estado = cbbEstado.Text;
            controlCliente.modCliente.Fax = tbxFax.Text;
            controlCliente.modCliente.InscrEstadual = tbxInscricaoEstadual.Text;
            controlCliente.modCliente.Logradouro = tbxLogradouro.Text;
            controlCliente.modCliente.MaisInfo = tbxMaisInfo.Text;
            controlCliente.modCliente.Nome = tbxNome.Text;
            controlCliente.modCliente.Numero = tbxNumero.Text;
            controlCliente.modCliente.Operadora1 = cbbOperadoraCelular1.Text;
            controlCliente.modCliente.Operadora2 = cbbOperadoraCelular2.Text;
            controlCliente.modCliente.PessoaFisicaJuridica = "F";
            if (rdbPessoaJuridica.Checked)
            {
                controlCliente.modCliente.PessoaFisicaJuridica = "J";
            }
            controlCliente.modCliente.Pk_Codigo = Convert.ToInt32(tbxCodigo.Text);
            controlCliente.modCliente.RazaoSocial = tbxRazaoSocial.Text;
            controlCliente.modCliente.Rg = tbxRG.Text;
            controlCliente.modCliente.Site = tbxSite.Text;
            controlCliente.modCliente.Status = "ATIVO";
            controlCliente.modCliente.Telefone1 = tbxTelefone1.Text;
            controlCliente.modCliente.Telefone2 = tbxTelefone2.Text;

            //carrega o Byte de imagem na classe
            if (caminhoImagem != "")
            {
                long tamanhoDaImagem = 0;
                FileInfo imagem = new FileInfo(caminhoImagem);
                tamanhoDaImagem = imagem.Length;
                imagemEmBytes = new byte[Convert.ToInt32(tamanhoDaImagem)];
                FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read, FileShare.Read);
                fs.Read(imagemEmBytes, 0, Convert.ToInt32(tamanhoDaImagem));
                fs.Close();
                controlCliente.modCliente.ImagemCliente = imagemEmBytes;
            }
            #endregion

            #region Opcao = 0 (insere novo cliente)
            if (opcao == 0)
            {
                bool retorno = controlCliente.cInsereCliente();
                if (retorno)
                {
                    MessageBox.Show("Cliente inserido com sucesso!", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao inserir Cliente! Mensagem: " + controlCliente.modCliente.ErroClasse, "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            #region Opcao = 1 (altera cliente existente)
            if (opcao == 1)
            {
                bool retorno = controlCliente.cAlteraCliente();
                if (retorno)
                {
                    MessageBox.Show("Cliente Alterado com sucesso!", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else
                {
                    MessageBox.Show("Falha ao Alterar Cliente! Mensagem: " + controlCliente.modCliente.ErroClasse, "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            #region Opcao = 2 (exclui o cliente)
            if (opcao == 2)
            {
                bool retorno = controlCliente.cExcluiCliente();
                if (retorno)
                {
                    MessageBox.Show("Excluido com sucesso!", "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao Excluir Cliente! Mensagem: " + controlCliente.modCliente.ErroClasse, "FuturaData TCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
        }
        #endregion Método executarOperacao

        #region Método Carrega ArrayList do Cadastro de Clientes
        private void carregarClientes()
        {
            arrayClientes = controlCliente.cObterCliente();
        }
        #endregion Método carregarDataGridView

        #region Método Carregar Dados (carrega os dados e exibe nos campos)
        private void carregarDados(string opcao)
        {
            if (opcao == "campos")
            {
                this.carregarClientes();
                travarControles();
            }
            else
            {

                if (arrayClientes.Length == 0)
                {                    
                    limparControles();
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = false;
                }
                else
                {
                    //entrará nesse IF se o usuário tiver selecionado algum cliente no listview pesquisas
                    if (idClienteSelecionado != "")
                    {
                        int indicePesquisa = 0;
                        foreach (iModCliente item in arrayClientes)
                        {
                            if (idClienteSelecionado == item.Pk_Codigo.ToString())
                            {
                                indice = indicePesquisa;
                            }
                            
                        }
                    }

                    #region Antigo Codigo que continha o carregar cliente via DataSet
                    //tbxCodigo.Text = dt_DadosClientes.Rows[indice]["CODIGO"].ToString();
                    //if (dt_DadosClientes.Rows[indice]["PESSOAFISICAJURIDICA"].ToString().Trim() == "F")
                    //{
                    //    rdbPessoaFisica.Checked = true;
                    //}
                    //else
                    //{
                    //    rdbPessoaJuridica.Checked = true;
                    //}

                    //tbxCpfCnpj.Text = dt_DadosClientes.Rows[indice]["CPFCNPJ"].ToString();
                    //verificaStatusControles();
                    //tbxRG.Text = dt_DadosClientes.Rows[indice]["RG"].ToString();

                    //tbxNome.Text = dt_DadosClientes.Rows[indice]["NOME"].ToString();
                    //tbxRazaoSocial.Text = dt_DadosClientes.Rows[indice]["RAZAOSOCIAL"].ToString();
                    //tbxInscricaoEstadual.Text = dt_DadosClientes.Rows[indice]["INSCRESTADUAL"].ToString();
                    //cbbEstado.Text = dt_DadosClientes.Rows[indice]["ESTADO"].ToString();
                    //tbxCep.Text = dt_DadosClientes.Rows[indice]["CEP"].ToString();
                    //tbxLogradouro.Text = dt_DadosClientes.Rows[indice]["LOGRADOURO"].ToString();
                    //tbxNumero.Text = dt_DadosClientes.Rows[indice]["NUMERO"].ToString();
                    //tbxBairro.Text = dt_DadosClientes.Rows[indice]["BAIRRO"].ToString();
                    //tbxCidade.Text = dt_DadosClientes.Rows[indice]["CIDADE"].ToString();
                    //tbxComplemento.Text = dt_DadosClientes.Rows[indice]["COMPLEMENTO"].ToString();
                    //tbxTelefone1.Text = dt_DadosClientes.Rows[indice]["TELEFONE1"].ToString();
                    //tbxTelefone2.Text = dt_DadosClientes.Rows[indice]["TELEFONE2"].ToString();
                    //tbxFax.Text = dt_DadosClientes.Rows[indice]["FAX"].ToString();
                    //tbxCelular1.Text = dt_DadosClientes.Rows[indice]["CELULAR1"].ToString();
                    //cbbOperadoraCelular1.Text = dt_DadosClientes.Rows[indice]["OPERADORA1"].ToString();
                    //tbxCelular2.Text = dt_DadosClientes.Rows[indice]["CELULAR2"].ToString();
                    //cbbOperadoraCelular2.Text = dt_DadosClientes.Rows[indice]["OPERADORA2"].ToString();

                    //tbxMaisInfo.Text = dt_DadosClientes.Rows[indice]["MAISINFO"].ToString();

                    //#region Trecho de tratamento do picture box
                    //string imagemConvertida = dt_DadosClientes.Rows[indice]["POSSUI_IMAGEM"].ToString();

                    ////apenas se tiver imagem eu irei tentar recuperar ela no banco...
                    //if (imagemConvertida.ToUpper() == "TRUE" || imagemConvertida == "1")
                    //{
                    //    try
                    //    {
                    //        controlCliente.modCliente.Pk_Codigo = Convert.ToInt32(tbxCodigo.Text);
                    //        pctImagemCliente.Image = controlCliente.cObterImagem();
                    //        pctImagemCliente.Refresh();
                    //    }
                    //    catch
                    //    {
                    //        pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
                    //        pctImagemCliente.Refresh();
                    //    }
                    //}
                    //else
                    //{
                    //    pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
                    //    pctImagemCliente.Refresh();
                    //}
                    //#endregion Trecho de tratamento do picture box
                    //#endregion
                    #endregion
                    
                    iModCliente objClienteAtual = arrayClientes[indice];
                    if (objClienteAtual.PessoaFisicaJuridica.ToString() == "F")
                    {
                        rdbPessoaFisica.Checked = true;
                    }
                    else
                    {
                        rdbPessoaJuridica.Checked = true;
                    }

                    tbxCpfCnpj.Text = objClienteAtual.CpfCnpj.ToString();
                    verificaStatusControles();
                    tbxRG.Text = objClienteAtual.Rg.ToString();
                    tbxCodigo.Text = objClienteAtual.Pk_Codigo.ToString();
                    tbxNome.Text = objClienteAtual.Nome.ToString();
                    tbxRazaoSocial.Text = objClienteAtual.RazaoSocial.ToString();
                    tbxInscricaoEstadual.Text = objClienteAtual.InscrEstadual.ToString();
                    cbbEstado.Text = objClienteAtual.Estado.ToString();
                    tbxCep.Text = objClienteAtual.Cep.ToString();
                    tbxLogradouro.Text = objClienteAtual.Logradouro.ToString();
                    tbxNumero.Text = objClienteAtual.Numero.ToString();
                    tbxBairro.Text = objClienteAtual.Bairro.ToString();
                    tbxCidade.Text = objClienteAtual.Cidade.ToString();
                    tbxComplemento.Text = objClienteAtual.Complemento.ToString();
                    tbxTelefone1.Text = objClienteAtual.Telefone1.ToString();
                    tbxTelefone2.Text = objClienteAtual.Telefone2.ToString();
                    tbxFax.Text = objClienteAtual.Fax.ToString();
                    tbxCelular1.Text = objClienteAtual.Celular1.ToString();
                    cbbOperadoraCelular1.Text = objClienteAtual.Operadora1.ToString();
                    tbxCelular2.Text = objClienteAtual.Celular2.ToString();
                    cbbOperadoraCelular2.Text = objClienteAtual.Operadora2.ToString();

                    tbxMaisInfo.Text = objClienteAtual.MaisInfo.ToString();

                    #region Trecho de tratamento do picture box
                    string imagemConvertida = objClienteAtual.PessoaFisicaJuridica.ToString();

                    //apenas se tiver imagem eu irei tentar recuperar ela no banco...
                    if (imagemConvertida.ToUpper() == "TRUE" || imagemConvertida == "1")
                    {
                        try
                        {
                            controlCliente.modCliente.Pk_Codigo = Convert.ToInt32(tbxCodigo.Text);
                            pctImagemCliente.Image = controlCliente.cObterImagem();
                            pctImagemCliente.Refresh();
                        }
                        catch
                        {
                            pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
                            pctImagemCliente.Refresh();
                        }
                    }
                    else
                    {
                        pctImagemCliente.Image = FuturaDataTCC.Properties.Resources.usuarioSemFoto;
                        pctImagemCliente.Refresh();
                    }
                    #endregion Trecho de tratamento do picture box                    
                }
            }
        }
        //fim mostra dados
        #endregion Método carregarDados
                
        #region Método Verifica os Controles quando é selecionado PF ou PJ
        //desativa alguns controles, dependendo da chk clicada, ele ativa ou não os controles respectivos
        //Serve para desativar/ativar os controles na configuração de segurança
        //e formatar máscaras e desativar/ativar controles para PF/PJ
        private void verificaStatusControles()
        {
            if (rdbPessoaFisica.Checked == true)
            {
                if (opcao != 2)
                {
                    tbxCpfCnpj.Mask = "999,999,999-99";
                    tbxRazaoSocial.ReadOnly = true;
                    tbxInscricaoEstadual.ReadOnly = true;
                    
                    tbxInscricaoEstadual.Clear();
                    
                    tbxRazaoSocial.Clear();
                    tbxRG.Mask = "99,999,999-9";
                    tbxRG.ReadOnly = false;                    
                    tbxCpfCnpj.Focus();
                }
                else
                {
                    tbxCpfCnpj.Mask = "999,999,999-99";
                }
            }
            else
            {
                if (opcao != 2)
                {
                    tbxCpfCnpj.Mask = "99,999,999/9999-99";
                    tbxRazaoSocial.ReadOnly = false;
                    tbxInscricaoEstadual.ReadOnly = false;
                    tbxRG.Clear();
                    tbxRG.Text = "";
                    tbxRG.ReadOnly = true;                    
                    tbxCpfCnpj.Focus();
                }
                else
                {
                    tbxCpfCnpj.Mask = "99,999,999/9999-99";
                }
            }
        }
        #endregion Método verificarStatusControles

        #region Método verificaCampos (valida antes de salvar)
        private bool verificaCampos()
        {
            this.Refresh();

            clsNewContasMatematicas contasMatematicas = new clsNewContasMatematicas();
            clsValidacaoDeCampos validacaoCampos = new clsValidacaoDeCampos();
            clsCpfCnpjValidacao validacao = new clsCpfCnpjValidacao();
            bool validaCPFCnpj = true;
            bool retorno = true;
            //verifica se os campos de preenchimento obrigatório estão prenchidos
            if (rdbPessoaFisica.Checked == false && rdbPessoaJuridica.Checked == false)
            {
                MessageBox.Show("Selecione o tipo de Cliente (Pessoa Fisica ou Juridica)!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsFuncoes.DesenhaRetanguloVermelho(rdbPessoaFisica);
                retorno = false;
                return retorno;
            }

            if (tbxCpfCnpj.Text == String.Empty)
            {
                MessageBox.Show("O campo CPF/CNPJ não foi preenchido, por favor preencha", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsFuncoes.DesenhaRetanguloVermelho(tbxCpfCnpj);
                tbxCpfCnpj.Focus();
                retorno = false;
                return retorno;
            }

           
                if (validaCPFCnpj == true)
                {
                    string teste = tbxCpfCnpj.Text.Replace("-", "").Replace(",", "").Replace(".", "");

                    if (!teste.Trim().Equals(""))
                    {
                        if ((rdbPessoaFisica.Checked == true) && (validacao.ValidaCPF(tbxCpfCnpj.Text.Replace("-", "").Replace(",", "").Replace(".", "")) == false))
                        {
                            MessageBox.Show("Número de CPF Inserido é Incorreto, por favor insira um numero válido!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFuncoes.DesenhaRetanguloVermelho(tbxCpfCnpj);
                            tbxCpfCnpj.Focus();
                            retorno = false;
                            return retorno;
                        }

                        if ((rdbPessoaJuridica.Checked == true) && (validacao.ValidaCNPJ(tbxCpfCnpj.Text.Replace("/", "").Replace(",", "").Replace(".", "")) == false))
                        {
                            MessageBox.Show("Número de CNPJ Inserido é Incorreto, por favor insira um numero válido!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFuncoes.DesenhaRetanguloVermelho(tbxCpfCnpj);
                            tbxCpfCnpj.Focus();
                            retorno = false;
                            return retorno;
                        }

                        if ((rdbPessoaJuridica.Checked == true) && (tbxRazaoSocial.Text == ""))
                        {
                            MessageBox.Show("Por favor informe a Razão Social para um Cadastro Pessoa Jurídica", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFuncoes.DesenhaRetanguloVermelho(tbxRazaoSocial);
                            tbxRazaoSocial.Focus();
                            retorno = false;
                            return retorno;
                        }
                    }
            }

           
                if ((rdbPessoaFisica.Checked != true) && tbxInscricaoEstadual.Text.Equals(""))
                {
                    MessageBox.Show("Erro no Campo Inscrição Estadual - Favor preencha-o (use 'ISENTO' quando o cliente não possuir)!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clsFuncoes.DesenhaRetanguloVermelho(tbxInscricaoEstadual);
                    tbxInscricaoEstadual.Focus();
                    retorno = false;
                    return retorno;
                }
            

            if (tbxTelefone1.Text.Replace("(", "").Replace(")", "").Replace("-", "") == "")
            {
                MessageBox.Show("O preechimento do campo Telefone 1 é obrigatória ", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsFuncoes.DesenhaRetanguloVermelho(tbxTelefone1);
                tbxTelefone1.Focus();
                retorno = false;
                return retorno;
            }
            else
            {
                if (tbxTelefone1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Length < 10)
                {
                    MessageBox.Show("O campo Telefone 1 não foi preenchido corretamente Ex:(11)1234-1234", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clsFuncoes.DesenhaRetanguloVermelho(tbxTelefone1);
                    tbxTelefone1.Focus();
                    retorno = false;
                    return retorno;
                }
            }


            if (tbxEmail.Text != "" && validacaoCampos.ValidaEmail(tbxEmail.Text) == false)
            {
                MessageBox.Show("Campo E-mail está no Formato Incorreto! Exemplo: alguem@empresa.com", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsFuncoes.DesenhaRetanguloVermelho(tbxEmail);
                tbxEmail.Focus();
                retorno = false;
                return retorno;
            }

            if (tbxNome.Text == "")
            {
                MessageBox.Show(null, "Por favor informe um nome para o Cliente.", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsFuncoes.DesenhaRetanguloVermelho(tbxNome);
                tbxNome.Focus();
                retorno = false;
                return retorno;
            }

            return retorno;
        }

        #endregion Método verificaCampos

        #region Método Busca por Palavra Chave (para a pesquisa de clientes no listview)

        private void tbxPalavraChaveBusca_TextChanged(object sender, EventArgs e)
        {
            lvwPesquisaClientes.Items.Clear(); //limpo o ListView para mostrar nova consulta
            lvwPesquisaClientes.Columns.Clear();

            #region Cria e Formata as Colunas dentro do ListView

            lvwPesquisaClientes.Columns.Add("", 0, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Codigo", 60, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Cpf/Cnpj", 120, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Nome (Fantasia)", 240, HorizontalAlignment.Center);
            lvwPesquisaClientes.Columns.Add("Razão Social", 250, HorizontalAlignment.Center);

            #endregion Cria e Formata as Colunas dentro do ListView

            if (tbxPalavraChaveBusca.Text != "")
            {
                #region Pesquisa == 2 (Nome)

                if (cod_Pesq == 2)//nome
                {
                    #region Monta o Filtro de Dados e a Pesquisa

                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxPalavraChaveBusca.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxPalavraChaveBusca.Text.ToString().Substring(0, tamanhoFiltro);

                    #endregion Monta o Filtro de Dados e a Pesquisa

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada

                    dt_DadosFiltrados.Columns.Add("CODIGOCLIENTE");
                    dt_DadosFiltrados.Columns.Add("CPF_CNPJ");
                    dt_DadosFiltrados.Columns.Add("NOMEFANTASIA");
                    dt_DadosFiltrados.Columns.Add("RAZAOSOCIAL");

                    #endregion Cria o DataTable para Armazenar a Pesquisa Filtrada

                    #region Varre o DataTable Atual para fazer o Filtro

                    //cria o DataTable com o Filtro de Pesquisa
                    foreach (iModCliente item in arrayClientes)
                    {
                        if (item.Nome.Length >= tamanhoFiltro)
                        {
                            if (item.Nome.Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CODIGOCLIENTE"] = item.Pk_Codigo.ToString();
                                DR["CPF_CNPJ"] = item.CpfCnpj;
                                DR["NOMEFANTASIA"] = item.Nome;
                                DR["RAZAOSOCIAL"] = item.RazaoSocial;
                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;
                            }
                        }
                    }

                    #endregion Varre o DataTable Atual para fazer o Filtro

                    #region Faz o Update Dentro do ListView

                    lvwPesquisaClientes.BeginUpdate();
                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaClientes.Items.Add("");
                        if (i2 % 2 == 0)
                        {
                            lvwPesquisaClientes.Items[i2].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwPesquisaClientes.Items[i2].BackColor = Color.White;
                        }
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CODIGOCLIENTE"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CPF_CNPJ"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["NOMEFANTASIA"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["RAZAOSOCIAL"].ToString());
                    }

                    #endregion Faz o Update Dentro do ListView

                    lvwPesquisaClientes.EndUpdate();
                    lblClientesListados.Text = arrayClientes.Length.ToString() + " Clientes Cadastrados ao total. " + dt_DadosFiltrados.Rows.Count.ToString() + " Clientes com a palavra chave pesquisada.";
                    lblClientesListados.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==2

                #endregion Pesquisa == 2 (Nome)

                #region Pesquisa == 1 (CPF Cnpj)

                if (cod_Pesq == 1)//CPF CNPJ
                {
                    #region Monta o Filtro de Dados e a Pesquisa

                    DataTable dt_DadosFiltrados = new DataTable();
                    int tamanhoFiltro = tbxPalavraChaveBusca.Text.Length; //recebe o tamanho (quantidade) caracters pesquisa
                    //filtro (palavras digitadas na textbox para o filtro)
                    string filtro = tbxPalavraChaveBusca.Text.ToString().Substring(0, tamanhoFiltro);

                    #endregion Monta o Filtro de Dados e a Pesquisa

                    #region Cria o DataTable para Armazenar a Pesquisa Filtrada

                    dt_DadosFiltrados.Columns.Add("CODIGOCLIENTE");
                    dt_DadosFiltrados.Columns.Add("CPF_CNPJ");
                    dt_DadosFiltrados.Columns.Add("NOMEFANTASIA");
                    dt_DadosFiltrados.Columns.Add("RAZAOSOCIAL");

                    #endregion Cria o DataTable para Armazenar a Pesquisa Filtrada

                    #region Varre o DataTable Atual para fazer o Filtro


                    foreach (iModCliente item in arrayClientes)
                    {
                        if (item.CpfCnpj.Length >= tamanhoFiltro)
                        {
                            if (item.CpfCnpj.Substring(0, tamanhoFiltro).ToUpper() == filtro.ToUpper())
                            {
                                DataRow DR = dt_DadosFiltrados.NewRow();
                                DR["CODIGOCLIENTE"] = item.Pk_Codigo.ToString();
                                DR["CPF_CNPJ"] = item.CpfCnpj;
                                DR["NOMEFANTASIA"] = item.Nome;
                                DR["RAZAOSOCIAL"] = item.RazaoSocial;
                                dt_DadosFiltrados.Rows.Add(DR);
                                DR = null;
                            }
                        }
                    }

                    #endregion Varre o DataTable Atual para fazer o Filtro

                    #region Faz o Update Dentro do ListView

                    for (int i2 = 0; i2 < dt_DadosFiltrados.Rows.Count; i2++)
                    {
                        lvwPesquisaClientes.Items.Add("");
                        if (i2 % 2 == 0)
                        {
                            lvwPesquisaClientes.Items[i2].BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            lvwPesquisaClientes.Items[i2].BackColor = Color.White;
                        }
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CODIGOCLIENTE"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["CPF_CNPJ"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["NOMEFANTASIA"].ToString());
                        lvwPesquisaClientes.Items[i2].SubItems.Add(dt_DadosFiltrados.Rows[i2]["RAZAOSOCIAL"].ToString());
                    }

                    #endregion Faz o Update Dentro do ListView

                    lvwPesquisaClientes.EndUpdate();
                    lblClientesListados.Text = arrayClientes.Length.ToString() + " Clientes Cadastrados ao total. " + dt_DadosFiltrados.Rows.Count.ToString() + " Clientes com a palavra chave pesquisada.";
                    lblClientesListados.Refresh();
                    dt_DadosFiltrados.Dispose(); //apago o objeto da memória
                }//fim if==2
                #endregion Pesquisa == 1 (CPF Cnpj)
            }
            else
            {
                int indiceLinhaColorida = 0;
                foreach (iModCliente item in arrayClientes)
                {
                    lvwPesquisaClientes.BeginUpdate();
                    lvwPesquisaClientes.Items.Add("");
                    if (indiceLinhaColorida % 2 == 0)
                    {
                        lvwPesquisaClientes.Items[indiceLinhaColorida].BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lvwPesquisaClientes.Items[indiceLinhaColorida].BackColor = Color.White;
                    }
                    lvwPesquisaClientes.Items[indiceLinhaColorida].SubItems.Add(item.Pk_Codigo.ToString());
                    lvwPesquisaClientes.Items[indiceLinhaColorida].SubItems.Add(item.CpfCnpj);
                    lvwPesquisaClientes.Items[indiceLinhaColorida].SubItems.Add(item.Nome);
                    lvwPesquisaClientes.Items[indiceLinhaColorida].SubItems.Add(item.RazaoSocial);
                    indiceLinhaColorida++;
                }
                lvwPesquisaClientes.EndUpdate();
                lblClientesListados.Text = arrayClientes.Length.ToString() + " Produtos Cadastrados ao total.";
                lblClientesListados.Refresh();
            }
        }

        private void dt_DadosClientes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //this.indice = this.dt_DadosClientes.CurrentRow.Index;
            this.carregarDados("somente campos");
            this.travarControles();
            this.DestravaBotaoOpcoes();
            this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
        }//fim método

        #endregion Método Busca por Palavra Chave
        
        #endregion

        #region **************EVENTOS***************
        #region Eventos dos Botões Novo, Alterar, Excluir, Salvar e Cancelar

        #region Evento Botao Novo
        private void btnNovo_Click(object sender, EventArgs e)
        {                           
                limparControles();
                destravarControles();
                opcao = 0;
                travaBotaoOpcoes();
                tbxCodigo.Text = "0"; //0 quando é novo cliente
                caminhoImagem = "";                
                tbxCpfCnpj.Focus();              
                verificaStatusControles();
                idClienteSelecionado = tbxCodigo.Text;
                verificaStatusControles();                
                this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;                    
        }
        #endregion        

        #region Evento Botao Salvar

        private void btnSalvar_Click(object sender, EventArgs e)
        {           
            if ((verificaCampos()) == true) //só irá executar se validar todos campos
            {   
                this.executarOperacao();
                this.carregarDados("campos");
                indice = arrayClientes.Length-1;
                this.carregarDados("campos e DataGridView");
                idClienteSelecionado = "";
                travarControles();
                DestravaBotaoOpcoes();
            }            
        }

        #endregion Evento Botao Salvar
        
        #region Evento Botao Alterar
        private void btnAlterar_Click(object sender, EventArgs e)
        {            
                this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
                this.opcao = 1;
                this.destravarControles();
                this.travaBotaoOpcoes();                
                idClienteSelecionado = tbxCodigo.Text; //PRA QUANDO SALVAR E ALTERAR CONTINUAR NO MESMO CLIENTE
                verificaStatusControles();
        }
        #endregion

        #region Evento Botao Excluir
        private void btnExcluir_Click(object sender, EventArgs e)
        {
                this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
                if (MessageBox.Show("Você está prestes a excluir o registro, deseja realmente continuar?", "FuturaData - Gestão de Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.opcao = 2;
                    this.executarOperacao();
                    this.carregarDados("campos e DataGridView");
                    tbxPalavraChaveBusca.Text = "";
                    tbxPalavraChaveBusca_TextChanged(sender, e);
                }
        }

        #endregion Evento btnExcluir

        #region Evento Botao Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
            this.DestravaBotaoOpcoes();
            this.carregarDados("campos e DataGridView");
            this.Refresh();
        }
        #endregion

        #endregion Botões Novo, Alterar, Excluir, salvar Cadastros E cancelar operação

        #region Eventos dos Botões Anterior, Último, Próximo, e Primeiro Cadastro

        #region Evento Anterior
        private void btnCadAnterior_Click(object sender, EventArgs e)
        {
            if (indice > 0)
            {
                this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
                this.indice--;
                this.carregarDados("campos e DataGridView");
            }
            else
                MessageBox.Show("Você está no primeiro Cadastro!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Evento Próximo
        private void btnProximoCad_Click(object sender, EventArgs e)
        {
            if (indice != arrayClientes.Length-1)
            {
                this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
                this.indice++;
                this.carregarDados("campos e DataGridView");
            }
            else
                MessageBox.Show("Você está no ultimo Cadastro!", "FuturaData - Gestão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Evento Primeiro
        private void btnPrimeiroCad_Click(object sender, EventArgs e)
        {            
            this.indice = 0;
            this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
            this.carregarDados("campos e DataGridView");
        }
        #endregion

        #region Evento Último
        private void btnUltimoCad_Click(object sender, EventArgs e)
        {
            this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
            this.indice = arrayClientes.Length-1;
            this.carregarDados("campos e DataGridView");
        }
        #endregion Último

        #endregion Eventos dos Botões anterior, ultimo, proximo, e primeiro Cadastro
        
        #region Evento DoubleClick sobre o ListView que Carrega um Cliente
        private void lvwPesquisaClientes_DoubleClick(object sender, EventArgs e)
        {
            string cod_Sist = lvwPesquisaClientes.SelectedItems[0].SubItems[1].Text.ToString().Trim();
            int indiceEncontrado = 0;
            foreach(iModCliente item in arrayClientes)
            {
                if(item.Pk_Codigo.ToString() == cod_Sist)
                {
                    indice = indiceEncontrado;
                }
                indiceEncontrado++;
            }
            this.carregarDados("carrega tudo");
            this.tbcCadastroCliente.SelectedTab = this.tbpInformacoesCliente;
        }
        #endregion

        #region Evento das Radio Buttons
        private void rdbNomeCliente_CheckedChanged(object sender, EventArgs e)
        {
            cod_Pesq = 2;
            tbxPalavraChaveBusca.Clear();
            tbxPalavraChaveBusca.Focus();
        }
        
        private void rdbCnpjCpf_CheckedChanged(object sender, EventArgs e)
        {
            cod_Pesq = 1;
            tbxPalavraChaveBusca.Clear();
            tbxPalavraChaveBusca.Focus();
        }
        
        private void rdbPessoaFisica_CheckedChanged(object sender, EventArgs e)
        {
            verificaStatusControles();
        }

        private void rdbPessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            verificaStatusControles();
        }
        #endregion Evento das radio Buttons
                
        #region Evento sobre a TextBox de Pesquisa
        private void tbxPalavraChaveBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lvwPesquisaClientes.Items.Count == 0)
                {
                    tbxPalavraChaveBusca.Clear();
                    tbxPalavraChaveBusca.Focus();
                }
                else
                {
                    this.lvwPesquisaClientes.Focus();
                    this.lvwPesquisaClientes.Select();
                    this.lvwPesquisaClientes.Items[0].Selected = true;
                    this.lvwPesquisaClientes.Activation = ItemActivation.OneClick;
                    
                }
            }
        }
        #endregion

        #region Evento do Botao Alterar Imagem do Cliente
        private void btnAlterarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileProcurarImagem = new OpenFileDialog();
            openFileProcurarImagem.Title = "Selecione uma imagem para o seu logo";
            openFileProcurarImagem.InitialDirectory = @"C:\";
            openFileProcurarImagem.RestoreDirectory = true;
            openFileProcurarImagem.Filter = "Imagens JPG (apenas formato JPG) (*.jpg)|*.jpg;";

            if (openFileProcurarImagem.ShowDialog() == DialogResult.OK)
            {
                clsImagem img = new clsImagem();
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

        #region Evento que Pesquisa pelo CEP
        private void tbxCep_Leave(object sender, EventArgs e)
        {
            if (opcao == 0 || opcao == 1 && tbxLogradouro.Text == "")
            {
                clsCEP futuraData = new clsCEP();
                try
                {
                    bool retornoCep = futuraData.retornaCEPBancoDadosFuturaData(tbxCep.Text.Replace("-", ""));
                    if (retornoCep)
                    {
                        DataTable dt_DadosEndereco = new DataTable();
                        dt_DadosEndereco = futuraData.Ds_DadosRetorno.Tables[0];

                        if (dt_DadosEndereco.Rows.Count != 0)
                        {
                            try
                            {
                                tbxLogradouro.Text = dt_DadosEndereco.Rows[0]["endereco"].ToString().ToUpper();
                                tbxBairro.Text = dt_DadosEndereco.Rows[0]["bairro"].ToString().ToUpper();
                                tbxCidade.Text = dt_DadosEndereco.Rows[0]["cidade"].ToString().ToUpper();
                                cbbEstado.Text = dt_DadosEndereco.Rows[0]["uf"].ToString().ToUpper();
                                tbxNumero.Focus();

                            }
                            catch
                            {

                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }
        #endregion
        #endregion
    }//fim classe
}//fim namespace