CREATE TABLE [WorkStatuses](
[Id] INT IDENTITY (1,1),
[Status] NVARCHAR(32) NOT NULL);
GO

--�������� ���������� �����
ALTER TABLE [WorkStatuses]
ADD CONSTRAINT [pk_workStatusId] PRIMARY KEY ([Id]);
GO