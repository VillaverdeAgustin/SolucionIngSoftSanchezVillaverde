USE [FerreDB]
GO

ALTER TABLE [dbo].[Usuarios] WITH CHECK ADD CONSTRAINT [FK_Usuarios_Permiso] FOREIGN KEY([Rol])
REFERENCES [dbo].[Permiso] ([Nombre_Permiso])
GO

ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Permiso]
GO
