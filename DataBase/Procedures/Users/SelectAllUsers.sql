CREATE PROCEDURE [SelectAllUsers]
AS
SELECT [U].[Id],
	   [U].[EmployeeId],
	   [Login],
	   [Password] FROM [Users] as [U]
JOIN [Employees] as [E]
ON [E].[Id] = [U].[EmployeeId];

SELECT  [U].[Id] as [UserId],
        [Emails].[Id] as [Id],
		[Address] FROM [Emails]
JOIN [Users] as [U]
ON [U].[EmployeeId] = [Emails].[EmployeeId];


SELECT [U].[Id] as [UserId],
	   [R].[Id], 
       [R].[Name] FROM [Roles] as [R]
JOIN [RolesUsers] as [RU]
ON [R].[Id] = [RoleId]
JOIN [Users] as [U]
ON [U].[Id] = [UserId];
GO