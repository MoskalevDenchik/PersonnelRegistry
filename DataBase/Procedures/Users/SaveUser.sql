CREATE PROCEDURE [SaveUser]
@Id INT,
@EmployeeId INT,
@Login NVARCHAR(16),
@Password NVARCHAR(16),
@Roles [RolesType] READONLY
AS 
IF @Id = 0
EXEC [InsertUser] @EmployeeId,@Login,@Password,@Roles;
ELSE 
EXEC [UpdateUser] @Id,@EmployeeId,@Login,@Password,@Roles;
GO