using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationQuestionnaireLineConfiguration : IEntityTypeConfiguration<ApplicationQuestionnaireLine>
    {
        public void Configure(EntityTypeBuilder<ApplicationQuestionnaireLine> builder)
        {
            builder.ToTable(nameof(ApplicationQuestionnaireLine));
            builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.Property(x => x.QuestionId).HasMaxLength(100);
            builder.Property(x => x.Text).HasMaxLength(1000);
            builder.Property(x => x.Type);
            builder.Property(x => x.AnswerErpRecId);
            builder.Property(x => x.Answer).HasMaxLength(1000);
            builder.Property(x => x.Comments).HasMaxLength(1000);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.SequenceNumber).HasMaxLength(10).HasColumnType("decimal(10,2)");
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.IsDeleted);

            builder.HasMany(x => x.ApplicationQuestionnaireLineAnswer)
                .WithOne()
                .HasForeignKey(y => y.ApplicationQuestionnaireLineErpRecId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
