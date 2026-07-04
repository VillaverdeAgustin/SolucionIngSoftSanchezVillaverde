USE [FerreDB]
GO

-- 1. Agregar la columna reflexiva que reemplaza a la tabla puente PermisoPermisos
ALTER TABLE [dbo].[Permiso] ADD [Nombre_PermisoPadre] NVARCHAR(100) NULL
GO

ALTER TABLE [dbo].[Permiso] WITH CHECK ADD CONSTRAINT [FK_Permiso_PermisoPadre]
    FOREIGN KEY ([Nombre_PermisoPadre]) REFERENCES [dbo].[Permiso] ([Nombre_Permiso])
GO

ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [FK_Permiso_PermisoPadre]
GO

-- 2. Migrar las relaciones existentes de PermisoPermisos a la nueva columna.
--    Conflicto resuelto: GestionUsuarios tenia dos padres (Admin y Vendedor);
--    se conserva bajo Admin unicamente.
UPDATE [dbo].[Permiso] SET [Nombre_PermisoPadre] = 'Admin'
    WHERE [Nombre_Permiso] IN ('GestionPerfiles', 'GestionUsuarios')
GO

UPDATE [dbo].[Permiso] SET [Nombre_PermisoPadre] = 'Cajero'
    WHERE [Nombre_Permiso] IN ('GenerarFactura', 'GenerarReserva', 'RealizarCobro', 'RegistrarCliente')
GO

UPDATE [dbo].[Permiso] SET [Nombre_PermisoPadre] = 'Vendedor'
    WHERE [Nombre_Permiso] IN ('LlenarCarrito', 'SeleccionarProducto')
GO

-- 3. Eliminar la tabla puente, ya reemplazada por la FK reflexiva
DROP TABLE [dbo].[PermisoPermisos]
GO
