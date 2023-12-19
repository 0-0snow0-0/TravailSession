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
            if (SingletonAdmin.getInstance().LoggedIn == false)
            {
                btnAjouter.IsEnabled = false;
            }
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
            try
            {
                AjoutE dialog = new AjoutE();
                //dialog.InClient = client;
                dialog.XamlRoot = gEmployes.XamlRoot;
                dialog.Title = "Ajout d'un employé";
                dialog.PrimaryButtonText = "Ajouter";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultat = await dialog.ShowAsync();

                if (dialog.ErreurMsg != "Ok" && dialog.ErreurMsg != null)                
                    throw new Exception(dialog.ErreurMsg);                
                else if (resultat.ToString() != "None")
                {
                    ContentDialog dialog1 = new ContentDialog();
                    dialog1.XamlRoot = gEmployes.XamlRoot;
                    dialog1.Title = "Succès";
                    dialog1.Content = "Employé ajouté avec succès.";
                    dialog1.CloseButtonText = "Ok";

                    ContentDialogResult resultat1 = await dialog1.ShowAsync();                    
                }


            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = gEmployes.XamlRoot;
                dialog.Title = "Erreur";
                dialog.Content = ex.Message;
                dialog.CloseButtonText = "Ok";

                ContentDialogResult resultat = await dialog.ShowAsync();
            }
            
        }
    }
}
