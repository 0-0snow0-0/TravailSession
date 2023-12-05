using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Security.Cryptography.X509Certificates;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    public sealed partial class ModifierE : ContentDialog
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
        public ModifierE()
        {
            this.InitializeComponent();
        }

        public string Matricule { get => matricule; }
        public string Nom { get => nom; }
        public string Prenom { get => prenom;}
        public DateTime DateNaissance { get => dateNaissance; }
        public string Email { get => email; }
        public string Adresse { get => adresse;}
        public DateTime DateEmbauche { get => dateEmbauche; }
        public double TauxHoraire { get => tauxHoraire; }
        public string Photo_url { get => photo_url; }
        public string Statut { get => statut; }
        public string NumProjet { get => numProjet;}

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            matricule = tbxMatricule.Text;
            nom = tbxNom.Text;
            prenom = tbxPrenom.Text;
            //dateNaissance = dtDateN.Date.ToString("yyyy-MM-dd");
            email = tbxEmail.Text;
            adresse = tbxAdresse.Text;
            //dateEmbauche = new DateTime();
            tauxHoraire = Double.Parse(tbxTauxH.Text);
            photo_url = tbxPhotoUrl.Text;
            statut =  cStatut.SelectedItem.ToString();
            numProjet = tbxNumP.Text;

        }

        
    }
}
