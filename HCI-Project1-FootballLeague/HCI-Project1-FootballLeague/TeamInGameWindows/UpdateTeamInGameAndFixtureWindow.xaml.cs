using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using MySqlX.XDevAPI;
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
    /// Interaction logic for UpdateTeamInGameAndFixtureWindow.xaml
    /// </summary>
    public partial class UpdateTeamInGameAndFixtureWindow : Window
    {
        private ChooseFixtureAndSeasonWindow window;
        private GameInfo gameInfo;
        private int season;
        private int fixture;
        public UpdateTeamInGameAndFixtureWindow(ChooseFixtureAndSeasonWindow win, GameInfo gi, int season, int fixture)
        {
            InitializeComponent();
            window = win;
            gameInfo = gi;
            HomeGoalsTB.Text = gameInfo.HomeTeamGoals.ToString();
            AwayGoalsTB.Text = gameInfo.AwayTeamGoals.ToString();
            DatePickerBox.SelectedDate = gameInfo.GameDate;
            this.season = season;
            this.fixture = fixture;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var homeGoals = HomeGoalsTB.Text;
            var awayGoals = AwayGoalsTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            int intHomeGoals = Int32.Parse(homeGoals);
            int intAwayGoals = Int32.Parse(awayGoals);
            if (intHomeGoals>0 && intAwayGoals>0 && !"".Equals(homeGoals) && !"".Equals(awayGoals) && date != null)
            {
                GameDB.UpdateClubInGame(new ClubInGame(gameInfo.HomeClubId, gameInfo.GameId, Int32.Parse(homeGoals), true));
                GameDB.UpdateClubInGame(new ClubInGame(gameInfo.AwayClubId, gameInfo.GameId, Int32.Parse(awayGoals), true));
                GameDB.UpdateGame(new Game(gameInfo.GameId, date, fixture, season));
                window.DrawData();
                Close();

            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }
    }
}
