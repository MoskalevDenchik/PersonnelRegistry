CREATE TABLE [Users](
[Id] INT IDENTITY (1,1),
[EmployeeId] INT NOT NULL,
[Login] NVARCHAR(32) NOT NULL,
[Password] NVARCHAR(64) NOT NULL)
GO

--�������� ���������� �����
ALTER TABLE [Users]
ADD CONSTRAINT [pk_UserId] PRIMARY KEY ([Id]);
GO

--�������� ������������
ALTER TABLE [Users]
ADD CONSTRAINT [uq_UserId] UNIQUE ([EmployeeId]);
GO

--�������� �������� �����
ALTER TABLE [Users]
ADD CONSTRAINT [fk_UserId_to_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([Id]) ON DELETE CASCADE;
GO