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

namespace HCI_Project1_FootballLeague.StatsWindows
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        private int season;
        public StatsWindow()
        {
            InitializeComponent();
            foreach (var s in SeasonStatsDB.GetSeasons())
            {
                ChooseSeasonBox.Items.Add(s);
            }
            //PopulateData();
        }

        private void PopulateData(int season)
        {
            
            //MessageBox.Show(season.ToString());
            List<SeasonStats> stats = SeasonStatsDB.GetStats(season);
            foreach (SeasonStats s in stats)
            {
                s.ClubName = SeasonStatsDB.GetClubName(s.ClubId.ToString());
                DataGridXAML.Items.Add(s);
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ("".Equals(SearchTB.Text))
            {
                PopulateData(season);
            }
            else
            {
                DataGridXAML.Items.Clear();
                List<SeasonStats> stats = SeasonStatsDB.SearchStats(season, SearchTB.Text);
                foreach (SeasonStats s in stats)
                {
                    DataGridXAML.Items.Add(s);
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.season = (int)ChooseSeasonBox.SelectedItem;
            PopulateData(season);
        }
    }
}
