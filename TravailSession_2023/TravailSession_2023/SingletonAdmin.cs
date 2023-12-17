using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static bool LoggedIn { get; set; } = false;
        
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
            
            try
            {   if (admin.FirstBoot != true)
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

                    connection.Close();
                }
                else 
                {
                    Console.WriteLine("Activation already performed.");
                }
                
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
