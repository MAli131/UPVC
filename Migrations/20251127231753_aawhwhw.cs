using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class aawhwhw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$2QtzbZBcG8q6fIggre7/rOCEKQNq1zty5H2dReYsqulviMubbx/Vq");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "DetailHeroImagePath",
                value: "/images/product/Ema-42s-hero.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "DetailHeroImagePath",
                value: "/images/product/EMA-60-hero.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "DetailHeroImagePath",
                value: "/images/product/EMA-60S-hero.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "DetailHeroImagePath",
                value: "/images/product/EMA-STYLE-hero.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$QBDece6cOLp1D64ffloxI.TRyyVakcO4RznhZX8AnFG3C1kDZMH0K");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "DetailHeroImagePath",
                value: "/images/product/product-d-hero1.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "DetailHeroImagePath",
                value: "/images/product/product-d-hero1.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "DetailHeroImagePath",
                value: "/images/product/product-d-hero1.png");

            migrationBuilder.UpdateData(
                table: "ProductDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "DetailHeroImagePath",
                value: "/images/product/product-d-hero1.png");
        }
    }
}
