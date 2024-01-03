using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class LabelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Candidate",
                newName: "Disabled");

            migrationBuilder.RenameColumn(
                name: "CanRelocated",
                table: "Candidate",
                newName: "CanRelocate");

            migrationBuilder.AddColumn<bool>(
                name: "IsSynced",
                table: "Application",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSynced",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "Disabled",
                table: "Candidate",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "CanRelocate",
                table: "Candidate",
                newName: "CanRelocated");
        }
    }
}
