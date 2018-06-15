CREATE PROCEDURE [SelectUserByLogin]
@Login NVARCHAR(32)
AS
SELECT [U].[Id],
	   [U].[EmployeeId],
	   [Login],
	   [Password] FROM [Users] as [U]
JOIN [Employees] as [E]
ON [E].[Id] = [U].[EmployeeId]
WHERE [U].[Login] = @Login;

SELECT  [U].[Id] as [UserId],
        [Emails].[Id],
		[Address] FROM [Emails]
JOIN [Users] as [U]
ON [U].[EmployeeId] = [Emails].[EmployeeId]
WHERE [U].[Login] = @Login;


SELECT [U].[Id] as [UserId],
	   [R].[Id], 
       [R].[Name] FROM [Roles] as [R]
JOIN [RolesUsers] as [RU]
ON [R].[Id] = [RoleId]
JOIN [Users] as [U]
ON [U].[Id] = [UserId]
WHERE [U].[Login] = @Login;
GO