
using HCI_Project1_FootballLeague.PlayerWindows;
using HCI_Project1_FootballLeague.StadiumWindows;
using HCI_Project1_FootballLeague.StatsWindows;
using HCI_Project1_FootballLeague.TeamInGameWindows;
using HCI_Project1_FootballLeague.TeamWindows;
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

namespace HCI_Project1_FootballLeague.AdminsStartWindows
{
    /// <summary>
    /// Interaction logic for LeagueAdminStartWindow.xaml
    /// </summary>
    public partial class LeagueAdminStartWindow : Window
    {
        private MainWindow window;
        public LeagueAdminStartWindow(MainWindow win)
        {
            InitializeComponent();
            window = win;
            WriteLanguage();
        }

        public void WriteLanguage()
        {
            var LeagueAdminSWTitle = "";
            var LeagueAdminSWStadiumsBTN = "";
            var LeagueAdminSWClubBTN = "";
            var LeagueAdminSWStatsBTN = "";
            var LeagueAdminSWGamesBTN = "";
            var LeagueAdminSWPlayersBTN = "";
            var LeagueAdminSWPlayersInGameBTN = "";
            var LeagueAdminSWOptionsBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                LeagueAdminSWTitle = ConfigurationManager.AppSettings["LeagueAdminSWTitle"];
                LeagueAdminSWStadiumsBTN = ConfigurationManager.AppSettings["LeagueAdminSWStadiumsBTN"];
                LeagueAdminSWClubBTN = ConfigurationManager.AppSettings["LeagueAdminSWClubBTN"];
                LeagueAdminSWStatsBTN = ConfigurationManager.AppSettings["LeagueAdminSWStatsBTN"];
                LeagueAdminSWGamesBTN = ConfigurationManager.AppSettings["LeagueAdminSWGamesBTN"];
                LeagueAdminSWPlayersBTN = ConfigurationManager.AppSettings["LeagueAdminSWPlayersBTN"];
                LeagueAdminSWPlayersInGameBTN = ConfigurationManager.AppSettings["LeagueAdminSWPlayersInGameBTN"];
                LeagueAdminSWOptionsBTN = ConfigurationManager.AppSettings["LeagueAdminSWOptionsBTN"];
            }
            else
            {
                LeagueAdminSWTitle = ConfigurationManager.AppSettings["LeagueAdminSWTitleSE"];
                LeagueAdminSWStadiumsBTN = ConfigurationManager.AppSettings["LeagueAdminSWStadiumsBTNSE"];
                LeagueAdminSWClubBTN = ConfigurationManager.AppSettings["LeagueAdminSWClubBTNSE"];
                LeagueAdminSWStatsBTN = ConfigurationManager.AppSettings["LeagueAdminSWStatsBTNSE"];
                LeagueAdminSWGamesBTN = ConfigurationManager.AppSettings["LeagueAdminSWGamesBTNSE"];
                LeagueAdminSWPlayersBTN = ConfigurationManager.AppSettings["LeagueAdminSWPlayersBTNSE"];
                LeagueAdminSWPlayersInGameBTN = ConfigurationManager.AppSettings["LeagueAdminSWPlayersInGameBTNSE"];
                LeagueAdminSWOptionsBTN = ConfigurationManager.AppSettings["LeagueAdminSWOptionsBTNSE"];
            }
            this.Title = LeagueAdminSWTitle;
            StadiumButton.Content = LeagueAdminSWStadiumsBTN;
            ClubsButton.Content = LeagueAdminSWClubBTN;
            StatsButton.Content = LeagueAdminSWStatsBTN;
            GamesButton.Content = LeagueAdminSWGamesBTN;
            PlayersButton.Content = LeagueAdminSWPlayersBTN;
            PlayersInGameButton.Content = LeagueAdminSWPlayersInGameBTN;
            OptionsButton.Content = LeagueAdminSWOptionsBTN;

            window.WriteLanguage();
        }

        private void StadiumButton_Click(object sender, RoutedEventArgs e)
        {
            StadiumWindow sw = new StadiumWindow();
            sw.ShowDialog();
        }

        private void ClubsButton_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow tw = new TeamWindow();
            tw.ShowDialog();
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            StatsWindow sw = new StatsWindow();
            sw.ShowDialog();
        }

        private void PlayersButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerShowWindow psw = new PlayerShowWindow();
            psw.ShowDialog();
        }

        private void GamesButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseFixtureAndSeasonWindow scw = new ChooseFixtureAndSeasonWindow();
            scw.ShowDialog();
        }

        private void PlayersInGameButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerInGameWindows.PlayerInGameStartWindow win = new PlayerInGameWindows.PlayerInGameStartWindow();
            win.ShowDialog();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.OptionsWindow win = new OptionsWindow.OptionsWindow(null, this);
            win.ShowDialog();
        }
    }
}
