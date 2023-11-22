using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class ClubInGame
    {
        public int ClubId { get; set; }
        public int GameId { get; set; }
        public int NumGoalsScored { get; set; }
        public bool IsHomeTeam { get; set; }


        public ClubInGame() { }

        public ClubInGame(int clubId, int gameId, int numGoalsScored, bool isHomeTeam)
        {
            ClubId = clubId;
            GameId = gameId;
            NumGoalsScored = numGoalsScored;
            IsHomeTeam = isHomeTeam;
        }
    }
}
