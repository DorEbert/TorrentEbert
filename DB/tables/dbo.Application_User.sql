CREATE TABLE [dbo].[Application_User] (
    [Application_User_ID] INT          IDENTITY (1, 1) NOT NULL,
    [User_Name]           VARCHAR (50) NULL,
    [User_Password]       VARCHAR (50) NULL,
    [First_Name]          VARCHAR (50) NULL,
    [Last_Name]           VARCHAR (50) NULL,
    [Date_Of_Birth]       DATETIME     NULL,
    [Is_Active]           BIT          NULL,
    [IPAdress]            VARCHAR (50) NULL,
    [Port]                VARCHAR (50) NULL,
    UNIQUE NONCLUSTERED ([User_Name] ASC),
    CONSTRAINT [PK_Application_User] PRIMARY KEY CLUSTERED ([Application_User_ID] ASC)
);

