CREATE PROCEDURE [dbo].SP_GetIpAdressesPerFileID
	@FileName varchar(50)
	
AS
	SET NOCOUNT ON;  
	SELECT A.IPAdress FROM FilePerUser AS FPU
	INNER JOIN Application_User AS A
	ON FPU.Application_User_ID = A.Application_User_ID
	INNER JOIN Files AS F
	ON F.Files_ID = FPU.Files_ID
	WHERE F.FileName = @FileName AND A.IPAdress IS NOT NULL
RETURN