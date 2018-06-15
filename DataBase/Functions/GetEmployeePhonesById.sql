CREATE FUNCTION [GetEmployeePhonesById](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesEmployeeView]
 WHERE [EmployeeId]= @Id);
GO