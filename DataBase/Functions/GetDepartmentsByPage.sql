CREATE FUNCTION [GetDepartmentsByPage](@PageSize INT,@PageNumber INT)
RETURNS Table
AS
RETURN SELECT [Id] as [DepartmentId] FROM [Departments]
	 ORDER BY [Id]
	   OFFSET @PageSize *( @PageNumber - 1 ) ROWS
   FETCH NEXT @PageSize ROWS ONLY ;
GO