CREATE PROCEDURE [SaveWorkStatus]
@Id INT,
@Status NVARCHAR(16)
AS
IF @Id = 0
EXEC [InsertWorkStatus] @Status;
ELSE 
EXEC [UpdateWorkStatus] @Id,@Status;
GO