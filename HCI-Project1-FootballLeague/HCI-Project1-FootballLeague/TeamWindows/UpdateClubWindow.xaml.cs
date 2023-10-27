using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StadiumWindows;
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

namespace HCI_Project1_FootballLeague.TeamWindows
{
    /// <summary>
    /// Interaction logic for UpdateClubWindow.xaml
    /// </summary>
    public partial class UpdateClubWindow : Window
    {
        private TeamWindow tw = null;
        private FootballClub f = null;
        private List<Stadium> stadiums = StadiumDB.GetStadiums();
        public UpdateClubWindow(TeamWindow mainTeamWindow, FootballClub f)
        {
            InitializeComponent();
            tw = mainTeamWindow;
            this.f = f;
            NameTB.Text = f.Name;
            DatePicker.SelectedDate = f.Date;
            TrophiesTB.Text = f.NumTrophies.ToString();
            foreach (Stadium s in stadiums)
            {
                StadiumComboBox.Items.Add(s);
            }
                foreach (Stadium s in stadiums)
            {
                if (s.StadiumId == f.StadiumId)
                {
                    StadiumComboBox.SelectedItem = s;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = f.ClubId;
            var name = NameTB.Text;
            var date = DatePicker.SelectedDate.Value;
            var trophies = TrophiesTB.Text;
            var stadium = (Stadium)StadiumComboBox.SelectedItem;
            FootballClub fc = new FootballClub(id, name, date, Int32.Parse(trophies), stadium.StadiumId);
            FootballClubDB.UpdateClub(fc);
            tw.DrawData();
            Close();
        }

    }
}
