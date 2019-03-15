<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="SignUp.aspx.cs" Inherits="WebPortal.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Sign Up</h1>
            <p>Please fill in this form to create an account.</p>
            <hr>

            <label for="UserName"><b>UserName</b></label>
            <input id="txt_UserName" type="text" runat="server" placeholder="Enter UserName" name="txt_UserName" required>

            <label for="psw"><b>Password</b></label>
            <input id="txt_Password" type="password" runat="server" placeholder="Enter Password" name="psw" required>

            <label for="psw-repeat"><b>Repeat Password</b></label>
            <input id="txt_pswrepeat" type="password" runat="server" placeholder="Repeat Password" name="pswrepeat" required>
            <label for="FirstName"><b>First Name</b></label>
            <input id="txt_FirstName" type="text" runat="server" placeholder="First Name" name="txt_FirstName" required>
            
            <label for="LastName"><b>Last Name</b></label>
            <input id="txt_LastName" type="text" runat="server" placeholder="Last Name" name="txt_LastName" required>
            
            <label for="date"><b>Date Of Birth</b></label>
            <input id="dt_DateOfBirth" runat="server" type="datetime-local" placeholder="Date Of Birth" name="DateOfBirth" required><br />
            <br />
            <asp:Button ID="SignUp_Button" runat="server" OnClick="SignUp_ClickAsync" Text="Sign Up" Width="318px" style="margin-bottom: 0px" />
       
            </div>
    </form>
</body>
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

</style>
</html>
