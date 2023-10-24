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
    public class FootballClubDB
    {
        public static List<FootballClub> GetClubs()
        {
            List<FootballClub> clubs = new List<FootballClub>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT KlubId, Naziv, DatumOsnivanja, BrojOsvojenihTrofeja, StadionId FROM fudbalski_klub";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                clubs.Add(new FootballClub(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetInt32(3), reader.GetInt32(4)));
            }
            reader.Close();
            conn.Close();

            return clubs;
        }

        public static bool AddClub(FootballClub club)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();


                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO fudbalski_klub(Naziv, DatumOsnivanja, BrojOsvojenihTrofeja, StadionId" +
                    ") VALUES (@Naziv, @DatumOsnivanja, @BrojOsvojenihTrofeja, @StadionId)";
                cmd2.Parameters.AddWithValue("@Naziv", club.Name);
                cmd2.Parameters.AddWithValue("@DatumOsnivanja", club.Date);
                cmd2.Parameters.AddWithValue("@BrojOsvojenihTrofeja", club.NumTrophies);
                cmd2.Parameters.AddWithValue("@StadionId", club.StadiumId);
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
                cmd.CommandText = "DELETE FROM fudbalski_klub WHERE KlubId=@KlubId";
                cmd.Parameters.AddWithValue("@KlubId", id);

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

        public static bool UpdateStadium(FootballClub club)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE fudbalski_klub SET Naziv=@Naziv, DatumOsnivanja=@DatumOsnivanja, BrojOsvojenihTrofeja=@BrojOsvojenihTrofeja, StadionId=@StadionId WHERE KlubId=@KlubId";

                // Set the parameters for the update query
                cmd.Parameters.AddWithValue("@Naziv", club.Name);
                cmd.Parameters.AddWithValue("@DatumOsnivanja", club.Date);
                cmd.Parameters.AddWithValue("@BrojOsvojenihTrofeja", club.NumTrophies);
                cmd.Parameters.AddWithValue("@StadionId", club.StadiumId);
                cmd.Parameters.AddWithValue("@KlubId", club.ClubId);

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
