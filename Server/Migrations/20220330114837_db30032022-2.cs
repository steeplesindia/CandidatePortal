using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatePortal.Server.Migrations
{
    public partial class db300320222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_State_Code_CountryERPRecId_DataArea",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_SkillLevel_Code_DataArea",
                table: "SkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_PersonalTitle_Code_DataArea",
                table: "PersonalTitle");

            migrationBuilder.DropIndex(
                name: "IX_Job_Code_DataArea",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Education_Code_DataArea",
                table: "Education");

            migrationBuilder.DropIndex(
                name: "IX_Country_Code_DataArea",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_DataArea",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_CertificateType_Code_DataArea",
                table: "CertificateType");

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "State",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "SkillLevel",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "PersonalTitle",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Job",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Education",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Country",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "City",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<long>(
                name: "CountryERPRecId",
                table: "City",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "CertificateType",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_State_Code_CountryERPRecId",
                table: "State",
                columns: new[] { "Code", "CountryERPRecId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevel_Code",
                table: "SkillLevel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTitle_Code",
                table: "PersonalTitle",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_Code",
                table: "Job",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Education_Code",
                table: "Education",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "CountryERPRecId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateType_Code_DataArea",
                table: "CertificateType",
                columns: new[] { "Code", "DataArea" },
                unique: true,
                filter: "[DataArea] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City",
                column: "CountryERPRecId",
                principalTable: "Country",
                principalColumn: "ERPRecId"
                //onDelete: ReferentialAction.Cascade
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_State_Code_CountryERPRecId",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_SkillLevel_Code",
                table: "SkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_PersonalTitle_Code",
                table: "PersonalTitle");

            migrationBuilder.DropIndex(
                name: "IX_Job_Code",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Education_Code",
                table: "Education");

            migrationBuilder.DropIndex(
                name: "IX_Country_Code",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_City_Code_StateERPRecId_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_CountryERPRecId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_CertificateType_Code_DataArea",
                table: "CertificateType");

            migrationBuilder.DropColumn(
                name: "CountryERPRecId",
                table: "City");

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "State",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "SkillLevel",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "PersonalTitle",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Job",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Education",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "Country",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "City",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataArea",
                table: "CertificateType",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Code_CountryERPRecId_DataArea",
                table: "State",
                columns: new[] { "Code", "CountryERPRecId", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevel_Code_DataArea",
                table: "SkillLevel",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTitle_Code_DataArea",
                table: "PersonalTitle",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_Code_DataArea",
                table: "Job",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Education_Code_DataArea",
                table: "Education",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code_DataArea",
                table: "Country",
                columns: new[] { "Code", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Code_StateERPRecId_DataArea",
                table: "City",
                columns: new[] { "Code", "StateERPRecId", "DataArea" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificateType_Code_DataArea",
                table: "CertificateType",
                columns: new[] { "Code", "DataArea" },
                unique: true);
        }
    }
}
