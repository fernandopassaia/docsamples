using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataContrValidacoes;
using DllFuturaDataCriptografia;
using System.IO;
using DllFuturaDataTCC.Utilitarios;
using DllFuturaDataTCC;
using System.Threading;
using DllInfoSigaUtil.Grafico;

namespace FuturaDataTCC.Utilitarios
{
    public partial class frmConfigSistema : Form
    {
        #region Construtor e Variaveis Internas do Form
        frmInicializacao frmInicial;
        string caminhoImagem = "";
        Byte[] imagemEmBytes;

        public frmConfigSistema(frmInicializacao frmInicia)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            carregaDados();
            if (null == null) // mano isso é a master POG das POGueiras... hahahahaha
            {
                //MessageBox.Show("Tá de zoeira!");
            }
        }
        #endregion
        
        #region Dica de Envio
        private void btnDicasEnvio_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "O Sistema FuturaData Comércio contém um gerenciador inteligente de erros, que sempre quando é gerado uma exceção, o sistema coleta informações sobre o Erro Gerado, como código do erro." + Environment.NewLine + Environment.NewLine + "Enviar essas informações para a FuturaData é importante, pois assim você irá nos ajudar a localizar erros no sistema de maneira mais ágil, e resolve-los de maneira muito mais dinamica. Colabore com o envio do relatório de erro, nenhuma informação pessoal de caixa, faturamento, estoque e etc será enviada a FuturaData! A FuturaData se compromete no contrato desse sistema a manter todas informações em total sigilo e elas só serão usadas para melhoria do sistema. Essa opção também não deixará seu sistema Lento ou Sobrecarregado, o sistema não lhe fará perguntas de envio e não precisará de sua intervenção, fazendo o envio silenciosamente, havendo conexão com a internet. Não havendo, nenhum erro será apresentado e você não será notificado" + Environment.NewLine + Environment.NewLine + "É muito importante para nós o envio! Dessa maneira, a FuturaData poderá coletar os erros mais comuns no sistema e lançar atualizações mais rapidamente, além, de poder coletar informações sobre os erros enviados por sua empresa e lhe auxiliar de maneira mais rápida e prática na resolução dos mesmos! Colabore e permita o envio dessas informações! Obrigado." + Environment.NewLine + Environment.NewLine + "Ativando a opção 'Desejo Guardar Backups de meu BD no Servidor FuturaData!' será enviado automaticamente um backup via internet para o SERVIDOR da FuturaData, isso significa que, seus dados terão sempre uma cópia de segurança junto a FuturaData também. Isso faz com que você possa trabalhar mais tranquilamente. Nenhuma informação pessoal será usada pela FuturaData e nem suas informações distribuidas de qualquer maneira, sendo essa opção, apenas uma questão de mais segurança para você empresa. Você não precisa enviar um backup para a FuturaData, mas tenha sempre um backup seguro de suas informações que também são geradas em seu Servidor. Caso o backup seja muito grande, o sistema não enviará a Futuradata e lhe avisará sobre isso, fique tranqüilo! Os arquivos são compactados com um algoritmo de alta performance, onde bancos de 100MB são reduzidos para até 10MB, sendo muito eficiente o envio!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        #endregion

        #region Dica de fechamento automatico de caixa e impressora
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ativando essas opções, o Sistema Automaticamente irá fazer o Fechamento da Impressora Fiscal e/ou do Caixa. Ou seja? Se você checar a opção e configurar 18:00, todo dia, as 18h, o sistema automaticamente irá fazer a Redução Z da impressora e/ou o Fechamento de Caixa Sozinho. Essa opção facilita para as empresas que tem horário fixo de funcionamento, e não querem cuidar do fechamento do caixa e da impressora.", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Botao Alterar Logo
        private void btnAlterarLogo_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Botao Salvar
        private void btnSalvarInformacoes_Click(object sender, EventArgs e)
        {            
            if(validarCampos() == true)
            {
                if (MessageBox.Show(null, "Atenção: INFORME OS DADOS CORRETAMENTE. O sistema irá gerar informações de IBGE, código do munícipio e outros, a partir das informações inseridas para gerar a NF-e. Não insira informações inválidas - notas serão rejeitadas. Você está prestes a fazer alterações de suas configurações no Sistema. Esses dados no sistema serão usados em seus relatórios, geração dos arquivos digitais da NF-e, orçamentos, envios pela internet e etc. Preencha corretamente as informações. Você deseja continuar? (essa operação pode levar de 5 a 15 segundos para que todas tabelas sejam criadas, por favor aguarde...)", "FuturaData- Configuração do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(tbxIM.Text == "")
                    {
                        tbxIM.Text = "ISENTO";
                    }
                    loading.showLoading(400, 480);
                    bool primeiroCadastro = false;

                    

                    
                    #region dá um set e trabalha o Array de Imagens
                    if (caminhoImagem != "")
                    {
                        long tamanhoDaImagem = 0;
                        FileInfo imagem = new FileInfo(caminhoImagem);
                        tamanhoDaImagem = imagem.Length;
                        imagemEmBytes = new byte[Convert.ToInt32(tamanhoDaImagem)];
                        FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read, FileShare.Read);
                        fs.Read(imagemEmBytes, 0, Convert.ToInt32(tamanhoDaImagem));
                        fs.Close();
                    }
                    #endregion
                    
                    clsConfiguracoes config = new clsConfiguracoes();
                    //bool status = config.newGravaInformacoes

                    tbxIDClienteFD.Text = "0";

                    bool status = config.newGravaInformacoes(tbxNomeFantasia.Text, tbxCNPJ.Text, tbxRazaoSocial.Text, tbxIE.Text, tbxIM.Text,
                        tbxRamoAtividade.Text, tbxCep.Text, cbbEstado.Text, tbxRua.Text, tbxNumero.Text, tbxBairro.Text, tbxCidade.Text,
                        tbxComplemento.Text, tbxTelefone1.Text, tbxTelefone2.Text, tbxTelefone3.Text, tbxFax.Text, tbx0800.Text, tbxCelular1.Text, cbbOperadoraCelular1.Text,
                        tbxCelular2.Text, cbbOperadoraCelular2.Text, tbxCelular3.Text, cbbOperadoraCelular3.Text, tbxNextelID.Text, tbxEmailPrincipal.Text,
                        tbxEmailFinanceiro.Text, tbxEmailCobranca.Text, tbxMSN.Text, tbxSkype.Text, tbxRedeSocial1.Text, tbxRedeSocial2.Text, tbxSite.Text, tbxPessoasESetores.Text, imagemEmBytes, Convert.ToInt32(tbxIDClienteFD.Text),
                        "", DateTime.Now, frmInicial.numeroUsuarioLogado, Convert.ToInt32(tbxIDClienteFD.Text), true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);

                    #region Antigos Métodos que Gravavam Informacoes no Banco de Dados, enviavam e-mail com cadastro e gravavam no FTP
                    //public bool newGravaInformacoes(string nome, string cnpj, string razaoSocial, string ie, string im, string ramoAtividade,
                    //string cep, string estado, string rua, string numero, string bairro, string cidade, string complemento, string telefone1, string telefone2, 
                    //string telefone3, string fax, string telefoneGratuito, string celular1, string operadoraCelular1,string celular2, string operadoraCelular2,
                    //string celular3, string operadoraCelular3, string nextelID, string email, string emailFinanceiro, string emailCobranca, string msn, string skype,
                    //string redeSocial1, string redeSocial2, string site, string pessoasESetores, Byte[] imagemSistemas, int idClienteFD, string senhaPortal,
                    //DateTime dataInstalacaoSistema, int numeroUsuarioLogado, string nomeUsuarioLogado, string nomeHost)

                    //clsEmail email = new clsEmail();
                    //bool status2 = email.enviarCadastroDaEmpresaParaFuturaData(tbxNomeFantasia.Text, tbxRazaoSocial.Text, tbxCNPJ.Text, tbxIE.Text, tbxIM.Text, tbxRamoAtividade.Text,
                    //    cbbEstado.Text, tbxCep.Text, tbxRua.Text, tbxNumero.Text, tbxBairro.Text, tbxCidade.Text, tbxComplemento.Text, tbxTelefone1.Text,
                    //    tbxTelefone2.Text, tbxFax.Text, tbxEmailPrincipal.Text, tbxSite.Text, tbxDataInstalação.Text, tbxDataInstalação.Text, "", "", "",
                    //    false, false,
                    //    "", false, false, caminhoImagem, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);

                   

                    //#region Cria o XML com os dados da empresa e sobe para o FTP da FuturaData (para criar Log dos clientes)
                    //string cnpjEmpresaSemPont = tbxCNPJ.Text.Replace(".", "").Replace("-", "").Replace(",", "").Replace("-", "").Replace("/", "");

                    //if (File.Exists(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont +"-small.xml"))
                    //{
                    //    File.Delete(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont +"-small.xml");
                    //}

                    //XmlDocument doc = new XmlDocument();
                    //XmlNode raiz = doc.CreateElement("Empresa");

                    //doc.AppendChild(raiz);
                    //doc.Save(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml");
                    //doc.Load(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml");

                    ////tbxNomeFantasia.Text, tbxRazaoSocial.Text, tbxCNPJ.Text, tbxIE.Text, tbxIM.Text, tbxRamoAtividade.Text,
                    ////    cbbEstado.Text, tbxCep.Text, tbxRua.Text, tbxNumero.Text, tbxBairro.Text, tbxCidade.Text, tbxComplemento.Text, tbxTelefone1.Text,
                    ////    tbxTelefone2.Text, tbxFax.Text, tbxEmail.Text, tbxSite.Text, tbxDataInstalação.Text, tbxDataUltimoBck.Text, "",

                    //XmlNode linha1 = doc.CreateElement("NomeFantasia");
                    //XmlNode linha2 = doc.CreateElement("RazaoSocial");
                    //XmlNode linha3 = doc.CreateElement("CNPJ");
                    //XmlNode linha4 = doc.CreateElement("InscricaoEstadual");
                    //XmlNode linha5 = doc.CreateElement("InscricaoMunicipal");
                    //XmlNode linha6 = doc.CreateElement("RamoAtividade");
                    //XmlNode linha7 = doc.CreateElement("Estado");
                    //XmlNode linha8 = doc.CreateElement("Cep");
                    //XmlNode linha9 = doc.CreateElement("Rua");
                    //XmlNode linha10 = doc.CreateElement("Numero");
                    //XmlNode linha11 = doc.CreateElement("Bairro");
                    //XmlNode linha12 = doc.CreateElement("Cidade");
                    //XmlNode linha13 = doc.CreateElement("Complemento");
                    //XmlNode linha14 = doc.CreateElement("Telefone1");
                    //XmlNode linha15 = doc.CreateElement("Telefone2");
                    //XmlNode linha16 = doc.CreateElement("Fax");
                    //XmlNode linha17 = doc.CreateElement("Email");
                    //XmlNode linha18 = doc.CreateElement("Site");
                    //XmlNode linha19 = doc.CreateElement("DataInstalacao");
                    //XmlNode linha20 = doc.CreateElement("Sistema");

                    //linha1.InnerText = tbxNomeFantasia.Text;
                    //linha2.InnerText = tbxRazaoSocial.Text;
                    //linha3.InnerText = tbxCNPJ.Text;
                    //linha4.InnerText = tbxIE.Text;
                    //linha5.InnerText = tbxIM.Text;
                    //linha6.InnerText = tbxRamoAtividade.Text;
                    //linha7.InnerText = cbbEstado.Text;
                    //linha8.InnerText = tbxCep.Text;
                    //linha9.InnerText = tbxRua.Text;
                    //linha10.InnerText = tbxNumero.Text;
                    //linha11.InnerText = tbxBairro.Text;
                    //linha12.InnerText = tbxCidade.Text;
                    //linha13.InnerText = tbxComplemento.Text;
                    //linha14.InnerText = tbxTelefone1.Text;
                    //linha15.InnerText = tbxTelefone2.Text;
                    //linha16.InnerText = tbxFax.Text;
                    //linha17.InnerText = tbxEmailPrincipal.Text;
                    //linha18.InnerText = tbxSite.Text;
                    //linha19.InnerText = tbxDataInstalação.Text;
                    //linha20.InnerText = "FuturaData NF-e em 1 Minuto";

                    //doc.SelectSingleNode("/Empresa").AppendChild(linha1);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha2);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha3);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha4);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha5);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha6);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha7);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha8);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha9);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha10);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha11);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha12);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha13);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha14);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha15);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha16);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha17);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha18);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha19);
                    //doc.SelectSingleNode("/Empresa").AppendChild(linha20);

                    //doc.Save(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml");

                    //if (File.Exists(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml"))
                    //{
                    //    clsAcessoFTP acessoFTP = new clsAcessoFTP();
                    //    //ftp://ftp.futuradata.com.br/www/log_clientes/

                    //    acessoFTP.ftpArquivoFTP = "ftp://ftp.futuradata.com.br/www/log_clientes/empresa " + cnpjEmpresaSemPont + "-small.xml";
                    //    acessoFTP.ftpCaminhoArquivoLocal = @"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml";
                    //    acessoFTP.ftpEndereco = "ftp://ftp.futuradata.com.br/www/log_clientes/";
                    //    acessoFTP.ftpModoPassivo = true;
                    //    acessoFTP.ftpSenha = "fdtech03";
                    //    acessoFTP.ftpUsuario = "futuradata";
                    //    bool retornoFTP = acessoFTP.ftpUpload();
                    //    Thread.Sleep(500);
                    //    if (retornoFTP)
                    //    {
                    //        File.Delete(@"c:\FuturaData\TCC\empresa " + cnpjEmpresaSemPont + "-small.xml");
                    //    }
                    //}
                    #endregion

                    if (status == true && frmInicial.primeiroUsoSistema)
                    {
                        config.alterarPapelParedeUsuario(51, true, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            
                        //clsSuporteSistema suporte = new clsSuporteSistema();
                        //suporte.insereInformacoesSuporteTecnico("fernandopassaia@futuradata.com.br", "nfe@futuradata.com.br", 0, "", "");

                        //clsBancos bancos = new clsBancos();
                        //bancos.incluir("Conta Interna", "000", "000", tbxNomeFantasia.Text, tbxCNPJ.Text, frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
                                           
                        loading.stopLoading();
                        Thread.Sleep(500);                        
                        MessageBox.Show("Dados gravados com sucesso. O Sistema está pronto para Uso! Foi criado o primeiro Usuário PADRÃO com todos os previlégios de administrador, anote as informações:" + Environment.NewLine + Environment.NewLine + "Usuario: adm" + Environment.NewLine + "Senha: 1234fd" + Environment.NewLine + Environment.NewLine + "Anote o usuário e senha padrão pois eles serão solicitados no seu próximo login ao sistema. O Sistema fechará automaticamente agora e já está pronto para uso!", "FuturaDataTCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        clsFilaProcessosWindows.finalizarPrograma();
                    }
                    else
                    {
                        if(status != true)
                        {
                            MessageBox.Show("Operação não concluida com sucesso! Por favor verifique. Um log de erros foi gerado! Se o problema persistir, procure a FuturaData", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {                            
                            loading.stopLoading();
                            Thread.Sleep(500);                         
                            MessageBox.Show("Dados gravados com sucesso. O Sistema está pronto para Uso! Foi criado o primeiro Usuário PADRÃO com todos os previlégios de administrador, anote as informações:" + Environment.NewLine + Environment.NewLine + "Usuario: adm" + Environment.NewLine + "Senha: 1234fd" + Environment.NewLine + Environment.NewLine + "Anote o usuário e senha padrão pois eles serão solicitados no seu próximo login ao sistema. O Sistema fechará automaticamente agora e já está pronto para uso!", "FuturaDataTCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFilaProcessosWindows.finalizarPrograma();
                        }
                    }
                }
            }
        }
        #endregion

        #region validaCampos
        public bool validarCampos()
        {
            bool retorno = true;
            
            clsCpfCnpjValidacao validar = new clsCpfCnpjValidacao();
            string cnpj = tbxCNPJ.Text.Replace("/", "");
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");
            cnpj = cnpj.Replace(",", "");
            if (validar.ValidaCNPJ(cnpj) == false && validar.ValidaCPF(cnpj) == false)
            {
                MessageBox.Show("CNPJ ou CPF Inválido. Por favor insira um CNPJ ou CPF Válido!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (cnpj == "00000000000000" || cnpj == "00000000000")
            {
                MessageBox.Show("Informe o CNPJ/CPF de sua empresa corretamente. Essa informação é importante para geração correta das NF-es.", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxBairro.Text == "")
            {
                MessageBox.Show("Informe um Bairro!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxCep.Text == "")
            {
                MessageBox.Show("Informe um CEP!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxCidade.Text == "")
            {
                MessageBox.Show("Informe uma Cidade!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxEmailPrincipal.Text == "")
            {
                MessageBox.Show("Informe seu Email!", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }
                       

            if (tbxNomeFantasia.Text == "")
            {
                MessageBox.Show("Informe o nome Fantasia", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxNumero.Text == "")
            {
                MessageBox.Show("Informe o Número da Rua", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxRamoAtividade.Text == "")
            {
                MessageBox.Show("Informe seu Ramo de Atividade", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxRazaoSocial.Text == "")
            {
                MessageBox.Show("Informe sua Razão Social", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxRua.Text == "")
            {
                MessageBox.Show("Informe sua Rua","FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (tbxTelefone1.Text == "")
            {
                MessageBox.Show("Informe o Telefone 1 (é necessário ter ao menos 1 telefone válido)", "FuturaData- Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            if (caminhoImagem == "")
            {
                if (pctImagemCliente.Image == null)
                {
                    if (MessageBox.Show(null, "Você não selecionou uma IMAGEM para sua empresa. Selecionando a imagem, ela será automáticamente inserida no seu DANFe para NF-e. Caso você não selecione - seu DANFe sairá sem imagem. Deseja realmeite ignorar e não inserir uma imagem?", "FuturaDataTCC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        retorno = false;
                    }
                }
            }



            return retorno;
        }
        #endregion

        #region CarregaDados
        public void carregaDados()
        {
            clsInicializacao ini = new clsInicializacao();
            DataTable dt_DadosSistema = new DataTable();
            tbxIDClienteFD.Text = "0"; //zera - caso não tenha informação irá gravar novocliente...
            bool retorno = ini.retornaDadosCliente(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            if (retorno == true)
            {
                dt_DadosSistema = ini.getDs_DadosRetorno().Tables[0];
                if (dt_DadosSistema.Rows.Count > 0)
                {  
                    tbxNomeFantasia.Text = dt_DadosSistema.Rows[0]["NOME"].ToString().Trim();
                    tbxRazaoSocial.Text = dt_DadosSistema.Rows[0]["RAZAOSOCIAL"].ToString().Trim();
                    tbxCNPJ.Text = dt_DadosSistema.Rows[0]["CNPJ"].ToString().Trim();
                    tbxIE.Text = dt_DadosSistema.Rows[0]["IE"].ToString().Trim();
                    tbxIM.Text = dt_DadosSistema.Rows[0]["IM"].ToString().Trim();
                    tbxRamoAtividade.Text = dt_DadosSistema.Rows[0]["RAMOATIVIDADE"].ToString().Trim();
                    cbbEstado.Text = dt_DadosSistema.Rows[0]["ESTADO"].ToString().Trim();
                    tbxCep.Text = dt_DadosSistema.Rows[0]["CEP"].ToString().Trim();
                    tbxRua.Text = dt_DadosSistema.Rows[0]["RUA"].ToString().Trim();
                    tbxNumero.Text = dt_DadosSistema.Rows[0]["NUMERO"].ToString().Trim();
                    tbxBairro.Text = dt_DadosSistema.Rows[0]["BAIRRO"].ToString().Trim();
                    tbxCidade.Text = dt_DadosSistema.Rows[0]["CIDADE"].ToString().Trim();
                    tbxComplemento.Text = dt_DadosSistema.Rows[0]["COMPLEMENTO"].ToString().Trim();
                    tbxTelefone1.Text = dt_DadosSistema.Rows[0]["TELEFONE1"].ToString().Trim();
                    tbxTelefone2.Text = dt_DadosSistema.Rows[0]["TELEFONE2"].ToString().Trim();
                    tbxFax.Text = dt_DadosSistema.Rows[0]["FAX"].ToString().Trim();
                    tbxEmailPrincipal.Text = dt_DadosSistema.Rows[0]["EMAIL"].ToString().Trim();
                    tbxSite.Text = dt_DadosSistema.Rows[0]["SITE"].ToString().Trim();
                    //tbxDataInstalação.Text = dt_DadosSistema.Rows[0]["DATAINSTALACAOSISTEMA"].ToString().Trim();                    
                    //cbbModeloImpressora.Text = dt_DadosSistema.Rows[0]["MODELOIMPRESSORA"].ToString().Trim();   
                    //tbxMensagemAgradecimento.Text = dt_DadosSistema.Rows[0]["MENSAGEMAGRADECIMENTO"].ToString().Trim();
                    //tbxSuporteIDCliente.Text = dt_DadosSistema.Rows[0]["NUMEROCLIENTEFUTURADATA"].ToString().Trim();
                    //tbxSuporteRefFinanceiro.Text = dt_DadosSistema.Rows[0]["EMAILCONTATOFINANCEIRO"].ToString().Trim();
                    //tbxSuporteRefSuporte.Text = dt_DadosSistema.Rows[0]["EMAILCONTATOSUPORTE"].ToString().Trim();
                    //tbxSuporteSenhaPortal.Text = new clsCriptografia().Descriptografar(dt_DadosSistema.Rows[0]["SENHAPORTAL"].ToString().Trim());

                    tbxTelefone3.Text = dt_DadosSistema.Rows[0]["TELEFONE3"].ToString().Trim();
                    tbx0800.Text = dt_DadosSistema.Rows[0]["TELEFONEGRATUITO"].ToString().Trim();
                    tbxCelular1.Text = dt_DadosSistema.Rows[0]["CELULAR1"].ToString().Trim();
                    cbbOperadoraCelular1.Text = dt_DadosSistema.Rows[0]["OPERADORA1"].ToString().Trim();
                    tbxCelular2.Text = dt_DadosSistema.Rows[0]["CELULAR2"].ToString().Trim();
                    cbbOperadoraCelular2.Text = dt_DadosSistema.Rows[0]["OPERADORA2"].ToString().Trim();
                    tbxCelular3.Text = dt_DadosSistema.Rows[0]["CELULAR3"].ToString().Trim();
                    cbbOperadoraCelular3.Text = dt_DadosSistema.Rows[0]["OPERADORA3"].ToString().Trim();
                    tbxNextelID.Text = dt_DadosSistema.Rows[0]["NEXTELID"].ToString().Trim();
                    
                    tbxEmailFinanceiro.Text = dt_DadosSistema.Rows[0]["EMAILFINANCEIRO"].ToString().Trim();
                    tbxEmailCobranca.Text = dt_DadosSistema.Rows[0]["EMAILCOBRANCA"].ToString().Trim();
                    tbxMSN.Text = dt_DadosSistema.Rows[0]["MSN"].ToString().Trim();
                    tbxSkype.Text = dt_DadosSistema.Rows[0]["SKYPE"].ToString().Trim();
                    tbxRedeSocial1.Text = dt_DadosSistema.Rows[0]["REDE_SOCIAL1"].ToString().Trim();
                    tbxRedeSocial2.Text = dt_DadosSistema.Rows[0]["REDE_SOCIAL2"].ToString().Trim();
                    
                    tbxPessoasESetores.Text = dt_DadosSistema.Rows[0]["PESSOASESETORES"].ToString().Trim();
                    
                    tbxIDClienteFD.Text = dt_DadosSistema.Rows[0]["IDCLIENTE_FD"].ToString().Trim();
                    //tbxSenhaPortalCRMWeb.Text = dt_DadosSistema.Rows[0]["SENHA_PORTAL"].ToString().Trim();
                    
                    string testeImagem = dt_DadosSistema.Rows[0]["IMAGEM_BINARIO"].ToString();

                    if (testeImagem != "") //carrega a imagem
                    {
                        try
                        {
                            Byte[] imagemCarregada;
                            imagemCarregada = (Byte[])(dt_DadosSistema.Rows[0]["IMAGEM_BINARIO"]);
                            MemoryStream streamMemory = new MemoryStream(imagemCarregada);
                            pctImagemCliente.Image = Image.FromStream(streamMemory);
                            imagemEmBytes = imagemCarregada; //transfere para o objeto que será usado no caso de uma inserção ou atualização
                            testeImagem = null;
                        }
                        catch (Exception) { }

                    }
                    else
                    {
                        testeImagem = null;
                        pctImagemCliente.Image = FuturaDataTCC.Properties.Resources._default;
                    }

                    
                    if (cbbOperadoraCelular1.Text == "")
                    {
                        cbbOperadoraCelular1.Text = "Vivo";
                    }
                    if (cbbOperadoraCelular2.Text == "")
                    {
                        cbbOperadoraCelular2.Text = "Tim";
                    }
                    if (cbbOperadoraCelular3.Text == "")
                    {
                        cbbOperadoraCelular3.Text = "Claro";
                    }
                    
                    if (tbxIDClienteFD.Text == "")
                    {
                        tbxIDClienteFD.Text = "0";
                    }
                    tbxIE.Text = "ISENTO";
                    tbxIM.Text = "ISENTO";
                    tbxNomeFantasia.Focus();
                }
                else
                {
                    //tbxDataInstalação.Text = DateTime.Now.ToString();
                    tbxIDClienteFD.Text = "0";
                    //tbxSenhaPortalCRMWeb.Text = "1234fd";
                    cbbOperadoraCelular1.Text = "Vivo";
                    cbbOperadoraCelular2.Text = "Vivo";
                    cbbOperadoraCelular3.Text = "Vivo";
                    tbxIE.Text = "ISENTO";
                    tbxIM.Text = "ISENTO";
                    tbxNomeFantasia.Focus();
                    //tbxDataUltimoBck.Text = DateTime.Now.ToString();                    
                }

                DataTable dt_DadosLicenca = new DataTable();
                clsConfiguracoes config = new clsConfiguracoes();
                clsAtivacaoSoftware ativa = new clsAtivacaoSoftware(new clsConexao().recuperaStringConexaoSQLServer());
                bool retorno2 = ativa.obterInfoLicencaSistema(frmInicial.numeroUsuarioLogado, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);

                if (retorno2 == true)
                {
                    try
                    {
                    dt_DadosLicenca = config.getDs_DadosRetorno().Tables[0];
                    if (dt_DadosSistema.HasErrors == true)
                    {
                        string tamanhoBaseDados = new clsInicializacao().retornaTamanhoBaseDeDados(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
                        clsCriptografia crip = new clsCriptografia();
                        string subChave = crip.Descriptografar(dt_DadosLicenca.Rows[0]["SC2323"].ToString().Trim());
                        string chave = crip.Descriptografar(dt_DadosLicenca.Rows[0]["CH4488"].ToString().Trim());
                        string dataExpira = crip.Descriptografar(dt_DadosLicenca.Rows[0]["DE0559"].ToString().Trim());
                        string quantidadeHosts = crip.Descriptografar(dt_DadosLicenca.Rows[0]["QH4689"].ToString().Trim());
                    }
                    }
                    catch
                    {

                    }
                }
            }
        }
        #endregion

        #region Botao de Configuracao de Hosts
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Checked Changed da CheckBox
        private void chkEnviarCadastroFuturaData_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Botao Alterar Imagem
        private void btnAlterarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileProcurarImagem = new OpenFileDialog();
            openFileProcurarImagem.Title = "Selecione uma imagem para o seu logo";
            //openFileProcurarImagem.InitialDirectory = @"C:\";
            openFileProcurarImagem.RestoreDirectory = true;
            openFileProcurarImagem.Filter = "Imagens JPG (apenas formato JPG) (*.jpg)|*.jpg;";

            if (openFileProcurarImagem.ShowDialog() == DialogResult.OK)
            {                
                caminhoImagem = @"c:\FuturaData\retorno.jpg";
                bool retorno = new clsImagem().gerarNovaImagem(1000, 620, openFileProcurarImagem.FileName.ToString(), caminhoImagem);
                
                if (File.Exists("c:\\FuturaData\\TCC\\logo.gif"))
                {
                    File.Delete("c:\\FuturaData\\TCC\\logo.gif");
                }

                if (File.Exists("c:\\FuturaData\\TCC\\logo.jpg"))
                {
                    File.Delete("c:\\FuturaData\\TCC\\logo.jpg");
                }

                bool retorno2 = new clsImagem().gerarNovaImagemDanfe(160, 160, openFileProcurarImagem.FileName.ToString(), "c:\\FuturaData\\TCC\\logo.gif");
                bool retorno3 = new clsImagem().gerarNovaImagemDanfe(160, 160, openFileProcurarImagem.FileName.ToString(), "c:\\FuturaData\\TCC\\logo.jpg");

                if (retorno)//se ele conseguir gerar ele pega o novo caminho da nova imagem
                {
                    caminhoImagem = "c:\\FuturaData\\TCC\\EXPORTADOS\\" + openFileProcurarImagem.SafeFileName.ToString();
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

        #region Método verificarStatusControles
        //desativa alguns controles, dependendo da chk clicada, ele ativa ou não os controles respectivos
        //Serve para desativar/ativar os controles na configuração de segurança
        //e formatar máscaras e desativar/ativar controles para PF/PJ
        private void verificaStatusControles()
        {
            
            tbxCNPJ.Mask = "99.999.999/9999-99";                
            
        }        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            verificaStatusControles();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            verificaStatusControles();
        }
        #endregion

        #region Evento Leave da TbxEmail
        private void tbxEmail_Leave(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Evento Leave da TbxCEP
        private void tbxCep_Leave(object sender, EventArgs e)
        {
            tbxCep.BackColor = SystemColors.Window;
            clsNewFuturaData futuraData = new clsNewFuturaData(new clsConexao().recuperaStringConexaoSQLServer());
            try
            {
                bool retornoCep = futuraData.retornaCEPBancoDadosFuturaData(tbxCep.Text.Replace("-", ""));
                if (retornoCep)
                {
                    DataTable dt_DadosEndereco = new DataTable();
                    dt_DadosEndereco = futuraData.getDs_DadosRetorno().Tables[0];

                    if (dt_DadosEndereco.Rows.Count != 0)
                    {
                        try
                        {
                            tbxRua.Text = dt_DadosEndereco.Rows[0]["endereco"].ToString().ToUpper();
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
        #endregion

        #region Diversos Eventos de Label
        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void pctImagemImpressora_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(null, "Seja bem vindo ao Sistema FuturaData." + Environment.NewLine + "Primeiramente gostariamos de informar que você está em um ambiente seguro, e que todas as informações aqui serão salvas em um banco de dados seguro e criptografado, e essas informações serão apenas usadas em seu próprio sistema ou pela FuturaData para lhe fornecer o melhor Suporte!" + Environment.NewLine + Environment.NewLine + "Aconselhamos o preenchimento correto das informações e de todos os campos possíveis. Os seus telefones inclusive móveis devem ser preenchidos assim como as pessoas e setores de sua empresa, pois dessa forma a Futuradata poderá lhe localizar facilmente e lhe auxiliar na hora que um Suporte se faça necessário." + Environment.NewLine + Environment.NewLine + "Além disso, informações básicas (como CNPJ, Razão Social, Fantasia, Endereço, Telefone) serão usados em todo o sistema, como Emissão de NFe, Cupom Fiscal, Relatórios entre outros. O preenchimento com informações incorretas poderá causar o mau funcionamento do sistema ou recusão e geração de arquivos fiscais incorretos. Até mesmo o Logotipo é importante, pois será usado no DANFe e Cotações Impressas em Jato de Tinta. (nota: Os campos em Amarelo são os quais são obrigatórios!)" + Environment.NewLine + Environment.NewLine + "Caso você tenha dúvidas, procure nosso suporte para seu auxílio ou leia nosso Contrato com relação a segurança e preservação de seus dados. Desejamos uma Excelente Experiência em Nosso Sistema - Seja bem Vindo!", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(sender, null);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(null, "No seu primeiro Cadastro - caso haja conexão com a internet, a FuturaData irá habilitar seus serviços de CRM Online. Através de um ID de cliente (que será gerado automaticamente) você poderá DIRETO, de dentro do sistema, com apenas 1 único clique: " + Environment.NewLine + Environment.NewLine + "Abrir um CHAT para Suporte, Efetuar Chamados e Reportar Erros, Efetuar Ativação Online Automatica, Verificar Serviços Hospedados nas Nuvens, Verificar Extrato Financeiro e muito mais..." + Environment.NewLine + Environment.NewLine + "Caso você ainda não tenha um ID (ou seja 0) basta conectar seu computador a Internet e SALVAR novamente os dados de sua empresa - o ID será gerado automaticamente. Caso você já tenha um ID - basta utilizar todos os benefícios procurando pelo ícone 'Super Suporte' na tela principal do sistema.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }//fim classe
}//fim namespace
