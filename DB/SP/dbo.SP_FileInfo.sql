
CREATE PROCEDURE SP_FileInfo
 @search_parameter varchar(50)
AS    
 
   SET NOCOUNT ON;  
   IF(@search_parameter IS  NULL) BEGIN
	 SELECT [Files_ID],[FileName],[Size],[Type],ISNULL(Amount_Of_Peers,0) AS Amount_Of_Peers  from GetFileView()
  END ELSE BEGIN
	  SELECT [Files_ID],[FileName],[Size],[Type],ISNULL(Amount_Of_Peers,0) AS Amount_Of_Peers from GetFileView()
	  WHERE FileName like '%' + @search_parameter +'%'
  END
RETURN