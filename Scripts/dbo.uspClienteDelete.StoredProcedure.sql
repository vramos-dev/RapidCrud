USE [Rapid]
GO
/****** Object:  StoredProcedure [dbo].[uspClienteDelete]    Script Date: 2/11/2022 12:12:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteDelete]
(
	@Id int
)
AS
BEGIN
	
	DELETE FROM Cliente 
	WHERE 
		Id = @Id;
	
END
GO
