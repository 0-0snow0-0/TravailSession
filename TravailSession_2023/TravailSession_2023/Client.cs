using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    public class Client : INotifyPropertyChanged
    {
        int id;
        string nom;
        string adresse;
        string numTel;
        string email;

        public Client(int id, string nom, string adresse, string numTel, string email)
        { 
            this.id = id;
            this.nom = nom;
            this.adresse = adresse;
            this.numTel = numTel;
            this.email = email;
        }

        public int Id
        { 
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string NumTel
        {
            get { return numTel; }
            set { numTel = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
