using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class sgsgw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4c/xp.IMmofZ3p8IXPfTT.ofRvclgQPk2hlncTbOSDrbUQWHkYr4e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$BB/bCSdaoO8Zb.jiHh.Tj.aq38fjj.Fn/8P1oOxU85IZFaDk0CcHy");
        }
    }
}
