using System;
using Core.Data;
using Core.Context;
namespace Core.Repository
{
	public class ProjectRepository : EntityBaseRepository<Project>, IProjectRepository
	{
		public ProjectRepository(CoreContext context)
			: base(context)
		{ }
	}
}
