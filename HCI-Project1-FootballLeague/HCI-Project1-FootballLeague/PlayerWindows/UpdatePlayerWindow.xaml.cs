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

namespace HCI_Project1_FootballLeague.PlayerWindows
{
    /// <summary>
    /// Interaction logic for UpdatePlayerWindow.xaml
    /// </summary>
    /// 

    public partial class UpdatePlayerWindow : Window
    {
        private PlayerShowWindow pw = null;
        private Player p = null;
        private List<FootballClub> clubs = FootballClubDB.GetClubs();
        public UpdatePlayerWindow(PlayerShowWindow mainPlayerWindow, Player p)
        {
            InitializeComponent();
            pw = mainPlayerWindow;
            this.p = p;
            FirstNameTB.Text = p.FirstName;
            LastNameTB.Text = p.LastName;
            DatePickerBox.SelectedDate = p.DateOfContract;
            ShirtNumberTB.Text = p.ShirtNumber.ToString();
            foreach (FootballClub s in clubs)
            {
                ClubComboBox.Items.Add(s);
            }
            foreach (FootballClub s in clubs)
            {
                if (s.ClubId == p.ClubId)
                {
                    ClubComboBox.SelectedItem = s;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = p.PlayerId;
            var firstName = FirstNameTB.Text;
            var lastName = LastNameTB.Text;
            var shirtNum = ShirtNumberTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var club = (FootballClub)ClubComboBox.SelectedItem;
            int intShirtNum = Int32.Parse(shirtNum);
            if(intShirtNum > 0 && !"".Equals(firstName) && !"".Equals(lastName) && !"".Equals(shirtNum))
            {
                Player pl = new Player(id, Int32.Parse(shirtNum), p.NumGoals, p.NumAssists, p.NumYellowCards, p.NumRedCards, firstName,
                lastName, date, club.ClubId);
                PlayerDB.UpdatePlayer(pl);
                pw.DrawData();
                Close();
            } else
            {
                MessageBox.Show("One of required inputs not entered correctly!");
            }
            

        }
    }
}
