CREATE PROCEDURE [GetMaritalStatusById]
@Id INT
AS
SELECT [Id],
	   [Status] FROM MaritalStatuses
WHERE [Id] = @Id;
GO