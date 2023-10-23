using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class Stadium
    {

        public int StadiumId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public String Town { get; set; }

        public Stadium() { }

        public Stadium(string name, int capacity, String town)
        {
            Name = name;
            Capacity = capacity;
            Town = town;
        }
        public Stadium(int stadiumId, string name, int capacity, String town)
        {
            StadiumId = stadiumId;
            Name = name;
            Capacity = capacity;
            Town = town;
        }
    }
}
