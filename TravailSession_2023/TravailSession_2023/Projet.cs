using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    class Projet : INotifyPropertyChanged
    {
        string numProjet;
        string titre;
        DateTime dateDebut;
        string description;
        int budget;
        int nbrEmprequis;
        double totalSalaire;
        int client;
        string statut;

        public Projet(string numProjet, string titre, DateTime dateDebut, string description, int budget, int nbrEmpRequis, double totalSalaire, int client, string statut) 
        { 
            this.numProjet = numProjet;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.budget = budget;
            this.nbrEmprequis = nbrEmpRequis;
            this.totalSalaire = totalSalaire;
            this.client = client;
            this.statut = statut;
        }

        public string NumProjet
        {
            get { return numProjet; }
            set { numProjet = value; }
        }

        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }

        public DateTime DateDebut
        {
            get { return dateDebut; }
            set {  dateDebut = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Budget
        {
            get { return budget; }
            set { Budget = value; }
        }

        public int NbrEmpRequis
        {
            get { return nbrEmprequis; }
            set { nbrEmprequis = value; }
        }

        public double TotalSalaire
        {
            get { return  totalSalaire; }
            set { totalSalaire = value; }
        }

        public int Client
        {
            get { return client; }
            set {  client = value; }
        }

        public string Statut
        {
            get { return statut; }
            set {  statut = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
