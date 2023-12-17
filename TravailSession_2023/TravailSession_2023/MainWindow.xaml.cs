using Google.Protobuf.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {   
        public MainWindow()
        {
            this.InitializeComponent();
           
            if (SingletonAdmin.LoggedIn == false) 
            {
                //ShowLoginDialog(); 
                mainFrame.Navigate(typeof(PProjets), "En cours");
            }
            else 
            {
                mainFrame.Navigate(typeof(PProjets), "En cours");
            }
            

        }

        

        

        private async void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = (NavigationViewItem)args.InvokedItemContainer;

            switch (item.Name)
            {
                case "Proj":
                    mainFrame.Navigate(typeof(PProjets), "En cours");
                    break;
                case "Client":
                    mainFrame.Navigate(typeof(PClients));
                    break;
                case "Emp":
                    mainFrame.Navigate(typeof(PEmployes));
                    break;
                case "Login":
                    await ShowLoginDialog();

                    //Connexion dialog = new Connexion();

                    //dialog.XamlRoot = navView.XamlRoot;
                    //dialog.Title = "Connexion";
                    //dialog.PrimaryButtonText = "Continuer";
                    //dialog.CloseButtonText = "Annuler";
                    //dialog.DefaultButton = ContentDialogButton.Primary;


                    //ContentDialogResult resultat = await dialog.ShowAsync();

                    //if (resultat == ContentDialogResult.Primary)
                    //{

                    //   SingletonAdmin.LoggedIn = true;

                    //}


                    break;
                default:
                    mainFrame.Navigate(typeof(PProjets));
                    break;
            }
        }
        private async Task ShowLoginDialog()
        {
            Connexion dialog = new Connexion();
            dialog.XamlRoot = navView.XamlRoot;
            dialog.Title = "Connexion";
            dialog.PrimaryButtonText = "Continuer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            // Handle the result if needed
            // if (resultat == ContentDialogResult.Primary)
            // {
            //     SingletonAdmin.LoggedIn = true;
            // }
        }
        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }


    }
}
