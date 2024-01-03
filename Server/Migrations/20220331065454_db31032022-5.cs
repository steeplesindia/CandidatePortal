using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId",
                principalTable: "Country",
                principalColumn: "ERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId",
                principalTable: "Country",
                principalColumn: "ERPRecId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
