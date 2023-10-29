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
            if (!"".Equals(goals) && !"".Equals(assists) && !"".Equals(minutesPlayed) && player != null)
            {
                PlayerInGame pig = new PlayerInGame(player.PlayerId, Int32.Parse(goals), Int32.Parse(assists), gotYellow, gotRed, player.ClubId, gameInfo.GameId, startedGame, Int32.Parse(minutesPlayed));
                PlayerDB.AddPlayerInGame(pig);
                window.DataGridXAML.Items.Add(pig);
                Close();
            }
            else
            {
                MessageBox.Show("One of required inputs not entered correctly!");
            }
        }
    }
}
