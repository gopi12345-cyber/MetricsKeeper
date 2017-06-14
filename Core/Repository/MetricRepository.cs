using System;
using Core.Data;
using Core.Context;
namespace Core.Repository
{
	public class MetricRepository : EntityBaseRepository<Metric>, IMetricRepository
	{
		public MetricRepository(CoreContext context)
			: base(context)
		{ }
	}

}