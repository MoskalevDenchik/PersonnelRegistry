CREATE PROCEDURE [SelectDepartmentByName]
@Name NVARCHAR(16)
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
 WHERE @Name = [Name]

SELECT [DepartmentId],
	   [D].[Id],
	   [Number] FROM [DepartmentPhones] as [DP]
JOIN [Departments] as [D]
ON [D].[Id] = [DP].[DepartmentId]
WHERE [D].[Name] = @Name;
GO