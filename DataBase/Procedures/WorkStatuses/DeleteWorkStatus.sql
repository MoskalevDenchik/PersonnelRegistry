CREATE PROCEDURE [DeleteWorkStatus]
@Id INT
AS
DELETE  [WorkStatuses]
WHERE [Id] = @id;
GO