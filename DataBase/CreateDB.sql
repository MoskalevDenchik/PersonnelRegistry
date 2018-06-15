
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

---------------------------------------------------WorkStatus--------------------------------------------
CREATE TABLE [WorkStatuses](
[Id] INT IDENTITY (1,1),
[Status] NVARCHAR(32) NOT NULL);
GO

--Создание первичного ключа
ALTER TABLE [WorkStatuses]
ADD CONSTRAINT [pk_workStatusId] PRIMARY KEY ([Id]);
GO

----------------------------------------------------KindPhone--------------------------------------------
CREATE TABLE [KindPhones](
[Id] INT IDENTITY(1,1),
[Kind] NVARCHAR(32) NOT NULL)
GO
--Создание первичного ключа
ALTER TABLE [KindPhones]
ADD CONSTRAINT [pk_kindPhoneId] PRIMARY KEY ([Id]);
GO

---------------------------------------------------Phones-------------------------------------------------
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

---------------------------------------------------MaritalStatus-----------------------------------------
CREATE TABLE [MaritalStatuses]
([Id] INT IDENTITY(1, 1) NOT NULL,
 [Status] NVARCHAR(16) NOT NULL)
 GO

 --Создание первичного ключа
ALTER TABLE [MaritalStatuses]
ADD CONSTRAINT [pk_maritalStatusId] PRIMARY KEY (Id);
GO


--------------------------------------------------Departments--------------------------------------------
CREATE TABLE [Departments](
[Id] INT IDENTITY(1, 1),
[ParentID] INT NULL,
[Name] NVARCHAR(32) NULL,
[Address] NVARCHAR(128) NULL,
[Description] NVARCHAR(128) NULL)
GO
--Создание первичного ключа
ALTER TABLE [Departments]
ADD CONSTRAINT [pk_departmentId] PRIMARY KEY ([Id]);
GO



----------------------------------------------------Employees---------------------------------------------
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


--------------------------------------------------PhonesEmployees---------------------------------------
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

-------------------------------------------------PhonesDepartment--------------------------------------
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

------------------------------------------------------Users----------------------------------------------
CREATE TABLE [Users](
[Id] INT IDENTITY (1,1),
[EmployeeId] INT NULL,
[Login] NVARCHAR(32) NOT NULL,
[Password] NVARCHAR(64) NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Users]
ADD CONSTRAINT [pk_UserId] PRIMARY KEY ([Id]);
GO

--Создание уникальности
ALTER TABLE [Users]
ADD CONSTRAINT [uq_UserId] UNIQUE ([EmployeeId]);
GO

--Создание внешнего ключа
ALTER TABLE [Users]
ADD CONSTRAINT [fk_UserId_to_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([Id]) ON DELETE CASCADE;
GO

------------------------------------------------------Roles----------------------------------------------
CREATE TABLE [Roles](
[Id] INT IDENTITY(1,1),
[Name] NVARCHAR(32) NOT NULL)
GO

--Создание первичного ключа
ALTER TABLE [Roles]
ADD CONSTRAINT [pk_RolesId] PRIMARY KEY ([Id]);
GO

---------------------------------------------------RolesUsers--------------------------------------------
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

----------------------------------------------------Emails-----------------------------------------------
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

-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------VIEWS-------------------------------------------------
-----------------------------------------------------------------------------------------------------------

-----------------------------------------------PhonesDepartmentView----------------------------------------
CREATE VIEW [PhonesDepartmentView] 
AS
SELECT [DepartmentId],
	   [P].[Id],
	   [Number],
	   [KindId],
	   [Kind]  FROM [PhonesDepartments]
  JOIN [Phones] as [P]
    ON [PhoneId] = [Id]
  JOIN [KindPhones] as [KP]
    ON [KP].[Id] = [P].[KindId];
GO

----------------------------------------------PhonesEmployeeView-------------------------------------------
CREATE VIEW [PhonesEmployeeView]
AS
SELECT [EmployeeId],
	   [P].[Id],
	   [Number],
	   [KindId],
	   [Kind]  FROM [PhonesEmployees]
  JOIN [Phones] as [P]
    ON [PhoneId] = [Id]
  JOIN [KindPhones] as [KP]
    ON [KP].[Id] = [P].[KindId];
GO



-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------TYPES-------------------------------------------------
-----------------------------------------------------------------------------------------------------------

---------------------------------------------------PhoneTable----------------------------------------------
CREATE Type [PhonesType] as TABLE(
[Id] INT null,
[Number] NVARCHAR(16) null,
[KindId] INT null)
GO

--------------------------------------------------EmailsTable----------------------------------------------
CREATE Type [EmailsType] as TABLE
([Id] INT NULL,
[Address] NVARCHAR(32) NULL)
GO

--------------------------------------------------RolesTable----------------------------------------------
CREATE Type [RolesType] as TABLE
([Id] INT NULL,
[Role] NVARCHAR(32) NULL)
GO



-----------------------------------------------------------------------------------------------------------
---------------------------------------------------TRIGGERS------------------------------------------------
-----------------------------------------------------------------------------------------------------------

--------------------------------------------InsertDepartmentPhonesTriger-----------------------------------

CREATE TRIGGER [InsertDepartmentPhonesTriger] 
ON [PhonesDepartmentView] INSTEAD OF INSERT 
AS 
BEGIN
declare @Ids table([Id] INT);
 
INSERT INTO [Phones](
			[Number],
			[KindId]) OUTPUT [inserted].[Id] into @Ids	
	 SELECT [Number],
	        [KindId] FROM inserted;

INSERT INTO [PhonesDepartments](
			[PhoneId],
			[DepartmentId])
	 SELECT [Id],
		    (SELECT TOP 1 [DepartmentId]  FROM [inserted]) FROM @Ids
END;
GO

--------------------------------------------InsertEmployeePhonesTriger-------------------------------------

CREATE TRIGGER [InsertEmployeePhonesTriger]
ON [PhonesEmployeeView] INSTEAD OF INSERT 
AS 
BEGIN
declare @Ids TABLE([Id] INT );
 
INSERT INTO [Phones](
			[Number],
			[KindId]) OUTPUT [inserted].[Id] into @Ids	
	 SELECT [Number],
			[KindId] FROM inserted;

INSERT INTO [PhonesEmployees](
			[PhoneId],
			[EmployeeId])
	 SELECT [Id],
		    (SELECT TOP 1 [EmployeeId]  FROM [inserted]) FROM @Ids
END;
GO

--------------------------------------------------------------------------------------------------------------
---------------------------------------------------FUNCTIONS--------------------------------------------------
--------------------------------------------------------------------------------------------------------------

---------------------------------------------GetEmployeesByPage-------------------------------------------

CREATE FUNCTION [AllEmployeesByPage](@PageSize INT,@Page INT)
RETURNS Table
AS
 return SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
	ORDER BY [Id]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO
---------------------------------------------GetEmployeesByPage-------------------------------------------

CREATE FUNCTION [GetDepartmentsByPage](@PageSize INT,@Page INT)
RETURNS Table
AS
RETURN SELECT [Id] as [DepartmentId] FROM [Departments]
	   ORDER BY [Id]
	   OFFSET @PageSize *( @Page - 1 ) ROWS
	   FETCH NEXT @PageSize ROWS ONLY ;
GO

---------------------------------------------GetPhoneByDepartmentId-------------------------------------------
CREATE FUNCTION [GetDepartmentPhonesById](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [Id],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView]
 WHERE [DepartmentId]= @Id);
GO

---------------------------------------------GetEmployeePhonesById--------------------------------------------
CREATE FUNCTION [GetEmployeePhonesById](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesEmployeeView]
 WHERE [EmployeeId]= @Id);
GO

---------------------------------------------GetPhoneByDepartmentId-------------------------------------------
CREATE FUNCTION [GetEmployeePhonesByDepartmentId](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [PE].[Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesEmployeeView] as [PE]
  JOIN [Employees] as [E]
    ON [E].[Id] = [EmployeeId]
WHERE [DepartmentId] = @Id);
GO 

---------------------------------------------GetPhoneByDepartmentId-------------------------------------------
CREATE FUNCTION [GetDepartmentPhonesByEmployeeId](@Id INT)
RETURNS TABLE 
AS
RETURN(
SELECT [PD].[Id],
	   [PD].[DepartmentId],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [PD]
  JOIN [Employees] as [E]
    ON [E].[DepartmentId] = [PD].[DepartmentId]
 WHERE [E].Id = @Id);
GO 
---------------------------------------------GetEmployeeById----------------------------------------
CREATE FUNCTION [GetEmployeeById](@Id INT)
RETURNS TABLE 
AS
RETURN(
Select [E].[Id],
	   [DepartmentId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MS].[Id] as [MaritalStatusId],
	   [MS].[Status] as [MaritalStatus],
	   [WS].[Id] as [WorkStatusId],
	   [WS].[Status] as [WorkStatus] FROM [Employees] as [E]
LEFT JOIN [MaritalStatuses] as [MS]
    ON [E].[MaritalStatusId] = [MS].[Id]
LEFT JOIN [WorkStatuses] as [WS]
    ON [E].[WorkStatusId] = [WS].[Id]
 WHERE [E].[Id] = @Id);
 GO

 ---------------------------------------------GetEmployeeBySerchParams----------------------------------------

CREATE FUNCTION [dbo].[GetEmployeeBySerchParams](
@LastName nvarchar(16) = '',
@FirstName nvarchar(16) = '',
@MiddleName nvarchar(16) = '',
@minTime INT,
@maxTime INT,
@WorkStatusId int)
RETURNS TABLE
AS
RETURN SELECT [Id] as [EmployeeId],
              [DepartmentId] FROM [Employees] 
		WHERE [LastName] LIKE @LastName + '%' 
		  AND [FirstName] LIKE @FirstName +'%'
		  AND [MiddleName] LIKE @MiddleName +'%'
		  AND DATEDIFF( YEAR, BeginningWork, ISNULL(EndWork,GETDATE())) BETWEEN @minTime and @maxTime
		  AND WorkStatusId = IIF(@WorkStatusId = 0, [WorkStatusId],@WorkStatusId);
GO

 ---------------------------------------------AllEmployeesByPage----------------------------------------
CREATE FUNCTION [dbo].[GetAllEmployeesByPage](@PageSize INT,@Page INT)
RETURNS Table
AS
 return SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
	ORDER BY [Id]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO

 ----------------------------------------GetPageEmployeesByDepartmentId--------------------------------
CREATE FUNCTION [dbo].[GetPageEmployeesByDepartmentId](@DepartmentId INT,@PageSize INT,@Page INT)
RETURNS Table
AS
 RETURN SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
    WHERE [DepartmentId] = @DepartmentId
	ORDER BY [Id]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO



---------------------------------------------------------------------------------------------------------------
---------------------------------------------------PROCRDURES--------------------------------------------------
---------------------------------------------------------------------------------------------------------------






----------------------------------------------------DEPARTMENT-------------------------------------------------

-------------------------------------------------GetAllDepartmts-----------------------------------------------
CREATE PROCEDURE [SelectAllDepartments]
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]

SELECT [Id], 
	   [DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] 
GO

-----------------------------------------------SelectAllDepartmtsByPage------------------------------------------
CREATE PROCEDURE [SelectAllDepartmtsByPage]
@PageSize INT,
@Page INT
AS
CREATE TABLE [#Searched]([DepartmentId] INT)
INSERT INTO  [#Searched] 
	  SELECT [DepartmentId] FROM dbo.GetDepartmentsByPage(@PageSize,@Page)

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [dbo].[Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId];

SELECT COUNT([Id]) as [Count] FROM [Departments];
GO

-------------------------------------------------SelectDepartmentById-----------------------------------------
CREATE PROCEDURE [SelectDepartmentById]
@Id INT
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
 WHERE @Id = [Id]

SELECT [DepartmentId],
	   [Id],
	   [Number],
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView]
WHERE [DepartmentId] = @Id;
GO

-------------------------------------------------SelectDepartmentByEmployeeId----------------------------------
CREATE PROCEDURE [SelectDepartmentByEmployeeId]
@Id INT
AS
SELECT [D].[Id],
	   [ParentID],
       [Name],
	   [D].[Address],
	   [Description] FROM [Departments] as [D]
LEFT JOIN [Employees] as [E]
ON [D].[Id] = DepartmentId
WHERE [E].[Id] = @Id;   
GO

-------------------------------------------------InsertDepartment---------------------------------------------
CREATE PROCEDURE [InsertDepartment]
@parentId INT  = NULL,
@name NVARCHAR(32),
@address NVARCHAR (64) =  NULL,
@description NVARCHAR(128) = NULL,
@Phones [PhonesType] READONLY
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
			 @description);
DECLARE @DepartmentId INT = @@IDENTITY;

INSERT INTO [PhonesDepartmentView]
			([DepartmentId],
			[Number],
		    [KindId])
     SELECT @DepartmentId,
	        [Number],
            [KindId] FROM @Phones;

SELECT SCOPE_IDENTITY();
GO
-----------------------------------------------UpdateDepartmentPhone-------------------------------------------
CREATE PROCEDURE [UpdateDepartmentPhones]
@DepartmentId INT,
@Phones [PhonesType] READONLY
AS
UPDATE [Phones]
   SET [Number] = [a].[Number],
	   [KindId] = [a].[KindId] FROM @Phones  as [a]
 WHERE [Phones].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Number] is NOT NULL;

DELETE [Phones]
 WHERE [Id] IN (SELECT [Id] From @Phones	
				WHERE [Number] is NULL);

INSERT INTO [PhonesDepartmentView](
			[DepartmentId],
			[Number],
		    [KindId])
	 SELECT @DepartmentId,
	        [Number],
            [KindId] FROM @Phones as [a] 
	  WHERE [a].[Id] = 0;
GO
-------------------------------------------------UpdateDepartment---------------------------------------------
CREATE PROCEDURE [UpdateDepartment]
@Id INT,
@ParentId INT = NUll,
@Name nvarchar(32),
@Address nvarchar(64) = NULL,
@Description nvarchar(128) = NULL,
@Phones [PhonesType] READONLY
AS
EXEC [UpdateDepartmentPhones] @Id, @Phones;

UPDATE [dbo].[Departments]
   SET [ParentID] = @ParentId
      ,[Name] = @Name
      ,[Address] =  @Address
      ,[Description] = @Description
 WHERE [Id] = @Id;
GO

-----------------------------------------------DeleteDepartmentById------------------------------------------
CREATE PROCEDURE [DeleteDepartmentById]
@Id INT
AS
DELETE FROM [dbo].[Phones] 
	  WHERE [Id] IN (SELECT [PhoneId] FROM [dbo].[PhonesDepartments]
					  WHERE [DepartmentId] = @Id);

DELETE FROM [dbo].[Departments]
	  WHERE [Id] = @Id;
GO


	
-----------------------------------------------------EMPLOYEE----------------------------------------------


--------------------------------------------------SelectEmployeeById------------------------------------------
CREATE PROCEDURE [SelectEmployeeById]
@Id INT
AS
Select [Id],
	   [DepartmentId],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MaritalStatusId],
	   [MaritalStatus],
	   [WorkStatusId],
	   [WorkStatus] FROM [GetEmployeeById](@Id)

SELECT [Id],
	   [EmployeeId],
	   [Number],
	   [KindId],
	   [Kind] FROM [GetEmployeePhonesById](@Id);

SELECT [Id],
	   [EmployeeId],
       [Address] FROM [Emails]
 WHERE [EmployeeId] = @Id;

  exec [SelectDepartmentByEmployeeId] @Id;

SELECT [Id],
	   [DepartmentId],
	   [Number],
	   [KindId],
	   [Kind] FROM [GetDepartmentPhonesByEmployeeId](@Id);

GO

-------------------------------------------------SelectEmployeesByDepartmentId-------------------------------
CREATE PROCEDURE [SelectPageEmployeesByDepartmentId]
@DepartmentId INT,
@PageSize INT,
@Page INT 
AS

IF @DepartmentId = 0
EXEC [SelectAllEmployees] @PageSize,@Page;

ELSE
BEGIN
CREATE TABLE [#Searched]([EmployeeId] INT,[DepartmentId] INT);
INSERT INTO  [#Searched] 
	  SELECT [EmployeeId], [DepartmentId] FROM [dbo].[GetPageEmployeesByDepartmentId](@DepartmentId,@PageSize,@Page)
	  WHERE [DepartmentId] = @DepartmentId;

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MS].[Status] as [MaritalStatus],
	   [MaritalStatusId],
	   [WS].[Status] as [WorkStatus],
	   [WorkStatusId]  FROM Employees as [E]
LEFT JOIN [MaritalStatuses] as [MS]
ON [E].[MaritalStatusId] =[Ms].[Id]
LEFT JOIN [WorkStatuses] as [WS]
ON [E].[WorkStatusId] =[WS].[Id]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [E].[Id]; 

SELECT [PE].[EmployeeId],
	   [PE].[Id],
	   [Number],
       [KindId],
	   [Kind] From [PhonesEmployeeView] as [PE]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [PE].[EmployeeId];

SELECT [Emails].[EmployeeId],
	   [Emails].[Id], 
	   [Emails].[Address] FROM [Emails]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [Emails].[EmployeeId];

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId]
	GROUP BY [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind];

SELECT COUNT([ID]) as [Count] FROM [Employees]
WHERE [DepartmentId] = @DepartmentId;
END;
GO

----------------------------------------------------SelectAllEmployees---------------------------------------
CREATE PROCEDURE [SelectAllEmployees]
@PageSize INT,
@Page INT 
AS

CREATE TABLE [#Searched](EmployeeId INT,DepartmentId INT);
INSERT INTO  [#Searched] 
	  SELECT [EmployeeId], [DepartmentId] FROM dbo.GetAllEmployeesByPage(@PageSize,@Page);

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MS].[Status] as [MaritalStatus],
	   [MaritalStatusId],
	   [WS].[Status] as [WorkStatus],
	   [WorkStatusId]  FROM Employees as [E]
LEFT JOIN [MaritalStatuses] as [MS]
ON [E].[MaritalStatusId] =[Ms].[Id]
LEFT JOIN [WorkStatuses] as [WS]
ON [E].[WorkStatusId] =[WS].[Id]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [E].[Id]; 

SELECT [PE].[EmployeeId],
	   [PE].[Id],
	   [Number],
       [KindId],
	   [Kind] From [PhonesEmployeeView] as [PE]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [PE].[EmployeeId];

SELECT [Emails].[EmployeeId],
	   [Emails].[Id], 
	   [Emails].[Address] FROM [Emails]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [Emails].[EmployeeId];

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId]
	GROUP BY [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind];

SELECT COUNT([ID]) as [Count] FROM [Employees];
GO


----------------------------------------------------SelectPageEmployeesBySearchParams--------------------------------------------

CREATE PROCEDURE [SelectPageEmployeesBySearchParams]
@LastName nvarchar(16) = '',
@FirstName nvarchar(16) = '',
@MiddleName nvarchar(16) = '',
@fromYear INT,
@toYear INT,
@WorkStatusId int,
@PageSize INT,
@Page INT
AS

CREATE TABLE [#Searched]([EmployeeId] INT,[DepartmentId] INT);
INSERT INTO  [#Searched] 
SELECT * FROM dbo.GetEmployeeBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId)
    ORDER BY [EmployeeId]
	OFFSET @PageSize *( @Page - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HasRole],
	   [MS].[Status] as [MaritalStatus],
	   [MaritalStatusId],
	   [WS].[Status] as [WorkStatus],
	   [WorkStatusId]  FROM Employees as [E]
LEFT JOIN [MaritalStatuses] as [MS]
ON [E].[MaritalStatusId] =[Ms].[Id]
LEFT JOIN [WorkStatuses] as [WS]
ON [E].[WorkStatusId] =[WS].[Id]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [E].[Id];  

SELECT [PE].[EmployeeId],
	   [PE].[Id],
	   [Number],
       [KindId],
	   [Kind] From [PhonesEmployeeView] as [PE]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [PE].[EmployeeId];

SELECT [Emails].[EmployeeId],
	   [Emails].[Id], 
	   [Emails].[Address] FROM [Emails]
  JOIN [#Searched] as [S]
ON [S].[EmployeeId] = [Emails].[EmployeeId];

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId]
	GROUP BY [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind] FROM [PhonesDepartmentView] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number], 
	   [KindId],
	   [Kind];

SELECT COUNT(*) as [Count] FROM dbo.GetEmployeeBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId);

GO

----------------------------------------------------InsertEmployee--------------------------------------------
CREATE PROCEDURE [InsertEmployee]
@LastName nvarchar(16) = null,
@FirstName nvarchar(16) = null,
@MiddleName nvarchar(16) = null,
@DepartmentId int = null,
@Address nvarchar(128) = null,
@ImagePath nvarchar(max) = null,
@BeginningWork date = null,
@EndWork date = null,
@WorkStatusId INT = null,  
@MaritalStatusId int = null,
@Phones [PhonesType] READONLY,
@Emails [EmailsType] READONLY
AS
INSERT INTO [Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
           ,[ImagePath]
           ,[BeginningWork]
           ,[EndWork]
           ,[MaritalStatusId]
		   ,[WorkStatusId])
     VALUES
           (@LastName,
			@FirstName,
			@MiddleName,
			@DepartmentId,
			@Address,
			@ImagePath,
			@BeginningWork,
			@EndWork,
			@MaritalStatusId,
			@WorkStatusId);

DECLARE @Id INT = @@IDENTITY;

INSERT INTO [PhonesEmployeeView](
		    [EmployeeId],
			[Number],
		    [KindId])
	SELECT  @Id,
			[Number],
			[KindId] FROM @Phones;

INSERT INTO [dbo].[Emails](
			[EmployeeId],
			[Address])
	SELECT  @Id,
			[Address] FROM @Emails;
GO

---------------------------------------------UpdateEmployeePhone---------------------------------------------
CREATE PROCEDURE [UpdateEmployeePhones]
@EmployeeId INT,
@Phones [PhonesType] READONLY
AS
UPDATE [Phones]
   SET [Number] = [a].[Number],
	   [KindId] = [a].[KindId] FROM @Phones  as [a]
 WHERE [Phones].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Number] is NOT NULL;

DELETE [Phones]
 WHERE [Id] IN (SELECT [Id] From @Phones	
				WHERE [Number] is NULL);

INSERT INTO [PhonesEmployeeView](
			[EmployeeId],
			[Number],
		    [KindId])
	 SELECT @EmployeeId,
	        [Number],
            [KindId] FROM @Phones as [a] 
	  WHERE [a].[Id] = 0 ;
GO

---------------------------------------------UpdateEmployeeEmail---------------------------------------------
CREATE PROCEDURE [UpdateEmployeeEmails]
@EmployeeId INT,
@Emails [EmailsType] READONLY
AS
UPDATE [Emails]
   SET [Address] = [a].[Address] FROM @Emails as[a]
 WHERE [Emails].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Address] is NOT NULL;

DELETE [Emails]
 WHERE [Id] IN (SELECT [Id] From @Emails	
				WHERE [Address] is NULL);

INSERT INTO [Emails](
			[Address])
	 SELECT [Address] FROM @Emails as [a]
	  WHERE [a].[Id] = 0;
GO
-------------------------------------------------UpdateEmployee---------------------------------------------
CREATE PROCEDURE [UpdateEmployee]
@Id INT,
@LastName nvarchar(16) = null,
@FirstName nvarchar(16) = null,
@MiddleName nvarchar(16) = null,
@DepartmentId int = null,
@Address nvarchar(128) = null,
@ImagePath nvarchar(max) = null,
@BeginningWork date = null,
@EndWork date = null,
@MaritalStatusId int = null,
@WorkStatusId int = null,
@Phones [PhonesType] READONLY,
@Emails [EmailsType] READONLY
AS
EXEC [UpdateEmployeePhones] @Id, @Phones;
EXEC [UpdateEmployeeEmails] @Id, @Emails;

UPDATE [Employees]
      SET [LastName] = @LastName,
          [FirstName] = @FirstName,
          [MiddleName] = @MiddleName,
          [DepartmentId] = @DepartmentId,
          [Address] = @Address,
          [ImagePath] = @ImagePath,
          [BeginningWork] = @BeginningWork,
          [EndWork] = @EndWork,
          [MaritalStatusId] = @MaritalStatusId,
		  [WorkStatusId] = @WorkStatusId
	WHERE [Id] = @Id;
GO

-----------------------------------------------DeleteEmployeeById------------------------------------------
CREATE PROCEDURE [DeleteEmployeeById]
@Id INT
AS
DELETE FROM [dbo].[Phones] 
	  WHERE [Id] IN (SELECT [PhoneId] FROM [dbo].[PhonesEmployees]
					  WHERE [EmployeeId] = @Id);
					  
DELETE FROM [dbo].[Emails]
      WHERE [EmployeeId] = @id;  

DELETE FROM [dbo].[Employees]
	  WHERE [Id] = @Id;
GO

--------------------------------------------------KINDPHONES-------------------------------------------------


---------------------------------------------GetAllKindPhones------------------------------------------------
CREATE PROCEDURE [SelectAllKindPhones]
AS
SELECT [Id],
	   [Kind] FROM [KindPhones];
GO

-----------------------------------------------SelectKindPhoneById--------------------------------------------
CREATE PROCEDURE [SelectKindPhoneById]
@Id INT
AS
SELECT [Id],
	   [Kind] FROM [KindPhones]
WHERE [Id] = @Id;
GO
------------------------------------------------InsertKindPhone--------------------------------------------
CREATE PROCEDURE [InsertKindPhone]
@Kind NVARCHAR(32)
AS
INSERT INTO [KindPhones]
VALUES (@Kind);
GO

------------------------------------------------UpdateKindPhone--------------------------------------------
CREATE PROCEDURE [UpdateKindPhone]
@Id INT,
@Kind NVARCHAR(32)
AS
UPDATE [KindPhones]
SET [Kind] = @Kind  
WHERE Id = @Id;
GO

------------------------------------------------DeleteKindPhone--------------------------------------------
CREATE PROCEDURE [DeleteKindPhone]
@Id INT
AS
DELETE  [KindPhones]
WHERE [Id] = @id;
GO


------------------------------------------------MARITALSTATUS-------------------------------------------------


---------------------------------------------GetAllMaritalStatuses--------------------------------------------
CREATE PROCEDURE [GetAllMaritalStatuses]
AS
SELECT [Id],
	   [Status] FROM [MaritalStatuses];
GO

-----------------------------------------------GetMaritalStatusById--------------------------------------------
CREATE PROCEDURE [GetMaritalStatusById]
@Id INT
AS
SELECT [Id],
	   [Status] FROM MaritalStatuses
WHERE [Id] = @Id;
GO

------------------------------------------------InsertMaritalStatus--------------------------------------------
CREATE PROCEDURE [InsertMaritalStatus]
@Status NVARCHAR(32)
AS
INSERT INTO [MaritalStatuses]
VALUES (@Status);
GO

------------------------------------------------UpdateMaritalStatus--------------------------------------------
CREATE PROCEDURE [UpdateMaritalStatus]
@Id INT,
@Status NVARCHAR(32)
AS
UPDATE [MaritalStatuses]
SET [Status] = @Status  
WHERE Id = @Id;
GO

------------------------------------------------DeleteMaritalStatus--------------------------------------------
CREATE PROCEDURE [DeleteMaritalStatus]
@Id INT
AS
DELETE  [MaritalStatuses]
WHERE [Id] = @id;
GO

------------------------------------------------MARITALSTATUS-------------------------------------------------


---------------------------------------------SelectAllWorkStatus--------------------------------------------
CREATE PROCEDURE [SelectAllWorkStatus]
AS
SELECT [Id],
	   [Status] FROM [WorkStatuses];
GO

-----------------------------------------------SelectWorkStatusById--------------------------------------------
CREATE PROCEDURE [SelectWorkStatusById]
@Id INT
AS
SELECT [Id],
	   [Status] FROM [WorkStatuses]
WHERE [Id] = @Id;
GO

------------------------------------------------InsertWorkStatus--------------------------------------------
CREATE PROCEDURE [InsertWorkStatus]
@Status NVARCHAR(32)
AS
INSERT INTO [WorkStatuses]
VALUES (@Status);
GO

------------------------------------------------UpdateWorkStatus--------------------------------------------
CREATE PROCEDURE [UpdateWorkStatus]
@Id INT,
@Status NVARCHAR(32)
AS
UPDATE [WorkStatuses]
SET [Status] = @Status  
WHERE Id = @Id;
GO

------------------------------------------------DeleteWorkStatus------------------------------------------
CREATE PROCEDURE [DeleteWorkStatus]
@Id INT
AS
DELETE  [WorkStatuses]
WHERE [Id] = @id;
GO

---------------------------------------------------Users--------------------------------------------------

---------------------------------------------SelectAllUsers-----------------------------------------------
CREATE PROCEDURE [SelectAllUsers]
AS
SELECT [U].[Id],
	   [U].[EmployeeId],
	   [Login],
	   [Password] FROM [Users] as [U]
JOIN [Employees] as [E]
ON [E].[Id] = [U].[EmployeeId];

SELECT  [U].[Id] as [UserId],
        [Emails].[Id] as [Id],
		[Address] FROM [Emails]
JOIN [Users] as [U]
ON [U].[EmployeeId] = [Emails].[EmployeeId];


SELECT [U].[Id] as [UserId],
	   [R].[Id], 
       [R].[Name] FROM [Roles] as [R]
JOIN [RolesUsers] as [RU]
ON [R].[Id] = [RoleId]
JOIN [Users] as [U]
ON [U].[Id] = [UserId];
GO


---------------------------------------------SelectUserByLogin-----------------------------------------------
CREATE PROCEDURE [SelectUserByLogin]
@Login NVARCHAR(32)
AS
SELECT [U].[Id],
	   [U].[EmployeeId],
	   [Login],
	   [Password] FROM [Users] as [U]
JOIN [Employees] as [E]
ON [E].[Id] = [U].[EmployeeId]
WHERE [U].[Login] = @Login;

SELECT  [U].[Id] as [UserId],
        [Emails].[Id],
		[Address] FROM [Emails]
JOIN [Users] as [U]
ON [U].[EmployeeId] = [Emails].[EmployeeId]
WHERE [U].[Login] = @Login;


SELECT [U].[Id] as [UserId],
	   [R].[Id], 
       [R].[Name] FROM [Roles] as [R]
JOIN [RolesUsers] as [RU]
ON [R].[Id] = [RoleId]
JOIN [Users] as [U]
ON [U].[Id] = [UserId]
WHERE [U].[Login] = @Login;
GO

---------------------------------------------SelectUserById--------------------------------------------------
CREATE PROCEDURE [SelectUserById]
@Id INT
AS
SELECT [U].[Id],
	   [U].[EmployeeId],
	   [Login],
	   [Password] FROM [Users] as [U]
JOIN [Employees] as [E]
ON [E].[Id] = [U].[EmployeeId]
WHERE [U].[Id] = @Id;

SELECT  [U].[Id] as [UserId],
        [Emails].[Id] as [Id],
		[Address] FROM [Emails]
JOIN [Users] as [U]
ON [U].[EmployeeId] = [Emails].[EmployeeId]
WHERE [U].[Id] = @Id;


SELECT [U].[Id] as [UserId],
	   [R].[Id], 
       [R].[Name] FROM [Roles] as [R]
JOIN [RolesUsers] as [RU]
ON [R].[Id] = [RoleId]
JOIN [Users] as [U]
ON [U].[Id] = [UserId]
WHERE [U].[Id] = @Id;
GO

---------------------------------------------InsertUser--------------------------------------------------
CREATE PROCEDURE [InsertUser]
@EmployeeId INT,
@Login NVARCHAR(64),
@Password NVARCHAR(64),
@Roles [RolesType] READONLY 
AS
INSERT INTO [Users](
			[EmployeeId],
			[Login],
			[Password])
	VALUES
			(@EmployeeId,
			 @Login,
			 @Password);
DECLARE @UserId INT = @@IDENTITY;

UPDATE [Employees]
SET [HasRole] = 1
WHERE [Id] = @EmployeeId;
 

INSERT INTO [RolesUsers](
			[RoleId],
			[UserId])
     SELECT [Id], 
	        @UserId FROM @Roles;
GO

---------------------------------------------DeleteUser--------------------------------------------------
CREATE PROCEDURE [DeleteUser]
@Id INT
AS 
UPDATE [Employees]
SET [HasRole] = 0
WHERE [Id] = (SELECT [EmployeeId] FROM [Users] WHERE [Id] = @Id);
 
DELETE [Users]
WHERE [Id] = @Id;

GO
 


-----------------------------------------------Roles-------------------------------------------------------

---------------------------------------------SelectAllRoles------------------------------------------------

CREATE PROCEDURE [SelectAllRoles]
AS
SELECT [Id],
       [Name] FROM [Roles];
GO




-------------------------------------------------------------------------------------------------------------
------------------------------------------INSERT DATA FOR TEST-----------------------------------------------
-------------------------------------------------------------------------------------------------------------

---------------------------------------------Add Roles------------------------------------------------------
INSERT INTO [Roles](
			[Name])
VALUES 
('admin'),
('editor');
GO



---------------------------------------------Add KindPhone------------------------------------------------------
INSERT INTO [WorkStatuses]
		    ([Status])
VALUES
('Работает'),
('Отпуск'),
('Командирован'),
('Уволен');
GO

---------------------------------------------Add KindPhone------------------------------------------------------
INSERT INTO [KindPhones]
		    ([Kind])
VALUES
('Домашний'),
('Мобильный'),
('Рабочий');
GO

---------------------------------------------MaritalStatus-----------------------------------------------
INSERT INTO [dbo].[MaritalStatuses]
           ([Status])
VALUES
('Холост'),
('Женат'),
('Вдовец');
GO


------------------------------------------------Add Phones------------------------------------------------------
INSERT INTO [dbo].[Phones](
			[Number],
		    [KindId])
VALUES
('8029698789814',1),
('8029698789898',2),
('8029698781684',3),
('8029661811614',1),
('8029698843114',1),
('8029687984513',2),
('8029684949492',3),
('8029683828688',2),
('8029677843131',1),
('8029661811614',3),
('8029698843114',2),
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
('8029677843131',1);
GO

---------------------------------------------Inser Departments-----------------------------------------------
INSERT INTO [dbo].[Departments](
			[ParentID],
		    [Name],
            [Address],
            [Description])
VALUES
(null,'Отдел кадров','ул.Еловая, д.10, оф.20','Отдел занимается кадрами'),
(1,'Отдел продаж','ул.Сосновая, д.23, оф.11','Отдел занимается продажами'),
(null,'Отдел рекламы','ул.Кедровая, д.13, оф.23','Отдел занимается рекламой'),
(null,'Отдел развития','ул.Вафельная, д.40, оф.20','Отдел занимается развитием'),
(1,'Отдел финансов','ул.Еловая, д.13, оф.20','Отдел занимается финансами'),
(null,'Отдел транспорта','ул.Кенова, д.10, оф.206','Отдел занимается транспортом');
GO

------------------------------------------PhonesDepartments--------------------------------------------------
INSERT INTO [dbo].[PhonesDepartments](
			[DepartmentId],
			[PhoneId])
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
(6,10);
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
           ,[BeginningWork]
           ,[EndWork]
		   ,[WorkStatusId]
		   ,[HasRole])
VALUES
('Федоров','Олег','Николаевич',1,'ул.Ветрова, д.10, кв.20',1,'/Content/Images/UserImg.jpg','2012-06-12',null,1,1),
('Курленков','Николай','Валерьевич',2,'ул.Николаева, д.23, кв.20',2,'/Content/Images/UserImg.jpg','2017-07-18',null,1,1),
('Авдеев','Евгений','Константинович',3,'ул.Утенева, д.14, кв.20',2,'/Content/Images/UserImg.jpg','2017-07-18','2018-01-18',1,1),
('Хорламов','Степан','Степанович',4,'ул.Лохова, д.16, кв.20',2,'/Content/Images/UserImg.jpg','2015-03-18',null,1,0),
('Немиров','Федор','Николаевич',5,'ул.Брякова, д.25, кв.20',3,'/Content/Images/UserImg.jpg','2017-02-28',null,1,0),
('Шеренков','Василий','Андреевич',6,'ул.Куренкова, д.27, кв.20',1,'/Content/Images/UserImg.jpg','2016-01-12','2017-09-18',1,0),
('Пупсов','Андрей','Григорьевич',1,'ул.Фаева, д.27, кв.34',2,'/Content/Images/UserImg.jpg','2016-07-01',null,1,0),
('Кенотов','Николай','Андреевич',4,'ул.Куренкова, д.27, кв.20',1,'/Content/Images/UserImg.jpg','2014-09-15',null,1,0);
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
(6,19);
GO
-----------------------------------------------Emails-------------------------------------------------------
INSERT INTO [dbo].[Emails]
           ([EmployeeId]
           ,[Address])
     VALUES
(1,'gerdrt@mail.ru'),
(2,'uritrt@mail.ru'),
(2,'werefs@mail.ru'),
(3,'sfsdft@mail.ru'),
(3,'sdffst@mail.ru'),
(4,'vfvdfv@mail.ru'),
(4,'rvdvdf@mail.ru'),
(5,'dfvdct@mail.ru'),
(6,'gdfvdt@mail.ru');
GO

---------------------------------------------Add Users------------------------------------------------------
INSERT INTO [Users](
            [EmployeeId],
			[Login],
			[Password])
VALUES 
(1,'Turbo','1234'),
(2,'Den', '1234'),
(3,'Red','1234');
GO

---------------------------------------------Add LoginesRoles------------------------------------------------------
INSERT INTO [RolesUsers](
			[UserId],
			[RoleId])
VALUES 
(1,1),
(2,2),
(3,2);
GO

