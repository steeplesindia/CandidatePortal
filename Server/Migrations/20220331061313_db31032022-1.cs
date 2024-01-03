using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.AlterColumn<long>(
                name: "StateERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true,
                filter: "[StateERPRecId] IS NOT NULL AND [CountryERPRecId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.AlterColumn<long>(
                name: "StateERPRecId",
                table: "City",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true);
        }
    }
}
