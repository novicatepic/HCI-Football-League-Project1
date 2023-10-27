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

namespace HCI_Project1_FootballLeague.PlayerWindows
{
    /// <summary>
    /// Interaction logic for PlayerShowWindow.xaml
    /// </summary>
    public partial class PlayerShowWindow : Window
    {
        public PlayerShowWindow()
        {
            InitializeComponent();
            PopulateData();
        }

        private List<FootballClub> clubs = FootballClubDB.GetClubs();

        private void PopulateData()
        {
            List<Player> players = PlayerDB.GetPlayers();
            foreach (Player p in players)
            {
                p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                DataGridXAML.Items.Add(p);
            }
        }

        public void DrawData()
        {
            DataGridXAML.Items.Clear();
            PopulateData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            AddPlayerWindow apw = new AddPlayerWindow(this);
            apw.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                var selectedPlayer = (Player)DataGridXAML.SelectedItem;
                PlayerDB.DeletePlayer(selectedPlayer.PlayerId);
                DrawData();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private static void NotSelectedMessage()
        {
            MessageBox.Show("Item not selected!");
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem != null)
            {
                UpdatePlayerWindow upw = new UpdatePlayerWindow(this, (Player)DataGridXAML.SelectedItem);
                upw.ShowDialog();
            }
            else
            {
                NotSelectedMessage();
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ("".Equals(SearchTB.Text))
            {
                DrawData();
            }
            else
            {
                DataGridXAML.Items.Clear();
                List<Player> players = PlayerDB.SearchPlayers(SearchTB.Text);
                foreach (Player p in players)
                {
                    p.ClubName = PlayerDB.GetClubName(p.PlayerId.ToString());
                    DataGridXAML.Items.Add(p);
                }
            }
        }
    }
}
