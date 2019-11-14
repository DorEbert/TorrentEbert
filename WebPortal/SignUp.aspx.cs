using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Torrent_Server_Side.Commom.Models;

namespace WebPortal
{
    public partial class SignUp : System.Web.UI.Page
    {
        public const string USERCONTROLLER_SIGNUP = "UserController-SignUp";
        private Proxy.Proxy _proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            _proxy = new Proxy.Proxy();
        }

        public async Task<bool> SignUp_Async(User user)
        {
            string requestURI = ConfigurationManager.ConnectionStrings[USERCONTROLLER_SIGNUP].ToString();
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8,
                "application/json");
            return await _proxy.SignUpUser(user, requestURI, content);
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