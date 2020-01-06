using System;
using System.Runtime.InteropServices;

namespace CSOBRF_Fiscal
{
    public class InterfaceEpson
    {
        //=================================================================================================;
        // EPSON_Serial
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Abrir_Porta(int dwVelocidade, int wPorta);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Abrir_Fechar_Porta_CMD(int dwVelocidade, int wPorta);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Abrir_PortaAD([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszVelocidade, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszPorta);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Abrir_PortaEX();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Fechar_Porta();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Obter_Estado_Com();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Serial_Config_Simplificada(int dwTipo);
        //=================================================================================================;
        // EPSON_Fiscal
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Abrir_Cupom(string pszCNPJ, string pszRazaoSocial, string pszEndereco1, string pszEndereco2, int dwPosicao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Vender_Item(string pszCodigo, string pszDescricao, string pszQuantidade, int dwQuantCasasDecimais, string pszUnidade, string pszPrecoUnidade, int dwPrecoCasasDecimais, string pszAliquotas, int dwLinhas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Vender_Item_AD(string pszCodigo, string pszDescricao, string pszQuantidade, int dwQuantCasasDecimais, string pszUnidade, string pszPrecoUnidade, int dwPrecoCasasDecimais, string pszAliquotas, int dwLinhas, int dwRoundTruncate, int dwOwnManufactured);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Obter_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszSubTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Pagamento(string pszNumeroPagamento, string pszValorPagamento, int dwCasasDecimais, string pszDescricao1, string pszDescricao2);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Desconto_Acrescimo_Item(string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Desconto_Acrescimo_ItemEX(string pszNumeroItem, string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Desconto_Acrescimo_Subtotal(string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Cupom();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_CupomEX();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Item(string pszNumeroItem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Ultimo_Item();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Desconto_Acrescimo_Item(bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Desconto_Acrescimo_ItemEX(string pszNumeroItem, bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Acrescimo_Desconto_Subtotal(bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Cancelar_Item_Parcial(string pszNumeroItem, string pszQuantidade, int dwQuantCasasDecimais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Imprimir_Mensagem(string pszLinhaTexto1, string pszLinhaTexto2, string pszLinhaTexto3, string pszLinhaTexto4, string pszLinhaTexto5, string pszLinhaTexto6, string pszLinhaTexto7, string pszLinhaTexto8);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Imprimir_MensagemEX(string pszTextLine);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Configurar_Codigo_Barras_Mensagem(int dwLinha, int dwTipo, int dwAltura, int dwLargura, int dwPosicao, int dwCaracter);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Fechar_CupomEX([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalCupom);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Fechar_Cupom(bool bCortarPapel, bool bAdicional);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Fiscal_Dados_Consumidor(string pszCNPJ, string pszRazaoSocial, string pszEndereco1, string pszEndereco2, int dwPosicao);
        //=================================================================================================;
        // EPSON_NaoFiscal
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Abrir_Comprovante(string pszCNPJ, string pszRazaoSocial, string pszEndereco1, string pszEndereco2, int dwPosicao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Vender_Item(string pszNumeroOperacao, string pszValorOperacao, int dwCasasDecimais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Desconto_Acrescimo_Item(string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Desconto_Acrescimo_ItemEX(string pszNumeroItem, string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Desconto_Acrescimo_Subtotal(string pszValor, int dwCasasDecimais, bool bDesconto, bool bPercentagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Pagamento(string pszNumeroPagamento, string pszValorPagamento, int dwCasasDecimais, string pszDescricao1, string pszDescricao2);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Item(string pszItem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Ultimo_Item();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Desconto_Acrescimo_Item(bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Desconto_Acrescimo_ItemEX(string pszNumeroItem, bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Desconto_Acrescimo_Subtotal(bool bDesconto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Comprovante();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_ComprovanteEX();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Fechar_Comprovante(bool bCortarPapel);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Abrir_CCD(string pszNumeroPagamento, string pszValor, int dwCasasDecimais, string pszParcelas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Abrir_Relatorio_Gerencial(string pszNumeroRelatorio);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Imprimir_LinhaEX(string pszLinha);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Imprimir_Linha(string pszLinha);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Imprimir_15Linhas(string pszLinha01, string pszLinha02, string pszLinha03, string pszLinha04, string pszLinha05, string pszLinha06, string pszLinha07, string pszLinha08, string pszLinha09, string pszLinha10, string pszLinha11, string pszLinha12, string pszLinha13, string pszLinha14, string pszLinha15);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Fechar_CCD(bool bCortarPapel);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Fechar_Relatorio_Gerencial(bool bCortarPapel);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_CCD(string pszNumeroPagamento, string pszValor, int dwCasasDecimais, string pszParcelas, string pszNumeroCupom);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Cancelar_Pagamento(string pszFormaPagamento, string pszNovaFormaPagamento, string pszValor, int dwCasasDecimais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Nova_Parcela_CCD();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Nova_Via_CCD();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Reimprimir_CCD();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Sangria(string pszValor, int dwCasasDecimais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Fundo_Troco(string pszValor, int dwCasasDecimais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Imprimir_Codigo_Barras(int dwTipo, int dwAltura, int dwLargura, int dwPosicao, int dwCaracter, string pszCodigo);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_NaoFiscal_Obter_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszSubTotal);
        //=================================================================================================;
        // EPSON_RelatorioFiscal
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_LeituraX();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_RZ(string pszData, string pszHora, int dwHorarioVerao, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszCRZ);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_RZEX(int dwHorarioVerao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_Leitura_MF(string pszInicio, string pszFim, int dwTipoImpressao, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszBuffer, string pszArquivo, int pdwTamanhoBuffer, int dwTamBuffer);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_Salvar_LeituraX(string pszArquivo);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_RelatorioFiscal_Abrir_Jornada();
        //=================================================================================================;
        // EPSON_Obter
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_Usuario([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDadosUsuario);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Tabela_Aliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaAliquotas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Tabela_Aliquotas_Cupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaAliquotas, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalICMS, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalISS);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Tabela_Pagamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaPagamentos);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Tabela_NaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaNaoFiscais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Tabela_Relatorios_Gerenciais([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaRelatoriosGerenciais);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Cancelado([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalCancelado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Aliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszAliquotasTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Bruto([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszVendaBruta);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Descontos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalDescontos);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Acrescimos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalAcrescimos);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Troco([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotalTroco);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Venda_Liquida_ICMS([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Venda_Liquida_ISSQN([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_ICMS([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_ISSQN([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTotal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_Impressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDadosImpressora);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Cliche_Usuario([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDadosUsuario);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Cliche_UsuarioEX([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDadosUsuario);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Data_Hora_Jornada([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDataHora);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Numero_ECF_Loja([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Hora_Relogio([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Simbolo_Moeda([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Casas_Decimais([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Desconto_Iss([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Contadores([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_Impressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_ImpressoraEX([MarshalAs(UnmanagedType.VBByRefStr)] ref string szEstadoImpressora, [MarshalAs(UnmanagedType.VBByRefStr)] ref string szEstadoFiscal, [MarshalAs(UnmanagedType.VBByRefStr)] ref string szRetornoComando, [MarshalAs(UnmanagedType.VBByRefStr)] ref string szMsgErro);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_GT([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Linhas_Impressas([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Linhas_Impressas_RG([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Linhas_Impressas_CCD([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_Jornada([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Caracteres_Linha([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Operador([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Numero_Ultimo_Item([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Informacao_Item(string pszNumeroItem, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDadosItem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_Cupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszEstado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Informacao_Ultimo_Documento([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszInfo);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_Corte_Papel(ref bool bHabilitado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Linhas_Impressas_Vendas([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszLinhasImpressas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Linhas_Impressas_Pagamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszLinhasImpressas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Itens_Vendidos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszItens);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_Memoria_Fiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszEstado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_MFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszEstado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_Leituras_X_Impressas([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszLeituras);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_Horario_Verao(ref bool bEstado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Venda_Bruta([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszBrutAmount, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszLastBrutAmount);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Mensagem_Erro(string pszCodigoErro, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszMensagemErro);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_MF_MFD(string pszInicio, string pszFinal, int dwTipoEntrada, int dwEspelhos, int dwAtoCotepe, int dwSintegra, string pszArquivoSaida);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_Arquivos_MF_MFD(string pszInicio, string pszFim, int dwTipoEntrada, int dwEspelhos, int dwAtoCOTEPE, int dwSintegra, string pszArquivoSaida, string pszArquivoMF, string pszArquivoMFD);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_MF_MFD_Progresso(string pszInicio, string pszFim, int dwTipoEntrada, int dwEspelhos, int dwAtoCOTEPE, int dwSintegra, string pszArquivoSaida, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszProgresso);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Versao_DLL([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszVersao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Total_JornadaEX(string pszOption, string pszZnumber, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszData);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Dados_Ultima_RZ([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszLastRZData);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_AtoCOTEPE_SeparadoEX(string pszFileName, int dwOption);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Arquivos_Binarios(string pszInicio, string pszFinal, int dwTipoEntrada, string pszArquivoMF, string pszArquivoMFD);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Arquivo_Binario_MF(string pszArquivo);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Arquivo_Binario_MFD(string pszFileName);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Versao_SWBasicoEX([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszVersion, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDate, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pzsTime);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Codigo_Nacional_ECF([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszCodigo, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszNomeArquivo);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Numero_Usuario([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszNumeroUsuario);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Arredonda_Trunca_Fabricacao([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszIsRound, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszIsOwn);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Log_Comandos([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTabelaLog, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszTamanho);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Estado_ReducaoZ_Automatica(ref bool bHabilitado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Obter_Valores_Cupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszDados);
        //=================================================================================================;
        // EPSON_Config
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Aliquota(string pszTaxa, bool bTipoISS);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Relatorio_Gerencial(string pszReportDescription);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Forma_Pagamento(bool bVinculado, string pszNumeroMeio, string pszDescricao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Forma_PagamentoEX(bool bVinculado, string pszDescricao);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Totalizador_NaoFiscal(string pszDescricao, ref int pdwNumeroTotalizador);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Horario_Verao();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Espaco_Entre_Documentos(string pszLinhas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Espaco_Entre_Linhas(string pszEspacos);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_Logotipo(bool bHabilita);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Logotipo(string pszDados, int dwTamDados, string pszLinha);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Operador(string pszDados);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_OperadorEX(string pszDados, int dwReport);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Corte_Papel(bool bHabilitado);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Serial_Impressora(string pszVelocidade);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Dados_Sintegra(string pszRazaoSocial, string pszLogradouro, string pszNumero, string pszComplemento, string pszBairro, string pszMunicipio, string pszCEP, string pszUF, string pszFax, string pszFone, string pszNomeContato);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Dados_SPED(string CFOP, string AliqPis, string AliqCofins, string CodObsLancFisc, string UF);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_CAT52_Auto(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_RJSSER16_Auto(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_Mensagem_Cupom_Mania(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_Mensagem_Minas_Legal(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_PAFECF_Auto(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Dados_PAFECF(string pszDeveloperCNPJ, string pszDeveloperIE, string pszDeveloperIM, string pszDeveloperName, string pszPAFName, string pszPAFVersion, string pszPAFMD5, string pszERVersion);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Mensagem_Aplicacao(string pszLinha01, string pszLinha02);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Mensagem_Aplicacao_Auto(string pszLinha01, string pszLinha02);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_Sintegra_Auto(int dwAction);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_Habilita_EAD(bool bHabilita);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Config_ReducaoZ_Automatica(bool bHabilita);
        //=================================================================================================;
        // EPSON_Cheque
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Configurar_Moeda(string pszSingular, string pszPlural);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Configurar_Parametros1(string pszNumeroRegistro, string pszValorX, string pszValorY, string pszDescricao1X, string pszDescricao1Y, string pszDescricao2X, string pszDescricao2Y);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Configurar_Parametros2(string pszNumeroRegistro, string pszParaX, string pszParaY, string pszCidadeX, string pszCidadeY, string pszOffsetDia, string pszOffsetMes, string pszOffsetAno, string pszAdicionalX, string pszAdicionalY);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Imprimir(string pszNumeroRegistro, string pszValor, int dwCasasDecimais, string pszPara, string pszCidade, string pszDados, string pszTexto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_ImprimirEX(string pszNumeroRegistro, string pszValor, int dwCasasDecimais, string pszPara, string pszCidade, string pszDados, string pszTexto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Preparar_Endosso();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Endosso_Estacao(string pszToX, string pszToY, int dwHorizontal);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Imprimir_Endosso(string pszLinhaTexto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Ejetar_Endosso();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Cancelar_Impressao();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Endosso_EstacaoEX(string pszToX, string pszToY, int dwHorizontal, string pszText);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Endosso_EstacaoCFG(string pszToX, string pszToY, int dwHorizontal, string pszText, bool bEject);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Cheque_Ler_MICR([MarshalAs(UnmanagedType.VBByRefStr)]ref string pszMICR);
        //=================================================================================================;
        // EPSON_Impressora
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Impressora_Abrir_Gaveta();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Impressora_Cortar_Papel();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Impressora_Avancar_Papel(int dwLines);
        //=================================================================================================;
        // EPSON_Autenticar
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Autenticar_Imprimir(string pszMensagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Autenticar_Reimprimir();
        //=================================================================================================;
        // EPSON_Display
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Display_Enviar_Texto(string pszTexto);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Display_Cursor(int dwAcao, int dwColuna, int dwLinha);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Display_Apagar_Texto(int dwLinha);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Display_Configurar(int dwBrilho, int dwLampejo, int dwRolagem);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Display_Inicializar(int dwAcao);
        //=================================================================================================;
        // EPSON_Sys
        //=================================================================================================;
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Sys_Informar_Handle_Janela(IntPtr hHandle);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Sys_Atualizar_Janela();
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Sys_Aguardar_Arquivo(string pszArquivo, int dwTimeout, bool bBloqueiaEntradas, bool bSincrono);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Sys_Bloquear_Entradas(bool bBloqueiaEntradas);
        [DllImport("InterfaceEpson.dll")]
        public static extern int EPSON_Sys_Log(string pszPath, int dwAction);
    }//
}//
