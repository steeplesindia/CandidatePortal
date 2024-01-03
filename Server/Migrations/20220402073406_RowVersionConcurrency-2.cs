using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class RowVersionConcurrency2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //DropColumn("dbo.Cities", "RowVersion", null);
            //AddColumn("dbo.Cities", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Candidate",
                schema: null
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Candidate",
                type: "timestamp",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]
                //oldClrType: typeof(byte[]),
                //oldType: "timestamp",
                //oldNullable: true
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Candidate",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "timestamp",
                oldRowVersion: true);
        }
    }
}
