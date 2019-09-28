using DllFuturaDataTCC.DataAccessObject;
using DllFuturaDataTCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Controllers
{
    public class iConPlanoContas
    {
        public iModPlanoContas modPlanCont = new iModPlanoContas();
        public iDaoPlanoContas daoPlanCont = new iDaoPlanoContas();
        public bool cIncluirPlanoDeContasMestre()
        {
            bool retorno = daoPlanCont.dIncluirPlanoDeContasMestre(modPlanCont);
            return retorno;
        }

        public bool cObterTodosPlanosDeContaMestres()
        {
            bool retorno = daoPlanCont.dObterTodosPlanosDeContaMestres();
            return retorno;
        }

        public bool cObterUltimoPlanosDeContaMestresCadastrado()
        {
            bool retorno = daoPlanCont.dObterUltimoPlanosDeContaMestresCadastrado();
            return retorno;
        }

        public bool cObterTodosPlanosDeContaMestresDeEntradaOuSaida()
        {
            bool retorno = daoPlanCont.dObterTodosPlanosDeContaMestresDeEntradaOuSaida();
            return retorno;
        }

        public bool cIncluirPlanoDeContasSubCategoria()
        {
            bool retorno = daoPlanCont.dIncluirPlanoDeContasSubCategoria(modPlanCont);
            return retorno;
        }

        public bool cObterUltimoPlanosDeSubCategoriaCadastrado()
        {
            bool retorno = daoPlanCont.dObterUltimoPlanosDeSubCategoriaCadastrado(modPlanCont);
            return retorno;
        }

        public bool cObterTodasSubCategoriasDePlanosDeContasCadastrados()
        {
            bool retorno = daoPlanCont.dObterTodasSubCategoriasDePlanosDeContasCadastrados();
            return retorno;
        }

        public bool cObterTodasSubCategoriasDeEntradaDosPlanosDeContasCadastrados()
        {
            bool retorno = daoPlanCont.dObterTodasSubCategoriasDeEntradaDosPlanosDeContasCadastrados();
            return retorno;
        }

        public bool cObterSubCategoriaPlanoContasPorCodigo()
        {
            bool retorno = daoPlanCont.dObterSubCategoriaPlanoContasPorCodigo(modPlanCont);
            return retorno;
        }   
    }//fim classe
}//fim namespace
