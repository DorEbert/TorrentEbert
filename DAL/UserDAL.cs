using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torrent_Server_Side.Commom.Models;

namespace DAL
{
    public class UserDAL : BaseDAL
    {
        public UserDAL() : base()
        { }

        public bool Login(User user)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_LoginUser", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@User_Name", user.UserName));
            cmd.Parameters.Add(new SqlParameter("@User_Password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@IPAdress", user.IPAdress));
            cmd.Parameters.Add(new SqlParameter("@Port", user.Port));
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value.ToString();
            _connection.Close();
            return result == "1" ? true : false;

        }

        public bool SignUp(User user)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_SignUpUser", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@User_Name", user.UserName));
            cmd.Parameters.Add(new SqlParameter("@User_Password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@First_Name", user.FirstName));
            cmd.Parameters.Add(new SqlParameter("@Last_Name", user.LastName));
            cmd.Parameters.Add(new SqlParameter("@Date_Of_Birth", user.DateOfBirth));
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value.ToString();
            _connection.Close();
            return result == "1" ? true : false;
        }

        public List<string> IpAdressesPerFileID(string FileName)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_GetIpAdressesPerFileID", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FileName", FileName));
            List<string> result = new List<string>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string IpAdress = reader["IPAdress"].ToString(); 
                    result.Add(IpAdress);
                }
            }
            _connection.Close();
            return result;
        }

        public bool UpdateFilesPerUser(User user, List<FilesInfo> filesList)
        {
            _connection.Open();
            foreach (FilesInfo fileInfo in filesList)
            {
                SqlCommand cmd = new SqlCommand("SP_InserFilesIntoTable", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@User_Name", user.UserName));
                cmd.Parameters.Add(new SqlParameter("@FileName", fileInfo.FileName));
                cmd.Parameters.Add(new SqlParameter("@Size", fileInfo.Size));
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
            }
            _connection.Close();
            return "1" == "1" ? true : false;
        }

        public bool LogOut(string username)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("SP_LogOut", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@User_Name", username));
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value.ToString();
            _connection.Close();
            return result == "1" ? true : false;
        }

        public bool Update(User user)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("CreateOrUpdateApplicationUser", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Is_To_Update", "1"));
            cmd.Parameters.Add(new SqlParameter("@Application_User_ID", user.Application_User_ID));
            cmd.Parameters.Add(new SqlParameter("@User_Name", user.UserName));
            cmd.Parameters.Add(new SqlParameter("@User_Password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@First_Name", user.FirstName));
            cmd.Parameters.Add(new SqlParameter("@Last_Name", user.LastName));
            cmd.Parameters.Add(new SqlParameter("@Date_Of_Birth", user.DateOfBirth));
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value.ToString();
            _connection.Close();
            return result == "1" ? true : false;
        }
        
        public bool Delete(string Application_User_ID)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("CreateOrUpdateApplicationUser", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Is_To_Update","0"));
            cmd.Parameters.Add(new SqlParameter("@Application_User_ID", Application_User_ID));
            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value.ToString();
            _connection.Close();
            return result == "1" ? true : false;
        }
    }
}
