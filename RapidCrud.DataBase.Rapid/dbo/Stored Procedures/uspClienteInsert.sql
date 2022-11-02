CREATE PROCEDURE uspClienteInsert
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
