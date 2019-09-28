using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CSOBRF_Util.Arquivos
{
    public class LeituraConversaoArquivos
    {
        #region Leitura de Arquivo XLS/XLSX (Excel)        
        public DataTable lerExcel(string caminhoArquivo, string comandoSQLExecutar)
        {
            try
            {
                //caminhoArquivo = @"C:\dados\Excel\Empregados.xlsx";
                OleDbConnection conexao;
                OleDbCommand comando;
                //String conexaoComando = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + caminhoArquivo + ";" + "Extended Properties=Excel 8.0;");
                String conexaoComando = "";

                //if (caminhoArquivo.ToUpper().Contains(".XLSX"))
                //{

                //18102017 - REMOVI O OLEDB4 por que tava dando pau após o Windows Update. Mais info aqui: https://stackoverflow.com/questions/46706254/oledb-exception-when-trying-to-make-connection-with-excel
                conexaoComando = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + caminhoArquivo + ";Extended Properties='Excel 12.0;HDR=Yes';";

                //}
                //else
                //{
                    //conexaoComando = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + caminhoArquivo + ";Extended Properties='Excel 8.0;HDR=Yes';";                    
                //}
                

                conexao = new OleDbConnection(conexaoComando);
                conexao.Open();

                //DataTable activityDataTable = conexao.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                
                comando = new OleDbCommand();
                comando.Connection = conexao;
                comando.CommandType = CommandType.Text;
                comando.CommandText = comandoSQLExecutar; 

                OleDbDataAdapter DataAdap = new OleDbDataAdapter(comando);
                DataSet DsDataSet = new DataSet();                
                DataAdap.Fill(DsDataSet);
                return DsDataSet.Tables[0];
            }
            catch(Exception Erro)
            {
                MessageBox.Show("Falha ao Ler XLSx CSOBRF_Util: " + Erro.Message.ToString());
                return null;
            }
        }//fim método
        #endregion

        #region Pega o nome das Folhas do Arquivo Excel, para aqueles casos que não localiza o nome das páginas internas do Excel
        // Utilize o namespace  System.Data.OleDb;
        public String[] PegarNomesFolhasExcel(string caminhoExcel)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Configura a Connection String
                String connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\";", caminhoExcel);

                // Cria o objeto de conexão usando a connection string
                objConn = new OleDbConnection(connString);

                // Abre a conexão com o banco de dados
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Adiciona os nomes na array
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop através de todas as folhas se você quiser também..
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Consultar cada folha de excel
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion
    }//fim classe
}//fim namespace
