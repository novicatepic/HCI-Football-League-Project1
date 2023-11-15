using HCI_Project1_FootballLeague.AdminsStartWindows;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.NewPasswordWindow;
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

namespace HCI_Project1_FootballLeague.OptionsWindow
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private MainAdminStartWindow window;
        private LeagueAdminStartWindow window2;
        public OptionsWindow(MainAdminStartWindow win, LeagueAdminStartWindow win2)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            window = win;
            window2 = win2;
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
            var OptionsWHeaderTitle = "";
            var OptionsWHeaderLBL = "";
            var OptionsWLanguageLBL = "";
            var OptionsWInterfaceLBL = "";
            var OptionsWSubmitBTN = "";
            var OptionsWindowNewPasswordLBL = "";
            var OptionsWindowNewPasswordBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                OptionsWHeaderTitle = ConfigurationManager.AppSettings["OptionsWHeaderTitle"];
                OptionsWHeaderLBL = ConfigurationManager.AppSettings["OptionsWHeaderLBL"];
                OptionsWLanguageLBL = ConfigurationManager.AppSettings["OptionsWLanguageLBL"];
                OptionsWInterfaceLBL = ConfigurationManager.AppSettings["OptionsWInterfaceLBL"];
                OptionsWSubmitBTN = ConfigurationManager.AppSettings["OptionsWSubmitBTN"];

                OptionsWindowNewPasswordLBL = ConfigurationManager.AppSettings["OptionsWindowNewPasswordLBL"];
                OptionsWindowNewPasswordBTN = ConfigurationManager.AppSettings["OptionsWindowNewPasswordBTN"];
            }
            else
            {
                OptionsWHeaderTitle = ConfigurationManager.AppSettings["OptionsWHeaderTitleSE"];
                OptionsWHeaderLBL = ConfigurationManager.AppSettings["OptionsWHeaderLBLSE"];
                OptionsWLanguageLBL = ConfigurationManager.AppSettings["OptionsWLanguageLBLSE"];
                OptionsWInterfaceLBL = ConfigurationManager.AppSettings["OptionsWInterfaceLBLSE"];
                OptionsWSubmitBTN = ConfigurationManager.AppSettings["OptionsWSubmitBTNSE"];

                OptionsWindowNewPasswordLBL = ConfigurationManager.AppSettings["OptionsWindowNewPasswordLBLSE"];
                OptionsWindowNewPasswordBTN = ConfigurationManager.AppSettings["OptionsWindowNewPasswordBTNSE"];
                
            }
            this.Title = OptionsWHeaderTitle;
            HeaderLabel.Content = OptionsWHeaderLBL;
            SubmitBTN.Content = OptionsWSubmitBTN;
            LanguageLabel.Content = OptionsWLanguageLBL;
            InterfaceLabel.Content = OptionsWInterfaceLBL;
            NewPasswordLBL.Content = OptionsWindowNewPasswordLBL;
            ChPWBttn.Content = OptionsWindowNewPasswordBTN;
        }
        private void PopulateData()
        {
            LanguageCB.Items.Add("en");
            LanguageCB.Items.Add("sr");
            LanguageCB.SelectedItem = MainWindow.LoggedInAdmin.Language;

            InterfaceCB.Items.Add("Large Buttons - Alice Background");
            InterfaceCB.Items.Add("Medium Buttons - Beige Background");
            InterfaceCB.Items.Add("Small Buttons - Tan Background");
            InterfaceCB.SelectedItem = MainWindow.LoggedInAdmin.Look;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string item = (string)LanguageCB.SelectedItem;
            string lookItem = (string)InterfaceCB.SelectedItem;
            if (!item.Equals(MainWindow.LoggedInAdmin.Language) || !lookItem.Equals(MainWindow.LoggedInAdmin.Look))
            {
                MainWindow.LoggedInAdmin.Language = item;
                MainWindow.LoggedInAdmin.Look = lookItem;
                AdminDB.UpdateAdminPreferences(MainWindow.LoggedInAdmin);
                //MessageBox.Show("HERE");
                if(window != null)
                {
                    window.WriteLanguage();
                    window.DrawStyle();
                    this.DrawStyle();
                    this.WriteLanguage();
                }
                if (window2 != null)
                {
                    window2.WriteLanguage();
                    window2.DrawStyle();
                    this.DrawStyle();
                    this.WriteLanguage();
                }
                //Close();
            }
        }

        private void ChPWBttn_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow cpw = new ChangePasswordWindow();
            cpw.ShowDialog();
        }
    }
}
