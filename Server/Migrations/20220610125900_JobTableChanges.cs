using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class JobTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "PublishedToPortal",
                table: "Job",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsSynced",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"CREATE PROCEDURE write_watermark @LastModifiedtime datetime, @TableName varchar(250)
                                AS
                                BEGIN
                                DELETE FROM WatermarkTable
                                WHERE [TableName] = @TableName

                                INSERT INTO WatermarkTable ([WatermarkValue], [TableName]) VALUES (@LastModifiedtime, @TableName)
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSynced",
                table: "Job");

            migrationBuilder.AlterColumn<bool>(
                name: "PublishedToPortal",
                table: "Job",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "Job",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.Sql(@"drop procedure write_watermark");
        }
    }
}
