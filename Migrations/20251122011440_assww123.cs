using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class assww123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$aZCKrjswRFvM3mZSOQrVC.Szuo5LqBjgBo5CAtg/JgFy1CACO0BMK");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubtitleAr",
                value: "ECONOMIC SLIDING WINDOW SYSTEM");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubtitleAr",
                value: "THE DEFINITIVE CASEMENT WINDOW\nAND DOOR SYSTEM");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubtitleAr",
                value: "STANDARD SLIDING WINDOW SYSTEM");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubtitleAr",
                value: "PREMIUM SLIDING WINDOW AND\nDOOR SYSTEM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ILOvwUmkX64iOFXjBXeF6.VHZluSj44hyxuzASWu2mWPk2Aj.fnG.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubtitleAr",
                value: "البديل الإقتصادي لنظام EMA-60S مخصص للنوافذ الأصغر السعر والأوفر، مع الحفاظ على العزل و المتانة التي تحتاجها لنوافذك.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubtitleAr",
                value: "الإختيار الأمثل لضمان أقصى تهوئة من خلال النوافذ. بالإضافة الى ذلك فإن الإغلاق المحكم للقطاع يوفر عزلا صوتيا أعلى من الأنظمة الاخرى.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubtitleAr",
                value: "الإختيار النموذجي لنوافذ الجرار الخاصة بك، ملائم للفتحات الكبيرة التي تتيح لك الإستمتاع بالمنظر الخارجي. علاوة على ذلك، فإن الحركة الأفقية لأنظمة الجرار تجعلها أكثر ملائمة للمساحات الداخلية المحدودة");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubtitleAr",
                value: "نظام الجرار الجديد و المتطور من EMAPEN, الذي يقدم ضلفة لباب جرار بجودة استثنائية وحلق ببار ٦ سم. يتميز النظام أيضا بأعلى سمك خارجي في مجموعتنا، مما يوفر أقصى درجات المتانة و العزل.");
        }
    }
}
