using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App03_LayoutXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //note: To NavigationPage Works i need to Instance it here
            MainPage = new NavigationPage(new MainPage());
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
