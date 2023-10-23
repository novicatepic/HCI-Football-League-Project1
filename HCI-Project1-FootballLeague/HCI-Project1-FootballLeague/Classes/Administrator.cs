using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project1_FootballLeague.Classes
{
    public class Administrator
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsMainAdmin { get; set; }

        public Administrator() { }

        public Administrator(int adminId, string userName, string password, bool isMainAdmin)
        {
            AdminId = adminId;
            UserName = userName;
            Password = password;
            IsMainAdmin = isMainAdmin;
        }
    }
}
