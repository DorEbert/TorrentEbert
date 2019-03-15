CREATE PROCEDURE SP_LoginUser(
    @User_Name           VARCHAR (50) ,
    @User_Password       VARCHAR (50) ,
	@IPAdress			 VARCHAR (50) ,
	@Port	     		 VARCHAR (50))
AS

DECLARE @Application_User_ID INT;
SELECT TOP 1 @Application_User_ID = Application_User_ID
FROM Application_User
WHERE User_Name = @User_Name and User_Password = @User_Password and Is_Active=0

IF(@Application_User_ID IS NOT NULL)BEGIN
	UPDATE Application_User
	SET Is_Active = 1, IPAdress = @IPAdress,Port = @Port
	WHERE Application_User_ID = @Application_User_ID

	RETURN 1
	END
 
RETURN -1