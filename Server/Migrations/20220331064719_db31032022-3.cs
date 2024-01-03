using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db310320223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StateERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StateERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "((0))");
        }
    }
}
