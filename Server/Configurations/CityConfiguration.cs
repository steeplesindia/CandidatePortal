using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CityConfiguration: IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(nameof(City));
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.HasIndex(x => new { x.Code, x.StateERPRecId, x.CountryERPRecId, x.CountyId }).IsUnique();

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.StateERPRecId).HasDefaultValueSql(null);
            builder.Property(x => x.CountryERPRecId);
            builder.Property(x => x.CountyId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
        }
    }
}
