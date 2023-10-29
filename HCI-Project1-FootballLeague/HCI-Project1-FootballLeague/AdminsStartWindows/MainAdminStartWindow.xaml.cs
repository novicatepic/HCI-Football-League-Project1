using HCI_Project1_FootballLeague.LeagueAdminTableWindow;
using System.Windows;

namespace HCI_Project1_FootballLeague.AdminsStartWindows
{
    /// <summary>
    /// Interaction logic for MainAdminStartWindow.xaml
    /// </summary>
    public partial class MainAdminStartWindow : Window
    {
        public MainAdminStartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new LeagueAdminWindow().ShowDialog();
        }
    }
}
