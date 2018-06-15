CREATE PROCEDURE [SelectWorkStatusById]
@Id INT
AS
SELECT [Id],
	   [Status] FROM [WorkStatuses]
WHERE [Id] = @Id;
GO