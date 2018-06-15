CREATE PROCEDURE [UpdateWorkStatus]
@Id INT,
@Status NVARCHAR(32)
AS
UPDATE [WorkStatuses]
SET [Status] = @Status  
WHERE Id = @Id;
GO