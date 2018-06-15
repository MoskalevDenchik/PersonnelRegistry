CREATE PROCEDURE [UpdateDepartmentPhones]
@DepartmentId INT,
@Phones [PhonesType] READONLY
AS
UPDATE [Phones]
   SET [Number] = [a].[Number],
	   [KindId] = [a].[KindId] FROM @Phones  as [a]
 WHERE [Phones].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Number] is NOT NULL;

DELETE [Phones]
 WHERE [Id] IN (SELECT [Id] From @Phones	
				WHERE [Number] is NULL);

INSERT INTO [PhonesDepartmentView](
			[DepartmentId],
			[Number],
		    [KindId])
	 SELECT @DepartmentId,
	        [Number],
            [KindId] FROM @Phones as [a] 
	  WHERE [a].[Id] = 0;
GO