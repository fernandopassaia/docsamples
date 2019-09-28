using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CSOBRF_Util.ImprModoTexto
{
    public class ComunicacaoImprTexto
    {
        #region Variaveis Internas 
        private int FILE_SHARE_WRITE = 2;
        private StreamWriter fileWriter;
        private int GENERIC_WRITE = 0x40000000;
        private int hPort;
        private IntPtr hPortP;
        private bool lOK = false;
        private int OPEN_EXISTING = 3;
        private FileStream outFile;
        private string sPorta = "LPT1";
        #endregion

        #region Set do Char
        private string Chr(int asc)
        {
            string str = "";
            return (str + ((char)asc));
        }
        #endregion

        #region DllImport da Kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern int CloseHandle(int hObject);
        [DllImport("kernel32.dll")]
        private static extern int CreateFileA(string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);
        #endregion

        #region Imprimir Linha
        /// <summary>
        /// Imprime uma linha na impressora (mandar String)
        /// </summary>
        /// <param name="sLinha">Linha que será impressa</param>
        public void Imp(string sLinha)
        {
            if (this.lOK)
            {
                this.fileWriter.Write(sLinha);
                this.fileWriter.Flush();
            }
        }
        #endregion

        #region Imprimir em Linha por Coluna
        /// <summary>
        /// Imprime em uma Coluna (passar o numero dela e a Linha
        /// </summary>
        /// <param name="nCol"></param>
        /// <param name="sLinha"></param>
        public void ImpCol(int nCol, string sLinha)
        {
            sLinha.PadLeft(nCol, ' ');
            this.Imp(sLinha);
        }

        /// <summary>
        /// Imprime em uma Coluna (passar o numero dela e a Linha
        /// </summary>
        /// <param name="nCol"></param>
        /// <param name="sLinha"></param>
        public void ImpColLF(int nCol, string sLinha)
        {
            sLinha.PadLeft(nCol, ' ');
            this.ImpLFormatacao(sLinha);
        }
        #endregion

        #region Imprime linha com Formatacao
        /// <summary>
        /// Imprime Linha na Impressora - Modo de Uso:
        /// (enviar parametro assim:) (NegritoOn+"Negrito ligado"+imp.NegritoOff)
        /// ou (imp.Expandido+"Expandido"+imp.Normal) ou (imp.Comprimido+"Comprimido"+imp.Normal)
        /// se enviar apenas o texto "TESTE" irá imprimir sem formatacao alguma
        /// </summary>
        /// <param name="sLinha">Texto da Linha (enviar o tipo de formatacao junto),
        /// se enviar apenas String "TESTE" irá imprimir sem formatacao alguma</param>
        public void ImpLFormatacao(string sLinha)
        {
            if (this.lOK)
            {
                try
                {
                this.fileWriter.WriteLine(sLinha);
                this.fileWriter.Flush();
                //Thread.Sleep(200);
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Inicio (Abre a Impressora pra Impressão do Cupom)
        /// <summary>
        /// Region Abre a Impressora para a Impressão do Cupom
        /// </summary>
        /// <param name="sPortaInicio">Porta para Abertura (Lpt1, Com1, Com2)</param>
        /// <returns></returns>
        public bool IniciarImpressao(string sPortaInicio)
        {
            sPortaInicio.ToUpper();            
            this.sPorta = sPortaInicio;
            this.hPort = CreateFileA(this.sPorta, this.GENERIC_WRITE, this.FILE_SHARE_WRITE, 0, this.OPEN_EXISTING, 0, 0);
            if (this.hPort != -1)
            {
                this.hPortP = new IntPtr(this.hPort);
                this.outFile = new FileStream(this.hPortP, FileAccess.Write, false);
                this.fileWriter = new StreamWriter(this.outFile);
                this.lOK = true;
            }
            else
            {
                this.lOK = false;
            }
            return this.lOK;
        }
        #endregion

        #region Pula Linha
        /// <summary>
        /// Pula Linha na Impressora (Inteiro com o número de linhas a pular)
        /// </summary>
        /// <param name="nLinha">Inteiro com Número de Linhas a Pular</param>
        public void PulaLinha(int nLinha)
        {
            for (int i = 0; i < nLinha; i++)
            {                
                this.ImpLFormatacao(" ");
            }
        }
        #endregion

        #region Formatação de Linhas (Comprimido, Negrito, Extendido, etc)
        /// <summary>
        /// Region Imprime Comprimido
        /// </summary>
        public string Comprimido
        {
            get
            {
                return this.Chr(15);
            }
        }

        /// <summary>
        /// Region Imprime Expandido
        /// </summary>
        public string Expandido
        {
            get
            {
                return this.Chr(14);
            }
        }

        /// <summary>
        /// Region Volta o Expandido pro Normal
        /// </summary>
        public string ExpandidoNormal
        {
            get
            {
                return this.Chr(20);
            }
        }

        /// <summary>
        /// Region Tira o Negrito
        /// </summary>
        public string NegritoOff
        {
            get
            {
                return (this.Chr(0x1b) + this.Chr(70));
            }
        }

        /// <summary>
        /// Region Põe o Negrito
        /// </summary>
        public string NegritoOn
        {
            get
            {
                return (this.Chr(0x1b) + this.Chr(0x45));
            }
        }

        /// <summary>
        /// Region tira o Comprimido
        /// </summary>
        public string ComprimidoNormal
        {
            get
            {
                return this.Chr(0x12);
            }
        }
        #endregion

        #region Finaliza a Impressão e Fecha a Porta da Impressora
        /// <summary>
        /// Finaliza a Impressão e Fecha a Porta de Comunicação
        /// </summary>
        public void FinalizaImpressão()
        {
            if (this.lOK)
            {
                this.fileWriter.Close();
                this.outFile.Close();
                CloseHandle(this.hPort);
                this.lOK = false;
            }
        }
        #endregion
    }//fim classe
}//fim namespace
