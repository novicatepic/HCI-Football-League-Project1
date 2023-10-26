using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HCI_Project1_FootballLeague.Classes
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int ShirtNumber { get; set; }
        public int NumGoals { get; set; }
        public int NumAssists { get; set; }
        public int NumYellowCards { get; set; }
        public int NumRedCards { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfContract { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }

        public Player() { }

        public Player(int playerId, int shirtNum, int numGoals, int numAssists, int numYellowCards, int numRedCards)
        {
            PlayerId = playerId;
            ShirtNumber = shirtNum;
            NumGoals = numGoals;
            NumAssists = numAssists;
            NumYellowCards = numYellowCards;
            NumRedCards = numRedCards;
        }

        public Player(int shirtNumber, int numGoals, int numAssists, int numYellowCards, int numRedCards, string firstName, string lastName, DateTime dateOfContract, int clubId)
        {
            ShirtNumber = (int) shirtNumber;
            NumGoals = numGoals;
            NumAssists = numAssists;
            NumYellowCards = numYellowCards;
            NumRedCards = numRedCards;
            FirstName = firstName;
            LastName = lastName;
            DateOfContract = dateOfContract;
            ClubId = clubId;
        }

        public Player(int playerId, int shirtNumber, int numGoals, int numAssists, int numYellowCards, int numRedCards, string firstName, string lastName, DateTime dateOfContract, int clubId)
        {
            PlayerId = playerId;
            ShirtNumber = shirtNumber;
            NumGoals = numGoals;
            NumAssists = numAssists;
            NumYellowCards = numYellowCards;
            NumRedCards = numRedCards;
            FirstName = firstName;
            LastName = lastName;
            DateOfContract = dateOfContract;
            ClubId = clubId;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
