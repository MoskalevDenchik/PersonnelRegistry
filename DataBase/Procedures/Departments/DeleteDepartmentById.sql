CREATE PROCEDURE [DeleteDepartmentById]
@Id INT
AS
DELETE FROM [dbo].[Phones] 
	  WHERE [Id] IN (SELECT [PhoneId] FROM [dbo].[PhonesDepartments]
					  WHERE [DepartmentId] = @Id);

DELETE FROM [dbo].[Departments]
	  WHERE [Id] = @Id;
GO