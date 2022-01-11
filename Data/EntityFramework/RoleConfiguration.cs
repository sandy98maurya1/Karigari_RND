using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole { Name = "Labour", NormalizedName = "LABOUR" },
                    new IdentityRole { Name = "Company", NormalizedName = "COMPANY" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );

        }
    }
}
