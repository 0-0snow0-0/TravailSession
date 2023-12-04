using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public ZoomE()
        {
            this.InitializeComponent();
        }
        Boolean nePasFermer = true; 
        private async void ModE_Click(object sender, RoutedEventArgs e)
        {

            ModifierE dialog = new ModifierE();
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
                zPrenom.Text = "Prenom : " + dialog.Prenom ;
                zNom.Text = "Nom : " + dialog.Nom ;
                zMatricule.Text = "Matricule : " + dialog.Matricule;
                zDateN.Text = "Date de Naissance : " + dialog.DateNaissance;
                zEmail.Text = "Courriel : " + dialog.Email;
                zAdresse.Text = "Adresse : " + dialog.Adresse;
                zDateE.Text = "Date d'embauche : " + dialog.DateEmbauche;
                zTauxH.Text = "Taux x Horaire : " + dialog.TauxHoraire;
                zPhotoURL.Text = "Photo : " + dialog.Photo_url;
                zStatut.Text = "Statut : " + dialog.Statut;
                zNumP.Text = "Numero de projet : " + dialog.NumProjet;

                //Uri uri = new Uri(listeE[index].Illustration);
               // rImage.Source = new BitmapImage(uri);
            }

           

            
        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            //base - Must implement the OK 

            //SupprimerE dialog = new SupprimerE();
            //dialog.XamlRoot = gZoomE.XamlRoot;
            //dialog.Title = "Suppresion de l'employe";
            //dialog.PrimaryButtonText = "Oui";
            //dialog.CloseButtonText = "Annuler";
            //dialog.DefaultButton = ContentDialogButton.Primary;
            
            //ContentDialogResult resultat = await dialog.ShowAsync();

            //if (resultat == ContentDialogResult.Primary) 
            //{
               //Continue here 
                

            //}

            //var result = await dialog.ShowAsync(); 

        }
    }
}
