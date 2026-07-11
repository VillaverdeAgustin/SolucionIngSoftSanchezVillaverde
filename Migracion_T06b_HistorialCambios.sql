-- =============================================================
-- Migracion T06b: Control de Cambios sobre la entidad Usuario
-- (Auditoria con patron Memento)
-- Crea la tabla HistorialCambios (una fila por campo modificado,
-- con valor anterior y posterior, responsable y fecha), sus SPs
-- de registro y consulta, y las traducciones de la pantalla.
-- Ejecutar sobre la base FerreDB, despues de Migracion_T05.
-- =============================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -----------------------------------------------
-- Tabla
-- -----------------------------------------------
IF OBJECT_ID('dbo.HistorialCambios', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[HistorialCambios](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [EntidadId] [int] NOT NULL,
        [NombreCampo] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
        [ValorAnterior] [nvarchar](300) COLLATE Modern_Spanish_CI_AS NULL,
        [ValorNuevo] [nvarchar](300) COLLATE Modern_Spanish_CI_AS NULL,
        [Usuario] [nvarchar](50) COLLATE Modern_Spanish_CI_AS NOT NULL,
        [Fecha] [datetime] NOT NULL,
     CONSTRAINT [PK_HistorialCambios] PRIMARY KEY CLUSTERED ([Id] ASC),
     CONSTRAINT [FK_HistorialCambios_Usuarios] FOREIGN KEY([EntidadId]) REFERENCES [dbo].[Usuarios] ([Codigo])
    ) ON [PRIMARY]
END
GO

-- -----------------------------------------------
-- Stored Procedures
-- -----------------------------------------------
IF OBJECT_ID('dbo.SP_RegistrarCambio', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SP_RegistrarCambio]
GO
CREATE PROCEDURE [dbo].[SP_RegistrarCambio]
    @EntidadId INT,
    @NombreCampo NVARCHAR(50),
    @ValorAnterior NVARCHAR(300),
    @ValorNuevo NVARCHAR(300),
    @Usuario NVARCHAR(50),
    @Fecha DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HistorialCambios (EntidadId, NombreCampo, ValorAnterior, ValorNuevo, Usuario, Fecha)
    VALUES (@EntidadId, @NombreCampo, @ValorAnterior, @ValorNuevo, @Usuario, @Fecha);
END
GO

IF OBJECT_ID('dbo.SP_ExtHistorialCambios', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SP_ExtHistorialCambios]
GO
CREATE PROCEDURE [dbo].[SP_ExtHistorialCambios]
    @EntidadId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, EntidadId, NombreCampo, ValorAnterior, ValorNuevo, Usuario, Fecha
    FROM HistorialCambios
    WHERE EntidadId = @EntidadId
    ORDER BY Fecha DESC, Id DESC
END
GO

-- -----------------------------------------------
-- Traducciones de la pantalla Historial de Cambios
-- -----------------------------------------------
DECLARE @ES INT = (SELECT Id FROM Idioma WHERE Codigo = 'ES');
DECLARE @EN INT = (SELECT Id FROM Idioma WHERE Codigo = 'EN');

DELETE FROM Traduccion WHERE Clave LIKE N'HIST[_]%' OR Clave = N'USU_BTN_HISTORIAL';

INSERT INTO Traduccion (IdIdioma, Clave, Texto) VALUES
(@ES, N'USU_BTN_HISTORIAL',   N'Ver Historial'),
(@EN, N'USU_BTN_HISTORIAL',   N'View History'),
(@ES, N'HIST_TITULO',         N'Historial de Cambios'),
(@EN, N'HIST_TITULO',         N'Change History'),
(@ES, N'HIST_LBL_TITULO',     N'HISTORIAL DE CAMBIOS'),
(@EN, N'HIST_LBL_TITULO',     N'CHANGE HISTORY'),
(@ES, N'HIST_LBL_USUARIO',    N'Cambios del usuario: '),
(@EN, N'HIST_LBL_USUARIO',    N'Changes for user: '),
(@ES, N'HIST_GB_DETALLE',     N'Detalle del Cambio'),
(@EN, N'HIST_GB_DETALLE',     N'Change Detail'),
(@ES, N'HIST_COL_CAMPO',      N'Campo'),
(@EN, N'HIST_COL_CAMPO',      N'Field'),
(@ES, N'HIST_COL_ANTERIOR',   N'Valor Anterior'),
(@EN, N'HIST_COL_ANTERIOR',   N'Previous Value'),
(@ES, N'HIST_COL_NUEVO',      N'Valor Nuevo'),
(@EN, N'HIST_COL_NUEVO',      N'New Value'),
(@ES, N'HIST_COL_RESPONSABLE',N'Modificado Por'),
(@EN, N'HIST_COL_RESPONSABLE',N'Modified By'),
(@ES, N'HIST_DETALLE_FMT',    N'El {0}, el usuario {1} modifico el campo [{2}]: de "{3}" a "{4}".'),
(@EN, N'HIST_DETALLE_FMT',    N'On {0}, user {1} modified the field [{2}]: from "{3}" to "{4}".'),
(@ES, N'HIST_SIN_CAMBIOS',    N'El usuario seleccionado no registra cambios.'),
(@EN, N'HIST_SIN_CAMBIOS',    N'The selected user has no recorded changes.');
GO
