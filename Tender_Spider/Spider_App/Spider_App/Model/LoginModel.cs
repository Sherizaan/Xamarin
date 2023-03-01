using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Spider_App.Model
{
    class LoginModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string id;
        public string displayname;
        public string hasaccess;
        public string message;

        public string ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(ID)); }
        }
        public string Name
        {
            get { return displayname; }
            set { displayname = value; OnPropertyChanged(nameof(Name)); }
        }
        public string HasAccess
        {
            get { return hasaccess; }
            set { hasaccess = value; OnPropertyChanged(nameof(HasAccess)); }
        }
        
        public string Error
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Error)); }
        }
    }
}