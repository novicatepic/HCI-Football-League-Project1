using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class GameInfo
    {
        public int HomeClubId { get; set; }
        public int AwayClubId { get; set; }
        public int GameId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public string Result { get; set; }
        public DateTime GameDate { get; set; }
       

        public GameInfo(int homeClubId, int awayClubId,int gameId, string homeTeamName, string awayTeamName, int homeTeamGoals, int awayTeamGoals, DateTime gameDate)
        {
            HomeClubId = homeClubId;
            AwayClubId = awayClubId;
            GameId = gameId;
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
            HomeTeamGoals = homeTeamGoals;
            AwayTeamGoals = awayTeamGoals;
            Result = HomeTeamGoals + "-" + AwayTeamGoals;
            GameDate = gameDate;
        }

        public override string ToString()
        {
            return HomeTeamName + " - " + AwayTeamName;
        }
    }
}
