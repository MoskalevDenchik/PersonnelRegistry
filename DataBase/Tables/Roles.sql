CREATE TABLE [Roles](
[Id] INT IDENTITY(1,1),
[Name] NVARCHAR(32) NOT NULL)
GO

--�������� ���������� �����
ALTER TABLE [Roles]
ADD CONSTRAINT [pk_RolesId] PRIMARY KEY ([Id]);
GO
