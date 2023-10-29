using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
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

namespace HCI_Project1_FootballLeague.StadiumWindows
{
    /// <summary>
    /// Interaction logic for AddStadiumWindow.xaml
    /// </summary>
    public partial class AddNewClubWindow : Window
    {
        private TeamWindow tw = null;
        private List<Stadium> stadiums = StadiumDB.GetStadiums();
        public AddNewClubWindow(TeamWindow mainTeamWindow)
        {
            InitializeComponent();
            tw = mainTeamWindow;
            foreach (Stadium s in stadiums)
            {
                StadiumComboBox.Items.Add(s);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var trophies = TrophiesWonTB.Text;
            var stadium = (Stadium)StadiumComboBox.SelectedItem;
            int intTrophies = Int32.Parse(trophies);
            if (intTrophies>=0 && !"".Equals(name) && !"".Equals(trophies) && date != null)
            {
                FootballClub club = new FootballClub(name, date, Int32.Parse(trophies), stadium.StadiumId);
                FootballClubDB.AddClub(club);
                tw.DrawData();
                Close();
            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }
    }
}
