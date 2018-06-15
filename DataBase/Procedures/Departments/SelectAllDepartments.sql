CREATE PROCEDURE [SelectAllDepartments]
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]

SELECT [Id], 
	   [DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] 
GO