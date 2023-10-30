using HCI_Project1_FootballLeague.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            cmd.CommandText = "SELECT * FROM administrator";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                /*MessageBox.Show(reader.GetInt32(0).ToString());
                MessageBox.Show(reader.GetString(1));
                MessageBox.Show(reader.GetString(2));
                MessageBox.Show(reader.GetBoolean(3).ToString());
                MessageBox.Show(reader.GetString(4));*/
                administrators.Add(new Administrator(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4)));
            }
            reader.Close();
            conn.Close();

            return administrators;
        }

        public static List<Administrator> SearchAdmins(string searchString)
        {
            List<Administrator> admins = new List<Administrator>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT GlavniAdminId, KorisnickoIme, Lozinka, IsGlavniAdmin FROM administrator WHERE KorisnickoIme LIKE CONCAT(@SearchString, '%') AND IsGlavniAdmin=0";
            cmd.Parameters.AddWithValue("@SearchString", searchString);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                admins.Add(new Administrator(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), 
                    reader.GetString(4)));
            }
            reader.Close();
            conn.Close();

            return admins;
        }

        public static List<Administrator> GetLeagueAdministrators()
        {
            List<Administrator> administrators = new List<Administrator>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM administrator WHERE IsGlavniAdmin=0";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                administrators.Add(new Administrator(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), 
                    reader.GetString(4)));
            }
            reader.Close();
            conn.Close();

            return administrators;
        }

        public static bool AddAdmin(Administrator admin)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();


                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO administrator(KorisnickoIme, Lozinka, IsGlavniAdmin) VALUES (@KorisnickoIme, @Lozinka, @IsGlavniAdmin)";
                cmd2.Parameters.AddWithValue("@KorisnickoIme", admin.UserName);
                cmd2.Parameters.AddWithValue("@Lozinka", admin.Password);
                cmd2.Parameters.AddWithValue("@IsGlavniAdmin", admin.IsMainAdmin);
                int brojRedova = cmd2.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                Trace.WriteLine(e);
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn2.Close();

            }
            return false;
        }

        public static bool DeleteAdmin(int id)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM administrator WHERE GlavniAdminId=@GlavniAdminId";
                cmd.Parameters.AddWithValue("@GlavniAdminId", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
                //MessageBox.Show($"{rowsAffected} row(s) deleted.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;

        }

        public static bool UpdateAdmin(Administrator admin)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE administrator SET KorisnickoIme=@KorisnickoIme, Lozinka=@Lozinka, IsGlavniAdmin=@IsGlavniAdmin WHERE GlavniAdminId=@GlavniAdminId";

                // Set the parameters for the update query
                cmd.Parameters.AddWithValue("@KorisnickoIme", admin.UserName);
                cmd.Parameters.AddWithValue("@Lozinka", admin.Password);
                cmd.Parameters.AddWithValue("@IsGlavniAdmin", admin.IsMainAdmin);
                cmd.Parameters.AddWithValue("@GlavniAdminId", admin.AdminId);

                int rowsAffected = cmd.ExecuteNonQuery();

                //MessageBox.Show($"{rowsAffected} row(s) updated.");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public static bool UpdateAdminPreferences(Administrator admin)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE administrator SET Jezik=@Jezik WHERE GlavniAdminId=@GlavniAdminId";

                cmd.Parameters.AddWithValue("@Jezik", admin.Language);
                cmd.Parameters.AddWithValue("@GlavniAdminId", admin.AdminId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

    }
}
