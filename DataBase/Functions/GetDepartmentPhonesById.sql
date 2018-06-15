CREATE FUNCTION [GetDepartmentPhonesById](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [Id],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView]
 WHERE [DepartmentId]= @Id);
GO