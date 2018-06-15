CREATE FUNCTION [AllEmployeesByPage](@PageSize INT,@Page INT)
RETURNS Table
AS
 return SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
	ORDER BY [Id]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO