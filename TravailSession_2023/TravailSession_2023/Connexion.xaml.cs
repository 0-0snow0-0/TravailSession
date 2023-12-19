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
using System.Threading.Tasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    public sealed partial class Connexion : ContentDialog
    {
        string utilisateur;
        string password;
        bool check;
        
        public Connexion()
        {
            this.InitializeComponent();
           
            
        }

        public bool Check 
        { set { check = value;
                if (check == true)
                { pwdCMDP.Visibility = Visibility.Visible; }
               } 
        }
        
        public string Utilisateur { get => utilisateur; }
        public string Password { get => password; }

        private async  void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            if (check) 
            {
                

                eNU.Visibility = Visibility.Collapsed;
                eMDP.Visibility = Visibility.Collapsed;
                eCMDP.Visibility = Visibility.Collapsed;
                eNU.Text = "Veuillez entrer votre nom d'utilisateur";
                eCMDP.Text = "Veuillez entrer la confirmation mot de passe";
                bool erreur = false;

                if (tbxNU.Text == "")
                {
                    erreur = true;
                    eNU.Visibility = Visibility.Visible;
                }
                else if (tbxNU.Text.Length > 100)
                {
                    erreur = true;
                    eNU.Text = "Entré trop longue, veuillez raccourcir";
                    eNU.Visibility = Visibility.Visible;
                }

                if (pwdMDP.Password == "")
                {
                    erreur = true;
                    eMDP.Visibility = Visibility.Visible;
                }                

                if (pwdCMDP.Password == "")
                {
                    erreur = true;
                    eCMDP.Visibility = Visibility.Visible;
                }
                if(pwdMDP.Password != pwdCMDP.Password)
                {
                    erreur = true;
                    eCMDP.Text = "Les deux mots de passe doivent être identiques";
                    eCMDP.Visibility = Visibility.Visible;
                }

                if (!erreur)
                {
                    utilisateur = tbxNU.Text;
                    password = pwdMDP.Password;

                    try
                    {
                        Admin admin = new Admin(utilisateur, password);
                        SingletonAdmin.getInstance().activateAdmin(admin);
                        
                    }
                    catch (Exception ex)
                    {
                        ErrorDialog dialog = new ErrorDialog();
                        dialog.XamlRoot = stkConnexion.XamlRoot;
                        dialog.ErrorMessage = ex.Message;
                        dialog.Title = "Erreur";
                        dialog.PrimaryButtonText = "Ok";
                        dialog.CloseButtonText = "Ok";

                        var resultat = await dialog.ShowAsync();

                        args.Cancel = true;
                    }
                    
                    
                }
                else
                {
                    args.Cancel = true;
                }

            }
            else {                
                    eNU.Visibility = Visibility.Collapsed;
                    eMDP.Visibility = Visibility.Collapsed;                    
                    eMDP.Text = "Veuillez entrer votre mot de passe";

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

                        try
                        {
                            Admin admin = new Admin(utilisateur, password);
                            SingletonAdmin.getInstance().validationAdmin(admin);
                        }
                        catch (Exception ex)
                        {
                            eMDP.Text = "La connexion à échouer, veuillez réessayer";
                            eMDP.Visibility = Visibility.Visible;
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
}
