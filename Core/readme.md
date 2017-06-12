I used this as the example for implementation of repo and context:
https://chsakell.com/2016/06/23/rest-apis-using-asp-net-core-and-entity-framework-core/

Dependencies:

 - MySQL
 - Nuget packages are all self-explanatory

Getting started with this thing:

 - Open the solution in VS, it should pull all dependencies and all
 - Update appsetings.json for database settings or create database 'mk' and GRANT ALL ON database.mk to 'mk'@'localhost' IDENTIFIED by 'mk'
 - run migrations: dotnet ef database update

 This should get you running.