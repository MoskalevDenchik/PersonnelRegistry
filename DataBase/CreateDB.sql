
USE [master]
GO

------------------------------------------------DataBase-----------------------------------------------
CREATE DATABASE [PR_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PR_DB',
  FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PR_DB.mdf' , 
  SIZE = 8192KB , 
  MAXSIZE = UNLIMITED, 
  FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PR_DB_log',
  FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PR_DB_log.ldf' ,
  SIZE = 8192KB , 
  MAXSIZE = 2048GB ,
  FILEGROWTH = 65536KB );
GO

USE [PR_DB]
GO

CREATE LOGIN PR_user   
    WITH PASSWORD = '1234';  
GO  
  
CREATE USER PR_user FOR LOGIN PR_user
Go
ALTER ROLE [db_owner] ADD MEMBER [PR_user] 
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

---------------------------------------------------MaritalStatus-----------------------------------------
CREATE TABLE [MaritalStatuses]
([Id] INT IDENTITY(1, 1),
 [Status] NVARCHAR(16) NOT NULL)
 GO

 --Создание первичного ключа
ALTER TABLE [MaritalStatuses]
ADD CONSTRAINT [pk_maritalStatusId] PRIMARY KEY (Id);
GO

--------------------------------------------------Departments--------------------------------------------
CREATE TABLE [Departments](
[Id] INT IDENTITY(1, 1),
[ParentID] INT,
[Name] NVARCHAR(16),
[Address] NVARCHAR(64) NULL,
[Description] NVARCHAR(128) NULL)
GO
--Создание первичного ключа
ALTER TABLE [Departments]
ADD CONSTRAINT [pk_departmentId] PRIMARY KEY ([Id]);
GO
---------------------------------------------------DepartmentPhones-------------------------------------------------
CREATE TABLE [DepartmentPhones](
[Id] INT IDENTITY(1, 1),
[DepartmentId] INT,
[Number] NVARCHAR(16));
GO

--Создание первичного ключа
ALTER TABLE [DepartmentPhones]
ADD CONSTRAINT [pk_PhoneId] PRIMARY KEY([Id]);
GO

--Создание внешнего ключа
ALTER TABLE [DepartmentPhones]
ADD CONSTRAINT [fk_Phones_to_DepartmentsId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([Id]) ON DELETE CASCADE;
GO

----------------------------------------------------Employees---------------------------------------------
CREATE TABLE [Employees](
[Id] INT IDENTITY(1, 1),
[DepartmentId] INT NULL ,
[LastName] NVARCHAR(16) NULL,
[FirstName] NVARCHAR(16) NULL,
[MiddleName] NVARCHAR(16) NULL,
[Address] NVARCHAR(64) NULL,
[MaritalStatusId]INT NULL,
[HomePhone] NVARCHAR(16) NULL,
[WorkPhone] NVARCHAR(16) NULL,
[MobilePhone] NVARCHAR(16) NULL,
[ImagePath] NVARCHAR(MAX) NULL,
[BeginningWork] DATETIME  NULL,
[EndWork] DATETIME NULL,
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

------------------------------------------------------Users----------------------------------------------
CREATE TABLE [Users](
[Id] INT IDENTITY (1,1),
[EmployeeId] INT NULL,
[Login] NVARCHAR(16) NOT NULL,
[Password] NVARCHAR(16) NOT NULL)
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
[Name] NVARCHAR(32))
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
[EmployeeId] INT,
[Address] VARCHAR(32))
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
-----------------------------------------------------TYPES-------------------------------------------------
-----------------------------------------------------------------------------------------------------------

---------------------------------------------------PhoneTable----------------------------------------------
CREATE Type [PhonesType] as TABLE(
[Id] INT NULL,
[Number] NVARCHAR(16) NULL);
GO

--------------------------------------------------EmailsTable----------------------------------------------
CREATE Type [EmailsType] as TABLE
([Id] INT NULL,
[Address] NVARCHAR(16) NULL)
GO

--------------------------------------------------RolesTable----------------------------------------------
CREATE Type [RolesType] as TABLE
([Id] INT NULL,
[Name] NVARCHAR(16) NULL)
GO

--------------------------------------------------------------------------------------------------------------
---------------------------------------------------FUNCTIONS--------------------------------------------------
--------------------------------------------------------------------------------------------------------------

---------------------------------------------GetEmployeesByPage-------------------------------------------

CREATE FUNCTION [GetEmployeesByPage](@PageSize INT,@PageNumber INT)
RETURNS TABLE
AS
 RETURN SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
	  ORDER BY [Id]
	OFFSET @PageSize *( @PageNumber - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO
---------------------------------------------GetDepartmentsByPage-------------------------------------------

CREATE FUNCTION [GetDepartmentsByPage](@PageSize INT,@PageNumber INT)
RETURNS Table
AS
RETURN SELECT [Id] as [DepartmentId] FROM [Departments]
	 ORDER BY [Id]
	   OFFSET @PageSize *( @PageNumber - 1 ) ROWS
   FETCH NEXT @PageSize ROWS ONLY ;
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
	   [WorkPhone],
	   [HomePhone],
	   [MobilePhone],
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

 ---------------------------------------------GetEmployeesBySerchParams----------------------------------------

CREATE FUNCTION [dbo].[GetEmployeesBySerchParams](
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



 ----------------------------------------GetPageEmployeesByDepartmentId--------------------------------
CREATE FUNCTION [dbo].[GetPageEmployeesByDepartmentId](@DepartmentId INT,@PageSize INT,@PageNumber INT)
RETURNS Table
AS
 RETURN SELECT [Id] as [EmployeeId],
			   [DepartmentId] FROM [Employees]
    WHERE [DepartmentId] = @DepartmentId
	ORDER BY [Id]
	OFFSET @PageSize *( @PageNumber - 1 ) ROWS
	FETCH NEXT @PageSize ROWS ONLY ;
GO

---------------------------------------------GetUserIdByEmplyeeId----------------------------------------

CREATE FUNCTION [dbo].[GetUserIdByEmplyeeId](@EmployeeId INT)
RETURNS INT 
AS
BEGIN 
RETURN (SELECT TOP 1[Id] FROM [Users] WHERE [EmployeeId] = @EmployeeId)
END;
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
	   [Number] FROM [DepartmentPhones]
GO

-----------------------------------------------SelectAllDepartmtsByPage------------------------------------------
CREATE PROCEDURE [SelectPageDepartmts]
@PageSize INT,
@PageNumber INT
AS
CREATE TABLE [#Searched]([DepartmentId] INT)
INSERT INTO  [#Searched] 
	  SELECT [DepartmentId] FROM dbo.GetDepartmentsByPage(@PageSize,@PageNumber)

SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [dbo].[Departments]
  JOIN [#Searched]
    ON [Id] =[DepartmentId];

SELECT [Id], 
	   [P].[DepartmentId],
	   [Number] FROM [DepartmentPhones] as [P]
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
	   [Number] FROM [DepartmentPhones]
WHERE [DepartmentId] = @Id;
GO

-------------------------------------------------SelectDepartmentByName-----------------------------------------
CREATE PROCEDURE [SelectDepartmentByName]
@Name NVARCHAR(16)
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
 WHERE @Name = [Name]

SELECT [DepartmentId],
	   [D].[Id],
	   [Number] FROM [DepartmentPhones] as [DP]
JOIN [Departments] as [D]
ON [D].[Id] = [DP].[DepartmentId]
WHERE [D].[Name] = @Name;
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

SELECT [DP].[DepartmentId],
	   [DP].[Id],
	   [Number] FROM [DepartmentPhones] as [DP]
  JOIN [Departments] as [D]
  ON [DP].[DepartmentId] = [D].[Id]
  LEFT JOIN [Employees] as [E]
  ON [D].[Id] = [E].[DepartmentId]
WHERE [E].[Id] = @Id;   
GO
-------------------------------------------------SelectDepartmentByParentId----------------------------------
CREATE PROCEDURE [SelectDepartmentByParentId]
@ParentId INT
AS
SELECT [Id]
      ,[ParentID]
      ,[Name]
      ,[Address]
      ,[Description] FROM [Departments]
 WHERE @ParentId = [ParentID]

SELECT [DepartmentId],
	   [DP].[Id],
	   [Number] FROM [DepartmentPhones] as [DP]
  JOIN [Departments] as [D]
    ON [DepartmentId] = [D].[Id]
 WHERE [ParentID] = @ParentId;
GO
-------------------------------------------------SaveDepartment---------------------------------------------
CREATE PROCEDURE [SaveDepartment]
@Id INT,
@parentId INT,
@name NVARCHAR(32),
@address NVARCHAR (64),
@description NVARCHAR(128),
@Phones [PhonesType] READONLY
AS
IF @Id=0
EXEC [InsertDepartment] @ParentId,@Name,@Address,@description,@Phones;
ELSE
EXEC [UpdateDepartment] @Id,@ParentId,@Name,@Address,@description,@Phones;
GO

-------------------------------------------------InsertDepartment---------------------------------------------
CREATE PROCEDURE [InsertDepartment]
@parentId INT,
@name NVARCHAR(32),
@address NVARCHAR (64),
@description NVARCHAR(128),
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

INSERT INTO [DepartmentPhones]
			([DepartmentId],
			[Number])
     SELECT @DepartmentId,
	        [Number] FROM @Phones;

SELECT SCOPE_IDENTITY();
GO

---------------------------------------------UpdateEmployeeEmail---------------------------------------------
CREATE PROCEDURE [UpdateDepartmentPhones]
@EmployeeId INT,
@Phones [PhonesType] READONLY
AS
UPDATE [DepartmentPhones]
   SET [Number] = [a].[Number] FROM @Phones as[a]
 WHERE [DepartmentPhones].[Id] = [a].[Id] and [a].[Id] != 0 and [a].[Number] is NOT NULL;

DELETE [DepartmentPhones]
 WHERE [Id] IN (SELECT [Id] From @Phones	
				WHERE [Number] is NULL);

INSERT INTO [DepartmentPhones](
			[Number])
	 SELECT [Number] FROM @Phones as [a]
	  WHERE [a].[Id] = 0;
GO
-------------------------------------------------UpdateDepartment---------------------------------------------
CREATE PROCEDURE [UpdateDepartment]
@Id INT,
@ParentId INT,
@Name nvarchar(32),
@Address nvarchar(64),
@Description nvarchar(128),
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

-----------------------------------------------DeleteDepartment------------------------------------------
CREATE PROCEDURE [DeleteDepartment]
@Id INT
AS
DELETE FROM [dbo].[DepartmentPhones] 
	  WHERE [DepartmentId] = @Id;

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
	   [HomePhone],
	   [WorkPhone],
	   [MobilePhone],
	   [MaritalStatusId],
	   [MaritalStatus],
	   [WorkStatusId],
	   [WorkStatus] FROM [GetEmployeeById](@Id)

SELECT [Id],
	   [EmployeeId],
       [Address] FROM [Emails]
 WHERE [EmployeeId] = @Id;

  exec [SelectDepartmentByEmployeeId] @Id;


GO

-------------------------------------------------SelectEmployeesByDepartmentId-------------------------------
CREATE PROCEDURE [SelectPageEmployeesByDepartmentId]
@DepartmentId INT,
@PageSize INT,
@PageNumber INT 
AS

IF @DepartmentId = 0
EXEC [SelectPageEmployees] @PageSize,@PageNumber;

ELSE
BEGIN
CREATE TABLE [#Searched]([EmployeeId] INT,[DepartmentId] INT);
INSERT INTO  [#Searched] 
	  SELECT [EmployeeId], [DepartmentId] FROM [dbo].[GetPageEmployeesByDepartmentId](@DepartmentId,@PageSize,@PageNumber)
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
	   [HomePhone],
	   [WorkPhone],
	   [MobilePhone],
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
	   [Number] FROM [DepartmentPhones] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number]

SELECT COUNT([ID]) as [Count] FROM [Employees]
WHERE [DepartmentId] = @DepartmentId;
END;
GO

----------------------------------------------------SelectPageEmployees---------------------------------------
CREATE PROCEDURE [SelectPageEmployees]
@PageSize INT,
@PageNumber INT 
AS

CREATE TABLE [#Searched](EmployeeId INT,DepartmentId INT);
INSERT INTO  [#Searched] 
	  SELECT [EmployeeId], [DepartmentId] FROM dbo.GetEmployeesByPage(@PageSize,@PageNumber);

SELECT [E].[Id],
	   [LastName],
	   [FirstName],
	   [MiddleName],
	   [E].[DepartmentId],
	   [Address],
	   [ImagePath],
	   [BeginningWork],
	   [EndWork],
	   [HomePhone],
	   [WorkPhone],
	   [MobilePhone],
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
	   [Number] FROM [DepartmentPhones] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number]

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
@PageNumber INT
AS

CREATE TABLE [#Searched]([EmployeeId] INT,[DepartmentId] INT);
INSERT INTO  [#Searched] 
SELECT * FROM dbo.GetEmployeesBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId)
    ORDER BY [EmployeeId]
	OFFSET @PageSize *( @PageNumber - 1 ) ROWS
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
	   [EndWork],
	   [HomePhone],
	   [MobilePhone],
	   [WorkPhone],
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
	   [Number] FROM [DepartmentPhones] as [P]
  JOIN [#Searched] as [S]
    ON [S].[DepartmentId] = [P].[DepartmentId]
	GROUP BY [Id], 
	   [P].[DepartmentId],
	   [Number]

SELECT COUNT(*) as [Count] FROM dbo.GetEmployeesBySerchParams(@LastName,@FirstName,@MiddleName,@fromYear,@toYear,@WorkStatusId);

GO
-------------------------------------------------SaveEmployee---------------------------------------------
CREATE PROCEDURE [SaveEmployee]
@Id INT,
@LastName nvarchar(16),
@FirstName nvarchar(16),
@MiddleName nvarchar(16),
@DepartmentId INT,
@Address nvarchar(64),
@ImagePath nvarchar(max),
@BeginningWork DATETIME,
@EndWork DATETIME = null,
@HomePhone NVARCHAR(16) = null,
@WorkPhone NVARCHAR(16),
@MobilePhone NVARCHAR(16),
@MaritalStatusId INT,
@WorkStatusId INT,
@Emails [EmailsType] READONLY
AS
IF @Id=0
EXEC [InsertEmployee] @LastName,@FirstName,@MiddleName,@DepartmentId,@Address,@ImagePath,@BeginningWork,@EndWork,@HomePhone,@WorkPhone,@MobilePhone,@MaritalStatusId,@WorkStatusId,@Emails;
ELSE 
EXEC [UpdateEmployee] @Id,@LastName,@FirstName,@MiddleName,@DepartmentId,@Address,@ImagePath,@BeginningWork,@EndWork,@HomePhone,@WorkPhone,@MobilePhone,@MaritalStatusId,@WorkStatusId,@Emails;
GO
----------------------------------------------------InsertEmployee--------------------------------------------
CREATE PROCEDURE [InsertEmployee]
@LastName nvarchar(16),
@FirstName nvarchar(16),
@MiddleName nvarchar(16),
@DepartmentId INT,
@Address nvarchar(128),
@ImagePath nvarchar(max),
@BeginningWork DATETIME,
@EndWork DATETIME = NULL,
@HomePhone NVARCHAR(16)=null,
@WorkPhone NVARCHAR(16),
@MobilePhone NVARCHAR(16),
@MaritalStatusId INT,
@WorkStatusId INT,  
@Emails [EmailsType] READONLY
AS
INSERT INTO [Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
	       ,[HomePhone]
	       ,[WorkPhone]
		   ,[MobilePhone]
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
			@HomePhone,
			@WorkPhone,
			@MobilePhone,
			@ImagePath,
			@BeginningWork,
			@EndWork,
			@MaritalStatusId,
			@WorkStatusId);

DECLARE @Id INT = @@IDENTITY;

INSERT INTO [dbo].[Emails](
			[EmployeeId],
			[Address])
	SELECT  @Id,
			[Address] FROM @Emails;
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
@LastName nvarchar(16),
@FirstName nvarchar(16),
@MiddleName nvarchar(16),
@DepartmentId INT,
@Address nvarchar(64),
@ImagePath nvarchar(max),
@BeginningWork DATETIME,
@EndWork DATETIME,
@HomePhone NVARCHAR(16),
@WorkPhone NVARCHAR(16),
@MobilePhone NVARCHAR(16),
@MaritalStatusId INT,
@WorkStatusId INT,
@Emails [EmailsType] READONLY
AS
EXEC [UpdateEmployeeEmails] @Id, @Emails;

UPDATE [Employees]
      SET [LastName] = @LastName,
          [FirstName] = @FirstName,
          [MiddleName] = @MiddleName,
          [DepartmentId] = @DepartmentId,
          [Address] = @Address,
		  [HomePhone] = @HomePhone,
		  [MobilePhone] = @MobilePhone,
		  [WorkPhone] = @WorkPhone,
          [ImagePath] = @ImagePath,
          [BeginningWork] = @BeginningWork,
          [EndWork] = @EndWork,
          [MaritalStatusId] = @MaritalStatusId,
		  [WorkStatusId] = @WorkStatusId
	WHERE [Id] = @Id;
GO

-----------------------------------------------DeleteEmployeeById------------------------------------------
CREATE PROCEDURE [DeleteEmployee]
@Id INT
AS
DELETE FROM [dbo].[Emails]
      WHERE [EmployeeId] = @id;  

DELETE FROM [dbo].[Employees]
	  WHERE [Id] = @Id;
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

------------------------------------------------SaveMaritalStatus--------------------------------------------
CREATE PROCEDURE [SaveMaritalStatus]
@Id INT,
@Status NVARCHAR(16)
AS
IF @Id = 0
EXEC [InsertMaritalStatus] @Status;
ELSE 
EXEC [UpdateMaritalStatus] @Id,@Status;
GO

------------------------------------------------InsertMaritalStatus--------------------------------------------
CREATE PROCEDURE [InsertMaritalStatus]
@Status NVARCHAR(16)
AS
INSERT INTO [MaritalStatuses]
VALUES (@Status);
GO

------------------------------------------------UpdateMaritalStatus--------------------------------------------
CREATE PROCEDURE [UpdateMaritalStatus]
@Id INT,
@Status NVARCHAR(16)
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

------------------------------------------------WORKSTATUS-------------------------------------------------


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

------------------------------------------------SaveWorkStatus--------------------------------------------
CREATE PROCEDURE [SaveWorkStatus]
@Id INT,
@Status NVARCHAR(16)
AS
IF @Id = 0
EXEC [InsertWorkStatus] @Status;
ELSE 
EXEC [UpdateWorkStatus] @Id,@Status;
GO

------------------------------------------------InsertWorkStatus--------------------------------------------
CREATE PROCEDURE [InsertWorkStatus]
@Status NVARCHAR(16)
AS
INSERT INTO [WorkStatuses]
VALUES (@Status);
GO

------------------------------------------------UpdateWorkStatus--------------------------------------------
CREATE PROCEDURE [UpdateWorkStatus]
@Id INT,
@Status NVARCHAR(16)
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
@Login NVARCHAR(16)
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

---------------------------------------------SelectUserById--------------------------------------------------
CREATE PROCEDURE [SelectUserByEmployeeId]
@EmployeeId INT
AS
DECLARE @UserId INT = [dbo].[GetUserIdByEmplyeeId](@EmployeeId); 

EXEC SelectUserById @UserId;
GO

------------------------------------------------SaveUser--------------------------------------------
CREATE PROCEDURE [SaveUser]
@Id INT,
@EmployeeId INT,
@Login NVARCHAR(16),
@Password NVARCHAR(16),
@Roles [RolesType] READONLY
AS 
IF @Id = 0
EXEC [InsertUser] @EmployeeId,@Login,@Password,@Roles;
ELSE 
EXEC [UpdateUser] @Id,@EmployeeId,@Login,@Password,@Roles;
GO

---------------------------------------------InsertUser--------------------------------------------------
CREATE PROCEDURE [InsertUser]
@EmployeeId INT,
@Login NVARCHAR(16),
@Password NVARCHAR(16),
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

---------------------------------------------UpdateUser--------------------------------------------------
CREATE PROCEDURE [UpdateUser]
@Id INT,
@EmployeeId INT,
@Login NVARCHAR(16),
@Password NVARCHAR(16),
@Roles [RolesType] READONLY 
AS
DELETE [RolesUsers]
 WHERE [UserId] = @Id; 

INSERT INTO [RolesUsers](
			[RoleId],
			[UserId])
     SELECT [Id], 
	        @Id FROM @Roles;

UPDATE [Users]
    SET[EmployeeId] = @EmployeeId,
	   [Login] = @Login,
	   [Password] = @Password
where [ID] = @Id;
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
 
-------------------------------------------------Roles-----------------------------------------------------

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

---------------------------------------------MaritalStatus-----------------------------------------------
INSERT INTO [dbo].[MaritalStatuses]
           ([Status])
VALUES
('Холост'),
('Женат'),
('Вдовец');
GO



---------------------------------------------Inser Departments-----------------------------------------------
INSERT INTO [dbo].[Departments](
			[ParentID],
		    [Name],
            [Address],
            [Description])
VALUES
(0,'Отдел метала','ул.Еловая, д.10, оф.20','Занимается разработкой костюмов'),
(0,'Отдел галактики','ул.Сосновая, д.23, оф.11','Занимается спасением вселенной'),
(0,'Отдел асгарда','ул.Кедровая, д.13, оф.23','Следит за порядком во всех мирах'),
(0,'Отдел магии','ул.Вафельная, д.40, оф.20','Занимается защитой от других измерений'),
(0,'Отдел стариков','ул.Еловая, д.13, оф.20','Занимается защитой Америки'),
(0,'Отдел управления','ул.Кенова, д.10, оф.206','Занимается сбором мстителей');
GO

---------------------------------------------Inser Employees-----------------------------------------------
INSERT INTO [dbo].[Employees]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[DepartmentId]
           ,[Address]
		   ,[WorkPhone]
		   ,[HomePhone]
		   ,[MobilePhone]
           ,[MaritalStatusId]
           ,[ImagePath]
           ,[BeginningWork]
           ,[EndWork]
		   ,[WorkStatusId]
		   ,[HasRole])
VALUES
('Дауни','Роберт','Джон',1,'Нью-Йорк, США','80256578245','80291958245','80296845645',1,'/Content/Images/DauniMl.jpg', CAST('2008-01-08 00:00:00.000' as datetime),null,1,1),
('Прэтт','Крис','Кристофер',2,' Верджиния, Миннесота,США','80298678245','80297894545','80275978245',2,'/Content/Images/Pret.jpg',CAST('2012-01-08 00:00:00.000' as datetime),null,1,1),
('Хемсворт','Крис','Батькович',3,'Мельбурн, Виктория, Австралия','80296578245','80296578245','80296578245',2,'/Content/Images/tor.jpg',CAST('2009-01-08 00:00:00.000' as datetime),null,1,1),
('Эванс','Крис','Кристофер',5,' Бостон, Массачусетс, США','80296578245','80296578245','80296578245',2,'/Content/Images/cap.jpg',CAST('2008-01-08 00:00:00.000' as datetime),null,1,0),
('Хиддлстон','Том','Томас',3,'Вестминстер, Лондон, Великобритания','80296578245','80296578245','80296578245',2,'/Content/Images/Hiddlston.jpg',CAST('2011-01-08 00:00:00.000' as datetime),null,1,0),
('Салдана','Зои','Назарио',2,' Пассеик, Нью-Джерси, США','80296578245','80296578245','80296578245',2,'/Content/Images/zoe.jpg',CAST('2012-05-08 12:35:29.123' as datetime),null,1,0),
('Батиста','Дэвид','Майкл',2,'Лос-Анджелес, Калифорния, США','80296578245','80296578245','80296578245',3,'/Content/Images/Bautista.jpg',CAST('2009-05-08 12:35:29.123' as datetime),null,1,0),
('Пэлтроу','Гвинет','Кейт',1,' Лос-Анджелес, Калифорния, США','80296578245','80296578245','80296578245',1,'/Content/Images/gvin.jpg',CAST('2014-05-08 12:35:29.123' as datetime),null,1,0),
('Стэн','Себастиан','Батькович',5,'Констанца, Румыния','80296578245','80296578245','80296578245',2,'/Content/Images/baki.jpg',CAST('2012-05-08 12:35:29.123' as datetime),null,1,0),
('Джексон','Сэмюэл','Лерой',6,'Вашингтон, США','80296578245','80296578245','80296578245',1,'/Content/Images/jackson.jpg',CAST('2007-05-08 12:35:29.123' as datetime),null,1,0),
('Камбербэтч','Бенедикт','Карлтон',4,'Хаммерсмит и Фулем, Лондон','80296578245','80296578245','80296578245',2,'/Content/Images/betch.jpg',CAST('2012-05-08 12:35:29.123' as datetime),null,1,0),
('Суинтон','Тильда','Кэтрин',4,'Лондон, Великобритания','80296578245','80296578245','80296578245',2,'/Content/Images/tilda.jpg',CAST('2012-05-08 12:35:29.123' as datetime),null,1,0);
GO

------------------------------------------------Add Phones------------------------------------------------------
INSERT INTO [dbo].[DepartmentPhones](
			[DepartmentId],
			[Number])
VALUES
(1,'8029698789814'),
(2,'8029698789898'),
(3,'8029698781684'),
(4,'8029661811614'),
(5,'8029698781684'),
(6,'8029661811614')
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
(6,'gdfvdt@mail.ru'),
(7,'gerdrt@mail.ru'),
(8,'uritrt@mail.ru'),
(9,'werefs@mail.ru'),
(10,'sfsdft@mail.ru'),
(11,'sdffst@mail.ru'),
(12,'vfvdfv@mail.ru');
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

