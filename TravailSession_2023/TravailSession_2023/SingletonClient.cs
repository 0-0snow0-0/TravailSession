using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession_2023
{
    internal class SingletonClient
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=1564431;Pwd=1564431");

        ObservableCollection<Client> listeClients;
        static SingletonClient instance = null;

        public SingletonClient() 
        { 
            listeClients = new ObservableCollection<Client>();
            reload();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();
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

        public string ajouterClient(Client client) 
        { 
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_client");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id_client", client.Id);
                commande.Parameters.AddWithValue("nom_client", client.Nom);
                commande.Parameters.AddWithValue("adresse_client", client.Adresse);
                commande.Parameters.AddWithValue("numTel_client", client.NumTel);
                commande.Parameters.AddWithValue("email_client", client.Email);

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

        public string modifierClient(Client client)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("modification_client");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id_client", client.Id);
                commande.Parameters.AddWithValue("nom_client", client.Nom);
                commande.Parameters.AddWithValue("adresse_client", client.Adresse);
                commande.Parameters.AddWithValue("numTel_client", client.NumTel);
                commande.Parameters.AddWithValue("email_client", client.Email);

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

        public string supprimerClient(Client client) 
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
