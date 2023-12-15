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
using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession_2023
{
    public sealed partial class ModifierP : ContentDialog
    {
        string numProjet;
        string titre;
        DateTime dateDebut;
        string description;
        long budget;
        int nbrEmprequis;
        double totalSalaire;
        int client;
        string statut;
        Projet projet;
        ObservableCollection<Client> listeClients;

        public ModifierP()
        {
            this.InitializeComponent();

            listeClients = SingletonClients.getInstance().getListeClients();
            foreach (Client client in listeClients)
            {
                string item = client.Nom;
                cbxClient.Items.Add(item);
            }            
        }

        public string NumProjet { get => numProjet; }
        public string Titre { get => titre; }
        public DateTime DateDebut { get => dateDebut; }
        public string Description { get => description; }
        public long Budget { get => budget; }
        public int NbrEmpRequis { get => nbrEmprequis; }
        public double TotalSalaire { get => totalSalaire; }
        public int Client { get => client; }
        public string Statut { get => statut; }
        public Projet InProjet 
        {
            get => projet;
            set 
            {
                projet = value;

                numProjet = projet.NumProjet;
                tbxNumProjet.Text = numProjet;
                
                titre = projet.Titre;
                tbxTitre.Text = titre;

                dateDebut = projet.DateDebut;
                dtDateDebut.SelectedDate = dateDebut;

                description = projet.Description;
                tbxDescription.Text = description;

                budget = projet.Budget;
                tbxBudget.Text = budget.ToString();

                nbrEmprequis = projet.NbrEmpRequis;
                string sNumEmpRequis = "";
                for(int i = 0; i<5; i++)
                {
                    if(nbrEmprequis == i)
                    {
                        sNumEmpRequis = i.ToString();
                    }
                }                
                cbxNbrEmpRequis.SelectedItem = sNumEmpRequis;

                totalSalaire = projet.TotalSalaire;
                tbxTotalSalaire.Text = totalSalaire.ToString();


                string sClient = "";
                foreach (Client client in listeClients)
                {
                    if (client.Id.ToString() == cbxClient.SelectedItem.ToString())
                    {
                        sClient = client.Id.ToString();
                    }
                }

                client = projet.Client;
                cbxClient.SelectedItem = sClient;

                statut = projet.Statut;
                cStatut.SelectedValue = statut;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eNumP.Visibility = Visibility.Collapsed;
            eTitre.Visibility = Visibility.Collapsed;
            eDateD.Visibility = Visibility.Collapsed;
            eDescription.Visibility = Visibility.Collapsed;
            eBudget.Visibility = Visibility.Collapsed;
            eNbrEmpRequis.Visibility = Visibility.Collapsed;
            eTotalSalaire.Visibility = Visibility.Collapsed;
            eClient.Visibility = Visibility.Collapsed;
            eStatut.Visibility = Visibility.Collapsed;

            bool erreur = false;

            if(tbxTitre.Text == "")
            {
                erreur = true;
                eTitre.Visibility= Visibility.Visible;
            }

            if (dtDateDebut.SelectedDate == null)
            {
                erreur = true;
                eDateD.Visibility = Visibility.Visible;
            }

            if (tbxDescription.Text == "")
            {
                erreur = true;
                eDescription.Visibility = Visibility.Visible;
            }

            int iBudget;

            if (tbxBudget.Text == "")
            {
                erreur = true;
                eBudget.Visibility = Visibility.Visible;
            }
            else if (Int32.TryParse(tbxBudget.Text, out iBudget))
            {
                if(iBudget > 0) 
                {
                    erreur = true;
                    eBudget.Text = "Veuillez entrer un budget non négatif";
                    eBudget.Visibility = Visibility.Visible;
                }
            }
            else
            {
                erreur = true;
                eBudget.Text = "Veuillez entrer une valeur numérique";
                eBudget.Visibility = Visibility.Visible;
            }

            if(cbxNbrEmpRequis.SelectedIndex == -1) 
            {
                erreur = true;
                eNbrEmpRequis.Visibility = Visibility.Visible;
            }

            /* Pas besoin de mettre car doit calculer le salaire plus tard.
            if(tbxTotalSalaire.Text = "")
            */

            if(cbxClient.SelectedIndex == -1)
            {
                erreur = true;
                eClient.Visibility = Visibility.Visible;
            }

            if(cStatut.SelectedIndex == -1)
            {
                erreur = true;
                cStatut.Visibility = Visibility.Visible;
            }

            /*
            numProjet = tbxNumProjet.Text;
            titre = tbxTitre.Text;
            //dateDebut = SelectedDate(dtDateDebut).ToString();
            description = tbxDescription.Text;
            budget = int.Parse(tbxBudget.Text);
            nbrEmprequis = int.Parse(tbxNbrEmpRequis.Text);
            totalSalaire = Double.Parse(tbxTotalSalaire.Text);
            client = int.Parse(tbxClient.Text);
            statut = cStatut.SelectedItem.ToString();
            */
        }
    }
}
