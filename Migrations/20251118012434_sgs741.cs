using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class sgs741 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$lMKtxCf8360VjGqvIE4O5.vWsK2Xzd/b9jJw95GMmyyUinV5rrhFm");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/images/home2/adv1.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "/images/home2/adv2.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "/images/home2/adv3.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/images/home2/adv4.png");

            migrationBuilder.UpdateData(
                table: "HomePageSections",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/images/home2/adv5.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
