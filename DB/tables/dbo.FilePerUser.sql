CREATE TABLE [dbo].[FilePerUser] (
    [Application_User_ID] INT NOT NULL,
    [Files_ID]            INT NOT NULL,
    CONSTRAINT [FK_FilePerUser_Application_User] FOREIGN KEY ([Application_User_ID]) REFERENCES [dbo].[Application_User] ([Application_User_ID]) ON DELETE CASCADE
);

