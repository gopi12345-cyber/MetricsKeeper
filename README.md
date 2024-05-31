# What is MetricsKeeper?

MetricsKeeper is being built to serve a purpose of storing, aggregating and visualizing metrics or different sorts.
It is being built primarily for software engineering teams and their management but can be used to cover needs of other industries.

## The Idea
My vision at this point is about creating a REST-driven backend that will allow creating metrics and reporting them. All metrics are linked to *Projects*, projects are grouped by *Portfolios* that end up in *Organizations*. Once the REST API is all good and running, we can start working on two big things:  

### UI
We will need UI to allow *manual* entry of metrics and visualizing this data.

### Metric Collectors (agents)
I am planning to build metric collection agents for various systems to make collection of metrics simple and complete (target systems at this point are: Sonar, Jenkins, Jira)

# Why MetricsKeeper?

Well, I am doing this as a personal study (while playing a managerial role in software industry, I find it important to be hands-on with technology)
and in response to my failed search to find I tool that will serve my needs fully.

# Status and Contributions

It is at early stages of development, I encourage you to contribute if you are interested! At this point, any work in the web ui space would be very much appreciated.

# Technologies:

- AspNet.Core, Web API
- EntityFrameworkCore
- MySQL
- InfluxDB (metrics storage backend)

# Roadmap for WebApi:

 - (Core) Convert EntityBaseRepository methods to be async
 - (Core) Implement options to opt-in for specific fields while Include() (and potentially other methods) 
 - (Core) Store MetricReports in Redis
 - (Core) Change objects retrieval returns from IEnumerable to IQueryable
 - (Core) Paging for large lists & Nested paging
 - Authentication and Org-based Authorization
 - Generate documentation with Swagger https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger# dotnet
