using System;
using Core.Data;
using Core.Context;
namespace Core.Repository
{
	public class OrgRepository : EntityBaseRepository<Org>, IOrgRepository
	{
		public OrgRepository(CoreContext context)
			: base(context)
		{ }
	}

}
