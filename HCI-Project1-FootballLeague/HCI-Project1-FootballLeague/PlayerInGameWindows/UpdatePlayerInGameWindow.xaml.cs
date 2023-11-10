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

namespace HCI_Project1_FootballLeague.PlayerInGameWindows
{
    /// <summary>
    /// Interaction logic for UpdatePlayerInGameWindow.xaml
    /// </summary>
    public partial class UpdatePlayerInGameWindow : Window
    {
        private PlayerInGameStartWindow window;
        private PlayerInGame player;
        private bool isHomePlayer;
        private GameInfo gameInfo;
        public UpdatePlayerInGameWindow(PlayerInGameStartWindow win, PlayerInGame pig, bool isHomeTeam, GameInfo gameInfo, int season, int fixtureNum)
        {
            InitializeComponent();
            window = win;
            this.player = pig;
            this.isHomePlayer = isHomeTeam;
            this.gameInfo = gameInfo;
            PopulateData();
            WriteLanguage();
            DrawStyle();

            if (isHomePlayer)
            {
                TeamLBL.Content = AddPlayerTeamLBL + gameInfo.HomeTeamName;
            }
            else
            {
                TeamLBL.Content = AddPlayerTeamLBL + gameInfo.AwayTeamName;
            }
            PlayerLBL.Content = UpdatePlayerIGPlayerLBL + player.FirstName + " " + player.LastName;
            SeasonLBL.Content = AddPlayerSeasonLBL + season;
            FixtureNumLBL.Content = AddPlayerFixtureLBL + fixtureNum;

        }

        string AddPlayerTeamLBL = "";
        string AddPlayerFixtureLBL = "";
        string AddPlayerSeasonLBL = "";
        string UpdatePlayerIGPlayerLBL = "";
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
            var UpdatePlayerInGameWTitle = "";
            var UpdatePlayerInGameWHeaderLBL = "";
            var UpdatePlayerInGameWNumGoalsLBL = "";
            var UpdatePlayerInGameWNumAssistsLBL = "";
            var UpdatePlayerInGameWMinutesPlayedLBL = "";

            var UpdatePlayerInGameWYellowCardCB = "";
            var UpdatePlayerInGameWRedCardCB = "";
            var UpdatePlayerInGameWStartedGameCB = "";
            var UpdatePlayerInGameWSubmitBTN = "";
            if ("en".Equals(MainWindow.LoggedInAdmin.Language))
            {
                UpdatePlayerInGameWTitle = ConfigurationManager.AppSettings["UpdatePlayerInGameWTitle"];
                UpdatePlayerInGameWNumGoalsLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWNumGoalsLBL"];
                UpdatePlayerInGameWHeaderLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWHeaderLBL"];
                UpdatePlayerInGameWNumAssistsLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWNumAssistsLBL"];
                UpdatePlayerInGameWMinutesPlayedLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWMinutesPlayedLBL"];

                UpdatePlayerInGameWYellowCardCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWYellowCardCB"];
                UpdatePlayerInGameWRedCardCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWRedCardCB"];
                UpdatePlayerInGameWStartedGameCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWStartedGameCB"];
                UpdatePlayerInGameWSubmitBTN = ConfigurationManager.AppSettings["UpdatePlayerInGameWSubmitBTN"];

                AddPlayerTeamLBL = ConfigurationManager.AppSettings["AddPlayerTeamLBL"];
                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBL"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBL"];
                UpdatePlayerIGPlayerLBL = ConfigurationManager.AppSettings["UpdatePlayerIGPlayerLBL"];
            }
            else
            {
                UpdatePlayerInGameWTitle = ConfigurationManager.AppSettings["UpdatePlayerInGameWTitleSE"];
                UpdatePlayerInGameWNumGoalsLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWNumGoalsLBLSE"];
                UpdatePlayerInGameWHeaderLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWHeaderLBLSE"];
                UpdatePlayerInGameWNumAssistsLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWNumAssistsLBLSE"];
                UpdatePlayerInGameWMinutesPlayedLBL = ConfigurationManager.AppSettings["UpdatePlayerInGameWMinutesPlayedLBLSE"];

                UpdatePlayerInGameWYellowCardCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWYellowCardCBSE"];
                UpdatePlayerInGameWRedCardCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWRedCardCBSE"];
                UpdatePlayerInGameWStartedGameCB = ConfigurationManager.AppSettings["UpdatePlayerInGameWStartedGameCBSE"];
                UpdatePlayerInGameWSubmitBTN = ConfigurationManager.AppSettings["UpdatePlayerInGameWSubmitBTNSE"];

                AddPlayerTeamLBL = ConfigurationManager.AppSettings["AddPlayerTeamLBLSE"];
                AddPlayerFixtureLBL = ConfigurationManager.AppSettings["AddPlayerFixtureLBLSE"];
                AddPlayerSeasonLBL = ConfigurationManager.AppSettings["AddPlayerSeasonLBLSE"];
                UpdatePlayerIGPlayerLBL = ConfigurationManager.AppSettings["UpdatePlayerIGPlayerLBLSE"];
            }
            this.Title = UpdatePlayerInGameWTitle;
            MinsPlayedLabel.Content = UpdatePlayerInGameWMinutesPlayedLBL;
            HeaderLabel.Content = UpdatePlayerInGameWHeaderLBL;
            SubmitBTN.Content = UpdatePlayerInGameWSubmitBTN;
            YellowCardCB.Content = UpdatePlayerInGameWYellowCardCB;
            RedCardCB.Content = UpdatePlayerInGameWRedCardCB;
            StartedGameCB.Content = UpdatePlayerInGameWStartedGameCB;
            NumGoalsLabel.Content = UpdatePlayerInGameWNumGoalsLBL;
            NumAssistsLabel.Content = UpdatePlayerInGameWNumAssistsLBL;
        }
        private void PopulateData()
        {
            NumGoalsTB.Text = player.NumGoalsInGame.ToString();
            NumAssistsTB.Text = player.NumAssistsInGame.ToString();
            MinutesPlayedTB.Text = player.MinutesPlayed.ToString();
            if(player.HasYellow)
            {
                YellowCardCB.IsChecked = true;
            } else
            {
                YellowCardCB.IsChecked = false;
            }
            if (player.HasRed)
            {
                RedCardCB.IsChecked = true;
            }
            else
            {
                RedCardCB.IsChecked = false;
            }
            if(player.StartedGame)
            {
                StartedGameCB.IsChecked = true;
            } else
            {
                StartedGameCB.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var goals = NumGoalsTB.Text;
            var assists = NumAssistsTB.Text;
            var minutesPlayed = MinutesPlayedTB.Text;
            bool gotYellow = false;
            if ((bool)YellowCardCB.IsChecked)
            {
                gotYellow = true;
            }
            bool gotRed = false;
            if ((bool)RedCardCB.IsChecked)
            {
                gotRed = true;
            }
            bool startedGame = false;
            if ((bool)StartedGameCB.IsChecked)
            {
                startedGame = true;
            }

            ClubInGame cig = null;
            List<PlayerInGame> playersFromTeamAndGame = new List<PlayerInGame>();
            int teamScored = 0;
            if (isHomePlayer)
            {
                cig = FootballClubDB.GetClubInGame(gameInfo.HomeClubId, gameInfo.GameId);
                foreach (PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.HomeClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGame.Add(pig);
                }
                teamScored = gameInfo.HomeTeamGoals;
            }
            else
            {
                cig = FootballClubDB.GetClubInGame(gameInfo.AwayClubId, gameInfo.GameId);
                foreach (PlayerInGame pig in PlayerDB.GetPlayersFromClubAndGame(gameInfo.AwayClubId, gameInfo.GameId))
                {
                    playersFromTeamAndGame.Add(pig);
                }
                teamScored = gameInfo.AwayTeamGoals;
            }
            int currGoals = 0;
            int currAssists = 0;
            foreach (PlayerInGame pig in playersFromTeamAndGame)
            {
                if(pig.PlayerId != player.PlayerId)
                {
                    currGoals += pig.NumGoalsInGame;
                    currAssists += pig.NumAssistsInGame;
                }
                
            }
            int intGoals = Int32.Parse(goals);
            int intAssists = Int32.Parse(assists);
            int intMinutesPlayer = Int32.Parse(minutesPlayed);
            


            if (intGoals>=0 && intAssists >=0 && intMinutesPlayer >=0 && intMinutesPlayer <=90 && !"".Equals(goals) && !"".Equals(assists) && !"".Equals(minutesPlayed))
            {
                if (intGoals + currGoals > teamScored || intAssists + currAssists > teamScored)
                {
                    MessageBox.Show("Too many goals and assists!");
                } else
                {
                    PlayerInGame pig = new PlayerInGame(player.PlayerId, Int32.Parse(goals), Int32.Parse(assists), gotYellow, gotRed, player.ClubId, player.GameId, startedGame, Int32.Parse(minutesPlayed));
                    pig.FirstName = player.FirstName;
                    pig.LastName = player.LastName;
                    PlayerDB.UpdatePlayerInGame(pig);
                    foreach (var player in window.DataGridXAML.Items)
                    {
                        PlayerInGame p = (PlayerInGame)player;
                        if (p.PlayerId == pig.PlayerId)
                        {
                            window.DataGridXAML.Items.Remove(player);
                            break;
                        }
                    }
                    window.DataGridXAML.Items.Add(pig);
                    Close();
                }
                    
            }
            else
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
