CREATE PROCEDURE SP_InserFilesIntoTable(
    @User_Name           VARCHAR (50) ,
    @FileName           VARCHAR (50) ,
	@Size			    int )

AS
DECLARE @Files_ID INT = NULL;
SELECT TOP 1 @Files_ID = Files_ID
FROM Files
WHERE FileName = @FileName 

IF(@Files_ID IS NULL) BEGIN
	INSERT INTO Files (FileName,SIZE)
	VALUES(@FileName,@Size)

	SELECT TOP 1 @Files_ID = Files_ID
	FROM Files
	WHERE FileName = @FileName 

	END

DECLARE @Application_User_ID INT;
SELECT TOP 1 @Application_User_ID = Application_User_ID
FROM Application_User
WHERE User_Name = @User_Name 

INSERT INTO FilePerUser(Application_User_ID,Files_ID)
VALUES(@Application_User_ID,@Files_ID)

RETURN 1