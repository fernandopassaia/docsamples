using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModItensOrcamento
    {
        private int pkIdItemVenda;
        public int PkIdItemVenda
        {
            get { return pkIdItemVenda; }
            set { pkIdItemVenda = value; }
        }

        private int numeroItem;

        public int NumeroItem
        {
            get { return numeroItem; }
            set { numeroItem = value; }
        }
        private string codigoFabric;

        public string CodigoFabric
        {
            get { return codigoFabric; }
            set { codigoFabric = value; }
        }
        private string descricaoAplicacao;

        public string DescricaoAplicacao
        {
            get { return descricaoAplicacao; }
            set { descricaoAplicacao = value; }
        }
        private decimal precoVendaBanco;

        public decimal PrecoVendaBanco
        {
            get { return precoVendaBanco; }
            set { precoVendaBanco = value; }
        }
        private decimal valorTotalSemDescAcre;

        public decimal ValorTotalSemDescAcre
        {
            get { return valorTotalSemDescAcre; }
            set { valorTotalSemDescAcre = value; }
        }
        private decimal valorUnit;

        public decimal ValorUnit
        {
            get { return valorUnit; }
            set { valorUnit = value; }
        }
        private decimal quantidade;

        public decimal Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        private decimal valorTotal;

        public decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }
        private decimal desconto;

        public decimal Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }
        private decimal acrescimo;

        public decimal Acrescimo
        {
            get { return acrescimo; }
            set { acrescimo = value; }
        }

        public iModProduto produto = new iModProduto();
    }
}
