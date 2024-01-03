using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CountryConfiguration: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(nameof(Country));
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.HasIndex(x => new { x.Code }).IsUnique();

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DataArea).HasMaxLength(10);

            builder.HasMany(x => x.States)
                .WithOne(z => z.Country)
                .HasForeignKey(y => y.CountryERPRecId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasMany(x => x.Cities)
                .WithOne(z => z.Country)
                .HasForeignKey(y => y.CountryERPRecId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
