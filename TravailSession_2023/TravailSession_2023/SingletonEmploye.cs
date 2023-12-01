using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    internal class SingletonEmployes
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=1564431;Pwd=1564431");

        ObservableCollection<Employe> listeEmployes;
        static SingletonEmployes instance = null;

        public SingletonEmployes()
        {
            listeEmployes = new ObservableCollection<Employe>();
            reload();
        }

        public static SingletonEmployes getInstance()
        {
            if (instance == null)
                instance = new SingletonEmployes();
            return instance;
        }

        public ObservableCollection<Employe> getListeEmployes()
        {
            return listeEmployes;
        }

        public Employe getEmploye(int index)
        {
            return listeEmployes[index];
        }

        public void ajouterEmployes(Employe employe)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_employe");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("matricule_e", employe.Matricule);
                commande.Parameters.AddWithValue("nom_e", employe.Nom);
                commande.Parameters.AddWithValue("prenom_e", employe.Prenom);
                commande.Parameters.AddWithValue("dateNaissance_e", employe.DateNaissance);
                commande.Parameters.AddWithValue("email_e", employe.Email);
                commande.Parameters.AddWithValue("adresse_e", employe.Adresse);
                commande.Parameters.AddWithValue("dateEmbauche_e", employe.DateEmbauche);
                commande.Parameters.AddWithValue("tauxHoraire_e", employe.TauxHoraire);
                commande.Parameters.AddWithValue("photo_url_e", employe.Photo_url);
                commande.Parameters.AddWithValue("statut_e", employe.Statut);
                commande.Parameters.AddWithValue("numProjet_e", employe.NumProjet);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();
                reload();
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
            }
        }

        public void modifierEmploye(Employe employe)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("modification_employe");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("matricule_e", employe.Matricule);
                commande.Parameters.AddWithValue("nom_e", employe.Nom);
                commande.Parameters.AddWithValue("prenom_e", employe.Prenom);
                commande.Parameters.AddWithValue("dateNaissance_e", employe.DateNaissance);
                commande.Parameters.AddWithValue("email_e", employe.Email);
                commande.Parameters.AddWithValue("adresse_e", employe.Adresse);
                commande.Parameters.AddWithValue("dateEmbauche_e", employe.DateEmbauche);
                commande.Parameters.AddWithValue("tauxHoraire_e", employe.TauxHoraire);
                commande.Parameters.AddWithValue("photo_url_e", employe.Photo_url);
                commande.Parameters.AddWithValue("statut_e", employe.Statut);
                commande.Parameters.AddWithValue("numProjet_e", employe.NumProjet);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();
                reload();
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
            }
        }

        public void supprimerEmploye(Employe employe)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("supprimer_employe");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("matricule_employe", employe.Matricule);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();
                reload();
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
            }
        }


        private void reload()
        {
            listeEmployes.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand("show_all_employes");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    /*Si la conversion ne marche pas. Peut être que les dates doivent êtres convertient en string first*/
                    Employe employe = new Employe(r["matricule"].ToString(), r["nom"].ToString(), r["prenom"].ToString(), Convert.ToDateTime(r["dateNaissance"]), r["email"].ToString(), r["adresse"].ToString(), Convert.ToDateTime(r["dateEmbauche"]), Convert.ToDouble(r["tauxHoraire"]), r["photo_url"].ToString(), r["statut"].ToString(), r["numProjet"].ToString());
                    listeEmployes.Add(employe);
                }

                r.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
            }
        }
    }
}
