using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationConfiguration: IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable(nameof(Application));
            builder.HasKey(x => new { x.Number });
            builder.Property(x => x.Number).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.CandidateNumber, x.JobERPRecId }).IsUnique();

            builder.Property(x => x.CandidateNumber).IsRequired();
            builder.Property(x => x.JobERPRecId).IsRequired();
            builder.Property(x => x.ApplicationStatus).HasDefaultValueSql("((0))");
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.AppliedDate).HasColumnType("datetime");
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.IsSynced);// 04-06-2022

            builder.HasMany(x => x.ApplicationCommunications)
                .WithOne()
                .HasForeignKey(y => y.ApplicationNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ApplicationChecklists)
                .WithOne()
                .HasForeignKey(y => y.ApplicationNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ApplicationQuestionnaire)
                .WithOne()
                .HasForeignKey(y => y.ApplicationNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
