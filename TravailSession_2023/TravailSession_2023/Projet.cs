using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    public class Projet : INotifyPropertyChanged
    {
        string numProjet;
        string titre;
        DateTime dateDebut;
        string description;
        long budget;
        int nbrEmprequis;
        double totalSalaire;
        int client;
        string statut;

       

        public Projet(string numProjet, string titre, DateTime dateDebut, string description, long budget, int nbrEmpRequis, double totalSalaire, int client, string statut) 
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

        public Projet(string numProjet, string titre)
        {
            this.numProjet=numProjet;
            this.titre=titre;
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

        public string TitreString
        {
            get { return "Nom projet : " + titre; }            
        }

        public DateTime DateDebut
        {
            get { return dateDebut; }
            set {  dateDebut = value; }
        }

        public string DateDebutString
        {
            get { return "Date D. : " + dateDebut.ToString("yyyy/MM/dd"); }            
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public long Budget
        {
            get { return budget; }
            set { Budget = value; }
        }
        public string BudgetString
        {
            get { return "Budget : " + budget + "$"; }           
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
        public string ClientString
        {
            get { return "Num. Client : " + client; }            
        }

        public string Statut
        {
            get { return statut; }
            set {  statut = value; }
        }

        public string ToCSV_String()
        {
            return NumProjet + "," + Titre + "," + DateDebut.ToString("yyyy/MM/dd") + "," + Description + "," + Budget.ToString() + "," + NbrEmpRequis.ToString() + "," + totalSalaire.ToString() + "," + Client.ToString() + "," + Statut;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
