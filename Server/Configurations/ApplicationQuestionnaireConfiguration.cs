using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationQuestionnaireConfiguration : IEntityTypeConfiguration<ApplicationQuestionnaire>
    {
        public void Configure(EntityTypeBuilder<ApplicationQuestionnaire> builder)
        {
            builder.ToTable(nameof(ApplicationQuestionnaire));
            //builder.HasKey(x => new { x.Number });
            builder.Property(x => x.Number).ValueGeneratedOnAdd();
            builder.Property(x => x.ApplicationNumber).IsRequired();
            builder.Property(x => x.Completed).HasDefaultValueSql("((0))");
            builder.Property(x => x.Status).HasDefaultValueSql("((0))");
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();

            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.IsDeleted);

            builder.HasMany(x => x.ApplicationQuestionnaireLine)
                .WithOne()
                .HasForeignKey(y => y.ApplicationQuestionnaireErpRecId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(x => x.ApplicationQuestionnaireLineAnswer)
            //    .WithOne()
            //    .HasForeignKey(y => y.ApplicationQuestionnaireLineErpRecId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
