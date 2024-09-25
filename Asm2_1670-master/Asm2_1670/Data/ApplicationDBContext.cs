using Asm2_1670.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Asm2_1670.Data
{
	public class ApplicationDBContext : IdentityDbContext
	{
		public DbSet<Categories> Categories { get; set; }
		public DbSet<WorkExperience> WorkExperiences { get; set; }
		public DbSet<Education> Educations { get; set; }
		public DbSet<Portfolio> Portfolios { get; set; }
		public DbSet<Award> Awards { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Application> Applications { get; set; }
		public ApplicationDBContext(DbContextOptions options):base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Categories>().HasData(
				new Categories{ Id = 1, Name = "Education Training", Icon = "la la-graduation-cap", Proposer = "Admin", Status = "Active"},
				new Categories { Id = 2, Name = "Design, Art & Multimedia", Icon = "la la-bullhorn", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 3, Name = "Accounting / Finance", Icon = "la la-line-chart", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 4, Name = "Human Resource", Icon = "la la-users", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 5, Name = "Telecommuncations", Icon = "la la-phone", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 6, Name = "Restaurant / Food Service", Icon = "la la-cutlery", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 7, Name = "Construction / Facilities", Icon = "la la-building", Proposer = "Admin", Status = "Active" },
				new Categories { Id = 8, Name = "Health", Icon = "la la-user-md", Proposer = "Admin", Status = "Active" }
				);
		}
	}
}
