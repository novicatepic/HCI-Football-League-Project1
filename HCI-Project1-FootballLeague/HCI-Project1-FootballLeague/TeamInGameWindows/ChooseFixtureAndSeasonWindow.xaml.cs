using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StatsWindows;
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

namespace HCI_Project1_FootballLeague.TeamInGameWindows
{
    /// <summary>
    /// Interaction logic for ChooseFixtureAndSeasonWindow.xaml
    /// </summary>
    public partial class ChooseFixtureAndSeasonWindow : Window
    {
        public ChooseFixtureAndSeasonWindow()
        {
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            foreach (var season in SeasonStatsDB.GetSeasons())
            {
                ChooseSeasonBox.Items.Add(season);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSeasonBox.SelectedItem != null && ChooseFixtureBox != null)
            {
                /*StatsWindow sw = new StatsWindow((int)ChooseSeasonBox.SelectedItem);
                sw.ShowDialog();*/

            } else
            {
                ItemNotSelected();
            }
        }

        private static void ItemNotSelected()
        {
            MessageBox.Show("Item not selected!");
        }

        private void ChooseSeasonBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var fixture in GameDB.GetFixturesInSeason((Int32)ChooseSeasonBox.SelectedItem))
            {
                ChooseFixtureBox.Items.Add(fixture);
            }
        }
    }
}
