using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class ConvertAllToManyToManyAndFixImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$hY1cG.FW2BHHyEIs/isTquDRJ52tifARFFhcwJSxDbSvwM.MJqNjq");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/images/product/EMA-42S.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/images/product/EMA-60.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/images/product/EMA-60s.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/images/product/EMA-STYLE.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$wfSY3sLdW2KaQKIxx7Ch0e.ee9sg0I2VeQw6rZmMwC1W2FfFXhH9m");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/images/product/EMA-42S.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/images/product/EMA-60.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/images/product/EMA-60s.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/images/product/EMA-STYLE.jpg");
        }
    }
}
