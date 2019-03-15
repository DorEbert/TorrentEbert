using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Torrent_Server_Side.Commom.Models;

namespace ClassLibrary1
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


    }
}
