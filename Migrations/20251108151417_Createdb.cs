using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPVC.Migrations
{
    /// <inheritdoc />
    public partial class Createdb : Migration
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
                name: "Category",
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
                    table.PrimaryKey("PK_Category", x => x.Id);
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
                    Platform = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubtitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_ContactPages_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AboutPages",
                columns: new[] { "Id", "ContentAr", "ContentEn", "DeletedAt", "ImagePath", "IsActive", "PageKey", "SubtitleAr", "SubtitleEn", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[] { 1, "تفتخر إيمابن بامتلاكها أكبر شبكة موزعين على مستوى الجمهورية، مما جعلها الخيار الأول والمفضل في العديد من مشاريع البناء الكبرى. ويأتي هذا الانتشار الواسع كدليل على الثقة التي اكتسبتها الشركة بين شركائها في السوق المحلي، بفضل التزامها بالمواعيد وجودة منتجاتها العالية. كما تضم الشركة أكبر منشأة بثق في مصر، مزوّدة بسبعة خطوط إنتاج تعمل بطاقة تصل إلى 500 طن متري شهريًا، مما يمكّنها من تلبية الطلبات المتزايدة بكفاءة واستقرار في الإنتاج.", "EMAPEN boasts the largest network of distributors in the country, proving itself as the profile of choice for many construction projects. Additionally, the company features Egypt's largest extrusion facility, equipped with seven production lines and a capacity of 500 metric tons per month.", null, null, true, "About", "تُصنّع قطاعات إيمابن وفقًا لمواصفات ISO 9001:2015 العالمية، مما يضمن التزام الشركة بأعلى معايير الجودة في جميع مراحل التصنيع والإدارة. ويُترجم هذا الالتزام إلى عمليات إنتاج دقيقة ومنتجات ذات أداء ثابت وموثوق، تعكس رؤية إيمابن في توفير حلول متكاملة تجمع بين المتانة، والكفاءة، والجمال في آنٍ واحد.", "EMAPEN's profiles conform to the ISO 9001:2015 standard, ensuring consistent manufacturing processes and product performance.", "منذ تأسيسها عام 2015، أثبتت شركة إيمابن بشكلٍ متواصل موثوقية قطاعاتها في مئات المشاريع المتنوعة، مما رسّخ مكانتها كواحدة من أبرز الشركات الرائدة في تصنيع أنظمة الـuPVC في مصر. وقد جاءت هذه الريادة نتيجة التزام الشركة الدائم بالجودة، والابتكار في التصميم، والاستمرارية في تحسين الأداء لتلبية احتياجات السوق المتغيرة ومواكبة أحدث التطورات في مجال صناعة النوافذ والأبواب.", "Since its establishment in 2015, EMAPEN has consistently demonstrated the reliability of its profiles across a multitude of projects, thereby solidifying its standing as one of the leading manufacturers of UPVC in Egypt.", null });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "IsActive", "LastLoginAt", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@emapen.com", true, null, "$2a$11$290Zr1xiNqxawJEvLTSK3.IccBNXFsfc8zHgu4r6JcnA3igt0Cjxu", null, "admin" });

            migrationBuilder.InsertData(
                table: "CompanyInfos",
                columns: new[] { "Id", "AddressAr", "AddressEn", "CreatedAt", "DeletedAt", "DescriptionAr", "DescriptionEn", "Email", "FaviconPath", "IsActive", "LogoPath", "Mobile", "NameAr", "NameEn", "Phone", "UpdatedAt", "WorkingHoursJson" },
                values: new object[] { 1, "مدينة 6 أكتوبر، المنطقة الصناعية 1، قطعة رقم 238 - الجيزة، مصر", "6th of October City Industrial Zone 1, Land no.238 - Giza, Egypt", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "المزود الرائد لحلول النوافذ والأبواب uPVC", "Leading provider of uPVC windows and doors solutions", "info@emapen.net", null, true, null, "+201069946220", "إيمابن", "EMAPEN", "15726", null, null });

            migrationBuilder.InsertData(
                table: "HomePages",
                columns: new[] { "Id", "ContentAr", "ContentEn", "ContentOtherAr", "ContentOtherEn", "DeletedAt", "ImagePath", "IsActive", "MetaDataJson", "PageKey", "SecondaryImagePath", "SubtitleAr", "SubtitleEn", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "الشركة الرائدة في تصنيع أنظمة نوافذ و أبواب الuPVC في مصر و القارة الأفريقية، والتي أثبتت كفائتها في مشاريع متعددة، مستندة إلى أكثر من عقد من الخبرة في الصناعة.", "The leading manufacturer of uPVC window and door systems in Egypt and the African region, demonstrating the reliability of its profiles across a multitude of projects with over a decade of experience in the industry.", "", "", null, null, true, null, "Index", null, "تصفح منتجاتنا", "Check our product line", "من قطاع متين تأتي نوافذ بعمر طويل", "Our profiles ensure that your windows will endure", null },
                    { 2, "نظرًا لأن التوصيل الحراري لمادة الـ uPVC أقل بكثير من الألومنيوم، فهي تتمتع بخصائص <span>عزل حراري</span> فائقة. يساعد هذا العزل الحراري في تقليل <span>فقدان الحرارة</span> خلال فصل الشتاء، ويحدّ من <span>اكتساب الحرارة</span> في الصيف، مما يقلل الاعتماد على أنظمة التدفئة والتبريد الاصطناعية، ويجعلها المادة المثالية لتوفير الطاقة و <span>خفض فواتير الخدمات</span>.", "With a thermal conductivity significantly lower than aluminum, uPVC possesses superior <span>thermal insulation</span> properties. This thermal insulation <span>reduces heat loss</span> during winter and minimizes heat gain during summer, reducing the reliance on artificial heating and cooling systems and making it the material of choice for saving energy and <span>lowering utility bills</span>.", "", "", null, null, true, null, "Home2", null, "موفر للطاقة", "Energy efficiency", "نافذة المستقبل", "The window of the future", null },
                    { 3, "أكبر منشأة بثق، أكبر عدد من الملفات الشخصية، أكبر شبكة من الموردين.", "Largest extrusion facility, largest number of profiles, largest network of suppliers.", "", "", null, null, true, null, "Home3", null, "الرائد في السوق", "Market Leader", "الأكبر في مصر", "Largest in Egypt", null },
                    { 4, "كما تُعرف إيمابن بقدرتها الإنتاجية العالية والتزامها الدائم بتلبية المتطلبات الكبيرة للمشروعات الضخمة، مع الحرص على الالتزام بالمعايير الدولية في كل مرحلة من مراحل التصنيع والتنفيذ.", "EMAPEN is also known for its high production capacity, consistently meeting the high demands of many construction projects.", "تحتوي القائمة التالية على مجموعة من أبرز المشاريع الكبرى التي تم تزويدها بقطاعات إيمابن، والتي تعكس الثقة المتبادلة بين الشركة وشركائها في قطاع البناء.", "The following is some of the large-scale projects that have specified our profiles.", null, null, true, null, "Home4", null, "تُعرف قطاعات إيمابن بأدائها الاستثنائي، حيث نالت ثقة العديد من العملاء، مما رسّخ سمعتها كأحد أفضل القطاعات المتخصصة في تطبيقات النوافذ والأبواب لمختلف مشاريع البناء في مصر وخارجها. وتتميّز منتجات إيمابن بجودتها العالية وثبات أدائها في البيئات المتنوعة، ما جعلها الخيار المفضل لدى كبرى شركات التطوير العقاري والمقاولين.", "Renowned for their exceptional performance, EMAPEN’s profiles have garnered the trust of numerous clients, firmly establishing our reputation as a preferred choice for window and door applications in a diverse set of construction projects.", "المشاريع المكتملة", "Projects completed", null }
                });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DisplayOrder", "IconClass", "IsActive", "Platform", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, "bi bi-facebook", true, "Facebook", null, "https://www.facebook.com/share/1Bhcz5o4dt/" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, "bi bi-twitter", true, "Twitter", null, "https://twitter.com/emapen" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, "bi bi-instagram", true, "Instagram", null, "https://www.instagram.com/emapen.upvc.egypt?igsh=eG9uMGR5bmttaXZv" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, "bi bi-linkedin", true, "LinkedIn", null, "https://www.linkedin.com/company/emapen-for-upvc-profile/" },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, "bi bi-whatsapp", true, "WhatsApp", null, "https://wa.me/201069946220" }
                });

            migrationBuilder.InsertData(
                table: "AboutSection",
                columns: new[] { "Id", "AboutPageId", "ContentAr", "ContentEn", "DeletedAt", "IconPath", "Order", "SectionType", "TitleAr", "TitleEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "هي تقديم قطاعات uPVC عالية الجودة والمتانة، مطابقة للمعايير الدولية، وتلبي احتياجات العملاء في مختلف الأسواق. نحرص على تحقيق ذلك من خلال استخدام أحدث خطوط الإنتاج والمعدات المتطورة، بإشراف فريق من المهندسين والفنيين ذوي الكفاءة العالية، لضمان تحقيق أعلى مستويات الدقة والأداء في كل منتج يحمل اسم إيمابن.", "is to provide uPVC profiles of high quality and durability that satisfy international standards and meets customer demands through advanced machinery and necessary equipment managed by qualified professionals.", null, null, 1, 1, "مهمتنا", "Our mission", null },
                    { 2, 1, "أن نكون المورد الأكثر موثوقية واعتمادًا لقطاعات uPVC في الأسواق المحلية وأسواق التصدير، من خلال ترسيخ اسم إيمابن كرمز للجودة والالتزام والابتكار في هذه الصناعة. نسعى إلى توسيع نطاق أعمالنا إقليميًا ودوليًا، وتعزيز مكانتنا كشريك موثوق يوفّر حلول نوافذ وأبواب متكاملة تلبي أعلى المعايير الفنية والجمالية، بما يضمن رضا العملاء واستدامة الثقة في منتجاتنا.", "is to become the most reliable and trusted supplier of uPVC profiles in both domestic and export markets.", null, null, 2, 2, "رؤيتنا", "Our vision", null }
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
                name: "ContactPages");

            migrationBuilder.DropTable(
                name: "HomePages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "AboutPages");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
