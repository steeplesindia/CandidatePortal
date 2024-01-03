using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CandidateCertificateTypeConfiguration: IEntityTypeConfiguration<CandidateCertificateType>
    {
        public void Configure(EntityTypeBuilder<CandidateCertificateType> builder)
        {
            builder.ToTable(nameof(CandidateCertificateType));
            builder.Property(x => x.Number).ValueGeneratedOnAdd();// 28-05-22

            builder.HasKey(x => new { x.CandidateNumber, x.CertificateTypeERPRecId, x.StartDate }); // 28-05-22

            builder.Property(x => x.CandidateNumber);
            builder.Property(x => x.CertificateTypeERPRecId);

            //builder.Property(x => x.StartDate);
            //builder.Property(x => x.EndDate);
            builder.Property(x => x.Notes).HasMaxLength(1000);
            builder.Property(x => x.RenewalRequired);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.IsDeleted);

            builder.Property(x => x.StartDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");

            builder.Property(x => x.EndDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
        }
    }
}
