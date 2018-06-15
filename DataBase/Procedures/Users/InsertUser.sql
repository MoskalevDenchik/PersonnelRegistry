CREATE PROCEDURE [InsertUser]
@EmployeeId INT,
@Login NVARCHAR(64),
@Password NVARCHAR(64),
@Roles [RolesType] READONLY 
AS
INSERT INTO [Users](
			[EmployeeId],
			[Login],
			[Password])
	VALUES
			(@EmployeeId,
			 @Login,
			 @Password);
DECLARE @UserId INT = @@IDENTITY;

UPDATE [Employees]
SET [HasRole] = 1
WHERE [Id] = @EmployeeId;
 

INSERT INTO [RolesUsers](
			[RoleId],
			[UserId])
     SELECT [Id], 
	        @UserId FROM @Roles;
GO