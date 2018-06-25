CREATE PROCEDURE [SaveMaritalStatus]
@Id INT,
@Status NVARCHAR(16)
AS
IF @Id = 0
EXEC [InsertMaritalStatus] @Status;
ELSE 
EXEC [UpdateMaritalStatus] @Id,@Status;
GO