using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class CopyrightText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CopyrightTextAr",
                table: "CompanyInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CopyrightTextEn",
                table: "CompanyInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$WCEx9Z234zDnS5hnbyCD2.T4kQah0VQZsbG/wYljxS.JiKcYbhPE6");

            migrationBuilder.UpdateData(
                table: "CompanyInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CopyrightTextAr", "CopyrightTextEn" },
                values: new object[] { "إيمابن جميع الحقوق محفوظة", "EMAPEN all rights reserved" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyrightTextAr",
                table: "CompanyInfos");

            migrationBuilder.DropColumn(
                name: "CopyrightTextEn",
                table: "CompanyInfos");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$zyaMFdos2IAoJi4sNOlGs./AMnAHKgLIdKLBa0jj6T6xVIC2Qs2cm");
        }
    }
}
