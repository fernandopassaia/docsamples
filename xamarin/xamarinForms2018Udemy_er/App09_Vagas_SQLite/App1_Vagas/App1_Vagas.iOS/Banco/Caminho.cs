using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using App1_Vagas.Banco;
using Xamarin.Forms;
using System.IO;
using App1_Vagas.iOS.Banco;

[assembly:Dependency(typeof(Caminho))] //preciso declarar pro Dependency aqui...
namespace App1_Vagas.iOS.Banco
{
    //note que essa classe implementa minha interface lá do projeto principal
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            //esse caminho é um ENUMERADOR, então eu consigo pegar as pastas, como vídeos e tal...
            string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //.. eu volto uma pasta, e ai ele vai entrar em Library, no caso do iOS, preciso salvar na Library
            string caminhoDaBiblioteca = Path.Combine(caminhoDaPasta, "..", "Library");

            string caminhoBanco = Path.Combine(caminhoDaBiblioteca, NomeArquivoBanco);

            return caminhoBanco;
        }
    }
}