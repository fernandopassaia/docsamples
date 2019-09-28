using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModProduto
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }

        Byte[] imagemProduto;

        public Byte[] ImagemProduto
        {
            get { return imagemProduto; }
            set { imagemProduto = value; }
        }
        int pk_Codigo;

        public int Pk_Codigo
        {
            get { return pk_Codigo; }
            set { pk_Codigo = value; }
        }

        string codigoFabric;
        public string CodigoFabric
        {
          get { return codigoFabric; }
          set { codigoFabric = value; }
        }
        
        string codigoOriginal;
        public string CodigoOriginal
        {
          get { return codigoOriginal; }
          set { codigoOriginal = value; }
        }
        
        string descricao;
        public string Descricao
        {
          get { return descricao; }
          set { descricao = value; }
        }
        
        string aplicacao;
        public string Aplicacao
        {
          get { return aplicacao; }
          set { aplicacao = value; }
        }

        decimal precoCusto;
        public decimal PrecoCusto
        {
          get { return precoCusto; }
          set { precoCusto = value; }
        }

        decimal margemLucro;
        public decimal MargemLucro
        {
          get { return margemLucro; }
          set { margemLucro = value; }
        }
        
        decimal precoVenda;
        public decimal PrecoVenda
        {
          get { return precoVenda; }
          set { precoVenda = value; }
        }


        decimal qtdAtual;
        public decimal QtdAtual
        {
          get { return qtdAtual; }
          set { qtdAtual = value; }
        }

        string unidade;
        public string Unidade
        {
          get { return unidade; }
          set { unidade = value; }
        }

        decimal porcImpPago;
        public decimal PorcImpPago
        {
          get { return porcImpPago; }
          set { porcImpPago = value; }
        }

        string icms;
        public string Icms
        {
          get { return icms; }
          set { icms = value; }
        }

        string corredorSetor;
        public string CorredorSetor
        {
          get { return corredorSetor; }
          set { corredorSetor = value; }
        }

        string localCaixa;
        public string LocalCaixa
        {
          get { return localCaixa; }
          set { localCaixa = value; }
        }
        
        string maisInfo;
        public string MaisInfo
        {
            get { return maisInfo; }
            set { maisInfo = value; }
        }

        bool possuiImagem;
        public bool PossuiImagem
        {
            get { return possuiImagem; }
            set { possuiImagem = value; }
        }

        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        DataSet ds_DadosRetorno;

        public DataSet Ds_DadosRetorno
        {
            get { return ds_DadosRetorno; }
            set { ds_DadosRetorno = value; }
        }
        #endregion
    }//fim classe
}//fim namespace
