using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project1_FootballLeague.PlayerInGameWindows
{
    /// <summary>
    /// Interaction logic for PlayerInGameStartWindow.xaml
    /// </summary>
    public partial class PlayerInGameStartWindow : Window
    {
        public PlayerInGameStartWindow()
        {
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            HomePlayersBTN.ClearValue(Button.FontSizeProperty);
            AwayPlayersBTN.ClearValue(Button.FontSizeProperty);
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

        string WhosePlayersLBLAway = "";
        string   WhosePlayersLBLHome = "";
        public void WriteLanguage()
        {
            var PlayerInGameWTitle = "";
            var PlayerInGameWChooseSeasonLBL = "";
            var PlayerInGameWFixtureNumLBL = "";
            var PlayerInGameWFixtureLBL = "";
            var PlayerInGameWHomePlayersBTN = "";

            var PlayerInGameWAwayPlayersBTN = "";
            var PlayerInGameWAddBTN = "";
            var PlayerInGameWUpdateBTN = "";
            var PlayerInGameWDeleteBTN = "";


            var PIGFirstNameCOL = "";
            var PIGLastNameCOL = "";
            var PIGNumGoalsCOL = "";
            var PIGNumAssistsCOL = "";
            var PIGYellowCOL = "";
            var PIGRedCOL = "";
            var PIGStartedGameCOL = "";
            var PIGMinsPlayedCOL = "";

            
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                PlayerInGameWTitle = ConfigurationManager.AppSettings["PlayerInGameWTitle"];
                PlayerInGameWChooseSeasonLBL = ConfigurationManager.AppSettings["PlayerInGameWChooseSeasonLBL"];
                PlayerInGameWFixtureNumLBL = ConfigurationManager.AppSettings["PlayerInGameWFixtureNumLBL"];
                PlayerInGameWFixtureLBL = ConfigurationManager.AppSettings["PlayerInGameWFixtureLBL"];
                PlayerInGameWHomePlayersBTN = ConfigurationManager.AppSettings["PlayerInGameWHomePlayersBTN"];

                PlayerInGameWAwayPlayersBTN = ConfigurationManager.AppSettings["PlayerInGameWAwayPlayersBTN"];
                PlayerInGameWAddBTN = ConfigurationManager.AppSettings["PlayerInGameWAddBTN"];
                PlayerInGameWUpdateBTN = ConfigurationManager.AppSettings["PlayerInGameWUpdateBTN"];
                PlayerInGameWDeleteBTN = ConfigurationManager.AppSettings["PlayerInGameWDeleteBTN"];

                PIGFirstNameCOL = ConfigurationManager.AppSettings["PIGFirstNameCOL"];
                PIGLastNameCOL = ConfigurationManager.AppSettings["PIGLastNameCOL"];
                PIGNumGoalsCOL = ConfigurationManager.AppSettings["PIGNumGoalsCOL"];
                PIGNumAssistsCOL = ConfigurationManager.AppSettings["PIGNumAssistsCOL"];
                PIGYellowCOL = ConfigurationManager.AppSettings["PIGYellowCOL"];
                PIGRedCOL = ConfigurationManager.AppSettings["PIGRedCOL"];
                PIGStartedGameCOL = ConfigurationManager.AppSettings["PIGStartedGameCOL"];
                PIGMinsPlayedCOL = ConfigurationManager.AppSettings["PIGMinsPlayedCOL"];

                WhosePlayersLBLHome = ConfigurationManager.AppSettings["WhosePlayersLBLHome"];
                WhosePlayersLBLAway = ConfigurationManager.AppSettings["WhosePlayersLBLAway"];
            }
            else
            {
                PlayerInGameWTitle = ConfigurationManager.AppSettings["PlayerInGameWTitleSE"];
                PlayerInGameWChooseSeasonLBL = ConfigurationManager.AppSettings["PlayerInGameWChooseSeasonLBLSE"];
                PlayerInGameWFixtureNumLBL = ConfigurationManager.AppSettings["PlayerInGameWFixtureNumLBLSE"];
                PlayerInGameWFixtureLBL = ConfigurationManager.AppSettings["PlayerInGameWFixtureLBLSE"];
                PlayerInGameWHomePlayersBTN = ConfigurationManager.AppSettings["PlayerInGameWHomePlayersBTNSE"];

                PlayerInGameWAwayPlayersBTN = ConfigurationManager.AppSettings["PlayerInGameWAwayPlayersBTNSE"];
                PlayerInGameWAddBTN = ConfigurationManager.AppSettings["PlayerInGameWAddBTNSE"];
                PlayerInGameWUpdateBTN = ConfigurationManager.AppSettings["PlayerInGameWUpdateBTNSE"];
                PlayerInGameWDeleteBTN = ConfigurationManager.AppSettings["PlayerInGameWDeleteBTNSE"];

                PIGFirstNameCOL = ConfigurationManager.AppSettings["PIGFirstNameCOLSE"];
                PIGLastNameCOL = ConfigurationManager.AppSettings["PIGLastNameCOLSE"];
                PIGNumGoalsCOL = ConfigurationManager.AppSettings["PIGNumGoalsCOLSE"];
                PIGNumAssistsCOL = ConfigurationManager.AppSettings["PIGNumAssistsCOLSE"];
                PIGYellowCOL = ConfigurationManager.AppSettings["PIGYellowCOLSE"];
                PIGRedCOL = ConfigurationManager.AppSettings["PIGRedCOLSE"];
                PIGStartedGameCOL = ConfigurationManager.AppSettings["PIGStartedGameCOLSE"];
                PIGMinsPlayedCOL = ConfigurationManager.AppSettings["PIGMinsPlayedCOLSE"];
                WhosePlayersLBLHome = ConfigurationManager.AppSettings["WhosePlayersLBLHomeSE"];
                WhosePlayersLBLAway = ConfigurationManager.AppSettings["WhosePlayersLBLAwaySE"];
            }
            this.Title = PlayerInGameWTitle;
            ChooseSeasonLabel.Content = PlayerInGameWChooseSeasonLBL;
            FixtureNumLabel.Content = PlayerInGameWFixtureNumLBL;
            FixtureLabel.Content = PlayerInGameWFixtureLBL;
            HomePlayersBTN.Content = PlayerInGameWHomePlayersBTN;

            AwayPlayersBTN.Content = PlayerInGameWAwayPlayersBTN;
            AddButton.Content = PlayerInGameWAddBTN;
            UpdateButton.Content = PlayerInGameWUpdateBTN;
            DeleteButton.Content = PlayerInGameWDeleteBTN;

            PIGFirstNameC.Header = PIGFirstNameCOL;
            PIGLastNameC.Header = PIGLastNameCOL;
            PIGNumGoalsC.Header = PIGNumGoalsCOL;
            PIGNumAssistsC.Header = PIGNumAssistsCOL;
            PIGYellowC.Header = PIGYellowCOL;
            PIGRedC.Header = PIGRedCOL;
            PIGStartedGameC.Header = PIGStartedGameCOL;
            PIGMinsPlayedC.Header = PIGMinsPlayedCOL;
        }
        private void PopulateData()
        {
            foreach (var season in SeasonStatsDB.GetSeasons())
            {
                ChooseSeasonBox.Items.Add(season);
            }
            for (int i = 1; i < 10; i++)
            {
                ChooseFixtureNumBox.Items.Add(i);
            }
        }

        public void Clear()
        {
            DataGridXAML.Items.Clear();
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
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null)
            {
                GameInfo gInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                AddPlayerInGameWIndow win = new AddPlayerInGameWIndow(this, gInfo, isHome, (int)ChooseSeasonBox.SelectedItem, (int)ChooseFixtureNumBox.SelectedItem);
                win.ShowDialog();
            }
            else
            {
                ItemNotSelected();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null && DataGridXAML.SelectedItem != null)
            {
                PlayerInGame pig = (PlayerInGame)DataGridXAML.SelectedItem;
                GameInfo gInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                UpdatePlayerInGameWindow win = new UpdatePlayerInGameWindow(this, pig, isHome, gInfo, (int)ChooseSeasonBox.SelectedItem, (int)ChooseFixtureNumBox.SelectedItem);
                win.ShowDialog();
            }
            else
            {
                ItemNotSelected();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && DataGridXAML.SelectedItem != null)
            {
                PlayerInGame player = (PlayerInGame)DataGridXAML.SelectedItem;
                PlayerDB.DeletePlayerInGame(player.PlayerId, player.ClubId, player.GameId);
                DataGridXAML.Items.Remove(player);
            }
            else
            {
                ItemNotSelected();
            }
        }

        private void ChooseSeasonBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clear();
            if (ChooseFixtureNumBox.SelectedItem != null)
            {
                SelectGamesFromSeasonAndFixture();
            }
        }

        private void SelectGamesFromSeasonAndFixture()
        {
            ChooseRealFixtureBox.Items.Clear();
            int season = (int)ChooseSeasonBox.SelectedItem;
            int fixtureNum = (int)ChooseFixtureNumBox.SelectedItem;

            List<GameWithClub> gamesWithClub = GameDB.GetGamesBasedOnSeasonAndFixture(season, fixtureNum);
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
                ChooseRealFixtureBox.Items.Add(gInfo);
            }
        }

        private void ChooseFixtureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clear();
            if (ChooseSeasonBox.SelectedItem != null)
            {
                SelectGamesFromSeasonAndFixture();
            }
        }

        private void ChooseRealFixtureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool isHome = false;
        private void HomePlayersBTN_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            WhosePlayersLBL.Content = WhosePlayersLBLHome;
            isHome = true;
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null)
            {
                GameInfo gameInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                List<PlayerInGame> playersInGame = PlayerDB.GetPlayerFromGame(gameInfo.GameId);
                foreach (PlayerInGame player in playersInGame)
                {
                    if (player.IsFromHomeTeam)
                    {
                        DataGridXAML.Items.Add(player);
                    }

                }
            }
            else
            {
                ItemNotSelected();
            }
        }

        private void AwayPlayersBTN_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            WhosePlayersLBL.Content = WhosePlayersLBLAway;
            isHome = false;
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null)
            {
                GameInfo gameInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                List<PlayerInGame> playersInGame = PlayerDB.GetPlayerFromGame(gameInfo.GameId);
                foreach (PlayerInGame player in playersInGame)
                {
                    if (!player.IsFromHomeTeam)
                    {
                        DataGridXAML.Items.Add(player);
                    }
                }
            }
            else
            {
                ItemNotSelected();
            }
        }
    }
}
