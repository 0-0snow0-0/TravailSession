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
using Google.Protobuf.WellKnownTypes;
using System.Security.Cryptography;

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

        private static int countB = 0;
        public string Utilisateur { get => utilisateur; }
        public string Password { get => password; }
        public Boolean FirstBoot { get => firstBoot; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eNU.Visibility = Visibility.Collapsed;
            eMDP.Visibility = Visibility.Collapsed;
            bool erreur = false;

            if (tbxNU.Text == "")
            {
                erreur = true;
                eNU.Visibility = Visibility.Visible;
            }

            if (pwdMDP.Password == "")
            {
                erreur = true;
                eMDP.Visibility = Visibility.Visible;
            }

            if (!erreur)
            {
                utilisateur = tbxNU.Text;
                password = pwdMDP.Password;

                Admin admin = new Admin(utilisateur, password, firstBoot);

                try 
                { 
                    

                    if (countB == 0)
                    {
                        
                        SingletonAdmin.getInstance().activateAdmin(admin);
                        SingletonAdmin.LoggedIn = true;
                        
                    }
                    else
                    {
                        SingletonAdmin.LoggedIn = false;
                        // no good
                        SingletonAdmin.getInstance().validationAdmin(admin);
                        SingletonAdmin.LoggedIn = true;
                    }
                    countB++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    
                    args.Cancel = true; 
                }
            }
            else
            {
                args.Cancel = true;
            }

           
        }
    
    }
}
