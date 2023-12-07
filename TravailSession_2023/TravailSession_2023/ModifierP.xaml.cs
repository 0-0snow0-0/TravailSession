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
        public ModifierP()
        {
            this.InitializeComponent();
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
                tbxNbrEmpRequis.Text = nbrEmprequis.ToString();

                totalSalaire = projet.TotalSalaire;
                tbxTotalSalaire.Text = totalSalaire.ToString();

                client = projet.Client;
                tbxClient.Text = client.ToString();

                statut = projet.Statut;
                cStatut.SelectedValue = statut;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            numProjet = tbxNumProjet.Text;
            titre = tbxTitre.Text;
            //dateDebut = SelectedDate(dtDateDebut).ToString();
            description = tbxDescription.Text;
            budget = int.Parse(tbxBudget.Text);
            nbrEmprequis = int.Parse(tbxNbrEmpRequis.Text);
            totalSalaire = Double.Parse(tbxTotalSalaire.Text);
            client = int.Parse(tbxClient.Text);
            statut = cStatut.SelectedItem.ToString();

        }
    }
}
