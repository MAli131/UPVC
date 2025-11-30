using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class agwgwg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatbotFAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QuestionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AnswerAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatbotFAQs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$CtHbD316jAfNOxWoNP6JVuR32AzkXdUTYIee0Rh0oUMKmmD7Wcyba");

            migrationBuilder.InsertData(
                table: "ChatbotFAQs",
                columns: new[] { "Id", "AnswerAr", "AnswerEn", "Category", "CreatedAt", "DeletedAt", "DisplayOrder", "IsActive", "QuestionAr", "QuestionEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "أنظمة uPVC هي أنظمة نوافذ وأبواب مصنوعة من البولي فينيل كلورايد غير الملدن، وهي مادة متينة ومقاومة للعوامل الجوية وعازلة للحرارة والصوت.", "uPVC systems are window and door systems made from unplasticized polyvinyl chloride, a durable material that is weather-resistant and provides excellent thermal and sound insulation.", "General", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, true, "ما هي أنظمة uPVC؟", "What are uPVC systems?", null },
                    { 2, "منتجات EMAPEN توفر عزلاً حرارياً وصوتياً ممتازاً، مقاومة عالية للعوامل الجوية، سهولة في الصيانة، وعمر افتراضي طويل يصل إلى 50 عاماً.", "EMAPEN products offer excellent thermal and sound insulation, high resistance to weather conditions, easy maintenance, and a long lifespan of up to 50 years.", "Products", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, true, "ما هي مميزات منتجات EMAPEN؟", "What are the advantages of EMAPEN products?", null },
                    { 3, "يمكنك التواصل معنا عبر صفحة 'اتصل بنا' أو الاتصال بنا مباشرة على الخط الساخن. فريقنا سيساعدك في اختيار المنتج المناسب.", "You can contact us through the 'Contact Us' page or call us directly on our hotline. Our team will help you choose the right product.", "Orders", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, true, "كيف يمكنني طلب منتجاتكم؟", "How can I order your products?", null },
                    { 4, "نعم، نوفر خدمة التركيب من خلال فريق فني متخصص ومدرب على أعلى مستوى.", "Yes, we provide installation services through a specialized technical team trained to the highest standards.", "Services", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, true, "هل تقدمون خدمة التركيب؟", "Do you provide installation service?", null },
                    { 5, "نقدم ضماناً شاملاً على جميع منتجاتنا. تختلف مدة الضمان حسب نوع المنتج ويمكن أن تصل إلى 10 سنوات.", "We offer comprehensive warranty on all products. The warranty period varies by product type and can extend up to 10 years.", "Warranty", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, true, "ما هي مدة الضمان على المنتجات؟", "What is the warranty period?", null },
                    { 6, "نعم، منتجات EMAPEN مصممة خصيصاً لتتحمل المناخات الحارة والرطبة مع مقاومة عالية للأشعة فوق البنفسجية.", "Yes, EMAPEN products are specifically designed to withstand hot and humid climates with high UV resistance.", "Technical", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 6, true, "هل المنتجات مناسبة للمناخ الحار؟", "Are products suitable for hot climates?", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatbotFAQs");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$AHU7I2zj6hdrMrB5B/rtROscmSOjf5i7Ugu8oirrsqndKCsUhk28C");
        }
    }
}
