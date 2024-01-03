using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable(nameof(Candidate));
            builder.HasKey(x => x.Number);
            builder.Property(x => x.Number).ValueGeneratedOnAdd();

            builder.Property(x => x.PersonalTitleERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.CurrentJobTitle).HasMaxLength(100);
            builder.Property(x => x.HighestDegreeERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.PreviousEmployee);
            builder.Property(x => x.CountryERPRecId).HasDefaultValueSql("((0))").IsRequired(false);
            builder.Property(x => x.StateERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.CityERPRecId).HasDefaultValueSql("((0))");
            builder.Property(x => x.Street).HasMaxLength(250);
            builder.Property(x => x.ZipCode).HasMaxLength(100);
            builder.Property(x => x.NationalityId).HasDefaultValueSql("((0))").IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ContactNo).HasMaxLength(20);
            builder.Property(x => x.AlternateContactNo).HasMaxLength(20);
            builder.Property(x => x.ImageURL).HasMaxLength(1000);
            builder.Property(x => x.ResumeURL).HasMaxLength(1000);
            builder.Property(x => x.FaceBookLink).HasMaxLength(1000);
            builder.Property(x => x.TwitterLink).HasMaxLength(1000);
            builder.Property(x => x.LinkedinLink).HasMaxLength(1000);
            builder.Property(x => x.Dob).HasColumnType("datetime");
            builder.Property(x => x.RowVersion).HasColumnType("timestamp").IsRowVersion();
            builder.Property(x => x.Gender);
            builder.Property(x => x.MaritalStatus);
            builder.Property(x => x.Disabled);// 04-06-2022
            builder.Property(x => x.CanTravel);
            builder.Property(x => x.CanRelocate);// 04-06-2022
            builder.Property(x => x.CreatedDate).HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.IsSynced);
            builder.Property(x => x.ERPRecId);
            builder.Property(x => x.DataArea).HasMaxLength(10);
            builder.Property(x => x.Dob)
               //.HasConversion<DateOnlyConverter>()
               .HasColumnType("datetime");
            
            builder.Property(x => x.IsDeleted);

            builder.HasMany(x => x.CandidateSkills)
                .WithOne()
                .HasForeignKey(y => y.CandidateNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CandidateCertificateTypes)
                .WithOne()
                .HasForeignKey(y => y.CandidateNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CandidateEducations)
                .WithOne()
                .HasForeignKey(y => y.CandidateNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CandidateExperiences)
                .WithOne()
                .HasForeignKey(y => y.CandidateNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Applications)
                .WithOne()
                .HasForeignKey(y => y.CandidateNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CountryERPRecId);

            builder.HasOne(d => d.State)
                .WithMany(p => p.Candidates)
                .HasForeignKey(d => d.StateERPRecId);

            builder.HasOne(d => d.City)
                .WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CityERPRecId);

            builder.HasOne(d => d.PersonalTitle)
                .WithMany(p => p.Candidates)
                .HasForeignKey(d => d.PersonalTitleERPRecId);

            //Akshay 14-06-22
            builder.HasOne(d => d.EducationLevel)
              .WithMany(p => p.Candidates)
              .HasForeignKey(d => d.HighestDegreeERPRecId);
        }
    }
}
