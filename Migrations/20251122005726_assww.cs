using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class assww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrochurePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubtitleAr",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubtitleEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ILOvwUmkX64iOFXjBXeF6.VHZluSj44hyxuzASWu2mWPk2Aj.fnG.");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrochurePath", "CategoryAr", "CategoryEn", "CreatedAt", "DeletedAt", "DescriptionAr", "DescriptionEn", "DetailsAr", "DetailsEn", "DisplayOrder", "GalleryImagesJson", "ImagePath", "IsActive", "NameAr", "NameEn", "SubtitleAr", "SubtitleEn", "ThumbnailPath", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "/files/brochure/EMA-42S.pdf", null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "البديل الإقتصادي لنظام EMA-60S مخصص للنوافذ الأصغر السعر والأوفر، مع الحفاظ على العزل و المتانة التي تحتاجها لنوافذك.", "The economic alternative to EMA60S, dedicated for smaller sliding windows and smaller budgets while still maintaining the exceptional quality you need for your windows.", null, null, 1, null, "/images/product/EMA-42S.jpg", true, "EMA-42S", "Ema-42s", "البديل الإقتصادي لنظام EMA-60S مخصص للنوافذ الأصغر السعر والأوفر، مع الحفاظ على العزل و المتانة التي تحتاجها لنوافذك.", "ECONOMIC SLIDING WINDOW SYSTEM", null, null },
                    { 2, "/files/brochure/EMA-60.pdf", null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "الإختيار الأمثل لضمان أقصى تهوئة من خلال النوافذ. بالإضافة الى ذلك فإن الإغلاق المحكم للقطاع يوفر عزلا صوتيا أعلى من الأنظمة الاخرى.", "Your profile of choice for ensuring maximum ventilation through windows. Moreover, the casement's tight seal provides it with a superior sound insulation compared to its counterparts.", null, null, 2, null, "/images/product/EMA-60.jpg", true, "EMA-60", "EMA-60", "الإختيار الأمثل لضمان أقصى تهوئة من خلال النوافذ. بالإضافة الى ذلك فإن الإغلاق المحكم للقطاع يوفر عزلا صوتيا أعلى من الأنظمة الاخرى.", "THE DEFINITIVE CASEMENT WINDOW\nAND DOOR SYSTEM", null, null },
                    { 3, "/files/brochure/EMA-60S.pdf", null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "الإختيار النموذجي لنوافذ الجرار الخاصة بك، ملائم للفتحات الكبيرة التي تتيح لك الإستمتاع بالمنظر الخارجي. علاوة على ذلك، فإن الحركة الأفقية لأنظمة الجرار تجعلها أكثر ملائمة للمساحات الداخلية المحدودة", "The default option for your sliding windows, ideal when opting for wide unobstructed views. Moreover, the horizontal movement of the sliding systems makes them more suitable for limited interior spaces.", null, null, 3, null, "/images/product/EMA-60s.jpg", true, "EMA-60S", "EMA-60S", "الإختيار النموذجي لنوافذ الجرار الخاصة بك، ملائم للفتحات الكبيرة التي تتيح لك الإستمتاع بالمنظر الخارجي. علاوة على ذلك، فإن الحركة الأفقية لأنظمة الجرار تجعلها أكثر ملائمة للمساحات الداخلية المحدودة", "STANDARD SLIDING WINDOW SYSTEM", null, null },
                    { 4, "/files/brochure/EMA-STYLE.pdf", null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "نظام الجرار الجديد و المتطور من EMAPEN, الذي يقدم ضلفة لباب جرار بجودة استثنائية وحلق ببار ٦ سم. يتميز النظام أيضا بأعلى سمك خارجي في مجموعتنا، مما يوفر أقصى درجات المتانة و العزل.", "EMAPEN's new and refined sliding system, introducing a sliding door sash of exceptional quality and a 60mm built-in bar. The system also boasts the highest profile thickness in the suite, giving you the highest durability and insulation.", null, null, 4, null, "/images/product/EMA-STYLE.jpg", true, "EMA-STYLE", "EMA-STYLE", "نظام الجرار الجديد و المتطور من EMAPEN, الذي يقدم ضلفة لباب جرار بجودة استثنائية وحلق ببار ٦ سم. يتميز النظام أيضا بأعلى سمك خارجي في مجموعتنا، مما يوفر أقصى درجات المتانة و العزل.", "PREMIUM SLIDING WINDOW AND\nDOOR SYSTEM", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "BrochurePath",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubtitleAr",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubtitleEn",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$lMKtxCf8360VjGqvIE4O5.vWsK2Xzd/b9jJw95GMmyyUinV5rrhFm");
        }
    }
}
