CREATE TABLE [PhonesDepartments](
[PhoneId] INT NOT NULL,
[DepartmentId] INT NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [PhonesDepartments]
ADD CONSTRAINT [pk_PhoneIdAndDepartment] PRIMARY KEY ([PhoneId]);
GO

--Создание уникальности
ALTER TABLE [PhonesDepartments]
ADD CONSTRAINT [uq_PhoneIdAndDepartment] UNIQUE ([PhoneId]);
GO

--Создание внешнего ключа
ALTER TABLE [PhonesDepartments]
ADD CONSTRAINT [fk_PhonesDepartments_to_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [PhonesDepartments]
ADD CONSTRAINT [fk_PhonesDepartments_to_PhonesId] FOREIGN KEY ([PhoneId]) REFERENCES [Phones]([Id]) ON DELETE CASCADE;
GO