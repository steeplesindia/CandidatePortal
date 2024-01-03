using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationChecklistConfiguration : IEntityTypeConfiguration<ApplicationChecklist>
    {
        public void Configure(EntityTypeBuilder<ApplicationChecklist> builder)
        {
            builder.ToTable(nameof(ApplicationChecklist));
            builder.HasKey(x => x.Number);
            builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.Property(x => x.ApplicationNumber).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Remarks).HasMaxLength(1000);
            builder.Property(x => x.CompletedDateTime).HasColumnType("datetime");
            //builder.Property(x => x.RepliedDateTime).HasColumnType("datetime");
            builder.Property(x => x.Status).HasDefaultValueSql("((0))");
            builder.Property(x => x.Completed);
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();

            builder.HasMany(x => x.ApplicationChecklistDocuments)
                .WithOne()
                .HasForeignKey(y => y.ApplicationChecklistNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
