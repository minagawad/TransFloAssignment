CREATE PROCEDURE [dr].[usp_UpdateDriver]
 @Id INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(20)
AS
BEGIN
    UPDATE [dr].[Driver] SET
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber
    WHERE Id = @Id
END