CREATE PROCEDURE uspClienteUpdate
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
