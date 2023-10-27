using HCI_Project1_FootballLeague.Classes;
using HCI_Project1_FootballLeague.DBFunctions;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTB.Text;
            var capacity = CapacityTB.Text;
            var town = TownTB.Text;
            if (!"".Equals(name) && !"".Equals(capacity) && !"".Equals(town))
            {
                Stadium st = new Stadium(s.StadiumId, name, Int32.Parse(capacity), town);
                StadiumDB.UpdateStadium(st);
                sw.DrawData();
                Close();
            }
            else
            {
                MessageBox.Show("Name, capacity or town not entered!");
            }
        }
    }
}
