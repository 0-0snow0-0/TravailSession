using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class ZoomC : Page
    {
        int index = -1;
        public ZoomC()
        {
            this.InitializeComponent();
        }

        Client client;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;
                if (index >= 0)
                {

                    client = SingletonClient.getInstance().getClient(index);

                    zId.Text = client.Id.ToString();
                    zNom.Text = client.Nom;
                    zAdresse.Text = client.Adresse;
                    zNumT.Text = client.NumTel;
                    zEmail.Text = client.Email;

                    if (SingletonAdmin.getInstance().LoggedIn == false) 
                    {
                        ModC.IsEnabled = false;
                        Sup.IsEnabled = false; 
                    }
                    
                }

            }

            

        }

        
        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            bool sup = false;
            string errorMessage = "";

            ConfirmDialog dialog = new ConfirmDialog();
            dialog.ObjectType = "client";
            dialog.XamlRoot = gZoomC.XamlRoot;
            dialog.Title = "Suppresion du client " + client.Id;
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            sup = dialog.Confirm;

            if(sup)
            {
                errorMessage = SingletonClient.getInstance().supprimerClient(client);
            }

            if(errorMessage != "Ok") 
            {
                ErrorDialog dialogE = new ErrorDialog();
                dialogE.ErrorMessage = errorMessage;
                dialogE.XamlRoot = gZoomC.XamlRoot;
                dialogE.Title = "Erreur SQL";
                dialogE.PrimaryButtonText = "Ok";
                dialogE.CloseButtonText = "Annuler";
                dialogE.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultE = await dialogE.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(PClients));
            }
        }

        




        private async void ModC_Click(object sender, RoutedEventArgs e)
        {
            ModifierC dialog = new ModifierC();
            dialog.InClient = client;
            dialog.XamlRoot = gZoomC.XamlRoot;
            dialog.Title = "Modification de l'employe";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

           

            if (resultat == ContentDialogResult.Primary)
            {
                
                zId.Text = "Prenom : " + dialog.Id;
                zNom.Text = "Nom : " + dialog.Nom;
                zAdresse.Text = "Adresse : " + dialog.Adresse;
                zNumT.Text = "" + dialog.NumTel;
                zEmail.Text = "Courriel : " + dialog.Email;
            }
        }
    }
}
