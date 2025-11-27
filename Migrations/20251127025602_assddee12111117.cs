using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class assddee12111117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$GYtXhi6VHlaZnW.hvwDUnekZnQdwmxK4zEZ4PUJMVyrzVbKnLXGUm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$hY1cG.FW2BHHyEIs/isTquDRJ52tifARFFhcwJSxDbSvwM.MJqNjq");
        }
    }
}
