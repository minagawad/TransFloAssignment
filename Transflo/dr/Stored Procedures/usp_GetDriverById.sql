CREATE PROCEDURE [dr].[usp_GetDriverById]
	 @Id INT
AS
BEGIN
    SELECT * FROM [dr].[Driver] WHERE Id = @Id
END