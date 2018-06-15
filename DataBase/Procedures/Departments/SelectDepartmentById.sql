CREATE PROCEDURE [SelectDepartmentById]
@Id INT
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
 WHERE @Id = [Id]

SELECT [DepartmentId],
	   [Id],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView]
WHERE [DepartmentId] = @Id;
GO