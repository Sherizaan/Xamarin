using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SDC_App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Animate();
        }
        public async Task Animate()
        {
            imgLogo.Opacity = 0;
            await imgLogo.FadeTo(1, 4000);
            Application.Current.MainPage = new;  
        }
    }
}
