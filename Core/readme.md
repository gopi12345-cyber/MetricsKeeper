I used this as the example for implementation of repo and context:
https://chsakell.com/2016/06/23/rest-apis-using-asp-net-core-and-entity-framework-core/

# Getting started with this thing:

 - Install MySQL and InfluxDB
 - Open the solution in VS, it should pull all dependencies and all
 - Update appsetings.json for database settings or create database 'mk' and do "GRANT ALL ON database.mk to 'mk'@'localhost' IDENTIFIED by 'mk'"
 - run migrations: dotnet ef database update
 - Go to Progam.cs and uncomment the two lines of code that populate the database with test data (the test data comes with TestData.json in /Test folder)
 - I could not get it to work with MCVS on mac to copy appsettings.json and TestData.json to the output dir, so you may need to copy these two files to the output directory manually

This should get you running.

# API facade:

 General pattern is very simple:

  - use Content-Type: application/json
  - use content in raw json in body of request

 HTTP {GET, POST, PUT, DELETE} /api/{entity}:

 GET /api/org - will list all orgs
 GET /api/org/1 - will give details on org with id=1
 PUT /api/org - will update the org based on its id (in body)
 POST /api/org - will create a new org
 delete /api/org/1 - will delete the org with id=1

# API available:

 - /api/org
 - /api/portfolio
 - /api/project
 - /api/metric

# API specific extensions:

 - TBD GET /api/entity/?expand=true - will expand the entity to include its lazy-loaded children
 - TBD GET /api/entity/?withdeleted=true - will return all data, including deleted/not active
 - TBD GET /api/entity?page=n - will return a page

