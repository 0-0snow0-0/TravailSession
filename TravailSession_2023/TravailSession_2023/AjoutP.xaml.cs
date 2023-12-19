using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AjoutP : ContentDialog
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

        string erreurMsg;

        public AjoutP()
        {
            this.InitializeComponent();

            listeClients = SingletonClient.getInstance().getListeClients();
            foreach (Client client in listeClients)
            {
                string item = client.Nom;
                cbxClient.Items.Add(item);
            }
        }

        public string ErreurMsg
        {
            get => erreurMsg;
            set => erreurMsg = value;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //eNumP.Visibility = Visibility.Collapsed;
            eTitre.Visibility = Visibility.Collapsed;
            eDateD.Visibility = Visibility.Collapsed;
            eDescription.Visibility = Visibility.Collapsed;
            eBudget.Visibility = Visibility.Collapsed;
            eNbrEmpRequis.Visibility = Visibility.Collapsed;
            //eTotalSalaire.Visibility = Visibility.Collapsed;
            eClient.Visibility = Visibility.Collapsed;
            eStatut.Visibility = Visibility.Collapsed;

            bool erreur = false;

            if (tbxTitre.Text == "")
            {
                erreur = true;
                eTitre.Visibility = Visibility.Visible;
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
                if (iBudget < 0)
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

            if (cbxNbrEmpRequis.SelectedIndex == -1)
            {
                erreur = true;
                eNbrEmpRequis.Visibility = Visibility.Visible;
            }

            /* Pas besoin de mettre car doit calculer le salaire plus tard.
            if(tbxTotalSalaire.Text = "")
            */

            if (cbxClient.SelectedIndex == -1)
            {
                erreur = true;
                eClient.Visibility = Visibility.Visible;
            }

            if (cStatut.SelectedIndex == -1)
            {
                erreur = true;
                cStatut.Visibility = Visibility.Visible;
            }

            if (!erreur)
            {
                numProjet = "";
                titre = tbxTitre.Text;
                dateDebut = Convert.ToDateTime(dtDateDebut.Date.DateTime);
                description = tbxDescription.Text;
                budget = Convert.ToInt32(tbxBudget.Text);
                nbrEmprequis = Convert.ToInt16(cbxNbrEmpRequis.SelectedItem);
                totalSalaire = 0;

                foreach (Client client1 in listeClients)
                {
                    if (client1.Nom == cbxClient.SelectedItem.ToString())
                    {
                        client = client1.Id;
                    }
                }
                statut = cStatut.SelectedItem.ToString();

                Projet projet = new Projet(numProjet, titre, dateDebut, description, budget, nbrEmprequis, totalSalaire, client, statut);

                ErreurMsg = SingletonProjets.getInstance().ajouterProjets(projet);
            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
