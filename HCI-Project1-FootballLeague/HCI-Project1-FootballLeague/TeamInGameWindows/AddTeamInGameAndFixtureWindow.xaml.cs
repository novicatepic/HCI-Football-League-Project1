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

            List<ClubInGame> homeClubMatches = GameDB.GetClubInGameBasedOnClubId(homeTeam.ClubId);
            List<ClubInGame> awayClubMatches = GameDB.GetClubInGameBasedOnClubId(awayTeam.ClubId);
            bool found = false;
            foreach(ClubInGame cig1 in homeClubMatches)
            {
                foreach(ClubInGame cig2 in awayClubMatches)
                {
                    if(cig1.GameId == cig2.GameId && cig1.IsHomeTeam)
                    {
                        found = true;
                    }
                }
            }
            if(!found)
            {
                var homeGoals = HomeGoalsTB.Text;
                var awayGoals = AwayGoalsTB.Text;
                var date = DatePickerBox.SelectedDate.Value;
                int intHomeGoals = Int32.Parse(homeGoals);
                int intAwayGoals = Int32.Parse(awayGoals);
                if (intHomeGoals >= 0 && intAwayGoals >= 0 && homeTeam != null && awayTeam != null && !"".Equals(homeGoals) && !"".Equals(awayGoals) && date != null)
                {
                    SeasonStats homeTeamStats = SeasonStatsDB.GetStatsBasedOnClub(homeTeam.ClubId);
                    SeasonStats awayTeamStats = SeasonStatsDB.GetStatsBasedOnClub(awayTeam.ClubId);
                    if (intHomeGoals > intAwayGoals)
                    {
                        homeTeamStats.NumWins += 1;
                        awayTeamStats.NumLoses += 1;
                        homeTeamStats.NumPoints += 3;
                    }
                    else if (intHomeGoals < intAwayGoals)
                    {
                        awayTeamStats.NumWins += 1;
                        homeTeamStats.NumLoses += 1;
                        awayTeamStats.NumPoints += 3;
                    }
                    else
                    {
                        homeTeamStats.NumDraws += 1;
                        awayTeamStats.NumDraws += 1;
                        homeTeamStats.NumPoints += 1;
                        awayTeamStats.NumPoints += 1;
                    }
                    homeTeamStats.NumScored += intHomeGoals;
                    homeTeamStats.NumConceded += intAwayGoals;
                    awayTeamStats.NumScored += intAwayGoals;
                    awayTeamStats.NumConceded += intHomeGoals;
                    homeTeamStats.NumGamesPlayed += 1;
                    awayTeamStats.NumGamesPlayed += 1;

                    SeasonStatsDB.UpdateStats(homeTeamStats);
                    SeasonStatsDB.UpdateStats(awayTeamStats);

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
            } else
            {
                MessageBox.Show("Team already played!");
            }

            
        }
    }
}
