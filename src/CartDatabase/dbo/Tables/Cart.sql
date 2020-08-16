CREATE TABLE [dbo].[Cart] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([Id] ASC)
);

