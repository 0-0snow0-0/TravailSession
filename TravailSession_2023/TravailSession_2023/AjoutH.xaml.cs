using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
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
    public sealed partial class AjoutH : ContentDialog
    {
        Projet projet;
        ObservableCollection<Employe> listeEmploye;
        ObservableCollection<Employe> listeEmployeProjet;
        ObservableCollection<Projet> listeProjetEnCours;

        public AjoutH()
        {
            this.InitializeComponent();

            listeEmploye = SingletonEmployes.getInstance().getListeEmployes();
            foreach (Employe employe in listeEmploye)
            {
                string item = employe.Prenom + " " + employe.Nom;
                cbxEmploye.Items.Add(item);
            }
        }

        public Projet Projet 
        {
            get => projet;
            set 
            { 
                projet = value;
                listeEmployeProjet = SingletonEmployes.getInstance().getListeEmployesPourProjet(projet);
            } 
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eEmploye.Visibility = Visibility.Collapsed;
            eEmploye.Text = "Veuillez choisir un employé à assigner au projet";

            listeEmploye = SingletonEmployes.getInstance().getListeEmployes(); 
            listeProjetEnCours = SingletonProjets.getInstance().getListeProjetsEnCours();
            

            Employe emp = null;

            bool erreur = false;

            

            if (cbxEmploye.SelectedIndex == -1)
            {
                erreur = true;
                eEmploye.Visibility = Visibility.Visible;
                goto End;
            }
            else
            {
                foreach (Employe employe in listeEmploye)
                {
                    if (cbxEmploye.SelectedItem.ToString() == employe.Prenom + " " + employe.Nom)
                    {
                        emp = employe;
                    }
                }
                listeEmployeProjet = SingletonEmployes.getInstance().getListeEmployesPourProjet(projet);
                foreach (Employe employe in listeEmployeProjet)
                {
                    if (emp.Matricule == employe.Matricule)
                    {
                        erreur = true;
                        eEmploye.Text = "Cet employé est déjà assigné a ce projet";
                        eEmploye.Visibility = Visibility.Visible; 
                        goto End;
                    }
                }
                foreach (Projet projet1 in listeProjetEnCours)
                {
                    if (emp.NumProjet == projet1.NumProjet)
                    {
                        erreur = true;
                        eEmploye.Text = "Cet employé est déjà assigné à un projet qui est encore en cours";
                        eEmploye.Visibility = Visibility.Visible;
                        goto End;
                    }
                }
            }

            End:
            if (!erreur)
            {     
                HeuresTravaille ht = new HeuresTravaille(emp.Matricule, projet.NumProjet, 0, 0);
                SingletonHeuresTravaille.getInstance().ajouterHeuresTravailles(ht);
            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
