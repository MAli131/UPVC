using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class SocialMediaPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Platform",
                table: "SocialMedias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$BB/bCSdaoO8Zb.jiHh.Tj.aq38fjj.Fn/8P1oOxU85IZFaDk0CcHy");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Platform",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Platform",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 3,
                column: "Platform",
                value: 3);

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 4,
                column: "Platform",
                value: 4);

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 5,
                column: "Platform",
                value: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Platform",
                table: "SocialMedias",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$PwrM1y4eOsV4plL5TsYkg.SyuzHDg095Qtbquiguv0BnNHx9oZVWO");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Platform",
                value: "Facebook");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Platform",
                value: "Twitter");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 3,
                column: "Platform",
                value: "Instagram");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 4,
                column: "Platform",
                value: "LinkedIn");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 5,
                column: "Platform",
                value: "WhatsApp");
        }
    }
}
