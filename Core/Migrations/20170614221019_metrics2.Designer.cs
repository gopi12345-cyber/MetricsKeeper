using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Core.Context;
using Core.Data;

namespace Core.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20170614221019_metrics2")]
    partial class metrics2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Core.Data.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("ProjectId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("Core.Data.Org", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Org");
                });

            modelBuilder.Entity("Core.Data.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("Core.Data.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("PortfolioId");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Core.Data.Metric", b =>
                {
                    b.HasOne("Core.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Core.Data.Portfolio", b =>
                {
                    b.HasOne("Core.Data.Org", "Organization")
                        .WithMany("Portfolios")
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Core.Data.Project", b =>
                {
                    b.HasOne("Core.Data.Portfolio", "Portfolio")
                        .WithMany("Projects")
                        .HasForeignKey("PortfolioId");
                });
        }
    }
}
