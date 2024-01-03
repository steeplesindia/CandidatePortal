using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CandidateSkillConfiguration: IEntityTypeConfiguration<CandidateSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.ToTable(nameof(CandidateSkill));
            builder.HasKey(x => new { x.Number });
            builder.Property(x => x.Number).ValueGeneratedOnAdd();// 28-05-22
            
            builder.HasKey(x => new { x.CandidateNumber, x.SkillERPRecId, x.LevelDate }); // 28-05-22

            builder.Property(x => x.CandidateNumber);
            builder.Property(x => x.SkillERPRecId);
            builder.Property(x => x.SkillLevelERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.LevelDate);
            builder.Property(x => x.YearOfExperience)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValueSql("((0))"); ;
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.LevelDate)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
        }
    }
}
