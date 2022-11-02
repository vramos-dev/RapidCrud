USE [Rapid]
GO
/****** Object:  StoredProcedure [dbo].[uspClienteInsert]    Script Date: 2/11/2022 12:12:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteInsert]
(
	@Dni varchar(8),
	@Nombre varchar(100),
	@Apellido varchar(100),
	@Telefono varchar(9),
	@Email varchar(100)
)
AS
BEGIN
	
	INSERT INTO Cliente 
	(
		Dni,
		Nombre,
		Apellido,
		Telefono,
		Email
	)
	VALUES 
	(
		@Dni,
		@Nombre,
		@Apellido,
		@Telefono,
		@Email
	);

	SELECT CONVERT(INT, SCOPE_IDENTITY());
	
END
GO
