using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using Microsoft.Reporting.WinForms;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Controllers;

namespace FuturaDataTCC.Relatorios.Caixa
{
    public partial class frmNewRelatAnalCaixa : Form
    {
        #region Construtor do Form e Variaveis Internas
        frmInicializacao frmInicial;
        DataTable dt_DadosVendas = new DataTable();
        DataTable dt_DadosEstoque = new DataTable();
        DataTable dt_VendasVista = new DataTable();        
        DataTable dt_MovimentoFinanceiro = new DataTable();
        iConCaixa controlCaixa = new iConCaixa();
        clsNewContasMatematicas newContas = new clsNewContasMatematicas();
        decimal valorTotalPedidos = 0;
        decimal valorTotalBrutoPedidos = 0;
        string caixa = "";
        decimal valorAVista = 0;
        string valorabertura = "";
        
        string estornos = "";
        string quantidadeitensvend = "";
        string quantidadeitensest = "";
        string vendasavista = "";
        string valorbrutovendido = "";
        string valorliquidovendido = "";
        string valesrecebidos = "";
        string valorfechamentoparcial = "";
        string valorfechamentofinal = "";
        string caminhoGrafico = "";
        public frmNewRelatAnalCaixa(frmInicializacao frmIni, int idCaixa, string valorabertura, string estornos, string quantidadeitensvend, string quantidadeitensest, string vendasavista, string valorbrutovendido, string valorliquidovendido, string valorfechamentoparcial, string valorfechamentofinal, string caminhoGrafico)
        {
            InitializeComponent();
            frmInicial = frmIni;

            this.valorabertura = valorabertura;            
            this.estornos = estornos;
            this.quantidadeitensvend = quantidadeitensvend;
            this.quantidadeitensest = quantidadeitensest;
            this.vendasavista = vendasavista;            
            this.valorbrutovendido = valorbrutovendido;
            this.valorliquidovendido = valorliquidovendido;
            
            this.valorfechamentoparcial = valorfechamentoparcial;
            this.valorfechamentofinal = valorfechamentofinal;
            this.caminhoGrafico = caminhoGrafico;
            //obterPedidosFechadosPorIDCaixa
            controlCaixa.modCaixa.IdCaixa = Convert.ToInt32(idCaixa);
            bool retorno = controlCaixa.daoCaixa.dObterPedidosFechadosPorCaixa(controlCaixa.modCaixa);
            if (retorno)
            {
                dt_DadosVendas = controlCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
            }

            bool retorno2 = controlCaixa.cObterMovimentacaoEstoquePorIDCaixa();
            if (retorno2)
            {
                dt_DadosEstoque = controlCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
            }

            bool retorno3 = controlCaixa.cObterMovimentacaoFinanceiraPorIDCaixa();
            if(retorno3)
            {
                dt_VendasVista = controlCaixa.daoCaixa.Ds_DadosRetorno.Tables[0];
            }


            dt_MovimentoFinanceiro.Columns.Add("FK_CODIGOPEDIDO");
            dt_MovimentoFinanceiro.Columns.Add("NUMEROPARCELA");
            dt_MovimentoFinanceiro.Columns.Add("NUMEROTOTALPARCELAS");
            dt_MovimentoFinanceiro.Columns.Add("DESCRICAO_SUBCATEGORIA");
            dt_MovimentoFinanceiro.Columns.Add("VALORBRUTO");
            dt_MovimentoFinanceiro.Columns.Add("VALORPAGO");
            
            //dt_MovimentoFinanceiro = dt_VendasVista;
            
            decimal vendasAVista = 0;
            for (int i = 0; i < dt_VendasVista.Rows.Count; i++)
            {
                vendasAVista = vendasAVista + Convert.ToDecimal(dt_VendasVista.Rows[i]["VALORPAGO"].ToString());
            }


            DataRow DRd = dt_MovimentoFinanceiro.NewRow();
            DRd["FK_CODIGOPEDIDO"] = "........";
            DRd["NUMEROPARCELA"] = "......";
            DRd["NUMEROTOTALPARCELAS"] = "......";
            DRd["DESCRICAO_SUBCATEGORIA"] = "Vendas a Vista... Total R$ " + newContas.newValidaAjustaArredonda2CasasDecimais(vendasAVista.ToString());
            DRd["VALORBRUTO"] = "........";
            DRd["VALORPAGO"] = "........";
            dt_MovimentoFinanceiro.Rows.Add(DRd);

            for (int i = 0; i < dt_VendasVista.Rows.Count; i++)
            {
                DataRow DR = dt_MovimentoFinanceiro.NewRow();
                DR["FK_CODIGOPEDIDO"] = dt_VendasVista.Rows[i]["FK_CODIGOPEDIDO"].ToString();
                DR["NUMEROPARCELA"] = dt_VendasVista.Rows[i]["NUMEROPARCELA"].ToString();
                DR["NUMEROTOTALPARCELAS"] = dt_VendasVista.Rows[i]["NUMEROTOTALPARCELAS"].ToString();
                DR["DESCRICAO_SUBCATEGORIA"] = dt_VendasVista.Rows[i]["DESCRICAO_SUBCATEGORIA"].ToString();
                DR["VALORBRUTO"] = dt_VendasVista.Rows[i]["VALORBRUTO"].ToString();
                DR["VALORPAGO"] = dt_VendasVista.Rows[i]["VALORPAGO"].ToString();
                valorAVista = valorAVista + Convert.ToDecimal(dt_VendasVista.Rows[i]["VALORBRUTO"].ToString());
                dt_MovimentoFinanceiro.Rows.Add(DR);
                if (caixa == "")
                {
                    caixa = "CAIXA: " + dt_VendasVista.Rows[i]["IDENTIFICACAO_CAIXA"].ToString();
                }
                DR = null;
            }
        }
        #endregion

        #region Evento Load do Form
        private void frmNewRelatAnalCaixa_Load(object sender, EventArgs e)
        {
            #region Cria os objetos do relatório de Vendas

            for (int i = 0; i < dt_DadosVendas.Rows.Count; i++)
            {
                valorTotalPedidos = valorTotalPedidos + Convert.ToDecimal(dt_DadosVendas.Rows[i]["VALORFINALPAGO"].ToString());
                valorTotalBrutoPedidos = valorTotalBrutoPedidos + Convert.ToDecimal(dt_DadosVendas.Rows[i]["VALORBRUTOORCT"].ToString());
            }

            //seta o processamento para local
            rpwRelatCaixaPedidosFechados.ProcessingMode = ProcessingMode.Local;
            //reseta o componente visual de relatório
            this.rpwRelatCaixaPedidosFechados.Reset();
            //seta o nome do relatório que será exibido (nome do projeto + "." + arquivo .rdlc)

            rpwRelatCaixaPedidosFechados.LocalReport.ReportEmbeddedResource = "FuturaDataTCC.Relatorios.Caixa.rptRelatPedidos.rdlc";


            //define um objeto para datasource do relatório - para ver, menu REPORT>DataSources no Relatório
            ReportDataSource dataSourceRelatorio = new ReportDataSource("dsPedidosFechadosPorCaixa", dt_DadosVendas);

            ReportParameter parametro1 = new ReportParameter("VALORTOTAL", newContas.newValidaAjustaArredonda2CasasDecimais(valorTotalPedidos.ToString()));
            rpwRelatCaixaPedidosFechados.LocalReport.SetParameters(parametro1);

            ReportParameter parametro1a = new ReportParameter("VALORBRUTO", newContas.newValidaAjustaArredonda2CasasDecimais(valorTotalBrutoPedidos.ToString()));
            rpwRelatCaixaPedidosFechados.LocalReport.SetParameters(parametro1a);

            //define o datasource para o relatório FDLOJAMOVEISDataSet1.xsd
            rpwRelatCaixaPedidosFechados.LocalReport.DataSources.Add(dataSourceRelatorio);
            
            //rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosClientes);
            //rpwRelatCaixaPedidosFechados.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosPedidoEItens);
            rpwRelatCaixaPedidosFechados.RefreshReport();
            #endregion

            #region Cria os objetos do relatório de estoque
            //seta o processamento para local
            rpwRelatCaixaMovimentoEstoque.ProcessingMode = ProcessingMode.Local;
            //reseta o componente visual de relatório
            this.rpwRelatCaixaMovimentoEstoque.Reset();
            //seta o nome do relatório que será exibido (nome do projeto + "." + arquivo .rdlc)

            rpwRelatCaixaMovimentoEstoque.LocalReport.ReportEmbeddedResource = "FuturaDataTCC.Relatorios.Caixa.rptRelatEstoque.rdlc";

            decimal qtdVendidos = 0;
            decimal qtdEstornados = 0;
            decimal valorVendidos = 0;
            decimal valorEstornados = 0;

            for (int i = 0; i < dt_DadosEstoque.Rows.Count; i++)
            {
                if (dt_DadosEstoque.Rows[i]["TIPO_ES"].ToString() == "1") //1 = entrada/estorno
                {
                    qtdEstornados = qtdEstornados + Convert.ToDecimal(dt_DadosEstoque.Rows[i]["QUANTIDADE"].ToString());
                    valorEstornados = valorEstornados + Convert.ToDecimal(dt_DadosEstoque.Rows[i]["VALORTOTAL"].ToString());
                }
                else
                {
                    qtdVendidos = qtdVendidos + Convert.ToDecimal(dt_DadosEstoque.Rows[i]["QUANTIDADE"].ToString());
                    valorVendidos = valorVendidos + Convert.ToDecimal(dt_DadosEstoque.Rows[i]["VALORTOTAL"].ToString());
                }
            }



            //define um objeto para datasource do relatório - para ver, menu REPORT>DataSources no Relatório
            ReportDataSource dataSourceRelatorio2 = new ReportDataSource("dsMovimentoEstoque", dt_DadosEstoque);


            ReportParameter parametro2 = new ReportParameter("QTD_ITENSVEND", newContas.newValidaAjustaArredonda2CasasDecimais(qtdVendidos.ToString()));
            rpwRelatCaixaMovimentoEstoque.LocalReport.SetParameters(parametro2);

            ReportParameter parametro3 = new ReportParameter("QTD_ITENSEST", newContas.newValidaAjustaArredonda2CasasDecimais(qtdEstornados.ToString()));
            rpwRelatCaixaMovimentoEstoque.LocalReport.SetParameters(parametro3);

            ReportParameter parametro4 = new ReportParameter("VALORVENDIDO", newContas.newValidaAjustaArredonda2CasasDecimais(valorVendidos.ToString()));
            rpwRelatCaixaMovimentoEstoque.LocalReport.SetParameters(parametro4);

            ReportParameter parametro5 = new ReportParameter("VALORESTORNADO", newContas.newValidaAjustaArredonda2CasasDecimais(valorEstornados.ToString()));
            rpwRelatCaixaMovimentoEstoque.LocalReport.SetParameters(parametro5);

            //define o datasource para o relatório FDLOJAMOVEISDataSet1.xsd
            rpwRelatCaixaMovimentoEstoque.LocalReport.DataSources.Add(dataSourceRelatorio2);

            //rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosClientes);
            //rpwRelatCaixaPedidosFechados.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosPedidoEItens);
            rpwRelatCaixaMovimentoEstoque.RefreshReport();
            #endregion
            
            #region Cria os objetos do relatório de movimentacao financeira
            //seta o processamento para local
            rpwRelatCaixaMovimFinanceira.ProcessingMode = ProcessingMode.Local;
            //reseta o componente visual de relatório
            this.rpwRelatCaixaMovimFinanceira.Reset();
            //seta o nome do relatório que será exibido (nome do projeto + "." + arquivo .rdlc)

            rpwRelatCaixaMovimFinanceira.LocalReport.ReportEmbeddedResource = "FuturaDataTCC.Relatorios.Caixa.rptRelatCaixaFinanc.rdlc";
            //VALOR BRUTO
            //VALOR PAGO

            decimal valorbruto = 0;
            decimal valorliquido = 0;
            
            for (int i = 0; i < dt_MovimentoFinanceiro.Rows.Count; i++)
            {
                if (dt_MovimentoFinanceiro.Rows[i]["VALORBRUTO"].ToString().Substring(0, 1) != ".")
                {
                    valorbruto = valorbruto + Convert.ToDecimal(dt_MovimentoFinanceiro.Rows[i]["VALORBRUTO"].ToString());
                    valorliquido = valorliquido + Convert.ToDecimal(dt_MovimentoFinanceiro.Rows[i]["VALORPAGO"].ToString());                   
                }
            }

            //define um objeto para datasource do relatório - para ver, menu REPORT>DataSources no Relatório
            ReportDataSource dataSourceRelatorio3 = new ReportDataSource("ds_MovFinancCaixa", dt_MovimentoFinanceiro);
            
            ReportParameter parametro6 = new ReportParameter("VALORLIQUIDO", newContas.newValidaAjustaArredonda2CasasDecimais(valorliquido.ToString()));
            rpwRelatCaixaMovimFinanceira.LocalReport.SetParameters(parametro6);

            ReportParameter parametro7 = new ReportParameter("CAIXA", caixa);
            rpwRelatCaixaMovimFinanceira.LocalReport.SetParameters(parametro7);

            ReportParameter parametro8 = new ReportParameter("VALORAVISTA", newContas.newValidaAjustaArredonda2CasasDecimais(valorAVista.ToString()));
            rpwRelatCaixaMovimFinanceira.LocalReport.SetParameters(parametro8);

            //define o datasource para o relatório FDLOJAMOVEISDataSet1.xsd
            rpwRelatCaixaMovimFinanceira.LocalReport.DataSources.Add(dataSourceRelatorio3);

            //rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosClientes);
            //rpwRelatCaixaPedidosFechados.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosPedidoEItens);
            rpwRelatCaixaMovimFinanceira.RefreshReport();
            #endregion

            #region Cria os objetos do relatório de fechamento
            //seta o processamento para local
            rpwRelatCaixaConfFechamento.ProcessingMode = ProcessingMode.Local;
            //reseta o componente visual de relatório
            this.rpwRelatCaixaConfFechamento.Reset();
            //seta o nome do relatório que será exibido (nome do projeto + "." + arquivo .rdlc)

            rpwRelatCaixaConfFechamento.LocalReport.ReportEmbeddedResource = "FuturaDataTCC.Relatorios.Caixa.rptRelatFechtCaixa.rdlc";

            rpwRelatCaixaConfFechamento.LocalReport.EnableExternalImages = true;

            //VALOR BRUTO
            //VALOR PAGO
            
            //define um objeto para datasource do relatório - para ver, menu REPORT>DataSources no Relatório
            //ReportDataSource dataSourceRelatorio4 = new ReportDataSource("ds_MovFinancCaixa", dt_MovimentoFinanceiro);
            
            ReportParameter parametroFecht1 = new ReportParameter("VALORABERTURA", newContas.newValidaAjustaArredonda2CasasDecimais(valorabertura.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht1);

            ReportParameter parametroFecht4 = new ReportParameter("ESTORNOS", newContas.newValidaAjustaArredonda2CasasDecimais(estornos.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht4);

            ReportParameter parametroFecht5 = new ReportParameter("QUANTIDADEITENSVEND", newContas.newValidaAjustaArredonda2CasasDecimais(quantidadeitensvend.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht5);

            ReportParameter parametroFecht6 = new ReportParameter("QUANTIDADEITENSEST", newContas.newValidaAjustaArredonda2CasasDecimais(quantidadeitensest.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht6);

            ReportParameter parametroFecht7 = new ReportParameter("VENDASAVISTA", newContas.newValidaAjustaArredonda2CasasDecimais(vendasavista.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht7);

            ReportParameter parametroFecht10 = new ReportParameter("VALORBRUTOVENDIDO", newContas.newValidaAjustaArredonda2CasasDecimais(valorbrutovendido.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht10);

            ReportParameter parametroFecht11 = new ReportParameter("VALORLIQUIDOVENDIDO", newContas.newValidaAjustaArredonda2CasasDecimais(valorliquidovendido.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht11);

            ReportParameter parametroFecht12 = new ReportParameter("VALESRECEBIDOS", newContas.newValidaAjustaArredonda2CasasDecimais(valesrecebidos.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht12);

            ReportParameter parametroFecht13 = new ReportParameter("VALORFECHAMENTOPARCIAL", newContas.newValidaAjustaArredonda2CasasDecimais(valorfechamentoparcial.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht13);

            ReportParameter parametroFecht14 = new ReportParameter("VALORFECHAMENTOFINAL", newContas.newValidaAjustaArredonda2CasasDecimais(valorfechamentofinal.ToString()));
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht14);

            ReportParameter parametroFecht15 = new ReportParameter("CAIXA", caixa);
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroFecht15);
            
            //define o datasource para o relatório FDLOJAMOVEISDataSet1.xsd
            //rpwRelatCaixaMovimFinanceira.LocalReport.DataSources.Add(dataSourceRelatorio3);


            //string LogoGraficoRelatorio = "file://c:\\FuturaData\\TCC\\logo.jpg";
            string LogoGraficoRelatorio = "file://" + caminhoGrafico;

            //esse parâmetro deve ser setado em design time no componente image do relatório
            ReportParameter parametroGrafico = new ReportParameter("pLogoPath", LogoGraficoRelatorio);
            rpwRelatCaixaConfFechamento.LocalReport.SetParameters(parametroGrafico);


            //rpwImpressaoRelatorio.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosClientes);
            //rpwRelatCaixaPedidosFechados.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDadosPedidoEItens);
            rpwRelatCaixaConfFechamento.RefreshReport();
            #endregion
        }
        #endregion
    }//fim classe
}//fim namespace
