using Google.Protobuf.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            mainFrame.Navigate(typeof(PProjets), "En cours");
            
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool admin = false;
            admin = SingletonAdmin.getInstance().checkAdmin();
            await ShowLoginDialog(admin);
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
                    if (SingletonAdmin.getInstance().LoggedIn == true)
                    {
                        PerformLogin();
                    }
                    else 
                    {
                        await ShowLoginDialog(false);
                    }
                    

                    break;
                default:
                    mainFrame.Navigate(typeof(PProjets));
                    break;
            }
        }
        private async Task ShowLoginDialog(bool admin)
        {
            Connexion dialog = new Connexion();
            dialog.XamlRoot = navView.XamlRoot;
            dialog.Check = admin;
            dialog.Title = "Connexion";
            dialog.PrimaryButtonText = "Continuer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary && SingletonAdmin.getInstance().LoggedIn == true)
            {
                PerformLogout();
                
            }
            
        }
        
        private void PerformLogin()
        {
            ShowLogoutMessageDialog();

            Login.Content = "Connexion";

            Windows.UI.Color color = Windows.UI.Color.FromArgb(0xFF, 0xB0, 0xB9, 0xA6);
            SolidColorBrush brush = new SolidColorBrush(color);

            Login.Background = brush;

           
        }
        private void PerformLogout() 
        {
            ShowLoginMessageDialog();
            Login.Content = "Déconnexion";

            Windows.UI.Color color = Windows.UI.Color.FromArgb(0xFF, 0x84, 0x3C, 0x41);
            SolidColorBrush brush = new SolidColorBrush(color);
            Login.Background = brush;

        }
        private async void ShowLogoutMessageDialog()
        {
            ContentDialog logoutDialog = new ContentDialog();
            logoutDialog.XamlRoot = navView.XamlRoot;
            logoutDialog.Title = "Déconnexion réussie";
            logoutDialog.CloseButtonText = "OK";
            logoutDialog.Content = "Vous êtes maintenant déconnecté.";

            var resultat = await logoutDialog.ShowAsync();
            
            SingletonAdmin.getInstance().LoggedIn = false;
            this.mainFrame.Navigate(typeof(PProjets), "En cours");
        }

        private async void ShowLoginMessageDialog()
        {
            ContentDialog loginDialog = new ContentDialog();
            loginDialog.XamlRoot = navView.XamlRoot;
            loginDialog.Title = "Connexion réussie";
            loginDialog.CloseButtonText = "OK";
            loginDialog.Content = "Vous êtes maintenant connecté.";

            var resultat = await loginDialog.ShowAsync();

            SingletonAdmin.getInstance().LoggedIn = true;
            this.mainFrame.Navigate(typeof(PProjets), "En cours");
        }
        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }
    }
}
