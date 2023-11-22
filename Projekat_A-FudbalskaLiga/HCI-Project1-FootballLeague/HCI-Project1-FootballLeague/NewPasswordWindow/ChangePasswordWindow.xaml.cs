using HCI_Project1_FootballLeague.AdminsStartWindows;
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

namespace HCI_Project1_FootballLeague.NewPasswordWindow
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            UserNameTB.Text = MainWindow.LoggedInAdmin.UserName;
            DrawStyle();
            WriteLanguage();
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
            var ChPWTitle = "";
            var OldPWLBL = "";
            var NewPWLBL = "";
            var NewPWAgainLBL = "";
            var SubmitPWBTN = "";
            var ChPWHeader = "";
            var ChPWUNLBL = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                ChPWTitle = ConfigurationManager.AppSettings["ChPWTitle"];
                OldPWLBL = ConfigurationManager.AppSettings["OldPWLBL"];
                NewPWLBL = ConfigurationManager.AppSettings["NewPWLBL"];
                NewPWAgainLBL = ConfigurationManager.AppSettings["NewPWAgainLBL"];
                SubmitPWBTN = ConfigurationManager.AppSettings["SubmitPWBTN"];
                ChPWHeader = ConfigurationManager.AppSettings["ChPWHeader"];
                ChPWUNLBL = ConfigurationManager.AppSettings["ChPWUNLBL"];
            }
            else
            {
                ChPWTitle = ConfigurationManager.AppSettings["ChPWTitleSE"];
                OldPWLBL = ConfigurationManager.AppSettings["OldPWLBLSE"];
                NewPWLBL = ConfigurationManager.AppSettings["NewPWLBLSE"];
                NewPWAgainLBL = ConfigurationManager.AppSettings["NewPWAgainLBLSE"];
                SubmitPWBTN = ConfigurationManager.AppSettings["SubmitPWBTNSE"];
                ChPWHeader = ConfigurationManager.AppSettings["ChPWHeaderSE"];
                ChPWUNLBL = ConfigurationManager.AppSettings["ChPWUNLBLSE"];
            }
            this.Title = ChPWTitle;
            HeaderLabel.Content = ChPWHeader;
            SubmitBTN.Content = SubmitPWBTN;
            OldPWLabel.Content = OldPWLBL;
            NewPWLabel.Content = NewPWLBL;
            RepeatPWLabel.Content = NewPWAgainLBL;
            UserNameLabel.Content = ChPWUNLBL;
        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserNameTB.Text;
            var oldPassword = OldPWTB.Password;
            var newPassword = NewPasswordTB.Password;
            var newPWRepeated = RepeatPWTB.Password;
            if (!"".Equals(userName) && !"".Equals(oldPassword) && !"".Equals(newPassword) && !"".Equals(newPWRepeated))
            {
                if(newPassword.Equals(newPWRepeated) && oldPassword.Equals(MainWindow.LoggedInAdmin.Password))
                {
                    bool result = AdminDB.UpdateAdminCredentials(userName, newPassword, MainWindow.LoggedInAdmin.AdminId);
                    if(result)
                    {
                        SuccessfullChangeMessage();
                        Close();
                    }                
                } else
                {
                    UnsuccessfulChangeMessage();
                }
                
            }
            else
            {
                NoInputMessage();
            }
        }

        private void SuccessfullChangeMessage()
        {
            var Mssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                Mssg = ConfigurationManager.AppSettings["SuccessfullPWChange"];
            }
            else
            {
                Mssg = ConfigurationManager.AppSettings["SuccessfullPWChangeSE"];
            }
            MessageBox.Show(Mssg);
        }
        private void UnsuccessfulChangeMessage()
        {
            var Mssg = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                Mssg = ConfigurationManager.AppSettings["UnsuccessfullPWChange"];
            }
            else
            {
                Mssg = ConfigurationManager.AppSettings["UnsuccessfullPWChangeSE"];
            }
            MessageBox.Show(Mssg);
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
