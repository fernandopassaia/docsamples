using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConOrcamento
    {
        public iModOrcamento modOrcamento = new iModOrcamento();
        public iDaoOrcamento daoOrcamento = new iDaoOrcamento();
        public int cInsereOrcamento()
        {
            int retorno = daoOrcamento.dInsereOrcamento(modOrcamento);
            return retorno;
        }

        public bool cAlteraOrcamento()
        {
            bool retorno = daoOrcamento.dAlteraOrcamento(modOrcamento);
            return retorno;
        }

        public iModOrcamento[] cObterOrcamentos()
        {
            iModOrcamento[] orcamentos = daoOrcamento.dObterOrcamento();
            return orcamentos;
        }

        public iModItensOrcamento[] cObterProdutosDeUmOrcamento()
        {
            iModItensOrcamento[] itensOrcamento = daoOrcamento.dObterProdutosDeUmOrcamento(modOrcamento);
            return itensOrcamento;
        }

        public iModOrcamento[] cObterOrcamentosApenasEmAberto()
        {
            iModOrcamento[] orcamentos = daoOrcamento.dObterOrcamentosApenasEmAberto();
            return orcamentos;
        }

        public iModOrcamento[] cObterOrcamentoPorID()
        {
            iModOrcamento[] orcamentos = daoOrcamento.dObterOrcamentoPorID(modOrcamento);
            return orcamentos;
        }

        public DataSet cRetornaDadosOrcamentoRelatorio(int codOrc)
        {
            DataSet dsRetorno = daoOrcamento.dRetornaDadosOrcamentoRelatorio(codOrc);
            return dsRetorno;
        }
    }//fim classe
}//fim namespace
