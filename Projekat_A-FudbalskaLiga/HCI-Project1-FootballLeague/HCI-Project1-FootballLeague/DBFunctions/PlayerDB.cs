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

        public static List<Player> SearchPlayers(string searchString)
        {
            List<Player> players = new List<Player>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac WHERE Ime LIKE CONCAT(@SearchString, '%')";
            cmd.Parameters.AddWithValue("@SearchString", searchString);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                players.Add(new Player(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6),
                    reader.GetString(7), reader.GetDateTime(8), reader.GetInt32(9)));
            }
            reader.Close();
            conn.Close();

            return players;
        }

        public static string GetClubName(string playerId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT f.Naziv FROM fudbalski_klub f INNER JOIN igrac i ON i.KlubId=f.KlubId WHERE IgracId=@IgracId";
            cmd.Parameters.AddWithValue("@IgracId", playerId);
            MySqlDataReader reader = cmd.ExecuteReader();
            string clubName = "";
            while (reader.Read())
            {
                clubName = reader.GetString(0);
            }
            reader.Close();
            conn.Close();
            return clubName;
        }

        public static List<PlayerInGame> GetPlayersFromClubAndGame(int clubId, int gameId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();
            List<PlayerInGame> playersInGame = new List<PlayerInGame>();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac_na_utakmici WHERE KlubId=@KlubId AND UtakmicaId=@UtakmicaId";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
            cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                playersInGame.Add(new PlayerInGame(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetBoolean(7), reader.GetInt32(8)));
            }
            reader.Close();
            conn.Close();
            return playersInGame;
        }

        public static PlayerInGame GetPlayerFromClubAndGame(int clubId, int gameId, int playerId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();
            PlayerInGame playerInGame = null; ;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac_na_utakmici WHERE KlubId=@KlubId AND UtakmicaId=@UtakmicaId AND IgracId=@IgracId";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
            cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
            cmd.Parameters.AddWithValue("@IgracId", playerId);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                playerInGame = new PlayerInGame(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetBoolean(7), reader.GetInt32(8));
            }
            reader.Close();
            conn.Close();
            return playerInGame;
        }

        public static List<Player> GetPlayersWhoPlayedBasedOnGameAndClub(int gameId, int clubId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();
            List<Player> players = new List<Player>();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac i INNER JOIN igrac_na_utakmici inu ON i.IgracId=inu.IgracId WHERE " +
                "inu.KlubId=i.KlubId AND inu.UtakmicaId=@UtakmicaId AND i.KlubId=@KlubId";
            cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
            cmd.Parameters.AddWithValue("@KlubId", clubId);
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

        public static List<Player> GetPlayersFromClub(int clubId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();
            List<Player> players = new List<Player>();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM igrac i WHERE i.KlubId=@KlubId";
            cmd.Parameters.AddWithValue("@KlubId", clubId);
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
                MessageBox.Show("ERROR/ГРЕШКА");
            }
            finally
            {
                conn2.Close();

            }
            return false;
        }

        public static bool AddPlayerInGame(PlayerInGame player)
        {

            MySqlConnection conn2 = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn2.Open();


                MySqlCommand cmd2 = conn2.CreateCommand();
                cmd2 = conn2.CreateCommand();
                cmd2.CommandText = "INSERT INTO igrac_na_utakmici(IgracId, BrojGolovaNaUtakmici, BrojAsistencijaNaUtakmici, DobioZuti, DobioCrveni, KlubId, UtakmicaId,PoceoUtakmicu, OdigraoMinuta) " +
                    "VALUES (@IgracId, @BrojGolovaNaUtakmici, @BrojAsistencijaNaUtakmici, @DobioZuti, @DobioCrveni, @KlubId, @UtakmicaId, @PoceoUtakmicu, @OdigraoMinuta)";
                cmd2.Parameters.AddWithValue("@IgracId", player.PlayerId);
                cmd2.Parameters.AddWithValue("@BrojGolovaNaUtakmici", player.NumGoalsInGame);
                cmd2.Parameters.AddWithValue("@BrojAsistencijaNaUtakmici", player.NumAssistsInGame);
                cmd2.Parameters.AddWithValue("@DobioZuti", player.HasYellow);
                cmd2.Parameters.AddWithValue("@DobioCrveni", player.HasRed);
                cmd2.Parameters.AddWithValue("@KlubId", player.ClubId);
                cmd2.Parameters.AddWithValue("@UtakmicaId", player.GameId);
                cmd2.Parameters.AddWithValue("@PoceoUtakmicu", player.StartedGame);
                cmd2.Parameters.AddWithValue("@OdigraoMinuta", player.MinutesPlayed);
                int brojRedova = cmd2.ExecuteNonQuery();
                //MessageBox.Show("!");
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("ERROR/ГРЕШКА");
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

        public static bool DeletePlayerInGame(int playerId, int clubId, int gameId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM igrac_na_utakmici WHERE IgracId=@IgracId AND UtakmicaId=@UtakmicaId AND KlubId=@KlubId";
                cmd.Parameters.AddWithValue("@IgracId", playerId);
                cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
                cmd.Parameters.AddWithValue("@KlubId", clubId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR/ГРЕШКА");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public static bool DeleteAllPlayersFromGame(int gameId)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM igrac_na_utakmici WHERE UtakmicaId=@UtakmicaId";
                cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR/ГРЕШКА");
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
                MessageBox.Show("ERROR/ГРЕШКА");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public static bool UpdatePlayerInGame(PlayerInGame player)
        {
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE igrac_na_utakmici SET BrojGolovaNaUtakmici=@BrojGolovaNaUtakmici, BrojAsistencijaNaUtakmici=@BrojAsistencijaNaUtakmici, DobioZuti=@DobioZuti" +
                    ", DobioCrveni=@DobioCrveni, PoceoUtakmicu=@PoceoUtakmicu, OdigraoMinuta=@OdigraoMinuta" +
                    " WHERE IgracId=@IgracId AND KlubId=@KlubId AND UtakmicaId=@UtakmicaId";

                cmd.Parameters.AddWithValue("@BrojGolovaNaUtakmici", player.NumGoalsInGame);
                cmd.Parameters.AddWithValue("@BrojAsistencijaNaUtakmici", player.NumAssistsInGame);
                cmd.Parameters.AddWithValue("@DobioZuti", player.HasYellow);
                cmd.Parameters.AddWithValue("@DobioCrveni", player.HasRed);
                cmd.Parameters.AddWithValue("@PoceoUtakmicu", player.StartedGame);
                cmd.Parameters.AddWithValue("@OdigraoMinuta", player.MinutesPlayed);
                cmd.Parameters.AddWithValue("@IgracId", player.PlayerId);
                cmd.Parameters.AddWithValue("@KlubId", player.ClubId);
                cmd.Parameters.AddWithValue("@UtakmicaId", player.GameId);

                int rowsAffected = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR/ГРЕШКА");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public static List<PlayerInGame> GetPlayerFromGame(int gameId)
        {
            List<PlayerInGame> players = new List<PlayerInGame>();
            MySqlConnection conn = new MySqlConnection(MainWindow.ConnectionString);
            conn.Open();
            /*SELECT DISTINCT i.IgracId, i.Ime, i.Prezime, inu.BrojGolovaNaUtakmici, inu.BrojAsistencijaNaUtakmici, inu.DobioZuti, inu.DobioCrveni, inu.PoceoUtakmicu, inu.OdigraoMinuta, inu.KlubId, inu.UtakmicaId, knu.IsDomacin FROM igrac i  INNER JOIN igrac_na_utakmici inu ON i.IgracId=inu.IgracIdINNER JOIN klub_na_utakmici knu ON knu.KlubId=i.KlubId AND knu.UtakmicaId=inu.UtakmicaId WHERE inu.UtakmicaId*/
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT i.IgracId, i.Ime, i.Prezime, inu.BrojGolovaNaUtakmici, inu.BrojAsistencijaNaUtakmici, inu.DobioZuti, inu.DobioCrveni, inu.PoceoUtakmicu, inu.OdigraoMinuta, inu.KlubId, inu.UtakmicaId, knu.IsDomacin FROM igrac i  INNER JOIN igrac_na_utakmici inu ON i.IgracId=inu.IgracId INNER JOIN klub_na_utakmici knu ON knu.KlubId=i.KlubId AND knu.UtakmicaId=inu.UtakmicaId WHERE inu.UtakmicaId=@UtakmicaId";
            cmd.Parameters.AddWithValue("@UtakmicaId", gameId);
            
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PlayerInGame pig = new PlayerInGame(reader.GetInt32(0), reader.GetInt32(3), reader.GetInt32(4), reader.GetBoolean(5),
                    reader.GetBoolean(6), reader.GetInt32(9), reader.GetInt32(10), reader.GetBoolean(7), reader.GetInt32(8));
                pig.FirstName = reader.GetString(1);
                pig.LastName = reader.GetString(2);

                pig.IsFromHomeTeam = reader.GetBoolean(11);
                players.Add(pig);

            }
            reader.Close();
            conn.Close();
            return players;
        }
    }
}
