using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            WriteLanguage();
            DrawStyle();
            FixtureNumLBL.Content = AddPlayerFixtureLBL + fixture;
            SeasonLBL.Content = AddPlayerSeasonLBL + season;
        }

        public void DrawStyle()
        {
            SubmitBTN.ClearValue(Button.FontSizeProperty);
            Style backgroundStyle = null;
            Style buttonStyle = null;
            if ("Large Buttons - Alice Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundAlice");
                buttonStyle = (Style)FindResource("FontLargeBtn");
            }
            else if ("Medium Buttons - Beige Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundBeige");
                buttonStyle = (Style)FindResource("FontMediumBtn");
            }
            else
            {
                backgroundStyle = (Style)FindResource("BackgroundTan");
                buttonStyle = (Style)FindResource("FontSmallBtn");
            }
            Grid.Style = backgroundStyle;
            foreach (UIElement element in Grid.Children)
            {
                if (element is Button)
                {
                    Button button = (Button)element;
                    button.Style = buttonStyle;
                }
            }
        }

        string AddPlayerFixtureLBL = "";
        string AddPlayerSeasonLBL = "";
        public void WriteLanguage()
        {
            var AddTeamInGameWTitle = "";
            var AddTeamInGameWHeaderLBL = "";
            var AddTeamInGameWHomeTeamLBL = "";
            var AddTeamInGameWAwayTeamBL = "";

            var AddTeamInGameWHomeGoalsLBL = "";
            var AddTeamInGameWAwayGoalsLBL = "";
            var AddTeamInGameWGameDateLBL = "";
            var AddTeamInGameWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                AddTeamInGameWTitle = ConfigurationManager.AppSettings["AddTeamInGameWTitle"];
                AddTeamInGameWHeaderLBL = ConfigurationManager.AppSettings["AddTeamInGameWHeaderLBL"];
                AddTeamInGameWHomeTeamLBL = ConfigurationManager.AppSettings["AddTeamInGameWHomeTeamLBL"];
                AddTeamInGameWAwayTeamBL = ConfigurationManager.AppSettings["AddTeamInGameWAwayTeamBL"];
                AddTeamInGameWHomeGoalsLBL = ConfigurationManager.AppSettings["AddTeamInGameWHomeGoalsLBL"];
                AddTeamInGameWAwayGoalsLBL = ConfigurationManager.AppSettings["AddTeamInGameWAwayGoalsLBL"];
                AddTeamInGameWGameDateLBL = ConfigurationManager.AppSettings["AddTeamInGameWGameDateLBL"];
                AddTeamInGameWSubmitBTN = ConfigurationManager.AppSettings["AddTeamInGameWSubmitBTN"];

                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBL"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBL"];
            }
            else
            {
                AddTeamInGameWTitle = ConfigurationManager.AppSettings["AddTeamInGameWTitleSE"];
                AddTeamInGameWHeaderLBL = ConfigurationManager.AppSettings["AddTeamInGameWHeaderLBLSE"];
                AddTeamInGameWHomeTeamLBL = ConfigurationManager.AppSettings["AddTeamInGameWHomeTeamLBLSE"];
                AddTeamInGameWAwayTeamBL = ConfigurationManager.AppSettings["AddTeamInGameWAwayTeamBLSE"];
                AddTeamInGameWHomeGoalsLBL = ConfigurationManager.AppSettings["AddTeamInGameWHomeGoalsLBLSE"];
                AddTeamInGameWAwayGoalsLBL = ConfigurationManager.AppSettings["AddTeamInGameWAwayGoalsLBLSE"];
                AddTeamInGameWGameDateLBL = ConfigurationManager.AppSettings["AddTeamInGameWGameDateLBLSE"];
                AddTeamInGameWSubmitBTN = ConfigurationManager.AppSettings["AddTeamInGameWSubmitBTNSE"];

                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBLSE"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBLSE"];
            }
            this.Title = AddTeamInGameWTitle;
            HeaderLabel.Content = AddTeamInGameWHeaderLBL;
            HomeTeamLabel.Content = AddTeamInGameWHomeTeamLBL;
            AwayTeamLabel.Content = AddTeamInGameWAwayTeamBL;
            HomeGoalsLabel.Content = AddTeamInGameWHomeGoalsLBL;
            AwayGoalsLabel.Content = AddTeamInGameWAwayGoalsLBL;
            GameDateLabel.Content = AddTeamInGameWGameDateLBL;
            SubmitBTN.Content = AddTeamInGameWSubmitBTN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FootballClub homeTeam = (FootballClub)HomeClubComboBox.SelectedItem;
            FootballClub awayTeam = (FootballClub)AwayClubComboBox.SelectedItem;

            if(homeTeam != null && awayTeam != null && homeTeam.ClubId != awayTeam.ClubId)
            {
                List<ClubInGame> homeClubMatches = GameDB.GetClubInGameBasedOnClubId(homeTeam.ClubId, season);
                List<ClubInGame> awayClubMatches = GameDB.GetClubInGameBasedOnClubId(awayTeam.ClubId, season);
                bool found = false;
                foreach (ClubInGame cig1 in homeClubMatches)
                {
                    foreach (ClubInGame cig2 in awayClubMatches)
                    {
                        if (cig1.GameId == cig2.GameId && cig1.IsHomeTeam)
                        {
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    try
                    {
                        var homeGoals = HomeGoalsTB.Text;
                        var awayGoals = AwayGoalsTB.Text;
                        var date = DatePickerBox.SelectedDate.Value;
                        int intHomeGoals = Int32.Parse(homeGoals);
                        int intAwayGoals = Int32.Parse(awayGoals);
                        if (intHomeGoals >= 0 && intAwayGoals >= 0 && homeTeam != null && awayTeam != null && !"".Equals(homeGoals) && !"".Equals(awayGoals) && date != null)
                        {
                            SeasonStats homeTeamStats = SeasonStatsDB.GetStatsBasedOnClub(homeTeam.ClubId, season);
                            SeasonStats awayTeamStats = SeasonStatsDB.GetStatsBasedOnClub(awayTeam.ClubId, season);

                            if (homeTeamStats == null)
                            {
                                SeasonStatsDB.AddStats(new SeasonStats(0, 0, 0, 0, 0, 0, homeTeam.ClubId, 0, season));
                                homeTeamStats = SeasonStatsDB.GetStatsBasedOnClub(homeTeam.ClubId, season);
                            }
                            if (awayTeamStats == null)
                            {
                                SeasonStatsDB.AddStats(new SeasonStats(0, 0, 0, 0, 0, 0, awayTeam.ClubId, 0, season));
                                awayTeamStats = SeasonStatsDB.GetStatsBasedOnClub(awayTeam.ClubId, season);
                            }
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
                            NoInputMessage();
                        }
                    } catch(Exception ex)
                    {
                        NoInputMessage();
                    }
                    
                }
                else
                {
                    TeamPlayedMessage();
                }
            } else
            {
                NoInputMessage();
            }

            

            
        }

        private void NoInputMessage()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }

        private void TeamPlayedMessage()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["TeamPlayedMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["TeamPlayedMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }
    }
}
