using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
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

namespace HCI_Project1_FootballLeague.LeagueAdminTableWindow
{
    /// <summary>
    /// Interaction logic for LeagueAdminWindow.xaml
    /// </summary>
    public partial class LeagueAdminWindow : Window
    {
        public LeagueAdminWindow()
        {
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            //AdminIdCol.Header = "hedd";
        }

        public void WriteLanguage()
        {
            var LeagueAdminWTitle = "";
            var LeagueAdminWFilterLBL = "";
            var LeagueAdminWAddBTN = "";
            var LeagueAdminWUpdateBTN = "";
            var LeagueAdminWDeleteBTN = "";

            var AdminIdCOL = "";
            var AdminUserNameCOL = "";
            var AdminPasswordCOL = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                LeagueAdminWTitle = ConfigurationManager.AppSettings["LeagueAdminSWTitle"];
                LeagueAdminWFilterLBL = ConfigurationManager.AppSettings["LeagueAdminWFilterLBL"];
                LeagueAdminWAddBTN = ConfigurationManager.AppSettings["LeagueAdminWAddBTN"];
                LeagueAdminWUpdateBTN = ConfigurationManager.AppSettings["LeagueAdminWUpdateBTN"];
                LeagueAdminWDeleteBTN = ConfigurationManager.AppSettings["LeagueAdminWDeleteBTN"];

                AdminIdCOL = ConfigurationManager.AppSettings["AdminIdCOL"];
                AdminUserNameCOL = ConfigurationManager.AppSettings["AdminUserNameCOL"];
                AdminPasswordCOL = ConfigurationManager.AppSettings["AdminPasswordCOL"];
            }
            else
            {
                LeagueAdminWTitle = ConfigurationManager.AppSettings["LeagueAdminSWTitleSE"];
                LeagueAdminWFilterLBL = ConfigurationManager.AppSettings["LeagueAdminWFilterLBLSE"];
                LeagueAdminWAddBTN = ConfigurationManager.AppSettings["LeagueAdminWAddBTNSE"];
                LeagueAdminWUpdateBTN = ConfigurationManager.AppSettings["LeagueAdminWUpdateBTNSE"];
                LeagueAdminWDeleteBTN = ConfigurationManager.AppSettings["LeagueAdminWDeleteBTNSE"];

                AdminIdCOL = ConfigurationManager.AppSettings["AdminIdCOLSE"];
                AdminUserNameCOL = ConfigurationManager.AppSettings["AdminUserNameCOLSE"];
                AdminPasswordCOL = ConfigurationManager.AppSettings["AdminPasswordCOLSE"];
            }
            this.Title = LeagueAdminWTitle;
            AddButton.Content = LeagueAdminWAddBTN;
            UpdateButton.Content = LeagueAdminWUpdateBTN;
            DeleteButton.Content = LeagueAdminWDeleteBTN;
            FilterLabel.Content = LeagueAdminWFilterLBL;

            AdminIdC.Header = AdminIdCOL;
            AdminUserNameC.Header = AdminUserNameCOL;
            AdminPasswordC.Header = AdminPasswordCOL;
        }
        private void PopulateData()
        {
            List<Administrator> admins = AdminDB.GetLeagueAdministrators();
            foreach (Administrator admin in admins)
            {
                DataGridXAML.Items.Add(admin);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedAdmin = (Administrator)DataGridXAML.SelectedItem;
                AdminDB.DeleteAdmin(selectedAdmin.AdminId);
                DrawData();
            } else
            {
                NotSelectedMessage();
            }
                
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddLeeagueAdminWindow win = new AddLeeagueAdminWindow(this);
            win.ShowDialog();           
        }

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                Administrator admin = (Administrator)DataGridXAML.SelectedItem;
                UpdateLeagueAdminWindow win = new UpdateLeagueAdminWindow(this, admin);
                win.ShowDialog();
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

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ("".Equals(SearchTB.Text))
            {
                DrawData();
            }
            else
            {
                DataGridXAML.Items.Clear();
                List<Administrator> admins = AdminDB.SearchAdmins(SearchTB.Text);
                foreach (Administrator a in admins)
                {
                    DataGridXAML.Items.Add(a);
                }
            }
        }
    }
}
