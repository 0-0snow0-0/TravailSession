using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            try
            {
                AjoutP dialog = new AjoutP();
                dialog.XamlRoot = gProjets.XamlRoot;
                dialog.Title = "Ajout d'un projet";
                dialog.PrimaryButtonText = "Ajouter";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultat = await dialog.ShowAsync();

                if(resultat.ToString() != "None")
                {
                    ContentDialog dialog1 = new ContentDialog();
                    dialog1.XamlRoot = gProjets.XamlRoot;
                    dialog1.Title = "Succès !";
                    dialog1.Content = "Projet ajouté avec succès";
                    dialog1.CloseButtonText = "Ok";

                    ContentDialogResult resultat1 = await dialog1.ShowAsync();
                    this.Frame.Navigate(typeof(PProjets), "En cours");
                }                

                
            }
            catch (Exception ex) 
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = gProjets.XamlRoot;
                dialog.Title = "Erreur";
                dialog.Content = ex.Message;
                dialog.CloseButtonText = "Ok";

                ContentDialogResult resultat = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(PProjets), "En cours");
            }
            
        }

        private async void btnExporation_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var window = (Application.Current as App)?.m_window as MainWindow;

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Liste_Projets";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            ObservableCollection<Projet> listeProjet;
            listeProjet = SingletonProjets.getInstance().getListeProjets();

            IEnumerable<Projet> obsCollection = listeProjet;
            var list = new List<Projet>(obsCollection);

            try
            {
                if (monFichier != null)
                {
                    var csvLines = list.Select(x => x.ToCSV_String());

                    string csvContent = string.Join(Environment.NewLine, csvLines);
                    await Windows.Storage.FileIO.WriteTextAsync(monFichier, csvContent, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                    //await Windows.Storage.FileIO.WriteLinesAsync(monFichier, list.ConvertAll(x => x.ToCSV_String()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = gProjets.XamlRoot;
                    dialog.Title = "Succès !";
                    dialog.Content = "Exportation faite avec succès";                    
                    dialog.CloseButtonText = "Ok";       

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    this.Frame.Navigate(typeof(PProjets), "En cours");
                }
            }
            catch (Exception ex) 
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = gProjets.XamlRoot;
                dialog.Title = "Erreur";
                dialog.Content = ex.Message;
                dialog.CloseButtonText = "Ok";

                ContentDialogResult resultat = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(PProjets), "En cours");
            }

        }

    }
}
