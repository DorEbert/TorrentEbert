CREATE TABLE [dbo].[Files] (
    [Files_ID] INT          IDENTITY (1000, 1) NOT NULL,
    [FileName] VARCHAR (50) NULL,
    [Size]     INT          NULL,
    [Type]     VARCHAR (50) NULL,
    [Location] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Files_ID] ASC)
);

