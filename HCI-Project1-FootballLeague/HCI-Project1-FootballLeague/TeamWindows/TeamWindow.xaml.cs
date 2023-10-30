using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StadiumWindows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        private List<Stadium> stadiums = StadiumDB.GetStadiums();
        public TeamWindow()
        {
            InitializeComponent();
            PopulateData();
            WriteLanguage();
        }

        public void WriteLanguage()
        {
            var TeamWTitle = "";
            var TeamWFilterLBL = "";
            var TeamWAddBTN = "";
            var TeamWUpdateBTN = "";
            var TeamWDeleteBTN = "";

            var TeamNameCOL = "";
            var TeamFoundationDateCOL = "";
            var TeamTrophiesWonCOL = "";
            var TeamStadiumNameCOL = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                TeamWTitle = ConfigurationManager.AppSettings["TeamWTitle"];
                TeamWFilterLBL = ConfigurationManager.AppSettings["TeamWFilterLBL"];
                TeamWAddBTN = ConfigurationManager.AppSettings["TeamWAddBTN"];
                TeamWUpdateBTN = ConfigurationManager.AppSettings["TeamWUpdateBTN"];
                TeamWDeleteBTN = ConfigurationManager.AppSettings["TeamWDeleteBTN"];

                TeamNameCOL = ConfigurationManager.AppSettings["TeamNameCOL"];
                TeamFoundationDateCOL = ConfigurationManager.AppSettings["TeamFoundationDateCOL"];
                TeamTrophiesWonCOL = ConfigurationManager.AppSettings["TeamTrophiesWonCOL"];
                TeamStadiumNameCOL = ConfigurationManager.AppSettings["TeamStadiumNameCOL"];
            }
            else
            {
                TeamWTitle = ConfigurationManager.AppSettings["TeamWTitleSE"];
                TeamWFilterLBL = ConfigurationManager.AppSettings["TeamWFilterLBLSE"];
                TeamWAddBTN = ConfigurationManager.AppSettings["TeamWAddBTNSE"];
                TeamWUpdateBTN = ConfigurationManager.AppSettings["TeamWUpdateBTNSE"];
                TeamWDeleteBTN = ConfigurationManager.AppSettings["TeamWDeleteBTNSE"];

                TeamNameCOL = ConfigurationManager.AppSettings["TeamNameCOLSE"];
                TeamFoundationDateCOL = ConfigurationManager.AppSettings["TeamFoundationDateCOLSE"];
                TeamTrophiesWonCOL = ConfigurationManager.AppSettings["TeamTrophiesWonCOLSE"];
                TeamStadiumNameCOL = ConfigurationManager.AppSettings["TeamStadiumNameCOLSE"];
            }
            this.Title = TeamWTitle;
            AddButton.Content = TeamWAddBTN;
            UpdateButton.Content = TeamWUpdateBTN;
            DeleteButton.Content = TeamWDeleteBTN;
            FilterLabel.Content = TeamWFilterLBL;

            TeamNameC.Header = TeamNameCOL;
            TeamFoundationDateC.Header = TeamFoundationDateCOL;
            TeamTrophiesWonC.Header = TeamTrophiesWonCOL;
            TeamStadiumNameC.Header = TeamStadiumNameCOL;

        }

        private void PopulateData()
        {
            List<FootballClub> clubs = FootballClubDB.GetClubs();
            foreach (FootballClub f in clubs)
            {
                f.StadiumName = FootballClubDB.GetStadiumName(f.ClubId.ToString());
                DataGridXAML.Items.Add(f);
            }
        }

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }
    
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewClubWindow acw = new AddNewClubWindow(this);
            acw.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedClub = (FootballClub)DataGridXAML.SelectedItem;
                FootballClubDB.DeleteClub(selectedClub.ClubId);
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private static void NotSelectedMessage()
        {
            MessageBox.Show("Item not selected!");
        }

        private void ConfirmUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateClubWindow ucw = new UpdateClubWindow(this, (FootballClub)DataGridXAML.SelectedItem);
                ucw.ShowDialog();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateClubWindow ucw = new UpdateClubWindow(this, (FootballClub)DataGridXAML.SelectedItem);
                ucw.ShowDialog();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ("".Equals(SearchTB.Text))
            {
                DrawData();
            }
            else
            {
                DataGridXAML.Items.Clear();
                List<FootballClub> clubs = FootballClubDB.SearchClubs(SearchTB.Text);
                foreach (FootballClub c in clubs)
                {
                    c.StadiumName = FootballClubDB.GetStadiumName(c.ClubId.ToString());
                    DataGridXAML.Items.Add(c);
                }
            }
        }
    }
}
