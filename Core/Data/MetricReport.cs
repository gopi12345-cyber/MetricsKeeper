using System;
namespace Core.Data
{
    public class MetricReport : BaseEntity, IEntityBase
    {
        public Metric Metric { get; set; }
        public string Value { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
    }
}
