USE [master]
GO
------------------------------------------------DataBase-----------------------------------------------
CREATE DATABASE [PersonDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PersonDB',
  FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PersonDB.mdf' , 
  SIZE = 8192KB , 
  MAXSIZE = UNLIMITED, 
  FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PersonDB_log',
  FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PersonDB_log.ldf' ,
  SIZE = 8192KB , 
  MAXSIZE = 2048GB ,
  FILEGROWTH = 65536KB )
GO

USE [PersonDB]
GO


---------------------------------------------------------------------------------------------------------
-----------------------------------------------------TABLES----------------------------------------------
---------------------------------------------------------------------------------------------------------



--------------------------------------------------Departments--------------------------------------------
CREATE TABLE [Departments](
[DepartmentId] INT IDENTITY(1, 1) NOT NULL,
[ParentID] INT NULL,
[Name] NVARCHAR(32) NOT NULL,
[Address] NVARCHAR(64) NULL,
[Description] NVARCHAR(128) NULL)
GO
--Создание первичного ключа
ALTER TABLE [Departments]
ADD CONSTRAINT [pk_department] PRIMARY KEY ([DepartmentId]);
GO

---------------------------------------------------MaritalStatus-----------------------------------------
CREATE TABLE [MaritalStatus]
([Id] INT IDENTITY(1, 1) NOT NULL,
 [StatusName] NVARCHAR(32) NOT NULL)
 GO

 --Создание первичного ключа
ALTER TABLE [MaritalStatus]
ADD CONSTRAINT [pk_ID] PRIMARY KEY (Id);
GO

----------------------------------------------------Employees---------------------------------------------
CREATE TABLE Employees (
[EmployeeId] INT IDENTITY(1, 1) NOT NULL ,
[LastName] NVARCHAR(16) NULL,
[FirstName] NVARCHAR(16) NULL,
[MiddleName] NVARCHAR(16) NULL,
[DepartmentId] INT NULL,
[Address] NVARCHAR(128) NULL,
[MaritalStatusId]INT NULL,
[ImagePath] NVARCHAR(MAX) NULL,
[BeginningOfWork] DATE  NULL,
[EndOfWork] DATE NULL)
GO

--Создание первичного ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [pk_employees] PRIMARY KEY (EmployeeId);
GO

--Создание внешнего ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [fk_departments] FOREIGN KEY (DepartmentId) REFERENCES [Departments]([DepartmentId]) ON DELETE SET NULL ;
GO

--Создание внешнего ключа
ALTER TABLE [Employees]
ADD CONSTRAINT [fk_maritalStatus] FOREIGN KEY ([MaritalStatusId]) REFERENCES [MaritalStatus]([Id]) ON DELETE SET NULL ;
GO

---------------------------------------------------Phones-------------------------------------------------
CREATE TABLE [Phones](
[PhoneId] INT IDENTITY(1, 1) NOT NULL,
[Phone] NVARCHAR(16) NOT NULL,
[PhoneType] INT NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Phones]
ADD CONSTRAINT [pk_PhoneId] PRIMARY KEY([PhoneId]);
GO

--------------------------------------------------PhonesEmployees---------------------------------------
CREATE TABLE [PhonesEmployees](
[EmployeeId] INT NOT NULL,
[PhoneId] INT NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [pk_PhoneIdAndEmployeeId] PRIMARY KEY ([PhoneId]);
GO

--Создание уникальности
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [uq_PhoneIdAndEmployeeId] UNIQUE ([PhoneId]);
GO

--Создание внешнего ключа
ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [fk_PhonesEmploees_to_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId]);
GO

ALTER TABLE [PhonesEmployees]
ADD CONSTRAINT [fk_PhonesEmployees_to_Pnone] FOREIGN KEY ([PhoneId]) REFERENCES [Phones]([PhoneId]);
GO

-------------------------------------------------PhonesDepartment--------------------------------------
CREATE TABLE [PhonesDepartments](
[DepartmentId] INT NOT NULL,
[PhoneId] INT NOT NULL)
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
ADD CONSTRAINT [fk_PhonesDepartments_to_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([DepartmentId]) ON DELETE CASCADE;
GO

ALTER TABLE [PhonesDepartments]
ADD CONSTRAINT [fk_PhonesDepartments_to_Phones] FOREIGN KEY ([PhoneId]) REFERENCES [Phones]([PhoneId]) ON DELETE CASCADE;
GO

----------------------------------------------------Emails-----------------------------------------------
CREATE TABLE [Emails](
[EmailId] INT IDENTITY(1, 1) NOT NULL,
[EmployeeId] INT NOT NULL,
[Email] VARCHAR(32) NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Emails]
ADD CONSTRAINT [pk_Emails] PRIMARY KEY([EmailId]);
GO

--Создание внешнего ключа
ALTER TABLE [Emails]
ADD CONSTRAINT [fk_Emails_to_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId]);
GO


-----------------------------------------------------------------------------------------------------------
---------------------------------------------------FUNCTIONS-----------------------------------------------
-----------------------------------------------------------------------------------------------------------

---------------------------------------------GetPhonesByDepartmentId--------------------------------------------
CREATE FUNCTION [GetPhonesByDepartmentId](@DepartmentId INT)
RETURNS TABLE 
AS
RETURN(
SELECT [p].[PhoneId],
	   [Phone],
	   [PhoneType] 
	   FROM [PhonesDepartments] as PD
JOIN [Phones] as P
ON PD.PhoneId = P.PhoneId
WHERE PD.DepartmentId = @DepartmentId)
GO


-----------------------------------------------------------------------------------------------------------
---------------------------------------------------PROCRDURES----------------------------------------------
-----------------------------------------------------------------------------------------------------------


----------------------------------------------------PHONES-----------------------------------------------------


--------------------------------------------------InsertPhone--------------------------------------------------
CREATE PROCEDURE [InsertPhone]
@Phone NVARCHAR(16),
@PhoneType INT
AS
INSERT INTO [dbo].[Phones]
           ([Phone]
           ,[PhoneType])
     VALUES
           (@Phone,
			@PhoneType)
RETURN @@IDENTITY;
GO

--------------------------------------------------UpdatePhone-------------------------------------------------
CREATE PROCEDURE [UpdatePhone]
@PhoneId INT,
@Number nvarchar(16),
@PhoneType int
AS
UPDATE [Phones]
   SET [Phone] = @Number,
       [PhoneType] = @PhoneType
 WHERE PhoneId = @PhoneId
GO

------------------------------------------------DeletePhone---------------------------------------------------
CREATE PROCEDURE [DeletePhone]
@PhoneId varchar(16)
AS
DELETE [Phones]
WHERE PhoneId = @PhoneId;
GO


-----------------------------------------------------EMPLOYEE----------------------------------------------


--------------------------------------------------GetEmployeeById------------------------------------------
CREATE PROCEDURE [GetEmployeeById]
@EmployeeId INT
AS
Select [EmployeeId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningOfWork],
	   [EndOfWork],
	   [b].[StatusName] as [MaritalStatus] FROM Employees as a
JOIN MaritalStatus as b
ON a.MaritalStatusId = b.Id
WHERE EmployeeId = @EmployeeId

Select [Phone], [PhoneType] FROM Phones as p
JOIN PhonesEmployees as pe 
ON p.PhoneId = pe.PhoneId
WHERE pe.EmployeeId = @EmployeeId

Select Email FROM Emails
WHERE EmployeeId = @EmployeeId;
GO

-------------------------------------------------GetEmployeesByDepartmentId-------------------------------
CREATE PROCEDURE [GetEmployeesByDepartmentId]
@DepartmentId INT
AS
Select [EmployeeId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningOfWork],
	   [EndOfWork],
	   [b].[StatusName] FROM Employees as a
JOIN MaritalStatus as b
ON a.MaritalStatusId = b.Id
WHERE DepartmentId = @DepartmentId;
GO

----------------------------------------------------GetAllEmployees---------------------------------------
CREATE PROCEDURE [GetAllEmployees]
AS
Select [EmployeeId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningOfWork],
	   [EndOfWork],
	   [b].[StatusName] FROM Employees as a
JOIN MaritalStatus as b
ON a.MaritalStatusId = b.Id;

select a.EmployeeId, b.Phone, PhoneType From PhonesEmployees as a
JOIN Phones as b
On a.PhoneId = b.PhoneId;

select EmployeeId, Email FROM Emails;
GO

--------------------------------------------------GetAllSortEmployees-------------------------------------
CREATE PROCEDURE [GetAllSortEmployees]
AS
Select [EmployeeId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [a].[DepartmentId],
	   [b].[Name] as [DepartmentName],
	   (select TOP 1 [Phone] 
	   From Phones as p 
	   JOIN PhonesEmployees as e 
	   on p.PhoneId = e.PhoneId 
	   where e.EmployeeId = a.EmployeeId and p.PhoneType = 1 ) as WorkPhone
	    FROM Employees as a
		JOIN Departments as b
		ON a.DepartmentId = b.DepartmentId

GO

----------------------------------------------GetAllShortEmployeesByDepartmentId----------------------------------------
CREATE PROCEDURE [GetAllShortEmployeesByDepartmentId]
@DepartmentId INT
AS
Select [EmployeeId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [a].[DepartmentId],
	   [b].[Name] as [DepartmentName],
	   (select [Phone] 
	   From Phones as p 
	   JOIN PhonesEmployees as e 
	   on p.PhoneId = e.PhoneId 
	   where e.EmployeeId = a.EmployeeId and p.PhoneType = 1 ) as WorkPhone
	    FROM Employees as a
		JOIN Departments as b
		ON a.DepartmentId = b.DepartmentId
WHERE a.DepartmentId = @DepartmentId
GO

----------------------------------------------------AddEmployee--------------------------------------------
CREATE PROCEDURE [AddEmployee]
@LastName nvarchar(16) = null,
@FirstName nvarchar(16) = null,
@MiddleName nvarchar(16) = null,
@DepartmentId int = null,
@Address nvarchar(128) = null,
@ImagePath nvarchar(max) = null,
@BeginningOfWork date = null,
@EndOfWork date = null,
@MaritalStatusId int = null

AS
INSERT INTO [Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
           ,[ImagePath]
           ,[BeginningOfWork]
           ,[EndOfWork]
           ,[MaritalStatusId])
     VALUES
           (@LastName,
			@FirstName,
			@MiddleName,
			@DepartmentId,
			@Address,
			@ImagePath,
			@BeginningOfWork,
			@EndOfWork,
			@MaritalStatusId)
SELECT SCOPE_IDENTITY();
GO


----------------------------------------------------DEPARTMENT-------------------------------------------------


---------------------------------------------------GetAllDepartmts---------------------------------------------
CREATE PROCEDURE [GetAllDepartmts]
AS
SELECT [DepartmentId]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description]
  FROM [dbo].[Departments]

SELECT [DepartmentId], 
	   [Phone], 
	   [PhoneType] FROM Phones as a 
JOIN PhonesDepartments as b
ON a.PhoneId = b.PhoneId
GO

-------------------------------------------------GetDepartmentById---------------------------------------------
CREATE PROCEDURE [GetDepartmentById]
@DepartmentId INT
AS
SELECT [DepartmentId]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments] as dep
WHERE @DepartmentId = dep.DepartmentId

SELECT [PhoneId],
	   [Phone],
	   [PhoneType]  FROM [GetPhonesByDepartmentId](@DepartmentId);
GO

-------------------------------------------------InsertDepartment---------------------------------------------
CREATE PROCEDURE [InsertDepartment]
@parentId INT  = NULL,
@name VARCHAR(32),
@address VARCHAR (64) =  NULL,
@description VARCHAR(128) = NULL
AS
INSERT INTO [dbo].[Departments]
           ([ParentId]
           ,[Name]
           ,[Address]
           ,[Description])
     VALUES
			(@parentId,
			 @name,
			 @address,
			 @description)
 
SELECT SCOPE_IDENTITY();
GO

-------------------------------------------------UpdateDepartment---------------------------------------------
CREATE PROCEDURE [UpdateDepartment]
@DepartmentId INT,
@ParentId INT = NUll,
@Name nvarchar(32),
@Address nvarchar(64) = NULL,
@Description nvarchar(128) = NULL
AS
UPDATE [dbo].[Departments]
   SET [ParentID] = @ParentId
      ,[Name] = @Name
      ,[Address] =  @Address
      ,[Description] = @Description
 WHERE DepartmentId = @DepartmentId
GO

-----------------------------------------------DeleteDepartmentById------------------------------------------
CREATE PROCEDURE [DeleteDepartmentById]
@DepartmentId int
AS
DELETE Departments
WHERE DepartmentId = @DepartmentId
GO

-------------------------------------------InsertPhoneByDepartmentId--------------------------------------------
CREATE PROCEDURE [InsertPhoneByDepartmentId]
@DepartmetnId INT,
@Phone NVARCHAR(16),
@PhoneType INT
AS
BEGIN
DECLARE @PhoneID INT;

EXEC @PhoneID = InsertPhone @Phone,@PhoneType

INSERT INTO [dbo].[PhonesDepartments]
			([DepartmentId],
			 [PhoneId])
      VALUES
			(@DepartmetnId,
			 @PhoneID)
END
GO

--------------------------------------------UpdateDepartmentPhone----------------------------------------------
CREATE PROCEDURE [UpdateDepartmentPhone]
@DepartmentId INT,
@PhoneId INT = null,
@Number nvarchar(16) = null,
@PhoneType int = null
AS

if @Number is null
begin 
EXEC [DeletePhone] @PhoneId
end;

else if @PhoneId is null
begin
EXEC [InsertPhoneByDepartmentId] @DepartmentId, @Number, @PhoneType
end;

else
begin
EXEC [UpdatePhone] @PhoneId,@Number,@PhoneType
end;
GO


-------------------------------------------------------------------------------------------------------------
------------------------------------------INSERT DATA FOR TEST-----------------------------------------------
-------------------------------------------------------------------------------------------------------------

---------------------------------------------Add Phones------------------------------------------------------
INSERT INTO [Phones]
			([Phone],
		    [PhoneType])
VALUES
('8029698789814',0),
('8029698789898',2),
('8029698781684',1),
('8029661811614',0),
('8029698843114',1),
('8029687984513',2),
('8029684949492',0),
('8029683828688',2),
('8029677843131',1),
('8029661811614',0),
('8029698843114',1),
('8029687984513',1),
('8029684949492',1),
('8029683828688',2),
('8029677843131',1),
('8029683828688',2),
('8029677843131',1),
('8029661811614',1),
('8029698843114',1),
('8029687984513',1),
('8029684949492',1),
('8029683828688',1),
('8029677843131',1)
GO

---------------------------------------------Inser Departments-----------------------------------------------
INSERT INTO [dbo].[Departments]
           ([ParentID]
           ,[Name]
           ,[Address]
           ,[Description])
VALUES
(null,'Отдел кадров','ул.Еловая, д.10, оф.20','Отдел занимается кадрами'),
(null,'Отдел продаж','ул.Сосновая, д.23, оф.11','Отдел занимается продажами'),
(null,'Отдел рекламы','ул.Кедровая, д.13, оф.23','Отдел занимается рекламой'),
(null,'Отдел развития','ул.Вафельная, д.40, оф.20','Отдел занимается развитием'),
(null,'Отдел финансов','ул.Еловая, д.13, оф.20','Отдел занимается финансами'),
(null,'Отдел транспорта','ул.Кенова, д.10, оф.206','Отдел занимается транспортом')
GO

------------------------------------------PhonesDepartments--------------------------------------------------
INSERT INTO [dbo].[PhonesDepartments]
           ([DepartmentId]
           ,[PhoneId])
     VALUES
(1,1),
(2,2),
(2,3),
(3,4),
(3,5),
(4,6),
(4,7),
(5,8),
(6,9),
(6,10)

GO

---------------------------------------------Inser Employees-----------------------------------------------
INSERT INTO [dbo].[MaritalStatus]
           ([StatusName])
VALUES
('Single'),
('Married'),
('Widowed')

GO


---------------------------------------------Inser Employees-----------------------------------------------
INSERT INTO [dbo].[Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
           ,[MaritalStatusId]
           ,[ImagePath]
           ,[BeginningOfWork]
           ,[EndOfWork])
VALUES
('Федоров','Олег','Николаевич',1,'ул.Ветрова, д.10, кв.20',1,null,'2012-06-12',null),
('Курленков','Николай','Валерьевич',2,'ул.Николаева, д.23, кв.20',2,null,'2017-07-18',null),
('Авдеев','Евгений','Константинович',3,'ул.Утенева, д.14, кв.20',2,null,'2017-07-18','2018-01-18'),
('Хорламов','Степан','Степанович',4,'ул.Лохова, д.16, кв.20',2,null,'2015-03-18',null),
('Немиров','Федор','Николаевич',5,'ул.Брякова, д.25, кв.20',3,null,'2017-02-28',null),
('Шеренков','Василий','Андреевич',6,'ул.Куренкова, д.27, кв.20',1,null,'2016-01-12','2017-09-18'),
('Пупсов','Андрей','Григорьевич',1,'ул.Фаева, д.27, кв.34',2,null,'2016-07-01',null),
('Кенотов','Николай','Андреевич',4,'ул.Куренкова, д.27, кв.20',1,null,'2014-09-15',null)


GO




------------------------------------------PhonesEmployees----------------------------------------------------
INSERT INTO [dbo].[PhonesEmployees]
           ([EmployeeId]
           ,[PhoneId])
     VALUES
(1,11),
(2,12),
(2,13),
(3,14),
(3,15),
(4,16),
(4,17),
(5,18),
(6,19)

GO
-----------------------------------------------Emails-------------------------------------------------------
INSERT INTO [dbo].[Emails]
           ([EmployeeId]
           ,[Email])
     VALUES
(1,'gerdrt@mail.ru'),
(2,'uritrt@mail.ru'),
(2,'werefs@mail.ru'),
(3,'sfsdft@mail.ru'),
(3,'sdffst@mail.ru'),
(4,'vfvdfv@mail.ru'),
(4,'rvdvdf@mail.ru'),
(5,'dfvdct@mail.ru'),
(6,'gdfvdt@mail.ru')

GO







