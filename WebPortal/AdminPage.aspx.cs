using Newtonsoft.Json;
using Proxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Torrent_Server_Side.Commom.Models;

namespace WebPortal
{
    public partial class AdminPage : System.Web.UI.Page
    {
        public const string ADMIN_FILES_URL = "Admin-Files";
        public const string ADMIN_USERS_URL = "Admin-Users_Table";
        public const string DELETE_USER_URL = "Delete-User";
        public const string UPDATE_USER_URL = "Update-User";
        private Proxy.Proxy _proxy;

        protected async void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _proxy = new Proxy.Proxy();
                await onloadAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
        async Task<bool> onloadAsync()
        {
                HttpResponseMessage response;
                response = await InitialFilesTable();
                await InitialUsersTable();
                return response.IsSuccessStatusCode;
            

        }
        private async Task<HttpResponseMessage> InitialFilesTable(string search_parameter = "")
        {
            int NumberOfRows;
            HttpResponseMessage response;
            string responseBody;
            NumberOfRows = FilesTable.Rows.Count;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (FilesTable.Rows.Count > 1)
                    FilesTable.Rows.Remove(FilesTable.Rows[1]);
            }
            string requestURI = ConfigurationManager.ConnectionStrings[ADMIN_FILES_URL].ToString();
            response = await _proxy.GetListOfFileAsync(requestURI, search_parameter.Trim());
            responseBody = response.Content.ReadAsStringAsync().Result;
            List<FilesInfo> myFilesList = JsonConvert.DeserializeObject<List<FilesInfo>>(responseBody);
            BuildTable(myFilesList, FilesTable);
            return response;
        }

        private async Task InitialUsersTable()
        {
            string requestURI = ConfigurationManager.ConnectionStrings[ADMIN_USERS_URL].ToString();
            List<User> myUsersList;
            string responseBody;
            int NumberOfRows = UsersTable.Rows.Count;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (UsersTable.Rows.Count > 1)
                    UsersTable.Rows.Remove(UsersTable.Rows[1]);
            }
            responseBody = await _proxy.getUsersFromServer(requestURI);
            myUsersList = JsonConvert.DeserializeObject<List<User>>(responseBody); BuildTable(myUsersList, UsersTable);
        }
       

        public void BuildTable<T>(List<T> Data,HtmlTable theTable)
        {
            HtmlTableRow htColumnsRow = new HtmlTableRow();
            Data.ForEach(delegate (T obj)
            {
                HtmlTableRow htRow = new HtmlTableRow();
                obj.GetType().GetProperties().ToList().ForEach(delegate (PropertyInfo prop)
                {
                    HtmlTableCell htCell = new HtmlTableCell();
                    htCell.InnerText = prop.GetValue(obj, null).ToString();
                    htRow.Cells.Add(htCell);
                });
                theTable.Rows.Add(htRow);
            });
        }
        protected async void DeleteButtonAsync(object sender, EventArgs e)
        {
            var user = GetUser();
            bool is_succeed = false;
            try
            {
                string requestURI = ConfigurationManager.ConnectionStrings[DELETE_USER_URL].ToString();
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                is_succeed = await _proxy.DeleteUser_Async(requestURI,user.Application_User_ID);//, content);
                await onloadAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

            }
            if (!is_succeed)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Did NOT Signup')", true);

        }

        private User GetUser()
        {
            int user_id = Convert.ToInt32(Application_User_ID.Text);
            string TheUserName = txt_UserName.Text;
            string ThePassword = txt_Password.Text;
            string TheFirstName = txt_FirstName.Text;
            string ThelastName = txt_LastName.Text;
            DateTime DateOfBirth = Convert.ToDateTime(dt_DateOfBirth.Value);

            User user = new User();
            user.Application_User_ID = user_id;
            user.FirstName = TheFirstName;
            user.LastName = ThelastName;
            user.UserName = TheUserName;
            user.DateOfBirth = DateOfBirth;
           
            user.Password = ThePassword;
            return user;
        }
        protected async void UpdateButtonAsync(object sender, EventArgs e)
        {
            var user = GetUser();
            bool is_succeed = false;
            try
            {
                is_succeed = await UpdateUser_Async(user);
                await onloadAsync();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Signup Successfully')", true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

            }
            if (!is_succeed)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Did NOT Signup')", true);
        }

        async Task<bool> UpdateUser_Async(User user)
        {
            string requestURL = ConfigurationManager.ConnectionStrings[UPDATE_USER_URL].ToString();
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8,
                "application/json");
            return await _proxy.UpdateUser(requestURL, user, content);
        }    

        protected async void SearchButton_OnClickAsync(object sender, EventArgs e)
        {
            string parameter = txt_Search.Text.ToString();
            var response = await InitialFilesTable(parameter);
           
        }
    }
}