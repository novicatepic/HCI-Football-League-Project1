using HCI_Project1_FootballLeague.AdminsStartWindows;
using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.LeagueAdminTableWindow;
using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project1_FootballLeague
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MySqlHciFootballLeague"].ConnectionString;
        public static Administrator LoggedInAdmin = null;
        public MainWindow()
        {
            InitializeComponent();
            ProceedBTN.FontSize = 16;
        }

        public void WriteLanguage()
        {
            var MWTitle = "";
            var MWHeaderLBL = "";
            var MWUNLabel = "";
            var MWPWLabel = "";
            var MWProceedBTN = "";
            if ("en".Equals(LoggedInAdmin.Language))
            {
                MWTitle = ConfigurationManager.AppSettings["MWTitle"];
                MWHeaderLBL = ConfigurationManager.AppSettings["MWHeaderLBL"];
                MWUNLabel = ConfigurationManager.AppSettings["MWUNLabel"];
                MWPWLabel = ConfigurationManager.AppSettings["MWPWLabel"];
                MWProceedBTN = ConfigurationManager.AppSettings["MWProceedBTN"];
            } else
            {
                MWTitle = ConfigurationManager.AppSettings["MWTitleSE"];
                MWHeaderLBL = ConfigurationManager.AppSettings["MWHeaderLBLSE"];
                MWUNLabel = ConfigurationManager.AppSettings["MWUNLabelSE"];
                MWPWLabel = ConfigurationManager.AppSettings["MWPWLabelSE"];
                MWProceedBTN = ConfigurationManager.AppSettings["MWProceedBTNSE"];
            }
            this.Title = MWTitle;
            HeaderLabel.Content = MWHeaderLBL;
            UserNameLabel.Content = MWUNLabel;
            PasswordLabel.Content = MWPWLabel;
            ProceedBTN.Content = MWProceedBTN;
        }

        public void DrawStyle()
        {
            ProceedBTN.ClearValue(Button.FontSizeProperty);
            Style backgroundStyle = null;
            Style buttonStyle = null;
            if ("Large Buttons - Alice Background".Equals(LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundAlice");
                buttonStyle = (Style)FindResource("FontLargeBtn");
            } else if("Medium Buttons - Beige Background".Equals(LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundBeige");
                buttonStyle = (Style)FindResource("FontMediumBtn");
            } else
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrator admin = CheckCredentials();
            if (admin != null)
            {
                LoggedInAdmin = admin;
                WriteLanguage();
                DrawStyle();
                if (admin.IsMainAdmin)
                {
                    MainAdminStartWindow mainAdminStartWindow = new MainAdminStartWindow(this);
                    mainAdminStartWindow.ShowDialog();
                }
                else
                {
                    LeagueAdminStartWindow leagueAdminStartWindow = new LeagueAdminStartWindow(this);
                    leagueAdminStartWindow.Show();
                    
                }
            } else
            {
                MessageBox.Show("Incorrect credentials!");
            }
        }

        private Administrator CheckCredentials()
        {
            string userName = userNameBox.Text;
            string password = passwordBox.Text;
            List<Administrator> admins = AdminDB.GetAdministrators();
            foreach (var admin in admins)
            {
                if (admin.UserName.Equals(userName) && admin.Password.Equals(password))
                {
                    return admin;
                }
            }
            return null;
        }

        private static void WrongCredentialsMessage()
        {
            var NoInputMssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                NoInputMssg = ConfigurationManager.AppSettings["WrongCredentialsMssg"];
            }
            else
            {
                NoInputMssg = ConfigurationManager.AppSettings["WrongCredentialsMssgSE"];
            }
            MessageBox.Show(NoInputMssg);
        }

    }
}
