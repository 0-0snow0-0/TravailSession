using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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

        int countL = 0;

        public static bool LoggedIn
        { get; set; } = false;

        public ObservableCollection<Admin> getAdmin()
        {
            return User;
        }

        public Admin getAdmin(int index)
        {
            return User[index];
        }
        public void activateAdmin(Admin admin)
        {
            if (countL == 0 && admin.FirstBoot != true)
            {
                try
                {

                    MySqlCommand commande = new MySqlCommand("activate_admin");
                    commande.Connection = connection;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("email_a", admin.Utilisateur);
                    commande.Parameters.AddWithValue("password_a", admin.Password);
                    commande.Parameters.AddWithValue("firstBoot_a", admin.FirstBoot);

                    connection.Open();
                    commande.Prepare();
                    commande.ExecuteNonQuery();
                    countL++;
                    connection.Close();


                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        countL = 0;
                        Console.WriteLine(ex.Message);
                        connection.Close();
                    }
                }
            }
            else { validationAdmin(admin); }

        }

        

       
    public void validationAdmin(Admin admin)
        {
            if (countL > 0) 
            {
                try
                {
                    MySqlCommand commande = new MySqlCommand("verify_admin");
                    commande.Connection = connection;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("username_a", admin.Utilisateur);
                    commande.Parameters.AddWithValue("password_a", admin.Password);

                    commande.Parameters.Add(new MySqlParameter("result_message", MySqlDbType.VarChar, 255));
                    commande.Parameters["result_message"].Direction = ParameterDirection.Output;

                    connection.Open();
                    commande.Prepare();
                    commande.ExecuteNonQuery();

                    string resultMessage = commande.Parameters["result_message"].Value.ToString();

                    if (resultMessage != "Login successful")
                    {
                        throw new Exception(resultMessage); // Throw an exception if login fails
                    }

                    connection.Close();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
            }
            
        }
       
        
    }
}
