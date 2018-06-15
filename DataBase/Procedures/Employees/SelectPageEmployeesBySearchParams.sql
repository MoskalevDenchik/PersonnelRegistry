CREATE PROCEDURE [SelectPageEmployeesBySearchParams]
@LastName nvarchar(16) = '',
@FirstName nvarchar(16) = '',
@MiddleName nvarchar(16) = '',
@fromYear INT,
@toYear INT,
@WorkStatusId int,
@PageSize INT,
@Page INT
AS

CREATE TABLE [#Searched]([EmployeeId] INT,[DepartmentId] INT);
INSERT INTO  [#Searched] 
SELECT * FROM dbo.GetEmployeeBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId)
    ORDER BY [EmployeeId]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
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

SELECT [PE].[EmployeeId],
	   [PE].[Id],
	   [Number],
       [KindId],
	   [Kind] From [PhonesEmployeeView] as [PE]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [PE].[EmployeeId];

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
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind];

SELECT COUNT(*) as [Count] FROM dbo.GetEmployeeBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId);

GO