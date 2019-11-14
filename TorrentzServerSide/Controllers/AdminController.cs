using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Torrent_Server_Side.Commom.Models;

namespace TorrentzServerSide.Controllers
{
    public class AdminController : ApiController
    {
        private dbHandlerBL _dbHandler;

        public AdminController()
        {
            _dbHandler = new dbHandlerBL();
        }

        [HttpGet]
        public HttpResponseMessage Users()
        {
            try
            {
                List<User> result = _dbHandler.ListOfUsers();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new DataTable());
            }
        }
        [HttpGet]
        public HttpResponseMessage Files(string search_parameter = "")
        {
            try
            {
                List<FilesInfo> result = _dbHandler.ListOfFile(search_parameter);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch 
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new DataTable());
            }
        }
        
    }
}
