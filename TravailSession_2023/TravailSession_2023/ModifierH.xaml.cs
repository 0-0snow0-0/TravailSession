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
    public sealed partial class ModifierH : ContentDialog
    {
        double heuresTravailles;
        string numProjet;
        string matricule;

        string erreurMsg;

        public ModifierH()
        {
            this.InitializeComponent();
        }

        public string NumProjet
        {
            set 
            { 
                numProjet = value;
            }
        }

        public string Matricule
        {
            set
            {
                matricule = value;
            }
        }

        public string ErreurMsg
        {
            get => erreurMsg;
            set => erreurMsg = value;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eHeuresT.Visibility = Visibility.Collapsed;
            eHeuresT.Text = "Veuillez entrer un nombre d'heures travaillés sur le projet";

            bool erreur = false;

            if(tbxHeuresT.Text == "")
            {
                erreur = true;
                eHeuresT.Visibility= Visibility.Visible;
                goto End;
            }
            else
            {
                double numHeures;
                if(Double.TryParse(tbxHeuresT.Text, out numHeures))
                {
                    if(numHeures < 0)
                    {
                        erreur = true;
                        eHeuresT.Text = "Veuillez entrer une valeur positive";
                        eHeuresT.Visibility= Visibility.Visible;
                        goto End;
                    } if(numHeures > 10000)
                    {
                        erreur =true;
                        eHeuresT.Text = "Le nombre d'heures travaillés ne doit pas dépasser 10000";
                        eHeuresT.Visibility = Visibility.Visible;
                        goto End;
                    }
                }
                else
                {
                    erreur = true;
                    eHeuresT.Text = "Veuillez entrer une valeur numérique";
                    eHeuresT.Visibility = Visibility.Visible; 
                    goto End;
                }
            }

            End:
            if(!erreur)
            {
                double numHeures = Convert.ToDouble(tbxHeuresT.Text);

                HeuresTravaille ht = new HeuresTravaille(matricule, numProjet, numHeures, 0);

                ErreurMsg = SingletonHeuresTravaille.getInstance().modifierHeuresTravaille(ht);
            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
