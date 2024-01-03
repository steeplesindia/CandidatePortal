using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificateType",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateType", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevel",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PortalLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Locations = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NoOfOpenings = table.Column<int>(type: "int", nullable: true),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    PublishedToPortal = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalTitle",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTitle", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "SkillLevel",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevel", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryERPRecId",
                        column: x => x.CountryERPRecId,
                        principalTable: "Country",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StateERPRecId = table.Column<long>(type: "bigint", nullable: true),
                    CountryERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    CountyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryERPRecId",
                        column: x => x.CountryERPRecId,
                        principalTable: "Country",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_City_State_StateERPRecId",
                    //    column: x => x.StateERPRecId,
                    //    principalTable: "State",
                    //    principalColumn: "ERPRecId",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalTitleERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CurrentJobTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HighestDegreeERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    PreviousEmployee = table.Column<int>(type: "int", nullable: true),
                    CountryERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    StateERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    CityERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    Street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternateContactNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ResumeURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FaceBookLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LinkedinLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<short>(type: "smallint", nullable: true),
                    MaritalStatus = table.Column<short>(type: "smallint", nullable: true),
                    Disabled = table.Column<int>(type: "int", nullable: true),
                    CanTravel = table.Column<int>(type: "int", nullable: true),
                    CanRelocate = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Candidate_City_CityERPRecId",
                        column: x => x.CityERPRecId,
                        principalTable: "City",
                        principalColumn: "ERPRecId");
                    table.ForeignKey(
                        name: "FK_Candidate_Country_CountryERPRecId",
                        column: x => x.CountryERPRecId,
                        principalTable: "Country",
                        principalColumn: "ERPRecId");
                    table.ForeignKey(
                        name: "FK_Candidate_EducationLevel_HighestDegreeERPRecId",
                        column: x => x.HighestDegreeERPRecId,
                        principalTable: "EducationLevel",
                        principalColumn: "ERPRecId");
                    table.ForeignKey(
                        name: "FK_Candidate_PersonalTitle_PersonalTitleERPRecId",
                        column: x => x.PersonalTitleERPRecId,
                        principalTable: "PersonalTitle",
                        principalColumn: "ERPRecId");
                    table.ForeignKey(
                        name: "FK_Candidate_State_StateERPRecId",
                        column: x => x.StateERPRecId,
                        principalTable: "State",
                        principalColumn: "ERPRecId");
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateNumber = table.Column<long>(type: "bigint", nullable: false),
                    JobERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationStatus = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    AppliedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Application_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_Job_JobERPRecId",
                        column: x => x.JobERPRecId,
                        principalTable: "Job",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateCertificateType",
                columns: table => new
                {
                    CandidateNumber = table.Column<long>(type: "bigint", nullable: false),
                    CertificateTypeERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RenewalRequired = table.Column<int>(type: "int", nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCertificateType", x => new { x.CandidateNumber, x.CertificateTypeERPRecId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_CandidateCertificateType_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateCertificateType_CertificateType_CertificateTypeERPRecId",
                        column: x => x.CertificateTypeERPRecId,
                        principalTable: "CertificateType",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducation",
                columns: table => new
                {
                    CandidateNumber = table.Column<long>(type: "bigint", nullable: false),
                    EducationERPRecId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((0))"),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AvgGrade = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValueSql: "((0))"),
                    Scale = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValueSql: "((0))"),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducation", x => new { x.CandidateNumber, x.EducationERPRecId });
                    table.ForeignKey(
                        name: "FK_CandidateEducation_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateEducation_Education_EducationERPRecId",
                        column: x => x.EducationERPRecId,
                        principalTable: "Education",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateExperience",
                columns: table => new
                {
                    CandidateNumber = table.Column<long>(type: "bigint", nullable: false),
                    Employer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmployerURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EmployerContactNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EmployerLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExperience", x => new { x.CandidateNumber, x.Employer, x.StartDate });
                    table.ForeignKey(
                        name: "FK_CandidateExperience_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    CandidateNumber = table.Column<long>(type: "bigint", nullable: false),
                    SkillERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    LevelDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLevelERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    YearOfExperience = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValueSql: "((0))"),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => new { x.CandidateNumber, x.SkillERPRecId, x.LevelDate });
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Skill_SkillERPRecId",
                        column: x => x.SkillERPRecId,
                        principalTable: "Skill",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_SkillLevel_SkillLevelERPRecId",
                        column: x => x.SkillLevelERPRecId,
                        principalTable: "SkillLevel",
                        principalColumn: "ERPRecId");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationChecklist",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    Completed = table.Column<int>(type: "int", nullable: true),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationChecklist", x => x.Number);
                    table.ForeignKey(
                        name: "FK_ApplicationChecklist_Application_ApplicationNumber",
                        column: x => x.ApplicationNumber,
                        principalTable: "Application",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCommunication",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<long>(type: "bigint", nullable: false),
                    CommunicationDirection = table.Column<short>(type: "smallint", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submitted = table.Column<int>(type: "int", nullable: true),
                    SubmittedDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCommunication", x => x.Number);
                    table.ForeignKey(
                        name: "FK_ApplicationCommunication_Application_ApplicationNumber",
                        column: x => x.ApplicationNumber,
                        principalTable: "Application",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationQuestionnaire",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<long>(type: "bigint", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationQuestionnaire", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_ApplicationQuestionnaire_Application_ApplicationNumber",
                        column: x => x.ApplicationNumber,
                        principalTable: "Application",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationChecklistDocument",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationChecklistNumber = table.Column<long>(type: "bigint", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationChecklistDocument", x => x.Number);
                    table.ForeignKey(
                        name: "FK_ApplicationChecklistDocument_ApplicationChecklist_ApplicationChecklistNumber",
                        column: x => x.ApplicationChecklistNumber,
                        principalTable: "ApplicationChecklist",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCommunicationDocument",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationCommunicationNumber = table.Column<long>(type: "bigint", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSynced = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCommunicationDocument", x => x.Number);
                    table.ForeignKey(
                        name: "FK_ApplicationCommunicationDocument_ApplicationCommunication_ApplicationCommunicationNumber",
                        column: x => x.ApplicationCommunicationNumber,
                        principalTable: "ApplicationCommunication",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationQuestionnaireLine",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationQuestionnaireErpRecId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    AnswerErpRecId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    SequenceNumber = table.Column<decimal>(type: "decimal(10,2)", maxLength: 10, nullable: false),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationQuestionnaireLine", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_ApplicationQuestionnaireLine_ApplicationQuestionnaire_ApplicationQuestionnaireErpRecId",
                        column: x => x.ApplicationQuestionnaireErpRecId,
                        principalTable: "ApplicationQuestionnaire",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationQuestionnaireLineAnswer",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationQuestionnaireLineErpRecId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    SequenceNumber = table.Column<decimal>(type: "decimal(10,2)", maxLength: 10, nullable: false),
                    IsSynced = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationQuestionnaireLineAnswer", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_ApplicationQuestionnaireLineAnswer_ApplicationQuestionnaireLine_ApplicationQuestionnaireLineErpRecId",
                        column: x => x.ApplicationQuestionnaireLineErpRecId,
                        principalTable: "ApplicationQuestionnaireLine",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_CandidateNumber_JobERPRecId",
                table: "Application",
                columns: new[] { "CandidateNumber", "JobERPRecId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_JobERPRecId",
                table: "Application",
                column: "JobERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationChecklist_ApplicationNumber",
                table: "ApplicationChecklist",
                column: "ApplicationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationChecklistDocument_ApplicationChecklistNumber",
                table: "ApplicationChecklistDocument",
                column: "ApplicationChecklistNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCommunication_ApplicationNumber",
                table: "ApplicationCommunication",
                column: "ApplicationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCommunicationDocument_ApplicationCommunicationNumber",
                table: "ApplicationCommunicationDocument",
                column: "ApplicationCommunicationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationQuestionnaire_ApplicationNumber",
                table: "ApplicationQuestionnaire",
                column: "ApplicationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationQuestionnaireLine_ApplicationQuestionnaireErpRecId",
                table: "ApplicationQuestionnaireLine",
                column: "ApplicationQuestionnaireErpRecId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationQuestionnaireLineAnswer_ApplicationQuestionnaireLineErpRecId",
                table: "ApplicationQuestionnaireLineAnswer",
                column: "ApplicationQuestionnaireLineErpRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CityERPRecId",
                table: "Candidate",
                column: "CityERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CountryERPRecId",
                table: "Candidate",
                column: "CountryERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_HighestDegreeERPRecId",
                table: "Candidate",
                column: "HighestDegreeERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_PersonalTitleERPRecId",
                table: "Candidate",
                column: "PersonalTitleERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_StateERPRecId",
                table: "Candidate",
                column: "StateERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCertificateType_CertificateTypeERPRecId",
                table: "CandidateCertificateType",
                column: "CertificateTypeERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducation_EducationERPRecId",
                table: "CandidateEducation",
                column: "EducationERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillERPRecId",
                table: "CandidateSkill",
                column: "SkillERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillLevelERPRecId",
                table: "CandidateSkill",
                column: "SkillLevelERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateType_Code_DataArea",
                table: "CertificateType",
                columns: new[] { "Code", "DataArea" },
                unique: true,
                filter: "[DataArea] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId_CountyId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId", "CountyId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL AND [CountyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateERPRecId",
                table: "City",
                column: "StateERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Education_Code",
                table: "Education",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_Code",
                table: "EducationLevel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_Code",
                table: "Job",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTitle_Code",
                table: "PersonalTitle",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Code",
                table: "Skill",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevel_Code",
                table: "SkillLevel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Code_CountryERPRecId",
                table: "State",
                columns: new[] { "Code", "CountryERPRecId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryERPRecId",
                table: "State",
                column: "CountryERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationChecklistDocument");

            migrationBuilder.DropTable(
                name: "ApplicationCommunicationDocument");

            migrationBuilder.DropTable(
                name: "ApplicationQuestionnaireLineAnswer");

            migrationBuilder.DropTable(
                name: "CandidateCertificateType");

            migrationBuilder.DropTable(
                name: "CandidateEducation");

            migrationBuilder.DropTable(
                name: "CandidateExperience");

            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "ApplicationChecklist");

            migrationBuilder.DropTable(
                name: "ApplicationCommunication");

            migrationBuilder.DropTable(
                name: "ApplicationQuestionnaireLine");

            migrationBuilder.DropTable(
                name: "CertificateType");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SkillLevel");

            migrationBuilder.DropTable(
                name: "ApplicationQuestionnaire");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "EducationLevel");

            migrationBuilder.DropTable(
                name: "PersonalTitle");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
