using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320226 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_StateERPRecId",
                table: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_City_StateERPRecId",
                table: "City",
                column: "StateERPRecId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City",
                column: "StateERPRecId",
                principalTable: "State",
                principalColumn: "ERPRecId");
        }
    }
}
