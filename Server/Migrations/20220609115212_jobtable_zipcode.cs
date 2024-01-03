using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class jobtable_zipcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDeadline",
                table: "Job",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locations",
                table: "Job",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfOpenings",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "Job",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "PortalLink",
                table: "Job",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PublishedToPortal",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Job",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Job",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Candidate",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_HighestDegreeERPRecId",
                table: "Candidate",
                column: "HighestDegreeERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_Code",
                table: "EducationLevel",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_EducationLevel_HighestDegreeERPRecId",
                table: "Candidate",
                column: "HighestDegreeERPRecId",
                principalTable: "EducationLevel",
                principalColumn: "ERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_EducationLevel_HighestDegreeERPRecId",
                table: "Candidate");

            migrationBuilder.DropTable(
                name: "EducationLevel");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_HighestDegreeERPRecId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "ApplicationDeadline",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Locations",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "NoOfOpenings",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PortalLink",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PublishedToPortal",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
