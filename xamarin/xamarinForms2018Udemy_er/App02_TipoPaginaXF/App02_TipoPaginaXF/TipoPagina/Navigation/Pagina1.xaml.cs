using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App02_TipoPaginaXF.TipoPagina.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pagina1 : ContentPage
    {
        public Pagina1()
        {
            InitializeComponent();
        }

        private void MudarParaPagina2(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Pagina2()); //pro Navigation funcionar, preciso ter instanciado ele antes, entao fiz isso na Pagina3.xaml.cs
        }

        private void ChamarModal(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new Modal());
        }

        private void ChamarMaster(object sender, EventArgs args)
        {
            //como irei mudar para uma pagina totalmente diferente, redireciono tudo
            App.Current.MainPage = new Master.Master();
        }
    }
}