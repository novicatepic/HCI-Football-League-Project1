using HCI_Project1_FootballLeague.Classes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Project1_FootballLeague.DBFunctions
{
    public class SeasonStatsDB
    {
        public static List<SeasonStats> GetStats(int season)
        {
            List<SeasonStats> stats = new List<SeasonStats>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM statistika_u_sezoni WHERE Sezona=@Sezona";
            cmd.Parameters.AddWithValue("@Sezona", season);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                stats.Add(new SeasonStats(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                    reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)));
            }
            reader.Close();
            conn.Close();

            return stats;
        }

        public static SeasonStats GetStatsBasedOnClub(int clubId, int season)
        {
            SeasonStats stats = null;
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM statistika_u_sezoni WHERE KlubId=@KlubId AND Sezona=@Sezona";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
            cmd.Parameters.AddWithValue("@Sezona", season);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats = new SeasonStats(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                    reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
            }
            reader.Close();
            conn.Close();

            return stats;
        }

        public static string GetClubName(string clubId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT f.Naziv FROM fudbalski_klub f INNER JOIN statistika_u_sezoni s ON s.KlubId=f.KlubId WHERE f.KlubId=@KlubId";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
            MySqlDataReader reader = cmd.ExecuteReader();
            string stadiumName = "";
            while (reader.Read())
            {
                stadiumName = reader.GetString(0);
            }
            reader.Close();
            conn.Close();
            return stadiumName;
        }

        public static List<SeasonStats> SearchStats(int season, string searchString)
        {
            List<SeasonStats> stats = new List<SeasonStats>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT f.Naziv, s.* FROM fudbalski_klub f INNER JOIN statistika_u_sezoni s ON s.KlubId=f.KlubId WHERE f.Naziv LIKE CONCAT(@SearchString, '%') AND Sezona=@Sezona";
            cmd.Parameters.AddWithValue("@SearchString", searchString);
            cmd.Parameters.AddWithValue("@Sezona", season);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats.Add(new SeasonStats(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
                    reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetString(0)));
            }
            reader.Close();
            conn.Close();
            return stats;
        }

        public static List<Int32> GetSeasons()
        {
            List<Int32> seasons = new List<Int32>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT Sezona FROM utakmica";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                seasons.Add(reader.GetInt32(0));
            }
            reader.Close();
            conn.Close();

            return seasons;
        }

        
        public static bool AddStats(SeasonStats stats)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();


                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO statistika_u_sezoni(BrojOdigranihUtakmica, BrojPobjeda, BrojNerijesenih, BrojPoraza, BrojPostignutihGolova, BrojPrimljenihGolova, KlubId, BrojBodova, Sezona) " +
                    "VALUES (@BrojOdigranihUtakmica, @BrojPobjeda, @BrojNerijesenih, @BrojPoraza, @BrojPostignutihGolova, @BrojPrimljenihGolova, @KlubId, @BrojBodova, @Sezona)";
                cmd2.Parameters.AddWithValue("@BrojOdigranihUtakmica", stats.NumGamesPlayed);
                cmd2.Parameters.AddWithValue("@BrojPobjeda", stats.NumWins);
                cmd2.Parameters.AddWithValue("@BrojNerijesenih", stats.NumDraws);
                cmd2.Parameters.AddWithValue("@BrojPoraza", stats.NumLoses);
                cmd2.Parameters.AddWithValue("@BrojPostignutihGolova", stats.NumScored);
                cmd2.Parameters.AddWithValue("@BrojPrimljenihGolova", stats.NumConceded);
                cmd2.Parameters.AddWithValue("@KlubId", stats.ClubId);
                cmd2.Parameters.AddWithValue("@BrojBodova", stats.NumPoints);
                cmd2.Parameters.AddWithValue("@Sezona", stats.SeasonNum);
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


        //Not tested yet
        public static bool DeleteStats(int clubId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM statistika_u_sezoni WHERE KlubId=@KlubId";
                cmd.Parameters.AddWithValue("@KlubId", clubId);
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

        public static bool UpdateStats(SeasonStats stats)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE statistika_u_sezoni SET BrojOdigranihUtakmica=@BrojOdigranihUtakmica, BrojPobjeda=@BrojPobjeda, BrojNerijesenih=@BrojNerijesenih" +
                    ", BrojPoraza=@BrojPoraza, BrojPostignutihGolova=@BrojPostignutihGolova, BrojPrimljenihGolova=@BrojPrimljenihGolova, " +
                    "BrojBodova=@BrojBodova WHERE KlubId=@KlubId AND Sezona=@Sezona";

                // Set the parameters for the update query
                cmd.Parameters.AddWithValue("@BrojOdigranihUtakmica", stats.NumGamesPlayed);
                cmd.Parameters.AddWithValue("@BrojPobjeda", stats.NumWins);
                cmd.Parameters.AddWithValue("@BrojNerijesenih", stats.NumDraws);
                cmd.Parameters.AddWithValue("@BrojPoraza", stats.NumLoses);
                cmd.Parameters.AddWithValue("@BrojPostignutihGolova", stats.NumScored);
                cmd.Parameters.AddWithValue("@BrojPrimljenihGolova", stats.NumConceded);
                cmd.Parameters.AddWithValue("@BrojBodova", stats.NumPoints);
                cmd.Parameters.AddWithValue("@KlubId", stats.ClubId);
                cmd.Parameters.AddWithValue("@Sezona", stats.SeasonNum);

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
