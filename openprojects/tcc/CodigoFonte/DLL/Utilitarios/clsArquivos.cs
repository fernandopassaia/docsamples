using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsArquivos
    {
        #region Variaveis Internas e Método para Retornar DataSet de Retorno
        string erroClasse { get; set; }
        private DataSet ds_DadosRetorno = new DataSet();

        /// <summary>
        /// Método de Acesso para Atribuir um DataSet ao DataSet privado da Classe
        /// </summary>
        /// <param name="ds_DadosRet">DataSet que será atribuido</param>
        public void setDs_DadosRetorno(DataSet ds_DadosRet)
        {
            ds_DadosRetorno = ds_DadosRet;
        }

        /// <summary>
        /// Método de Acesso para Retornar um DataSet Consultado através de um Método
        /// </summary>
        /// <returns>Retorna o DataSet preenchido por um método de pesquisa</returns>
        public DataSet getDs_DadosRetorno()
        {
            return this.ds_DadosRetorno;
        }
        #endregion

        #region "Método para Criar Arquivo"
        /// <summary>
        /// /// Método para Criar Arquivo em diretório;
        /// Pré requisitos:
        /// - Passar o Caminho;
        /// - Nome do Arquivo;
        /// - Extensão do Arquivo;
        /// </summary>
        /// <param name="caminho">Caminho do Arquivo (ex: C:\FuturaData\Standart_Comercio)</param>
        /// <param name="nomeArquivo">Nome do arquivo (ex: teste.txt)</param>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se Conseguir, false se não</returns>
        public bool criarArquivoTxt(string caminho, string nomeArquivo, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            if (File.Exists(caminho + @"\\" + nomeArquivo))
            {
                return true;
            }

            else
            {                
                StreamWriter myStreamWriter = null;

                // Ensure that the creation of the new StreamWriter is wrapped in a 
                //   try-catch block, since an invalid filename could have been used.
                try
                {
                    // Create a StreamWriter using a static File class.
                    myStreamWriter = File.CreateText(caminho + "\\" + nomeArquivo);
                    
                    // Write the entire contents of the txtFileText text box
                    //   to the StreamWriter in one shot.
                    
                    myStreamWriter.Write("PORTA=LPT1");
                    myStreamWriter.Write(Environment.NewLine.ToString());

                    myStreamWriter.Flush();
                    return true;
                }
                //caindo no CATCH chama as rotinas que geram os logs de erro
                catch (Exception erro)
                {
                    
                    erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsArquivos", "criaArquivoTxt()", erro.Message.ToString(), "Erro criando arquivo de TXT de LOG");
                    return false;
                }

                finally
                {
                    // Close the object if it has been created.
                    if (myStreamWriter != null)
                    {
                        myStreamWriter.Close();
                    }
                }
            }//fim else
        }
        #endregion

        #region "Método para Gravar Conteúdo em um Arquivo TXT sem tirar o conteúdo já existente"
        /// <summary>
        /// Grava o Conteudo em um Arquivo TXT
        /// </summary>
        /// <param name="conteudo">Conteudo que vai ser gravado no arquivo</param>
        /// <param name="caminho">Caminho do Arquivo (c:\futuradata\infosiga)</param>
        /// <param name="nomeArquivo">nome do arquivo (teste.txt)</param>
        /// <param name="numeroUsuarioLogado">Numero do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeUsuarioLogado">Nome do Usuário Logado no Sistema (Trat.Erros)</param>
        /// <param name="nomeHost">Nome do Host Logado no Sistema (Trat.Erros)</param>
        /// <returns>Retorna True se Conseguir Gravar, False se não</returns>
        public bool gravarConteudoArquivoTxt(string conteudo, string caminho, string nomeArquivo, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            // The StreamWriter must be defined outside of the try-catch block
            //   in order to reference it in the Finally block.
            StreamWriter myStreamWriter = null;

            // Ensure that the creation of the new StreamWriter is wrapped in a 
            //   try-catch block, since an invalid filename could have been used.
            try
            {
                // Create a StreamWriter using a static File class.
                myStreamWriter = File.AppendText(caminho + @"\" + nomeArquivo);

                // Write the entire contents of the txtFileText text box
                //   to the StreamWriter in one shot.                 
                myStreamWriter.Write(conteudo);
                myStreamWriter.Write(Environment.NewLine.ToString());

                // Flush the stream to ensure everything is flushed
                myStreamWriter.Flush();
                return true;

            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsArquivos", "gravarConteudoArquitoTXT()", erro.Message.ToString(), "Erro gravando conteudo em um arquivo TXT");
                return false;
            }

            finally
            {
                // Close the object if it has been created.
                if (myStreamWriter != null)
                {
                    myStreamWriter.Close();
                }
            }
        }
        #endregion                
                        
        #region Método "Para deletar Arquivo do diretório especificado"
        /// <summary>
        /// Deleta um arquivo passando o caminho completo do mesmo
        /// </summary>
        /// <param name="diretorioCompletoComNomeArquivo">Caminho do Arquivo (ex: c:\dir\text.txt)</param>
        /// <returns>Retorna True se conseguir, false se náo</returns>
        public bool deletarArquivo(string diretorioCompletoComNomeArquivo, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            try
            {
                string deletar = diretorioCompletoComNomeArquivo;
                File.Delete(deletar);
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (Exception erro)
            {
                
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsArquivos", "deletarArquivo()", erro.Message.ToString(), "Erro Deletando arquivo");
                return false;
            }            
        }
        #endregion

        #region Verifica se Arquivo Existe
        /// <summary>
        /// Verifica se um Arquivo Existe
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do Arquivo para Verificacao</param>
        /// <returns>Retorna True se existir, false se não</returns>
        public bool verificaSeArquivoExiste(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }//fim classe
}//fim namespace
