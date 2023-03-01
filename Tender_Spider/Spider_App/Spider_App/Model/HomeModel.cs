using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Spider_App.Model
{
    class HomeModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string search;
        public string tenders;
        public string preferences;
        public string dashboard;

        public string Search
        {
            get { return search; }
            set { search = value; OnPropertyChanged(nameof(Search)); }
        }
        public string Tenders
        {
            get { return tenders; }
            set { tenders = value; OnPropertyChanged(nameof(Tenders)); }
        }
        public string Preferences
        {
            get { return preferences; }
            set { preferences = value; OnPropertyChanged(nameof(Preferences)); }
        }
        public string Dashboard
        {
            get { return dashboard; }
            set { dashboard = value; OnPropertyChanged(nameof(Dashboard)); }
        }
    }
}