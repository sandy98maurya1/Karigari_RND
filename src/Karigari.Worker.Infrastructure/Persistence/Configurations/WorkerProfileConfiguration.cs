using Karigari.Worker.Domain.Entities;
using Karigari.Worker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karigari.Worker.Infrastructure.Persistence.Configurations;

internal sealed class WorkerProfileConfiguration : IEntityTypeConfiguration<WorkerProfile>
{
    public void Configure(EntityTypeBuilder<WorkerProfile> builder)
    {
        builder.ToTable("WorkerProfiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();

        var contactNumberConverter = new ValueConverter<ContactNumber, string>(
            contact => contact.Value,
            value => ContactNumber.Create(value));

        var nullableContactNumberConverter = new ValueConverter<ContactNumber?, string>(
            contact => contact!.Value,
            value => ContactNumber.Create(value));

        builder.Property(x => x.PrimaryContactNumber)
            .HasConversion(contactNumberConverter)
            .HasMaxLength(16)
            .IsRequired()
            .HasColumnName("PrimaryContactNumber");

        builder.Property(x => x.AlternateContactNumber)
            .HasConversion(nullableContactNumberConverter)
            .HasMaxLength(16)
            .HasColumnName("AlternateContactNumber");

        builder.Property(x => x.DateOfBirth).HasColumnType("date");
        builder.Property(x => x.IsActive).HasDefaultValue(false).IsRequired();
    }
}
