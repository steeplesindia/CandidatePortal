﻿using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatePortal.Server.Configurations
{
    internal sealed class SkillConfiguration: IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable(nameof(Skill));
            builder.HasKey(x => x.ERPRecId);
            builder.Property(x => x.ERPRecId).ValueGeneratedNever();
            builder.HasIndex(x => new { x.Code }).IsUnique();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DataArea).HasMaxLength(10);
        }
    }
}
