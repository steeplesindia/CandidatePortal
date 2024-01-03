using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class JobConfiguration: IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable(nameof(Job));
            //builder.HasKey(x => new { x.Number });
            //builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.HasIndex(x => new { x.Code }).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.LongDescription);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.OpeningDate).HasColumnType("datetime");
            builder.Property(x => x.ClosingDate).HasColumnType("datetime");
            
            builder.Property(x => x.PortalLink).HasMaxLength(250);
            builder.Property(x => x.Locations).HasMaxLength(250);
            builder.Property(x => x.NoOfOpenings);
            builder.Property(x => x.ApplicationDeadline).HasColumnType("datetime");
            builder.Property(x => x.Status);
            builder.Property(x => x.PublishedToPortal);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.IsSynced);

            builder.HasMany(x => x.Applications)
                .WithOne()
                .HasForeignKey(y => y.JobERPRecId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
