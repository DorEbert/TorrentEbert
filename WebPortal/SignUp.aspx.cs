using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Torrent_Server_Side.Commom.Models;

namespace WebPortal
{
    public partial class SignUp : System.Web.UI.Page
    {
        public const string USERCONTROLLER_SIGNUP = "UserController-SignUp";
        protected void Page_Load(object sender, EventArgs e)
        {
       

        }

        static async Task<bool> SignUp_Async(User user)
        {
            using (var client = new HttpClient())
            {
                string requestURI = ConfigurationManager.ConnectionStrings[USERCONTROLLER_SIGNUP].ToString();
                var response = await client.PostAsync(requestURI, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8,"application/json"));   
                return response.IsSuccessStatusCode;
            }
        }


        protected async void SignUp_ClickAsync(object sender, EventArgs e)
        {
            string TheUserName = txt_UserName.Value;
            string ThePassword = txt_Password.Value;
            string TheFirstName = txt_FirstName.Value;
            string ThelastName = txt_LastName.Value;
            DateTime DateOfBirth = Convert.ToDateTime(dt_DateOfBirth.Value);
            User user = new User();
            user.FirstName = TheFirstName;
            user.LastName = ThelastName;
            user.UserName = TheUserName;
            user.DateOfBirth = DateOfBirth;
            user.Password = ThePassword;
            bool is_succeed = false;
            try
            {
                is_succeed = await SignUp_Async(user);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Signup Successfully')", true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                
            }
            if (!is_succeed)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Did NOT Signup')", true);
                
        }
    }
}