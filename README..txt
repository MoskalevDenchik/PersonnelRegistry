How to deploy:

-After loading (unpacking) the project, and open the file "CreateDB.sql" in DataBase folder and set the address for your SQLServer and Execute script;
-Then you can change "endpoint address" for WCF service and "connectionString" for your DBConnection in "~/PersonnelRegistry/DM.PR/DM.PR.Data/DM.PR.Data.dll.config";
(default value for "endpoint address" - 8084 ) 
-Open DM.PR.sln and deploy DM.PR.Web on local storage.
-Deploy DM.PR.AdvertismentService on the IIS wihth "http://localhost:8084" if you didn't change andpoint address in config file.


Default logins for authentication:

Name: Turbo; Password: 1234; Role: admin
Name: Red; Password: 1234; Role: editor

