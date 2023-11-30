using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    internal class SingletonClients
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=1564431;Pwd=1564431");

        ObservableCollection<Client> listeClients;
        static SingletonClients instance = null;

        public SingletonClients() 
        { 
            listeClients = new ObservableCollection<Client>();
            reload();
        }

        public static SingletonClients getInstance()
        {
            if (instance == null)
                instance = new SingletonClients();
            return instance;
        }

        public ObservableCollection<Client> getListeClients() 
        { 
            return listeClients;
        }

        public Client getClient(int index)
        {
            return listeClients[index];
        }

        public void ajouterClient(Client client) 
        { 
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_client");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id", client.Id);
                commande.Parameters.AddWithValue("nom", client.Nom);
                commande.Parameters.AddWithValue("adresse", client.Adresse);
                commande.Parameters.AddWithValue("numTel", client.NumTel);
                commande.Parameters.AddWithValue("email", client.Email);

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

        public void modifierClient(Client client)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("modification_client");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id", client.Id);
                commande.Parameters.AddWithValue("nom", client.Nom);
                commande.Parameters.AddWithValue("adresse", client.Adresse);
                commande.Parameters.AddWithValue("numTel", client.NumTel);
                commande.Parameters.AddWithValue("email", client.Email);

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

        public void supprimerClient(Client client) 
        { 
            try
            {
                MySqlCommand commande = new MySqlCommand("supprimer_client");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id_client", client.Id);

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
            listeClients.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand("show_all_clients");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                
                connection.Open();
                commande.Prepare();                

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    Client client = new Client(Convert.ToInt16(r["id"]), r["nom"].ToString(), r["adresse"].ToString(), r["numTel"].ToString(), r["email"].ToString());
                    listeClients.Add(client);
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
