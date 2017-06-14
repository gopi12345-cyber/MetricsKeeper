I used this as the example for implementation of repo and context:
https://chsakell.com/2016/06/23/rest-apis-using-asp-net-core-and-entity-framework-core/

Dependencies:

 - MySQL
 - Redis (not yet, but be prepared - all metrics data will Sit in Redis)
 - Nuget packages are all self-explanatory and should pull when you open the solution

Getting started with this thing:

 - Open the solution in VS, it should pull all dependencies and all
 - Update appsetings.json for database settings or create database 'mk' and GRANT ALL ON database.mk to 'mk'@'localhost' IDENTIFIED by 'mk'
 - run migrations: dotnet ef database update

 This should get you running.

 Roadmap:

  - Convert EntityBaseRepository methods to be async
  - Implement options to opt-in for specific fields while Include() (and potentially other methods)
  - Store metrics in Redis
  - Change objects retrieval returns from IEnumerable to IQueryable
  - Paging for large lists
  - Nested paging
  - Authentication and Org-based Authorization
  - Generate documentation with Swagger https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger

 API facade:

 General pattern is very simple:

  - use Content-Type: application/json
  - use content in raw json in body of request

 HTTP {GET, POST, PUT, DELETE} /api/{entity}:

 GET /api/org - will list all orgs
 GET /api/org/1 - will give details on org with id=1
 PUT /api/org - will update the org based on its id (in body)
 POST /api/org - will create a new org
 delete /api/org/1 - will delete the org with id=1

 API available:

 - /api/org
 - /api/portfolio
 - /api/project

 API specific extensions:

 - TBD GET /api/entity/?expand=true - will expand the entity to include its lazy-loaded children
 - TBD GET /api/entity/?withdeleted=true - will return all data, including deleted/not active
 - TBD GET /api/entity?page=n - will return a page


 Metric regex

 SQALE Literal: /^[A-D]{1}$/i

