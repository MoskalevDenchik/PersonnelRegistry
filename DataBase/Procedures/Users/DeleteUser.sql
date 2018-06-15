CREATE PROCEDURE [DeleteUser]
@Id INT
AS 
UPDATE [Employees]
SET [HasRole] = 0
WHERE [Id] = (SELECT [EmployeeId] FROM [Users] WHERE [Id] = @Id);
 
DELETE [Users]
WHERE [Id] = @Id;

GO