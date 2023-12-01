using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    class HeuresTravaille : INotifyPropertyChanged
    {
        string matricule;
        string numProjet;
        double nbrHeures;
        double salaireEmploye;

        public HeuresTravaille(string matricule, string numProjet, double nbrHeures, double salaireEmploye)
        {
            this.matricule = matricule;
            this.numProjet = numProjet;
            this.nbrHeures = nbrHeures;
            this.salaireEmploye = salaireEmploye;
        }

        public string Matricule
        { 
            get { return matricule; } 
            set { matricule = value; }
        }

        public string NumProjet
        {
            get { return numProjet; }
            set {  numProjet = value; }
        }

        public double NbrHeures
        {
            get { return nbrHeures; }
            set {  nbrHeures = value; }
        }

        public double SalaireEmploye
        {
            get { return salaireEmploye; }
            set { salaireEmploye = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
