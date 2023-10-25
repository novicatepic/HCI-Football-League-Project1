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

namespace HCI_Project1_FootballLeague.StadiumWindows
{
    /// <summary>
    /// Interaction logic for StadiumWindow.xaml
    /// </summary>
    public partial class StadiumWindow : Window
    {
        public StadiumWindow()
        {
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            List<Stadium> stadiums = StadiumDB.GetStadiums();
            foreach (Stadium s in stadiums)
            {
                DataGridXAML.Items.Add(s);
            }
        }

        private void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var capacity = CapacityTB.Text;
            var town = TownTB.Text;
            if (!"".Equals(name) && !"".Equals(capacity) && !"".Equals(town))
            {
                Stadium stadium = new Stadium(name, Int32.Parse(capacity), town);
                StadiumDB.AddStadium(stadium);
                DrawData();
            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedStadium = (Stadium)DataGridXAML.SelectedItem;
                StadiumDB.DeleteStadium(selectedStadium.StadiumId);
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
                var stadium = (Stadium)DataGridXAML.SelectedItem;
                var id = stadium.StadiumId;
                var name = UpdateNameTB.Text;
                var capacity = CapacityUpdate.Text;
                var town = UpdateTownTB.Text;
                Stadium s = new Stadium(id, name, Int32.Parse(capacity), town);
                StadiumDB.UpdateStadium(s);
                UpdateNameLabel.Visibility = Visibility.Hidden;
                UpdateNameTB.Visibility = Visibility.Hidden;
                CapacityLabel.Visibility = Visibility.Hidden;
                CapacityUpdate.Visibility = Visibility.Hidden;
                UpdateTownLabel.Visibility = Visibility.Hidden;
                UpdateTownTB.Visibility = Visibility.Hidden;
                ConfirmUpdateButton.Visibility = Visibility.Hidden;
                DrawData();
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
                UpdateNameLabel.Visibility = Visibility.Visible;
                UpdateNameTB.Visibility = Visibility.Visible;
                CapacityLabel.Visibility = Visibility.Visible;
                CapacityUpdate.Visibility = Visibility.Visible;
                UpdateTownLabel.Visibility = Visibility.Visible;
                UpdateTownTB.Visibility = Visibility.Visible;
                ConfirmUpdateButton.Visibility = Visibility.Visible;
                var stadium = (Stadium)DataGridXAML.SelectedItem;
                UpdateTownTB.Text = stadium.Town;
                CapacityUpdate.Text = stadium.Capacity.ToString();
                UpdateNameTB.Text = stadium.Name;
            }
            else
            {
                NotSelectedMessage();
            }
        }
    }
}
