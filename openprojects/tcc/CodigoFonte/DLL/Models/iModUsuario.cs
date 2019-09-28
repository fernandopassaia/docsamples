using DllFuturaDataCriptografia;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModUsuario
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        
        int pk_Codigo;

        public int Pk_Codigo
        {
            get { return pk_Codigo; }
            set { pk_Codigo = value; }
        }

        string nomeUsuario;

        public string NomeUsuario
        {
            get { return nomeUsuario; }
            set { nomeUsuario = value; }
        }

        string loginUsuario;

        public string LoginUsuario
        {
            get { return loginUsuario; }
            set { loginUsuario = value; }
        }

        string lembrete;

        public string Lembrete
        {
            get { return lembrete; }
            set { lembrete = value; }
        }

        string senha;

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public string Funcao
        {
            get { return senha; }
            set { senha = value; }
        }
        #endregion
    }//fim classe
}//fim namespace
