using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Torrent_Server_Side.Commom.Models;

namespace WebPortal
{
    public partial class AdminPage : System.Web.UI.Page
    {
        public const string ADMIN_FILES_URI = "Admin-Files";
        public const string ADMIN_USERS_URI = "Admin-Users";
        public const string DELETE_USER_URI = "Delete-User";
        public const string UPDATE_USER_URI = "Update-User";
        protected async void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
            using (var client = new HttpClient())
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

                string requestURI = ConfigurationManager.ConnectionStrings[ADMIN_FILES_URI].ToString();
                if (search_parameter != "")
                {
                    
                    response = await client.GetAsync(requestURI  + "/?search_parameter="+ search_parameter.Trim());

                }
                else
                {
                    response = await client.GetAsync(requestURI);
                }
                responseBody = response.Content.ReadAsStringAsync().Result;
                List<FilesInfo> myFilesList = JsonConvert.DeserializeObject<List<FilesInfo>>(responseBody);
                BuildTable(myFilesList, FilesTable);
                return response;
            }
        }

        private async Task InitialUsersTable()
        {
            string requestURI = ConfigurationManager.ConnectionStrings[ADMIN_USERS_URI].ToString();
            using (var client = new HttpClient())
            {
                int NumberOfRows = UsersTable.Rows.Count;
                for (int i = 0; i < NumberOfRows; i++)
                {
                    if (UsersTable.Rows.Count > 1)
                        UsersTable.Rows.Remove(UsersTable.Rows[1]);
                }
                var response = await client.GetAsync(requestURI);
                var responseBody = response.Content.ReadAsStringAsync().Result;
                List<User> myUsersList = JsonConvert.DeserializeObject<List<User>>(responseBody);
                BuildTable(myUsersList, UsersTable);
            }
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
                is_succeed = await DeleteUser_Async(user);
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

        static async Task<bool> DeleteUser_Async(User user)
        {
            using (var client = new HttpClient())
            {
                string requestURI = ConfigurationManager.ConnectionStrings[DELETE_USER_URI].ToString();
                var response = await client.PostAsync(requestURI, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
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
            using (var client = new HttpClient())
            {
                string requestURI = ConfigurationManager.ConnectionStrings[UPDATE_USER_URI].ToString();
                var response = await client.PostAsync(requestURI, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
        }

        protected async void SearchButton_OnClickAsync(object sender, EventArgs e)
        {
            string parameter = txt_Search.Text.ToString();
            var response = await InitialFilesTable(parameter);
           
        }
    }
}