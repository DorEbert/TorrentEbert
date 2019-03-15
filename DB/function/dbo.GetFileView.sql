CREATE FUNCTION [dbo].GetFileView
(
)
RETURNS @returntable TABLE
(
	 [Files_ID] INT          ,
	 [FileName] VARCHAR (50) ,
	 [Size]     INT          ,
	 [Type]     VARCHAR (50) ,
	 [Location] VARCHAR (50) ,
	 Amount_Of_Peers      int
)
AS
BEGIN
	INSERT @returntable( [Files_ID], [FileName], [Size], [Type] ,[Location] ,Amount_Of_Peers )
	SELECT [Files_ID], [FileName], [Size], [Type] ,[Location] ,Amount_Of_Peers from FileView
	RETURN
END