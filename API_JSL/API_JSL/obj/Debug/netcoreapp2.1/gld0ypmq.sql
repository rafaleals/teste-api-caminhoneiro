IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Caminhao] (
    [IdCaminhao] int NOT NULL IDENTITY,
    [Marca] nvarchar(max) NULL,
    [Modelo] nvarchar(max) NULL,
    [Placa] nvarchar(max) NULL,
    [Eixo] int NOT NULL,
    CONSTRAINT [PK_Caminhao] PRIMARY KEY ([IdCaminhao])
);

GO

CREATE TABLE [Endereco] (
    [IdEndereco] int NOT NULL IDENTITY,
    [CEP] nvarchar(max) NULL,
    [Logradouro] nvarchar(max) NULL,
    [Numero] int NOT NULL,
    [Bairro] nvarchar(max) NULL,
    [Cidade] nvarchar(max) NULL,
    [UF] nvarchar(max) NULL,
    [Coordenadas] nvarchar(max) NULL,
    CONSTRAINT [PK_Endereco] PRIMARY KEY ([IdEndereco])
);

GO

CREATE TABLE [Motorista] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Sobrenome] nvarchar(max) NULL,
    [IdCaminhao] int NOT NULL,
    [IdEndereco] int NOT NULL,
    CONSTRAINT [PK_Motorista] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Motorista_Caminhao_IdCaminhao] FOREIGN KEY ([IdCaminhao]) REFERENCES [Caminhao] ([IdCaminhao]) ON DELETE CASCADE,
    CONSTRAINT [FK_Motorista_Endereco_IdEndereco] FOREIGN KEY ([IdEndereco]) REFERENCES [Endereco] ([IdEndereco]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Motorista_IdCaminhao] ON [Motorista] ([IdCaminhao]);

GO

CREATE INDEX [IX_Motorista_IdEndereco] ON [Motorista] ([IdEndereco]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201114234200_initialCreate', N'2.1.14-servicing-32113');

GO

