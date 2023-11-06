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

namespace HCI_Project1_FootballLeague.LeagueAdminTableWindow
{
    /// <summary>
    /// Interaction logic for AddLeeagueAdminWindow.xaml
    /// </summary>
    public partial class AddLeeagueAdminWindow : Window
    {
        private LeagueAdminWindow window;
        public AddLeeagueAdminWindow(LeagueAdminWindow win)
        {
            InitializeComponent();
            window = win;
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
            var LeagueAdminAddWTitle = "";
            var LeagueAdminAddWHeaderLBL = "";
            var LeagueAdminAddWUserNameLBL = "";
            var LeagueAdminAddWPasswordLBL = "";
            var LeagueAdminAddWSubmitBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                LeagueAdminAddWTitle = ConfigurationManager.AppSettings["LeagueAdminAddWTitle"];
                LeagueAdminAddWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminAddWHeaderLBL"];
                LeagueAdminAddWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminAddWUserNameLBL"];
                LeagueAdminAddWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminAddWPasswordLBL"];
                LeagueAdminAddWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminAddWSubmitBTN"];
            }
            else
            {
                LeagueAdminAddWTitle = ConfigurationManager.AppSettings["LeagueAdminAddWTitleSE"];
                LeagueAdminAddWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminAddWHeaderLBLSE"];
                LeagueAdminAddWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminAddWUserNameLBLSE"];
                LeagueAdminAddWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminAddWPasswordLBLSE"];
                LeagueAdminAddWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminAddWSubmitBTNSE"];
            }
            this.Title = LeagueAdminAddWTitle;
            UserNameLabel.Content = LeagueAdminAddWUserNameLBL;
            PasswordLabel.Content = LeagueAdminAddWPasswordLBL;
            SubmitBTN.Content = LeagueAdminAddWSubmitBTN;
            HeaderLabel.Content = LeagueAdminAddWHeaderLBL;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserNameTB.Text;
            var password = PasswordTB.Password;
            if (!"".Equals(userName) && !"".Equals(password))
            {
                Administrator admin = new Administrator(userName, password, false);
                AdminDB.AddAdmin(admin);
                window.DrawData();
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
