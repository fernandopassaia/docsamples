using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConCaixa
    {
        public iModCaixa modCaixa = new iModCaixa();
        public iDaoCaixa daoCaixa = new iDaoCaixa();
        public bool cFecharVenda()
        {
            bool retorno = daoCaixa.dFecharVenda(modCaixa);
            return retorno;
        }

        public bool cIncluirRecebimentoAVistaCaixa()
        {
            bool retorno = daoCaixa.dIncluirRecebimentoAVistaCaixa(modCaixa);
            return retorno;
        }

        public bool cObterPedidosFechadosPorCaixa()
        {
            bool retorno = daoCaixa.dObterPedidosFechadosPorCaixa(modCaixa);
            return retorno;
        }

        public bool cEfetuaFechamentoCaixa()
        {
            bool retorno = daoCaixa.dEfetuaFechamentoCaixa(modCaixa);
            return retorno;
        }

        public bool cObterInformacoesUltimoCaixa()
        {
            bool retorno = daoCaixa.dObterInformacoesUltimoCaixa(modCaixa);
            return retorno;
        }

        public bool cObterPedidosFechadosPorIDCaixa()
        {
            bool retorno = daoCaixa.dObterPedidosFechadosPorIDCaixa(modCaixa);
            return retorno;
        }

        public bool cObterMovimentacaoEstoquePorIDCaixa()
        {
            bool retorno = daoCaixa.dObterMovimentacaoEstoquePorIDCaixa(modCaixa);
            return retorno;
        }

        public bool cObterMovimentacaoFinanceiraPorIDCaixa()
        {
            bool retorno = daoCaixa.dObterMovimentacaoFinanceiraPorIDCaixa(modCaixa);
            return retorno;
        }

        public string cObterUltimoSequencialDosCaixas()
        {
            string retorno = daoCaixa.dObterUltimoSequencialDosCaixas();
            return retorno;
        }

        public bool cIncluirAbrirCaixa()
        {
            bool retorno = daoCaixa.dIncluirAbrirCaixa(modCaixa);
            return retorno;
        }

        public string cObterIDUltimoCaixa()
        {
            string retorno = daoCaixa.dObterIDUltimoCaixa();
            return retorno;
        }

        public bool cObterInformacoesSobreCaixaPelaPK()
        {
            bool retorno = daoCaixa.dObterInformacoesSobreCaixaPelaPK(modCaixa);
            return retorno;
        }
        
    }//fim namespace
}//fim classe
