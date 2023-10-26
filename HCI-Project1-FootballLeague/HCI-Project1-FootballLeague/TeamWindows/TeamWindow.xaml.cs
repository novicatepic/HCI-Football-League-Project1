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

namespace HCI_Project1_FootballLeague.TeamWindows
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        private List<Stadium> stadiums = StadiumDB.GetStadiums();
        public TeamWindow()
        {
            InitializeComponent();
            PopulateData();          
        }

        private void PopulateData()
        {
            List<FootballClub> clubs = FootballClubDB.GetClubs();
            foreach (FootballClub f in clubs)
            {
                f.StadiumName = FootballClubDB.GetStadiumName(f.ClubId.ToString());
                DataGridXAML.Items.Add(f);
            }
            foreach (Stadium s in stadiums)
            {
                StadiumComboBox.Items.Add(s);
            }
        }

        private void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }
    
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            var name = NameTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var trophies = TrophiesTB.Text;
            var stadium = (Stadium)StadiumComboBox.SelectedItem;
            if (!"".Equals(name) && !"".Equals(trophies) && date != null)
            {
                FootballClub club = new FootballClub(name, date, Int32.Parse(trophies), stadium.StadiumId);
                FootballClubDB.AddClub(club);
                DrawData();
            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedClub = (FootballClub)DataGridXAML.SelectedItem;
                FootballClubDB.DeleteClub(selectedClub.ClubId);
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
                var club = (FootballClub)DataGridXAML.SelectedItem;
                var id = club.ClubId;
                var name = UpdateNameTB.Text;
                var date = UpdateDateBox.SelectedDate.Value;
                var trophies = UpdateTrophiesWonTB.Text;
                var stadium = (Stadium)UpdateStadiumComboBox.SelectedItem;
                FootballClub fc = new FootballClub(id, name, date, Int32.Parse(trophies), stadium.StadiumId);
                FootballClubDB.UpdateClub(fc);
                UpdateNameLabel.Visibility = Visibility.Hidden;
                UpdateNameTB.Visibility = Visibility.Hidden;
                UpdateDateLabel.Visibility = Visibility.Hidden;
                UpdateDateBox.Visibility = Visibility.Hidden;
                UpdateTrophiesWonLabel.Visibility = Visibility.Hidden;
                UpdateTrophiesWonTB.Visibility = Visibility.Hidden;
                UpdateStadiumLabel.Visibility = Visibility.Hidden;
                UpdateStadiumComboBox.Visibility = Visibility.Hidden;
                ConfirmUpdateButton.Visibility = Visibility.Hidden;
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Stadium s in stadiums)
            {
                UpdateStadiumComboBox.Items.Add(s);
            }
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateNameLabel.Visibility = Visibility.Visible;
                UpdateNameTB.Visibility = Visibility.Visible;
                UpdateDateLabel.Visibility = Visibility.Visible;
                UpdateDateBox.Visibility = Visibility.Visible;
                UpdateTrophiesWonLabel.Visibility = Visibility.Visible;
                UpdateTrophiesWonTB.Visibility = Visibility.Visible;
                UpdateStadiumLabel.Visibility = Visibility.Visible;
                UpdateStadiumComboBox.Visibility = Visibility.Visible;
                ConfirmUpdateButton.Visibility = Visibility.Visible;
                var club = (FootballClub)DataGridXAML.SelectedItem;
                UpdateNameTB.Text = club.Name;
                UpdateDateBox.SelectedDate=club.Date;
                UpdateTrophiesWonTB.Text = club.NumTrophies.ToString();
                foreach (Stadium s in stadiums)
                {
                    if(s.StadiumId == club.StadiumId)
                    {
                        UpdateStadiumComboBox.SelectedItem=s;
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
                List<FootballClub> clubs = FootballClubDB.SearchClubs(SearchTB.Text);
                foreach (FootballClub c in clubs)
                {
                    c.StadiumName = FootballClubDB.GetStadiumName(c.ClubId.ToString());
                    DataGridXAML.Items.Add(c);
                }
            }
        }
    }
}
