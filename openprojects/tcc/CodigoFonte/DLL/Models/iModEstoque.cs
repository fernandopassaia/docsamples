using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModEstoque
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        int entradaOuSaida;

        public int EntradaOuSaida
        {
            get { return entradaOuSaida; }
            set { entradaOuSaida = value; }
        }
      
        decimal qtd;

        public decimal Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }

      
        decimal valorCusto;

        public decimal ValorCusto
        {
            get { return valorCusto; }
            set { valorCusto = value; }
        }
        decimal margemLucro;

        public decimal MargemLucro
        {
            get { return margemLucro; }
            set { margemLucro = value; }
        }
        decimal valorVenda;

        public decimal ValorVenda
        {
            get { return valorVenda; }
            set { valorVenda = value; }
        }
        decimal valorTotal;
        
        public decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }
        
        string maisInfo;

        public string MaisInfo
        {
            get { return maisInfo; }
            set { maisInfo = value; }
        }

        public iModOrcamento orcamento = new iModOrcamento(); //objeto pra receber o orcamento
        public iModProduto produto = new iModProduto(); //objeto pra receber o produto
        public iModCaixa caixa = new iModCaixa(); //objeto pra receber o caixa
        public iModCliente cliente = new iModCliente();
        #endregion
    }//fim classe
}//fim namespace
