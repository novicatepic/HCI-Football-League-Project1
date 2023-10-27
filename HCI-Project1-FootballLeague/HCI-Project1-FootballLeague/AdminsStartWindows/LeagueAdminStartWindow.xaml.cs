using HCI_Project1_FootballLeague.ChooseSeasonWindow;
using HCI_Project1_FootballLeague.PlayerWindows;
using HCI_Project1_FootballLeague.StadiumWindows;
using HCI_Project1_FootballLeague.StatsWindows;
using HCI_Project1_FootballLeague.TeamInGameWindows;
using HCI_Project1_FootballLeague.TeamWindows;
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

namespace HCI_Project1_FootballLeague.AdminsStartWindows
{
    /// <summary>
    /// Interaction logic for LeagueAdminStartWindow.xaml
    /// </summary>
    public partial class LeagueAdminStartWindow : Window
    {
        public LeagueAdminStartWindow()
        {
            InitializeComponent();
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
            /*SeasonChooserWindow scw = new SeasonChooserWindow();
            scw.ShowDialog();*/
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
    }
}
