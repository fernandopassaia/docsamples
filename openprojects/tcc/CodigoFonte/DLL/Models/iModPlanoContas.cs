using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DllFuturaDataTCC.Utilitarios;
using System.Data.SqlClient;

namespace DllFuturaDataTCC.Models
{
    public class iModPlanoContas
    {
        #region Atributos da Classe
        string descricaoCategoriaMestre;

        public string DescricaoCategoriaMestre
        {
            get { return descricaoCategoriaMestre; }
            set { descricaoCategoriaMestre = value; }
        }
        string mascaraPlanoMestre;

        public string MascaraPlanoMestre
        {
            get { return mascaraPlanoMestre; }
            set { mascaraPlanoMestre = value; }
        }

        string descricaoSubCategoria;

        public string DescricaoSubCategoria
        {
          get { return descricaoSubCategoria; }
          set { descricaoSubCategoria = value; }
        }
        string mascaraSubPlano;

        public string MascaraSubPlano
        {
          get { return mascaraSubPlano; }
          set { mascaraSubPlano = value; }
        }
        int planoMestreSubPlano;

        public int PlanoMestreSubPlano
        {
          get { return planoMestreSubPlano; }
          set { planoMestreSubPlano = value; }
        }

        int idCategoriaMestre;

        public int IdCategoriaMestre
        {
            get { return idCategoriaMestre; }
            set { idCategoriaMestre = value; }
        }

        #endregion
    }//fim classe
}//fim namespace
