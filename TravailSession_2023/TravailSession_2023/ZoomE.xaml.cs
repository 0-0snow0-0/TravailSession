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
                    zDateN.Text = employe.DateNaissance.ToString("dd/MM/yyyy");
                    zEmail.Text = employe.Email;
                    zAdresse.Text = employe.Adresse;
                    zDateE.Text = employe.DateEmbauche.ToString("dd/MM/yyyy");
                    zTauxH.Text = employe.TauxHoraire.ToString() + "$";
                    try
                    {
                        Uri uri = new Uri(employe.Photo_url);
                        zPhotoURL.Source = new BitmapImage(uri);
                    } 
                    catch{ }
                    zStatut.Text = employe.Statut;
                    if (employe.NumProjet.ToString() == "")
                        zNumP.Text = "À assigner sur la page du projet";
                    else
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
            try
            {
                ModifierE dialog = new ModifierE();
                dialog.InEmploye = employe;
                dialog.XamlRoot = gZoomE.XamlRoot;
                dialog.Title = "Modification de l'employe";
                dialog.PrimaryButtonText = "Oui";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultat = await dialog.ShowAsync();

                if (dialog.ErreurMsg != "Ok" && dialog.ErreurMsg != null)
                    throw new Exception(dialog.ErreurMsg);
                else if(resultat.ToString() != "None")
                {
                    ContentDialog dialog1 = new ContentDialog();

                    dialog1.XamlRoot = gZoomE.XamlRoot;
                    dialog1.Title = "Succès";
                    dialog1.Content = "Employé modifié avec succès";
                    dialog1.CloseButtonText = "Ok";
                    

                    ContentDialogResult resultat1 = await dialog1.ShowAsync();
                    this.Frame.Navigate(typeof(ZoomE), index);
                }
            }
            catch
            (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = gZoomE.XamlRoot;
                dialog.Title = "Erreur";
                dialog.Content = ex.Message;
                dialog.CloseButtonText = "Ok";

                ContentDialogResult resultat = await dialog.ShowAsync();
            }
            
            
        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            //Trouvé méthode beaucoup plus logique pour ajouts et modifs, mais cette manière marche aussi, donc ça va rester ça pour les sup
            bool sup = false;
            string errorMessage = "";

            ConfirmDialog dialog = new ConfirmDialog();
            dialog.ObjectType = "employé";
            dialog.XamlRoot = gZoomE.XamlRoot;
            dialog.Title = "Suppresion de l'employé " + employe.Matricule;
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
                if(errorMessage != "")
                {
                    ErrorDialog dialogE = new ErrorDialog();
                    dialogE.ErrorMessage = errorMessage;
                    dialogE.XamlRoot = gZoomE.XamlRoot;
                    dialogE.Title = "Erreur";
                    dialogE.PrimaryButtonText = "Ok";
                    dialogE.CloseButtonText = "Annuler";
                    dialogE.DefaultButton = ContentDialogButton.Primary;

                    ContentDialogResult resultE = await dialogE.ShowAsync();
                }
                
            }
            else
            {
                ContentDialog dialogR = new ContentDialog();
                dialogR.XamlRoot = gZoomE.XamlRoot;
                dialogR.Title = "Succès";
                dialogR.Content = "Employé supprimé avec succès.";
                dialogR.CloseButtonText = "Ok";
                

                ContentDialogResult resultE = await dialogR.ShowAsync();

                this.Frame.Navigate(typeof(PEmployes));
            }

        }
    }
}
