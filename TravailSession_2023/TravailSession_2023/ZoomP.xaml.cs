using Google.Protobuf.WellKnownTypes;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomP : Page
    {
        int index = -1;
        public ZoomP()
        {
            this.InitializeComponent();
        }
        Boolean nePasFermer = true;

        Projet projet;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;

                if (index >= 0)
                {                    
                    projet = SingletonProjets.getInstance().getProjet(index);

                    zNumP.Text = projet.NumProjet;
                    zTitre.Text = projet.Titre;
                    zDateD.Text = projet.DateDebut.ToString();
                    zBudget.Text = projet.Budget.ToString();
                    zDescription.Text = projet.Description;
                    zTotalS.Text = projet.TotalSalaire.ToString();
                    zNbrER.Text = projet.NbrEmpRequis.ToString();
                    zClient.Text = projet.Client.ToString();
                    zStatut.Text = projet.Statut;  

                    ObservableCollection<Employe> listeEmployes = SingletonEmployes.getInstance().getListeEmployesPourProjet(projet);
                    //ObservableCollection<HeuresTravaille> = SingletonHeuresTravaille.getInstance().getListeHeuresTravaillesPourProjet(projet);
                    int lenghtEmp = listeEmployes.Count;

                    switch(projet.NbrEmpRequis)
                    {
                        case 5: 
                            emp5.Visibility = Visibility.Visible; 
                            if(lenghtEmp >= 5)
                            {
                                nomEmp5.Text = listeEmployes[4].Nom;
                                matriculeEmp5.Text = listeEmployes[4].Matricule;

                                HeuresTravaille ht = SingletonHeuresTravaille.getInstance().getHeuresTravaillePourProjet(projet, listeEmployes[4].Matricule);

                                tbHeures5.Text = ht.NbrHeures.ToString();
                                tbSalaire5.Text = ht.SalaireEmploye.ToString() + "$";
                            }
                            else
                            {
                                btnAjouter5.Visibility = Visibility.Visible;
                                btnModifier5.Visibility = Visibility.Collapsed;
                            }
                            goto case 4;
                        case 4:
                            emp4.Visibility = Visibility.Visible;
                            if (lenghtEmp >= 4)
                            {
                                nomEmp4.Text = listeEmployes[3].Nom;
                                matriculeEmp4.Text = listeEmployes[3].Matricule;

                                HeuresTravaille ht = SingletonHeuresTravaille.getInstance().getHeuresTravaillePourProjet(projet, listeEmployes[3].Matricule);

                                tbHeures4.Text = ht.NbrHeures.ToString();
                                tbSalaire4.Text = ht.SalaireEmploye.ToString() + "$";
                            }
                            else
                            {
                                btnAjouter4.Visibility = Visibility.Visible;
                                btnModifier4.Visibility = Visibility.Collapsed;
                            }
                            goto case 3;
                        case 3:
                            emp3.Visibility = Visibility.Visible;
                            if (lenghtEmp >= 3)
                            {
                                nomEmp3.Text = listeEmployes[2].Nom;
                                matriculeEmp3.Text = listeEmployes[2].Matricule;

                                HeuresTravaille ht = SingletonHeuresTravaille.getInstance().getHeuresTravaillePourProjet(projet, listeEmployes[2].Matricule);

                                tbHeures3.Text = ht.NbrHeures.ToString();
                                tbSalaire3.Text = ht.SalaireEmploye.ToString() + "$";
                            }
                            else
                            {
                                btnAjouter3.Visibility = Visibility.Visible;
                                btnModifier3.Visibility = Visibility.Collapsed;
                            }
                            goto case 2;
                        case 2:
                            emp2.Visibility = Visibility.Visible;
                            if (lenghtEmp >= 2)
                            {
                                nomEmp2.Text = listeEmployes[1].Nom;
                                matriculeEmp2.Text = listeEmployes[1].Matricule;

                                HeuresTravaille ht = SingletonHeuresTravaille.getInstance().getHeuresTravaillePourProjet(projet, listeEmployes[1].Matricule);

                                tbHeures2.Text = ht.NbrHeures.ToString();
                                tbSalaire2.Text = ht.SalaireEmploye.ToString() + "$";
                            }
                            else
                            {
                                btnAjouter2.Visibility = Visibility.Visible;
                                btnModifier2.Visibility = Visibility.Collapsed;
                            }
                            goto case 1;
                        case 1:
                            emp1.Visibility = Visibility.Visible;
                            if (lenghtEmp >= 1)
                            {
                                nomEmp1.Text = listeEmployes[0].Nom;
                                matriculeEmp1.Text = listeEmployes[0].Matricule;

                                HeuresTravaille ht = SingletonHeuresTravaille.getInstance().getHeuresTravaillePourProjet(projet, listeEmployes[0].Matricule);

                                tbHeures1.Text = ht.NbrHeures.ToString();
                                tbSalaire1.Text = ht.SalaireEmploye.ToString() + "$";
                            }
                            else
                            {
                                btnAjouter1.Visibility = Visibility.Visible;
                                btnModifier1.Visibility = Visibility.Collapsed;
                            }
                            break;
                        default:
                            break;
                    }

                    for(int i = 0; i<projet.NbrEmpRequis; i++)
                    {
                        
                    }

                    if (SingletonAdmin.getInstance().LoggedIn == false)
                    {
                        Mod.IsEnabled = false;
                        Sup.IsEnabled = false;
                        btnModifier1.IsEnabled = false; btnAjouter1.IsEnabled = false;
                        btnModifier2.IsEnabled = false; btnAjouter2.IsEnabled = false;
                        btnModifier3.IsEnabled = false; btnAjouter3.IsEnabled = false;
                        btnModifier4.IsEnabled = false; btnAjouter4.IsEnabled = false;
                        btnModifier5.IsEnabled = false; btnAjouter5.IsEnabled = false;
                    }
                }

            }

        }   
        
       

        //Could move this into the SingletonAdmin
        private bool ConditionToEnableButton()
        {
            return SingletonAdmin.getInstance().LoggedIn;
        }
        private async void Mod_Click(object sender, RoutedEventArgs e)
        {
            ModifierP dialog = new ModifierP();
            dialog.InProjet = projet;
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Modification d'un projet";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            

            if (resultat == ContentDialogResult.Primary)
            {
                 
                zNumP.Text = "Numero du projet : " + dialog.NumProjet;
                zTitre.Text = "Titre : " + dialog.Titre;
                zDateD.Text = "Matricule : " + dialog.DateDebut;
                zBudget.Text = "Date de Naissance : " + dialog.Budget;
                zDescription.Text = "Courriel : " + dialog.Description;
                zTotalS.Text = "Adresse : " + dialog.TotalSalaire;
                zNbrER.Text = "Date d'embauche : " + dialog.NbrEmpRequis;
                zClient.Text = "Taux x Horaire : " + dialog.Client;
                zStatut.Text = "Statut : " + dialog.Statut;

              
            }

        }

        private async void Sup_Click(object sender, RoutedEventArgs e)
        {
            bool sup = false;
            string errorMessage = "";

            ConfirmDialog dialog = new ConfirmDialog();
            dialog.ObjectType = "projet";
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Suppresion du projet" + projet.NumProjet;
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            sup = dialog.Confirm;

            if (sup)
            {
                errorMessage = SingletonProjets.getInstance().supprimerProjet(projet);
            }

            if (errorMessage != "Ok")
            {
                ErrorDialog dialogE = new ErrorDialog();
                dialogE.ErrorMessage = errorMessage;
                dialogE.XamlRoot = gZoomP.XamlRoot;
                dialogE.Title = "Erreur SQL";
                dialogE.PrimaryButtonText = "Ok";
                dialogE.CloseButtonText = "Annuler";
                dialogE.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultE = await dialogE.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(PProjets), "En cours");
            }
        }

        private async void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            char index1 = ((Button)sender).Name.ElementAt(11); 
            Convert.ToInt32(index1);
            string matricule = null;
            
            switch(index1) 
            {
                case '1':
                    matricule = matriculeEmp1.Text; break;
                case '2':
                    matricule = matriculeEmp2.Text; break;
                case '3':
                    matricule = matriculeEmp3.Text; break;
                case '4':
                    matricule = matriculeEmp4.Text; break;
                case '5':
                    matricule = matriculeEmp5.Text; break;

            }

            ModifierH dialog = new ModifierH();
            dialog.NumProjet = projet.NumProjet;
            dialog.Matricule = matricule;
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Modif des heures travaillés";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult result = await dialog.ShowAsync();
            SingletonProjets.getInstance().reload();
            this.Frame.Navigate(typeof(ZoomP), index);
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutH dialog = new AjoutH();
            dialog.Projet = projet;
            dialog.XamlRoot = gZoomP.XamlRoot;
            dialog.Title = "Ajout employé au projet";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult result = await dialog.ShowAsync();
            SingletonProjets.getInstance().reload();
            this.Frame.Navigate(typeof(ZoomP), index);
        }

        
    }
}
