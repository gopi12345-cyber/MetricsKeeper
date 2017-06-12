using System;
using Core.Data;
using Core.Context;
using System.Collections.Generic;

namespace Core.Repository
{
    public class PortfolioRepository : EntityBaseRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(CoreContext context)
            : base(context)
        {
        

        
        }

		
    }
}
