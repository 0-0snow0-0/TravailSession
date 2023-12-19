using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomE : Page
    {
        int index = -1;
        public ZoomE()
        {
            this.InitializeComponent();
        }
        Boolean nePasFermer = true;

        Employe employe;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;
                if (index >= 0)
                {

                    employe = SingletonEmployes.getInstance().getEmploye(index);

                    zPrenom.Text = employe.Prenom;
                    zNom.Text = employe.Nom;
                    zMatricule.Text = employe.Matricule.ToString();
                    zDateN.Text = employe.DateNaissance.ToString();
                    zEmail.Text = employe.Email;
                    zAdresse.Text = employe.Adresse;
                    zDateE.Text = employe.DateEmbauche.ToString();
                    zTauxH.Text = employe.TauxHoraire.ToString();
                    zPhotoURL.Text = employe.Photo_url;
                    zStatut.Text = employe.Statut;
                    zNumP.Text = employe.NumProjet.ToString();
                     
                    if (SingletonAdmin.getInstance().LoggedIn == false) 
                    { 
                        ModE.IsEnabled = false; 
                        Sup.IsEnabled = false;
                    }
                }

            }

        }
        
        private async void ModE_Click(object sender, RoutedEventArgs e)
        {

            ModifierE dialog = new ModifierE();
            dialog.InEmploye = employe;
            dialog.XamlRoot = gZoomE.XamlRoot;
            dialog.Title = "Modification de l'employe";            
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            //if (nePasFermer) 
            //{
            //    args.Cancel = true;
            //}
            
            if (resultat == ContentDialogResult.Primary)
            {
                //Continue here 
                employe = SingletonEmployes.getInstance().getEmploye(index);

                zPrenom.Text = employe.Prenom;
                zNom.Text = employe.Nom;
                zMatricule.Text = employe.Matricule.ToString();
                zDateN.Text = employe.DateNaissance.ToString();
                zEmail.Text = employe.Email;
                zAdresse.Text = employe.Adresse;
                zDateE.Text = employe.DateEmbauche.ToString();
                zTauxH.Text = employe.TauxHoraire.ToString();
                zPhotoURL.Text = employe.Photo_url;
                zStatut.Text = employe.Statut;
                zNumP.Text = employe.NumProjet.ToString();

                //Uri uri = new Uri(listeE[index].Illustration);
                // rImage.Source = new BitmapImage(uri);
            }

           

            
        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {

            bool sup = false;
            string errorMessage = "";

            ConfirmDialog dialog = new ConfirmDialog();
            dialog.ObjectType = "employ�";
            dialog.XamlRoot = gZoomE.XamlRoot;
            dialog.Title = "Suppresion de l'employ�" + employe.Matricule;
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            sup = dialog.Confirm;

            if (sup)
            {
                errorMessage = SingletonEmployes.getInstance().supprimerEmploye(employe);
            }

            if (errorMessage != "Ok")
            {
                ErrorDialog dialogE = new ErrorDialog();
                dialogE.ErrorMessage = errorMessage;
                dialogE.XamlRoot = gZoomE.XamlRoot;
                dialogE.Title = "Erreur SQL";
                dialogE.PrimaryButtonText = "Ok";
                dialogE.CloseButtonText = "Annuler";
                dialogE.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultE = await dialogE.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(PEmployes));
            }

        }
    }
}
