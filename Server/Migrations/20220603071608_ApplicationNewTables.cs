using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class ApplicationNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateSkill",
                table: "CandidateSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateCertificateType",
                table: "CandidateCertificateType");

            //migrationBuilder.RenameColumn(
            //    name: "IsWillTravel",
            //    table: "Candidate",
            //    newName: "CanTravel");

            //migrationBuilder.RenameColumn(
            //    name: "IsWillRelocated",
            //    table: "Candidate",
            //    newName: "CanRelocated");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LevelDate",
                table: "CandidateSkill",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "CandidateSkill",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CandidateExperience",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "CandidateExperience",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "CandidateEducation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CandidateCertificateType",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "CandidateCertificateType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateSkill",
                table: "CandidateSkill",
                columns: new[] { "CandidateNumber", "SkillERPRecId", "LevelDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience",
                columns: new[] { "CandidateNumber", "Employer", "StartDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateCertificateType",
                table: "CandidateCertificateType",
                columns: new[] { "CandidateNumber", "CertificateTypeERPRecId", "StartDate" });

            migrationBuilder.CreateTable(
                name: "ApplicationChecklist",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
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
                    CommunicationDirection = table.Column<short>(type: "smallint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
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
                name: "ApplicationChecklistDocument",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationChecklistNumber = table.Column<long>(type: "bigint", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
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
                    DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationChecklistDocument");

            migrationBuilder.DropTable(
                name: "ApplicationCommunicationDocument");

            migrationBuilder.DropTable(
                name: "ApplicationChecklist");

            migrationBuilder.DropTable(
                name: "ApplicationCommunication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateSkill",
                table: "CandidateSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateCertificateType",
                table: "CandidateCertificateType");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CandidateSkill");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CandidateExperience");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CandidateCertificateType");

            //migrationBuilder.RenameColumn(
            //    name: "CanTravel",
            //    table: "Candidate",
            //    newName: "IsWillTravel");

            //migrationBuilder.RenameColumn(
            //    name: "CanRelocated",
            //    table: "Candidate",
            //    newName: "IsWillRelocated");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LevelDate",
                table: "CandidateSkill",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CandidateExperience",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CandidateCertificateType",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateSkill",
                table: "CandidateSkill",
                columns: new[] { "CandidateNumber", "SkillERPRecId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience",
                columns: new[] { "CandidateNumber", "Employer" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateCertificateType",
                table: "CandidateCertificateType",
                columns: new[] { "CandidateNumber", "CertificateTypeERPRecId" });
        }
    }
}
