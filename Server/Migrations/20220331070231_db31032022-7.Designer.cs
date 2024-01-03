﻿// <auto-generated />
using System;
using CandidatePortal.Server.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    [DbContext(typeof(PortalDbContext))]
    [Migration("20220331070231_db31032022-7")]
    partial class db310320227
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Application", b =>
                {
                    b.Property<long>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Number"), 1L, 1);

                    b.Property<short>("ApplicationStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("datetime");

                    b.Property<long>("CandidateNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<long>("JobERPRecId")
                        .HasColumnType("bigint");

                    b.HasKey("Number");

                    b.HasIndex("JobERPRecId");

                    b.HasIndex("CandidateNumber", "JobERPRecId")
                        .IsUnique();

                    b.ToTable("Application", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Candidate", b =>
                {
                    b.Property<long>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Number"), 1L, 1);

                    b.Property<string>("AlternateContactNo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<long?>("CityERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<long?>("CountryERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentJobTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FaceBookLink")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short?>("Gender")
                        .HasColumnType("smallint");

                    b.Property<long?>("HighestDegreeERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool?>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsWillRelocated")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsWillTravel")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LinkedinLink")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<short?>("MaritalStatus")
                        .HasColumnType("smallint");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("PersonalTitleERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("PreviousEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("ResumeURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<long?>("StateERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Street")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("TwitterLink")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Number");

                    b.HasIndex("CityERPRecId");

                    b.HasIndex("CountryERPRecId");

                    b.HasIndex("PersonalTitleERPRecId");

                    b.HasIndex("StateERPRecId");

                    b.ToTable("Candidate", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateCertificateType", b =>
                {
                    b.Property<long>("CandidateNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("CertificateTypeERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("RenewalRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("CandidateNumber", "CertificateTypeERPRecId");

                    b.HasIndex("CertificateTypeERPRecId");

                    b.ToTable("CandidateCertificateType", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateEducation", b =>
                {
                    b.Property<long>("CandidateNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("EducationERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal>("AvgGrade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<decimal>("Scale")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("CandidateNumber", "EducationERPRecId");

                    b.HasIndex("EducationERPRecId");

                    b.ToTable("CandidateEducation", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateExperience", b =>
                {
                    b.Property<long>("CandidateNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Employer")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("EmployerContactNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("EmployerLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmployerURL")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("CandidateNumber", "Employer");

                    b.ToTable("CandidateExperience", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateSkill", b =>
                {
                    b.Property<long>("CandidateNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("SkillERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("DataArea")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LevelDate")
                        .HasColumnType("datetime");

                    b.Property<long>("SkillLevelERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal>("YearOfExperience")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("CandidateNumber", "SkillERPRecId");

                    b.HasIndex("SkillERPRecId");

                    b.HasIndex("SkillLevelERPRecId");

                    b.ToTable("CandidateSkill", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CertificateType", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code", "DataArea")
                        .IsUnique()
                        .HasFilter("[DataArea] IS NOT NULL");

                    b.ToTable("CertificateType", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.City", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("CountryERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("StateERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code", "StateERPRecId", "CountryERPRecId")
                        .IsUnique()
                        .HasFilter("[StateERPRecId] IS NOT NULL AND [CountryERPRecId] IS NOT NULL");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Country", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Education", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Education", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Job", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OpeningDate")
                        .HasColumnType("datetime");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Job", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.PersonalTitle", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("PersonalTitle", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Skill", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Skill", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.SkillLevel", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("SkillLevel", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.State", b =>
                {
                    b.Property<long>("ERPRecId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("CountryERPRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("DataArea")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ERPRecId");

                    b.HasIndex("CountryERPRecId");

                    b.HasIndex("Code", "CountryERPRecId")
                        .IsUnique();

                    b.ToTable("State", (string)null);
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Application", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Candidate", null)
                        .WithMany("Applications")
                        .HasForeignKey("CandidateNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidatePortal.Shared.Entities.Job", null)
                        .WithMany("Applications")
                        .HasForeignKey("JobERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Candidate", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.City", "City")
                        .WithMany("Candidates")
                        .HasForeignKey("CityERPRecId");

                    b.HasOne("CandidatePortal.Shared.Entities.Country", "Country")
                        .WithMany("Candidates")
                        .HasForeignKey("CountryERPRecId");

                    b.HasOne("CandidatePortal.Shared.Entities.PersonalTitle", "PersonalTitle")
                        .WithMany("Candidates")
                        .HasForeignKey("PersonalTitleERPRecId");

                    b.HasOne("CandidatePortal.Shared.Entities.State", "State")
                        .WithMany("Candidates")
                        .HasForeignKey("StateERPRecId");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("PersonalTitle");

                    b.Navigation("State");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateCertificateType", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Candidate", null)
                        .WithMany("CandidateCertificateTypes")
                        .HasForeignKey("CandidateNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidatePortal.Shared.Entities.CertificateType", null)
                        .WithMany("CandidateCertificateTypes")
                        .HasForeignKey("CertificateTypeERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateEducation", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Candidate", null)
                        .WithMany("CandidateEducations")
                        .HasForeignKey("CandidateNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidatePortal.Shared.Entities.Education", null)
                        .WithMany("CandidateEducation")
                        .HasForeignKey("EducationERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateExperience", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Candidate", null)
                        .WithMany("CandidateExperiences")
                        .HasForeignKey("CandidateNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CandidateSkill", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Candidate", null)
                        .WithMany("CandidateSkills")
                        .HasForeignKey("CandidateNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidatePortal.Shared.Entities.Skill", null)
                        .WithMany("CandidateSkill")
                        .HasForeignKey("SkillERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidatePortal.Shared.Entities.SkillLevel", null)
                        .WithMany("CandidateSkill")
                        .HasForeignKey("SkillLevelERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.State", b =>
                {
                    b.HasOne("CandidatePortal.Shared.Entities.Country", null)
                        .WithMany("States")
                        .HasForeignKey("CountryERPRecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Candidate", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("CandidateCertificateTypes");

                    b.Navigation("CandidateEducations");

                    b.Navigation("CandidateExperiences");

                    b.Navigation("CandidateSkills");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.CertificateType", b =>
                {
                    b.Navigation("CandidateCertificateTypes");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.City", b =>
                {
                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Country", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("States");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Education", b =>
                {
                    b.Navigation("CandidateEducation");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Job", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.PersonalTitle", b =>
                {
                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.Skill", b =>
                {
                    b.Navigation("CandidateSkill");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.SkillLevel", b =>
                {
                    b.Navigation("CandidateSkill");
                });

            modelBuilder.Entity("CandidatePortal.Shared.Entities.State", b =>
                {
                    b.Navigation("Candidates");
                });
#pragma warning restore 612, 618
        }
    }
}
