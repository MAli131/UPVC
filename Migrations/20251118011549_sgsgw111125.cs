using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class sgsgw111125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xsMiR4KivUm1A5q2act1OOMVNxZcXaf0IeRnmjdrf605642j3NVzy");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "~/images/home2/adv1.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "~/images/home2/adv2.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "~/images/home2/adv3.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "~/images/home2/adv4.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "~/images/home2/adv5.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$UKgYiang7hJQJCmNE5FL3eWpdjB3cN3/SlRuCCy1FDV6XsLJGpdk.");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "~/images/home/hero2-icon1.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "~/images/home/hero2-icon2.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "~/images/home/hero2-icon3.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "~/images/home/hero2-icon4.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "~/images/home/hero2-icon5.png");
        }
    }
}
