CREATE PROCEDURE [SaveDepartment]
@Id INT,
@parentId INT,
@name NVARCHAR(32),
@address NVARCHAR (64),
@description NVARCHAR(128),
@Phones [PhonesType] READONLY
AS
IF @Id=0
EXEC [InsertDepartment] @ParentId,@Name,@Address,@description,@Phones;
ELSE
EXEC [UpdateDepartment] @Id,@ParentId,@Name,@Address,@description,@Phones;
GO