using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CandidateEducationConfiguration : IEntityTypeConfiguration<CandidateEducation>
    {
        public void Configure(EntityTypeBuilder<CandidateEducation> builder)
        {
            builder.ToTable(nameof(CandidateEducation));
            builder.HasKey(x => new { x.Number });
            builder.Property(x => x.Number).ValueGeneratedOnAdd();// 28-05-22
            builder.HasKey(x => new { x.CandidateNumber, x.EducationERPRecId });

            builder.Property(x => x.CandidateNumber);
            builder.Property(x => x.EducationERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.IsSynced);
            //builder.Property(x => x.StartDate);
            //builder.Property(x => x.EndDate);
            builder.Property(x => x.AvgGrade).IsRequired(false);
            //.HasColumnType("decimal(10, 2)")
            //.HasDefaultValueSql("((0))");

            builder.Property(x => x.Scale)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValueSql("((0))");

            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);

            builder.Property(x => x.StartDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");

            builder.Property(x => x.EndDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
        }
    }
}
