using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
using HCI_Project1_FootballLeague.StadiumWindows;
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

namespace HCI_Project1_FootballLeague.TeamWindows
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddNewStadiumWindow : Window
    {
        private StadiumWindow sw = null;
        public AddNewStadiumWindow(StadiumWindow mainStadiumWindow)
        {
            InitializeComponent();
            sw = mainStadiumWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var capacity = CapacityTB.Text;
            var town = TownTB.Text;
            int intCapacity = Int32.Parse(capacity);
            if (intCapacity>0 && !"".Equals(name) && !"".Equals(capacity) && !"".Equals(town))
            {
                Stadium stadium = new Stadium(name, Int32.Parse(capacity), town);
                StadiumDB.AddStadium(stadium);
                Close();
                sw.DrawData();
            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }
    }
}
