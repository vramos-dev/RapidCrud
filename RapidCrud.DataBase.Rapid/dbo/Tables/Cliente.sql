CREATE TABLE [dbo].[Cliente] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Dni]      VARCHAR (8)   NOT NULL,
    [Nombre]   VARCHAR (100) NOT NULL,
    [Apellido] VARCHAR (100) NOT NULL,
    [Telefono] VARCHAR (9)   NOT NULL,
    [Email]    VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_Dni]
    ON [dbo].[Cliente]([Dni] ASC);

