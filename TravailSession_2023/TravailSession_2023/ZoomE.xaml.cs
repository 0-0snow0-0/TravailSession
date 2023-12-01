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
    public sealed partial class ZoomE : Page
    {
        public ZoomE()
        {
            this.InitializeComponent();
        }

        private async void ModE_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = gZoomE.XamlRoot;
            dialog.Title = "Modifier l'employe";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "";

            var result = await dialog.ShowAsync();
        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            //base - Must implement the OK 

            ModifierE dialog = new ModifierE();
            dialog.XamlRoot = gZoomE.XamlRoot;
            dialog.Title = "Suppresion de l'employe";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            
            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary) 
            {
                //Continue here 

                TextBlock textBlock = new TextBlock();
            }

            var result = await dialog.ShowAsync(); 

        }
    }
}
