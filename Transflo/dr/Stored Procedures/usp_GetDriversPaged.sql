CREATE PROCEDURE [dr].[usp_GetDriversPaged]
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    IF @PageNumber >= 1
	BEGIN
	SET @PageNumber= @PageNumber -1
	END
	ELSE
	BEGIN
	SET @PageNumber= 0
	END
	DECLARE @Curentpage INT = (@PageNumber + 1)
	DECLARE @offset INT=(@PageNumber * @PageSize)
    DECLARE @TotalCount INT = (SELECT COUNT(*) FROM [dr].[Driver]);

    SELECT 
          @TotalCount 'TotalCount',
          @Curentpage 'Page',
          @PageSize 'PageSize',
          Id, 
          FirstName,
          LastName, 
          Email,
          PhoneNumber
    FROM [dr].[Driver]
    ORDER BY Id
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;

  
END