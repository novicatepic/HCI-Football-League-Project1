using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class SeasonStats
    {

        public int NumGamesPlayed { get; set; }
        public int NumWins { get; set; }
        public int NumDraws { get; set; }
        public int NumLoses { get; set; }
        public int NumScored { get; set; }
        public int NumConceded { get; set; }
        public int ClubId { get; set; }
        public int NumPoints { get; set; }
        public int SeasonNum { get; set; }

        public SeasonStats() { }

        public SeasonStats(int numGamesPlayed, int numWins, int numDraws, int numLoses, int numScored, int numConceded, int clubId, int numPoints, int seasonNum)
        {
            NumGamesPlayed = numGamesPlayed;
            NumWins = numWins;
            NumDraws = numDraws;
            NumLoses = numLoses;
            NumScored = numScored;
            NumConceded = numConceded;
            ClubId = clubId;
            NumPoints = numPoints;
            SeasonNum = seasonNum;
        }
    }
}
