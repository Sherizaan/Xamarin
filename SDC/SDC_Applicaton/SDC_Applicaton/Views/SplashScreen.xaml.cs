using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SDC_Applicaton.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            SplashImageMethod();



        }
        public async void SplashImageMethod()
        {
            await SplashImage.FadeTo(1, 4000);
            Application.Current.MainPage = new LoginPage();
        }
    }
}