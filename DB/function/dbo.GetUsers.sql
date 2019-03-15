CREATE FUNCTION [dbo].GetUsers
(
)
RETURNS @returntable TABLE
(
	 Application_User_ID INT,
	 User_Name          VARCHAR (50),
	 User_Password      VARCHAR (50),
	 First_Name			VARCHAR (50) ,
	 Last_Name          VARCHAR (50) ,
	 Date_Of_Birth      datetime,
	 Is_Active			bit
)
AS
BEGIN
	INSERT @returntable( Application_User_ID, User_Name, User_Password, First_Name ,Last_Name ,Date_Of_Birth,Is_Active)
	SELECT Application_User_ID, User_Name, User_Password, First_Name ,Last_Name ,Date_Of_Birth,Is_Active  from Application_User
	RETURN
END