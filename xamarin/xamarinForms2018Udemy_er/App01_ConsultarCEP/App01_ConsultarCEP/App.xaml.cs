using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App01_ConsultarCEP
{
    public partial class App : Application //Application is the principal structure on XAML File
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
