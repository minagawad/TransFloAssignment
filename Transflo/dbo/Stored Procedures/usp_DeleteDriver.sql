CREATE PROCEDURE [dbo].[usp_DeleteDriver]
	  @Id INT
AS
BEGIN
    DELETE FROM [dr].[Driver] WHERE Id = @Id
END