using HCI_Project1_FootballLeague.Classes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Project1_FootballLeague.DBFunctions
{
    public class GameDB
    {
        public static List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT UtakmicaId, DatumUtakmice, BrojKola, Sezona FROM utakmica";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                games.Add(new Game(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3)));
            }
            reader.Close();
            conn.Close();

            return games;
        }

        public static List<Int32> GetFixturesInSeason(int season)
        {
            List<Int32> fixtures = new List<Int32>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT BrojKola FROM utakmica WHERE Sezona=@Sezona";
            cmd.Parameters.AddWithValue("@Sezona", season);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                fixtures.Add(reader.GetInt32(0));
            }
            reader.Close();
            conn.Close();

            return fixtures;
        }

        public static void GetGamesBasedOnSeasonAndFixture(int season, int fixture)
        {
            List<Int32> fixtures = new List<Int32>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT k.KlubId, k.Naziv, ku.BrojPostignutihGolova, ku.IsDomacin, u.DatumUtakmice FROM" +
                "fudbalski_klub k INNER JOIN klub_na_utakmici ku ON ku.KlubId=k.KlubId INNER JOIN utakmica u ON u.UtakmicaId=ku.UtakmicaId" +
                " WHERE u.Sezona=@Sezona AND u.BrojKola=@BrojKola";
            cmd.Parameters.AddWithValue("@Sezona", season);
            cmd.Parameters.AddWithValue("@BrojKola", fixture);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                //fixtures.Add(reader.GetInt32(0));
            }
            reader.Close();
            conn.Close();
        }

        public static bool DeleteGame(int id)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM utakmica WHERE UtakmicaId=@UtakmicaId";
                cmd.Parameters.AddWithValue("@UtakmicaId", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
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

        public static bool UpdateGame(ClubInGame game)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE klub_na_utakmici SET BrojPostignutihGolova=@BrojPostignutihGolova, IsDomacin=@IsDomacinx WHERE KlubId=@KlubId AND UtakmicaId=@UtakmicaId";

                cmd.Parameters.AddWithValue("@BrojPostignutihGolova", game.NumGoalsScored);
                cmd.Parameters.AddWithValue("@IsDomacin", game.IsHomeTeam);
                cmd.Parameters.AddWithValue("@KlubId", game.ClubId);
                cmd.Parameters.AddWithValue("@UtakmicaId", game.GameId);
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
