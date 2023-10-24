using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project1_FootballLeague
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string ConnectionString = "Server=localhost;Database=fudbalskaliga-hci;UserId = root; Password = Root123!;";

        public MainWindow()
        {
            InitializeComponent();

            /*MySqlConnection conn = new MySqlConnection("Server=localhost;Database=fudbalskaliga;UserId = root; Password = Root123!;");
            conn.Open();

            List<SeasonStats> stadiums = new List<SeasonStats>();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT BrojOdigranihUtakmica, BrojPobjeda, BrojNerijesenih, BrojPoraza, BrojPostignutihGolova, " +
                "BrojPrimljenihGolova, FUDBALSKI_KLUB_IdKluba, BrojBodova FROM statistika_u_sezoni";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                stadiums.Add(new SeasonStats(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), 
                    reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), 2023));
            }
            reader.Close();
            conn.Close();*/

            /*List<Administrator> admins = AdminDB.GetAdministrators(); 
            foreach(Administrator s in admins)
            {
                MessageBox.Show(s.UserName);
            }*/

            /*foreach (FootballClub s in stadiums)
                {
                    MySqlCommand cmd2 = conn2.CreateCommand();
                    cmd2 = conn2.CreateCommand();
                    cmd2.CommandText = "INSERT INTO fudbalski_klub(Naziv, DatumOsnivanja, BrojOsvojenihTrofeja, StadionId) VALUES (@Naziv, @DatumOsnivanja, @BrojOsvojenihTrofeja, @StadionId)";
                    cmd2.Parameters.AddWithValue("@Naziv", s.Name);
                    cmd2.Parameters.AddWithValue("@DatumOsnivanja", s.Date);
                    cmd2.Parameters.AddWithValue("@BrojOsvojenihTrofeja", s.NumTrophies);
                    cmd2.Parameters.AddWithValue("@StadionId", s.StadionId);
                    int brojRedova = cmd2.ExecuteNonQuery();
                }*/


            /*MySqlConnection conn2 = new MySqlConnection("Server=localhost;Database=fudbalskaliga-hci;UserId = root; Password = Root123!;");
            try
            {
                conn2.Open();

                foreach (SeasonStats p in stadiums)
                {
                    MySqlCommand cmd2 = conn2.CreateCommand();
                    cmd2 = conn2.CreateCommand();
                    cmd2.CommandText = "INSERT INTO statistika_u_sezoni(BrojOdigranihUtakmica, BrojPobjeda, BrojNerijesenih, BrojPoraza, BrojPostignutihGolova, BrojPrimljenihGolova, KlubId, BrojBodova, Sezona)" +
                        " VALUES (@BrojOdigranihUtakmica, @BrojPobjeda, @BrojNerijesenih, @BrojPoraza, @BrojPostignutihGolova, @BrojPrimljenihGolova, @KlubId, @BrojBodova, @Sezona)";
                    cmd2.Parameters.AddWithValue("@BrojOdigranihUtakmica", p.NumGamesPlayed);
                    cmd2.Parameters.AddWithValue("@BrojPobjeda", p.NumWins);
                    cmd2.Parameters.AddWithValue("@BrojNerijesenih", p.NumDraws);
                    cmd2.Parameters.AddWithValue("@BrojPoraza", p.NumLoses);
                    cmd2.Parameters.AddWithValue("@BrojPostignutihGolova", p.NumScored);
                    cmd2.Parameters.AddWithValue("@BrojPrimljenihGolova", p.NumConceded);
                    cmd2.Parameters.AddWithValue("@KlubId", p.ClubId);
                    cmd2.Parameters.AddWithValue("@BrojBodova", p.NumPoints);
                    cmd2.Parameters.AddWithValue("@Sezona", p.SeasonNum);
                    int brojRedova = cmd2.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn2.Close();
            
            }*/

            //StadiumDB.AddStadium(new Stadium("Ime", 10, "Grad"));
            //StadiumDB.DeleteStadium(21);

            //StadiumDB.UpdateStadium(new Stadium(1, "Etihad!", 22000, "Manchester"));

            //AdminDB.AddAdmin(new Administrator("Kor5", "Lozinka5", false));
            //AdminDB.DeleteStadium(5);
            //AdminDB.UpdateAdmin(new Administrator(4, "Korisnik4", "Lozinka4", false));

            /*List<FootballClub> clubs = FootballClubDB.GetClubs();
            foreach(var club in clubs)
            {
                MessageBox.Show(club.Name);
            }*/

            //FootballClubDB.AddClub(new FootballClub("Ime", new DateTime(1990, 05, 05), 0, 1));
            //FootballClubDB.DeleteStadium(19);
            //FootballClubDB.UpdateStadium(new FootballClub(17, "Brentford", new DateTime(1986, 11, 21), 1, 20));

            //PlayerDB.DeletePlayer(14);

            /*List<SeasonStats> clubs = SeasonStatsDB.GetStats();
            foreach (var club in clubs)
            {
                MessageBox.Show($"{club.NumPoints}");
            }*/

            //SeasonStatsDB.UpdateStats(new SeasonStats(1, 1, 0, 0, 2, 1, 17, 3, 2023));

        }

        /*public class Employee
        {
            public string employeeID { get; set; }
            public string employeeName { get; set; }
            public string employeeAddress { get; set; }
            public string employeeCity { get; set; }
            public string employeeState { get; set; }

        }*/

        //Add NE Button Clicked
        public void AddNewEmployeeBN_Clicked(object sender, RoutedEventArgs e)
        {
            /*Employee temp = new Employee();
            temp.employeeID = IdTB.Text;
            temp.employeeName = NameTB.Text;
            temp.employeeAddress = AddressTB.Text;
            temp.employeeCity = CityTB.Text;
            temp.employeeState = StateTB.Text;
            DataGridXAML.Items.Add(temp);*/
        }
    }
}
