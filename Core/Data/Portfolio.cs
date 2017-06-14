using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Core.Data
{
    public class Portfolio : BaseEntity, IEntityBase
    {
        public string Name { get; set; }

        public int OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Org Organization { get; set; }

        public bool IsPrivate { get; set; }
        public List<Project> Projects {get;set;}
    }
}
