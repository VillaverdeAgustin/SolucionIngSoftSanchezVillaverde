-- =============================================================
-- Migracion T05b: Alta de Idiomas desde la aplicacion (frmIdiomas)
-- Agrega los SPs de escritura del modulo de idiomas y las claves
-- de traduccion del nuevo formulario de gestion.
-- Requiere haber ejecutado antes Migracion_T05_Idiomas.sql.
-- Ejecutar sobre la base FerreDB.
-- =============================================================

USE [FerreDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -----------------------------------------------
-- SP: crear idioma copiando las traducciones del idioma base.
-- El idioma nuevo nace funcional (con los textos del base como
-- punto de partida) y luego se editan desde la grilla.
-- -----------------------------------------------
IF OBJECT_ID('dbo.SP_CrearIdioma', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SP_CrearIdioma]
GO
CREATE PROCEDURE [dbo].[SP_CrearIdioma]
    @Codigo NVARCHAR(5),
    @Nombre NVARCHAR(50),
    @CodigoBase NVARCHAR(5)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdBase INT = (SELECT Id FROM Idioma WHERE Codigo = @CodigoBase);
    DECLARE @IdNuevo INT;

    INSERT INTO Idioma (Codigo, Nombre) VALUES (@Codigo, @Nombre);
    SET @IdNuevo = SCOPE_IDENTITY();

    INSERT INTO Traduccion (IdIdioma, Clave, Texto)
    SELECT @IdNuevo, Clave, Texto
    FROM Traduccion
    WHERE IdIdioma = @IdBase;
END
GO

-- -----------------------------------------------
-- SP: actualizar el texto de una traduccion
-- -----------------------------------------------
IF OBJECT_ID('dbo.SP_ActualizarTraduccion', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SP_ActualizarTraduccion]
GO
CREATE PROCEDURE [dbo].[SP_ActualizarTraduccion]
    @IdIdioma INT,
    @Clave NVARCHAR(100),
    @Texto NVARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Traduccion
    SET Texto = @Texto
    WHERE IdIdioma = @IdIdioma AND Clave = @Clave;
END
GO

-- -----------------------------------------------
-- Claves de traduccion del formulario de gestion de idiomas.
-- (Si ya se crearon otros idiomas copiados, estas claves nuevas
-- apareceran con la clave literal hasta que se traduzcan alli.)
-- -----------------------------------------------
DECLARE @ES INT = (SELECT Id FROM Idioma WHERE Codigo = 'ES');
DECLARE @EN INT = (SELECT Id FROM Idioma WHERE Codigo = 'EN');

DELETE FROM Traduccion WHERE Clave LIKE 'IDI_%' AND IdIdioma IN (@ES, @EN);

INSERT INTO Traduccion (IdIdioma, Clave, Texto) VALUES
(@ES, N'IDI_TITULO',            N'Gestión de Idiomas'),
(@EN, N'IDI_TITULO',            N'Language Management'),
(@ES, N'IDI_GB_NUEVO',          N'Nuevo Idioma'),
(@EN, N'IDI_GB_NUEVO',          N'New Language'),
(@ES, N'IDI_LBL_CODIGO',        N'Código'),
(@EN, N'IDI_LBL_CODIGO',        N'Code'),
(@ES, N'IDI_LBL_BASE',          N'Al crear un idioma se copian las traducciones del idioma base (Español) como punto de partida.'),
(@EN, N'IDI_LBL_BASE',          N'When creating a language, translations are copied from the base language (Spanish) as a starting point.'),
(@ES, N'IDI_BTN_CREAR',         N'Crear Idioma'),
(@EN, N'IDI_BTN_CREAR',         N'Create Language'),
(@ES, N'IDI_GB_TRADUCCIONES',   N'Traducciones'),
(@EN, N'IDI_GB_TRADUCCIONES',   N'Translations'),
(@ES, N'IDI_LBL_IDIOMA',        N'Idioma:'),
(@EN, N'IDI_LBL_IDIOMA',        N'Language:'),
(@ES, N'IDI_COL_CLAVE',         N'Clave'),
(@EN, N'IDI_COL_CLAVE',         N'Key'),
(@ES, N'IDI_COL_TEXTO',         N'Texto'),
(@EN, N'IDI_COL_TEXTO',         N'Text'),
(@ES, N'IDI_MSG_CREADO',        N'El idioma -- {0} -- fue creado exitosamente'),
(@EN, N'IDI_MSG_CREADO',        N'Language -- {0} -- was created successfully'),
(@ES, N'IDI_MSG_GUARDADO',      N'Traducciones guardadas correctamente'),
(@EN, N'IDI_MSG_GUARDADO',      N'Translations saved successfully'),
(@ES, N'IDI_ERR_DATOS',         N'Debe completar el código y el nombre del idioma'),
(@EN, N'IDI_ERR_DATOS',         N'You must enter the language code and name'),
(@ES, N'IDI_ERR_LONGITUD',      N'El código admite hasta 5 caracteres y el nombre hasta 50'),
(@EN, N'IDI_ERR_LONGITUD',      N'The code allows up to 5 characters and the name up to 50'),
(@ES, N'IDI_ERR_CODIGO_EXISTE', N'Ya existe un idioma con ese código'),
(@EN, N'IDI_ERR_CODIGO_EXISTE', N'A language with that code already exists');
GO
