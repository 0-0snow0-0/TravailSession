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
using Microsoft.WindowsAppSDK.Runtime.Packages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    public sealed partial class Connexion : ContentDialog
    {
        string utilisateur;
        string password;
        Boolean firstBoot;
        public Connexion()
        {
            this.InitializeComponent();
        }

        public string Utilisateur { get => utilisateur; }
        public string Password { get => password; }
        public Boolean FirstBoot { get => firstBoot; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            utilisateur = tbxNU.Text;
            password = pwdMDP.Password;

            Admin admin = new Admin(utilisateur, password, firstBoot);
            SingletonAdmin.LoggedIn = true;
            SingletonAdmin.getInstance().activateAdmin(admin);
        }
    
    }
}
