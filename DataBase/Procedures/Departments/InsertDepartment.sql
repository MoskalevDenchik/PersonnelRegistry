CREATE PROCEDURE [InsertDepartment]
@parentId INT  = NULL,
@name NVARCHAR(32),
@address NVARCHAR (64) =  NULL,
@description NVARCHAR(128) = NULL,
@Phones [PhonesType] READONLY
AS
INSERT INTO [dbo].[Departments]
           ([ParentId]
           ,[Name]
           ,[Address]
           ,[Description])
     VALUES
			(@parentId,
			 @name,
			 @address,
			 @description);
DECLARE @DepartmentId INT = @@IDENTITY;

INSERT INTO [PhonesDepartmentView]
			([DepartmentId],
			[Number],
		    [KindId])
     SELECT @DepartmentId,
	        [Number],
            [KindId] FROM @Phones;

SELECT SCOPE_IDENTITY();
GO