using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CSOBRF_Fiscal
{
    public class InterfaceSweda
    {
        #region Funções de Inicialização
        //**********************************************************//		
        [DllImport("Convecf.dll")]
        public static extern int ECF_AlteraSimboloMoeda(string SimbMoeda);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaAliquota(string Aliq, int ICMS_ISS);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NomeiaTotalizadorNaoSujeitoIcms(int Indice, string Totaliz);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaHorarioVerao();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaArredondamento();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaTruncamento();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ApagaTabelaNomesRelatoriosGerenciais();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ApagaTabelaAliquotas();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ApagaTabelaNomesFormasdePagamento();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ApagaTabelaNomesNaoFiscais();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaOperador(string oper);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RetornaIndiceComprovanteNaoFiscal(string indice);

        [DllImport("Convecf.dll")]
        public static extern int ECF_Registry_PathMFD(StringBuilder Caminho);

        [DllImport("Convecf.dll")]
        public static extern int ECF_Registry_Path(StringBuilder caminho);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RetornaPath(StringBuilder RetorCaminho);

        [DllImport("Convecf.dll")]
        public static extern int ECF_Registry_Default();

        #endregion
        
        #region Funções do Cupom Fiscal
        //******************************************************************/
        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreCupom(string CGC_CPF);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VendeItem(string Codigo, string Descricao, string Aliquota,
            string TipoQuantidade, string Quantidade, int CasasDecimais,
            string ValorUnitario, string TipoDesconto, string Desconto);


        [DllImport("Convecf.dll")]
        public static extern int ECF_VendeItemDepartamento(string Codigo, string Descricao,
            string Aliquota, string ValorUnitario, string Quantidade, string Acrescimo,
            string Desconto, string IndiceDepartamento, string UnidadeMedida);

        [DllImport("Convecf.dll")]
        public static extern int ECF_CancelaItemAnterior();

        [DllImport("Convecf.dll")]
        public static extern int ECF_CancelaItemGenerico(string NumeroItem);

        [DllImport("Convecf.dll")]
        public static extern int ECF_CancelaCupom();

        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaCupomResumido(string FormaPagamento, string Mensagem);


        [DllImport("Convecf.dll")]
        public static extern int ECF_EfetuaFormaPagamentoMFD(string FormaPag, string ValorFPag, string Parcelas, string NomeFPag);

        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaCupom(string FormaPagamento, string AcrescimoDesconto,
            string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);

        [DllImport("Convecf.dll")]
        public static extern int ECF_IniciaFechamentoCupom(string AcrescimoDesconto,
            string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);

        [DllImport("Convecf.dll")]
        public static extern int ECF_EfetuaFormaPagamentoDescricaoForma(string FormaPagamento,
             string ValorFormaPagamento, string Descricao);

        [DllImport("Convecf.dll")]
        public static extern int ECF_TerminaFechamentoCupom(string Mensagem);

        [DllImport("Convecf.dll")]
        public static extern int ECF_Sangria(string Valor);


        [DllImport("Convecf.dll")]
        public static extern int ECF_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor);

        [DllImport("Convecf.dll")]
        public static extern int ECF_AumentaDescricaoItem(string Descricao);

        [DllImport("Convecf.dll")]
        public static extern int ECF_UsaUnidadeMedida(string UnidadeMedida);

        [DllImport("CONVECF.DLL")]
        public static extern int ECF_RetornaCOO(StringBuilder Ret);

        [DllImport("CONVECF.DLL")]
        public static extern int ECF_ZAUTO();

        [DllImport("CONVECF.DLL")]
        public static extern int ECF_FechamentoDoDia();

        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaPortaSerial();

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbrePortaSerial();



        #endregion
        
        #region Retornos

        [DllImport("Convecf.dll")]
        public static extern int ECF_RetornoImpressora(ref int ACK, ref int ST1, ref int ST2);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RetornoImpressora(ref int ACK, ref int ST1, ref int ST2, ref int ST3);


        #endregion
        
        #region Funções Leitura ECF


        [DllImport("Convecf.dll")]
        public static extern int ECF_CapturaDocumentos(string TipoDownload, string ArquivoDestino, string COOInicial, string COOFinal, string Mostra);

        [DllImport("Convecf.dll")]
        public static extern int ECF_DownloadMF(string Arquivo);

        [DllImport("Convecf.dll")]
        public static extern int ECF_DownloadMFD(string Arquivo, string TipoDownload, string ParametroInicial, string ParametroFinal, string UsuarioECF);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaZPendente(StringBuilder zPendente);

        [DllImport("Convecf.dll")]
        public static extern int ECF_Suprimento(string Valor, string FormaPag);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ReducaoZ(string Data, string Hora);

        [DllImport("Convecf.dll")]
        public static extern int ECF_LeituraX();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ImprimeConfiguracoesImpressora();

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaImpressoraLigada();

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaModeloEcf();

        [DllImport("CONVECF.DLL")]
        public static extern int ECF_LeituraMemoriaFiscalData(string ZDataInicial, string ZDataFinal);

        [DllImport("Convecf.dll")]
        public static extern int ECF_LeituraMemoriaFiscalReducao(string ZCRZInicial, string ZCRZFinal);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaFormaPagamentoMFD(string FormaPagto, string OperacaoTef);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NomeiaRelatorioGerencialMFD(string Indice, string Descricao);

        [DllImport("Convecf.dll")]
        public static extern int ECF_CupomAdicionalMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_LeituraMemoriaFiscalSerialDataMFD(string DataIni, string DataFim, string Flag);

        [DllImport("Convecf.dll")]
        public static extern int ECF_LeituraMemoriaFiscalSerialData(string ZDataInicial, string ZDataFinal);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NumeroSerie(StringBuilder NumeroSerie);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NumeroSerieMFD(StringBuilder NumeroSerie);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VersaoFirmware(StringBuilder VersaoFirmware);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VersaoFirmwareMFD(StringBuilder VersaoFirmware);

        #endregion

        // Relatorio Sintegra

        #region Relatorio Sintegra

        [DllImport("Convecf.dll")]
        public static extern int ECF_RegistrosTipo60();

        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioTipo60Mestre();


        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioTipo60Analitico();

        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioTipo60AnaliticoMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioSintegraMFD(string Arquivo, string MesInic, string AnoInic, string Razao, string Endereco, string Numero, string Complemento,
                                                           string Bairro, string Cidade, string CEP, string Telefone, string Fax, string Contato);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ReproduzirMemoriaFiscalMFD(string Tipo, string FaixaIni, string FaixaFin, string PatTxt, string PatBin);

        [DllImport("Convecf.dll")]
        public static extern int ECF_DataHoraReducao(StringBuilder Data, StringBuilder Hora);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VersaoDll(StringBuilder Versao);

        #endregion
        
        #region Funções Gaveta

        [DllImport("Convecf.dll")]
        public static extern int ECF_AcionaGaveta();

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaEstadoGavetaStr(string EstadoGav);

        #endregion
        
        #region Retornos e Status do ECF

        [DllImport("Convecf.dll")]
        public static extern int ECF_ContadorCupomFiscalMFD(StringBuilder CuponsEmitidos);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NumeroCuponsCancelados(StringBuilder NumeroCancelamentos);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NumeroOperacoesNaoFiscais(StringBuilder NumeroOperacoes);

        [DllImport("Convecf.dll")]
        public static extern int ECF_NumeroIntervencoes(StringBuilder NumeroIntervencoes);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ContadoresTotalizadoresNaoFiscais(StringBuilder Contadores);

        [DllImport("Convecf.dll")]
        public static extern int ECF_GrandeTotal(StringBuilder GrandeTotal);

        [DllImport("Convecf.dll")]
        public static extern int ECF_MinutosLigada(StringBuilder Minutos);

        [DllImport("Convecf.dll")]
        public static extern int ECF_DataMovimento(StringBuilder Data);

        [DllImport("Convecf.dll")]
        public static extern int ECF_DataMovimentoUltimaReducaoMFD(StringBuilder DataMovimentoUltimaRZ);


        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaEstadoGaveta(out int EstadoGav);

        [DllImport("Convecf.dll")]
        public static extern int ECF_MapaResumoMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_MapaResumo();

        [DllImport("Convecf.dll")]

        public static extern int ECF_RetornaTipoEcf(StringBuilder Tipo);

        //Status 

        [DllImport("Convecf.dll")]
        public static extern int ECF_StatusRelatorioGerencial(StringBuilder Status);

        [DllImport("Convecf.dll")]
        public static extern int ECF_StatusEstendidoMFD(System.Int32 iStatus);

        [DllImport("Convecf.dll")]
        public static extern int ECF_StatusCupomFiscal(StringBuilder Status);

        [DllImport("Convecf.dll")]
        public static extern int ECF_StatusComprovanteNaoFiscalNaoVinculado(StringBuilder Status);

        #endregion
        
        #region Funções TEF
        [DllImport("Convecf.dll")]
        public static extern int ECF_IniciaModoTEF();

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreComprovanteNaoFiscalVinculado(string FormaPag, string Valor, string NumeroCupom);

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreComprovanteNaoFiscalVinculadoMFD(string FormaPag, string Valor,
                                           string NumeroCupom, string CNPJ, string Nome, string Endereco);
        [DllImport("Convecf.dll")]
        public static extern int ECF_UsaComprovanteNaoFiscalVinculadoTEF(string Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_UsaComprovanteNaoFiscalVinculado(string Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaComprovanteNaoFiscalVinculado();

        [DllImport("Convecf.dll")]
        public static extern int ECF_FinalizaModoTEF();

        [DllImport("Convecf.dll")]
        public static extern int ECF_SegundaViaNaoFiscalVinculadoMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_ReimpressaoNaoFiscalVinculadoMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_EstornoNaoFiscalVinculadoMFD(string CGC, string Nome, string Endereco);

        [DllImport("Convecf.dll")]
        public static extern int ECF_TEF_ImprimirRespostaCartao(string path, string MeioPag, string Travar, string valor);

        [DllImport("Convecf.dll")]
        public static extern int ECF_TEF_ImprimirResposta(string path, string MeioPag, string Travar);

        [DllImport("Convecf.dll")]
        public static extern int ECF_TEF_FechaRelatorio();

        [DllImport("Convecf.dll")]
        public static extern int ECF_TEF_EsperarArquivo(string path, string Tempo, string Travar);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioGerencialTEF(string Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_UsaRelatorioGerencialMFDTEF(string Texto);

        #endregion
        
        #region Funções das Operações Não Fiscais e outras


        [DllImport("Convecf.dll")]
        public static extern int ECF_RecebimentoNaoFiscal(string IndiceTot, string Valor, string MeioPagamento);

        // Leiaute banco
        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramarLeiauteCheque(string NumBanco, string LeiauteBanco);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaMoedaSingular(string MoedaSingular);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ProgramaMoedaPlural(string MoedaPlural);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaStatusChequeStr(string VerificaStatusCheque);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RetornaPortaVelocidade(StringBuilder Porta, StringBuilder Velocidade);

        [DllImport("Convecf.dll")]
        public static extern int ECF_VerificaStatusCheque(out int StatusCheque);

        [DllImport("Convecf.dll")]
        public static extern int ECF_ImprimeCheque(string Banco, string Valor, string Nominal,
                                                     string Cidade, string Data, string Mensagem);
        [DllImport("Convecf.dll")]
        public static extern int ECF_ImprimeCopiaCheque();

        [DllImport("Convecf.dll")]
        public static extern int ECF_IncluiCidadeFavorecido(string Cidade, string Nominal);

        // Funções de Comprovante não Fiscal

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);

        [DllImport("Convecf.dll")]
        public static extern int ECF_EfetuaRecebimentoNaoFiscalMFD(string IndiceTotal, string ValorReceb);


        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaRecebimentoNaoFiscalMFD(string Mensagem);

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreRecebimentoNaoFiscal(string indice, string tipoacredesc, string tipovalor, string acredesci, string receb, string texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_EfetuaFormaPagamentoNaoFiscal(string FormaPagam, string ValorFormaPagam);

        [DllImport("Convecf.dll")]
        public static extern int ECF_TotalizaRecebimentoMFD();

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreRelatorioGerencial();

        [DllImport("Convecf.dll")]
        public static extern int ECF_AbreRelatorioGerencialMDF(String Relatorio);
        // ECF_AbreRelatorioGerencialMFD
        [DllImport("Convecf.dll")]
        public static extern int ECF_EnviarTextoCNF(String Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_RelatorioGerencial(String Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_UsaRelatorioGerencialMFD(String Texto);

        [DllImport("Convecf.dll")]
        public static extern int ECF_FechaRelatorioGerencial();




        #endregion

        [DllImport("CONVECF.DLL")]
        public static extern int ECF_RetornoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3);
        
        #region     ANALISA RETORNO DA DLL


        public static void Analisa_Retorno_Dll(int Retorno)
        {
            // alguns retorno
            switch (Retorno)
            {

                case 0:
                    MessageBox.Show("Erro de comunicação", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show("Comando enviado com sucesso ", "ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -2:
                    MessageBox.Show("Parâmetro inválido. ", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -6:
                    MessageBox.Show("O mês selecionado ainda não está terminado.", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -8:
                    MessageBox.Show("Erro ao criar ou gravar no arquivo RETORNO.TXT.", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -11:
                    MessageBox.Show("Existe um documento aberto ", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -24:
                    MessageBox.Show("Forma de pagamento não programada", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -27:
                    MessageBox.Show("Status do ECF diferente de 6,0,0,0 (ACK,ST1,ST2 e ST3)", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -30:
                    MessageBox.Show("Função não compatível com a impressora.", " ECF.NET SWEDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

            }

        }
        #endregion
        
        /**************************************************************************************/
        /*	ST1                                 
            7  (128)  Fim de papel.  
            6   (64)  Pouco papel. 
            5   (32)  Erro de relógio. 
            4   (16)  Impressora em erro. 
            3    (8)  Irrelevante. 
            2    (4)  Comando inexistente. 
            1    (2)  Cupom Fiscal aberto. 
            0    (1)  Número de parâmetros do comando errado. 
        */
        /***********************************************************************************/
        /* ST2
          7  (128) Tipo de parâmetro de comando inválido.  
          6   (64) Memória Fiscal cheia. 
          5   (32) Erro de memória. 
          4   (16) Alíquota não programada. 
          3    (8) Tabela de alíquotas cheia. 
          2    (4) Cancelamento não permitido. 
          1    (2) CNPJ/IE do proprietário não programado. 
          0    (1) Comando não executado. 
        */
        
        #region Analisa Retorno ECF

        public static void Analisa_Retorno_ECF()
        {
            //Retorno igual 6,0,0 Comando Ok.

            int ACK = 0, ST1 = 0, ST2 = 0; //ST3=0;
            ECF_RetornoImpressora(ref  ACK, ref  ST1, ref  ST2);

            // ECF_RetornoImpressora(ref ACK, ref ST1, ref ST2, ref ST3);
            if (ACK != 6)
            {
                MessageBox.Show("Problemas ao Enviar Comando", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            if (ST1 >= 128)
            {
                MessageBox.Show("Fim de Papel Trocar Bubina", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 128;
            }

            else if (ST1 >= 64)
            {
                // O Papel esta Terminando - Verificar Papel

                MessageBox.Show("O Papel esta Terminando - Verificar Papel", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 64;
            }
            else if (ST1 >= 32)
            {
                MessageBox.Show("Erro no Relogio interno do ECF", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 32;
            }

            else if (ST1 >= 16)
            {
                MessageBox.Show("IMPRESSORA EM ERROR -DELIGUE E LIGUE O ECF", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 16;
            }
            else if (ST1 >= 8)
            {
                MessageBox.Show("ERRO NO ENVIO DO COMANDO - FAVOR REPETIR A OPERAÇÃO", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 8;

            }
            else if (ST1 >= 4)
            {
                MessageBox.Show("COMANDO INEXISTENTE -", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 4;

            }

            else if (ST1 >= 2)
            {
                MessageBox.Show("CUPOM FISCAL ABERTO - CANCELE OU TERMINE A VENDA", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 2;
            }

            else if (ST1 >= 1)
            {
                MessageBox.Show("PARAMETRO INVALIDO", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST1 -= 1;

            }


            /***********************************************************************************/
            /* ST2*/

            if (ST2 >= 128)
            {
                MessageBox.Show("TIPO DE CMD INVALIDO - ABRIR CHAMADO", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 128;

            }
            else if (ST2 >= 64)
            {
                MessageBox.Show("MEMORIA FISCAL CHEIA CHAMAR ASSITÊNCIA TECNICA", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 64;
            }
            else if (ST2 >= 32)
            {

                MessageBox.Show("ERRO DE CMOS DO ECF CHAMAR ASSINTECIA TECNICA", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 32;

            }
            else if (ST2 >= 16)
            {
                MessageBox.Show("ALIQUOTA NÃO PROGRAMADA", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 16;

            }
            else if (ST2 >= 8)
            {
                MessageBox.Show("CAPACIDADE DE ALIQUOTA LOTADA ", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 8;
            }
            else if (ST2 >= 4)
            {
                MessageBox.Show("CANCELAMENTO NÃO PERMITIDO PELO ECF", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 4;
            }

            else if (ST2 >= 2)
            {
                MessageBox.Show("CGC/IE NÃO PROGRAMADOS CHAMAR ASSITENCIA TECNICA", "ECF.NET SWEDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ST2 -= 2;

            }
            else if (ST2 >= 1)
            {

                MessageBox.Show("COMANDO NÃO EXECULTADO -  VERIFIQUE STATUS DA IMPRESSORA", "ECF.NET SWEDA",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                ST2 -= 1;
            }

        }

        public static void Analisa()
        {

            int ACK = 0, ST1 = 0, ST2 = 0, ST3 = 0;
            ECF_RetornoImpressoraMFD(ref ACK, ref ST1, ref ST2, ref ST3);

            MessageBox.Show("Retorno do ST3 : " + ST3.ToString(), "ECF.NET SWEDA");

        }
        #endregion
    }//fim classe
}//fim namespace



