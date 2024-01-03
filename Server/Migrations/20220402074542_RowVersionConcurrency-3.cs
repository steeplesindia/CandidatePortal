using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class RowVersionConcurrency3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                nullable: true,
                defaultValue: new byte[0]
                //oldClrType: typeof(byte[]),
                //oldType: "timestamp",
                //oldNullable: true
                );

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "RowVersion",
            //    table: "Candidate",
            //    type: "timestamp",
            //    rowVersion: true,
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldType: "timestamp",
            //    oldRowVersion: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Candidate",
                type: "timestamp",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "timestamp",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
