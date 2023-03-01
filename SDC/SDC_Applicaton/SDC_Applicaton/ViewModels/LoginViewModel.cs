using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using SDC_Applicaton;
using Plugin.Connectivity;
using Newtonsoft.Json;
using System.ComponentModel;
using SDC_Applicaton.Models;
using SDC_Applicaton.Views;
using SDC_Applicaton.ViewModels;

namespace SDC_Applicaton.ViewModels
{
   
    public class LoginViewModel : INotifyPropertyChanged  
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
        }


        public Action DisplayInvalidLoginPrompt;   
          
        private string _email;  
        public string E_mail
        {  
            get { return _email; }  
            set { _email = value; OnPropertyChanged(nameof(E_mail));
                 
            }  
        }
        private string _password;   
        public string Pass_word
        {  
            get { return _password; }  
            set { _password= value;  
                OnPropertyChanged(nameof(Pass_word));  
            }  
        }  
        public Command LoginButton { get; }  
        public LoginViewModel()  
        {
            LoginButton = new Command(async () => await OnSubmit());  
        }  
        async Task OnSubmit()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected == true)
                {
                    var connect = new HttpClient();
                    var uri = new Uri("https://sdc.digitalunbounded.com/api/SDCMobileLoginAuth");
                    var content = new StringContent(JsonConvert.SerializeObject(new { Username = E_mail, Password = Pass_word }), Encoding.UTF8, "application/json");
                    var response = await connect.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var validation = await response.Content.ReadAsStringAsync();
                        //await Application.Current.MainPage.DisplayAlert("", "" + validation, "OK");
                        var result = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<UserModels>(result);


                        if (user.hasAccess == "True")
                        {
                           // App.UserId = user.id;
                            //App.User_Name_Home = "Hello, " + user.name;
                            await App.Current.MainPage.DisplayAlert("Hello ", "" + user.name, "OK");

                            Application.Current.MainPage = new NavigationPage(new HomePage());

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error ", "" + user.error, "OK");
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("NetworkError", "Please check your connection", "OK");
                }
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Message", "" + e.Message, "OK");
            }
            {  
                 
            }  
        }  
    }  
} 
