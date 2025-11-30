using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class ssswwq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutSection_AboutPages_AboutPageId",
                table: "AboutSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboutSection",
                table: "AboutSection");

            migrationBuilder.RenameTable(
                name: "AboutSection",
                newName: "AboutSections");

            migrationBuilder.RenameIndex(
                name: "IX_AboutSection_AboutPageId",
                table: "AboutSections",
                newName: "IX_AboutSections_AboutPageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboutSections",
                table: "AboutSections",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$AHU7I2zj6hdrMrB5B/rtROscmSOjf5i7Ugu8oirrsqndKCsUhk28C");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutSections_AboutPages_AboutPageId",
                table: "AboutSections",
                column: "AboutPageId",
                principalTable: "AboutPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutSections_AboutPages_AboutPageId",
                table: "AboutSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboutSections",
                table: "AboutSections");

            migrationBuilder.RenameTable(
                name: "AboutSections",
                newName: "AboutSection");

            migrationBuilder.RenameIndex(
                name: "IX_AboutSections_AboutPageId",
                table: "AboutSection",
                newName: "IX_AboutSection_AboutPageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboutSection",
                table: "AboutSection",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$jZSi2UY5j7D1UrE69o0KvuOMfw4MOSzp6lBSDnERdarrK1VZ.bMMC");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutSection_AboutPages_AboutPageId",
                table: "AboutSection",
                column: "AboutPageId",
                principalTable: "AboutPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
