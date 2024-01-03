using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CertificateTypeConfiguration: IEntityTypeConfiguration<CertificateType>
    {
        public void Configure(EntityTypeBuilder<CertificateType> builder)
        {
            //builder.HasNoKey();
            builder.ToTable(nameof(CertificateType));
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();

            builder.HasIndex(x => new { x.Code, x.DataArea }).IsUnique();

            //builder.Property(x => x.ERPRecId).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DataArea).HasMaxLength(10);

            builder.HasMany(x => x.CandidateCertificateTypes)
                .WithOne()
                .HasForeignKey(y => y.CertificateTypeERPRecId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
