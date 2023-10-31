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
        public string Language { get; set; }
        public string Look { get; set; }

        public Administrator() { }

        public Administrator(string userName, string password, bool isMainAdmin)
        {
            UserName = userName;
            Password = password;
            IsMainAdmin = isMainAdmin;
            
        }
        public Administrator(int adminId, string userName, string password, bool isMainAdmin, string language, string look)
        {
            AdminId = adminId;
            UserName = userName;
            Password = password;
            IsMainAdmin = isMainAdmin;
            Language = language;
            Look = look;
        }
    }
}
