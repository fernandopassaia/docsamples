using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConEstoque
    {
        public iModEstoque modEstoque = new iModEstoque();
        public iDaoEstoque daoEstoque = new iDaoEstoque();
        public bool cEfetuaMovimentoDoEstoque()
        {
            bool retorno = daoEstoque.dEfetuaMovimentoDoEstoque(modEstoque);
            return retorno;
        }

        public bool cIncluirMovimentacaoEstoque()
        {
            bool retorno = daoEstoque.dIncluirMovimentacaoEstoque(modEstoque);
            return retorno;
        }

        public bool cObterMovimentacaoEstoquePorIDProduto()
        {
            bool retorno = daoEstoque.dObterMovimentacaoEstoquePorIDProduto(modEstoque);
            return retorno;
        }

        public bool cObterDadosDeUmProdutoParaEstorno()
        {
            bool retorno = daoEstoque.dObterDadosDeUmProdutoParaEstorno(modEstoque);
            return retorno;
        }
    }//fim classe
}//fim namespace
