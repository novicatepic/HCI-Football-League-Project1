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
    /// Interaction logic for UpdatePlayerInGameWindow.xaml
    /// </summary>
    public partial class UpdatePlayerInGameWindow : Window
    {
        private PlayerInGameStartWindow window;
        private PlayerInGame player;
        public UpdatePlayerInGameWindow(PlayerInGameStartWindow win, PlayerInGame pig)
        {
            InitializeComponent();
            window = win;
            this.player = pig;
            PopulateData();
        }

        private void PopulateData()
        {
            NumGoalsTB.Text = player.NumGoalsInGame.ToString();
            NumAssistsTB.Text = player.NumAssistsInGame.ToString();
            MinutesPlayedTB.Text = player.MinutesPlayed.ToString();
            if(player.HasYellow)
            {
                YellowCardCB.IsChecked = true;
            } else
            {
                YellowCardCB.IsChecked = false;
            }
            if (player.HasRed)
            {
                RedCardCB.IsChecked = true;
            }
            else
            {
                RedCardCB.IsChecked = false;
            }
            if(player.StartedGame)
            {
                StartedGameCB.IsChecked = true;
            } else
            {
                StartedGameCB.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var goals = NumGoalsTB.Text;
            var assists = NumAssistsTB.Text;
            var minutesPlayed = MinutesPlayedTB.Text;
            bool gotYellow = false;
            if ((bool)YellowCardCB.IsChecked)
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
            if (!"".Equals(goals) && !"".Equals(assists) && !"".Equals(minutesPlayed))
            {
                PlayerInGame pig = new PlayerInGame(player.PlayerId, Int32.Parse(goals), Int32.Parse(assists), gotYellow, gotRed, player.ClubId, player.GameId, startedGame, Int32.Parse(minutesPlayed));
                pig.FirstName = player.FirstName;
                pig.LastName = player.LastName;
                PlayerDB.UpdatePlayerInGame(pig);
                foreach(var player in window.DataGridXAML.Items)
                {
                    PlayerInGame p = (PlayerInGame)player;
                    if(p.PlayerId == pig.PlayerId)
                    {
                        window.DataGridXAML.Items.Remove(player);
                        break;
                    }
                }
                window.DataGridXAML.Items.Add(pig);
                Close();
            }
            else
            {
                MessageBox.Show("One of required inputs not entered!");
            }
        }
    }
}
