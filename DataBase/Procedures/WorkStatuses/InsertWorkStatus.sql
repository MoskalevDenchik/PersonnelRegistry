CREATE PROCEDURE [InsertWorkStatus]
@Status NVARCHAR(32)
AS
INSERT INTO [WorkStatuses]
VALUES (@Status);
GO