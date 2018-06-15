CREATE PROCEDURE [GetAllMaritalStatuses]
AS
SELECT [Id],
	   [Status] FROM [MaritalStatuses];
GO