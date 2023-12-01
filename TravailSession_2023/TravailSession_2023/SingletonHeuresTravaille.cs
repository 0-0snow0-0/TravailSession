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
    internal class SingletonHeuresTravailles
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=1564431;Pwd=1564431");

        ObservableCollection<HeuresTravaille> listeHeuresTravailles;
        static SingletonHeuresTravailles instance = null;

        public SingletonHeuresTravailles()
        {
            listeHeuresTravailles = new ObservableCollection<HeuresTravaille>();
            reload();
        }

        public static SingletonHeuresTravailles getInstance()
        {
            if (instance == null)
                instance = new SingletonHeuresTravailles();
            return instance;
        }

        public ObservableCollection<HeuresTravaille> getListeHeuresTravailles()
        {
            return listeHeuresTravailles;
        }

        public HeuresTravaille getHeuresTravaille(int index)
        {
            return listeHeuresTravailles[index];
        }

        public void ajouterHeuresTravailles(HeuresTravaille ht)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_heure");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_h", ht.NumProjet);
                commande.Parameters.AddWithValue("matricule_h", ht.Matricule);
                commande.Parameters.AddWithValue("nbrHeures_h", ht.NbrHeures);
                commande.Parameters.AddWithValue("salaireEmploye_h", ht.SalaireEmploye);
                

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

        public void modifierHeuresTravaille(HeuresTravaille ht)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("modification_heure");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_h", ht.NumProjet);
                commande.Parameters.AddWithValue("matricule_h", ht.Matricule);
                commande.Parameters.AddWithValue("nbrHeures_h", ht.NbrHeures);
                

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

        public void supprimerHeuresTravaille(HeuresTravaille ht)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("supprimer_heure");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("numProjet_heure", ht.NumProjet);
                commande.Parameters.AddWithValue("matricule_heure", ht.Matricule);

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
            listeHeuresTravailles.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand("show_all_heures");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    /*Si la conversion ne marche pas. Peut être que les dates doivent êtres convertient en string first*/
                    HeuresTravaille ht = new HeuresTravaille(r["numProjet"].ToString(), r["matricule"].ToString(), Convert.ToDouble(r["nbrHures"]), Convert.ToDouble(r["salaireemploye"]));
                    listeHeuresTravailles.Add(ht);
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
