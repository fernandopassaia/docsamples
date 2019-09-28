using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;

namespace CSOBRF_Util.FTP
{
    public class AcessoFTP
    {
        #region Varáveis privadas
        private string addressFTP;
        private string userFTP;
        private string passwordFTP;
        private string filePathFTP;
        private string filePathLocal;
        private string msgStatus;

        private bool modoPassivo = true;

        private List<string> listaFTP;
        private DataTable tbListaFTP;
        #endregion

        #region Variáveis públicas
        /// <summary>Endereço FTP</summary>
        public string ftpEndereco
        {
            get { return this.addressFTP; }
            set { this.addressFTP = value; }
        }
        /// <summary>Usuário FTP</summary>
        public string ftpUsuario
        {
            get { return this.userFTP; }
            set { this.userFTP = value; }
        }
        /// <summary>Senha FTP</summary>
        public string ftpSenha
        {
            get { return this.passwordFTP; }
            set { this.passwordFTP = value; }
        }
        /// <summary>
        /// Upload: Caminho do arquivo que será armazenado no servidor.
        /// Download: Caminho onde o arquivo será armazenado quando baixado do servidor [Você precisa estar registrado e conectado para ver este link.]
        /// </summary>
        public string ftpCaminhoArquivoLocal
        {
            get { return this.filePathLocal; }
            set { this.filePathLocal = value; }
        }
        /// <summary>
        /// Upload: Nome com o qual o arquivo que será armazenado no servidor.
        /// Download: Nome do arquivo que será baixado do servidor.
        /// </summary>
        public string ftpArquivoFTP
        {
            get { return this.filePathFTP; }
            set { this.filePathFTP = value; }
        }
        /// <summary>Status da última operação</summary>
        public string mensagemStatus
        {
            get { return this.msgStatus; }
        }

        /// <summary>
        /// True -> Modo Passivo; False -> Modo Ativo
        /// </summary>
        public bool ftpModoPassivo
        {
            set { this.modoPassivo = value; }
        }

        /// <summary>Armazena numa lista, o conteúdo do diretório filtrado.</summary>
        public List<string> ftpDiretorioFTP
        {
            get { return this.listaFTP; }
        }
        /// <summary>Armazena num DataTable, o conteúdo do diretório filtrado.</summary>
        public DataTable ftpTbListaFTP
        {
            get { return this.tbListaFTP; }
        }
        #endregion

        #region Métodos que compõem a rotina
        /// <summary>Realiza upload de arquivos via FTP</summary>
        /// <returns>Retorna true se a operação for bem sucedida, em caso contrário, retorna false</returns>
        public bool ftpUpload()
        {
            bool sucesso;
            try
            {
                //O endereço deverá estar no seguinte formato: ftp://servidor.com/nomeArquivo.ext
                string caminho = this.addressFTP + "/" + Path.GetFileName(this.filePathFTP);
                //Cria uma solicitação FTP
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(caminho);

                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Credentials = new NetworkCredential(this.userFTP, this.passwordFTP);
                ftpRequest.UsePassive = this.modoPassivo;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                //Carrega o arquivo que será enviado para o servidor FTP
                FileStream arquivo = File.OpenRead(this.filePathLocal);
                byte[] buffer = new byte[arquivo.Length];

                arquivo.Read(buffer, 0, buffer.Length);
                arquivo.Close();

                //Upload file
                Stream reqStream = ftpRequest.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                this.msgStatus = "";
                sucesso = true;
            }
            catch (Exception oErro)
            {
                this.msgStatus = oErro.Message;
                sucesso = false;
            }
            return sucesso;
        }

        /// <summary>Realiza download de arquivos via FTP</summary>
        /// <returns>Retorna true se a operação for bem sucedida, em caso contrário, retorna false</returns>
        public bool ftpDownload()
        {
            bool sucesso;
            byte[] downloadedData = new byte[0];

            try
            {
                //O endereço deverá estar no seguinte formato: ftp://servidor.com/arquivo.txt
                string caminho = this.addressFTP + "/" + Path.GetFileName(this.filePathFTP);

                //Cria uma solicitação FTP
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(caminho);

                //Now get the actual data
                request = (FtpWebRequest)FtpWebRequest.Create(caminho);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(this.userFTP, this.passwordFTP);
                request.UsePassive = this.modoPassivo;
                request.UseBinary = true;
                request.KeepAlive = false; // Fechar a conexão quando concluído

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();

                // Transfere para a memória
                // Nota: ajustar os fluxos aqui para fazer o download diretamente para o disco rígido
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks

                while (true)
                {
                    // Tenta ler os dados
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        //Escreve o conteúdo baixado
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                // Converter o fluxo baixado para um array de bytes
                downloadedData = memStream.ToArray();

                reader.Close();
                memStream.Close();
                response.Close();

                // Escreve os bytes do arquivo
                FileStream newFile = new FileStream(this.filePathLocal, FileMode.Create);
                newFile.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();

                sucesso = true;
            }
            catch (Exception oErro)
            {
                this.msgStatus = oErro.Message;
                sucesso = false;
            }

            return sucesso;
        }

        /// <summary>Preenche uma lista e DataTable com os dados do diretório FTP</summary>
        /// <returns>Retorna true se a operação for bem sucedida, em caso contrário, retorna false</returns>
        public bool ftpListarDiretorios()
        {
            List<string> files = new List<string>();
            bool sucesso;
            string conteudo;

            try
            {
                //O endereço deverá estar no seguinte formato: ftp://servidor.com/pastaDestino
                string caminho = this.addressFTP + "/" + Path.GetFileName(this.filePathFTP);

                //Cria uma solicitação FTP
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(caminho);

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(this.userFTP, this.passwordFTP);
                request.UsePassive = this.modoPassivo;
                request.UseBinary = true;
                request.KeepAlive = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                this.tbListaFTP = new DataTable();
                this.tbListaFTP.Columns.Add("Descrição", typeof(string));
                this.tbListaFTP.Columns.Add("Caminho", typeof(string));

                while (!reader.EndOfStream)
                {
                    conteudo = reader.ReadLine();

                    this.tbListaFTP.Rows.Add(conteudo, conteudo);
                    files.Add(conteudo);
                }

                reader.Close();
                responseStream.Close();
                response.Close();

                this.msgStatus = "";
                sucesso = true;
            }

            catch (Exception oErro)
            {
                this.msgStatus = oErro.Message;
                sucesso = false;
            }

            this.listaFTP = files;

            return sucesso;
        }

        /// <summary>Preenche uma lista e DataTable com os dados do diretório FTP</summary>
        /// <param name="extensao">Extensão dos arquivos (*.ext)</param>
        /// <returns>Retorna true se a operação for bem sucedida, em caso contrário, retorna false</returns>
        public bool ftpListarDiretorios(string extensao)
        {
            List<string> files = new List<string>();
            bool sucesso;
            string conteudo;

            try
            {
                //O endereço deverá estar no seguinte formato: ftp://servidor.com/pastaDestino
                string caminho = this.addressFTP + "/" + Path.GetFileName(this.filePathFTP);

                //Cria uma solicitação FTP
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(caminho);

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(this.userFTP, this.passwordFTP);
                request.UsePassive = this.modoPassivo;
                request.UseBinary = true;
                request.KeepAlive = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                this.tbListaFTP = new DataTable();
                this.tbListaFTP.Columns.Add("Descrição", typeof(string));
                this.tbListaFTP.Columns.Add("Caminho", typeof(string));

                while (!reader.EndOfStream)
                {
                    conteudo = reader.ReadLine();
                    // Se a extensão for a exigida.
                    if (Path.GetExtension(conteudo).ToUpper() == extensao.ToUpper())
                    {
                        this.tbListaFTP.Rows.Add(conteudo, conteudo);
                        files.Add(conteudo);
                    }
                }

                reader.Close();
                responseStream.Close();
                response.Close();

                this.msgStatus = "";
                sucesso = true;
            }

            catch (Exception oErro)
            {
                this.msgStatus = oErro.Message;
                sucesso = false;
            }

            this.listaFTP = files;

            return sucesso;
        }

        /// <summary>Deleta um arquivo de um diretório FTP</summary>
        /// <returns>Retorna true se a operação for bem sucedida, em caso contrário, retorna false</returns>
        public bool ftpDeletarArquivo()
        {
            bool sucesso;
            try
            {
                //O endereço deverá estar no seguinte formato: ftp://servidor.com/nomeArquivo.ext
                string caminho = this.addressFTP + "/" + Path.GetFileName(this.filePathFTP);
                //Cria uma solicitação FTP
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(caminho);

                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpRequest.Credentials = new NetworkCredential(this.userFTP, this.passwordFTP);
                ftpRequest.UsePassive = this.modoPassivo;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                // Deleta o arquivo
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                response.Close();

                this.msgStatus = "";
                sucesso = true;
            }
            catch (Exception oErro)
            {
                this.msgStatus = oErro.Message;
                sucesso = false;
            }
            return sucesso;
        }
        #endregion
    }//fim classe
}//fim namespace