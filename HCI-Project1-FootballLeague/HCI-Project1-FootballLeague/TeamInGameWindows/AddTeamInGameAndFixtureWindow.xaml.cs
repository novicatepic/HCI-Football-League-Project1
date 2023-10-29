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

namespace HCI_Project1_FootballLeague.TeamInGameWindows
{
    /// <summary>
    /// Interaction logic for AddTeamInGameAndFixtureWindow.xaml
    /// </summary>
    public partial class AddTeamInGameAndFixtureWindow : Window
    {
        ChooseFixtureAndSeasonWindow window;
        int season;
        int fixture;
        private List<FootballClub> availableClubs = new List<FootballClub>();
        public AddTeamInGameAndFixtureWindow(ChooseFixtureAndSeasonWindow win, int season, int fixture)
        {
            InitializeComponent();
            this.season = season;
            this.fixture = fixture;
            window = win;
            availableClubs = GameDB.GetFreeClubsFromFixtureAndSeason(fixture, season);
            List<FootballClub> clubs = FootballClubDB.GetClubs();
            foreach(FootballClub club in clubs)
            {
                bool found = false;
                foreach(FootballClub f in availableClubs)
                {
                    if(f.Name.Equals(club.Name))
                    {
                        found = true;
                    }
                }
                if(!found)
                {
                    HomeClubComboBox.Items.Add(club);
                    AwayClubComboBox.Items.Add(club);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FootballClub homeTeam = (FootballClub)HomeClubComboBox.SelectedItem;
            FootballClub awayTeam = (FootballClub)AwayClubComboBox.SelectedItem;
            var homeGoals = HomeGoalsTB.Text;
            var awayGoals = AwayGoalsTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            int intHomeGoals = Int32.Parse(homeGoals);
            int intAwayGoals = Int32.Parse(awayGoals);
            if (intHomeGoals>0 && intAwayGoals>0 && homeTeam != null && awayTeam != null && !"".Equals(homeGoals) && !"".Equals(awayGoals) && date != null)
            {
                int gId = GameDB.AddGame(new Game(date, fixture, season));
                GameDB.AddClubInGame(new ClubInGame(homeTeam.ClubId, gId, Int32.Parse(homeGoals), true));
                GameDB.AddClubInGame(new ClubInGame(awayTeam.ClubId, gId, Int32.Parse(awayGoals), false));
                window.DrawData();
                Close();

            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }
    }
}
