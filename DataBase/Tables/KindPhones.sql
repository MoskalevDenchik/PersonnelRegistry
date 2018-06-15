CREATE TABLE [KindPhones](
[Id] INT IDENTITY(1,1),
[Kind] NVARCHAR(32) NOT NULL)
GO
--Создание первичного ключа
ALTER TABLE [KindPhones]
ADD CONSTRAINT [pk_kindPhoneId] PRIMARY KEY ([Id]);
GO