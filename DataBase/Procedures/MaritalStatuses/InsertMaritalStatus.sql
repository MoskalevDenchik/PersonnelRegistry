CREATE PROCEDURE [InsertMaritalStatus]
@Status NVARCHAR(32)
AS
INSERT INTO [MaritalStatuses]
VALUES (@Status);
GO