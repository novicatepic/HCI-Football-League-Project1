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
    public class SeasonStatsDB
    {
        public static List<SeasonStats> GetStats()
        {
            List<SeasonStats> stats = new List<SeasonStats>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM statistika_u_sezoni";
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

        //Not tested yet
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
        public static bool DeleteStats(int clubId, int seasonNum)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM statistika_u_sezoni WHERE KlubId=@KlubId AND Sezona=@Sezona";
                cmd.Parameters.AddWithValue("@KlubId", clubId);
                cmd.Parameters.AddWithValue("@Sezona", seasonNum);

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
