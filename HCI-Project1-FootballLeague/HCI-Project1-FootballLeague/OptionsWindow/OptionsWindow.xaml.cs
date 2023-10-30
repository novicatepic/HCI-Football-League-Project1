using HCI_Project1_FootballLeague.AdminsStartWindows;
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
            InitializeComponent();
            PopulateData();
            WriteLanguage();
            window = win;
            window2 = win2;
        }

        public void WriteLanguage()
        {
            var OptionsWHeaderTitle = "";
            var OptionsWHeaderLBL = "";
            var OptionsWLanguageLBL = "";
            var OptionsWInterfaceLBL = "";
            var OptionsWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                OptionsWHeaderTitle = ConfigurationManager.AppSettings["OptionsWHeaderTitle"];
                OptionsWHeaderLBL = ConfigurationManager.AppSettings["OptionsWHeaderLBL"];
                OptionsWLanguageLBL = ConfigurationManager.AppSettings["OptionsWLanguageLBL"];
                OptionsWInterfaceLBL = ConfigurationManager.AppSettings["OptionsWInterfaceLBL"];
                OptionsWSubmitBTN = ConfigurationManager.AppSettings["OptionsWSubmitBTN"];
            }
            else
            {
                OptionsWHeaderTitle = ConfigurationManager.AppSettings["OptionsWHeaderTitleSE"];
                OptionsWHeaderLBL = ConfigurationManager.AppSettings["OptionsWHeaderLBLSE"];
                OptionsWLanguageLBL = ConfigurationManager.AppSettings["OptionsWLanguageLBLSE"];
                OptionsWInterfaceLBL = ConfigurationManager.AppSettings["OptionsWInterfaceLBLSE"];
                OptionsWSubmitBTN = ConfigurationManager.AppSettings["OptionsWSubmitBTNSE"];
            }
            this.Title = OptionsWHeaderTitle;
            HeaderLabel.Content = OptionsWHeaderLBL;
            SubmitBTN.Content = OptionsWSubmitBTN;
            LanguageLabel.Content = OptionsWLanguageLBL;
            InterfaceLabel.Content = OptionsWInterfaceLBL;
        }
        private void PopulateData()
        {
            LanguageCB.Items.Add("en");
            LanguageCB.Items.Add("sr");
            LanguageCB.SelectedItem = MainWindow.LoggedInAdmin.Language;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string item = (string)LanguageCB.SelectedItem;
            if (!item.Equals(MainWindow.LoggedInAdmin.Language))
            {
                MainWindow.LoggedInAdmin.Language = item;
                AdminDB.UpdateAdminPreferences(MainWindow.LoggedInAdmin);
                if(window != null)
                {
                    window.WriteLanguage();
                }
                if (window2 != null)
                {
                    window2.WriteLanguage();
                }
                Close();
            }
        }
    }
}
