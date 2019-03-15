using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDAL
    {
        protected string _connectionString;
        protected SqlConnection _connection;

        public BaseDAL()
        {
             _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            _connection = new SqlConnection(_connectionString);
        }
    }
}
