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
    /// Interaction logic for LeagueAdminWindow.xaml
    /// </summary>
    public partial class LeagueAdminWindow : Window
    {
        public LeagueAdminWindow()
        {
            InitializeComponent();
            PopulateData();
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
            var userName = UserNameTB.Text;
            var password = PasswordTB.Text;
            if(!"".Equals(userName) && !"".Equals(password))
            {
                Administrator admin = new Administrator(userName, password, false);
                AdminDB.AddAdmin(admin);
                DrawData();
            }
            else
            {
                MessageBox.Show("User name or password not entered!");
            }
            
        }

        private void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateNameLabel.Visibility = Visibility.Visible;
                UpdatePasswordLabel.Visibility = Visibility.Visible;
                UserNameUpdate.Visibility = Visibility.Visible;
                PasswordUpdate.Visibility = Visibility.Visible;
                ConfirmUpdateButton.Visibility = Visibility.Visible;
                var admin = (Administrator)DataGridXAML.SelectedItem;
                UserNameUpdate.Text = admin.UserName;
                PasswordUpdate.Text = admin.Password;
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
            if(DataGridXAML.SelectedItem != null)
            {
                var admin = (Administrator)DataGridXAML.SelectedItem;
                var id = admin.AdminId;
                var userName = UserNameUpdate.Text;
                var password = PasswordUpdate.Text;
                bool isMain = false;
                Administrator updatedAdmin = new Administrator(id, userName, password, isMain);
                AdminDB.UpdateAdmin(updatedAdmin);
                UpdateNameLabel.Visibility = Visibility.Hidden;
                UpdatePasswordLabel.Visibility = Visibility.Hidden;
                UserNameUpdate.Visibility = Visibility.Hidden;
                PasswordUpdate.Visibility = Visibility.Hidden;
                ConfirmUpdateButton.Visibility = Visibility.Hidden;
                DrawData();
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
                List<Administrator> admins = AdminDB.SearchAdmins(SearchTB.Text);
                foreach (Administrator a in admins)
                {
                    DataGridXAML.Items.Add(a);
                }
            }
        }
    }
}
