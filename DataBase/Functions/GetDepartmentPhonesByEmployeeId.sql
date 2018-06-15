CREATE FUNCTION [GetDepartmentPhonesByEmployeeId](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [PD].[Id],
	   [PD].[DepartmentId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [PD]
  JOIN [Employees] as [E]
    ON [E].[DepartmentId] = [PD].[DepartmentId]
 WHERE [E].Id = @Id);
GO 