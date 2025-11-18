using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class sgsgw111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubtitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubtitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingHoursJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaviconPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SloganEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SloganAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyrightTextEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyrightTextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentOtherEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentOtherAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailsEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GalleryImagesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutPageId = table.Column<int>(type: "int", nullable: false),
                    SectionType = table.Column<int>(type: "int", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutSection_AboutPages_AboutPageId",
                        column: x => x.AboutPageId,
                        principalTable: "AboutPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleHighlightEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubtitleHighlightAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentHighlightEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentHighlightAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentOtherEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentOtherAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

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

            migrationBuilder.InsertData(
                table: "AboutPages",
                columns: new[] { "Id", "ContentAr", "ContentEn", "DeletedAt", "ImagePath", "IsActive", "PageKey", "SubtitleAr", "SubtitleEn", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[] { 1, "تفتخر إيمابن بامتلاكها أكبر شبكة موزعين على مستوى الجمهورية، مما جعلها الخيار الأول والمفضل في العديد من مشاريع البناء الكبرى. ويأتي هذا الانتشار الواسع كدليل على الثقة التي اكتسبتها الشركة بين شركائها في السوق المحلي، بفضل التزامها بالمواعيد وجودة منتجاتها العالية. كما تضم الشركة أكبر منشأة بثق في مصر، مزوّدة بسبعة خطوط إنتاج تعمل بطاقة تصل إلى 500 طن متري شهريًا، مما يمكّنها من تلبية الطلبات المتزايدة بكفاءة واستقرار في الإنتاج.", "EMAPEN boasts the largest network of distributors in the country, proving itself as the profile of choice for many construction projects. Additionally, the company features Egypt's largest extrusion facility, equipped with seven production lines and a capacity of 500 metric tons per month.", null, null, true, "About", "تُصنّع قطاعات إيمابن وفقًا لمواصفات ISO 9001:2015 العالمية، مما يضمن التزام الشركة بأعلى معايير الجودة في جميع مراحل التصنيع والإدارة. ويُترجم هذا الالتزام إلى عمليات إنتاج دقيقة ومنتجات ذات أداء ثابت وموثوق، تعكس رؤية إيمابن في توفير حلول متكاملة تجمع بين المتانة، والكفاءة، والجمال في آنٍ واحد.", "EMAPEN's profiles conform to the ISO 9001:2015 standard, ensuring consistent manufacturing processes and product performance.", "منذ تأسيسها عام 2015، أثبتت شركة إيمابن بشكلٍ متواصل موثوقية قطاعاتها في مئات المشاريع المتنوعة، مما رسّخ مكانتها كواحدة من أبرز الشركات الرائدة في تصنيع أنظمة الـuPVC في مصر. وقد جاءت هذه الريادة نتيجة التزام الشركة الدائم بالجودة، والابتكار في التصميم، والاستمرارية في تحسين الأداء لتلبية احتياجات السوق المتغيرة ومواكبة أحدث التطورات في مجال صناعة النوافذ والأبواب.", "Since its establishment in 2015, EMAPEN has consistently demonstrated the reliability of its profiles across a multitude of projects, thereby solidifying its standing as one of the leading manufacturers of UPVC in Egypt.", null });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "IsActive", "LastLoginAt", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@emapen.com", true, null, "$2a$11$UKgYiang7hJQJCmNE5FL3eWpdjB3cN3/SlRuCCy1FDV6XsLJGpdk.", null, "admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "NameAr", "NameEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "مصنع نوافذ", "Window manufacturer (Egypt)", null },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "موزع", "Supplier", null },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "مهندس", "Architect", null },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "صاحب منزل", "Home owner", null },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "آخر", "other", null }
                });

            migrationBuilder.InsertData(
                table: "CompanyInfos",
                columns: new[] { "Id", "AddressAr", "AddressEn", "CopyrightTextAr", "CopyrightTextEn", "CreatedAt", "DeletedAt", "DescriptionAr", "DescriptionEn", "Email", "FaviconPath", "IsActive", "LogoPath", "Mobile", "NameAr", "NameEn", "Phone", "SloganAr", "SloganEn", "UpdatedAt", "WorkingHoursJson" },
                values: new object[] { 1, "مدينة 6 أكتوبر، المنطقة الصناعية 1، قطعة رقم 238 - الجيزة، مصر", "6th of October City Industrial Zone 1, Land no.238 - Giza, Egypt", "إيمابن جميع الحقوق محفوظة", "EMAPEN all rights reserved", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "المزود الرائد لحلول النوافذ والأبواب uPVC", "Leading provider of uPVC windows and doors solutions", "info@emapen.net", null, true, null, "+201069946220", "إيمابن", "EMAPEN", "15726", "أنظمة EMAPEN لـ uPVC للأبواب والنوافذ", "EMAPEN uPVC systems for doors and windows", null, null });

            migrationBuilder.InsertData(
                table: "ContactPages",
                columns: new[] { "Id", "AddressAr", "AddressEn", "CategoryId", "ContentAr", "ContentEn", "ContentHighlightAr", "ContentHighlightEn", "ContentOtherAr", "ContentOtherEn", "CreatedAt", "DeletedAt", "Email", "ImagePath", "IsActive", "MapUrl", "PhoneNumber", "SubtitleAr", "SubtitleEn", "SubtitleHighlightAr", "SubtitleHighlightEn", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[] { 1, null, null, null, "مصنع نوافذ، موزع، مهندس معماري، صاحب منزل، مهما كانت فئتك،", "Window manufacturer, Supplier, Architect, Home owner, no matter what your category is,", "يسعدنا دعمك.", "we would be glad to support you.", null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, null, null, "سواء كانت لديك أي استفسارات حول منتجاتنا أو كنت تفكر في تقديم طلب،", "Whether you have any questions regarding our products or you're considering making an order,", "لا تتردد في الاتصال بنا.", "feel free to contact us.", "اتصل بنا", "Contact Us", null });

            migrationBuilder.InsertData(
                table: "HomePages",
                columns: new[] { "Id", "ContentAr", "ContentEn", "ContentOtherAr", "ContentOtherEn", "DeletedAt", "ImagePath", "IsActive", "MetaDataJson", "PageKey", "SecondaryImagePath", "SubtitleAr", "SubtitleEn", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "الشركة الرائدة في تصنيع أنظمة نوافذ و أبواب الuPVC في مصر و القارة الأفريقية، والتي أثبتت كفائتها في مشاريع متعددة، مستندة إلى أكثر من عقد من الخبرة في الصناعة.", "The leading manufacturer of uPVC window and door systems in Egypt and the African region, demonstrating the reliability of its profiles across a multitude of projects with over a decade of experience in the industry.", "", "", null, null, true, null, "Index", null, "تصفح منتجاتنا", "Check our product line", "من قطاع متين تأتي نوافذ بعمر طويل", "Our profiles ensure that your windows will endure", null },
                    { 2, "نظرًا لأن التوصيل الحراري لمادة الـ uPVC أقل بكثير من الألومنيوم، فهي تتمتع بخصائص <span>عزل حراري</span> فائقة. يساعد هذا العزل الحراري في تقليل <span>فقدان الحرارة</span> خلال فصل الشتاء، ويحدّ من <span>اكتساب الحرارة</span> في الصيف، مما يقلل الاعتماد على أنظمة التدفئة والتبريد الاصطناعية، ويجعلها المادة المثالية لتوفير الطاقة و <span>خفض فواتير الخدمات</span>.", "With a thermal conductivity significantly lower than aluminum, uPVC possesses superior <span>thermal insulation</span> properties. This thermal insulation <span>reduces heat loss</span> during winter and minimizes heat gain during summer, reducing the reliance on artificial heating and cooling systems and making it the material of choice for saving energy and <span>lowering utility bills</span>.", "", "", null, null, true, null, "Home2", null, "موفر للطاقة", "Energy efficiency", "نافذة المستقبل", "The window of the future", null },
                    { 3, "", "", "", "", null, null, true, null, "Home3", null, "( بالنسبة للسوق المصري )", "( Relative to the Egyptian market )", "الأكبر في مصر", "Largest in Egypt", null },
                    { 4, "كما تُعرف إيمابن بقدرتها الإنتاجية العالية والتزامها الدائم بتلبية المتطلبات الكبيرة للمشروعات الضخمة، مع الحرص على الالتزام بالمعايير الدولية في كل مرحلة من مراحل التصنيع والتنفيذ.", "EMAPEN is also known for its high production capacity, consistently meeting the high demands of many construction projects.", "تحتوي القائمة التالية على مجموعة من أبرز المشاريع الكبرى التي تم تزويدها بقطاعات إيمابن، والتي تعكس الثقة المتبادلة بين الشركة وشركائها في قطاع البناء.", "The following is some of the large-scale projects that have specified our profiles.", null, null, true, null, "Home4", null, "تُعرف قطاعات إيمابن بأدائها الاستثنائي، حيث نالت ثقة العديد من العملاء، مما رسّخ سمعتها كأحد أفضل القطاعات المتخصصة في تطبيقات النوافذ والأبواب لمختلف مشاريع البناء في مصر وخارجها. وتتميّز منتجات إيمابن بجودتها العالية وثبات أدائها في البيئات المتنوعة، ما جعلها الخيار المفضل لدى كبرى شركات التطوير العقاري والمقاولين.", "Renowned for their exceptional performance, EMAPEN’s profiles have garnered the trust of numerous clients, firmly establishing our reputation as a preferred choice for window and door applications in a diverse set of construction projects.", "المشاريع المكتملة", "Projects completed", null }
                });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DisplayOrder", "IconClass", "IsActive", "Platform", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, "bi bi-facebook", true, 1, null, "https://www.facebook.com/share/1Bhcz5o4dt/" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, "bi bi-twitter", true, 2, null, "https://twitter.com/emapen" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, "bi bi-instagram", true, 3, null, "https://www.instagram.com/emapen.upvc.egypt?igsh=eG9uMGR5bmttaXZv" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, "bi bi-linkedin", true, 4, null, "https://www.linkedin.com/company/emapen-for-upvc-profile/" },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, "bi bi-whatsapp", true, 5, null, "https://wa.me/201069946220" }
                });

            migrationBuilder.InsertData(
                table: "AboutSection",
                columns: new[] { "Id", "AboutPageId", "ContentAr", "ContentEn", "DeletedAt", "IconPath", "Order", "SectionType", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "هي تقديم قطاعات uPVC عالية الجودة والمتانة، مطابقة للمعايير الدولية، وتلبي احتياجات العملاء في مختلف الأسواق. نحرص على تحقيق ذلك من خلال استخدام أحدث خطوط الإنتاج والمعدات المتطورة، بإشراف فريق من المهندسين والفنيين ذوي الكفاءة العالية، لضمان تحقيق أعلى مستويات الدقة والأداء في كل منتج يحمل اسم إيمابن.", "is to provide uPVC profiles of high quality and durability that satisfy international standards and meets customer demands through advanced machinery and necessary equipment managed by qualified professionals.", null, null, 1, 1, "مهمتنا", "Our mission", null },
                    { 2, 1, "أن نكون المورد الأكثر موثوقية واعتمادًا لقطاعات uPVC في الأسواق المحلية وأسواق التصدير، من خلال ترسيخ اسم إيمابن كرمز للجودة والالتزام والابتكار في هذه الصناعة. نسعى إلى توسيع نطاق أعمالنا إقليميًا ودوليًا، وتعزيز مكانتنا كشريك موثوق يوفّر حلول نوافذ وأبواب متكاملة تلبي أعلى المعايير الفنية والجمالية، بما يضمن رضا العملاء واستدامة الثقة في منتجاتنا.", "is to become the most reliable and trusted supplier of uPVC profiles in both domestic and export markets.", null, null, 2, 2, "رؤيتنا", "Our vision", null }
                });

            migrationBuilder.InsertData(
                table: "HomePageSections",
                columns: new[] { "Id", "ContentAr", "ContentEn", "DeletedAt", "HomePageId", "ImagePath", "IsActive", "Order", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "بطاقة إنتاجية تزيد عن 5000 طن متري سنويًا، نضمن أن عملياتنا مجهزة لتلبية الطلب المتزايد في السوق بثبات وكفاءة. تم تصميم منشآتنا الصناعية الحديثة لتقديم أنظمة نوافذ uPVC عالية الجودة تتوافق مع المعايير الدولية.", "With a production capacity of over 5,000 metric tons per annum, we ensure that our operations are equipped to meet the growing market demand with consistency and efficiency. Our state-of-the-art manufacturing facilities are designed to deliver high-quality uPVC window systems that comply with international standards.", null, 3, "~/images/home/trophy.png", true, 1, "أكبر منشأة للبثق", "Largest extrusion facility", null },
                    { 2, "مع أكثر من 30 ملف تعريف متميز في مجموعتنا، نحن قادرون على تلبية المتطلبات المتنوعة للسوق وتوفير حلول مصممة خصيصًا للتحديات الفريدة لكل عميل. سواء كان المشروع يتطلب أداءً حديثًا أنيقًا، أو متانة للخدمة الشاقة، أو جماليات، أو عزلًا فائقًا، فإن خط إنتاجنا الشامل يضمن أن هناك دائمًا حلًا مثاليًا.", "With over 30 distinct profiles in our range, we are able to address the diverse requirements of the market and provide solutions tailored to the unique challenges of each client. Whether the project demands sleek modern performance, or heavy-duty durability, aesthetics, superior insulation our comprehensive product line ensures that there is always a perfect fit.", null, 3, "~/images/home/trophy.png", true, 2, "أكبر عدد من الملفات", "Largest number of profiles", null },
                    { 3, "مع أكثر من 50 موردًا منتشرين في جميع أنحاء البلاد، أصبحت EMAPEN الخيار الأول للسوق المحلي بفضل شبكة التوزيع الواسعة. وهذا يضمن الوصول السريع والموثوق إلى منتجاتنا ويعكس أيضًا الثقة والشراكات القوية التي بنيناها داخل الصناعة.", "With over 50 suppliers spread across the country, EMAPEN has become the profile extensive distribution network not only of choice for the local market. This ensures fast and reliable access to our products but also reflects the strong trust and partnerships we have built within the industry.", null, 3, "~/images/home/trophy.png", true, 3, "أكبر شبكة موردين", "Largest network of suppliers", null },
                    { 4, "تمتلك مادة الuPVC خصائص عزل أفضل من الألمونيوم مما يقلل من فقدان الحرارة في فصل الصيف، و بالتالي يقل الإعتماد على أنظمة التدفئة و التبريد الكهربائية وتقل فواتير الكهرباء.", "With a thermal conductivity significantly lower than aluminum, uPVC possesses superior thermal insulation properties. This thermal insulation reduces heat loss during winter and minimizes heat gain during summer, reducing the reliance on artificial heating and cooling systems and making it the material of choice for saving energy and lowering utility bills.", null, 2, "~/images/home/hero2-icon1.png", true, 1, "موفر للطاقة", "Energy efficiency", null },
                    { 5, "نظرا لخصائصه و بنيته الفريدة، تتميز قطاعات الuPVC عزل الصوت العالي. الكثافة العالية لدى القطاعات تقلل من إنتقال الموجات الصوتية. بالإضافة إلى ذلك، يعزز نظام الغرف و الكاوتش من خصائص العزل الصوتي.", "Due to its inherent properties and construction, uPVC frames stand out with unmatched sound insulation features. The relatively high density of uPVC reduces the transmission of sound waves through the material. Moreover, the chambered system and the gasket further enhance the profile's sound insulation properties.", null, 2, "~/images/home/hero2-icon2.png", true, 2, "عزل الصوت", "Sound insulation", null },
                    { 6, "يقلل السطح الناعم للمادة من إلتصاق جزئيات الغبار عليها، بالإضافة إلى وجود آليات إغلاق فعالة مثل كاوتش الTPE المستورد الذي يعزز بشكل كبير مقاومة القطاع للغبار.", "The material's smooth surface finish reduces the adhesion of dust particles on the surface, not to mention effective sealing mechanisms such as rubber gaskets that greatly enhance the frame's dust resistance.", null, 2, "~/images/home/hero2-icon3.png", true, 3, "عزل الأتربة", "Dust proof", null },
                    { 7, "تصنف الuPVC كمادة غير تفاعلية ولا تتعرض للصدأ او التآكل مع مرور الوقت. و هذا يمنح الuPVC متانة فائقة و مقاومة ممتازة للطقس، مما يحافظ على سلامتها الهيكلية و جماله البصري على مر الزمن، و يقضي على الحاجة إلى إعادة طلاء أو التغطية الوقائية.", "Unlike aluminum, uPVC is classified as non-reactive material and does not suffer from rust or corrosion over time. This offers uPVC with superior durability and weather resistance, which maintains its structural integrity and aesthetic appearance over time and eliminates the need for repainting or protective coating.", null, 2, "~/images/home/hero2-icon4.png", true, 4, "المتانة", "Durability", null },
                    { 8, "تجمع نوافذ و أبواب الuPVC بواسطة اللحام، مما يوفر لك عزلا قويا و متانة أعلى مقارنة بالمثبتات الميكانيكية، بالإضافة إلى تقليل الصيانة لأن المسامير قد تتعرض للإرتخاء أو الصدأ.", "uPVC windows and doors are assembled by welding, which provides superior insulation and strength compared to mechanical fasteners, in addition to the reduction of maintenance since bolts or screws may loosen or corrode.", null, 2, "~/images/home/hero2-icon5.png", true, 5, "لحام الوصلات", "Welded joints", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutSection_AboutPageId",
                table: "AboutSection",
                column: "AboutPageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Username",
                table: "AdminUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactPages_CategoryId",
                table: "ContactPages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_PageKey",
                table: "HomePages",
                column: "PageKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomePageSections_HomePageId",
                table: "HomePageSections",
                column: "HomePageId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_Platform",
                table: "SocialMedias",
                column: "Platform",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutSection");

            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "CompanyInfos");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "ContactPages");

            migrationBuilder.DropTable(
                name: "HomePageSections");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "AboutPages");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "HomePages");
        }
    }
}
