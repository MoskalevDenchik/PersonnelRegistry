CREATE TRIGGER [InsertEmployeePhonesTriger]
ON [PhonesEmployeeView] INSTEAD OF INSERT 
AS 
BEGIN
declare @Ids TABLE([Id] INT );
 
INSERT INTO [Phones](
			[Number],
			[KindId]) OUTPUT [inserted].[Id] into @Ids	
	 SELECT [Number],
			[KindId] FROM inserted;

INSERT INTO [PhonesEmployees](
			[PhoneId],
			[EmployeeId])
	 SELECT [Id],
		    (SELECT TOP 1 [EmployeeId]  FROM [inserted]) FROM @Ids
END;
GO