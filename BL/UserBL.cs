using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;
using DAL;
using Torrent_Server_Side.Commom.Models;

namespace BL
{
    public class UserBL
    {
        private UserDAL _users;

        public UserBL()
        {
            _users = new UserDAL();            
        }

        public void Login(User user)
        {
            string message;
            if (!ValidateBasicUser(user, out message))
                throw new MissingInformationException(message);

            if (!_users.Login(user))
                throw new IllegalException();
        }

        public void SignUp(User user)
        {
            string message;
            if(!ValidateBasicUser(user, out message))
                throw  new MissingInformationException(message);
            _users.SignUp(user);
        }

        public bool ValidateBasicUser(User user, out string message)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                message = "User Name";
                return false;
            }

            if (string.IsNullOrEmpty((user.Password)))
            {
                message = "Password";
                return false;
            }
            message = string.Empty;
            return true;
        }

        public void Delete(string Application_User_ID)
        {
            if (!_users.Delete(Application_User_ID))
                throw new IllegalException();
        }

        public void Update(User user)
        {
            if (!_users.Update(user))
                throw new IllegalException();
        }

        public void LogOut(string username)
        {
            if (!_users.LogOut(username))
                throw new IllegalException();
        }

        public void UpdateFilesPerUser(User user, List<FilesInfo> filesList)
        {
            if(!_users.UpdateFilesPerUser(user, filesList))
               throw new NotImplementedException();
        }

        public List<string> IpAdressesPerFileID(string FileName)
        {
            return _users.IpAdressesPerFileID(FileName);
          
        }
    }
}
