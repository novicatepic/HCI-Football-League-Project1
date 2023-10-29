using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.TeamInGameWindows;
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
            MessageBox.Show("Item not selected!");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null)
            {
                GameInfo gInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                AddPlayerInGameWIndow win = new AddPlayerInGameWIndow(this, gInfo, isHome);
                win.ShowDialog();
            } else
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
                UpdatePlayerInGameWindow win = new UpdatePlayerInGameWindow(this, pig, isHome, gInfo);
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
            if(ChooseFixtureNumBox.SelectedItem != null)
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
            if(ChooseSeasonBox.SelectedItem != null)
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
            isHome = true;
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureNumBox.SelectedItem != null && ChooseRealFixtureBox.SelectedItem != null)
            {
                GameInfo gameInfo = (GameInfo)ChooseRealFixtureBox.SelectedItem;
                List<PlayerInGame> playersInGame = PlayerDB.GetPlayerFromGame(gameInfo.GameId);
                foreach (PlayerInGame player in playersInGame)
                {
                    if(player.IsFromHomeTeam)
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
