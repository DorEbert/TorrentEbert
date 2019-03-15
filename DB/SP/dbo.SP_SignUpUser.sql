CREATE PROCEDURE SP_SignUpUser
    @User_Name           VARCHAR (50) ,
    @User_Password       VARCHAR (50) ,
    @First_Name        VARCHAR (50) ,
    @Last_Name          VARCHAR (50) ,
    @Date_Of_Birth       DATETIME     
AS
	Insert into Application_User (
		User_Name     
		,User_Password 
		,First_Name    
		,Last_Name     
		,Date_Of_Birth 
		,Is_Active
	)
VALUES(
	@User_Name      
	,@User_Password  
	,@First_Name     
	,@Last_Name      
	,@Date_Of_Birth  
	,0
)
return 1