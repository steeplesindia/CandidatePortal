using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationCommunicationConfiguration : IEntityTypeConfiguration<ApplicationCommunication>
    {
        public void Configure(EntityTypeBuilder<ApplicationCommunication> builder)
        {
            builder.ToTable(nameof(ApplicationCommunication));
            builder.HasKey(x => x.Number);
            builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.Property(x => x.ApplicationNumber).IsRequired();
            builder.Property(x => x.CommunicationDirection);
            builder.Property(x => x.Subject).HasMaxLength(100);
            builder.Property(x => x.Message);
            builder.Property(x => x.Submitted);
            builder.Property(x => x.SubmittedDateTime).HasColumnType("datetime");
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();

            builder.HasMany(x => x.ApplicationCommunicationDocuments)
                .WithOne()
                .HasForeignKey(y => y.ApplicationCommunicationNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
