CREATE TABLE [Emails](
[Id] INT IDENTITY(1, 1),
[EmployeeId] INT NOT NULL,
[Address] VARCHAR(32) NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Emails]
ADD CONSTRAINT [pk_EmailsId] PRIMARY KEY([Id]);
GO

--Создание внешнего ключа
ALTER TABLE [Emails]
ADD CONSTRAINT [fk_Emails_to_EmployeesId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([Id]) ON DELETE CASCADE;
GO