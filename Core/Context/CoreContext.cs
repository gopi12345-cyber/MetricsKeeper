using System;
using Microsoft.EntityFrameworkCore;
using Core.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Context
{
	public class CoreContext : DbContext
	{
		public DbSet<Org> Orgs { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
		public DbSet<Project> Projects { get; set; }
        public DbSet<Metric> Metrics { get; set; }

		public CoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;

			}


			modelBuilder.Entity<Org>()
                        .ToTable("Org")
                        .HasIndex(c=>c.Name).IsUnique();

			//modelBuilder.Entity<Schedule>()
			//	.Property(s => s.CreatorId)
			//	.IsRequired();

			//modelBuilder.Entity<Schedule>()
			//	.Property(s => s.DateCreated)
			//	.HasDefaultValue(DateTime.Now);

			//modelBuilder.Entity<Schedule>()
			//	.Property(s => s.DateUpdated)
			//	.HasDefaultValue(DateTime.Now);

			//modelBuilder.Entity<Schedule>()
			//	.Property(s => s.Type)
			//	.HasDefaultValue(ScheduleType.Work);

			//modelBuilder.Entity<Schedule>()
			//	.Property(s => s.Status)
			//	.HasDefaultValue(ScheduleStatus.Valid);

			//modelBuilder.Entity<Schedule>()
				//.HasOne(s => s.Creator)
				//.WithMany(c => c.SchedulesCreated);

			modelBuilder.Entity<Portfolio>()
				.ToTable("Portfolio");

            modelBuilder.Entity<Portfolio>()
                        .HasOne(a => a.Organization);

			//modelBuilder.Entity<User>()
				//.Property(u => u.Name)
				//.HasMaxLength(100)
				//.IsRequired();

			modelBuilder.Entity<Project>()
				.ToTable("Project");

			//modelBuilder.Entity<Attendee>()
			//	.HasOne(a => a.User)
			//	.WithMany(u => u.SchedulesAttended)
			//	.HasForeignKey(a => a.UserId);

			//modelBuilder.Entity<Attendee>()
				//.HasOne(a => a.Schedule)
				//.WithMany(s => s.Attendees)
				//.HasForeignKey(a => a.ScheduleId);

		}
	}
}
