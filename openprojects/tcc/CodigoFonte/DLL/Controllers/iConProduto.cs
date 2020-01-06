using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{  
    public class iConProduto
    {
        public iModProduto modProduto = new iModProduto();
        public iDaoProduto daoProduto = new iDaoProduto();
        public bool cInsereProduto()
        {
            bool retorno = daoProduto.dInsereProduto(modProduto);
            return retorno;
        }

        public bool cAlteraProduto()
        {
            bool retorno = daoProduto.dAlteraProduto(modProduto);
            return retorno;
        }

        public bool cExcluiProduto()
        {
            bool retorno = daoProduto.dExcluiProduto(modProduto);
            return retorno;
        }

        public iModProduto[] cObterProduto()
        {
            iModProduto[] produtos = daoProduto.dObterProduto();
            return produtos;
        }

        public Image cObterImagem()
        {
            Image retorno = daoProduto.dRecuperarImagemProdutoNoBanco(modProduto);
            return retorno;
        }
    }//fim classe
}//fim namespace
