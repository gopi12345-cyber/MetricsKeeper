using System;
using Core.Data;
namespace Core.Repository
{
    public interface IOrgRepository : IEntityBaseRepository<Org>{}
    public interface IPortfolioRepository : IEntityBaseRepository<Portfolio>{}
    public interface IProjectRepository : IEntityBaseRepository<Project>{}
    public interface IMetricRepository : IEntityBaseRepository<Metric>{}
}
