using System;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Context
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//new AuthorMap(modelBuilder.Entity<Author>());
			//new BookMap(modelBuilder.Entity<Book>());
		}



	}
}
