CREATE TABLE [Employees](
[Id] INT IDENTITY(1, 1),
[DepartmentId] INT NULL,
[LastName] NVARCHAR(16) NULL,
[FirstName] NVARCHAR(16) NULL,
[MiddleName] NVARCHAR(16) NULL,
[Address] NVARCHAR(128) NULL,
[MaritalStatusId]INT NULL,
[ImagePath] NVARCHAR(MAX) NULL,
[BeginningWork] DATE  NULL,
[EndWork] DATE NULL,
[WorkStatusId] INT NULL,
[HasRole] BIT DEFAULT 0);
GO

--Создание первичного ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [pk_employeeId] PRIMARY KEY (Id);
GO

--Создание внешнего ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [fk_departmentId] FOREIGN KEY (DepartmentId) REFERENCES [Departments]([Id]) ON DELETE SET NULL ;
GO

--Создание внешнего ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [fk_maritalStatusId] FOREIGN KEY ([MaritalStatusId]) REFERENCES [MaritalStatuses]([Id]) ON DELETE SET NULL ;
GO

--Создание внешнего ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [fk_workStatusId] FOREIGN KEY ([WorkStatusId]) REFERENCES [WorkStatuses]([Id]) ON DELETE SET NULL ;
GO