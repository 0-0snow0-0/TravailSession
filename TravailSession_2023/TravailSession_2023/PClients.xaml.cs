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
    public sealed partial class PClients : Page
    {
        public PClients()
        {
            this.InitializeComponent();
            gvClients.ItemsSource = SingletonClients.getInstance().getListeClients();
        }

        private void gvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = gvClients.SelectedIndex;
            if (index >= 0)
            {
                this.Frame.Navigate(typeof(ZoomC), index);
            }
            //NavigationParameters parameters = new NavigationParameters
            //{
            //    LoggedIn = SingletonAdmin.LoggedIn,
            //    SelectedIndex = index
            //};

            //this.Frame.Navigate(typeof(ZoomC), parameters);
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutC dialog = new AjoutC();
            //dialog.InClient = client;
            dialog.XamlRoot = gClients.XamlRoot;
            dialog.Title = "Ajout d'un client";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();
            
        }
    }
}
