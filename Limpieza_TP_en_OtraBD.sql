-- LIMPIEZA: elimina los objetos del TP FerreSV que se crearon por error
-- en otra base de datos.
--
-- COMO USARLO:
--   1. En SSMS, conectate y selecciona en el desplegable la base del OTRO
--      proyecto (la que quedo con las tablas del TP metidas adentro).
--   2. Ejecuta primero el bloque de VERIFICACION y revisa la lista.
--   3. Si la lista muestra solo objetos del TP, ejecuta el resto.
--
-- Todos los DROP estan protegidos con IF OBJECT_ID: si el objeto no existe
-- en esa base, no hace nada. NO toca ningun objeto propio del otro proyecto.

-- ================== VERIFICACION (ejecutar primero) ==================
-- OJO: nombres como Usuarios/Clientes/Productos son genericos. Revisa la
-- columna create_date: los objetos que metio el TP tienen la fecha/hora de
-- cuando ejecutaste el script por error. Si un objeto es anterior, ES DEL
-- OTRO PROYECTO: borra su linea DROP antes de ejecutar la limpieza.
SELECT name AS ObjetoDelTP_Encontrado, type_desc, create_date
FROM sys.objects
WHERE name IN (
    'Permiso','Facturas','DigitoVertical','Clientes','Carritos','Bitacora',
    'Proovedores','Productos','Usuarios',
    'SP_ExtTabla','SP_ExtraerDVV','SP_ExtraerDVH','SP_ActualizarDVV',
    'SP_ExtBitacora','SP_RegistrarEvento','SP_BuscarBitacora','SP_ActualizarUs',
    'SP_Login','SP_ExtUser','SP_ElimUsuario','SP_CrearUsuario','SP_CambiarPass',
    'SP_ActualizarBloqueado'
)
ORDER BY type_desc, name;
GO

-- ================== LIMPIEZA (ejecutar despues de verificar) ==================
-- Stored procedures
IF OBJECT_ID('dbo.SP_ExtTabla','P') IS NOT NULL DROP PROCEDURE dbo.SP_ExtTabla;
IF OBJECT_ID('dbo.SP_ExtraerDVV','P') IS NOT NULL DROP PROCEDURE dbo.SP_ExtraerDVV;
IF OBJECT_ID('dbo.SP_ExtraerDVH','P') IS NOT NULL DROP PROCEDURE dbo.SP_ExtraerDVH;
IF OBJECT_ID('dbo.SP_ActualizarDVV','P') IS NOT NULL DROP PROCEDURE dbo.SP_ActualizarDVV;
IF OBJECT_ID('dbo.SP_ExtBitacora','P') IS NOT NULL DROP PROCEDURE dbo.SP_ExtBitacora;
IF OBJECT_ID('dbo.SP_RegistrarEvento','P') IS NOT NULL DROP PROCEDURE dbo.SP_RegistrarEvento;
IF OBJECT_ID('dbo.SP_BuscarBitacora','P') IS NOT NULL DROP PROCEDURE dbo.SP_BuscarBitacora;
IF OBJECT_ID('dbo.SP_ActualizarUs','P') IS NOT NULL DROP PROCEDURE dbo.SP_ActualizarUs;
IF OBJECT_ID('dbo.SP_Login','P') IS NOT NULL DROP PROCEDURE dbo.SP_Login;
IF OBJECT_ID('dbo.SP_ExtUser','P') IS NOT NULL DROP PROCEDURE dbo.SP_ExtUser;
IF OBJECT_ID('dbo.SP_ElimUsuario','P') IS NOT NULL DROP PROCEDURE dbo.SP_ElimUsuario;
IF OBJECT_ID('dbo.SP_CrearUsuario','P') IS NOT NULL DROP PROCEDURE dbo.SP_CrearUsuario;
IF OBJECT_ID('dbo.SP_CambiarPass','P') IS NOT NULL DROP PROCEDURE dbo.SP_CambiarPass;
IF OBJECT_ID('dbo.SP_ActualizarBloqueado','P') IS NOT NULL DROP PROCEDURE dbo.SP_ActualizarBloqueado;
GO

-- Tablas (en orden inverso a las FKs: primero las que referencian)
IF OBJECT_ID('dbo.Usuarios','U') IS NOT NULL DROP TABLE dbo.Usuarios;      -- FK a Permiso
IF OBJECT_ID('dbo.Productos','U') IS NOT NULL DROP TABLE dbo.Productos;    -- FK a Proovedores
IF OBJECT_ID('dbo.Permiso','U') IS NOT NULL DROP TABLE dbo.Permiso;
IF OBJECT_ID('dbo.Proovedores','U') IS NOT NULL DROP TABLE dbo.Proovedores;
IF OBJECT_ID('dbo.Facturas','U') IS NOT NULL DROP TABLE dbo.Facturas;
IF OBJECT_ID('dbo.DigitoVertical','U') IS NOT NULL DROP TABLE dbo.DigitoVertical;
IF OBJECT_ID('dbo.Clientes','U') IS NOT NULL DROP TABLE dbo.Clientes;
IF OBJECT_ID('dbo.Carritos','U') IS NOT NULL DROP TABLE dbo.Carritos;
IF OBJECT_ID('dbo.Bitacora','U') IS NOT NULL DROP TABLE dbo.Bitacora;
GO
