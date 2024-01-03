using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class ApplicationCommunicationDocumentConfiguration : IEntityTypeConfiguration<ApplicationCommunicationDocument>
    {
        public void Configure(EntityTypeBuilder<ApplicationCommunicationDocument> builder)
        {
            builder.ToTable(nameof(ApplicationCommunicationDocument));
            builder.HasKey(x => x.Number);
            builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.Property(x => x.ApplicationCommunicationNumber).IsRequired();
            builder.Property(x => x.DocumentPath).HasMaxLength(1000);
            builder.Property(x => x.DocumentName).HasMaxLength(100);

            builder.Property(x => x.IsSynced);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
        }
    }
}
