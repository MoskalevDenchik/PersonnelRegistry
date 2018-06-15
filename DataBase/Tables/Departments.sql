CREATE TABLE [Departments](
[Id] INT IDENTITY(1, 1),
[ParentID] INT NULL,
[Name] NVARCHAR(32) NULL,
[Address] NVARCHAR(128) NULL,
[Description] NVARCHAR(128) NULL)
GO
--Создание первичного ключа
ALTER TABLE [Departments]
ADD CONSTRAINT [pk_departmentId] PRIMARY KEY ([Id]);
GO