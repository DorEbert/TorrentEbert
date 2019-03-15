using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torrent_Server_Side.Commom.Models
{
    public class User
    {
        public int Application_User_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IPAdress { get; set; }
        public string Port { get; set; }
        public DateTime DateOfBirth { get; set; }
        public  bool Is_Active { get; set; }
    }
}
