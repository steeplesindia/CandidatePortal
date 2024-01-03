using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationQuestionnaireLineAnswerConfiguration : IEntityTypeConfiguration<ApplicationQuestionnaireLineAnswer>
    {
        public void Configure(EntityTypeBuilder<ApplicationQuestionnaireLineAnswer> builder)
        {
            builder.ToTable(nameof(ApplicationQuestionnaireLineAnswer));
            builder.Property(x => x.Number).ValueGeneratedOnAdd();
            builder.Property(x => x.Text).HasMaxLength(1000);
            builder.Property(x => x.SequenceNumber).HasMaxLength(10).HasColumnType("decimal(10,2)");
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.IsDeleted);
        }
    }
}
