CREATE PROCEDURE uspClienteGetOne
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
