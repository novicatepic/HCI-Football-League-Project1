using HCI_Project1_FootballLeague.AdminsStartWindows;
using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.LeagueAdminTableWindow;
using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project1_FootballLeague
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MySqlHciFootballLeague"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();      
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrator admin = CheckCredentials();
            if (admin != null)
            {
                if (admin.IsMainAdmin)
                {
                    MainAdminStartWindow mainAdminStartWindow = new MainAdminStartWindow();
                    mainAdminStartWindow.ShowDialog();
                }
                else
                {
                    LeagueAdminStartWindow leagueAdminStartWindow = new LeagueAdminStartWindow();
                    leagueAdminStartWindow.Show();
                    
                }
            } else
            {
                MessageBox.Show("Incorrect credentials!");
            }
        }

        private Administrator CheckCredentials()
        {
            string userName = userNameBox.Text;
            string password = passwordBox.Text;
            List<Administrator> admins = AdminDB.GetAdministrators();
            foreach (var admin in admins)
            {
                if (admin.UserName.Equals(userName) && admin.Password.Equals(password))
                {
                    return admin;
                }
            }
            return null;
        }

    }
}
