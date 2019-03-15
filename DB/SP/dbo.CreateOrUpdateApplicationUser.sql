CREATE PROCEDURE [dbo].CreateOrUpdateApplicationUser
	@Is_To_Update         bit,
/*@Is_To_Update = 1   = > update
  @Is_To_Update = 0   = > delete */
	@Application_User_ID  int,
	@User_Name           VARCHAR (50) = ' ',
    @User_Password       VARCHAR (50) = ' ' ,
    @First_Name        VARCHAR (50) = ' ',
    @Last_Name          VARCHAR (50) = ' ',
    @Date_Of_Birth       DATETIME  = null   
AS
	IF(@Is_To_Update = '1') BEGIN
		UPDATE Application_User
		SET User_Name = @User_Name,
			User_Password = @User_Password,
			First_Name = @First_Name,
			Last_Name = @Last_Name,
			Date_Of_Birth = @Date_Of_Birth
		WHERE Application_User_ID = @Application_User_ID
		RETURN 1
	END 
	ELSE IF(@Is_To_Update = '0') BEGIN
			DELETE FROM Application_User
			WHERE Application_User_ID = @Application_User_ID
			DELETE FROM FilePerUser 
			WHERE Application_User_ID = @Application_User_ID
			RETURN 1;
		END
RETURN -1