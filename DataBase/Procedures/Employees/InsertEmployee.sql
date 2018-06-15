CREATE PROCEDURE [InsertEmployee]
@LastName nvarchar(16) = null,
@FirstName nvarchar(16) = null,
@MiddleName nvarchar(16) = null,
@DepartmentId int = null,
@Address nvarchar(128) = null,
@ImagePath nvarchar(max) = null,
@BeginningWork date = null,
@EndWork date = null,
@WorkStatusId INT = null,  
@MaritalStatusId int = null,
@Phones [PhonesType] READONLY,
@Emails [EmailsType] READONLY
AS
INSERT INTO [Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
           ,[ImagePath]
           ,[BeginningWork]
           ,[EndWork]
           ,[MaritalStatusId]
		   ,[WorkStatusId])
     VALUES
           (@LastName,
			@FirstName,
			@MiddleName,
			@DepartmentId,
			@Address,
			@ImagePath,
			@BeginningWork,
			@EndWork,
			@MaritalStatusId,
			@WorkStatusId);

DECLARE @Id INT = @@IDENTITY;

INSERT INTO [PhonesEmployeeView](
		    [EmployeeId],
			[Number],
		    [KindId])
	SELECT  @Id,
			[Number],
			[KindId] FROM @Phones;

INSERT INTO [dbo].[Emails](
			[EmployeeId],
			[Address])
	SELECT  @Id,
			[Address] FROM @Emails;
GO
