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

        public static Game GetGameBasedOnId(int gameId)
        {
            Game game = null;
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM utakmica WHERE UtakmicaId=@UtakmicaId";
            cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                game = new Game(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3));
            }
            reader.Close();
            conn.Close();

            return game;
        }

        public static List<ClubInGame> GetClubInGameBasedOnClubId(int clubId, int season)
        {
            List<ClubInGame> game = new List<ClubInGame>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM klub_na_utakmici knu INNER JOIN utakmica u ON knu.UtakmicaId=u.UtakmicaId WHERE knu.KlubId=@KlubId AND Sezona=@Sezona";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
            cmd.Parameters.AddWithValue("@Sezona", season);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                game.Add(new ClubInGame(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetBoolean(3)));
            }
            reader.Close();
            conn.Close();

            return game;
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

        public static List<GameWithClub> GetGamesBasedOnSeasonAndFixture(int season, int fixture)
        {
            List<GameWithClub> gamesWithClubs = new List<GameWithClub>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT k.KlubId, u.UtakmicaId, fk.Naziv, k.IsDomacin, u.DatumUtakmice, k.BrojPostignutihGolova FROM" +
                    " fudbalski_klub fk INNER JOIN klub_na_utakmici k ON k.KlubId=fk.KlubId INNER JOIN utakmica u ON u.UtakmicaId=k.UtakmicaId" +
                    " WHERE u.Sezona=@Sezona AND u.BrojKola=@BrojKola";
            cmd.Parameters.AddWithValue("@Sezona", season);
            cmd.Parameters.AddWithValue("@BrojKola", fixture);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                gamesWithClubs.Add(new GameWithClub(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetBoolean(3),
                    reader.GetDateTime(4), reader.GetInt32(5)));
            }
            reader.Close();
            conn.Close();
            return gamesWithClubs;
        }

        public static List<FootballClub> GetFreeClubsFromFixtureAndSeason(int fixture, int season)
        {
            List<FootballClub> clubs = new List<FootballClub>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM fudbalski_klub fk INNER JOIN klub_na_utakmici knu ON knu.KlubId=fk.KlubId INNER JOIN " +
                "utakmica u ON u.UtakmicaId=knu.UtakmicaId WHERE u.Sezona=@Sezona AND u.BrojKola=@BrojKola";
            cmd.Parameters.AddWithValue("@Sezona", season);
            cmd.Parameters.AddWithValue("@BrojKola", fixture);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                clubs.Add(new FootballClub(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetInt32(3),
                    reader.GetInt32(4)));
            }
            reader.Close();
            conn.Close();
            return clubs;
        }

        public static bool DeleteClubInGame(int clubId, int gameId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM klub_na_utakmici WHERE UtakmicaId=@UtakmicaId AND KlubId=@KlubId";
                cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
                cmd.Parameters.AddWithValue("@KlubId", clubId);
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

        public static bool UpdateClubInGame(ClubInGame game)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE klub_na_utakmici SET BrojPostignutihGolova=@BrojPostignutihGolova, IsDomacin=@IsDomacin WHERE KlubId=@KlubId AND UtakmicaId=@UtakmicaId";

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

        public static bool UpdateGame(Game game)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE utakmica SET DatumUtakmice=@DatumUtakmice WHERE UtakmicaId=@UtakmicaId AND BrojKola=@BrojKola AND Sezona=@Sezona";

                cmd.Parameters.AddWithValue("@DatumUtakmice", game.GameDate);
                cmd.Parameters.AddWithValue("@UtakmicaId", game.GameId);
                cmd.Parameters.AddWithValue("@BrojKola", game.FixtureNum);
                cmd.Parameters.AddWithValue("@Sezona", game.SeasonNum);
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

        public static int AddGame(Game game)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();
                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO utakmica(DatumUtakmice, BrojKola, Sezona" +
                    ") VALUES (@DatumUtakmice, @BrojKola, @Sezona)";
                cmd2.Parameters.AddWithValue("@DatumUtakmice", game.GameDate);
                cmd2.Parameters.AddWithValue("@BrojKola", game.FixtureNum);
                cmd2.Parameters.AddWithValue("@Sezona", game.SeasonNum);
                int brojRedova = cmd2.ExecuteNonQuery();

                cmd2.CommandText = "SELECT LAST_INSERT_ID()";
                int lastInsertedId = Convert.ToInt32(cmd2.ExecuteScalar());

                return lastInsertedId;
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
            return -1;
        }

        public static bool AddClubInGame(ClubInGame clubInGame)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();
                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO klub_na_utakmici(KlubId, UtakmicaId, BrojPostignutihGolova, IsDomacin" +
                    ") VALUES (@KlubId, @UtakmicaId, @BrojPostignutihGolova, @IsDomacin)";
                cmd2.Parameters.AddWithValue("@KlubId", clubInGame.ClubId);
                cmd2.Parameters.AddWithValue("@UtakmicaId", clubInGame.GameId);
                cmd2.Parameters.AddWithValue("@BrojPostignutihGolova", clubInGame.NumGoalsScored);
                cmd2.Parameters.AddWithValue("@IsDomacin", clubInGame.IsHomeTeam);
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

    }
}
