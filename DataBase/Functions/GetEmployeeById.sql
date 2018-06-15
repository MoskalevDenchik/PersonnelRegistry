CREATE FUNCTION [GetEmployeeById](@Id INT)
RETURNS TABLE 
AS
RETURN(
Select [E].[Id],
	   [DepartmentId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MS].[Id] as [MaritalStatusId],
	   [MS].[Status] as [MaritalStatus],
	   [WS].[Id] as [WorkStatusId],
	   [WS].[Status] as [WorkStatus] FROM [Employees] as [E]
LEFT JOIN [MaritalStatuses] as [MS]
    ON [E].[MaritalStatusId] = [MS].[Id]
LEFT JOIN [WorkStatuses] as [WS]
    ON [E].[WorkStatusId] = [WS].[Id]
 WHERE [E].[Id] = @Id);
 GO