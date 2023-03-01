

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spider_App.View
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
            await SplashImage.FadeTo(1, 2000);
            Application.Current.MainPage = new LoginView();
        }
    }
}