CREATE PROCEDURE [dr].[usp_BulkInsertDrivers]

    @DriverList  [dr].[DriversListType] READONLY
AS
BEGIN
    INSERT INTO Driver (FirstName, LastName, Email, PhoneNumber)
    SELECT FirstName, LastName, Email, PhoneNumber
    FROM @DriverList;
END