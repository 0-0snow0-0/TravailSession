using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutE : ContentDialog
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
        Employe employe;
        ObservableCollection<Projet> listeProjetsEnCours;

        public AjoutE()
        {
            this.InitializeComponent();

            dtDateE.Date = DateTime.Now;            

            listeProjetsEnCours = SingletonProjets.getInstance().getListeProjetsEnCours();
            foreach (Projet projet in listeProjetsEnCours)
            {
                string item = "Projet : " + projet.Titre + " - " + projet.NumProjet;
                cNumP.Items.Add(item);
            }

            listeProjetsEnCours = SingletonProjets.getInstance().getListeProjets();
        }

        public string Matricule { get => matricule; }
        public string Nom { get => nom; }
        public string Prenom { get => prenom; }
        public DateTime DateNaissance { get => dateNaissance; }
        public string Email { get => email; }
        public string Adresse { get => adresse; }
        public DateTime DateEmbauche { get => dateEmbauche; }
        public double TauxHoraire { get => tauxHoraire; }
        public string Photo_url { get => photo_url; }
        public string Statut { get => statut; }
        public string NumProjet { get => numProjet; }
        
       
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ePrenom.Visibility = Visibility.Collapsed;
            eNom.Visibility = Visibility.Collapsed;
            eDateN.Visibility = Visibility.Collapsed;
            eEmail.Visibility = Visibility.Collapsed;
            eAdresse.Visibility = Visibility.Collapsed;
            eDateE.Visibility = Visibility.Collapsed;
            eTauxH.Visibility = Visibility.Collapsed;
            ePhotoUrl.Visibility = Visibility.Collapsed;
            eStatut.Visibility = Visibility.Collapsed;
            eNumP.Visibility = Visibility.Collapsed;

            eDateN.Text = "Veuillez choisir un date";
            eTauxH.Text = "Veuillez entrer un taux horaire";
            eDateE.Text = "Veuillez choisir un date";

            bool erreur = false;

            if (tbxPrenom.Text == "")
            {
                erreur = true;
                ePrenom.Visibility = Visibility.Visible;
            }

            if (tbxNom.Text == "")
            {
                erreur = true;
                eNom.Visibility = Visibility.Visible;
            }                     

            if (dtDateN.SelectedDate == null)
            {
                erreur = true;
                eDateN.Visibility = Visibility.Visible;
            }
            else if (dtDateE.SelectedDate > DateTime.Now.AddYears(-18))
            {
                erreur = true;
                eDateN.Text = "L'employé ne peux pas avoir en dessous de 18 ans";
                eDateN.Visibility = Visibility.Visible;
            }

            if (tbxEmail.Text == "")
            {
                erreur = true;
                eEmail.Visibility = Visibility.Visible;
            }

            if (tbxAdresse.Text == "")
            {
                erreur = true;
                eAdresse.Visibility = Visibility.Visible;
            }

            if (dtDateE.SelectedDate == null)
            {
                erreur = true;
                eDateE.Visibility = Visibility.Visible;
            }
            else if (dtDateN.SelectedDate > DateTime.Now)
            {
                erreur = true;
                eDateE.Text = "L'employé ne peux pas être engagé pour une date futur";
                eDateE.Visibility = Visibility.Visible;
            }

            double d_tauxH;

            if (tbxTauxH.Text == "")
            {
                erreur = true;
                eTauxH.Visibility = Visibility.Visible;
            }
            else if (double.TryParse(tbxTauxH.Text, out d_tauxH))
            {
                if (d_tauxH < 15)
                {
                    erreur = true;
                    eTauxH.Text = "Le taux horaire minimum est de 15$";
                    eTauxH.Visibility = Visibility.Visible;
                }
            }

            if (tbxPhotoUrl.Text == "")
            {
                erreur = true;
                ePhotoUrl.Visibility = Visibility.Visible;
            }

            if (cStatut.SelectedIndex <= -1)
            {
                erreur = true;
                eStatut.Visibility = Visibility.Visible;
            }

            if (cNumP.SelectedIndex <= -1)
            {
                erreur = true;
                eNumP.Visibility = Visibility.Visible;
            }

            if (!erreur)
            {
                matricule = tbxMatricule.Text;
                nom = tbxNom.Text;
                prenom = tbxPrenom.Text;
                dateNaissance = Convert.ToDateTime(dtDateN.Date.DateTime);//dateNaissance = dtDateN.Date.ToString("yyyy-MM-dd");
                email = tbxEmail.Text;
                adresse = tbxAdresse.Text;
                dateEmbauche = Convert.ToDateTime(dtDateE.Date.DateTime);//dateEmbauche = new DateTime();
                tauxHoraire = double.Parse(tbxTauxH.Text);
                photo_url = tbxPhotoUrl.Text;
                statut = cStatut.SelectedItem.ToString();


                foreach (Projet projet in listeProjetsEnCours)
                {
                    if (projet.NumProjet == cNumP.SelectedItem.ToString())
                    {
                        numProjet = projet.NumProjet;
                    }
                }

                employe = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo_url, statut, numProjet);

                SingletonEmployes.getInstance().modifierEmploye(employe);
            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
