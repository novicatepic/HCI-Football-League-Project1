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

namespace HCI_Project1_FootballLeague.ChooseSeasonWindow
{
    /// <summary>
    /// Interaction logic for SeasonChooserWindow.xaml
    /// </summary>
    public partial class SeasonChooserWindow : Window
    {
        public SeasonChooserWindow()
        {
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            foreach(var season in SeasonStatsDB.GetSeasons())
            {
                ChooseSeasonBox.Items.Add(season);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(ChooseSeasonBox.SelectedItem != null)
            {
                StatsWindow sw = new StatsWindow((int)ChooseSeasonBox.SelectedItem);
                sw.ShowDialog();
            }
            
        }
    }
}
