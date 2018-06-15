CREATE TABLE [RolesUsers](
[UserId] INT,
[RoleId] INT)
GO

--Создание внешнего ключа
ALTER TABLE [RolesUsers]
ADD CONSTRAINT [fk_RolesUsers_to_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [RolesUsers]
ADD CONSTRAINT [fk_RolesUsers_to_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id]) ON DELETE CASCADE;
GO