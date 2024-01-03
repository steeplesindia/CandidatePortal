using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class ChildTableRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CandidateSkill",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CandidateExperience",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CandidateEducation",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CandidateCertificateType",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Application",
                type: "timestamp",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CandidateSkill");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CandidateExperience");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CandidateCertificateType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Application");
        }
    }
}
