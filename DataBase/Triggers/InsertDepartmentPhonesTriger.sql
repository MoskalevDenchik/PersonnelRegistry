CREATE TRIGGER [InsertDepartmentPhonesTriger] 
ON [PhonesDepartmentView] INSTEAD OF INSERT 
AS 
BEGIN
declare @Ids table([Id] INT);
 
INSERT INTO [Phones](
			[Number],
			[KindId]) OUTPUT [inserted].[Id] into @Ids	
	 SELECT [Number],
	        [KindId] FROM inserted;

INSERT INTO [PhonesDepartments](
			[PhoneId],
			[DepartmentId])
	 SELECT [Id],
		    (SELECT TOP 1 [DepartmentId]  FROM [inserted]) FROM @Ids
END;
GO