CREATE PROCEDURE uspClienteDelete
(
	@Id int
)
AS
BEGIN
	
	DELETE FROM Cliente 
	WHERE 
		Id = @Id;
	
END
