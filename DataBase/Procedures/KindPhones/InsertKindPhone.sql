CREATE PROCEDURE [InsertKindPhone]
@Kind NVARCHAR(32)
AS
INSERT INTO [KindPhones]
VALUES (@Kind);
GO