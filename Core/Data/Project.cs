using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Core.Data
{
    public class Project : BaseEntity, IEntityBase
    {
        public string Name { get; set; }
        public int PortfolioId { get; set; }
        [ForeignKey("PortfolioId")]
        public Portfolio Portfolio { get; set; }
    }
}
