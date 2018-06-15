CREATE PROCEDURE [UpdateKindPhone]
@Id INT,
@Kind NVARCHAR(32)
AS
UPDATE [KindPhones]
SET [Kind] = @Kind  
WHERE Id = @Id;
GO