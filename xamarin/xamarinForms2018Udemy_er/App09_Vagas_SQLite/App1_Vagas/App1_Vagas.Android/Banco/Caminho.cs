using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using App1_Vagas.Banco;
using System.IO;
using App1_Vagas.Droid.Banco;

[assembly:Dependency(typeof(Caminho))] //preciso declarar pro Dependency aqui...
namespace App1_Vagas.Droid.Banco
{
    //note que essa classe implementa minha interface lá do projeto principal
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            //esse caminho é um ENUMERADOR, então eu consigo pegar as pastas, como vídeos e tal...
            string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

            return caminhoBanco;
        }
    }
}