using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    public class Employe : INotifyPropertyChanged
    {        
        string matricule;
        string nom;
        string prenom;
        DateTime dateNaissance;
        string email;
        string adresse;
        DateTime dateEmbauche;
        double tauxHoraire;
        string photo_url;
        string statut;
        string numProjet;


        public Employe(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo_url, string statut, string numProjet)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.adresse = adresse;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo_url = photo_url;
            this.statut = statut;
            this.numProjet = numProjet;
        }

        public string Matricule
        {
            get { return matricule; }
            set { matricule = value; }
        }

        public string Nom
        {
            get { return nom; }
            set {  nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set {  prenom = value; }
        }

        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set {  dateNaissance = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
            set { dateEmbauche = value;}
        }

        public double TauxHoraire
        {
            get { return tauxHoraire; }
            set { tauxHoraire = value; }
        }

        public string Photo_url
        {
            get { return photo_url; }
            set { photo_url = value; }
        }

        public string Statut
        {
            get { return statut; }
            set {  statut = value; }
        }

        public string NumProjet
        {
            get { return numProjet; }
            set {  numProjet = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
