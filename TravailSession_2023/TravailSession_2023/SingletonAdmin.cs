using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace TravailSession_2023
{
    internal class SingletonAdmin
    {
        MySqlConnection connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq7;Uid=2087801;Pwd=2087801");

        ObservableCollection<Admin> User;
        static SingletonAdmin instance = null;
        public SingletonAdmin()
        {
            User = new ObservableCollection<Admin>();

        }
        public static SingletonAdmin getInstance()
        {
            if (instance == null)
                instance = new SingletonAdmin();
            return instance;
        }

        public bool LoggedIn //change back and only use it in the singleton
        { get; set; } = false;

        public ObservableCollection<Admin> getAdmin()
        {
            return User;
        }

        public Admin getAdmin(int index)
        {
            return User[index];
        }

        public bool checkAdmin() 
        {
            try 
            {
                MySqlCommand commande = new MySqlCommand("show_admin");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();


                int check = -1;
                while (r.Read()) 
                {
                    check = Convert.ToInt16(r["count"]);
                }
                r.Close();
                connection.Close();

                if (check == 0) 
                { return true; }
                else { return false; }
                
            }
            catch (Exception e) 
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(e.Message);
                    connection.Close();
                }
                return false; 
            }
        }

        public void activateAdmin(Admin admin)
        {
            
                try
                {

                    MySqlCommand commande = new MySqlCommand("activate_admin");
                    commande.Connection = connection;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("username_a", admin.Utilisateur);
                    commande.Parameters.AddWithValue("password_a", genererSHA256(admin.Password));
                
                   
                    connection.Open();
                    commande.Prepare();
                    commande.ExecuteNonQuery();
                    
                    connection.Close();

                    SingletonAdmin.getInstance().LoggedIn = true;
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


        //public string messageE;

        public void validationAdmin(Admin admin)
        {


            try
            {
                MySqlCommand commande = new MySqlCommand("verify_admin");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("username_a", admin.Utilisateur);
                commande.Parameters.AddWithValue("password_a", genererSHA256(admin.Password));


                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();



                connection.Close();
                SingletonAdmin.getInstance().LoggedIn = true;
            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }

                throw;
            }

        }

        private string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
                sb.Append(b.ToString("x2"));

            string hashedPassword = sb.ToString();

            // Log or print the length of the hashed password for debugging
            Console.WriteLine($"Length of Hashed Password: {hashedPassword.Length}");

            return hashedPassword;
        }

    }
}
