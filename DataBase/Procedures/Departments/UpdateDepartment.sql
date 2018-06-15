CREATE PROCEDURE [UpdateDepartment]
@Id INT,
@ParentId INT = NUll,
@Name nvarchar(32),
@Address nvarchar(64) = NULL,
@Description nvarchar(128) = NULL,
@Phones [PhonesType] READONLY
AS
EXEC [UpdateDepartmentPhones] @Id, @Phones;

UPDATE [dbo].[Departments]
   SET [ParentID] = @ParentId
      ,[Name] = @Name
      ,[Address] =  @Address
      ,[Description] = @Description
 WHERE [Id] = @Id;
GO