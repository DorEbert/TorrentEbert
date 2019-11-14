using Se.Url;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Torrent_Server_Side.Commom.Models;

namespace Proxy
{
    public class Proxy
    {
        public User _user { get; set; }

        public Proxy()
        {
        
        }
        public async Task<bool> Login(string User_Name, string password, string IPAdress, string connection_string)
        {
            _user = new User();
            _user.UserName = User_Name;
            _user.Password = password;
            _user.IPAdress = IPAdress;
            // GetLocalIPAddress();
            UdpClient udpClient = new UdpClient(0);
            _user.Port = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port.ToString();
            bool is_succeed = await Login_Async(_user, connection_string);
            return is_succeed;
        }
        public async Task<bool> Login_Async(User user, string the_connection_string)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(the_connection_string, user);
                return response.IsSuccessStatusCode;
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetListOfFileAsync(string requestURI, string search_parameter)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;
                if (search_parameter != "")
                {
                    response = await client.GetAsync(requestURI + "/?search_parameter=" + search_parameter.Trim());
                }
                else
                {
                    response = await client.GetAsync(requestURI);
                }
                return response;
            }
        }
        public async Task CloseWindowReport(string connection_string)
        {
            string url = connection_string + "/?username=" + _user.UserName;
            using (var client = new HttpClient())
            {
                // todo change get to put
                HttpResponseMessage response = await client.GetAsync(url);
            }
        }
        public async Task UpdateFilesAtServer(string connectionString, FilesPerUser filesPerUser)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(connectionString, filesPerUser);
                //  return response.IsSuccessStatusCode;
            }
        }
        public async Task<string> GetListOfIPAdressPerFile(string connection_string,string FileName)
        {
            using (var client = new HttpClient())
            {
                string url = connection_string + "/?FileName=" + FileName;
                HttpResponseMessage response;
                response = await client.GetAsync(url);
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }
        public async Task<bool> DeleteUser_Async(string requestURI,int Application_User_ID)//User user, StringContent content)
        {
            UrlBuilder urlb = new UrlBuilder(requestURI);
            urlb.AppendQueryParam("Application_User_ID", Application_User_ID.ToString());
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(urlb.ToString());
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<bool> UpdateUser(string requestURL, User user, StringContent content)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(requestURL, content);
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<string> getUsersFromServer(string requestURI)
        {
            string responseBody;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestURI);
                responseBody = response.Content.ReadAsStringAsync().Result;
            }

            return responseBody;
        }
        public async Task<bool> SignUpUser(User user, string requestURI,StringContent content)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(requestURI, content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
