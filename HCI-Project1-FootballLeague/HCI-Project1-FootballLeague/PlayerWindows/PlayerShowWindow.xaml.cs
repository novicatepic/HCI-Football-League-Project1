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

namespace HCI_Project1_FootballLeague.PlayerWindows
{
    /// <summary>
    /// Interaction logic for PlayerShowWindow.xaml
    /// </summary>
    public partial class PlayerShowWindow : Window
    {
        public PlayerShowWindow()
        {
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            AddButton.ClearValue(Button.FontSizeProperty);
            UpdateButton.ClearValue(Button.FontSizeProperty);
            DeleteButton.ClearValue(Button.FontSizeProperty);
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
            foreach (UIElement element in InnerGRID.Children)
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
            var PlayerWTitle = "";
            var PlayerWFilterLBL = "";
            var PlayerWAddBTN = "";
            var PlayerWUpdateBTN = "";
            var PlayerWDeleteBTN = "";

            var PlayerFirstNameCOL = "";
            var PlayerLastNameCOL = "";
            var PlayerDateOfContractCOL = "";
            var PlayerShirtNumCOL = "";
            var PlayerNumGoalsCOL = "";
            var PlayerNumAssistsCOL = "";
            var PlayerNumYellowCOL = "";
            var PlayerNumRedCOL = "";
            var PlayerClubNameCOL = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                PlayerWTitle = ConfigurationManager.AppSettings["PlayerWTitle"];
                PlayerWAddBTN = ConfigurationManager.AppSettings["PlayerWAddBTN"];
                PlayerWFilterLBL = ConfigurationManager.AppSettings["PlayerWFilterLBL"];
                PlayerWUpdateBTN = ConfigurationManager.AppSettings["PlayerWUpdateBTN"];
                PlayerWDeleteBTN = ConfigurationManager.AppSettings["PlayerWDeleteBTN"];

                PlayerFirstNameCOL = ConfigurationManager.AppSettings["PlayerFirstNameCOL"];
                PlayerLastNameCOL = ConfigurationManager.AppSettings["PlayerLastNameCOL"];
                PlayerDateOfContractCOL = ConfigurationManager.AppSettings["PlayerDateOfContractCOL"];
                PlayerShirtNumCOL = ConfigurationManager.AppSettings["PlayerShirtNumCOL"];
                PlayerNumGoalsCOL = ConfigurationManager.AppSettings["PlayerNumGoalsCOL"];
                PlayerNumAssistsCOL = ConfigurationManager.AppSettings["PlayerNumAssistsCOL"];
                PlayerNumYellowCOL = ConfigurationManager.AppSettings["PlayerNumYellowCOL"];
                PlayerNumRedCOL = ConfigurationManager.AppSettings["PlayerNumRedCOL"];
                PlayerClubNameCOL = ConfigurationManager.AppSettings["PlayerClubNameCOL"];
            }
            else
            {
                PlayerWTitle = ConfigurationManager.AppSettings["PlayerWTitleSE"];
                PlayerWAddBTN = ConfigurationManager.AppSettings["PlayerWAddBTNSE"];
                PlayerWFilterLBL = ConfigurationManager.AppSettings["PlayerWFilterLBLSE"];
                PlayerWUpdateBTN = ConfigurationManager.AppSettings["PlayerWUpdateBTNSE"];
                PlayerWDeleteBTN = ConfigurationManager.AppSettings["PlayerWDeleteBTNSE"];

                PlayerFirstNameCOL = ConfigurationManager.AppSettings["PlayerFirstNameCOLSE"];
                PlayerLastNameCOL = ConfigurationManager.AppSettings["PlayerLastNameCOLSE"];
                PlayerDateOfContractCOL = ConfigurationManager.AppSettings["PlayerDateOfContractCOLSE"];
                PlayerShirtNumCOL = ConfigurationManager.AppSettings["PlayerShirtNumCOLSE"];
                PlayerNumGoalsCOL = ConfigurationManager.AppSettings["PlayerNumGoalsCOLSE"];
                PlayerNumAssistsCOL = ConfigurationManager.AppSettings["PlayerNumAssistsCOLSE"];
                PlayerNumYellowCOL = ConfigurationManager.AppSettings["PlayerNumYellowCOLSE"];
                PlayerNumRedCOL = ConfigurationManager.AppSettings["PlayerNumRedCOLSE"];
                PlayerClubNameCOL = ConfigurationManager.AppSettings["PlayerClubNameCOLSE"];
            }
            this.Title = PlayerWTitle;
            AddButton.Content = PlayerWAddBTN;
            UpdateButton.Content = PlayerWUpdateBTN;
            DeleteButton.Content = PlayerWDeleteBTN;
            FilterLabel.Content = PlayerWFilterLBL;

            PlayerFirstNameC.Header = PlayerFirstNameCOL;
            PlayerLastNameC.Header = PlayerLastNameCOL;
            PlayerDateOfContractC.Header = PlayerDateOfContractCOL;
            PlayerShirtNumC.Header = PlayerShirtNumCOL;
            PlayerNumGoalsC.Header = PlayerNumGoalsCOL;
            PlayerNumAssistsC.Header = PlayerNumAssistsCOL;
            PlayerNumYellowC.Header = PlayerNumYellowCOL;
            PlayerNumRedC.Header = PlayerNumRedCOL;
            PlayerClubNameC.Header = PlayerClubNameCOL;
        }

        private List<FootballClub> clubs = FootballClubDB.GetClubs();

        private void PopulateData()
        {
            List<Player> players = PlayerDB.GetPlayers();
            foreach (Player p in players)
            {
                p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                DataGridXAML.Items.Add(p);
            }
        }

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            AddPlayerWindow apw = new AddPlayerWindow(this);
            apw.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedPlayer = (Player)DataGridXAML.SelectedItem;
                PlayerDB.DeletePlayer(selectedPlayer.PlayerId);
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private static void NotSelectedMessage()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoSelectionMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoSelectionMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                UpdatePlayerWindow upw = new UpdatePlayerWindow(this, (Player)DataGridXAML.SelectedItem);
                upw.ShowDialog();
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
                List<Player> players = PlayerDB.SearchPlayers(SearchTB.Text);
                foreach (Player p in players)
                {
                    p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                    DataGridXAML.Items.Add(p);
                }
            }
        }
    }
}
