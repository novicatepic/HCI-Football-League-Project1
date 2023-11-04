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

namespace HCI_Project1_FootballLeague.PlayerWindows
{
    /// <summary>
    /// Interaction logic for UpdatePlayerWindow.xaml
    /// </summary>
    /// 

    public partial class UpdatePlayerWindow : Window
    {
        private PlayerShowWindow pw = null;
        private Player p = null;
        private List<FootballClub> clubs = FootballClubDB.GetClubs();
        public UpdatePlayerWindow(PlayerShowWindow mainPlayerWindow, Player p)
        {
            InitializeComponent();
            pw = mainPlayerWindow;
            this.p = p;
            FirstNameTB.Text = p.FirstName;
            LastNameTB.Text = p.LastName;
            DatePickerBox.SelectedDate = p.DateOfContract;
            ShirtNumberTB.Text = p.ShirtNumber.ToString();
            foreach (FootballClub s in clubs)
            {
                ClubComboBox.Items.Add(s);
            }
            foreach (FootballClub s in clubs)
            {
                if (s.ClubId == p.ClubId)
                {
                    ClubComboBox.SelectedItem = s;
                }
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
            var UpdatePlayerWTitle = "";
            var UpdatePlayerWHeaderLBL = "";
            var UpdatePlayerWFirstNameLBL = "";
            var UpdatePlayerWLastNameLBL = "";
            var UpdatePlayerWContractDateLBL = "";

            var UpdatePlayerWShirtNumberLBL = "";
            var UpdatePlayerWSelectClubLBL = "";
            var UpdatePlayerWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                UpdatePlayerWTitle = ConfigurationManager.AppSettings["UpdatePlayerWTitle"];
                UpdatePlayerWHeaderLBL = ConfigurationManager.AppSettings["UpdatePlayerWHeaderLBL"];
                UpdatePlayerWFirstNameLBL = ConfigurationManager.AppSettings["UpdatePlayerWFirstNameLBL"];
                UpdatePlayerWLastNameLBL = ConfigurationManager.AppSettings["UpdatePlayerWLastNameLBL"];
                UpdatePlayerWContractDateLBL = ConfigurationManager.AppSettings["UpdatePlayerWContractDateLBL"];

                UpdatePlayerWShirtNumberLBL = ConfigurationManager.AppSettings["UpdatePlayerWShirtNumberLBL"];
                UpdatePlayerWSelectClubLBL = ConfigurationManager.AppSettings["UpdatePlayerWSelectClubLBL"];
                UpdatePlayerWSubmitBTN = ConfigurationManager.AppSettings["UpdatePlayerWSubmitBTN"];
            }
            else
            {
                UpdatePlayerWTitle = ConfigurationManager.AppSettings["UpdatePlayerWTitleSE"];
                UpdatePlayerWHeaderLBL = ConfigurationManager.AppSettings["UpdatePlayerWHeaderLBLSE"];
                UpdatePlayerWFirstNameLBL = ConfigurationManager.AppSettings["UpdatePlayerWFirstNameLBLSE"];
                UpdatePlayerWLastNameLBL = ConfigurationManager.AppSettings["UpdatePlayerWLastNameLBLSE"];
                UpdatePlayerWContractDateLBL = ConfigurationManager.AppSettings["UpdatePlayerWContractDateLBLSE"];

                UpdatePlayerWShirtNumberLBL = ConfigurationManager.AppSettings["UpdatePlayerWShirtNumberLBLSE"];
                UpdatePlayerWSelectClubLBL = ConfigurationManager.AppSettings["UpdatePlayerWSelectClubLBLSE"];
                UpdatePlayerWSubmitBTN = ConfigurationManager.AppSettings["UpdatePlayerWSubmitBTNSE"];
            }
            this.Title = UpdatePlayerWTitle;
            HeaderLabel.Content = UpdatePlayerWHeaderLBL;
            FirstNameLabel.Content = UpdatePlayerWFirstNameLBL;
            LastNameLabel.Content = UpdatePlayerWLastNameLBL;
            ContractDateLabel.Content = UpdatePlayerWContractDateLBL;
            ShirtNumLabel.Content = UpdatePlayerWShirtNumberLBL;
            SelectClubLabel.Content = UpdatePlayerWSelectClubLBL;
            SubmitBTN.Content = UpdatePlayerWSubmitBTN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = p.PlayerId;
            var firstName = FirstNameTB.Text;
            var lastName = LastNameTB.Text;
            var shirtNum = ShirtNumberTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var club = (FootballClub)ClubComboBox.SelectedItem;
            int intShirtNum = Int32.Parse(shirtNum);
            if(intShirtNum > 0 && intShirtNum <= 90 && !"".Equals(firstName) && !"".Equals(lastName) && !"".Equals(shirtNum))
            {
                Player pl = new Player(id, Int32.Parse(shirtNum), p.NumGoals, p.NumAssists, p.NumYellowCards, p.NumRedCards, firstName,
                lastName, date, club.ClubId);
                PlayerDB.UpdatePlayer(pl);
                pw.DrawData();
                //Close();
            } else
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
