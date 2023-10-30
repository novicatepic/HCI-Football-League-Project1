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
    /// Interaction logic for AddLeeagueAdminWindow.xaml
    /// </summary>
    public partial class AddLeeagueAdminWindow : Window
    {
        private LeagueAdminWindow window;
        public AddLeeagueAdminWindow(LeagueAdminWindow win)
        {
            InitializeComponent();
            window = win;
            WriteLanguage();
        }

        public void WriteLanguage()
        {
            var LeagueAdminAddWTitle = "";
            var LeagueAdminAddWHeaderLBL = "";
            var LeagueAdminAddWUserNameLBL = "";
            var LeagueAdminAddWPasswordLBL = "";
            var LeagueAdminAddWSubmitBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                LeagueAdminAddWTitle = ConfigurationManager.AppSettings["LeagueAdminAddWTitle"];
                LeagueAdminAddWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminAddWHeaderLBL"];
                LeagueAdminAddWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminAddWUserNameLBL"];
                LeagueAdminAddWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminAddWPasswordLBL"];
                LeagueAdminAddWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminAddWSubmitBTN"];
            }
            else
            {
                LeagueAdminAddWTitle = ConfigurationManager.AppSettings["LeagueAdminAddWTitleSE"];
                LeagueAdminAddWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminAddWHeaderLBLSE"];
                LeagueAdminAddWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminAddWUserNameLBLSE"];
                LeagueAdminAddWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminAddWPasswordLBLSE"];
                LeagueAdminAddWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminAddWSubmitBTNSE"];
            }
            this.Title = LeagueAdminAddWTitle;
            UserNameLabel.Content = LeagueAdminAddWUserNameLBL;
            PasswordLabel.Content = LeagueAdminAddWPasswordLBL;
            SubmitBTN.Content = LeagueAdminAddWSubmitBTN;
            HeaderLabel.Content = LeagueAdminAddWHeaderLBL;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserNameTB.Text;
            var password = PasswordTB.Text;
            if (!"".Equals(userName) && !"".Equals(password))
            {
                Administrator admin = new Administrator(userName, password, false);
                AdminDB.AddAdmin(admin);
                window.DrawData();
                Close();
            }
            else
            {
                MessageBox.Show("User name or password not entered!");
            }
        }
    }
}
