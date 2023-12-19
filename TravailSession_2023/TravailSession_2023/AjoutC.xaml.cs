using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
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
    public sealed partial class AjoutC : ContentDialog
    {
        int id;
        string nom;
        string adresse;
        string numTel;
        string email;
        string erreurMsg;
        

        public AjoutC()
        {
            this.InitializeComponent();
        }

        public int Id { get => id; }
        public string Nom { get => nom; }
        public string Adresse { get => adresse; }
        public string NumTel { get => numTel; }
        public string Email { get => email; }

        public string ErreurMsg
        {
            get => erreurMsg;
            set => erreurMsg = value;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eId.Visibility = Visibility.Collapsed;
            eNom.Visibility = Visibility.Collapsed;
            eAdresse.Visibility = Visibility.Collapsed;
            eNumTel.Visibility = Visibility.Collapsed;
            eEmail.Visibility = Visibility.Collapsed;

            bool erreur = false;

            /* Est laissé vide car généré aléatoirement dans BD
            if (tbxId.Text == "")
            {
                erreur = true;
                eId.Visibility = Visibility.Visible;
            }
            */

            if (tbxNom.Text == "")
            {
                erreur = true;
                eNom.Visibility = Visibility.Visible;
            }

            if (tbxAdresse.Text == "")
            {
                erreur = true;
                eAdresse.Visibility = Visibility.Visible;
            }

            if (tbxNumTel.Text == "")
            {
                erreur = true;
                eNumTel.Visibility = Visibility.Visible;
            }

            if (tbxEmail.Text == "")
            {
                erreur = true;
                eEmail.Visibility = Visibility.Visible;
            }

            if (!erreur)
            {
                id = 1;
                nom = tbxNom.Text;
                adresse = tbxAdresse.Text;
                numTel = tbxNumTel.Text;
                email = tbxEmail.Text;

                Client client = new Client(id, nom, adresse, numTel, email);
                ErreurMsg = SingletonClient.getInstance().ajouterClient(client);
            }
            else
            {
                args.Cancel = true;
            }


        }
    }
}
