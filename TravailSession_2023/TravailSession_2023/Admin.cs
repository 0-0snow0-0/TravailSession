using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    class Admin : INotifyPropertyChanged
    {
        string utilisateur;
        string password;
       

        public Admin(string utilisateur, string password) 
        { 
            this.utilisateur = utilisateur;
            this.password = password;
        }

        public string Utilisateur
        {
            get { return utilisateur; }
            set { utilisateur = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
