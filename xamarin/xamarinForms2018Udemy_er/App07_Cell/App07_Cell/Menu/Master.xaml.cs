using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App07_Cell.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : MasterDetailPage
	{
		public Master ()
		{
			InitializeComponent ();
		}
        public void GoPagina1(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.TextCellPage());
            IsPresented = false; //it hides the MENU when i click on some button
        }
        public void GoPagina2(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.ImageCellPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
        public void GoPagina3(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.EntryCellPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
        public void GoPagina4(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.SwitchCellPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
        public void GoPagina5(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.ViewCellPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
        public void GoPagina6(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.ListViewPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
        public void GoPagina7(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new Pagina.ListViewButtonPage());
            IsPresented = false;//it hides the MENU when i click on some button
        }
    }
}