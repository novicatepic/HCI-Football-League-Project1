using HCI_Project1_FootballLeague.LeagueAdminTableWindow;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project1_FootballLeague.AdminsStartWindows
{
    /// <summary>
    /// Interaction logic for MainAdminStartWindow.xaml
    /// </summary>
    public partial class MainAdminStartWindow : Window
    {
        private MainWindow window;
        public MainAdminStartWindow(MainWindow win)
        {
            InitializeComponent();
            window = win;
            WriteLanguage();
            DrawStyle();
        }

        public void DrawStyle()
        {
            ShowLeagueAdminsButton.ClearValue(Button.FontSizeProperty);
            ShowOptionsButton.ClearValue(Button.FontSizeProperty);
            Style backgroundStyle = null;
            Style buttonStyle = null;
            if ("Large Buttons - Alice Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundAliceSP");
                buttonStyle = (Style)FindResource("FontLargeBtn");
            }
            else if ("Medium Buttons - Beige Background".Equals(MainWindow.LoggedInAdmin.Look))
            {
                backgroundStyle = (Style)FindResource("BackgroundBeigeSP");
                buttonStyle = (Style)FindResource("FontMediumBtn");
            }
            else
            {
                backgroundStyle = (Style)FindResource("BackgroundTanSP");
                buttonStyle = (Style)FindResource("FontSmallBtn");
            }
            Panel.Style = backgroundStyle;
            foreach (UIElement element in Panel.Children)
            {
                if (element is Button)
                {
                    Button button = (Button)element;
                    button.Style = buttonStyle;
                }
            }
            window.DrawStyle();
        }

        public void WriteLanguage()
        {
            var MainAdminSWTitle = "";
            var ShowLeagueAdminsBTN = "";
            var ShowLeagueAdminsOptionsBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                MainAdminSWTitle = ConfigurationManager.AppSettings["MainAdminSWTitle"];
                ShowLeagueAdminsBTN = ConfigurationManager.AppSettings["ShowLeagueAdminsBTN"];
                ShowLeagueAdminsOptionsBTN = ConfigurationManager.AppSettings["ShowLeagueAdminsOptionsBTN"];
            }
            else
            {
                MainAdminSWTitle = ConfigurationManager.AppSettings["MainAdminSWTitleSE"];
                ShowLeagueAdminsBTN = ConfigurationManager.AppSettings["ShowLeagueAdminsBTNSE"];
                ShowLeagueAdminsOptionsBTN = ConfigurationManager.AppSettings["ShowLeagueAdminsOptionsBTNSE"];
            }
            this.Title = MainAdminSWTitle;
            ShowLeagueAdminsButton.Content = ShowLeagueAdminsBTN;
            ShowOptionsButton.Content = ShowLeagueAdminsOptionsBTN;

            window.WriteLanguage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new LeagueAdminWindow().ShowDialog();
        }

        private void ShowOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.OptionsWindow win = new OptionsWindow.OptionsWindow(this, null);
            win.ShowDialog();         
        }
    }
}
