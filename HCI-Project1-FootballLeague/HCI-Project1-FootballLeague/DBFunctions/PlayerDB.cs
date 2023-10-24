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
    public class PlayerDB
    {
        public static List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                players.Add(new Player(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), 
                    reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetInt32(9)));
            }
            reader.Close();
            conn.Close();

            return players;
        }

        public static bool AddPlayer(Player player)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();


                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO igrac(BrojOpreme, BrojGolova, BrojAsistencija, BrojZutihKartona, BrojCrvenihKartona, Ime, Prezime, DatumZaposlenja, KlubId) " +
                    "VALUES (@BrojOpreme, @BrojGolova, @BrojAsistencija, @BrojZutihKartona, @BrojCrvenihKartona, @Ime, @Prezime, @DatumZaposlenja, @KlubId)";
                cmd2.Parameters.AddWithValue("@BrojOpreme", player.ShirtNumber);
                cmd2.Parameters.AddWithValue("@BrojGolova", player.NumGoals);
                cmd2.Parameters.AddWithValue("@BrojAsistencija", player.NumAssists);
                cmd2.Parameters.AddWithValue("@BrojZutihKartona", player.NumYellowCards);
                cmd2.Parameters.AddWithValue("@BrojCrvenihKartona", player.NumRedCards);
                cmd2.Parameters.AddWithValue("@Ime", player.FirstName);
                cmd2.Parameters.AddWithValue("@Prezime", player.LastName);
                cmd2.Parameters.AddWithValue("@DatumZaposlenja", player.DateOfContract);
                cmd2.Parameters.AddWithValue("@KlubId", player.ClubId);
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

        public static bool DeletePlayer(int id)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM igrac WHERE IgracId=@IgracId";
                cmd.Parameters.AddWithValue("@IgracId", id);

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

        public static bool UpdatePlayer(Player player)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE igrac SET BrojOpreme=@BrojOpreme, BrojGolova=@BrojGolova, BrojAsistencija=@BrojAsistencija" +
                    ", BrojZutihKartona=@BrojZutihKartona, BrojCrvenihKartona=@BrojCrvenihKartona, Ime=@Ime, " +
                    "Prezime=@Prezime, DatumZaposlenja=@DatumZaposlenja, KlubId=@KlubId WHERE IgracId=@IgracId";

                // Set the parameters for the update query
                cmd.Parameters.AddWithValue("@BrojOpreme", player.ShirtNumber);
                cmd.Parameters.AddWithValue("@BrojGolova", player.NumGoals);
                cmd.Parameters.AddWithValue("@BrojAsistencija", player.NumAssists);
                cmd.Parameters.AddWithValue("@BrojZutihKartona", player.NumYellowCards);
                cmd.Parameters.AddWithValue("@BrojCrvenihKartona", player.NumRedCards);
                cmd.Parameters.AddWithValue("@Ime", player.FirstName);
                cmd.Parameters.AddWithValue("@Prezime", player.LastName);
                cmd.Parameters.AddWithValue("@DatumZaposlenja", player.DateOfContract);
                cmd.Parameters.AddWithValue("@KlubId", player.ClubId);
                cmd.Parameters.AddWithValue("@IgracId", player.PlayerId);

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
