CREATE PROCEDURE [UpdateMaritalStatus]
@Id INT,
@Status NVARCHAR(32)
AS
UPDATE [MaritalStatuses]
SET [Status] = @Status  
WHERE Id = @Id;
GO