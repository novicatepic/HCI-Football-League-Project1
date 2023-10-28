using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class Game
    {

        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public int FixtureNum { get; set; }
        public int SeasonNum { get; set; }


        public Game(DateTime gameDate, int fixtureNum, int seasonNum) {
            GameDate = gameDate;
            FixtureNum = fixtureNum;
            SeasonNum = seasonNum;
        }

        public Game(int gameId, DateTime gameDate, int fixtureNum, int seasonNum)
        {
            GameId = gameId;
            GameDate = gameDate;
            FixtureNum = fixtureNum;
            SeasonNum = seasonNum;
        }
    }
}
