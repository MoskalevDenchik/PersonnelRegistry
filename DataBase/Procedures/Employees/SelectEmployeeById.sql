CREATE PROCEDURE [SelectEmployeeById]
@Id INT
AS
Select [Id],
	   [DepartmentId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MaritalStatusId],
	   [MaritalStatus],
	   [WorkStatusId],
	   [WorkStatus] FROM [GetEmployeeById](@Id)

SELECT [Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [GetEmployeePhonesById](@Id);

SELECT [Id],
	   [EmployeeId],
       [Address] FROM [Emails]
 WHERE [EmployeeId] = @Id;

  exec [SelectDepartmentByEmployeeId] @Id;

SELECT [Id],
	   [DepartmentId],
	   [Number],
	   [KindId],
	   [Kind] FROM [GetDepartmentPhonesByEmployeeId](@Id);

GO