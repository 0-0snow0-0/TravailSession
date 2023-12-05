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
        public ModifierC()
        {
            this.InitializeComponent();
        }

        public int Id { get => id; }
        public string Nom { get => nom; }
        public string Adresse { get => adresse; }
        public string NumTel { get => numTel; }
        public string Email { get => email; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            id = int.Parse(tbxId.Text);
            nom = tbxNom.Text;
            adresse = tbxAdresse.Text;
            numTel = tbxNumTel.Text;
            email = tbxEmail.Text;
            
        }
    }
}
