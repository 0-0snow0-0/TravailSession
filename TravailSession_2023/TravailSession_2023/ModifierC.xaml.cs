using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    public sealed partial class ModifierC : ContentDialog
    {
        int id;
        string nom;
        string adresse;
        string numTel;
        string email;

        Client client;

        public ModifierC()
        {
            this.InitializeComponent();
        }

        public int Id { get => id; }
        public string Nom { get => nom; }
        public string Adresse { get => adresse; }
        public string NumTel { get => numTel; }
        public string Email { get => email; }

        public Client InClient
        {
            get => client;
            set
            {
                client = value;

                id = client.Id;
                tbxId.Text = id.ToString();
                tbxId.IsReadOnly = true;

                nom = client.Nom;
                tbxNom.Text = nom;

                adresse = client.Adresse;
                tbxAdresse.Text = adresse;

                numTel = client.NumTel;
                tbxNumTel.Text = numTel;

                email = client.Email;
                tbxEmail.Text = email;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eId.Visibility = Visibility.Collapsed;
            eNom.Visibility = Visibility.Collapsed;
            eAdresse.Visibility = Visibility.Collapsed;
            eNumTel.Visibility = Visibility.Collapsed;
            eEmail.Visibility = Visibility.Collapsed;

            bool erreur = false;

            if(tbxId.Text == "")
            {
                erreur = true;
                eId.Visibility = Visibility.Visible;
            }

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

            if(!erreur)
            {
                id = int.Parse(tbxId.Text);
                nom = tbxNom.Text;
                adresse = tbxAdresse.Text;
                numTel = tbxNumTel.Text;
                email = tbxEmail.Text;
            }
            else
            {
                args.Cancel = true;
            }
            
            
        }
    }
}
