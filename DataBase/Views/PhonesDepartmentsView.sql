CREATE VIEW [PhonesDepartmentView] 
AS
SELECT [DepartmentId],
	   [P].[Id],
	   [Number],
	   [KindId],
	   [Kind]  FROM [PhonesDepartments]
  JOIN [Phones] as [P]
    ON [PhoneId] = [Id]
  JOIN [KindPhones] as [KP]
    ON [KP].[Id] = [P].[KindId];
GO
