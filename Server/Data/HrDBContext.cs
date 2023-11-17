using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HrApp.Server.Models.HrDB;

namespace HrApp.Server.Data
{
    public partial class HrDBContext : DbContext
    {
        public HrDBContext()
        {
        }

        public HrDBContext(DbContextOptions<HrDBContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HrApp.Server.Models.HrDB.Country>()
              .HasOne(i => i.Region)
              .WithMany(i => i.Countries)
              .HasForeignKey(i => i.region_id)
              .HasPrincipalKey(i => i.region_id);

            builder.Entity<HrApp.Server.Models.HrDB.Department>()
              .HasOne(i => i.Location)
              .WithMany(i => i.Departments)
              .HasForeignKey(i => i.location_id)
              .HasPrincipalKey(i => i.location_id);

            builder.Entity<HrApp.Server.Models.HrDB.Dependent>()
              .HasOne(i => i.Employee)
              .WithMany(i => i.Dependents)
              .HasForeignKey(i => i.employee_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .HasOne(i => i.Department)
              .WithMany(i => i.Employees)
              .HasForeignKey(i => i.department_id)
              .HasPrincipalKey(i => i.department_id);

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .HasOne(i => i.Job)
              .WithMany(i => i.Employees)
              .HasForeignKey(i => i.job_id)
              .HasPrincipalKey(i => i.job_id);

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .HasOne(i => i.Employee1)
              .WithMany(i => i.Employees1)
              .HasForeignKey(i => i.manager_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<HrApp.Server.Models.HrDB.Location>()
              .HasOne(i => i.Country)
              .WithMany(i => i.Locations)
              .HasForeignKey(i => i.country_id)
              .HasPrincipalKey(i => i.country_id);

            builder.Entity<HrApp.Server.Models.HrDB.Country>()
              .Property(p => p.country_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Department>()
              .Property(p => p.location_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .Property(p => p.first_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .Property(p => p.phone_number)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .Property(p => p.manager_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Employee>()
              .Property(p => p.department_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Job>()
              .Property(p => p.min_salary)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Job>()
              .Property(p => p.max_salary)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Location>()
              .Property(p => p.street_address)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Location>()
              .Property(p => p.postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Location>()
              .Property(p => p.state_province)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<HrApp.Server.Models.HrDB.Region>()
              .Property(p => p.region_name)
              .HasDefaultValueSql(@"(NULL)");
            this.OnModelBuilding(builder);
        }

        public DbSet<HrApp.Server.Models.HrDB.Country> Countries { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Department> Departments { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Dependent> Dependents { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Employee> Employees { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Job> Jobs { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Location> Locations { get; set; }

        public DbSet<HrApp.Server.Models.HrDB.Region> Regions { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}