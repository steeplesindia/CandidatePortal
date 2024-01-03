using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CandidateExperienceConfiguration : IEntityTypeConfiguration<CandidateExperience>
    {
        public void Configure(EntityTypeBuilder<CandidateExperience> builder)
        {
            builder.ToTable(nameof(CandidateExperience));
            builder.HasKey(x => new { x.Number });
            builder.Property(x => x.Number).ValueGeneratedOnAdd();// 28-05-22
            builder.HasKey(x => new { x.CandidateNumber, x.Employer, x.StartDate }); // 28-05-22

            builder.Property(x => x.CandidateNumber);
            builder.Property(x => x.Employer).HasMaxLength(100);
            builder.Property(x => x.Position).HasMaxLength(100);
            //builder.Property(x => x.StartDate);
            //builder.Property(x => x.EndDate);
            builder.Property(x => x.EmployerURL).HasMaxLength(1000);
            builder.Property(x => x.EmployerContactNo).HasMaxLength(15);
            builder.Property(x => x.EmployerLocation).HasMaxLength(100);
            builder.Property(x => x.Notes).HasMaxLength(1000);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.StartDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.EndDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
        }
    }
}
