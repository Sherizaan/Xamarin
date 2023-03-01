using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Spider_App.Model;
using Spider_App.View;

namespace Spider_App.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            UpdateDisplay();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string welcomemessage;
        private string tenders;
        private string search;
        private string preferences;
        private string dashboard;
        private bool is_busy;
        private HttpContent content;

        public string WelcomeMessage
        {
            get { return welcomemessage; }
            set { welcomemessage = value; OnPropertyChanged(nameof(WelcomeMessage)); }
        }
        public string Tenders
        {
            get { return tenders; }
            set { tenders = value; OnPropertyChanged(nameof(Tenders)); }
        }
        public string Search
        {
            get { return search; }
            set { search = value; OnPropertyChanged(nameof(Search)); }
        }
        public string Dashboard
        {
            get { return dashboard; }
            set { dashboard = value; OnPropertyChanged(nameof(Dashboard)); }
        }
        public string Preferences
        {
            get { return preferences; }
            set { preferences = value; OnPropertyChanged(nameof(Preferences)); }
        }
        public bool Is_Busy
        {
            get { return is_busy; }
            set { is_busy = value; OnPropertyChanged(nameof(Is_Busy)); }
        }

        void UpdateDisplay()
        {
            WelcomeMessage = "Welcome " + App.DisplayName;
            
        }
        
    }
}
