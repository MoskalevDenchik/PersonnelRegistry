CREATE PROCEDURE [SaveEmployee]
@Id INT,
@LastName nvarchar(16),
@FirstName nvarchar(16),
@MiddleName nvarchar(16),
@DepartmentId INT,
@Address nvarchar(64),
@ImagePath nvarchar(max),
@BeginningWork DATETIME,
@EndWork DATETIME = null,
@HomePhone NVARCHAR(16) = null,
@WorkPhone NVARCHAR(16),
@MobilePhone NVARCHAR(16),
@MaritalStatusId INT,
@WorkStatusId INT,
@Emails [EmailsType] READONLY
AS
IF @Id=0
EXEC [InsertEmployee] @LastName,@FirstName,@MiddleName,@DepartmentId,@Address,@ImagePath,@BeginningWork,@EndWork,@HomePhone,@WorkPhone,@MobilePhone,@MaritalStatusId,@WorkStatusId,@Emails;
ELSE 
EXEC [UpdateEmployee] @Id,@LastName,@FirstName,@MiddleName,@DepartmentId,@Address,@ImagePath,@BeginningWork,@EndWork,@HomePhone,@WorkPhone,@MobilePhone,@MaritalStatusId,@WorkStatusId,@Emails;
GO