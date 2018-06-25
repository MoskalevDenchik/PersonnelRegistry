CREATE PROCEDURE [SelectDepartmtsByPage]
@PageSize INT,
@Page INT
AS
CREATE TABLE [#Searched]([DepartmentId] INT)
INSERT INTO  [#Searched] 
	  SELECT [DepartmentId] FROM dbo.GetDepartmentsByPage(@PageSize,@Page)

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [dbo].[Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId];

SELECT COUNT([Id]) as [Count] FROM [Departments];
GO