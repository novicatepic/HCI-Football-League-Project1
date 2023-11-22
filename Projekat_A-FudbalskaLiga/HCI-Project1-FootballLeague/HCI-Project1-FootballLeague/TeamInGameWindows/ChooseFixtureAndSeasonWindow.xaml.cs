using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StatsWindows;
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
    /// Interaction logic for ChooseFixtureAndSeasonWindow.xaml
    /// </summary>
    public partial class ChooseFixtureAndSeasonWindow : Window
    {
        public ChooseFixtureAndSeasonWindow()
        {
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            //NextButton.ClearValue(Button.FontSizeProperty);
            AddButton.ClearValue(Button.FontSizeProperty);
            UpdateButton.ClearValue(Button.FontSizeProperty);
            DeleteButton.ClearValue(Button.FontSizeProperty);
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
            foreach (UIElement element in InnerGRID.Children)
            {
                if (element is Button)
                {
                    Button button = (Button)element;
                    button.Style = buttonStyle;
                }
            }
        }
        public void WriteLanguage()
        {
            var TeamInGameWTitle = "";
            var TeamInGameWChooseSeasonLBL = "";
            var TeamInGameWFixtureLBL = "";
            var TeamInGameWShowBTN = "";

            var TeamInGameWAddBTN = "";
            var TeamInGameWUpdateBTN = "";
            var TeamInGameWDeleteBTN = "";

            var GameHomeTeamCOL = "";
            var GameAwayTeamCOL = "";
            var GameResultCOL = "";
            var GameDateCOL = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                TeamInGameWTitle = ConfigurationManager.AppSettings["TeamInGameWTitle"];
                TeamInGameWChooseSeasonLBL = ConfigurationManager.AppSettings["TeamInGameWChooseSeasonLBL"];
                TeamInGameWFixtureLBL = ConfigurationManager.AppSettings["TeamInGameWFixtureLBL"];
                TeamInGameWShowBTN = ConfigurationManager.AppSettings["TeamInGameWShowBTN"];
                TeamInGameWAddBTN = ConfigurationManager.AppSettings["TeamInGameWAddBTN"];
                TeamInGameWUpdateBTN = ConfigurationManager.AppSettings["TeamInGameWUpdateBTN"];
                TeamInGameWDeleteBTN = ConfigurationManager.AppSettings["TeamInGameWDeleteBTN"];

                GameHomeTeamCOL = ConfigurationManager.AppSettings["GameHomeTeamCOL"];
                GameAwayTeamCOL = ConfigurationManager.AppSettings["GameAwayTeamCOL"];
                GameResultCOL = ConfigurationManager.AppSettings["GameResultCOL"];
                GameDateCOL = ConfigurationManager.AppSettings["GameDateCOL"];
            }
            else
            {
                TeamInGameWTitle = ConfigurationManager.AppSettings["TeamInGameWTitleSE"];
                TeamInGameWChooseSeasonLBL = ConfigurationManager.AppSettings["TeamInGameWChooseSeasonLBLSE"];
                TeamInGameWFixtureLBL = ConfigurationManager.AppSettings["TeamInGameWFixtureLBLSE"];
                TeamInGameWShowBTN = ConfigurationManager.AppSettings["TeamInGameWShowBTNSE"];
                TeamInGameWAddBTN = ConfigurationManager.AppSettings["TeamInGameWAddBTNSE"];
                TeamInGameWUpdateBTN = ConfigurationManager.AppSettings["TeamInGameWUpdateBTNSE"];
                TeamInGameWDeleteBTN = ConfigurationManager.AppSettings["TeamInGameWDeleteBTNSE"];

                GameHomeTeamCOL = ConfigurationManager.AppSettings["GameHomeTeamCOLSE"];
                GameAwayTeamCOL = ConfigurationManager.AppSettings["GameAwayTeamCOLSE"];
                GameResultCOL = ConfigurationManager.AppSettings["GameResultCOLSE"];
                GameDateCOL = ConfigurationManager.AppSettings["GameDateCOLSE"];
            }
            this.Title = TeamInGameWTitle;
            ChooseSeasonLabel.Content = TeamInGameWChooseSeasonLBL;
            //NextButton.Content = TeamInGameWShowBTN;
            AddButton.Content = TeamInGameWAddBTN;
            UpdateButton.Content = TeamInGameWUpdateBTN;
            DeleteButton.Content = TeamInGameWDeleteBTN;
            FixtureLabel.Content = TeamInGameWFixtureLBL;

            GameHomeTeamC.Header = GameHomeTeamCOL;
            GameAwayTeamC.Header = GameAwayTeamCOL;
            GameResultC.Header = GameResultCOL;
            GameDateC.Header = GameDateCOL;

        }

        private void PopulateData()
        {
            foreach (var season in SeasonStatsDB.GetSeasons())
            {
                ChooseSeasonBox.Items.Add(season);
            }
            for (int i=1; i<10; i++)
            {
                ChooseFixtureBox.Items.Add(i);
            }
        }

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            DrawGrid();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
           DataGridXAML.Items.Clear();
           if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureBox != null)
            {
                DrawGrid();

            }
            else
            {
                ItemNotSelected();
            }
        }

        private void DrawGrid()
        {
            List<GameWithClub> gamesWithClub = GameDB.GetGamesBasedOnSeasonAndFixture((int)ChooseSeasonBox.SelectedItem, (int)ChooseFixtureBox.SelectedItem);
            List<GameInfo> gameInformations = new List<GameInfo>();
            for (int i = 0; i < gamesWithClub.Count; i++)
            {
                for (int j = 0; j < gamesWithClub.Count; j++)
                {
                    if (i > j && gamesWithClub[i].GameId == gamesWithClub[j].GameId)
                    {
                        if (gamesWithClub[i].IsHomeTeam)
                        {
                            GameInfo gi = new GameInfo(gamesWithClub[i].ClubId, gamesWithClub[j].ClubId, gamesWithClub[i].GameId,
                                gamesWithClub[i].TeamName, gamesWithClub[j].TeamName, gamesWithClub[i].GoalsScored, gamesWithClub[j].GoalsScored, gamesWithClub[i].GameDate);
                            gameInformations.Add(gi);
                        }
                        else
                        {
                            GameInfo gi = new GameInfo(gamesWithClub[j].ClubId, gamesWithClub[i].ClubId, gamesWithClub[j].GameId,
                                gamesWithClub[j].TeamName, gamesWithClub[i].TeamName, gamesWithClub[j].GoalsScored, gamesWithClub[i].GoalsScored, gamesWithClub[j].GameDate);
                            gameInformations.Add(gi);
                        }

                    }
                }
            }
            foreach (GameInfo gInfo in gameInformations)
            {
                DataGridXAML.Items.Add(gInfo);
            }
        }

        private static void ItemNotSelected()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoSelectionMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoSelectionMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
     
            if(ChooseSeasonBox.SelectedItem != null && ChooseFixtureBox.SelectedItem != null)
            {
                int season = (int)ChooseSeasonBox.SelectedItem;
                int fixture = (int)ChooseFixtureBox.SelectedItem;
                AddTeamInGameAndFixtureWindow a = new AddTeamInGameAndFixtureWindow(this, season, fixture);
                a.ShowDialog();
            } else
            {
                ItemNotSelected();
            }
            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureBox.SelectedItem != null && DataGridXAML.SelectedItem != null)
            {
                int season = (int)ChooseSeasonBox.SelectedItem;
                int fixture = (int)ChooseFixtureBox.SelectedItem;
                GameInfo gameInfo = (GameInfo)DataGridXAML.SelectedItem;
                UpdateTeamInGameAndFixtureWindow win = new UpdateTeamInGameAndFixtureWindow(this, gameInfo, season, fixture);
                win.ShowDialog();
            } else
            {
                ItemNotSelected();
            }
                
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureBox.SelectedItem != null && DataGridXAML.SelectedItem != null)
            {
                int season = (int)ChooseSeasonBox.SelectedItem;
                GameInfo gInfo = (GameInfo)DataGridXAML.SelectedItem;
                SeasonStats homeTeamStats = SeasonStatsDB.GetStatsBasedOnClub(gInfo.HomeClubId, season);
                SeasonStats awayTeamStats = SeasonStatsDB.GetStatsBasedOnClub(gInfo.AwayClubId, season);
                if (gInfo.HomeTeamGoals > gInfo.AwayTeamGoals)
                {
                    homeTeamStats.NumWins -= 1;
                    awayTeamStats.NumLoses -= 1;
                    homeTeamStats.NumPoints -= 3;
                }
                else if (gInfo.HomeTeamGoals < gInfo.AwayTeamGoals)
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
                homeTeamStats.NumScored -= gInfo.HomeTeamGoals;
                homeTeamStats.NumConceded -= gInfo.AwayTeamGoals;
                awayTeamStats.NumScored -= gInfo.AwayTeamGoals;
                awayTeamStats.NumConceded -= gInfo.HomeTeamGoals;
                homeTeamStats.NumGamesPlayed -= 1;
                awayTeamStats.NumGamesPlayed -= 1;

                SeasonStatsDB.UpdateStats(homeTeamStats);
                SeasonStatsDB.UpdateStats(awayTeamStats);
                GameInfo gameInfo = (GameInfo)DataGridXAML.SelectedItem;
                PlayerDB.DeleteAllPlayersFromGame(gameInfo.GameId);
                GameDB.DeleteClubInGame(gameInfo.HomeClubId, gameInfo.GameId);
                GameDB.DeleteClubInGame(gameInfo.AwayClubId, gameInfo.GameId);
                GameDB.DeleteGame(gameInfo.GameId);
                DrawData();
            }
            else
            {
                ItemNotSelected();
            }
        }

        private void ChooseSeasonBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ChooseFixtureBox.SelectedItem != null)
            {
                DrawTable();
            }
        }

        private void DrawTable()
        {
            DataGridXAML.Items.Clear();
            DrawGrid();
        }

        private void ChooseFixtureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ChooseSeasonBox.SelectedItem != null)
            {
                DrawTable();
            }
        }
    }
}
