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

namespace HCI_Project1_FootballLeague.PlayerInGameWindows
{
    /// <summary>
    /// Interaction logic for AddPlayerInGameWIndow.xaml
    /// </summary>
    public partial class AddPlayerInGameWIndow : Window
    {
        private PlayerInGameStartWindow window;
        private GameInfo gameInfo;
        private bool isHomePlayer;
        public AddPlayerInGameWIndow(PlayerInGameStartWindow win, GameInfo gameInfo, bool isHomePlayer)
        {
            InitializeComponent();
            window = win;
            this.gameInfo = gameInfo;
            this.isHomePlayer = isHomePlayer;
            PopulateData();
        }

        private void PopulateData()
        {
            if(isHomePlayer)
            {
                AddPlayersToComboBox(gameInfo.HomeClubId);

            }
            else
            {
                AddPlayersToComboBox(gameInfo.AwayClubId);
            }
        }

        private void AddPlayersToComboBox(int clubId)
        {
            List<Player> players = PlayerDB.GetPlayersFromClub(clubId);
            List<Player> playersWhoPlayed = PlayerDB.GetPlayersWhoPlayedBasedOnGameAndClub(gameInfo.GameId, clubId);
            foreach (Player p in players)
            {
                
                bool found = false;
                foreach (Player player in playersWhoPlayed)
                {
                    if (p.PlayerId == player.PlayerId)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    PlayerComboBox.Items.Add(p);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var goals = NumGoalsTB.Text;
            var assists = NumAssistsTB.Text;
            var minutesPlayed = MinutesPlayedTB.Text;
            var player = (Player)PlayerComboBox.SelectedItem;
            bool gotYellow = false;
            if((bool)YellowCardCB.IsChecked)
            {
                gotYellow = true;
            }
            bool gotRed = false;
            if ((bool)RedCardCB.IsChecked)
            {
                gotRed = true;
            }
            bool startedGame = false;
            if ((bool)StartedGameCB.IsChecked)
            {
                startedGame = true;
            }
            ClubInGame cig = null;
            List<PlayerInGame> playersFromTeamAndGame = new List<PlayerInGame>();
            int teamScored = 0;
            if (isHomePlayer)
            {
                cig = FootballClubDB.GetClubInGame(gameInfo.HomeClubId, gameInfo.GameId);
                foreach(PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.HomeClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGame.Add(pig);
                }
                teamScored = gameInfo.HomeTeamGoals;
            } else
            {
                cig = FootballClubDB.GetClubInGame(gameInfo.AwayClubId, gameInfo.GameId);
                foreach (PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.AwayClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGame.Add(pig);
                }
                teamScored = gameInfo.AwayTeamGoals;
            }
            int currGoals = 0;
            int currAssists = 0;
            foreach(PlayerInGame pig in playersFromTeamAndGame)
            {
                currGoals += pig.NumGoalsInGame;
                currAssists += pig.NumAssistsInGame;
            }
            int intGoals = Int32.Parse(goals);
            int intAssists = Int32.Parse(assists);
            int intMinutesPlayer = Int32.Parse(minutesPlayed);
            
            if (intGoals>=0 && intAssists>=0 && intMinutesPlayer>=0 && intMinutesPlayer <=90 && !"".Equals(goals) && !"".Equals(assists) && !"".Equals(minutesPlayed) && player != null)
            {
                if(intGoals + currGoals > teamScored || intAssists + currAssists > teamScored)
                {
                    MessageBox.Show("Too many goals and assists!");
                } else
                {
                    PlayerInGame pig = new PlayerInGame(player.PlayerId, Int32.Parse(goals), Int32.Parse(assists), gotYellow, gotRed, player.ClubId, gameInfo.GameId, startedGame, Int32.Parse(minutesPlayed));
                    pig.FirstName = player.FirstName;
                    pig.LastName = player.LastName;
                    PlayerDB.AddPlayerInGame(pig);
                    window.DataGridXAML.Items.Add(pig);
                    Close();
                }           
            }
            else
            {
                MessageBox.Show("One of required inputs not entered correctly!");
            }
        }
    }
}
