IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Barbers] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(50) NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Barbers] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Services] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(50) NOT NULL,
    [Price] nvarchar(100) NOT NULL,
    [barberId] int NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Services_Barbers_barberId] FOREIGN KEY ([barberId]) REFERENCES [Barbers] ([id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Services_barberId] ON [Services] ([barberId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240527094147_First', N'8.0.6');
GO

COMMIT;
GO

