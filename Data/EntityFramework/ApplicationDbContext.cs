using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Labour>().Property(e => e.Id).ValueGeneratedOnAdd();

            builder.ApplyConfiguration(new RoleConfiguration());
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Labour> Employee { get; set; }
        public virtual DbSet<Company> Employer { get; set; }
        public virtual DbSet<OTPModel> OTPModel { get; set; }
        public virtual DbSet<SkillSet> SkillSet { get; set; }


    }
}
