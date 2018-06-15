CREATE VIEW [PhonesEmployeeView]
AS
SELECT [EmployeeId],
	   [P].[Id],
	   [Number],
	   [KindId],
	   [Kind]  FROM [PhonesEmployees]
  JOIN [Phones] as [P]
    ON [PhoneId] = [Id]
  JOIN [KindPhones] as [KP]
    ON [KP].[Id] = [P].[KindId];
GO