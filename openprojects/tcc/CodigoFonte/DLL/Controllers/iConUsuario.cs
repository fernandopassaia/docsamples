using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConUsuario
    {
        public iModUsuario modUsuario = new iModUsuario();
        public iDaoUsuario daoUsuario = new iDaoUsuario();
        public bool cInsereUsuario()
        {
            bool retorno = daoUsuario.dInsereUsuario(modUsuario);
            return retorno;
        }

        public bool cAlteraUsuario()
        {
            bool retorno = daoUsuario.dAlteraUsuario(modUsuario);
            return retorno;
        }

        public bool cExcluiUsuario()
        {
            bool retorno = daoUsuario.dExcluiUsuario(modUsuario);
            return retorno;
        }

        public iModUsuario[] cObterUsuario()
        {
            iModUsuario[] arrayUsuario = daoUsuario.dObterUsuario();
            return arrayUsuario;
        }

        public iModUsuario[] cObterInformacoesUsuario()
        {
            iModUsuario[] arrayUsuario = daoUsuario.dObterInformacoesUsuario(modUsuario);
            return arrayUsuario;
        }

        public bool cEfetuarLogon()
        {
            bool retorno = daoUsuario.dEfetuarLogon(modUsuario);
            return retorno;
        }
    }//fim classe
}//fim namespace
