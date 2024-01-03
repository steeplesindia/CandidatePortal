using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db30032022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skill_Code_DataArea",
                table: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Code",
                table: "Skill",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skill_Code",
                table: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Code_DataArea",
                table: "Skill",
                columns: new[] { "Code", "DataArea" },
                unique: true);
        }
    }
}
