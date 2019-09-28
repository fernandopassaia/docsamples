using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModCaixa
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        
        #region Campos da Tabela Principal de Caixa
        int idCaixa;

        public int IdCaixa
        {
            get { return idCaixa; }
            set { idCaixa = value; }
        }
        string identificacaoCaixa;

        public string IdentificacaoCaixa
        {
            get { return identificacaoCaixa; }
            set { identificacaoCaixa = value; }
        }
       
        decimal valorBrutoOrc;

        public decimal ValorBrutoOrc
        {
            get { return valorBrutoOrc; }
            set { valorBrutoOrc = value; }
        }
        decimal valorAcrescimo;

        public decimal ValorAcrescimo
        {
            get { return valorAcrescimo; }
            set { valorAcrescimo = value; }
        }
        decimal valorDesconto;

        public decimal ValorDesconto
        {
            get { return valorDesconto; }
            set { valorDesconto = value; }
        }
        decimal valorPago;

        public decimal ValorPago
        {
            get { return valorPago; }
            set { valorPago = value; }
        }
        decimal valorDadoCliente;

        public decimal ValorDadoCliente
        {
            get { return valorDadoCliente; }
            set { valorDadoCliente = value; }
        }
        decimal troco;

        public decimal Troco
        {
            get { return troco; }
            set { troco = value; }
        }
        string infoAdicional;

        public string InfoAdicional
        {
            get { return infoAdicional; }
            set { infoAdicional = value; }
        }
        string numeroCupomFiscal;

        public string NumeroCupomFiscal
        {
            get { return numeroCupomFiscal; }
            set { numeroCupomFiscal = value; }
        }
        string formaPagtoPrincipal;

        public string FormaPagtoPrincipal
        {
            get { return formaPagtoPrincipal; }
            set { formaPagtoPrincipal = value; }
        }

        decimal valorFechtCaixa;

        public decimal ValorFechtCaixa
        {
            get { return valorFechtCaixa; }
            set { valorFechtCaixa = value; }
        }

        string dataCaixa;

        public string DataCaixa
        {
            get { return dataCaixa; }
            set { dataCaixa = value; }
        }
        string seqDiario;

        public string SeqDiario
        {
            get { return seqDiario; }
            set { seqDiario = value; }
        }
        string seqGeral;

        public string SeqGeral
        {
            get { return seqGeral; }
            set { seqGeral = value; }
        }
        string mascara_Caixa_Inteira;

        public string Mascara_Caixa_Inteira
        {
            get { return mascara_Caixa_Inteira; }
            set { mascara_Caixa_Inteira = value; }
        }

        decimal valorAberturaCaixa;

        public decimal ValorAberturaCaixa
        {
            get { return valorAberturaCaixa; }
            set { valorAberturaCaixa = value; }
        }

        #endregion

        #region Campos da Tabela Auxiliar da Forma de Pagamento

      
        int parFormaPagtoPlanoConta;

        public int ParFormaPagtoPlanoConta
        {
            get { return parFormaPagtoPlanoConta; }
            set { parFormaPagtoPlanoConta = value; }
        }
        string parNumeroFatura;

        public string ParNumeroFatura
        {
            get { return parNumeroFatura; }
            set { parNumeroFatura = value; }
        }
        int parNumeroParcela;

        public int ParNumeroParcela
        {
            get { return parNumeroParcela; }
            set { parNumeroParcela = value; }
        }
        int parNumeroTotalParcelas;

        public int ParNumeroTotalParcelas
        {
            get { return parNumeroTotalParcelas; }
            set { parNumeroTotalParcelas = value; }
        }
        decimal parValorBruto;

        public decimal ParValorBruto
        {
            get { return parValorBruto; }
            set { parValorBruto = value; }
        }
        decimal parValorFatura;

        public decimal ParValorFatura
        {
            get { return parValorFatura; }
            set { parValorFatura = value; }
        }
        decimal parValorPago;

        public decimal ParValorPago
        {
            get { return parValorPago; }
            set { parValorPago = value; }
        }
        decimal parValorRemanescente;

        public decimal ParValorRemanescente
        {
            get { return parValorRemanescente; }
            set { parValorRemanescente = value; }
        }
        string parMaisInfo;

        public string ParMaisInfo
        {
            get { return parMaisInfo; }
            set { parMaisInfo = value; }
        }

        public iModCliente cliente = new iModCliente(); //objeto pra receber o cliente
        public iModOrcamento orcamento = new iModOrcamento(); //objeto pra receber o cliente
        public iModPlanoContas planoContas = new iModPlanoContas();

        #endregion

        #endregion
    }//fim classe
}//fim namespace
