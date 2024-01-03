using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class StateConfiguration: IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable(nameof(State));
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.HasIndex(x => new { x.Code, x.CountryERPRecId }).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.CountryERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);

            builder.HasMany(x => x.Cities)
                .WithOne(z=>z.State)
                .HasForeignKey(y => y.StateERPRecId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
