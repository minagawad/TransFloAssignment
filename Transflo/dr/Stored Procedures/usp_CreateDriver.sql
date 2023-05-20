CREATE PROCEDURE [dr].[usp_CreateDriver]
	  @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(50),
    @PhoneNumber VARCHAR(20)
AS
BEGIN
    INSERT INTO [dr].[Driver] (FirstName, LastName, Email, PhoneNumber)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber)
END