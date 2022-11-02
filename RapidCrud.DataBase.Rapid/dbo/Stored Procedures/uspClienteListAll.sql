CREATE PROCEDURE uspClienteListAll
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
