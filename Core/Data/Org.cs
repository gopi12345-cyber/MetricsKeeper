using System;
using System.Collections.Generic;
namespace Core.Data
{
    public class Org : BaseEntity, IEntityBase
    {
        public string Name { get; set; }
        public List<Portfolio> Portfolios { get; set; }
    }
}
