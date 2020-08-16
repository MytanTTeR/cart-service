CREATE TABLE [dbo].[Product] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (255)  NOT NULL,
    [Cost]           DECIMAL (18, 2) NOT NULL,
    [ForBonusPoints] BIT             NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

