<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="AdminPage.aspx.cs" Inherits="WebPortal.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
</head>
<body>
     <form id="Users_Table" runat="server">
     <h1>Users_Table</h1>
         <asp:Panel ID="Panel2" runat="server" Height="465px" Width="920px">
        <table id="UsersTable" runat="server" class="auto-style1" style="width:100%; margin-top: 0px;">
            <thead>
            <tr>
                <th>User ID</th>
                <th>User Name</th>
                <th>Password</th>
                <th>FirstName</th> 
                <th>LastName</th>
                <th>IP Adress</th>
                <th>Port</th>
                <th>DateOfBirth</th>
                <th>Is_Active</th>
            </tr>
            </thead>
        </table>
        </asp:Panel>
     <asp:TextBox ID="Application_User_ID" type="hidden" name="ApplicationApplicationUsers_ID" runat="server" ></asp:TextBox>
         <asp:Panel ID="Panel1" runat="server" Height="465px" Width="689px">
             <label for="UserName"><b>UserName</b></label>
             <asp:TextBox ID="txt_UserName" name="txtApplicationUsersName" runat="server" Visible="True"></asp:TextBox>

             <label for="psw"><b>Password</b></label>
             <asp:TextBox ID="txt_Password" name="txt_Password" runat="server" Visible="True"></asp:TextBox>

             <label for="FirstName"><b>First Name</b></label>
             <asp:TextBox ID="txt_FirstName" name="txt_Average" runat="server" Visible="True"></asp:TextBox>

             <label for="LastName"><b>Last Name</b></label>
             <asp:TextBox ID="txt_LastName" name="txt_Max" runat="server" Visible="True"></asp:TextBox>
            
             <label for="date"><b>Date Of Birth</b></label>
             <input id="dt_DateOfBirth" runat="server" placeholder="Date Of Birth" name="dt_DateOfBirth"/>
             <br />
             <asp:Button ID="Delete_Button" runat="server" OnClick="DeleteButtonAsync" Text="Delete" Width="318px" style="margin-bottom: 0px" />
             <asp:Button ID="Update_Button" runat="server" OnClick="UpdateButtonAsync" Text="Update" Width="318px" style="margin-bottom: 0px" />            
         </asp:Panel>
         <br />
         <br />
         <br />
     <h1>Files</h1>
         <asp:Panel ID="Panel3" runat="server" Height="465px" Width="689px">
         <asp:TextBox ID="txt_Search" name="txt_Search" placeholder="search file name..." runat="server" Visible="True"></asp:TextBox>
         <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_OnClickAsync" Text="Search" Width="318px" style="margin-bottom: 0px" />
         <table id="FilesTable" runat="server" class="auto-style1" style="margin-top: 0px;">
             <thead>
             <tr>
                 <th>File ID</th>
                 <th>File Name</th>
                 <th>Size</th>
                 <th>Type</th>
                 <th>Location</th>
                 <th>Amount Of Peers</th>
             </tr>
             </thead>
         </table>
         </asp:Panel>
         </form>
   

      </body>
</html>
<script>
    var table = document.getElementById('UsersTable');       
    for(var i = 1; i < table.rows.length; i++)
    {
        table.rows[i].onclick = function()
        {
            //rIndex = this.rowIndex;
            document.getElementById("Application_User_ID").value = this.cells[0].innerHTML;
            document.getElementById("txt_UserName").value = this.cells[1].innerHTML;
            document.getElementById("txt_Password").value = this.cells[2].innerHTML;
            document.getElementById("txt_FirstName").value = this.cells[3].innerHTML;
            document.getElementById("txt_LastName").value = this.cells[4].innerHTML;
            document.getElementById("dt_DateOfBirth").value = this.cells[7].innerHTML;
        };
    }
    
</script>
<style>
body {font-family: Arial, Helvetica, sans-serif;}
* {box-sizing: border-box}

/* Full-width input fields */
input[type=text], input[type=password] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
}

input[type=text]:focus, input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

hr {
  border: 1px solid #f1f1f1;
  margin-bottom: 25px;
}

/* Set a style for all buttons */
button {
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

button:hover {
  opacity:1;
}

/* Add padding to container elements */
.container {
  padding: 16px;
}

/* Clear floats */
.clearfix::after {
  content: "";
  clear: both;
  display: table;
}

    .auto-style1 {
        width: 100%;
    }

    </style> 
<style>
#UsersTable #FilesTable {
  font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

/*#Users_TableTable td, #Users_TableTable th #FilesTable td, #FilesTable th {
  border: 1px solid #ddd;
  padding: 8px;*/


#UsersTable tr:nth-child(even){background-color: #f2f2f2;}
#FilesTable tr:nth-child(even){background-color: #f2f2f2;}

#UsersTable tr:hover {background-color: #ddd;}
#FilesTable tr:hover {background-color: #ddd;}

#UsersTable th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #4CAF50;
  color: white;
}
#FilesTable th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #4CAF50;
  color: white;
}
</style>
