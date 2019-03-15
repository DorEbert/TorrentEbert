using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torrent_Server_Side.Commom.Models;

namespace DAL
{
    public class AdminDAL:BaseDAL
    {
        public AdminDAL() : base()
        { }

        public List<User> GetListOfUsers()
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_UserInfo", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            List<User> result = new List<User>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var _user = new User();
                    _user.Application_User_ID = Convert.ToInt32(reader["Application_User_ID"]);
                    _user.UserName = reader["User_Name"].ToString();
                    _user.Password = reader["User_Password"].ToString();
                    _user.FirstName = reader["First_Name"].ToString();
                    _user.LastName = reader["Last_Name"].ToString();
                    _user.IPAdress = reader["IPAdress"].ToString();
                    _user.Port = reader["Port"].ToString();
                    _user.DateOfBirth = (DateTime)reader["Date_Of_Birth"];
                    _user.Is_Active = (bool)reader["Is_Active"];
                    result.Add(_user);
                }
            }
            _connection.Close();
            return result;
        }

        public List<FilesInfo> GetListOfFiles(string search_parameter = "")
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_FileInfo", _connection);
            cmd.Parameters.Add(new SqlParameter("@search_parameter", search_parameter));
            cmd.CommandType = CommandType.StoredProcedure;
            List<FilesInfo> result = new List<FilesInfo>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var _file = new FilesInfo();
                    _file.Files_ID = Convert.ToInt32(reader["Files_ID"]);
                    _file.FileName = reader["FileName"].ToString();
                    _file.Location = " ";
                    _file.Type = " ";
                    _file.Size = Convert.ToInt32(reader["Size"]);
                    _file.Amount_Of_Peers = Convert.ToInt32(reader["Amount_Of_Peers"]);
                    result.Add(_file);
                }
            }
            _connection.Close();
            return result;
        }
    }
}
