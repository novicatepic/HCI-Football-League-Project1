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

namespace HCI_Project1_FootballLeague.StadiumWindows
{
    /// <summary>
    /// Interaction logic for UpdateStadium.xaml
    /// </summary>
    public partial class UpdateStadium : Window
    {
        private StadiumWindow sw = null;
        private Stadium s = null;
        public UpdateStadium(StadiumWindow mainStadiumWindow, Stadium s)
        {
            InitializeComponent();
            sw = mainStadiumWindow;
            this.s = s;
            NameTB.Text = s.Name;
            CapacityTB.Text = s.Capacity.ToString();
            TownTB.Text = s.Town;
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
            var UpdateStadiumWTitle = "";
            var UpdateStadiumWHeaderLBL = "";
            var UpdateStadiumWNameLBL = "";
            var UpdateStadiumWCapacityBL = "";
            var UpdateStadiumWTownLBL = "";
            var UpdateStadiumWSubmitBTN = "";

            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                UpdateStadiumWTitle = ConfigurationManager.AppSettings["UpdateStadiumWTitle"];
                UpdateStadiumWHeaderLBL = ConfigurationManager.AppSettings["UpdateStadiumWHeaderLBL"];
                UpdateStadiumWNameLBL = ConfigurationManager.AppSettings["UpdateStadiumWNameLBL"];
                UpdateStadiumWCapacityBL = ConfigurationManager.AppSettings["UpdateStadiumWCapacityBL"];
                UpdateStadiumWTownLBL = ConfigurationManager.AppSettings["UpdateStadiumWTownLBL"];
                UpdateStadiumWSubmitBTN = ConfigurationManager.AppSettings["UpdateStadiumWSubmitBTN"];
            }
            else
            {
                UpdateStadiumWTitle = ConfigurationManager.AppSettings["UpdateStadiumWTitleSE"];
                UpdateStadiumWHeaderLBL = ConfigurationManager.AppSettings["UpdateStadiumWHeaderLBLSE"];
                UpdateStadiumWNameLBL = ConfigurationManager.AppSettings["UpdateStadiumWNameLBLSE"];
                UpdateStadiumWCapacityBL = ConfigurationManager.AppSettings["UpdateStadiumWCapacityBLSE"];
                UpdateStadiumWTownLBL = ConfigurationManager.AppSettings["UpdateStadiumWTownLBLSE"];
                UpdateStadiumWSubmitBTN = ConfigurationManager.AppSettings["UpdateStadiumWSubmitBTNSE"];
            }
            this.Title = UpdateStadiumWTitle;
            NameLabel.Content = UpdateStadiumWNameLBL;
            HeaderLabel.Content = UpdateStadiumWHeaderLBL;
            CapacityLabel.Content = UpdateStadiumWCapacityBL;
            TownLabel.Content = UpdateStadiumWTownLBL;
            SubmitBTN.Content = UpdateStadiumWSubmitBTN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var capacity = CapacityTB.Text;
            var town = TownTB.Text;
            
            try
            {
                int intCapacity = Int32.Parse(capacity);
                if (intCapacity >= 0 && !"".Equals(name) && !"".Equals(capacity) && !"".Equals(town))
                {
                    Stadium st = new Stadium(s.StadiumId, name, Int32.Parse(capacity), town);
                    StadiumDB.UpdateStadium(st);
                    sw.DrawData();
                    Close();
                }
                else
                {
                    NoInputMessage();
                }
            } catch(Exception ex)
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
