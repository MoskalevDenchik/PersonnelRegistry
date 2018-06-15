CREATE PROCEDURE [DeleteKindPhone]
@Id INT
AS
DELETE  [KindPhones]
WHERE [Id] = @id;
GO