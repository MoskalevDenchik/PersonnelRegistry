
CREATE FUNCTION [dbo].[GetUserIdByEmplyeeId](@EmployeeId INT)
RETURNS INT 
AS
BEGIN 
RETURN (SELECT TOP 1[Id] FROM [Users] WHERE [EmployeeId] = @EmployeeId)
END;
GO