
CREATE PROCEDURE SP_UserInfo
AS    
 
   SET NOCOUNT ON;  
  SELECT  Application_User_ID, User_Name, User_Password, First_Name ,Last_Name ,Date_Of_Birth,Is_Active from GetUsers()
RETURN