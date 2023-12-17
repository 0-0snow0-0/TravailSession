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
        Boolean firstBoot;

        public Admin(string utilisateur, string password, Boolean firstBoot) 
        { 
            this.utilisateur = utilisateur;
            this.password = password;
            this.firstBoot = firstBoot;
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

        public Boolean FirstBoot 
        { 
            get { return firstBoot; }
            set{ firstBoot = false; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
