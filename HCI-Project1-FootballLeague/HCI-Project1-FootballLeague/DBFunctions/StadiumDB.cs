using HCI_Project1_FootballLeague.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Project1_FootballLeague.DBFunctions
{
    public class StadiumDB
    {
        public static List<Stadium> GetStadiums()
        {
            List<Stadium> stadiums = new List<Stadium>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT StadionId, Naziv, Kapacitet, Grad FROM stadion";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                stadiums.Add(new Stadium(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3)));
            }
            reader.Close();
            conn.Close();

            return stadiums;
        }

        public static List<Stadium> SearchStadiums(string searchString)
        {
            List<Stadium> stadiums = new List<Stadium>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT StadionId, Naziv, Kapacitet, Grad FROM stadion WHERE Naziv LIKE CONCAT(@SearchString, '%')";
            cmd.Parameters.AddWithValue("@SearchString", searchString);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                stadiums.Add(new Stadium(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3)));
            }
            reader.Close();
            conn.Close();

            return stadiums;
        }

        public static bool AddStadium(Stadium stadium)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();

                
                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO stadion(Naziv, Kapacitet, Grad) VALUES (@Naziv, @Kapacitet, @Grad)";
                cmd2.Parameters.AddWithValue("@Naziv", stadium.Name);
                cmd2.Parameters.AddWithValue("@Kapacitet", stadium.Capacity);
                cmd2.Parameters.AddWithValue("@Grad", stadium.Town);
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

        public static bool DeleteStadium(int id)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {         
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM stadion WHERE StadionId=@StadionId";
                cmd.Parameters.AddWithValue("@StadionId", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
                //MessageBox.Show($"{rowsAffected} row(s) deleted.");
            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            } finally
            {
                conn.Close();
            }
            return false;
          
        }

        public static bool UpdateStadium(Stadium stadium)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE stadion SET Naziv=@Naziv, Kapacitet=@Kapacitet, Grad=@Grad WHERE StadionId=@StadionId";

                // Set the parameters for the update query
                cmd.Parameters.AddWithValue("@Naziv", stadium.Name);
                cmd.Parameters.AddWithValue("@Kapacitet", stadium.Capacity);
                cmd.Parameters.AddWithValue("@Grad", stadium.Town);
                cmd.Parameters.AddWithValue("@StadionId", stadium.StadiumId);

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

    }
}
