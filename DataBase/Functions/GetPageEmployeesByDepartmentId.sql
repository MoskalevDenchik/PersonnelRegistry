CREATE FUNCTION [dbo].[GetPageEmployeesByDepartmentId](@DepartmentId INT,@PageSize INT,@Page INT)
RETURNS Table
AS
 RETURN SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
    WHERE [DepartmentId] = @DepartmentId
	ORDER BY [Id]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO