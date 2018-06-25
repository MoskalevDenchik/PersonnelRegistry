CREATE FUNCTION [dbo].[GetEmployeesBySerchParams](
@LastName nvarchar(16) = '',
@FirstName nvarchar(16) = '',
@MiddleName nvarchar(16) = '',
@minTime INT,
@maxTime INT,
@WorkStatusId int)
RETURNS TABLE
AS
RETURN SELECT [Id] as [EmployeeId],
              [DepartmentId] FROM [Employees] 
		WHERE [LastName] LIKE @LastName + '%' 
		  AND [FirstName] LIKE @FirstName +'%'
		  AND [MiddleName] LIKE @MiddleName +'%'
		  AND DATEDIFF( YEAR, BeginningWork, ISNULL(EndWork,GETDATE())) BETWEEN @minTime and @maxTime
		  AND WorkStatusId = IIF(@WorkStatusId = 0, [WorkStatusId],@WorkStatusId);
GO