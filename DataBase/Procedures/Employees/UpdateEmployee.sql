CREATE PROCEDURE [UpdateEmployee]
@Id INT,
@LastName nvarchar(16) = null,
@FirstName nvarchar(16) = null,
@MiddleName nvarchar(16) = null,
@DepartmentId int = null,
@Address nvarchar(128) = null,
@ImagePath nvarchar(max) = null,
@BeginningWork date = null,
@EndWork date = null,
@MaritalStatusId int = null,
@WorkStatusId int = null,
@Phones [PhonesType] READONLY,
@Emails [EmailsType] READONLY
AS
EXEC [UpdateEmployeePhones] @Id, @Phones;
EXEC [UpdateEmployeeEmails] @Id, @Emails;

UPDATE [Employees]
      SET [LastName] = @LastName,
          [FirstName] = @FirstName,
          [MiddleName] = @MiddleName,
          [DepartmentId] = @DepartmentId,
          [Address] = @Address,
          [ImagePath] = @ImagePath,
          [BeginningWork] = @BeginningWork,
          [EndWork] = @EndWork,
          [MaritalStatusId] = @MaritalStatusId,
		  [WorkStatusId] = @WorkStatusId
	WHERE [Id] = @Id;
GO