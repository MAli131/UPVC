using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class HomePageSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePageSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomePageId = table.Column<int>(type: "int", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePageSections_HomePages_HomePageId",
                        column: x => x.HomePageId,
                        principalTable: "HomePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$PwrM1y4eOsV4plL5TsYkg.SyuzHDg095Qtbquiguv0BnNHx9oZVWO");

            migrationBuilder.InsertData(
                table: "HomePageSections",
                columns: new[] { "Id", "ContentAr", "ContentEn", "DeletedAt", "HomePageId", "ImagePath", "IsActive", "Order", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "بطاقة إنتاجية تزيد عن 5000 طن متري سنويًا، نضمن أن عملياتنا مجهزة لتلبية الطلب المتزايد في السوق بثبات وكفاءة. تم تصميم منشآتنا الصناعية الحديثة لتقديم أنظمة نوافذ uPVC عالية الجودة تتوافق مع المعايير الدولية.", "With a production capacity of over 5,000 metric tons per annum, we ensure that our operations are equipped to meet the growing market demand with consistency and efficiency. Our state-of-the-art manufacturing facilities are designed to deliver high-quality uPVC window systems that comply with international standards.", null, 3, "~/images/home/trophy.png", true, 1, "أكبر منشأة للبثق", "Largest extrusion facility", null },
                    { 2, "مع أكثر من 30 ملف تعريف متميز في مجموعتنا، نحن قادرون على تلبية المتطلبات المتنوعة للسوق وتوفير حلول مصممة خصيصًا للتحديات الفريدة لكل عميل. سواء كان المشروع يتطلب أداءً حديثًا أنيقًا، أو متانة للخدمة الشاقة، أو جماليات، أو عزلًا فائقًا، فإن خط إنتاجنا الشامل يضمن أن هناك دائمًا حلًا مثاليًا.", "With over 30 distinct profiles in our range, we are able to address the diverse requirements of the market and provide solutions tailored to the unique challenges of each client. Whether the project demands sleek modern performance, or heavy-duty durability, aesthetics, superior insulation our comprehensive product line ensures that there is always a perfect fit.", null, 3, "~/images/home/trophy.png", true, 2, "أكبر عدد من الملفات", "Largest number of profiles", null },
                    { 3, "مع أكثر من 50 موردًا منتشرين في جميع أنحاء البلاد، أصبحت EMAPEN الخيار الأول للسوق المحلي بفضل شبكة التوزيع الواسعة. وهذا يضمن الوصول السريع والموثوق إلى منتجاتنا ويعكس أيضًا الثقة والشراكات القوية التي بنيناها داخل الصناعة.", "With over 50 suppliers spread across the country, EMAPEN has become the profile extensive distribution network not only of choice for the local market. This ensures fast and reliable access to our products but also reflects the strong trust and partnerships we have built within the industry.", null, 3, "~/images/home/trophy.png", true, 3, "أكبر شبكة موردين", "Largest network of suppliers", null }
                });

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContentAr", "ContentEn", "SubtitleAr", "SubtitleEn" },
                values: new object[] { "", "", "( بالنسبة للسوق المصري )", "( Relative to the Egyptian market )" });

            migrationBuilder.CreateIndex(
                name: "IX_HomePageSections_HomePageId",
                table: "HomePageSections",
                column: "HomePageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePageSections");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$WCEx9Z234zDnS5hnbyCD2.T4kQah0VQZsbG/wYljxS.JiKcYbhPE6");

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContentAr", "ContentEn", "SubtitleAr", "SubtitleEn" },
                values: new object[] { "أكبر منشأة بثق، أكبر عدد من الملفات الشخصية، أكبر شبكة من الموردين.", "Largest extrusion facility, largest number of profiles, largest network of suppliers.", "الرائد في السوق", "Market Leader" });
        }
    }
}
