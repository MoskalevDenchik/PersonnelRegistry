CREATE PROCEDURE [UpdateEmployeeEmails]
@EmployeeId INT,
@Emails [EmailsType] READONLY
AS
UPDATE [Emails]
   SET [Address] = [a].[Address] FROM @Emails as[a]
 WHERE [Emails].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Address] is NOT NULL;

DELETE [Emails]
 WHERE [Id] IN (SELECT [Id] From @Emails	
				WHERE [Address] is NULL);

INSERT INTO [Emails](
			[Address])
	 SELECT [Address] FROM @Emails as [a]
	  WHERE [a].[Id] = 0;
GO