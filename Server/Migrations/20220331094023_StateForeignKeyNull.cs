using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class StateForeignKeyNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateERPRecId",
                table: "City",
                column: "StateERPRecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_StateERPRecId",
                table: "City");

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL AND [CountryERPRecId] IS NOT NULL");
        }
    }
}
