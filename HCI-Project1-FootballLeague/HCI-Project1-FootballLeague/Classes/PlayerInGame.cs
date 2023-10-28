using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class PlayerInGame
    {

        public int PlayerId { get; set; }
        public int NumGoalsInGame { get; set; }
        public int NumAssistsInGame { get; set; }
        public bool HasYellow { get; set; }
        public bool HasRed { get; set; }
        public int ClubId { get; set; }
        public int GameId { get; set; }
        public bool StartedGame { get; set; }
        public int MinutesPlayed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFromHomeTeam { get; set; }

        public PlayerInGame() { }

        public PlayerInGame(int playerId, int numGoalsInGame, int numAssistsInGame, bool hasYellow, bool hasRed, int clubId, int gameId, bool startedGame, int minutesPlayed)
        {
            PlayerId = playerId;
            NumGoalsInGame = numGoalsInGame;
            NumAssistsInGame = numAssistsInGame;
            HasYellow = hasYellow;
            HasRed = hasRed;
            ClubId = clubId;
            GameId = gameId;
            StartedGame = startedGame;
            MinutesPlayed = minutesPlayed;
        }
    }
}
