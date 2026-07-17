-- Script de creacion de FerreDB (TP SanchezVillaverde)
-- Crea la base si no existe y fuerza el contexto con USE,
-- para que las tablas NUNCA se creen en otra base por accidente.
IF DB_ID(N'FerreDB') IS NULL
BEGIN
    CREATE DATABASE [FerreDB];
END
GO
USE [FerreDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[Nombre_Permiso] [nvarchar](100) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Tipo] [nvarchar](20) COLLATE Modern_Spanish_CI_AS NULL,
	[Rol] [bit] NULL,
	[Nombre_PermisoPadre] [nvarchar](100) COLLATE Modern_Spanish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Nombre_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[ID_Venta] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Cliente] [int] NOT NULL,
	[Monto_Total] [decimal](18, 2) NOT NULL,
	[Metodo_Pago] [nchar](10) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Fecha_Registro] [nchar](10) COLLATE Modern_Spanish_CI_AS NOT NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[ID_Venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitoVertical](
	[Tabla] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[DVV] [nvarchar](65) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[DNI] [int] NOT NULL,
	[Nombre] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Apellido] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Telefono] [int] NULL,
	[Direccion] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[FechaActualizacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carritos](
	[ID_Carrito] [int] NOT NULL,
	[ID_Venta] [int] NOT NULL,
	[ID_Producto] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[Registro] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Accion] [int] NOT NULL,
	[Fecha] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proovedores](
	[IDProovedor] [int] NOT NULL,
	[Nombre] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Direccion] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Observaciones] [nvarchar](max) COLLATE Modern_Spanish_CI_AS NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Proovedores] PRIMARY KEY CLUSTERED 
(
	[IDProovedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IDProducto] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Proovedor] [int] NOT NULL,
	[Nombre] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Descripcion] [nvarchar](max) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Stock] [int] NOT NULL,
	[Fecha_Actualizacion] [datetime] NOT NULL,
	[PrecioCompra] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IDProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [int] NOT NULL,
	[Nombre] [varchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Apellido] [varchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Usuario] [varchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[Rol] [nvarchar](100) COLLATE Modern_Spanish_CI_AS NULL,
	[Contraseña] [varchar](65) COLLATE Modern_Spanish_CI_AS NULL,
	[Direccion] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Telefono] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Email] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[Activo] [bit] NOT NULL,
	[Bloqueado] [bit] NOT NULL,
	[DVH] [nvarchar](65) COLLATE Modern_Spanish_CI_AS NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_PermisoPadre] FOREIGN KEY([Nombre_PermisoPadre])
REFERENCES [dbo].[Permiso] ([Nombre_Permiso])
GO
ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [FK_Permiso_PermisoPadre]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Proovedores] FOREIGN KEY([Proovedor])
REFERENCES [dbo].[Proovedores] ([IDProovedor])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Proovedores]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Permiso] FOREIGN KEY([Rol])
REFERENCES [dbo].[Permiso] ([Nombre_Permiso])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Permiso]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ExtTabla]
    @tabla NVARCHAR(50)
AS
BEGIN
    DECLARE @Query NVARCHAR(MAX);
    SET @Query = 'SELECT * FROM ' + @tabla

    EXEC sp_executesql @Query;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_ExtraerDVV
    @tabla NVARCHAR(50),
    @tablaVerif NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Query NVARCHAR(MAX);

    SET @Query = N'SELECT DVV 
                   FROM ' + QUOTENAME(@tabla) + 
                   N' WHERE tabla = @tablaVerif';

    EXEC sp_executesql @Query, N'@tablaVerif NVARCHAR(50)', @tablaVerif;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ExtraerDVH]
@tabla nvarchar(50)
as
begin
    DECLARE @Query NVARCHAR(MAX);
    SET @Query = 'SELECT DVH FROM ' + @tabla + ' order by Codigo'

    EXEC sp_executesql @Query;
end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ActualizarDVV]
@tabla nvarchar(50),
@dvv nvarchar(65)
as
begin
IF EXISTS (SELECT 1
           FROM DigitoVertical
           WHERE Tabla = @Tabla)
begin
    update DigitoVertical
    set DVV = @dvv
    where Tabla = @tabla
end
else
begin
    insert into DigitoVertical(Tabla,DVV)
    values(@tabla,@dvv)
end
end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ExtBitacora]
as
begin
select * from Bitacora
order by Fecha desc
end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_RegistrarEvento]
@usuario nvarchar(50),
@accion int,
@fecha datetime
as
begin
insert into Bitacora
(Usuario,Accion,Fecha)
values
(@usuario,@accion,@fecha);
end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_BuscarBitacora]
    @Usuario VARCHAR(50) = NULL,
    @Accion INT = NULL,
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL
AS
BEGIN
    SELECT B.Registro, B.Usuario, B.Accion, B.Fecha
    FROM Bitacora B
    WHERE (@Usuario IS NULL OR B.Usuario LIKE '%' + @Usuario + '%')
      AND (@Accion IS NULL OR B.Accion = @Accion)
      AND (@FechaDesde IS NULL OR B.Fecha >= @FechaDesde)
      AND (@FechaHasta IS NULL OR B.Fecha <= @FechaHasta)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ActualizarUs]
(
    @Codigo INT,
    @DNI NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Usuario NVARCHAR(50),
    @Rol NVARCHAR(50),
    @Direccion NVARCHAR(200),
    @Telefono NVARCHAR(15),
    @Email NVARCHAR(100),
    @Activo BIT,
    @Bloqueado BIT,
    @DVH NVARCHAR(65)
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN
		SELECT 1 FROM Usuarios WHERE Codigo = @Codigo
        BEGIN
            -- Actualizar los campos del usuario
            UPDATE Usuarios
            SET 
                DNI = @DNI,
                Nombre = @Nombre,
                Apellido = @Apellido,
                Usuario = @Usuario,
                Rol = @Rol,
                Direccion = @Direccion,
                Telefono = @Telefono,
                Email = @Email,
                Activo = @Activo,
                Bloqueado = @Bloqueado,
                DVH = @DVH
            WHERE Codigo = @Codigo;
        END
    END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Login]
(
	@user NVARCHAR(50),
	@pass NVARCHAR(65),
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Block INT;
	DECLARE @Active INT;
	--Verifico si existe el usuario en la tabla
	IF EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = @user)
	BEGIN
		--Verifico si el usuario se encuentra activo
		SELECT @Active = Activo FROM Usuarios WHERE  Usuario = @user;
		IF @Active = 0
		BEGIN 
			SET @Result = 4 --Indica usuario inactivo
		END
		ELSE
		BEGIN
			--Verifico si el usuario se encuentra bloqueado
			SELECT @Block = Bloqueado FROM Usuarios WHERE  Usuario = @user;
			IF @Block = 1
			BEGIN 
				SET @Result = 2 --Indica usuario bloqueado
			END
			ELSE
			BEGIN
				IF EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = @user AND Contraseña = @pass)
				BEGIN
					SET @Result = 1; --Indica Login correcto
				END
				ELSE
				BEGIN
					SET @Result = 3; --Indica contraseña incorrecta
				END
			END
		END
	END
	ELSE
	BEGIN
		SET @Result = 0 --Indica usuario no encontrado
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ExtUser]
    @user NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE Usuario = @user;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ElimUsuario]
(
    @Usuario VARCHAR(50),
    @dvh NVARCHAR(65)
)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 1 FROM Usuarios WHERE Usuario = @Usuario
    BEGIN
		-- Actualizar el campo Activo del usuario
		UPDATE Usuarios
        SET Activo = 0, DVH = @dvh
        WHERE Usuario = @Usuario;
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CrearUsuario]
(
    @DNI INT,
    @Nombre VARCHAR(50),
    @Apellido NVARCHAR(50),
    @Usuario NVARCHAR(50),
    @Rol NVARCHAR(50),
    @Contraseña NVARCHAR(65),
    @Direccion NVARCHAR(50),
    @Telefono NVARCHAR(50),
    @Email NVARCHAR(50),
    @Activo BIT,
    @Bloqueado BIT,
    @DVH NVARCHAR(65)
)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Usuarios (DNI, Nombre, Apellido, Usuario, Rol, Contraseña, Direccion, Telefono, Email, Activo, Bloqueado, DVH)
	VALUES (@DNI, @Nombre, @Apellido, @Usuario, @Rol, @Contraseña, @Direccion, @Telefono, @Email, @Activo, @Bloqueado,@DVH);
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CambiarPass]
(
    @Usuario VARCHAR(50),
    @pass VARCHAR(65),
    @DVH NVARCHAR(65)
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN 
		UPDATE Usuarios
        SET Contraseña = @pass, DVH = @DVH
        WHERE Usuario = @Usuario;
    END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ActualizarBloqueado]
    @Usuario varchar(50),
	@pass varchar(65),
	@Bloqueado bit,
	@DVH NVARCHAR(65)
AS
begin
	UPDATE Usuarios
	SET Bloqueado = @Bloqueado, DVH = @DVH,
	Contraseña = @pass
	WHERE Usuario = @Usuario;
end
--BEGIN
--	IF (@pass = '') -- Bloqueo Login
--	BEGIN
--		UPDATE Usuarios
--		SET Bloqueado = @Bloqueado, DVH = @DVH
--		WHERE Usuario = @Usuario;
--	END
--	ELSE -- Desbloqueo
--	BEGIN
--		UPDATE Usuarios
--		SET Bloqueado = @Bloqueado, DVH = @DVH,
--		Contraseña = @pass
--		WHERE Usuario = @Usuario;
--	END
--END

insert into Usuarios (DNI,Nombre,Apellido,Usuario,Contraseña,Rol,Activo,Bloqueado,DVH)
values(32382883,'Carlos','Sanchez','sanchezcarlos','ef994e7262a78b97c039adf58214ee7df1076824a7e47538948ba61ae02b05c7','Admin',1,0,'asd123asd') 

insert into Permiso(Nombre_Permiso,Tipo,Rol,Nombre_PermisoPadre)
values('Admin','Admin',1,'Vendedor')

GO
