using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class aawhwhw11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$2CcCMd5Cvk8yzXU14GVuXuyEVA/KQsJD.85fAaHkSgwyAz5PC5WzK");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/images/product/win8.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/images/product/win9.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagePath",
                value: "/images/product/win10.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagePath",
                value: "/images/product/win11.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImagePath",
                value: "/images/product/win12.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImagePath",
                value: "/images/product/win13.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImagePath",
                value: "/images/product/win14.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImagePath",
                value: "/images/product/win7.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$2QtzbZBcG8q6fIggre7/rOCEKQNq1zty5H2dReYsqulviMubbx/Vq");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/images/product/win7.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/images/product/win8.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagePath",
                value: "/images/product/win9.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagePath",
                value: "/images/product/win10.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImagePath",
                value: "/images/product/win11.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImagePath",
                value: "/images/product/win12.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImagePath",
                value: "/images/product/win13.png");

            migrationBuilder.UpdateData(
                table: "DesignOptions",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImagePath",
                value: "/images/product/win14.png");
        }
    }
}
