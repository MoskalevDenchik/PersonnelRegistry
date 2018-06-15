CREATE TABLE [PhonesEmployees](
[PhoneId] INT NOT NULL,
[EmployeeId] INT NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [pk_PhoneIdAndEmployeeId] PRIMARY KEY ([PhoneId]);
GO

--Создание уникальности
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [uq_PhoneIdAndEmployee] UNIQUE ([PhoneId]);
GO

--Создание внешнего ключа
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [fk_PhonesEmploees_to_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [fk_PhonesEmployees_to_PnoneId] FOREIGN KEY ([PhoneId]) REFERENCES [Phones]([Id]) ON DELETE CASCADE;
GO