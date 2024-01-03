using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320228 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "CountyId",
                table: "City",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId_CountyId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId", "CountyId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL AND [CountyId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId_CountyId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "City");

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL");
        }
    }
}
