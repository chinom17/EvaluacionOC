USE [EvaluacionOC]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 06/07/2018 03:34:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[Id] [tinyint] NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 06/07/2018 03:34:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[NombreUsuario] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[GeneroId] [tinyint] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Genero_GeneroId] FOREIGN KEY([GeneroId])
REFERENCES [dbo].[Genero] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Genero_GeneroId]
GO
/****** Object:  StoredProcedure [dbo].[spCrearUsuario]    Script Date: 06/07/2018 03:34:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SET QUOTED_IDENTIFIER ON|OFF
--SET ANSI_NULLS ON|OFF
--GO
CREATE PROCEDURE [dbo].[spCrearUsuario]
    @usuario AS NVARCHAR(MAX)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
	
    INSERT INTO dbo.Usuario ( Email ,
                              NombreUsuario ,
                              [Status] ,
                              GeneroId ,
                              [Password],
							  FechaCreacion )
                SELECT Email ,
                       NombreUsuario ,
                       Status ,
                       GeneroId ,
                       [Password],
					   GETDATE()
                FROM
                       OPENJSON(@usuario)
                           WITH ( Email NVARCHAR(100) '$.Email',
                                  NombreUsuario NVARCHAR(50) '$.NombreUsuario' ,
                                  [Status] BIT '$.Status',
                                  GeneroId TINYINT '$.GeneroId',
                                  [Password] NVARCHAR(MAX) '$.Password'
								  );

								  DECLARE @idUsuarioReturn INT
								  SELECT @idUsuarioReturn = @@IDENTITY
								  SELECT Id ,
                                         Email ,
                                         NombreUsuario ,
                                         Status ,
                                         GeneroId ,
                                         [Password],
										 FechaCreacion FROM dbo.Usuario WHERE Id = @idUsuarioReturn

GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 06/07/2018 03:34:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SET QUOTED_IDENTIFIER ON|OFF
--SET ANSI_NULLS ON|OFF
--GO
CREATE PROCEDURE [dbo].[spLogin]	
    @usuario AS NVARCHAR(50) ,
    @password AS NVARCHAR(MAX)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
    DECLARE @passE AS NVARCHAR(MAX);

    SELECT @passE = [Password]
    FROM   dbo.Usuario
    WHERE  NombreUsuario = @usuario;

    IF ( @passE = @password )
        BEGIN
            SELECT Id ,
                   Email ,
                   NombreUsuario ,
                   Status ,
                   GeneroId ,
                   '' AS [Password] ,
                   FechaCreacion FROM dbo.Usuario WHERE NombreUsuario = @usuario
        END;
    ELSE
        BEGIN
            SELECT Id ,
                   Email ,
                   NombreUsuario ,
                   Status ,
                   GeneroId ,
                   [Password] ,
                   FechaCreacion FROM dbo.Usuario WHERE id = 0
        END;
GO


INSERT INTO dbo.Genero ( Id ,
                         Descripcion )
VALUES ( 1 , -- Id - tinyint
         N'Masculio' -- Descripcion - nvarchar(max)
    )
GO

	INSERT INTO dbo.Genero ( Id ,
                         Descripcion )
VALUES ( 2 , -- Id - tinyint
         N'Femenino' -- Descripcion - nvarchar(max)
    )
GO