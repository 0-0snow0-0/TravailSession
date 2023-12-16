using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomP : Page
    {
        int index = -1;
        public ZoomP()
        {
            this.InitializeComponent();
        }
        Boolean nePasFermer = true;

        Projet projet;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;
                if (index >= 0)
                {

                    projet = SingletonProjets.getInstance().getProjet(index);

                    zNumP.Text = projet.NumProjet;
                    zTitre.Text = projet.Titre;
                    zDateD.Text = projet.DateDebut.ToString();
                    zBudget.Text = projet.Budget.ToString();
                    zDescription.Text = projet.Description;
                    zTotalS.Text = projet.TotalSalaire.ToString();
                    zNbrER.Text = projet.NbrEmpRequis.ToString();
                    zClient.Text = projet.Client.ToString();
                    zStatut.Text = projet.Statut;

                    switch(projet.NbrEmpRequis)
                    {
                        case 5: 
                            emp5.Visibility = Visibility.Visible;
                            goto case 4;
                        case 4:
                            emp4.Visibility = Visibility.Visible;
                            goto case 3;
                        case 3:
                            emp3.Visibility = Visibility.Visible;
                            goto case 2;
                        case 2:
                            emp2.Visibility = Visibility.Visible;
                            goto case 1;
                        case 1:
                            emp1.Visibility = Visibility.Visible;
                            break;
                        default:
                            break;
                    }

                    for(int i = 0; i<projet.NbrEmpRequis; i++)
                    {
                        
                    }

                    if (SingletonAdmin.LoggedIn == false) { Mod.IsEnabled = ConditionToEnableButton(); }
                }

            }

        }        

        //Could move this into the SingletonAdmin
        private bool ConditionToEnableButton()
        {
            return SingletonAdmin.LoggedIn;
        }
        private async void Mod_Click(object sender, RoutedEventArgs e)
        {
            ModifierP dialog = new ModifierP();
            dialog.InProjet = projet;
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Modification d'un projet";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            

            if (resultat == ContentDialogResult.Primary)
            {
                 
                zNumP.Text = "Numero du projet : " + dialog.NumProjet;
                zTitre.Text = "Titre : " + dialog.Titre;
                zDateD.Text = "Matricule : " + dialog.DateDebut;
                zBudget.Text = "Date de Naissance : " + dialog.Budget;
                zDescription.Text = "Courriel : " + dialog.Description;
                zTotalS.Text = "Adresse : " + dialog.TotalSalaire;
                zNbrER.Text = "Date d'embauche : " + dialog.NbrEmpRequis;
                zClient.Text = "Taux x Horaire : " + dialog.Client;
                zStatut.Text = "Statut : " + dialog.Statut;

              
            }

        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            bool sup = false;
            string errorMessage = "";

            ConfirmDialog dialog = new ConfirmDialog();
            dialog.ObjectType = "projet";
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Suppresion du projet" + projet.NumProjet;
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            sup = dialog.Confirm;

            if (sup)
            {
                errorMessage = SingletonProjets.getInstance().supprimerProjet(projet);
            }

            if (errorMessage != "Ok")
            {
                ErrorDialog dialogE = new ErrorDialog();
                dialogE.ErrorMessage = errorMessage;
                dialogE.XamlRoot = gZoomP.XamlRoot;
                dialogE.Title = "Erreur SQL";
                dialogE.PrimaryButtonText = "Ok";
                dialogE.CloseButtonText = "Annuler";
                dialogE.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultE = await dialogE.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(PProjets), "En cours");
            }
        }
    }
}
