using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class FootballClub
    {

        public int ClubId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NumTrophies { get; set; }
        public int StadiumId { get; set; }

        public FootballClub() { }


        public FootballClub(string name, DateTime date, int numTrophies, int stadiumId)
        {
            Name = name;
            Date = date;
            NumTrophies= numTrophies;
            StadiumId = stadiumId;
        }

        public FootballClub(int clubId, string name, DateTime date, int numTrophies, int stadiumId)
        {
            ClubId = clubId;
            Name = name;
            Date = date;
            NumTrophies = numTrophies;
            StadiumId = stadiumId;
        }
    }
}
