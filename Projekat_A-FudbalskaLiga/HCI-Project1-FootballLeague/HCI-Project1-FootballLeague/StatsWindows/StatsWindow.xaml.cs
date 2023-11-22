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
using System.Xml;

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
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            //NextButton.ClearValue(Button.FontSizeProperty);
            Style backgroundStyle = null;
            Style buttonStyle = null;
            if ("Large Buttons - Alice Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundAlice");
                buttonStyle = (Style)FindResource("FontLargeBtn");
            }
            else if ("Medium Buttons - Beige Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundBeige");
                buttonStyle = (Style)FindResource("FontMediumBtn");
            }
            else
            {
                backgroundStyle = (Style)FindResource("BackgroundTan");
                buttonStyle = (Style)FindResource("FontSmallBtn");
            }
            Grid.Style = backgroundStyle;
            foreach (UIElement element in Grid.Children)
            {
                if (element is Button)
                {
                    Button button = (Button)element;
                    button.Style = buttonStyle;
                }
            }
        }
        public void WriteLanguage()
        {
            var StatsWTitle = "";
            var StatsWChooseSeasonLBL = "";
            var StatsWFilterLBL = "";
            var StatsWShowBTN = "";

            var StatsTeamNameCOL = "";
            var StatsNumPlayedCOL = "";
            var StatsNumWinsCOL = "";
            var StatsNumDrawsCOL = "";
            var StatsNumLosesCOL = "";
            var StatsNumScoredCOL = "";
            var StatsNumConcededCOL = "";
            var StatsNumPointsCOL = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                StatsWTitle = ConfigurationManager.AppSettings["StatsWTitle"];
                StatsWChooseSeasonLBL = ConfigurationManager.AppSettings["StatsWChooseSeasonLBL"];
                StatsWFilterLBL = ConfigurationManager.AppSettings["StatsWFilterLBL"];
                StatsWShowBTN = ConfigurationManager.AppSettings["StatsWShowBTN"];

                StatsTeamNameCOL = ConfigurationManager.AppSettings["StatsTeamNameCOL"];
                StatsNumPlayedCOL = ConfigurationManager.AppSettings["StatsNumPlayedCOL"];
                StatsNumWinsCOL = ConfigurationManager.AppSettings["StatsNumWinsCOL"];
                StatsNumDrawsCOL = ConfigurationManager.AppSettings["StatsNumDrawsCOL"];
                StatsNumLosesCOL = ConfigurationManager.AppSettings["StatsNumLosesCOL"];
                StatsNumScoredCOL = ConfigurationManager.AppSettings["StatsNumScoredCOL"];
                StatsNumConcededCOL = ConfigurationManager.AppSettings["StatsNumConcededCOL"];
                StatsNumPointsCOL = ConfigurationManager.AppSettings["StatsNumPointsCOL"];
            }
            else
            {
                StatsWTitle = ConfigurationManager.AppSettings["StatsWTitleSE"];
                StatsWChooseSeasonLBL = ConfigurationManager.AppSettings["StatsWChooseSeasonLBLSE"];
                StatsWFilterLBL = ConfigurationManager.AppSettings["StatsWFilterLBLSE"];
                StatsWShowBTN = ConfigurationManager.AppSettings["StatsWShowBTNSE"];

                StatsTeamNameCOL = ConfigurationManager.AppSettings["StatsTeamNameCOLSE"];
                StatsNumPlayedCOL = ConfigurationManager.AppSettings["StatsNumPlayedCOLSE"];
                StatsNumWinsCOL = ConfigurationManager.AppSettings["StatsNumWinsCOLSE"];
                StatsNumDrawsCOL = ConfigurationManager.AppSettings["StatsNumDrawsCOLSE"];
                StatsNumLosesCOL = ConfigurationManager.AppSettings["StatsNumLosesCOLSE"];
                StatsNumScoredCOL = ConfigurationManager.AppSettings["StatsNumScoredCOLSE"];
                StatsNumConcededCOL = ConfigurationManager.AppSettings["StatsNumConcededCOLSE"];
                StatsNumPointsCOL = ConfigurationManager.AppSettings["StatsNumPointsCOLSE"];
            }
            this.Title = StatsWTitle;
            ChooseSeasonLabel.Content = StatsWChooseSeasonLBL;
            //FilterLabel.Content = StatsWFilterLBL;
            //NextButton.Content = StatsWShowBTN;

            StatsTeamNameC.Header = StatsTeamNameCOL;
            StatsNumPlayedC.Header = StatsNumPlayedCOL;
            StatsNumWinsC.Header = StatsNumWinsCOL;
            StatsNumDrawsC.Header = StatsNumDrawsCOL;
            StatsNumLosesC.Header = StatsNumLosesCOL;
            StatsNumScoredC.Header = StatsNumScoredCOL;
            StatsNumConcededC.Header = StatsNumConcededCOL;
            StatsNumPointsC.Header = StatsNumPointsCOL;
        }

        private void PopulateData(int season)
        {
            List<SeasonStats> stats = SeasonStatsDB.GetStats(season);
            foreach (SeasonStats s in stats)
            {
                s.ClubName = SeasonStatsDB.GetClubName(s.ClubId.ToString());
                DataGridXAML.Items.Add(s);
            }
            
        }

        /*private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
        }*/

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.season = (int)ChooseSeasonBox.SelectedItem;
            PopulateData(season);
        }

        private void ChooseSeasonBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridXAML.Items.Clear();
            this.season = (int)ChooseSeasonBox.SelectedItem;
            PopulateData(season);
        }
    }
}
