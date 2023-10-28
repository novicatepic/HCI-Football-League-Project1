using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StatsWindows;
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
    /// Interaction logic for ChooseFixtureAndSeasonWindow.xaml
    /// </summary>
    public partial class ChooseFixtureAndSeasonWindow : Window
    {
        public ChooseFixtureAndSeasonWindow()
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
            for(int i=1; i<10; i++)
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
            MessageBox.Show("Item not selected!");
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
                GameInfo gameInfo = (GameInfo)DataGridXAML.SelectedItem;
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
    }
}
