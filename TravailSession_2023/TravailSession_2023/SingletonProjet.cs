using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    internal class SingletonProjets
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=1564431;Pwd=1564431");

        ObservableCollection<Projet> listeProjets;
        static SingletonProjets instance = null;

        public SingletonProjets()
        {
            listeProjets = new ObservableCollection<Projet>();
            reload();
        }

        public static SingletonProjets getInstance()
        {
            if (instance == null)
                instance = new SingletonProjets();
            return instance;
        }


        public ObservableCollection<Projet> getListeProjets()
        {
            return listeProjets;
        }
        

        public ObservableCollection<Projet> getListeProjetsEnCours()
        {            
            showProjetsEnCours();
            return listeProjets;                       
        }

        public ObservableCollection<Projet> getListeProjetsTermine()
        {
            showProjetsTermine();
            return listeProjets;
        }

        public Projet getProjet(int index)
        {
            return listeProjets[index];
        }

        public void ajouterProjets(Projet projet)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_projet");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_projet", projet.NumProjet);
                commande.Parameters.AddWithValue("titre_projet", projet.Titre);
                commande.Parameters.AddWithValue("dateDebut_projet", projet.DateDebut);
                commande.Parameters.AddWithValue("description_projet", projet.Description);
                commande.Parameters.AddWithValue("budget_projet", projet.Budget);
                commande.Parameters.AddWithValue("nbrEmpRequis_projet", projet.NbrEmpRequis);
                commande.Parameters.AddWithValue("totalSalaire_projet", projet.TotalSalaire);
                commande.Parameters.AddWithValue("client_projet", projet.Client);
                commande.Parameters.AddWithValue("statut_projet", projet.Statut);                

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

        public void modifierProjet(Projet projet)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("modification_projet");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_projet", projet.NumProjet);
                commande.Parameters.AddWithValue("titre_projet", projet.Titre);                
                commande.Parameters.AddWithValue("description_projet", projet.Description);
                commande.Parameters.AddWithValue("budget_projet", projet.Budget);
                commande.Parameters.AddWithValue("nbrEmpRequis_projet", projet.NbrEmpRequis);
                commande.Parameters.AddWithValue("statut_projet", projet.Statut);

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

        public string supprimerProjet(Projet projet)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("supprimer_projet");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_projet", projet.NumProjet);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();
                reload();
                return "Ok";
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
                return ex.Message;
            }
        }
        // Problème de convertion DBNull
        public void showProjetsEnCours()
        {
            listeProjets.Clear();

            try
            {                
                MySqlCommand commande = new MySqlCommand("show_projets_en_cours");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    /*Si la conversion ne marche pas. Peut être que les dates doivent êtres convertient en string first*/
                    Projet projet = new Projet(r["numProjet"].ToString(), r["titre"].ToString(), Convert.ToDateTime(r["dateDebut"]), r["description"].ToString(), Convert.ToInt64(r["budget"]), Convert.ToInt16(r["nbrEmpRequis"]), Convert.ToDouble(r["totalSalaire"]), Convert.ToInt16(r["client"]), r["statut"].ToString());
                    listeProjets.Add(projet);
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

        public void showProjetsTermine()
        {
            listeProjets.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand("show_projets_termine");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    /*Si la conversion ne marche pas. Peut être que les dates doivent êtres convertient en string first*/
                    Projet projet = new Projet(r["numProjet"].ToString(), r["titre"].ToString(), Convert.ToDateTime(r["dateDebut"]), r["description"].ToString(), Convert.ToInt64(r["budget"]), Convert.ToInt16(r["nbrEmpRequis"]), Convert.ToDouble(r["totalSalaire"]), Convert.ToInt16(r["client"]), r["statut"].ToString());
                    listeProjets.Add(projet);
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


        private void reload()
        {
            listeProjets.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand("show_all_projets");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    /*Si la conversion ne marche pas. Peut être que les dates doivent êtres convertient en string first*/
                    Projet projet = new Projet(r["numProjet"].ToString(), r["titre"].ToString(), Convert.ToDateTime(r["dateDebut"]), r["desription"].ToString(), Convert.ToInt16(r["budget"]), Convert.ToInt16(r["nbrEmpRequis"]), Convert.ToDouble(r["totalSalaire"]), Convert.ToInt16(r["client"]), r["statut"].ToString());
                    listeProjets.Add(projet);
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
