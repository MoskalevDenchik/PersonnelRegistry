CREATE PROCEDURE [SelectPageEmployees]
@PageSize INT,
@PageNumber INT 
AS

CREATE TABLE [#Searched](EmployeeId INT,DepartmentId INT);
INSERT INTO  [#Searched] 
	  SELECT [EmployeeId], [DepartmentId] FROM dbo.GetEmployeesByPage(@PageSize,@PageNumber);

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HomePhone],
	   [WorkPhone],
	   [MobilePhone],
	   [HasRole],
	   [MS].[Status] as [MaritalStatus],
	   [MaritalStatusId],
	   [WS].[Status] as [WorkStatus],
	   [WorkStatusId]  FROM Employees as [E]
LEFT JOIN [MaritalStatuses] as [MS]
ON [E].[MaritalStatusId] =[Ms].[Id]
LEFT JOIN [WorkStatuses] as [WS]
ON [E].[WorkStatusId] =[WS].[Id]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [E].[Id]; 

SELECT [Emails].[EmployeeId],
	   [Emails].[Id], 
	   [Emails].[Address] FROM [Emails]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [Emails].[EmployeeId];

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId]
	GROUP BY [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number] FROM [DepartmentPhones] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number]

SELECT COUNT([ID]) as [Count] FROM [Employees];
GO