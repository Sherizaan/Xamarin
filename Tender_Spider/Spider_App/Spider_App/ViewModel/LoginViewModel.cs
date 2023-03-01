
using Spider_App.View;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Spider_App.Model;
using Xamarin.Essentials;
using Spider_App.Utils;

namespace Spider_App.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public LoginViewModel()
        {
            LoginButton = new Command(async () => await AuthenticateCredentials());
            LoadCredentials();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string user_name;
        private string pass_word;
        private bool is_busy;
        private bool did_remember;

        public Command LoginButton { get; }
        public string User_Name
        {
            get { return user_name; }
            set { user_name = value; OnPropertyChanged(nameof(User_Name)); }
        }
        public string Pass_Word
        {
            get { return pass_word; }
            set { pass_word = value; OnPropertyChanged(nameof(Pass_Word)); }
        }
        public bool Remember
        {
            get { return did_remember; }
            set { did_remember = value; OnPropertyChanged(nameof(Remember)); }
        }
        public bool Is_Busy
        {
            get { return is_busy; }
            set { is_busy = value; OnPropertyChanged(nameof(Is_Busy)); }
        }
 
        async Task AuthenticateCredentials()
        {
            try
            {
                Is_Busy = true;

                if (CrossConnectivity.Current.IsConnected)
                {
                    var connect = new HttpClient();
                    var uri = new Uri("https://spider.digitalunbounded.com/api/TenderSpiderMobileLoginAuth");

                    var content = new StringContent(JsonConvert.SerializeObject(new { Username = User_Name, Password = Pass_Word }), Encoding.UTF8, "application/json");

                    var response = await connect.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var validation = await response.Content.ReadAsStringAsync();

                        var user = JsonConvert.DeserializeObject<LoginModel>(validation);

                        if (Boolean.Parse(user.HasAccess))
                        {
                            if (Remember)
                            {
                                try
                                {
                                    await SecureStorage.SetAsync("password", Pass_Word);
                                    await SecureStorage.SetAsync("username", User_Name);
                                }
                                catch (Exception sec_stor_ex)
                                {
                                    await App.Current.MainPage.DisplayAlert("", "" + sec_stor_ex, "OK");
                                }
                            }
                            else
                            {
                                try
                                {
                                    await SecureStorage.SetAsync("password", "");
                                    await SecureStorage.SetAsync("username", "");
                                }
                                catch (Exception sec_stor_ex)
                                {
                                    await App.Current.MainPage.DisplayAlert("", "" + sec_stor_ex, "OK");
                                }
                            }
                            Settings.Remember = Remember.ToString();
                            App.ID = Int32.Parse(user.ID);
                            App.DisplayName = user.Name;
                            await App.Current.MainPage.DisplayAlert("Result", "Welcome " + user.Name, "OK");
                            App.Current.MainPage = new NavigationPage(new HomeView());
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "" + user.Error, "OK");
                        }
                    }
                }
                else
                {
                    Is_Busy = false;
                    await App.Current.MainPage.DisplayAlert("Network Error", "Please check your internet connection!", "OK");
                }
            }
            catch (Exception e)
            {
                Is_Busy = false;
                await App.Current.MainPage.DisplayAlert("Error", "" + e.Message, "OK");
            }
            Is_Busy = false;
        }
        async void LoadCredentials()
        {
            if (Settings.Remember != "")
            {
                if (bool.Parse(Settings.Remember))
                {
                    User_Name = await SecureStorage.GetAsync("username");
                    Pass_Word = await SecureStorage.GetAsync("password");
                }
                Remember = bool.Parse(Settings.Remember);
            }
        }
    }
}


    

