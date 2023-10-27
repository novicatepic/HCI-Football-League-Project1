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

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewStadiumWindow asw = new AddNewStadiumWindow(this);
            asw.ShowDialog();
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (DataGridXAML.SelectedItem != null)
            {
                UpdateStadium us = new UpdateStadium(this, (Stadium)DataGridXAML.SelectedItem);
                us.ShowDialog();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if("".Equals(SearchTB.Text))
            {
                DrawData();
            } else
            {
                DataGridXAML.Items.Clear();
                List<Stadium> stadiums = StadiumDB.SearchStadiums(SearchTB.Text);
                foreach (Stadium s in stadiums)
                {
                    DataGridXAML.Items.Add(s);
                }
            }
        }
    }
}
