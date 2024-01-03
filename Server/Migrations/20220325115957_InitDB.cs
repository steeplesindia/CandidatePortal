using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class InitDB : Migration
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.ERPRecId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    CountryERPRecId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((0))"),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    StateERPRecId = table.Column<long>(type: "bigint", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ERPRecId);
                    table.ForeignKey(
                        name: "FK_City_State_StateERPRecId",
                        column: x => x.StateERPRecId,
                        principalTable: "State",
                        principalColumn: "ERPRecId",
                        onDelete: ReferentialAction.Cascade);
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
                    PreviousEmployee = table.Column<bool>(type: "bit", nullable: true),
                    CountryERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    StateERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    CityERPRecId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((0))"),
                    Street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AlternateContactNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ResumeURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FaceBookLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LinkdinLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<short>(type: "smallint", nullable: true),
                    MaritalStatus = table.Column<short>(type: "smallint", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    IsWillTravel = table.Column<bool>(type: "bit", nullable: true),
                    IsWillRelocated = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    ApplicationStatus = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RenewalRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCertificateType", x => new { x.CandidateNumber, x.CertificateTypeERPRecId });
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
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AvgGrade = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValueSql: "((0))"),
                    Scale = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValueSql: "((0))"),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmployerURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EmployerContactNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmployerLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExperience", x => new { x.CandidateNumber, x.Employer });
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
                    SkillLevelERPRecId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((0))"),
                    LevelDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    YearOfExperience = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValueSql: "((0))"),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    DataArea = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ERPRecId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => new { x.CandidateNumber, x.SkillERPRecId });
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
                name: "IX_Candidate_CityERPRecId",
                table: "Candidate",
                column: "CityERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CountryERPRecId",
                table: "Candidate",
                column: "CountryERPRecId");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_DataArea",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_StateERPRecId",
                table: "City",
                column: "StateERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code_DataArea",
                table: "Country",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Education_Code_DataArea",
                table: "Education",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_Code_DataArea",
                table: "Job",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTitle_Code_DataArea",
                table: "PersonalTitle",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Code_DataArea",
                table: "Skill",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevel_Code_DataArea",
                table: "SkillLevel",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Code_CountryERPRecId_DataArea",
                table: "State",
                columns: new[] { "Code", "CountryERPRecId", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryERPRecId",
                table: "State",
                column: "CountryERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "CandidateCertificateType");

            migrationBuilder.DropTable(
                name: "CandidateEducation");

            migrationBuilder.DropTable(
                name: "CandidateExperience");

            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "CertificateType");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SkillLevel");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "PersonalTitle");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
