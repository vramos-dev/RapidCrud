USE [Rapid]
GO
/****** Object:  StoredProcedure [dbo].[uspClienteListAll]    Script Date: 2/11/2022 12:12:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteListAll]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
		Id,
		Dni,
		Nombre,
		Apellido,
		Telefono,
		Email
	FROM Cliente
	ORDER BY Apellido, Nombre;
	
END
GO
