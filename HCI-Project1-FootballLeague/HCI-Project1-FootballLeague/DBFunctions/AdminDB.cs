using HCI_Project1_FootballLeague.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.DBFunctions
{
    public class AdminDB
    {

        public static List<Administrator> GetAdministrators()
        {
            List<Administrator> administrators = new List<Administrator>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT GlavniAdminId, KorisnickoIme, Lozinka, IsGlavniAdmin FROM administrator";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                administrators.Add(new Administrator(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));
            }
            reader.Close();
            conn.Close();

            return administrators;
        }

    }
}
