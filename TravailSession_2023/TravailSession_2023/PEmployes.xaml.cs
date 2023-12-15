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
    public sealed partial class PEmployes : Page
    {
        public PEmployes()
        {
            this.InitializeComponent();
            gvEmployes.ItemsSource = SingletonEmployes.getInstance().getListeEmployes();
        }

        private void gvEmployes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = gvEmployes.SelectedIndex;
            if (index >= 0)
            {
                this.Frame.Navigate(typeof(ZoomE), index);                
            }
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutE dialog = new AjoutE();
            //dialog.InClient = client;
            dialog.XamlRoot = gEmployes.XamlRoot;
            dialog.Title = "Ajout d'un client";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();
        }
    }
}
