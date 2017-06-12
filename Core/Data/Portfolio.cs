using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data
{
    public class Portfolio : BaseEntity, IEntityBase
    {
        public string Name { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Org Organization { get; set; }
        public bool IsPrivate { get; set; }
    }
}
