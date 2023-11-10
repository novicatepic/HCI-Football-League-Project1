using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using MySqlX.XDevAPI;
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
    /// Interaction logic for UpdateTeamInGameAndFixtureWindow.xaml
    /// </summary>
    public partial class UpdateTeamInGameAndFixtureWindow : Window
    {
        private ChooseFixtureAndSeasonWindow window;
        private GameInfo gameInfo;
        private int season;
        private int fixture;
        private int currHomeGoals;
        private int currAwayGoals;
        public UpdateTeamInGameAndFixtureWindow(ChooseFixtureAndSeasonWindow win, GameInfo gi, int season, int fixture)
        {
            InitializeComponent();
            window = win;
            gameInfo = gi;
            HomeGoalsTB.Text = gameInfo.HomeTeamGoals.ToString();
            AwayGoalsTB.Text = gameInfo.AwayTeamGoals.ToString();
            currHomeGoals = gameInfo.HomeTeamGoals;
            currAwayGoals = gameInfo.AwayTeamGoals;
            DatePickerBox.SelectedDate = gameInfo.GameDate;
            this.season = season;
            this.fixture = fixture;
            WriteLanguage();
            DrawStyle();


            HomeTeamLBL.Content = UpdateTIGHomeTeamLBL + gi.HomeTeamName;
            AwayTeamLBL.Content = UpdateTIGAwayTeamLBL + gi.AwayTeamName;
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


        string UpdateTIGHomeTeamLBL = "";
        string UpdateTIGAwayTeamLBL = "";
        string AddPlayerFixtureLBL = "";
        string AddPlayerSeasonLBL = "";
        public void WriteLanguage()
        {
            var UpdateTeamInGameWTitle = "";
            var UpdateTeamInGameWHeaderLBL = "";
            var UpdateTeamInGameWHomeGoalsLBL = "";
            var UpdateTeamInGameWAwayGoalsLBL = "";

            var UpdateTeamInGameWGameDateLBL = "";
            var UpdateTeamInGameWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                UpdateTeamInGameWTitle = ConfigurationManager.AppSettings["UpdateTeamInGameWTitle"];
                UpdateTeamInGameWHeaderLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWHeaderLBL"];
                UpdateTeamInGameWHomeGoalsLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWHomeGoalsLBL"];
                UpdateTeamInGameWAwayGoalsLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWAwayGoalsLBL"];
                UpdateTeamInGameWGameDateLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWGameDateLBL"];
                UpdateTeamInGameWSubmitBTN = ConfigurationManager.AppSettings["UpdateTeamInGameWSubmitBTN"];

                UpdateTIGHomeTeamLBL = ConfigurationManager.AppSettings["UpdateTIGHomeTeamLBL"];
                UpdateTIGAwayTeamLBL = ConfigurationManager.AppSettings["UpdateTIGAwayTeamLBL"];
                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBL"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBL"];
            }
            else
            {
                UpdateTeamInGameWTitle = ConfigurationManager.AppSettings["UpdateTeamInGameWTitleSE"];
                UpdateTeamInGameWHeaderLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWHeaderLBLSE"];
                UpdateTeamInGameWHomeGoalsLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWHomeGoalsLBLSE"];
                UpdateTeamInGameWAwayGoalsLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWAwayGoalsLBLSE"];
                UpdateTeamInGameWGameDateLBL = ConfigurationManager.AppSettings["UpdateTeamInGameWGameDateLBLSE"];
                UpdateTeamInGameWSubmitBTN = ConfigurationManager.AppSettings["UpdateTeamInGameWSubmitBTNSE"];

                UpdateTIGHomeTeamLBL = ConfigurationManager.AppSettings["UpdateTIGHomeTeamLBLSE"];
                UpdateTIGAwayTeamLBL = ConfigurationManager.AppSettings["UpdateTIGAwayTeamLBLSE"];
                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBLSE"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBLSE"];
            }
            this.Title = UpdateTeamInGameWTitle;
            HeaderLabel.Content = UpdateTeamInGameWHeaderLBL;
            HomeGoalsLabel.Content = UpdateTeamInGameWHomeGoalsLBL;
            AwayGoalsLabel.Content = UpdateTeamInGameWAwayGoalsLBL;
            GameDateLabel.Content = UpdateTeamInGameWGameDateLBL;
            SubmitBTN.Content = UpdateTeamInGameWSubmitBTN;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var homeGoals = HomeGoalsTB.Text;
            var awayGoals = AwayGoalsTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            int intHomeGoals = Int32.Parse(homeGoals);
            int intAwayGoals = Int32.Parse(awayGoals);
            if (intHomeGoals>=0 && intAwayGoals>=0 && !"".Equals(homeGoals) && !"".Equals(awayGoals) && date != null)
            {
                SeasonStats homeTeamStats = SeasonStatsDB.GetStatsBasedOnClub(gameInfo.HomeClubId, season);
                SeasonStats awayTeamStats = SeasonStatsDB.GetStatsBasedOnClub(gameInfo.AwayClubId, season);
                if (currHomeGoals > currAwayGoals)
                {
                    homeTeamStats.NumWins -= 1;
                    awayTeamStats.NumLoses -= 1;
                    homeTeamStats.NumPoints -= 3;
                }
                else if (currHomeGoals < currAwayGoals)
                {
                    awayTeamStats.NumWins -= 1;
                    homeTeamStats.NumLoses -= 1;
                    awayTeamStats.NumPoints -= 3;
                }
                else
                {
                    homeTeamStats.NumDraws -= 1;
                    awayTeamStats.NumDraws -= 1;
                    homeTeamStats.NumPoints -= 1;
                    awayTeamStats.NumPoints -= 1;
                }
                homeTeamStats.NumScored -= currHomeGoals;
                homeTeamStats.NumConceded -= currAwayGoals;
                awayTeamStats.NumScored -= currAwayGoals;
                awayTeamStats.NumConceded -= currHomeGoals;
                homeTeamStats.NumGamesPlayed -= 1;
                awayTeamStats.NumGamesPlayed -= 1;



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




                ClubInGame cigHome = null;
                ClubInGame cigAway = null;
                List<PlayerInGame> playersFromTeamAndGameHome = new List<PlayerInGame>();
                List<PlayerInGame> playersFromTeamAndGameAway = new List<PlayerInGame>();
                int teamHomeScored = 0;
                int teamAwayScored = 0;

                cigHome = FootballClubDB.GetClubInGame(gameInfo.HomeClubId, gameInfo.GameId);
                teamHomeScored = gameInfo.HomeTeamGoals;

                cigAway = FootballClubDB.GetClubInGame(gameInfo.AwayClubId, gameInfo.GameId);
                teamAwayScored = gameInfo.AwayTeamGoals;

                foreach (PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.HomeClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGameHome.Add(pig);
                }
                foreach (PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.AwayClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGameAway.Add(pig);
                }

                int currGoalsHome = 0;
                int currAssistsHome = 0;
                foreach (PlayerInGame pig in playersFromTeamAndGameHome)
                {
                    currGoalsHome += pig.NumGoalsInGame;
                    currAssistsHome += pig.NumAssistsInGame;
                }

                int currGoalsAway = 0;
                int currAssistsAway = 0;
                foreach (PlayerInGame pig in playersFromTeamAndGameAway)
                {
                    currGoalsAway += pig.NumGoalsInGame;
                    currAssistsAway += pig.NumAssistsInGame;
                }

                if (intHomeGoals < currGoalsHome || intAwayGoals < currGoalsAway)
                {
                    MessageBox.Show("Too little goals and assists!");
                } else
                {
                    SeasonStatsDB.UpdateStats(homeTeamStats);
                    SeasonStatsDB.UpdateStats(awayTeamStats);
                    GameDB.UpdateClubInGame(new ClubInGame(gameInfo.HomeClubId, gameInfo.GameId, Int32.Parse(homeGoals), true));
                    GameDB.UpdateClubInGame(new ClubInGame(gameInfo.AwayClubId, gameInfo.GameId, Int32.Parse(awayGoals), false));
                    GameDB.UpdateGame(new Game(gameInfo.GameId, date, fixture, season));
                    window.DrawData();
                    Close();
                }            

            }
            else
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
    }
}
