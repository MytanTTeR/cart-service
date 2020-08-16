CREATE TABLE [dbo].[CartItem] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [CartId]    UNIQUEIDENTIFIER NOT NULL,
    [ProductId] INT              NOT NULL,
    CONSTRAINT [PK_CartItem_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItem_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE
);



