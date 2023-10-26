using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
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
using System.Windows.Shapes;

namespace HCI_Project1_FootballLeague.PlayerWindows
{
    /// <summary>
    /// Interaction logic for PlayerShowWindow.xaml
    /// </summary>
    public partial class PlayerShowWindow : Window
    {
        public PlayerShowWindow()
        {
            InitializeComponent();
            PopulateData();
        }

        private List<FootballClub> clubs = FootballClubDB.GetClubs();

        private void PopulateData()
        {
            List<Player> players = PlayerDB.GetPlayers();
            foreach (Player p in players)
            {
                p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                DataGridXAML.Items.Add(p);
            }
            foreach (FootballClub fc in clubs)
            {
                ClubComboBox.Items.Add(fc);
            }
        }

        private void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var firstName = FirstNameTB.Text;
            var lastName = LastNameTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var shirtNumber = ShirtNumberTB.Text;
            var club = (FootballClub)ClubComboBox.SelectedItem;
            if (!"".Equals(firstName) && !"".Equals(lastName) && date != null && !"".Equals(shirtNumber) && club != null)
            {
                Player player = new Player(Int32.Parse(shirtNumber), 0, 0, 0, 0, 0, firstName, lastName, date, club.ClubId);
                PlayerDB.AddPlayer(player);
                DrawData();
            }
            else
            {
                MessageBox.Show("One of required inputs not entered!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedPlayer = (Player)DataGridXAML.SelectedItem;
                PlayerDB.DeletePlayer(selectedPlayer.PlayerId);
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private static void NotSelectedMessage()
        {
            MessageBox.Show("Item not selected!");
        }

        private void ConfirmUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var player = (Player)DataGridXAML.SelectedItem;
                var id = player.PlayerId;
                var firstName = UpdateFirstNameTB.Text;
                var lastName = UpdateLastNameTB.Text;
                var shirtNum = UpdateShirtNumberTB.Text;
                var date = UpdateDateBox.SelectedDate.Value;
                var club = (FootballClub)UpdateClubComboBox.SelectedItem;
                Player p = new Player(id, Int32.Parse(shirtNum), player.NumGoals, player.NumAssists, player.NumYellowCards, player.NumRedCards, firstName,
                    lastName, date, club.ClubId);
                PlayerDB.UpdatePlayer(p);
                UpdateFirstNameLabel.Visibility = Visibility.Hidden;
                UpdateFirstNameTB.Visibility = Visibility.Hidden;
                UpdateDateLabel.Visibility = Visibility.Hidden;
                UpdateDateBox.Visibility = Visibility.Hidden;
                UpdateLastNameLabel.Visibility = Visibility.Hidden;
                UpdateLastNameTB.Visibility = Visibility.Hidden;
                UpdateClubComboBox.Visibility = Visibility.Hidden;
                UpdateFootballClubLabel.Visibility = Visibility.Hidden;
                ConfirmUpdateButton.Visibility = Visibility.Hidden;
                UpdateShirtNumLabel.Visibility = Visibility.Hidden;
                UpdateShirtNumberTB.Visibility = Visibility.Hidden;
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        //Player player = null;

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (FootballClub f in clubs)
            {
                UpdateClubComboBox.Items.Add(f);
            }
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateFirstNameLabel.Visibility = Visibility.Visible;
                UpdateFirstNameTB.Visibility = Visibility.Visible;
                UpdateDateLabel.Visibility = Visibility.Visible;
                UpdateDateBox.Visibility = Visibility.Visible;
                UpdateLastNameLabel.Visibility = Visibility.Visible;
                UpdateLastNameTB.Visibility = Visibility.Visible;
                UpdateClubComboBox.Visibility = Visibility.Visible;
                UpdateFootballClubLabel.Visibility = Visibility.Visible;
                ConfirmUpdateButton.Visibility = Visibility.Visible;
                UpdateShirtNumLabel.Visibility = Visibility.Visible;
                UpdateShirtNumberTB.Visibility = Visibility.Visible;
                Player player = (Player)DataGridXAML.SelectedItem;
                UpdateFirstNameTB.Text = player.FirstName;
                UpdateLastNameTB.Text = player.LastName;
                UpdateDateBox.SelectedDate = player.DateOfContract;
                UpdateShirtNumberTB.Text = player.ShirtNumber.ToString();
                foreach (FootballClub f in clubs)
                {
                    if (f.ClubId == player.ClubId)
                    {
                        UpdateClubComboBox.SelectedItem = f;
                    }
                }
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ("".Equals(SearchTB.Text))
            {
                DrawData();
            }
            else
            {
                DataGridXAML.Items.Clear();
                List<Player> players = PlayerDB.SearchPlayers(SearchTB.Text);
                foreach (Player p in players)
                {
                    p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                    DataGridXAML.Items.Add(p);
                }
            }
        }
    }
}
