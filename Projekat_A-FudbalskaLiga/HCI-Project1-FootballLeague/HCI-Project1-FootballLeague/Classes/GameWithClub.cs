using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class GameWithClub
    {
        public int ClubId { get; set; }
        public int GameId { get; set; }
        public string TeamName { get; set; }
        
        public bool IsHomeTeam { get; set; }
        
        public DateTime GameDate { get; set; }
        public int GoalsScored { get; set; }


        public GameWithClub(int clubId, int gameId, string teamName, bool isHomeTeam, DateTime gameDate, int goalsScored)
        {
            ClubId = clubId;
            GameId = gameId;
            TeamName = teamName;
            IsHomeTeam = isHomeTeam;
            GameDate = gameDate;
            GoalsScored = goalsScored;
        }

        public override string ToString()
        {
            return TeamName;
        }

    }
}
