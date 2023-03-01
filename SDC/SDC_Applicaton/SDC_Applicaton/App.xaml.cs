using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SDC_Applicaton.Views;

namespace SDC_Applicaton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SplashScreen();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
