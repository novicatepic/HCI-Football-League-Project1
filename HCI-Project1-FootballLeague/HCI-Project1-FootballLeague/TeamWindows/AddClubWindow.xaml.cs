using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.TeamWindows;
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

namespace HCI_Project1_FootballLeague.StadiumWindows
{
    /// <summary>
    /// Interaction logic for AddStadiumWindow.xaml
    /// </summary>
    public partial class AddNewClubWindow : Window
    {
        private TeamWindow tw = null;
        private List<Stadium> stadiums = StadiumDB.GetStadiums();
        public AddNewClubWindow(TeamWindow mainTeamWindow)
        {
            InitializeComponent();
            tw = mainTeamWindow;
            foreach (Stadium s in stadiums)
            {
                StadiumComboBox.Items.Add(s);
            }
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            SubmitBTN.ClearValue(Button.FontSizeProperty);
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
            var AddTeamWTitle = "";
            var AddTeamWHeaderLBL = "";
            var AddTeamWNameLBL = "";
            var AddTeamWFoundationDateBL = "";
            var AddTeamWTrophiesWonLBL = "";
            var AddTeamWStadiumLBL = "";
            var AddTeamWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                AddTeamWTitle = ConfigurationManager.AppSettings["AddTeamWTitle"];
                AddTeamWHeaderLBL = ConfigurationManager.AppSettings["AddTeamWHeaderLBL"];
                AddTeamWNameLBL = ConfigurationManager.AppSettings["AddTeamWNameLBL"];
                AddTeamWFoundationDateBL = ConfigurationManager.AppSettings["AddTeamWFoundationDateBL"];
                AddTeamWTrophiesWonLBL = ConfigurationManager.AppSettings["AddTeamWTrophiesWonLBL"];
                AddTeamWStadiumLBL = ConfigurationManager.AppSettings["AddTeamWStadiumLBL"];
                AddTeamWSubmitBTN = ConfigurationManager.AppSettings["AddTeamWSubmitBTN"];
            }
            else
            {
                AddTeamWTitle = ConfigurationManager.AppSettings["AddTeamWTitleSE"];
                AddTeamWHeaderLBL = ConfigurationManager.AppSettings["AddTeamWHeaderLBLSE"];
                AddTeamWNameLBL = ConfigurationManager.AppSettings["AddTeamWNameLBLSE"];
                AddTeamWFoundationDateBL = ConfigurationManager.AppSettings["AddTeamWFoundationDateBLSE"];
                AddTeamWTrophiesWonLBL = ConfigurationManager.AppSettings["AddTeamWTrophiesWonLBLSE"];
                AddTeamWStadiumLBL = ConfigurationManager.AppSettings["AddTeamWStadiumLBLSE"];
                AddTeamWSubmitBTN = ConfigurationManager.AppSettings["AddTeamWSubmitBTNSE"];
            }
            this.Title = AddTeamWTitle;
            HeaderLabel.Content = AddTeamWHeaderLBL;
            SubmitBTN.Content = AddTeamWSubmitBTN;
            NameLabel.Content = AddTeamWNameLBL;
            FoundationDateLabel.Content = AddTeamWFoundationDateBL;
            TrophiesWonLabel.Content = AddTeamWTrophiesWonLBL;
            StadiumLabel.Content = AddTeamWStadiumLBL;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var trophies = TrophiesWonTB.Text;
            var stadium = (Stadium)StadiumComboBox.SelectedItem;
            int intTrophies = Int32.Parse(trophies);
            if (intTrophies>=0 && !"".Equals(name) && !"".Equals(trophies) && date != null)
            {
                FootballClub club = new FootballClub(name, date, Int32.Parse(trophies), stadium.StadiumId);
                FootballClubDB.AddClub(club);
                tw.DrawData();
                //Close();
            }
            else
            {
                NoInputMessage();
            }
        }

        private void NoInputMessage()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }
    }
}
