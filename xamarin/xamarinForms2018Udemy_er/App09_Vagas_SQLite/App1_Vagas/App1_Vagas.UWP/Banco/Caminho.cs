using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App1_Vagas.Banco;
using Xamarin.Forms;
using System.IO;
using App1_Vagas.UWP.Banco;
using Windows.Storage; //pra Windows preciso importar esse pacote

[assembly:Dependency(typeof(Caminho))] //preciso declarar pro Dependency aqui...
namespace App1_Vagas.UWP.Banco
{
    //note que essa classe implementa minha interface lá do projeto principal
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            //esse caminho é um ENUMERADOR, então eu consigo pegar as pastas, como vídeos e tal...
            string caminhoDaPasta = ApplicationData.Current.LocalFolder.Path;

            string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

            return caminhoBanco;
        }
    }
}
