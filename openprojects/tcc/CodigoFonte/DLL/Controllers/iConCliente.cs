using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConCliente
    {
        public iModCliente modCliente = new iModCliente();
        public iDaoCliente daoCliente = new iDaoCliente();
        public bool cInsereCliente()
        {
            bool retorno = daoCliente.dInsereCliente(modCliente);
            return retorno;
        }

        public bool cAlteraCliente()
        {
            bool retorno = daoCliente.dAlteraCliente(modCliente);
            return retorno;
        }

        public bool cExcluiCliente()
        {
            bool retorno = daoCliente.dExcluiCliente(modCliente);
            return retorno;
        }

        public iModCliente[] cObterCliente()
        {
            iModCliente[] clientes = daoCliente.dObterCliente();
            return clientes;
        }

        public bool cObterMovimentacao()
        {
            bool retorno = daoCliente.dObterOrcamentosEVendasDoCliente(modCliente);
            return retorno;
        }

        public Image cObterImagem()
        {
            Image retorno = daoCliente.dRecuperarImagemClienteNoBanco(modCliente);
            return retorno;
        }
    }//fim classe
}//fim namespace
