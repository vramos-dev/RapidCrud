USE [Rapid]
GO
/****** Object:  StoredProcedure [dbo].[uspClienteGetOne]    Script Date: 2/11/2022 12:12:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteGetOne]
(
	@Id int,
	@Dni varchar(8)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT TOP 1
		Id,
		Dni,
		Nombre,
		Apellido,
		Telefono,
		Email
	FROM Cliente
	WHERE 
	   (ISNULL(@Id, 0) = 0 OR Id = @Id)
	   AND (ISNULL(@Dni, '') = '' OR Id = @Id);
	
END
GO
