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
    /// Interaction logic for UpdateLeagueAdminWindow.xaml
    /// </summary>
    public partial class UpdateLeagueAdminWindow : Window
    {
        private LeagueAdminWindow window;
        private Administrator admin;
        public UpdateLeagueAdminWindow(LeagueAdminWindow win, Administrator admin)
        {
            InitializeComponent();
            window = win;
            this.admin = admin;
            UserNameTB.Text = admin.UserName;
            PasswordTB.Text = admin.Password;
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
            var LeagueAdminUWTitle = "";
            var LeagueAdminUWHeaderLBL = "";
            var LeagueAdminUWUserNameLBL = "";
            var LeagueAdminUWPasswordLBL = "";
            var LeagueAdminUWSubmitBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                LeagueAdminUWTitle = ConfigurationManager.AppSettings["LeagueAdminUWTitle"];
                LeagueAdminUWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminUWHeaderLBL"];
                LeagueAdminUWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminUWUserNameLBL"];
                LeagueAdminUWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminUWPasswordLBL"];
                LeagueAdminUWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminUWSubmitBTN"];
            }
            else
            {
                LeagueAdminUWTitle = ConfigurationManager.AppSettings["LeagueAdminUWTitleSE"];
                LeagueAdminUWHeaderLBL = ConfigurationManager.AppSettings["LeagueAdminUWHeaderLBLSE"];
                LeagueAdminUWUserNameLBL = ConfigurationManager.AppSettings["LeagueAdminUWUserNameLBLSE"];
                LeagueAdminUWPasswordLBL = ConfigurationManager.AppSettings["LeagueAdminUWPasswordLBLSE"];
                LeagueAdminUWSubmitBTN = ConfigurationManager.AppSettings["LeagueAdminUWSubmitBTNSE"];
            }
            this.Title = LeagueAdminUWTitle;
            UserNameLabel.Content = LeagueAdminUWUserNameLBL;
            PasswordLabel.Content = LeagueAdminUWPasswordLBL;
            SubmitBTN.Content = LeagueAdminUWSubmitBTN;
            HeaderLabel.Content = LeagueAdminUWHeaderLBL;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = admin.AdminId;
            var userName = UserNameTB.Text;
            var password = PasswordTB.Text;
            bool isMain = false;
            if(!"".Equals(userName) && !"".Equals(password))
            {
                Administrator updatedAdmin = new Administrator(id, userName, password, isMain, admin.Language, admin.Look);
                AdminDB.UpdateAdmin(updatedAdmin);
                window.DrawData();
                Close();
            } else
            {
                NoInputMessage();
            }
            
        }

        private void NoInputMessage()
        {
            var NoInputMssg="";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssg"];
            } else
            {
                NoInputMssg = ConfigurationManager.AppSettings["NoInputMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }
    }
}
