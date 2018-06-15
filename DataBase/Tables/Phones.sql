CREATE TABLE [Phones](
[Id] INT IDENTITY(1, 1),
[Number] NVARCHAR(16) NOT NULL,
[KindId] INT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Phones]
ADD CONSTRAINT [pk_PhoneId] PRIMARY KEY([Id]);
GO

--Создание внешнего ключа
ALTER TABLE [Phones]
ADD CONSTRAINT [fk_kindId] FOREIGN KEY (KindId) REFERENCES [KindPhones]([Id]) ON DELETE SET NULL ;
GO