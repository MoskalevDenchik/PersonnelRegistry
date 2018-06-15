CREATE PROCEDURE [SelectDepartmentByEmployeeId]
@Id INT
AS
SELECT [D].[Id],
	   [ParentID],
       [Name],
	   [D].[Address],
	   [Description] FROM [Departments] as [D]
LEFT JOIN [Employees] as [E]
ON [D].[Id] = DepartmentId
WHERE [E].[Id] = @Id;   
GO