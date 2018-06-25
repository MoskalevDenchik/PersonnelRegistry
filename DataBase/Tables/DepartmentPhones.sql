CREATE TABLE [DepartmentPhones](
[Id] INT IDENTITY(1, 1),
[DepartmentId] INT,
[Number] NVARCHAR(16));
GO

--�������� ���������� �����
ALTER TABLE [DepartmentPhones]
ADD CONSTRAINT [pk_PhoneId] PRIMARY KEY([Id]);
GO

--�������� �������� �����
ALTER TABLE [DepartmentPhones]
ADD CONSTRAINT [fk_Phones_to_DepartmentsId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([Id]) ON DELETE CASCADE;
GO