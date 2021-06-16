CREATE DATABASE "CRUDdotNetDB";
go
USE "CRUDdotNetDB";

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Fornecedores] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] VARCHAR(200) NOT NULL,
    [CNPJ] VARCHAR(14) NOT NULL,
    [Endereco] VARCHAR(200) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Usuarios] (
    [Id] uniqueidentifier NOT NULL,
    [Login] VARCHAR(50) NOT NULL,
    [Senha] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] VARCHAR(200) NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [Quantidade] int NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Produtos_FornecedorId] ON [Produtos] ([FornecedorId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210614103549_Initial', N'3.1.14');

GO

