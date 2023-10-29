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
    /// Interaction logic for UpdateLeagueAdminWindow.xaml
    /// </summary>
    public partial class UpdateLeagueAdminWindow : Window
    {
        private LeagueAdminWindow window;
        private Administrator admin;
        public UpdateLeagueAdminWindow(LeagueAdminWindow win, Administrator admin)
        {
            InitializeComponent();
            window = win;
            this.admin = admin;
            UserNameTB.Text = admin.UserName;
            PasswordTB.Text = admin.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = admin.AdminId;
            var userName = UserNameTB.Text;
            var password = PasswordTB.Text;
            bool isMain = false;
            if(!"".Equals(userName) && !"".Equals(password))
            {
                Administrator updatedAdmin = new Administrator(id, userName, password, isMain);
                AdminDB.UpdateAdmin(updatedAdmin);
                window.DrawData();
                Close();
            } else
            {
                NoInputMessage();
            }
            
        }

        private void NoInputMessage()
        {
            MessageBox.Show("Parameters not entered!");
        }
    }
}
