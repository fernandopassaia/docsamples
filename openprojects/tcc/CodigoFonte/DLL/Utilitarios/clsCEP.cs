using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsCEP
    {
        DataSet ds_DadosRetorno = new DataSet();

        public DataSet Ds_DadosRetorno
        {
            get { return ds_DadosRetorno; }
            set { ds_DadosRetorno = value; }
        }

        #region Retorna CEP Presente no Banco de Dados FuturaData (metodo OK com nova Thread)
        public bool retornaCEPBancoDadosFuturaData(string cep)
        {                        
            SqlConnection conexao = new clsConexao().abrirConexaoBd();
            SqlCommand ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = conexao;
            ComandoSQL.CommandType = CommandType.StoredProcedure;
            ComandoSQL.CommandText = "sp_consulta_cep";
            ComandoSQL.CommandTimeout = 6;

            SqlParameter parametro = new SqlParameter("@CEP", cep);
            ComandoSQL.Parameters.Add(parametro);

            DataSet dsDadosRet = new DataSet();
            SqlDataAdapter DataAdap = new SqlDataAdapter(ComandoSQL);

            try
            {    //Executo a Query                
                DataAdap.Fill(dsDadosRet);
                ds_DadosRetorno = dsDadosRet;
                return true;
            }//fim try


        //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                return false;
            }

            finally
            {
                conexao.Close();
                conexao = null;
                ComandoSQL = null;
            }
        }//fim if verificaConexaoComAInternet
        #endregion        
    }
}
