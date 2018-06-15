CREATE FUNCTION [GetDepartmentsByPage](@PageSize INT,@Page INT)
RETURNS Table
AS
RETURN SELECT [Id] as [DepartmentId] FROM [Departments]
	   ORDER BY [Id]
	   OFFSET @PageSize *( @Page - 1 ) ROWS
	   FETCH NEXT @PageSize ROWS ONLY ;
GO
