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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        private PlayerShowWindow pw = null;
        private List<FootballClub> clubs = FootballClubDB.GetClubs();
        public AddPlayerWindow(PlayerShowWindow mainPlayerWindow)
        {
            InitializeComponent();
            pw = mainPlayerWindow;
            foreach (FootballClub fc in clubs)
            {
                ClubComboBox.Items.Add(fc);
            }
            WriteLanguage();
        }

        public void WriteLanguage()
        {
            var AddPlayerWTitle = "";
            var AddPlayerWHeaderLBL = "";
            var AddPlayerWFirstNameLBL = "";
            var AddPlayerWLastNameLBL = "";
            var AddPlayerWContractDateLBL = "";

            var AddPlayerWShirtNumberLBL = "";
            var AddPlayerWSelectClubLBL = "";
            var AddPlayerWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                AddPlayerWTitle = ConfigurationManager.AppSettings["AddPlayerWTitle"];
                AddPlayerWHeaderLBL = ConfigurationManager.AppSettings["AddPlayerWHeaderLBL"];
                AddPlayerWFirstNameLBL = ConfigurationManager.AppSettings["AddPlayerWFirstNameLBL"];
                AddPlayerWLastNameLBL = ConfigurationManager.AppSettings["AddPlayerWLastNameLBL"];
                AddPlayerWContractDateLBL = ConfigurationManager.AppSettings["AddPlayerWContractDateLBL"];

                AddPlayerWShirtNumberLBL = ConfigurationManager.AppSettings["AddPlayerWShirtNumberLBL"];
                AddPlayerWSelectClubLBL = ConfigurationManager.AppSettings["AddPlayerWSelectClubLBL"];
                AddPlayerWSubmitBTN = ConfigurationManager.AppSettings["AddPlayerWSubmitBTN"];
            }
            else
            {
                AddPlayerWTitle = ConfigurationManager.AppSettings["AddPlayerWTitleSE"];
                AddPlayerWHeaderLBL = ConfigurationManager.AppSettings["AddPlayerWHeaderLBLSE"];
                AddPlayerWFirstNameLBL = ConfigurationManager.AppSettings["AddPlayerWFirstNameLBLSE"];
                AddPlayerWLastNameLBL = ConfigurationManager.AppSettings["AddPlayerWLastNameLBLSE"];
                AddPlayerWContractDateLBL = ConfigurationManager.AppSettings["AddPlayerWContractDateLBLSE"];

                AddPlayerWShirtNumberLBL = ConfigurationManager.AppSettings["AddPlayerWShirtNumberLBLSE"];
                AddPlayerWSelectClubLBL = ConfigurationManager.AppSettings["AddPlayerWSelectClubLBLSE"];
                AddPlayerWSubmitBTN = ConfigurationManager.AppSettings["AddPlayerWSubmitBTNSE"];
            }
            this.Title = AddPlayerWTitle;
            HeaderLabel.Content = AddPlayerWHeaderLBL;
            FirstNameLabel.Content = AddPlayerWFirstNameLBL;
            LastNameLabel.Content = AddPlayerWLastNameLBL;
            ContractDateLabel.Content = AddPlayerWContractDateLBL;
            ShirtNumLabel.Content = AddPlayerWShirtNumberLBL;
            SelectClubLabel.Content = AddPlayerWSelectClubLBL;
            SubmitBTN.Content = AddPlayerWSubmitBTN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var firstName = FirstNameTB.Text;
            var lastName = LastNameTB.Text;
            var date = DatePickerBox.SelectedDate.Value;
            var shirtNumber = ShirtNumberTB.Text;
            var club = (FootballClub)ClubComboBox.SelectedItem;
            int intShirtNum = Int32.Parse(shirtNumber);
            if (intShirtNum>0 && !"".Equals(firstName) && !"".Equals(lastName) && date != null && !"".Equals(shirtNumber) && club != null)
            {
                Player player = new Player(Int32.Parse(shirtNumber), 0, 0, 0, 0, 0, firstName, lastName, date, club.ClubId);
                PlayerDB.AddPlayer(player);
                pw.DrawData();
                Close();
            }
            else
            {
                MessageBox.Show("One of required inputs not entered correctly!");
            }
        }
    }
}
