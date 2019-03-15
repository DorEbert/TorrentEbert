
CREATE VIEW [dbo].FileView
	AS SELECT FE.Files_ID,FE.FileName,FE.Size,FE.Type,FE.Location,FPU.Amount_Of_Peers 
FROM Files as FE
LEFT JOIN (SELECT Files_ID, COUNT(FPU.Application_User_ID) AS Amount_Of_Peers
           FROM FilePerUser AS FPU
			GROUP BY FPU.Files_ID  ) AS FPU
	ON FPU.Files_ID = FE.Files_ID