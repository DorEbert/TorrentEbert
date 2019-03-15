using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Torrent_Server_Side.Commom.Models;

namespace BL
{
    public class dbHandlerBL
    {
        private AdminDAL _admin;
        public dbHandlerBL()
        {
            _admin = new AdminDAL();
        }

        public List<User> ListOfUsers()
        {
            List<User> result = _admin.GetListOfUsers();
            return result;
        }

        public List<FilesInfo> ListOfFile(string search_parameter = "")
        {
            List<FilesInfo> result = _admin.GetListOfFiles(search_parameter);
            return result;
        }
    }
   

}
