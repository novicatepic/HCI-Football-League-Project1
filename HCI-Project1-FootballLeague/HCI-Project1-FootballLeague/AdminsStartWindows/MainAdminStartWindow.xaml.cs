using HCI_Project1_FootballLeague.LeagueAdminTableWindow;
using System;
using System.Collections.Generic;
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
