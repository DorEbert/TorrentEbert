using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Torrent_Server_Side.Commom.Models;

namespace TorrentzServerSide.Controllers
{
    public class UserController : ApiController
    {
        private UserBL _users;

        public UserController()
        {
            _users = new UserBL();
        }

        [HttpPost]
        public IHttpActionResult Login([FromBody]User user)
        {
            try
            {
                _users.Login(user);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult SignUp(User user)
        {
            try
            {
                _users.SignUp(user);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public IHttpActionResult DeleteUser(User user)
        {
            try
            {
                _users.Delete(user.Application_User_ID.ToString());
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateUser(User user)
        {
            try
            {
                _users.Update(user);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateFilesPerUser(FilesPerUser filesPerUser)
        {
            try
            {
                _users.UpdateFilesPerUser(filesPerUser._User, filesPerUser._FilesList);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet]
        public IHttpActionResult Logout(string username = "")
        {
            try
            {
                _users.LogOut(username);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet]
        public HttpResponseMessage IpAdressesPerFileID(string FileName)
        {
            try
            {
                List<string> result = _users.IpAdressesPerFileID(FileName);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new List<string>());
            }
        }
        
    }
}
