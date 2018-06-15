CREATE PROCEDURE [DeleteEmployeeById]
@Id INT
AS
DELETE FROM [dbo].[Phones] 
	  WHERE [Id] IN (SELECT [PhoneId] FROM [dbo].[PhonesEmployees]
					  WHERE [EmployeeId] = @Id);
					  
DELETE FROM [dbo].[Emails]
      WHERE [EmployeeId] = @id;  

DELETE FROM [dbo].[Employees]
	  WHERE [Id] = @Id;
GO