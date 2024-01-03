using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_CountryERPRecId",
                table: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_City_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId",
                principalTable: "Country",
                principalColumn: "ERPRecId");
        }
    }
}
