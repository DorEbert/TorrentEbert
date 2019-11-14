using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class FireBaseUser
    {
        public FireBaseUser()
        {
        }

        public FireBaseUser(string UserName, string Password, string FullName, string Gender, string NICno)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.FullName = FullName;
            this.Gender   = Gender;
            this.NICno = NICno;
        }
        public virtual bool Equals(FireBaseUser user)
        {
            //string error;
            if (user == null || this == null)
            {
                return false;
            }

            if (!user.UserName.Equals(this.UserName))
            {
            //    error = "UserName does not Exist!";
                return false;
            }else if (!user.Password.Equals(this.Password))
            {
               // error = "UserName and Password does not match!";
                return false;
            }
            return true;

        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string NICno { get; set; }
    }
}
