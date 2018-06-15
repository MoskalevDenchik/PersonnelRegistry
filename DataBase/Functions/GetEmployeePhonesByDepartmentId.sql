CREATE FUNCTION [GetEmployeePhonesByDepartmentId](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [PE].[Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesEmployeeView] as [PE]
  JOIN [Employees] as [E]
    ON [E].[Id] = [EmployeeId]
WHERE [DepartmentId] = @Id);
GO 