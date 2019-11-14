using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torrent_Server_Side.Commom.Models;
using System.Configuration;
using System.Net.Http;
using DAL;
using TorrentzServerSide.Controllers;
using Proxy;

namespace WebPortal.Tests
{
    [TestClass()]
    public class SignUpTest
    {
        public const string USERCONTROLLER_SIGNUP = "UserController-SignUp";
        public AdminDAL _admin { get; set; }
        public UserDAL _users { get; set; }
        public Proxy.Proxy _proxy;

        [TestMethod()]
        public void SignUp()
        {
            var sut = new UserController();
            //string requestURI = ConfigurationManager.ConnectionStrings[USERCONTROLLER_SIGNUP].ToString();
            List<User> users = new List<User>();
            _users = new UserDAL();
            _proxy = new Proxy.Proxy();
            _admin = new AdminDAL();
            int amount_of_users = _admin.GetListOfUsers().Count;
            for (int i = 0; i < 10; i++)
            {
                User user = new User();
                user.FirstName = "c" + i;
                user.LastName = "c" + i;
                user.UserName = "c" + i;
                user.DateOfBirth = DateTime.Now;
                user.Password = "c" + i;
                users.Add(user);
                string requestURI = ConfigurationManager.ConnectionStrings[USERCONTROLLER_SIGNUP].ToString();
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8,
                    "application/json");
                sut.SignUp(user);
                //_users.SignUp(user);
            }
            Assert.AreEqual(_admin.GetListOfUsers().Count, users.Count + amount_of_users);
        }
    }
}