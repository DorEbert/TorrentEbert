CREATE PROCEDURE [dbo].SP_LogOut
	@User_Name VARCHAR(50)
AS
DECLARE @Application_User_ID INT;
SELECT TOP 1 @Application_User_ID = Application_User_ID
FROM Application_User
WHERE User_Name = @User_Name

IF(@Application_User_ID IS NOT NULL)BEGIN
	UPDATE Application_User
	SET Is_Active = 0, IPAdress = '',Port = ''
	WHERE Application_User_ID = @Application_User_ID

	DELETE FROM FilePerUser 
	WHERE Application_User_ID = @Application_User_ID

	RETURN 1
	END
RETURN -1