using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataTCC.Utilitarios;
using Microsoft.Reporting.WinForms;
using System.IO;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;

namespace FuturaDataTCC.Relatorios.Orcamento
{
    public partial class frmImpressaoRelPedido : Form
    {
        #region Método Construtor e Variaveis Internas
        frmInicializacao frmInicial;        
        clsNewContasMatematicas newContas = new clsNewContasMatematicas();
        iConOrcamento controlOrcamento = new iConOrcamento();
        int codigoPedido = 0;
        string acrescimoOuDesconto = "";
        bool ordemProducao = false;
        string tipoFrete = "";
        string prazoEntrega = "";
        string garantia = "";
        string validadeProposta = "";
        string formaPagto = "";
        string qtdItens = "";
        string valorFinal = "";
        public frmImpressaoRelPedido(int codigoPedido, frmInicializacao frmInicia, string emailEnviar, bool OP, string tipoFret, string prazoEntreg, string garanti, string validadePropost, string formaPagt, string qtdIte, string valorTot)
        {
            InitializeComponent();
            frmInicial = frmInicia;
            this.codigoPedido = codigoPedido;

            tipoFrete = tipoFret;
            prazoEntrega = prazoEntreg;
            garantia = garanti;
            validadeProposta = validadePropost;
            formaPagto = formaPagt;
            qtdItens = qtdIte;
            valorFinal = valorTot;
        }
        #endregion

        #region Método Gerar Pdf Relatorio
        public void gerarPdfRelatorio()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            string mesAno = DateTime.Now.ToString("MMyyyy");
            
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString();
            byte[] bytes = rpwImpressaoRelatorio.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileInfo arquivo = new FileInfo(@"c:\FuturaData\TCC\Exportados\" + mesAno + @"\pedido_" + codigoPedido.ToString() + ".pdf");
            if (!Directory.Exists(@"c:\FuturaData\TCC\Exportados\" + mesAno))
            {
                Directory.CreateDirectory(@"c:\FuturaData\TCC\Exportados\" + mesAno);
            }

            if (arquivo.Exists)
            {
                try
                {
                    arquivo.Delete();
                }
                catch (IOException)
                {                    
                 
                }                
            }

            FileStream fs = new FileStream(@"c:\FuturaData\TCC\Exportados\" + mesAno + @"\pedido_" + codigoPedido.ToString() + ".pdf", FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
        #endregion

        #region Método Sub dados Clientes
        public void SetSubDadosClientes(object sender, SubreportProcessingEventArgs e)
        {
            controlOrcamento.modOrcamento.PkCodigo = codigoPedido;
            //cria o DataTable que será usado no relatório
            DataTable dt_DadosClientePedido = new DataTable();
            dt_DadosClientePedido = controlOrcamento.cRetornaDadosOrcamentoRelatorio(codigoPedido).Tables[0];

            e.DataSources.Add(new ReportDataSource("FDCORPORATEERPDataSet_Pedido_sp_RELATORIO_DADOS_CLIENTE_PEDIDO", dt_DadosClientePedido));
        }
        #endregion

        #region Método Sub dados Pedido Clientes
        public void SetSubDadosPedidoEItens(object sender, SubreportProcessingEventArgs e)
        {
            controlOrcamento.modOrcamento.PkCodigo = codigoPedido;
            iModItensOrcamento[] itensOrcamento = controlOrcamento.cObterProdutosDeUmOrcamento();
            DataTable dt_DadosPedido = new DataTable();
            dt_DadosPedido.Columns.Add("QUANTIDADE");
            dt_DadosPedido.Columns.Add("UNIDADE");
            dt_DadosPedido.Columns.Add("DESCRICAO");
            dt_DadosPedido.Columns.Add("VALORUNIT");
            dt_DadosPedido.Columns.Add("VALORTOTAL");
            
            for (int i = 0; i < itensOrcamento.Length; i++)
            {
                DataRow DR = dt_DadosPedido.NewRow();
                DR["QUANTIDADE"] = itensOrcamento[i].Quantidade;
                DR["UNIDADE"] = "UN";
                DR["DESCRICAO"] = itensOrcamento[i].DescricaoAplicacao;
                DR["VALORUNIT"] = itensOrcamento[i].ValorUnit;
                DR["VALORTOTAL"] = itensOrcamento[i].ValorTotal;
                dt_DadosPedido.Rows.Add(DR);
                DR = null;
            }

            e.DataSources.Add(new ReportDataSource("FDCORPORATEERPDataSet_Pedido_sp_RELATORIO_DADOS_PEDIDO", dt_DadosPedido));
        }
        #endregion

        #region Evento Load do Form
        private void frmImpressaoRelPedido_Load(object sender, EventArgs e)
        {
            #region Cria o DataTable que irá preencher ser o source do relatório
            //cria o DataTable que será usado no relatório
            clsConfiguracoes config = new clsConfiguracoes();
            bool retorno = config.retornaDadosEmpresaRel(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);
            DataTable dt_Relatorio = new DataTable();
            if (retorno)
            {
                dt_Relatorio = config.getDs_DadosRetorno().Tables[0];
            }
            #endregion

            #region Cria os objetos do relatório
            //seta o processamento para local
            rpwImpressaoRelatorio.ProcessingMode = ProcessingMode.Local;
            //reseta o componente visual de relatório
            this.rpwImpressaoRelatorio.Reset();
            //seta o nome do relatório que será exibido (nome do projeto + "." + arquivo .rdlc)
            rpwImpressaoRelatorio.LocalReport.ReportEmbeddedResource = "FuturaDataTCC.Relatorios.Orcamento.Relatorio_Pedido_de_Vendas.rdlc";

            rpwImpressaoRelatorio.LocalReport.EnableExternalImages = true;

            //define um objeto para datasource do relatório - para ver, menu REPORT>DataSources no Relatório
            ReportDataSource dataSourceRelatorio = new ReportDataSource("FDCORPORATEERPDataSet_Pedido_sp_DADOS_EMPRESA_REL", dt_Relatorio);
            
            ReportParameter parametro1 = new ReportParameter("NUMEROCOTACAO", codigoPedido.ToString());
            ReportParameter parametro2 = new ReportParameter("DATA", DateTime.Now.ToString());
            ReportParameter parametro4 = new ReportParameter("ACRESCIMOOUDESCONTO", "0");
            

            //colocar aqui o caminho do arquivo
            string LogoPath = "file://c:\\FuturaData\\TCC\\logo.jpg";

            //esse parâmetro deve ser setado em design time no componente image do relatório
            ReportParameter parametro3 = new ReportParameter("pLogoPath",LogoPath);

            ReportParameter parametro5 = new ReportParameter("TIPOFRETE", tipoFrete);
            ReportParameter parametro6 = new ReportParameter("PRAZOENTREGA", prazoEntrega);
            ReportParameter parametro7 = new ReportParameter("GARANTIA", garantia);
            ReportParameter parametro8 = new ReportParameter("VALIDADEPROPOSTA", validadeProposta);
            ReportParameter parametro9 = new ReportParameter("FORMAPAGTO", formaPagto);
            ReportParameter parametro10 = new ReportParameter("QTD_PRODUTOS", qtdItens);
            ReportParameter parametro11 = new ReportParameter("VALORFINAL", valorFinal);


            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro1);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro2);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro3);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro4);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro5);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro6);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro7);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro8);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro9);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro10);
            rpwImpressaoRelatorio.LocalReport.SetParameters(parametro11);

            //define o datasource para o relatório FDLOJAMOVEISDataSet1.xsd
            rpwImpressaoRelatorio.LocalReport.DataSources.Add(dataSourceRelatorio);




            rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosClientes);
            rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosPedidoEItens);
            rpwImpressaoRelatorio.RefreshReport();

            #endregion
        }
        #endregion
    }//fim classe
}//fim namespace