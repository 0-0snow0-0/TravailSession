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
    public sealed partial class PProjets : Page
    {
        public PProjets()
        {
            this.InitializeComponent();
            if (SingletonAdmin.getInstance().LoggedIn == false)
            {
                btnAjouter.IsEnabled = false;   
            }
            
            gvProjets.ItemsSource = SingletonProjets.getInstance().getListeProjetsEnCours();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter.ToString() == "En cours")
            {
                gvProjets.ItemsSource = SingletonProjets.getInstance().getListeProjetsEnCours();
                tbxTitre.Text = "Projets en cours";
            }

            if (e.Parameter.ToString() == "Terminé")
            {
                gvProjets.ItemsSource = SingletonProjets.getInstance().getListeProjetsTermine();
                tbxTitre.Text = "Projets terminés";
            }
        }

        private void gvProjets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = gvProjets.SelectedIndex;
            if (index >= 0)
            {
                this.Frame.Navigate(typeof(ZoomP), index);
            }
        }

        private void btnEnCours_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PProjets), "En cours");
        }

        private void btnTermine_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PProjets), "Terminé");
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutP dialog = new AjoutP();            
            dialog.XamlRoot = gProjets.XamlRoot;
            dialog.Title = "Ajout d'un projet";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            this.Frame.Navigate(typeof(PProjets), "En cours");
        }
    }
}
