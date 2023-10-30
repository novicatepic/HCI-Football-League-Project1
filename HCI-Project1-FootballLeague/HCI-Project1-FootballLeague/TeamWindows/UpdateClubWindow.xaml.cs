using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StadiumWindows;
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
            WriteLanguage();
        }

        public void WriteLanguage()
        {
            var UpdateTeamWTitle = "";
            var UpdateTeamWHeaderLBL = "";
            var UpdateTeamWNameLBL = "";
            var UpdateTeamWFoundationDateBL = "";
            var UpdateTeamWTrophiesWonLBL = "";
            var UpdateTeamWStadiumLBL = "";
            var UpdateTeamWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                UpdateTeamWTitle = ConfigurationManager.AppSettings["UpdateTeamWTitle"];
                UpdateTeamWHeaderLBL = ConfigurationManager.AppSettings["UpdateTeamWHeaderLBL"];
                UpdateTeamWNameLBL = ConfigurationManager.AppSettings["UpdateTeamWNameLBL"];
                UpdateTeamWFoundationDateBL = ConfigurationManager.AppSettings["UpdateTeamWFoundationDateBL"];
                UpdateTeamWTrophiesWonLBL = ConfigurationManager.AppSettings["UpdateTeamWTrophiesWonLBL"];
                UpdateTeamWStadiumLBL = ConfigurationManager.AppSettings["UpdateTeamWStadiumLBL"];
                UpdateTeamWSubmitBTN = ConfigurationManager.AppSettings["UpdateTeamWSubmitBTN"];
            }
            else
            {
                UpdateTeamWTitle = ConfigurationManager.AppSettings["UpdateTeamWTitleSE"];
                UpdateTeamWHeaderLBL = ConfigurationManager.AppSettings["UpdateTeamWHeaderLBLSE"];
                UpdateTeamWNameLBL = ConfigurationManager.AppSettings["UpdateTeamWNameLBLSE"];
                UpdateTeamWFoundationDateBL = ConfigurationManager.AppSettings["UpdateTeamWFoundationDateBLSE"];
                UpdateTeamWTrophiesWonLBL = ConfigurationManager.AppSettings["UpdateTeamWTrophiesWonLBLSE"];
                UpdateTeamWStadiumLBL = ConfigurationManager.AppSettings["UpdateTeamWStadiumLBLSE"];
                UpdateTeamWSubmitBTN = ConfigurationManager.AppSettings["UpdateTeamWSubmitBTNSE"];
            }
            this.Title = UpdateTeamWTitle;
            HeaderLabel.Content = UpdateTeamWHeaderLBL;
            SubmitBTN.Content = UpdateTeamWSubmitBTN;
            NameLabel.Content = UpdateTeamWNameLBL;
            FoundationDateLabel.Content = UpdateTeamWFoundationDateBL;
            TrophiesWonLabel.Content = UpdateTeamWTrophiesWonLBL;
            StadiumLabel.Content = UpdateTeamWStadiumLBL;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = f.ClubId;
            var name = NameTB.Text;
            var date = DatePicker.SelectedDate.Value;
            var trophies = TrophiesTB.Text;
            var stadium = (Stadium)StadiumComboBox.SelectedItem;
            int intTrophies = Int32.Parse(trophies);
            if (intTrophies>=0 && !"".Equals(name) && !"".Equals(trophies) && date != null)
            {
                FootballClub fc = new FootballClub(id, name, date, Int32.Parse(trophies), stadium.StadiumId);
                FootballClubDB.UpdateClub(fc);
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
