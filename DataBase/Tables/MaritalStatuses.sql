CREATE TABLE [MaritalStatuses]
([Id] INT IDENTITY(1, 1) NOT NULL,
 [Status] NVARCHAR(16) NOT NULL)
 GO

 --�������� ���������� �����
ALTER TABLE [MaritalStatuses]
ADD CONSTRAINT [pk_maritalStatusId] PRIMARY KEY (Id);
GO