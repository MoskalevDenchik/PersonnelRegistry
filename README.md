How to deploy:

1) After loading (unpacking) the project, and open the file "CreateDB.sql" in DataBase folder and set the address for your SQLServer and execute script; 

2) Then you need to set value for "Data Source" in connection string section in "~/PersonnelRegistry/DM.PR/DM.PR.Data/DM.PR.Data.dll.config";

3) Open DM.PR.sln and deploy  DM.PR.AdvertismentService on the IIS with port: 8084 or 
set another andpoint (but you must change "endpoint address" for WCF service in "~/PersonnelRegistry/DM.PR/DM.PR.Data/DM.PR.Data.dll.config");

4) Open DM.PR.sln and deploy DM.PR.Web on local storage;


Default logins for authentication:

Name: Turbo; Password: 1234; Role: admin
Name: Red; Password: 1234; Role: editor

