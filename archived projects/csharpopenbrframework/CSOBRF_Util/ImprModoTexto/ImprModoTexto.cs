using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using CSOBRF_Criptografia;
using System.Windows.Forms;
using CSOBRF_Validacoes;

namespace CSOBRF_Util.ImprModoTexto
{
    #region Construtor da Classe e Variaveis Internas
    public class ImprModoTexto
    {
        private int iRetorno;        

        #region Abre e Fecha Porta Epson
        public void abrePortaEpson()
        {
            iRetorno = EpsonNaoFiscal.IniciaPorta("USB");
            //if (iRetorno == 1)
            //    System.Windows.Forms.MessageBox.Show("Porta de comunicação aberta com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    System.Windows.Forms.MessageBox.Show("Erro ao abrir a porta de comunicação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void fechaPortaEpson()
        {
            iRetorno = EpsonNaoFiscal.FechaPorta();
            //if (iRetorno == 1)
            //    System.Windows.Forms.MessageBox.Show("Porta de comunicação fechada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    System.Windows.Forms.MessageBox.Show("Erro ao fechar a porta de comunicação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Imprime Exemplo
        /// <summary>
        /// Imprime Texto de Exemplo (da FuturaData)
        /// </summary>        
        /// <param name="porta">Porta da Impressora (Lpt1, Com1, Com2)</param>
        /// <returns>Retorna True se Conseguir tudo, False se não</returns>
        public bool imprimeExemplo(string porta)
        {
            if (porta == "")
            {
                porta = "LPT1";
            }
            try
            {
                ComunicacaoImprTexto imp = new ComunicacaoImprTexto();
                imp.IniciarImpressao(porta);
                imp.ImpLFormatacao("FuturaData DllInfoSigaUtil");
                imp.ImpLFormatacao("--------------------------");
                imp.ImpLFormatacao("Componente de impressao em modo texto");
                for (int i = 0; i < 6; i++)
                {
                    imp.ImpLFormatacao("FuturaData Tec.Sist.Info " + i.ToString());
                }
                imp.ImpLFormatacao(imp.NegritoOn + "FuturaData Negrito ligado" + imp.NegritoOff);
                imp.ImpLFormatacao(imp.Expandido + "FuturaData Expan.Ligado" + imp.ExpandidoNormal);
                imp.ImpLFormatacao(imp.Comprimido + "FuturaData Compr.Ligado" + imp.ComprimidoNormal);
                imp.PulaLinha(10);
                imp.FinalizaImpressão();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Imprime Orcamento (Genérico Formatacao FuturaData Business)
        /// <summary>
        /// Imprime um Orçamento em Impressora Não Fiscal Modo Texto
        /// </summary>
        /// <param name="numeroOrc">Numero do Orçamento a Ser Impresso</param>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se conseguir Imprimir, False se não</returns>
        public bool imprimeOrcamento(string porta, string modeloImpressora, string marcaImpressora, bool usarDriverFabricante, int quantidadeColunas, DataTable dt_DadosEmpresa, DataTable dt_ItensOrcamento, int numeroOrcamento, bool gravaNoTxt, string layoutCupom, bool imprimirTipoFrete, bool imprimirPrazoEntrega, bool imprimirGarantia, bool imprimirValidadeProposta, bool usarNegrito, bool usarGilhotina, int qtdLinhas_PularEntreCotacoes, int qtdLinhas_PularFinal, bool ultimaVia, int numeroUsuarioLogado, string nomeUsuarioLogado, string nomeHost)
        {
            NewContasMatematicas contas = new NewContasMatematicas();

            //inicia a impressão do cupom
            try
            {
                if (modeloImpressora.ToUpper() == "TÉRMICA" && !usarDriverFabricante) //aqui significa que a impressora é térmica, mas o driver é genérico
                {
                    #region Impressora Térmica com driver genérico
                    ComunicacaoImprTexto impr = new ComunicacaoImprTexto();
                    
                    string nomeArquivoOutPut = @"c:\futuradata\output_printer.txt"; //FERNANDO - 27062013 Testando impressão do cupom no TXT

                    if (gravaNoTxt)
                    {
                        if (File.Exists(nomeArquivoOutPut))
                        {
                            FileInfo fi = new FileInfo(nomeArquivoOutPut);
                            var TamanhoEmKb = (fi.Length / 1024F);
                            var TamanhoEmMb = ((fi.Length / 1024F) / 1024F);
                            var TamanhoEmGb = (((fi.Length / 1024F) / 1024F) / 1024F);

                            if (TamanhoEmKb > 999)
                            {
                                //verifica se arquivo já ultrapassou 1MB, se sim, exclui ele, manda pras belecas
                                File.Delete(nomeArquivoOutPut);
                            }
                        }
                    }

                    if (gravaNoTxt)
                    {
                        if (!System.IO.File.Exists(nomeArquivoOutPut))
                        {
                            System.IO.File.Create(nomeArquivoOutPut).Close();
                        }
                    }
                    
                    #region Dados da Empresa que Usam o Sistema
                    string nomeEmpresa = dt_DadosEmpresa.Rows[0]["NOME"].ToString().Trim();
                    string cnpj = dt_DadosEmpresa.Rows[0]["CNPJ"].ToString().Trim();
                    string rua = dt_DadosEmpresa.Rows[0]["RUA"].ToString().Trim();
                    string numero = dt_DadosEmpresa.Rows[0]["NUMERO"].ToString().Trim();
                    string bairro = dt_DadosEmpresa.Rows[0]["BAIRRO"].ToString().Trim();
                    string cidade = dt_DadosEmpresa.Rows[0]["CIDADE"].ToString().Trim();
                    string estado = dt_DadosEmpresa.Rows[0]["ESTADO"].ToString().Trim();
                    string telefone1 = dt_DadosEmpresa.Rows[0]["TELEFONE1"].ToString().Trim();
                    string telefone2 = dt_DadosEmpresa.Rows[0]["TELEFONE2"].ToString().Trim();
                    string fax = dt_DadosEmpresa.Rows[0]["FAX"].ToString().Trim();
                    string site = dt_DadosEmpresa.Rows[0]["SITE"].ToString().Trim();

                    string observacaoImprimir = dt_ItensOrcamento.Rows[0]["INFOADICIONAL"].ToString().Trim();
                    string retiradoPor = dt_ItensOrcamento.Rows[0]["FUNCIONARIO_RETIRA"].ToString().Trim();

                    string tipoFrete = "Frete: " + dt_ItensOrcamento.Rows[0]["TIPOFRETE"].ToString().Trim();
                    string prazoEntrega = "Prazo Entrega: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["PRAZOENTREGA"]).ToString("dd/MM/yyyy");
                    string prazoGarantia = "Garantia Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["GARANTIA"]).ToString("dd/MM/yyyy");
                    string prazoProposta = "Proposta Valida Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["VALIDADEPROPOSTA"]).ToString("dd/MM/yyyy");
                    
                    string cliente = "Cliente: " + dt_ItensOrcamento.Rows[0]["NOME_CLIENTE"].ToString();

                    string valorTotalCupom = dt_ItensOrcamento.Rows[0]["VALORFINAL"].ToString().Trim();

                    if (cliente.Length > 40)
                    {
                        cliente = cliente.Substring(0, 40);
                    }

                    string vendedor = "Vendedor: " + dt_ItensOrcamento.Rows[0]["NOME_VENDEDOR"].ToString();
                    if (vendedor.Length > 40)
                    {
                        vendedor = vendedor.Substring(0, 40);
                    }
                    #endregion

                    #region Emissao cabecário do Cupom
                    impr.IniciarImpressao(porta);
                    impr.ImpLFormatacao(nomeEmpresa + " - " + cnpj);

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("clsImprModoTexto: Iniciado Orcamento " + numeroOrcamento.ToString());
                        arquivo.Close();

                        System.IO.TextWriter arquivo2 = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo2.WriteLine(nomeEmpresa + " - " + cnpj);
                        arquivo2.Close();
                    }

                    impr.FinalizaImpressão();
                    
                    impr.IniciarImpressao(porta);
                    //impr.ImpLFormatacao("");
                    if (rua.Length > 36)
                    {
                        rua = rua.Substring(0, 36);
                    }
                    
                    impr.ImpLFormatacao(rua + ", " + numero + " - " + bairro);

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine(rua + ", " + numero + " - " + bairro);
                        arquivo.Close();
                    }
                    
                    
                    impr.FinalizaImpressão();
                    impr.IniciarImpressao(porta);
                    //impr.ImpLFormatacao("");
                    impr.ImpLFormatacao(cidade + " - " + estado + " - " + telefone1);

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine(cidade + " - " + estado + " - " + telefone1);
                        arquivo.Close();
                    }

                    impr.FinalizaImpressão();
                    //impr.ImpLFormatacao(site);
                    impr.IniciarImpressao(porta);
                    if (layoutCupom == "Layout 1")
                    {
                        impr.ImpLFormatacao("*****************************************");
                    }
                    else
                    {
                        impr.ImpLFormatacao("-----------------------------------------");
                    }


                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("******************************************");
                        arquivo.Close();
                    }


                    impr.FinalizaImpressão();
                    //impr.ImpLFormatacao("");
                    //impr.ImpLFormatacao(cliente);
                    impr.IniciarImpressao(porta);                    
                    impr.ImpLFormatacao(vendedor);

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine(vendedor);
                        arquivo.Close();
                    }

                    impr.FinalizaImpressão();


                    Thread.Sleep(400);
                    impr.IniciarImpressao(porta);

                    impr.ImpLFormatacao("Orcamento " + numeroOrcamento.ToString() + " - " + DateTime.Now.ToString());

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("Orcamento " + numeroOrcamento.ToString() + " - " + DateTime.Now.ToString());
                        arquivo.Close();
                    }


                    impr.FinalizaImpressão();
                    //impr.IniciarImpressao(porta);
                    //impr.ImpLFormatacao(impr.NegritoOn + " Numero Venda: " + numeroOrcamento.ToString() + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm") + impr.NegritoOff);
                    //impr.FinalizaImpressão();
                    if(!cliente.ToUpper().Contains("VENDA") && !cliente.ToUpper().Contains("CONSUMIDOR"))
                    {                        
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(cliente);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(cliente);
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();
                    }
                    
                    
                    
                    impr.IniciarImpressao(porta);
                    if (layoutCupom == "Layout 1")
                    {
                        impr.ImpLFormatacao("*****************************************");
                    }
                    else
                    {
                        impr.ImpLFormatacao("----------------------------------------");
                    }
                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("******************************************");
                        arquivo.Close();
                    }

                    impr.FinalizaImpressão();

                    #endregion

                    #region Emissao Itens do Cupom                                        
                    decimal valorTotalDesconto = 0;
                    decimal valorTotalAcrescimo = 0;
                    for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                    {
                        //string codigoProduto = dt_ItensOrcamento.Rows[i]["CODIGO_PRODUTO"].ToString().Trim();
                        string id_ProdutoVenda = dt_ItensOrcamento.Rows[i]["PK_ID"].ToString().Trim();
                        string descricao = dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Trim();
                        string codigoFabric = dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString().Trim();
                        string prateleira = dt_ItensOrcamento.Rows[i]["LOCALESTOQUE"].ToString().Trim();

                        string qtde = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString().Trim());
                        string precoUnitBruto = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString().Trim());
                        string precoUnitLiquido = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString().Trim());
                        string precoFinal = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString().Trim());
                        
                        string descItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                        string acreItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());
                        bool ignorarMudancaPreco = Convert.ToBoolean(dt_ItensOrcamento.Rows[i]["IGNORAR_MUDANCA_PRECOS"].ToString());
                                                
                        string codigoFabrOrig = "(" + id_ProdutoVenda + ") CodFabr: " + codigoFabric;

                        if (descricao.Length > 42)
                        {
                            descricao = descricao.Substring(0, 42);
                        }
                       
                        if (codigoFabrOrig.Length > 42)
                        {
                            codigoFabrOrig = codigoFabrOrig.Substring(0, 42);
                        }

                       
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(codigoFabrOrig);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(codigoFabrOrig);
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();


                        impr.IniciarImpressao(porta);
                        //impr.ImpLFormatacao("");
                        impr.ImpLFormatacao(descricao);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(descricao);
                            arquivo.Close();
                        }

                        //Thread.Sleep(500);
                        impr.FinalizaImpressão();
                        
                        impr.IniciarImpressao(porta);

                        //Se o Preço Unitário Bruto e o Preço Unitário Liquido forem diferentes, é sinal que houve desconto. Ai se estiver "ignorarMudancadePreco" eu vou imprimir o líquido e já era...
                        //caso contrário eu vou imprimir o bruto e o desconto logo abaixo... Caso contrário (situação normal) irei imprimir o valor do produto líquido mesmo, valor normal

                        if(precoUnitBruto != precoUnitLiquido)
                        {
                            if(ignorarMudancaPreco)
                            {
                                impr.ImpLFormatacao("R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal);
                            }
                            else
                            {
                                if (Convert.ToDecimal(precoUnitLiquido) < Convert.ToDecimal(precoUnitBruto))
                                {
                                    impr.ImpLFormatacao("R$ " + precoUnitBruto + " x " + qtde + " - " + descItem + "(desconto) = R$ " + precoFinal);
                                    valorTotalDesconto = valorTotalDesconto + Convert.ToDecimal(descItem);
                                }
                                else
                                {
                                    impr.ImpLFormatacao("R$ " + precoUnitBruto + " x " + qtde + " - " + acreItem + "(acrescimo) = R$ " + precoFinal);                                    
                                    valorTotalAcrescimo = valorTotalAcrescimo + Convert.ToDecimal(acreItem);
                                }
                            }
                        }
                        else
                        {
                            impr.ImpLFormatacao("R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal);
                        }
                        



                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine("R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal);
                            arquivo.Close();
                        }


                        impr.FinalizaImpressão();
                        impr.IniciarImpressao(porta);

                        if (!prateleira.Contains("Padrão"))
                        {
                            impr.ImpLFormatacao(prateleira);
                        }

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(prateleira);
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();

                        Thread.Sleep(300);
                        //impr.FinalizaImpressão();
                        //codigoProduto = null;
                        descricao = null;
                        qtde = null;                        
                        precoFinal = null;
                        codigoFabric = null;
                        //codigoOrig1 = null;
                        descItem = null;
                        prateleira = null;
                        impr.IniciarImpressao(porta);
                        if (layoutCupom == "Layout 1")
                        {
                            impr.ImpLFormatacao("*****************************************");
                        }
                        else
                        {
                            impr.ImpLFormatacao("-----------------------------------------");
                        }
                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine("******************************************");
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();
                    }

                    //Thread.Sleep(200);

                    //impr.IniciarImpressao(porta);
                    //impr.ImpLFormatacao("******************************************");

                    //impr.FinalizaImpressão();
                    //impr.FinalizaImpressão();

                    if (valorTotalDesconto > 0)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(impr.NegritoOn + " Valor Total do Desconto: R$ " + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalDesconto.ToString()) + impr.NegritoOff);
                        
                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(impr.NegritoOn + " Valor Total do Desconto: R$ " + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalDesconto.ToString()) + impr.NegritoOff);
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();
                    }
                    if(valorTotalAcrescimo > 0)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(impr.NegritoOn + " Valor Total do Acrescimo: R$ " + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalAcrescimo.ToString()) + impr.NegritoOff);
                        impr.FinalizaImpressão();
                    }

                    impr.IniciarImpressao(porta);
                    impr.ImpLFormatacao(impr.NegritoOn + " Valor Final: R$ " + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalCupom.ToString()) + impr.NegritoOff);

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine(impr.NegritoOn + " Valor Final: R$ " + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalCupom.ToString()) + impr.NegritoOff);
                        arquivo.Close();
                    }


                    impr.FinalizaImpressão();
                    #endregion

                    #region Final Impressão
                    if (observacaoImprimir != "")
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao("OBSERVACAO: " + observacaoImprimir);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine("OBSERVAÇÃO:" + observacaoImprimir);
                            arquivo.Close();
                        }

                        impr.FinalizaImpressão();
                    }

                    if (retiradoPor != "")
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao("Retirado Por: " + retiradoPor);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine("Retirado Por: " + retiradoPor);
                            arquivo.Close();
                        }
                        impr.FinalizaImpressão();
                    }

                    if (imprimirTipoFrete)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(tipoFrete);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(tipoFrete);
                            arquivo.Close();
                        }
                        impr.FinalizaImpressão();
                    }

                    if (imprimirPrazoEntrega)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(prazoEntrega);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(prazoEntrega);
                            arquivo.Close();
                        }
                        impr.FinalizaImpressão();
                    }

                    if (imprimirGarantia)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(prazoGarantia);

                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(prazoGarantia);
                            arquivo.Close();
                        }
                        impr.FinalizaImpressão();
                    }

                    if (imprimirValidadeProposta)
                    {
                        impr.IniciarImpressao(porta);
                        impr.ImpLFormatacao(prazoProposta);
                            
                        if (gravaNoTxt)
                        {
                            System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                            arquivo.WriteLine(prazoProposta);
                            arquivo.Close();
                        }
                        impr.FinalizaImpressão();
                    }
                    
                    impr.IniciarImpressao(porta);
                    impr.ImpLFormatacao("");

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("");
                        arquivo.Close();
                    }

                    impr.FinalizaImpressão();

                    impr.IniciarImpressao(porta);
                    impr.ImpLFormatacao("Sistemas FuturaData - www.futuradata.com.br");                    
                    //impr.FinalizaImpressão();

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("Sistemas FuturaData - www.futuradata.com.br");
                        arquivo.Close();
                    }

                    //impr.IniciarImpressao(porta);
                    //impr.ImpLFormatacao("");
                    impr.ImpLFormatacao("*Sem Valor Fiscal - Apenas Contr.Interno*");

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("*Sem Valor Fiscal - Apenas Contr.Interno*");
                        arquivo.Close();
                    }

                    impr.FinalizaImpressão();

                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("");
                        arquivo.Close();
                    }
                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("");
                        arquivo.Close();
                    }
                    if (gravaNoTxt)
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(nomeArquivoOutPut);
                        arquivo.WriteLine("");
                        arquivo.Close();
                    }


                    if (ultimaVia)
                    {
                        int contadorLinhasPuladas = 0;
                        while (contadorLinhasPuladas < qtdLinhas_PularFinal)
                        {
                            impr.IniciarImpressao(porta);
                            impr.ImpLFormatacao("");
                            impr.FinalizaImpressão();
                            contadorLinhasPuladas++;
                        }
                    }
                    else
                    {
                        int contadorLinhasPuladas = 0;
                        while (contadorLinhasPuladas < qtdLinhas_PularEntreCotacoes)
                        {
                            impr.IniciarImpressao(porta);
                            impr.ImpLFormatacao("");
                            impr.FinalizaImpressão();
                            contadorLinhasPuladas++;
                        }
                    }

                    impr.IniciarImpressao(porta);
                    impr.ImpLFormatacao("");                    
                    impr.FinalizaImpressão();
                    #endregion

                    return true;
                    #endregion
                }//fim termica

                if (modeloImpressora.ToUpper() == "TÉRMICA" && usarDriverFabricante) //aqui significa que a impressora é térmica, e será usado o driver do Fabricante!
                {
                    #region Daruma Baby!
                    if (marcaImpressora.ToUpper() == "DARUMA")
                    {                       
                        string bufferImpressao = "";

                        #region Dados da Empresa que Usam o Sistema
                        string nomeEmpresa = dt_DadosEmpresa.Rows[0]["NOME"].ToString().Trim();
                        string cnpj = dt_DadosEmpresa.Rows[0]["CNPJ"].ToString().Trim();
                        string rua = dt_DadosEmpresa.Rows[0]["RUA"].ToString().Trim();
                        string numero = dt_DadosEmpresa.Rows[0]["NUMERO"].ToString().Trim();
                        string bairro = dt_DadosEmpresa.Rows[0]["BAIRRO"].ToString().Trim();
                        string cidade = dt_DadosEmpresa.Rows[0]["CIDADE"].ToString().Trim();
                        string estado = dt_DadosEmpresa.Rows[0]["ESTADO"].ToString().Trim();
                        string telefone1 = dt_DadosEmpresa.Rows[0]["TELEFONE1"].ToString().Trim();
                        string telefone2 = dt_DadosEmpresa.Rows[0]["TELEFONE2"].ToString().Trim();
                        string fax = dt_DadosEmpresa.Rows[0]["FAX"].ToString().Trim();
                        string site = dt_DadosEmpresa.Rows[0]["SITE"].ToString().Trim();

                        string observacaoImprimir = dt_ItensOrcamento.Rows[0]["INFOADICIONAL"].ToString().Trim();
                        string retiradoPor = dt_ItensOrcamento.Rows[0]["FUNCIONARIO_RETIRA"].ToString().Trim();

                        string tipoFrete = "Frete: " + dt_ItensOrcamento.Rows[0]["TIPOFRETE"].ToString().Trim();
                        string prazoEntrega = "Prazo Entrega: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["PRAZOENTREGA"]).ToString("dd/MM/yyyy");
                        string prazoGarantia = "Garantia Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["GARANTIA"]).ToString("dd/MM/yyyy");
                        string prazoProposta = "Proposta Valida Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["VALIDADEPROPOSTA"]).ToString("dd/MM/yyyy");

                        string cliente = "Cliente: " + dt_ItensOrcamento.Rows[0]["NOME_CLIENTE"].ToString();

                        string valorTotalCupom = dt_ItensOrcamento.Rows[0]["VALORFINAL"].ToString().Trim();

                        if (cliente.Length > 40)
                        {
                            cliente = cliente.Substring(0, 40);
                        }

                        string vendedor = "Vendedor: " + dt_ItensOrcamento.Rows[0]["NOME_VENDEDOR"].ToString();
                        if (vendedor.Length > 40)
                        {
                            vendedor = vendedor.Substring(0, 40);
                        }
                        #endregion

                        #region Emissao cabecário do Cupom

                        bufferImpressao = bufferImpressao + "<b>" + nomeEmpresa + "</b> - " + cnpj + "<l></l>";

                        //bufferImpressao = bufferImpressao + "");
                        if (rua.Length > 36)
                        {
                            rua = rua.Substring(0, 36);
                        }

                        bufferImpressao = bufferImpressao + rua + ", " + numero + " - " + bairro + "<l></l>";
                        bufferImpressao = bufferImpressao + cidade + " - " + estado + " - " + telefone1 + "<l></l>";

                        if (layoutCupom == "Layout 1")
                        {
                            bufferImpressao = bufferImpressao + "*****************************************" + "<l></l>";
                        }
                        else
                        {
                            bufferImpressao = bufferImpressao + "-----------------------------------------" + "<l></l>";
                        }

                        bufferImpressao = bufferImpressao + vendedor + "<l></l>";
                        bufferImpressao = bufferImpressao + "Orcamento <ce>" + numeroOrcamento.ToString() + "</ce> - " + DateTime.Now.ToString() + "<l></l>";

                        if (!cliente.ToUpper().Contains("VENDA") && !cliente.ToUpper().Contains("CONSUMIDOR"))
                        {
                            bufferImpressao = bufferImpressao + cliente + "<l></l>";
                        }

                        if (layoutCupom == "Layout 1")
                        {
                            bufferImpressao = bufferImpressao + "*****************************************" + "<l></l>)";
                        }
                        else
                        {
                            bufferImpressao = bufferImpressao + "----------------------------------------" + "<l></l>)";
                        }
                        #endregion

                        #region Emissao Itens do Cupom                                        
                        decimal valorTotalDesconto = 0;
                        decimal valorTotalAcrescimo = 0;
                        for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                        {
                            //string codigoProduto = dt_ItensOrcamento.Rows[i]["CODIGO_PRODUTO"].ToString().Trim();
                            string id_ProdutoVenda = dt_ItensOrcamento.Rows[i]["PK_ID"].ToString().Trim();
                            string descricao = dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Trim();
                            string codigoFabric = dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString().Trim();
                            string prateleira = dt_ItensOrcamento.Rows[i]["LOCALESTOQUE"].ToString().Trim();

                            string qtde = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString().Trim());
                            string precoUnitBruto = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString().Trim());
                            string precoUnitLiquido = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString().Trim());
                            string precoFinal = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString().Trim());
                            string descItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                            string acreItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());

                            bool ignorarMudancaPreco = Convert.ToBoolean(dt_ItensOrcamento.Rows[i]["IGNORAR_MUDANCA_PRECOS"].ToString());

                            string codigoFabrOrig = "(" + id_ProdutoVenda + ") CodFabr: " + codigoFabric;

                            if (descricao.Length > 42)
                            {
                                descricao = descricao.Substring(0, 42);
                            }

                            if (codigoFabrOrig.Length > 42)
                            {
                                codigoFabrOrig = codigoFabrOrig.Substring(0, 42);
                            }

                            bufferImpressao = bufferImpressao + codigoFabrOrig + "<l></l>";
                            bufferImpressao = bufferImpressao + descricao + "<l></l>";


                            //Se o Preço Unitário Bruto e o Preço Unitário Liquido forem diferentes, é sinal que houve desconto. Ai se estiver "ignorarMudancadePreco" eu vou imprimir o líquido e já era...
                            //caso contrário eu vou imprimir o bruto e o desconto logo abaixo... Caso contrário (situação normal) irei imprimir o valor do produto líquido mesmo, valor normal

                            if (precoUnitBruto != precoUnitLiquido)
                            {
                                if (ignorarMudancaPreco)
                                {
                                    bufferImpressao = bufferImpressao + "R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal + "<l></l>";
                                }
                                else
                                {
                                    if (Convert.ToDecimal(precoUnitLiquido) < Convert.ToDecimal(precoUnitBruto))
                                    {
                                        bufferImpressao = bufferImpressao + "R$" + precoUnitBruto + " x " + qtde + " -" + descItem + "(desconto) = R$" + precoFinal + "<l></l>";
                                        valorTotalDesconto = valorTotalDesconto + Convert.ToDecimal(descItem);
                                    }
                                    else
                                    {
                                        bufferImpressao = bufferImpressao + "R$" + precoUnitBruto + " x " + qtde + " +" + acreItem + "(acrescimo) = R$" + precoFinal + "<l></l>";
                                        valorTotalAcrescimo = valorTotalAcrescimo + Convert.ToDecimal(acreItem);
                                    }
                                }
                            }
                            else
                            {
                                bufferImpressao = bufferImpressao + "R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal + "<l></l>";
                            }

                            if (!prateleira.Contains("Padrão"))
                            {
                                bufferImpressao = bufferImpressao + prateleira + "<l></l>";
                            }

                            //Thread.Sleep(100);
                            //impr.FinalizaImpressão();
                            //codigoProduto = null;
                            descricao = null;
                            qtde = null;
                            precoFinal = null;
                            codigoFabric = null;
                            //codigoOrig1 = null;
                            descItem = null;
                            prateleira = null;
                            if (layoutCupom == "Layout 1")
                            {
                                bufferImpressao = bufferImpressao + "*****************************************" + "<l></l>";
                            }
                            else
                            {
                                bufferImpressao = bufferImpressao + "-----------------------------------------" + "<l></l>";
                            }                          
                        }
                        
                        if (valorTotalDesconto > 0)
                        {
                            decimal valorBruto = Convert.ToDecimal(valorTotalCupom) + valorTotalDesconto;
                            bufferImpressao = bufferImpressao + " Valor Bruto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorBruto.ToString()) + "</b><l></l>";
                            bufferImpressao = bufferImpressao + " Valor total do Desconto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalDesconto.ToString()) + "</b><l></l>";
                        }
                        if (valorTotalAcrescimo > 0)
                        {
                            decimal valorBruto = Convert.ToDecimal(valorTotalCupom) - valorTotalAcrescimo;
                            bufferImpressao = bufferImpressao + " Valor Bruto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorBruto.ToString()) + "</b><l></l>";
                            bufferImpressao = bufferImpressao + " Valor total do Acrescimo: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalAcrescimo.ToString()) + "</b><l></l>";
                        }

                        bufferImpressao = bufferImpressao + " Valor Final: R$ <b><e>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalCupom.ToString()) + "</b></e><l></l>";
                        #endregion

                        #region Final da Impressão
                        if (observacaoImprimir != "")
                        {
                            bufferImpressao = bufferImpressao + "OBSERVACAO: " + observacaoImprimir + "<l></l>";
                        }

                        if (retiradoPor != "")
                        {
                            bufferImpressao = bufferImpressao + "Retirado Por: " + retiradoPor + "<l></l>";
                        }

                        if (imprimirTipoFrete)
                        {
                            bufferImpressao = bufferImpressao + tipoFrete + "<l></l>";
                        }

                        if (imprimirPrazoEntrega)
                        {
                            bufferImpressao = bufferImpressao + prazoEntrega + "<l></l>";
                        }

                        if (imprimirGarantia)
                        {
                            bufferImpressao = bufferImpressao + prazoGarantia + "<l></l>";
                        }

                        if (imprimirValidadeProposta)
                        {
                            bufferImpressao = bufferImpressao + prazoProposta + "<l></l>";
                        }

                        bufferImpressao = bufferImpressao + "" + "<l></l>";

                        bufferImpressao = bufferImpressao + "Sistemas FuturaData: www.futuradata.com.br" + "<l></l>";
                        
                        bufferImpressao = bufferImpressao + "*Sem Valor Fiscal - Apenas Contr.Interno*" + "<l></l>";

                        if (ultimaVia)
                        {
                            int contadorLinhasPuladas = 0;
                            while (contadorLinhasPuladas < qtdLinhas_PularFinal)
                            {
                                bufferImpressao = bufferImpressao + "" + "<l></l>";
                                contadorLinhasPuladas++;
                            }
                        }
                        else
                        {
                            int contadorLinhasPuladas = 0;
                            while (contadorLinhasPuladas < qtdLinhas_PularEntreCotacoes)
                            {
                                bufferImpressao = bufferImpressao + "" + "<l></l>";
                                contadorLinhasPuladas++;
                            }
                        }

                        DarumaNaoFiscal.iRetorno = DarumaNaoFiscal.iImprimirTexto_DUAL_DarumaFramework(bufferImpressao, 0);
                        //Thread.Sleep(100);
                        if (qtdLinhas_PularFinal > 0)
                        {
                            DarumaNaoFiscal.iRetorno = DarumaNaoFiscal.iImprimirTexto_DUAL_DarumaFramework("<sl>" + qtdLinhas_PularFinal.ToString() + "</sl>", 0);
                        }
                        if (usarGilhotina)
                        {
                            DarumaNaoFiscal.iRetorno = DarumaNaoFiscal.iImprimirTexto_DUAL_DarumaFramework("<gui></gui>", 0);
                        }
                        return true;
                        #endregion
                    }//fim daruma
                    #endregion

                    #region Epson YeahYeah MayMay HabibHabib
                    if (marcaImpressora.ToUpper() == "EPSON")
                    {
                        string bufferImpressao = "";
                        abrePortaEpson();
                        #region Dados da Empresa que Usam o Sistema
                        string nomeEmpresa = dt_DadosEmpresa.Rows[0]["NOME"].ToString().Trim();
                        string cnpj = dt_DadosEmpresa.Rows[0]["CNPJ"].ToString().Trim();
                        string rua = dt_DadosEmpresa.Rows[0]["RUA"].ToString().Trim();
                        string numero = dt_DadosEmpresa.Rows[0]["NUMERO"].ToString().Trim();
                        string bairro = dt_DadosEmpresa.Rows[0]["BAIRRO"].ToString().Trim();
                        string cidade = dt_DadosEmpresa.Rows[0]["CIDADE"].ToString().Trim();
                        string estado = dt_DadosEmpresa.Rows[0]["ESTADO"].ToString().Trim();
                        string telefone1 = dt_DadosEmpresa.Rows[0]["TELEFONE1"].ToString().Trim();
                        string telefone2 = dt_DadosEmpresa.Rows[0]["TELEFONE2"].ToString().Trim();
                        string fax = dt_DadosEmpresa.Rows[0]["FAX"].ToString().Trim();
                        string site = dt_DadosEmpresa.Rows[0]["SITE"].ToString().Trim();

                        string observacaoImprimir = dt_ItensOrcamento.Rows[0]["INFOADICIONAL"].ToString().Trim();
                        string retiradoPor = dt_ItensOrcamento.Rows[0]["FUNCIONARIO_RETIRA"].ToString().Trim();

                        string tipoFrete = "Frete: " + dt_ItensOrcamento.Rows[0]["TIPOFRETE"].ToString().Trim();
                        string prazoEntrega = "Prazo Entrega: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["PRAZOENTREGA"]).ToString("dd/MM/yyyy");
                        string prazoGarantia = "Garantia Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["GARANTIA"]).ToString("dd/MM/yyyy");
                        string prazoProposta = "Proposta Valida Ate: " + Convert.ToDateTime(dt_ItensOrcamento.Rows[0]["VALIDADEPROPOSTA"]).ToString("dd/MM/yyyy");

                        string cliente = "Cliente: " + dt_ItensOrcamento.Rows[0]["NOME_CLIENTE"].ToString();

                        string valorTotalCupom = dt_ItensOrcamento.Rows[0]["VALORFINAL"].ToString().Trim();

                        if (cliente.Length > 40)
                        {
                            cliente = cliente.Substring(0, 40);
                        }

                        string vendedor = "Vendedor: " + dt_ItensOrcamento.Rows[0]["NOME_VENDEDOR"].ToString();
                        if (vendedor.Length > 40)
                        {
                            vendedor = vendedor.Substring(0, 40);
                        }
                        #endregion

                        #region Emissao cabecário do Cupom
                        iRetorno = EpsonNaoFiscal.ImprimeTextoTag("\n");
                        iRetorno = EpsonNaoFiscal.ImprimeTextoTag("\n");
                        iRetorno = EpsonNaoFiscal.ImprimeTextoTag("\n");
                        
                        bufferImpressao = bufferImpressao +"<b>" + nomeEmpresa + " - " + cnpj + "</b> \n";

                        //bufferImpressao = bufferImpressao + "");
                        if (rua.Length > 36)
                        {
                            rua = rua.Substring(0, 36);
                        }

                        bufferImpressao = bufferImpressao + rua + ", " + numero + " - " + bairro + "\n";
                        bufferImpressao = bufferImpressao + cidade + " - " + estado + " - " + telefone1 + "\n";

                        if (layoutCupom == "Layout 1")
                        {
                            bufferImpressao = bufferImpressao + "*****************************************" + "\n";
                        }
                        else
                        {
                            bufferImpressao = bufferImpressao + "-----------------------------------------" + "\n";
                        }

                        bufferImpressao = bufferImpressao + vendedor + "\n";
                        bufferImpressao = bufferImpressao + "Orcamento " + numeroOrcamento.ToString() + " - " + DateTime.Now.ToString() + "\n";

                        if (!cliente.ToUpper().Contains("VENDA") && !cliente.ToUpper().Contains("CONSUMIDOR"))
                        {
                            bufferImpressao = bufferImpressao + cliente + "\n";
                        }

                        if (layoutCupom == "Layout 1")
                        {
                            bufferImpressao = bufferImpressao + "*****************************************" + "\n)";
                        }
                        else
                        {
                            bufferImpressao = bufferImpressao + "----------------------------------------" + "\n)";
                        }
                        #endregion

                        #region Emissao Itens do Cupom
                        decimal valorTotalDesconto = 0;
                        decimal valorTotalAcrescimo = 0;
                        for (int i = 0; i < dt_ItensOrcamento.Rows.Count; i++)
                        {
                            //string codigoProduto = dt_ItensOrcamento.Rows[i]["CODIGO_PRODUTO"].ToString().Trim();
                            string id_ProdutoVenda = dt_ItensOrcamento.Rows[i]["PK_ID"].ToString().Trim();
                            string descricao = dt_ItensOrcamento.Rows[i]["DESCRICAOAPLICACAO"].ToString().Trim();
                            string codigoFabric = dt_ItensOrcamento.Rows[i]["CODIGOFABRIC"].ToString().Trim();
                            string prateleira = dt_ItensOrcamento.Rows[i]["LOCALESTOQUE"].ToString().Trim();

                            string qtde = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["QUANTIDADE"].ToString().Trim());
                            string precoUnitBruto = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["PRECOVENDABANCO"].ToString().Trim());
                            string precoUnitLiquido = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORUNITARIO"].ToString().Trim());
                            string precoFinal = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["VALORTOTAL"].ToString().Trim());
                            string descItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["DESCONTO"].ToString());
                            string acreItem = contas.newValidaAjustaArredonda2CasasDecimais(dt_ItensOrcamento.Rows[i]["ACRESCIMO"].ToString());

                            bool ignorarMudancaPreco = Convert.ToBoolean(dt_ItensOrcamento.Rows[i]["IGNORAR_MUDANCA_PRECOS"].ToString());

                            string codigoFabrOrig = "(" + id_ProdutoVenda + ") CodFabr: " + codigoFabric;

                            if (descricao.Length > 42)
                            {
                                descricao = descricao.Substring(0, 42);
                            }

                            if (codigoFabrOrig.Length > 42)
                            {
                                codigoFabrOrig = codigoFabrOrig.Substring(0, 42);
                            }

                            bufferImpressao = bufferImpressao + codigoFabrOrig + "\n";
                            bufferImpressao = bufferImpressao + descricao + "\n";


                            //Se o Preço Unitário Bruto e o Preço Unitário Liquido forem diferentes, é sinal que houve desconto. Ai se estiver "ignorarMudancadePreco" eu vou imprimir o líquido e já era...
                            //caso contrário eu vou imprimir o bruto e o desconto logo abaixo... Caso contrário (situação normal) irei imprimir o valor do produto líquido mesmo, valor normal

                            if (precoUnitBruto != precoUnitLiquido)
                            {
                                if (ignorarMudancaPreco)
                                {
                                    bufferImpressao = bufferImpressao + "R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal + "\n";
                                }
                                else
                                {
                                    if (Convert.ToDecimal(precoUnitLiquido) < Convert.ToDecimal(precoUnitBruto))
                                    {
                                        bufferImpressao = bufferImpressao + "R$" + precoUnitBruto + " x " + qtde + " -" + descItem + "(desconto) = R$" + precoFinal + "\n";
                                        valorTotalDesconto = valorTotalDesconto + Convert.ToDecimal(descItem);
                                    }
                                    else
                                    {
                                        bufferImpressao = bufferImpressao + "R$" + precoUnitBruto + " x " + qtde + " +" + acreItem + "(acrescimo) = R$" + precoFinal + "\n";
                                        valorTotalAcrescimo = valorTotalAcrescimo + Convert.ToDecimal(acreItem);
                                    }
                                }
                            }
                            else
                            {
                                bufferImpressao = bufferImpressao + "R$ " + precoUnitLiquido + " x " + qtde + " = R$ " + precoFinal + "\n";
                            }

                            if (!prateleira.Contains("Padrão"))
                            {
                                bufferImpressao = bufferImpressao + prateleira + "<l></l>";
                            }

                            //Thread.Sleep(100);
                            //impr.FinalizaImpressão();
                            //codigoProduto = null;
                            descricao = null;
                            qtde = null;
                            precoFinal = null;
                            codigoFabric = null;
                            //codigoOrig1 = null;
                            descItem = null;
                            prateleira = null;
                            if (layoutCupom == "Layout 1")
                            {
                                bufferImpressao = bufferImpressao + "*****************************************" + "\n";
                            }
                            else
                            {
                                bufferImpressao = bufferImpressao + "-----------------------------------------" + "\n";
                            }                          
                        }
                        
                        if (valorTotalDesconto > 0)
                        {
                            decimal valorBruto = Convert.ToDecimal(valorTotalCupom) + valorTotalDesconto;
                            bufferImpressao = bufferImpressao + " Valor Bruto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorBruto.ToString()) + "</b><l></l>";
                            bufferImpressao = bufferImpressao + " Valor total do Desconto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalDesconto.ToString()) + "</b><l></l>";
                        }
                        if (valorTotalAcrescimo > 0)
                        {
                            decimal valorBruto = Convert.ToDecimal(valorTotalCupom) - valorTotalAcrescimo;
                            bufferImpressao = bufferImpressao + " Valor Bruto: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorBruto.ToString()) + "</b><l></l>";
                            bufferImpressao = bufferImpressao + " Valor total do Acrescimo: R$ <b>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalAcrescimo.ToString()) + "</b><l></l>";
                        }

                        bufferImpressao = bufferImpressao + " Valor Final: R$ <b><e>" + contas.newValidaAjustaArredonda2CasasDecimais(valorTotalCupom.ToString()) + "</b></e><l></l>";
                        #endregion

                        #region Final da Impressão
                        if (observacaoImprimir != "")
                        {
                            bufferImpressao = bufferImpressao + "OBSERVACAO: " + observacaoImprimir + "\n";
                        }

                        if (retiradoPor != "")
                        {
                            bufferImpressao = bufferImpressao + "Retirado Por: " + retiradoPor + "\n";
                        }

                        if (imprimirTipoFrete)
                        {
                            bufferImpressao = bufferImpressao + tipoFrete + "\n";
                        }

                        if (imprimirPrazoEntrega)
                        {
                            bufferImpressao = bufferImpressao + prazoEntrega + "\n";
                        }

                        if (imprimirGarantia)
                        {
                            bufferImpressao = bufferImpressao + prazoGarantia + "\n";
                        }

                        if (imprimirValidadeProposta)
                        {
                            bufferImpressao = bufferImpressao + prazoProposta + "\n";
                        }

                        bufferImpressao = bufferImpressao + "" + "\n";

                        bufferImpressao = bufferImpressao + "Sistemas FuturaData: www.futuradata.com.br" + "\n";

                        bufferImpressao = bufferImpressao + "*Sem Valor Fiscal - Apenas Contr.Interno*" + "\n";

                        if (ultimaVia)
                        {
                            int contadorLinhasPuladas = 0;
                            while (contadorLinhasPuladas < qtdLinhas_PularFinal)
                            {
                                bufferImpressao = bufferImpressao + "" + "\n";
                                contadorLinhasPuladas++;
                            }
                        }
                        else
                        {
                            int contadorLinhasPuladas = 0;
                            while (contadorLinhasPuladas < qtdLinhas_PularEntreCotacoes)
                            {
                                bufferImpressao = bufferImpressao + "" + "\n";
                                contadorLinhasPuladas++;
                            }
                        }

                        iRetorno = EpsonNaoFiscal.ImprimeTextoTag(bufferImpressao);
                        Thread.Sleep(100);
                        if (qtdLinhas_PularFinal > 0)
                        {
                            for (int i = 0; i <= qtdLinhas_PularFinal; i++)
                            {
                                i++;
                                iRetorno = EpsonNaoFiscal.ImprimeTextoTag("\n");
                            }                            
                        }
                        if (usarGilhotina)
                        {
                            iRetorno = EpsonNaoFiscal.AcionaGuilhotina(0);
                            //iRetorno = clsEpsonNaoFiscal.ImprimeTextoTag("<gui></gui>");
                        }

                        fechaPortaEpson();
                        return true;
                        #endregion                        
                    }
                    #endregion
                }//fim termica
                return false;
            }//fim try

            //retorna false caso dê erro
            catch(Exception erro)
            {
                MessageBox.Show("Erro gerado na Classe de Impressão Modo Texto: " + erro.Message.ToString());
                return false;
            }        
        }
        #endregion        

        #region Finaliza Impressao (Pula linhas)
        public void FinalizaImpressao()
        {
            string porta = retornaPortaImpressora();
            if (porta == "")
            {
                porta = "LPT1";
            }
            ComunicacaoImprTexto impr = new ComunicacaoImprTexto();
            impr.IniciarImpressao(porta);
            impr.PulaLinha(10);
        }
        #endregion
        
        #region Finaliza Impressao (Pula linhas)
        public void FinalizaImpressao(string porta, int numeroLinhasPular)
        {            
            if (porta == "")
            {
                porta = "LPT1";
            }
            ComunicacaoImprTexto impr = new ComunicacaoImprTexto();
            int linhasPuladas = 0;
            while(linhasPuladas < numeroLinhasPular)
            {
                impr.IniciarImpressao(porta);
                impr.ImpLFormatacao("");
                impr.FinalizaImpressão();
                linhasPuladas++;
            }
        }
        #endregion

        #region Retorna Porta da Impressora no InfoSiga
        /// <summary>
        /// Lê o Txt de Config do InfoSiga na pasta e retorna a porta da impressora
        /// </summary>
        /// <returns></returns>
        public string retornaPortaImpressora()
        {
            
            if (File.Exists("C:\\FUTURADATA\\BUSINESS\\CONFIGPDV.TXT") == false)
            {               
                return "LPT1";
            }
            else
            {
                StreamReader objReader = new StreamReader("C:\\FUTURADATA\\BUSINESS\\CONFIGPDV.TXT");
                string sLine = "";
                ArrayList arrText = new ArrayList();
                try
                {
                    objReader.Read();

                    sLine = objReader.ReadLine();

                    objReader.Close();

                    return sLine.Replace("ORTA=", "");
                }
                catch
                {
                    return "LPT1";
                }
            }
        }
        #endregion
    }
    #endregion
}//fim namespace
