USE [Rapid]
GO
/****** Object:  StoredProcedure [dbo].[uspClienteUpdate]    Script Date: 2/11/2022 12:12:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteUpdate]
(
	@Id int,
	@Dni varchar(8),
	@Nombre varchar(100),
	@Apellido varchar(100),
	@Telefono varchar(9),
	@Email varchar(100)
)
AS
BEGIN
	
	UPDATE Cliente 
	SET
		Dni = @Dni,
		Nombre = @Nombre,
		Apellido = @Apellido,
		Telefono = @Telefono,
		Email = @Email
	WHERE 
		Id = @Id;
	
END
GO
