using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModOrcamento
    { 
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        
        int pkCodigo;

        public int PkCodigo
        {
            get { return pkCodigo; }
            set { pkCodigo = value; }
        }
        
        string infoAdicional;

        public string InfoAdicional
        {
            get { return infoAdicional; }
            set { infoAdicional = value; }
        }
        decimal valorFinal;

        public decimal ValorFinal
        {
            get { return valorFinal; }
            set { valorFinal = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private DateTime dataOrc;

        public DateTime DataOrc
        {
            get { return dataOrc; }
            set { dataOrc = value; }
        }


        public iModItensOrcamento[] itensOrcamento; //objeto para receber os itens do Orcamento
        public iModCliente cliente = new iModCliente(); //objeto pra receber o cliente
        #endregion
    }//fim classe
}//fim namespace
