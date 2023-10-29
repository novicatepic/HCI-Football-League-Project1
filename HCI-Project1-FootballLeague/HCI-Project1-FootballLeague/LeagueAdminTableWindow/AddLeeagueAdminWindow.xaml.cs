using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
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
