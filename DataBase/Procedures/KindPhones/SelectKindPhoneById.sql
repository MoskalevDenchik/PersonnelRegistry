CREATE PROCEDURE [SelectKindPhoneById]
@Id INT
AS
SELECT [Id],
	   [Kind] FROM [KindPhones]
WHERE [Id] = @Id;
GO