using System;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Threading;

namespace CSOBRF_Util.Grafico
{
    public class Imagem
    {      
        #region Método Para gerar a Imagem em formato (GIF) Danfe
        /// <summary>
        /// Gera uma nova imagem através de um arquivo, e grava o destino com o novo tamanho
        /// </summary>
        /// <param name="larguraMaxima">Largura em Pixels (exemplo: 100 para 100px)</param>
        /// <param name="alturaMaxima">Altura em Pixels (exemplo: 100 para 100px)</param>
        /// <param name="arquivoOriginal">Caminho do original (ex: c:\imagem.jpg)</param>
        /// <param name="arquivoFinal">Caminho do Final (ex: c:\imagemFinal.jpg)</param>
        /// <returns>Retorna True se tudo der OK, False se não</returns>
        public bool gerarNovaImagemDanfe(int larguraMaxima, int alturaMaxima, string arquivoOriginal, string arquivoFinal)
        {
            try
            {
                Image imagemOriginal;
                imagemOriginal = Image.FromFile(arquivoOriginal);
                Bitmap backGround = new Bitmap(arquivoOriginal);
                Bitmap baseMap = new Bitmap(160, 160, PixelFormat.Format24bppRgb);
                Graphics graph = Graphics.FromImage(baseMap);

                graph.SmoothingMode = SmoothingMode.AntiAlias;
                graph.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), 0, 0, 160, 160);
                graph.DrawImage(imagemOriginal, new Rectangle(0, 0, 160, 160));
                graph.Dispose();

                baseMap.MakeTransparent(baseMap.GetPixel(0, 0));

                /* Grava a Imagem */
                baseMap.Save(arquivoFinal, ImageFormat.Gif);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        
        #region Método "Ajuste do Novo Tamanho da Imagem"
        //Define o melhor tamanho para a nova imagem, de acordo com a
        //proporção entre largura e altura da imagem original
        private Size novoTamanho(Size Original, Size Nova)
        {

            decimal proporcaoOriginal = ((Original.Height * 100) / Original.Width) / 100;
            Size tamanho = new Size(); ;
            if (proporcaoOriginal > 1)
            {

                proporcaoOriginal = ((Original.Width * 100) / Original.Height) / 100;

                tamanho.Height = Nova.Height;

                tamanho.Width = Convert.ToInt32(Nova.Height * proporcaoOriginal);

                return tamanho;
            }
            else
            {

                tamanho.Width = Nova.Width;

                tamanho.Height = Convert.ToInt32(Nova.Width * proporcaoOriginal);
                return tamanho;
            }
            /*
            If Response.Write("<BR><BR><b>Tamanho original:</b>  " & Original.Width & "x" & Original.Height & "<BR>")

            Response.Write("<b>Novo Tamanho:</b> " & NovoTamanho.Width & "x" & NovoTamanho.Height & "<BR>")
            */
        }
        #endregion        

        #region Gera Nova Imagem
        /// <summary>        
        ///A imagem original tem 1024x640 e 539KB. Todas as imagens serão reduzidas para 800x500...
        ///A primeira compactação foi 1 (qualidade baixa) com 10% de qualidade: Imagem de saída tem 14KB.
        ///A segunda compactação foi 1 (qualidade baixa) com 40% de qualidade: Imagem de saída tem 34KB.
        ///A terceira compactação foi 1 (qualidade baixa) com 70% de qualidade: Imagem de saída tem 54KB.
        ///A quarta compactação foi 1 (qualidade baixa) com 100% de qualidade: Imagem de saída tem 299KB.
        ///A quinta compactação foi 2 (qualidade alta) com 10% de qualidade: Imagem de saída tem 15KB.
        ///A sexta compactação foi 2 (qualidade alta) com 40% de qualidade: Imagem de saída tem 36KB.
        ///A setima compactação foi 2 (qualidade alta) com 70% de qualidade: Imagem de saída tem 57KB.
        ///A oitava compactação foi 2 (qualidade alta) com 100% de qualidade: Imagem de saída tem 310KB.
        /// </summary>
        /// <param name="larguraMaxima">Largura da Imagem Nova</param>
        /// <param name="alturaMaxima">Altura da Imagem Nova</param>
        /// <param name="arquivoOriginal">Arquivo original que será transformado</param>        
        /// <param name="qualidade">Qualidade - 1 (Baixa) / 2 (Alta)</param>
        /// <param name="nivelQualidade">Define a porcentagem de compactação que será aplicada sobre a imagem - 0 mais baixa até 100 mais alta</param>
        /// <param name="excluirArquivoOriginal">Se enviar True ele irá excluir o Arquivo Original (enviado por parametro) e irá excluir o -comp1 (primeira compactação)</param>
        /// <returns>Retorna o nome do arquivo final - que será o mesmo do arquivoFinal mas acrescido de '-comp2.jpg' ao invés de apenas .jpg. Retorna "ERRO" se ocorrer excessão</returns>
        public string gerarNovaImagem(int larguraMaxima, int alturaMaxima, string arquivoOriginal, int qualidade, int nivelQualidade, bool excluirArquivoOriginal)
        {
            string arquivoFinal1 = arquivoOriginal.Replace(".jpg", "-comp1.jpg");
            string arquivoFinal2 = arquivoOriginal.Replace(".jpg", "-comp2.jpg");
            try
            {
                try
                {
                    if (File.Exists(arquivoFinal1) == true)
                    {
                        File.Delete(arquivoFinal1);
                    }
                }
                catch
                {
                    if (File.Exists(arquivoFinal2) == true)
                    {
                        File.Delete(arquivoFinal2);
                    }
                }

                //Analise e Dados técnicos de conversão feitos por essa classe - Fernando 102014... Eu enviei uma imagem em 600x400.
                //As imagens aqui descritas foram salvas dentro do Namespace Grafico na Pasta Grafico dentro dessa DLL.
                
                //A imagem original tem 1024x640 e 539KB. Todas as imagens serão reduzidas para 800x500...
                //A primeira compactação foi 1 (qualidade baixa) com 10% de qualidade: Imagem de saída tem 14KB.
                //A segunda compactação foi 1 (qualidade baixa) com 40% de qualidade: Imagem de saída tem 34KB.
                //A terceira compactação foi 1 (qualidade baixa) com 70% de qualidade: Imagem de saída tem 54KB.
                //A quarta compactação foi 1 (qualidade baixa) com 100% de qualidade: Imagem de saída tem 299KB.
                //A quinta compactação foi 2 (qualidade alta) com 10% de qualidade: Imagem de saída tem 15KB.
                //A sexta compactação foi 2 (qualidade alta) com 40% de qualidade: Imagem de saída tem 36KB.
                //A setima compactação foi 2 (qualidade alta) com 70% de qualidade: Imagem de saída tem 57KB.
                //A oitava compactação foi 2 (qualidade alta) com 100% de qualidade: Imagem de saída tem 310KB.

                #region Altera a resolução da imagem
                Image imagemOriginal;
                Image imagemAlterada;

                Graphics grafico;       //Gráfico
                Size dimensaoFinal = new Size(); //Dimensão
                ImageFormat formato;    //Formato

                imagemOriginal = Image.FromFile(arquivoOriginal);
                dimensaoFinal = this.novoTamanho(imagemOriginal.Size, new Size(larguraMaxima, alturaMaxima));
                dimensaoFinal.Height = alturaMaxima; //precisei FORÇAR por que tava dando problema...
                dimensaoFinal.Width = larguraMaxima;

                //Define o novo formato da imagem
                formato = imagemOriginal.RawFormat;

                //Cria a nova imagem
                if (dimensaoFinal.Width == 0)
                {
                    dimensaoFinal.Width = 150;
                }

                if (dimensaoFinal.Height == 0)
                {
                    dimensaoFinal.Height = 150;
                }
                imagemAlterada = new Bitmap(dimensaoFinal.Width, dimensaoFinal.Height);

                grafico = Graphics.FromImage(imagemAlterada);

                //Opções relativas à qualidade da nova imagem
                if(qualidade == 1)
                {
                    grafico.CompositingQuality = CompositingQuality.HighSpeed;
                    grafico.SmoothingMode = SmoothingMode.HighSpeed;
                    grafico.InterpolationMode = InterpolationMode.Low;
                }
                if(qualidade == 2)
                {
                    grafico.CompositingQuality = CompositingQuality.HighQuality;
                    grafico.SmoothingMode = SmoothingMode.HighQuality;
                    grafico.InterpolationMode = InterpolationMode.High;
                }         

                //Desenha a imagem no objeto gráfico this.grafico
                grafico.DrawImage(imagemOriginal, new Rectangle(0, 0, dimensaoFinal.Width, dimensaoFinal.Height));
                
                imagemAlterada.Save(arquivoFinal1);

                //inicia a compactação da imagem
                Image myImage = Image.FromFile(arquivoFinal1);
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, nivelQualidade); 
                // Jpeg image codec 
                ImageCodecInfo   jpegCodec = GetEncoderInfo("image/jpeg"); 

                EncoderParameters encoderParams = new EncoderParameters(1); 
                encoderParams.Param[0] = qualityParam; 
                Thread.Sleep(500);

                myImage.Save(arquivoFinal2, jpegCodec, encoderParams);


                //tenta excluir o arquivoFinal1 (que é da primeira compactação) e o arquivo final (original) que foi enviado
                if(excluirArquivoOriginal)
                {
                    arquivoFinal1 = null;
                    //arquivoFinal2 = null;
                    imagemOriginal = null;
                    imagemAlterada = null;
                    grafico = null;
                    
                    formato = null;
                    myImage = null;
                    qualityParam = null;
                    jpegCodec = null;
                    encoderParams = null;
                    try
                    {
                        File.Delete(arquivoFinal1);
                    }
                    catch
                    {

                    }
                    try
                    {
                        File.Delete(arquivoOriginal);
                    }
                    catch
                    {

                    }
                }


                return arquivoFinal2;
                #endregion                                
            }
            catch
            {
                return "ERRO";
            }
            finally
            {
                arquivoFinal1 = "";
                arquivoFinal2 = "";
            }
        }
        #endregion

        #region GetCodecInfo
        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        } 
        #endregion
    }//fim classe
}//fim namespace
