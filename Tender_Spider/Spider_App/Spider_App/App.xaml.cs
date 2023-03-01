using Spider_App.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spider_App
{
    public partial class App : Application
    {
        

        public App()
        {
            InitializeComponent();

            MainPage = new SplashScreen();
        }
        static int id;
        static string displayname;
        public static string DisplayName
        {
            get { return displayname; }
            set { displayname = value; }
        }
        public static int ID
        {
            get { return id; }
            set { id = value; }
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

