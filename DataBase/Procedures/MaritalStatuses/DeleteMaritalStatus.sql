CREATE PROCEDURE [DeleteMaritalStatus]
@Id INT
AS
DELETE  [MaritalStatuses]
WHERE [Id] = @id;
GO