using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320224 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City",
                column: "StateERPRecId",
                principalTable: "State",
                principalColumn: "ERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_State_StateERPRecId",
                table: "City",
                column: "StateERPRecId",
                principalTable: "State",
                principalColumn: "ERPRecId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
