using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;
                if (index >= 0)
                {

                    Projet projet = SingletonProjets.getInstance().getProjet(index);

                    zNumP.Text = projet.NumProjet;
                    zTitre.Text = projet.Titre;
                    zDateD.Text = projet.DateDebut.ToString();
                    ZBudget.Text = projet.Budget.ToString();
                    zDescription.Text = projet.Description;
                    zTotalS.Text = projet.TotalSalaire.ToString();
                    zNbrER.Text = projet.NbrEmpRequis.ToString();
                    zClient.Text = projet.Client.ToString();
                    zStatut.Text = projet.Statut;
                }

            }

        }
        private void Mod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
