using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using UPVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UPVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<HomePageSection> HomePageSections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<ContactPage> ContactPages { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BaseEntity defaults for all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CreatedAt))
                        .HasDefaultValueSql("GETDATE()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.IsActive))
                        .HasDefaultValue(true);

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.IsDeleted))
                        .HasDefaultValue(false);
                }
            }

            // Add unique constraint for Username
            modelBuilder.Entity<AdminUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Add unique constraint for HomePage PageKey
            modelBuilder.Entity<HomePage>()
                .HasIndex(h => h.PageKey)
                .IsUnique();

            // Add unique constraint for SocialMedia Platform
            modelBuilder.Entity<SocialMedia>()
                .HasIndex(s => s.Platform)
                .IsUnique();

            modelBuilder.Entity<AboutPage>()
                .HasMany(p => p.Sections)
                .WithOne(s => s.AboutPage)
                .HasForeignKey(s => s.AboutPageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed default admin user (password: Admin@123)
            // Password hash for "Admin@123" using BCrypt
            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Default password
                    Email = "admin@emapen.com",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed default home pages
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 1,
                    PageKey = "Index",
                    TitleEn = "Our profiles ensure that your windows will endure",
                    TitleAr = "من قطاع متين تأتي نوافذ بعمر طويل",
                    SubtitleEn = "Check our product line",
                    SubtitleAr = "تصفح منتجاتنا",
                    ContentEn = "The leading manufacturer of uPVC window and door systems in Egypt and the African region, demonstrating the reliability of its profiles across a multitude of projects with over a decade of experience in the industry.",
                    ContentAr = "الشركة الرائدة في تصنيع أنظمة نوافذ و أبواب الuPVC في مصر و القارة الأفريقية، والتي أثبتت كفائتها في مشاريع متعددة، مستندة إلى أكثر من عقد من الخبرة في الصناعة.",
                    IsActive = true
                },
                new HomePage
                {
                    Id = 2,
                    PageKey = "Home2",
                    TitleEn = "The window of the future",
                    TitleAr = "نافذة المستقبل",
                    SubtitleEn = "Energy efficiency",
                    SubtitleAr = "موفر للطاقة",
                    ContentEn = "With a thermal conductivity significantly lower than aluminum, uPVC possesses superior <span>thermal insulation</span> properties. This thermal insulation <span>reduces heat loss</span> during winter and minimizes heat gain during summer, reducing the reliance on artificial heating and cooling systems and making it the material of choice for saving energy and <span>lowering utility bills</span>.",
                    ContentAr = "نظرًا لأن التوصيل الحراري لمادة الـ uPVC أقل بكثير من الألومنيوم، فهي تتمتع بخصائص <span>عزل حراري</span> فائقة. يساعد هذا العزل الحراري في تقليل <span>فقدان الحرارة</span> خلال فصل الشتاء، ويحدّ من <span>اكتساب الحرارة</span> في الصيف، مما يقلل الاعتماد على أنظمة التدفئة والتبريد الاصطناعية، ويجعلها المادة المثالية لتوفير الطاقة و <span>خفض فواتير الخدمات</span>.",
                    IsActive = true
                },

                
                new HomePage
                {
                    Id = 3,
                    PageKey = "Home3",
                    TitleEn = "Largest in Egypt",
                    TitleAr = "الأكبر في مصر",
                    SubtitleEn = "( Relative to the Egyptian market )",
                    SubtitleAr = "( بالنسبة للسوق المصري )",
                    ContentEn = "",
                    ContentAr = "",
                    IsActive = true
                },
                new HomePage
                {
                    Id = 4,
                    PageKey = "Home4",
                    TitleEn = "Projects completed",
                    TitleAr = "المشاريع المكتملة",
                    SubtitleEn = "Renowned for their exceptional performance, EMAPEN’s profiles have garnered the trust of numerous clients, firmly establishing our reputation as a preferred choice for window and door applications in a diverse set of construction projects.",
                    SubtitleAr = "تُعرف قطاعات إيمابن بأدائها الاستثنائي، حيث نالت ثقة العديد من العملاء، مما رسّخ سمعتها كأحد أفضل القطاعات المتخصصة في تطبيقات النوافذ والأبواب لمختلف مشاريع البناء في مصر وخارجها. وتتميّز منتجات إيمابن بجودتها العالية وثبات أدائها في البيئات المتنوعة، ما جعلها الخيار المفضل لدى كبرى شركات التطوير العقاري والمقاولين.",
                    ContentEn = "EMAPEN is also known for its high production capacity, consistently meeting the high demands of many construction projects.",
                    ContentAr = "كما تُعرف إيمابن بقدرتها الإنتاجية العالية والتزامها الدائم بتلبية المتطلبات الكبيرة للمشروعات الضخمة، مع الحرص على الالتزام بالمعايير الدولية في كل مرحلة من مراحل التصنيع والتنفيذ.",
                    ContentOtherEn = "The following is some of the large-scale projects that have specified our profiles.",
                    ContentOtherAr = "تحتوي القائمة التالية على مجموعة من أبرز المشاريع الكبرى التي تم تزويدها بقطاعات إيمابن، والتي تعكس الثقة المتبادلة بين الشركة وشركائها في قطاع البناء.",
                    IsActive = true
                }
            );

            // Seed HomePageSection data for Home3
            modelBuilder.Entity<HomePageSection>().HasData(
                new HomePageSection
                {
                    Id = 1,
                    HomePageId = 3, // Home3
                    TitleEn = "Largest extrusion facility",
                    TitleAr = "أكبر منشأة للبثق",
                    ContentEn = "With a production capacity of over 5,000 metric tons per annum, we ensure that our operations are equipped to meet the growing market demand with consistency and efficiency. Our state-of-the-art manufacturing facilities are designed to deliver high-quality uPVC window systems that comply with international standards.",
                    ContentAr = "بطاقة إنتاجية تزيد عن 5000 طن متري سنويًا، نضمن أن عملياتنا مجهزة لتلبية الطلب المتزايد في السوق بثبات وكفاءة. تم تصميم منشآتنا الصناعية الحديثة لتقديم أنظمة نوافذ uPVC عالية الجودة تتوافق مع المعايير الدولية.",
                    ImagePath = "~/images/home/trophy.png",
                    Order = 1,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 2,
                    HomePageId = 3, // Home3
                    TitleEn = "Largest number of profiles",
                    TitleAr = "أكبر عدد من الملفات",
                    ContentEn = "With over 30 distinct profiles in our range, we are able to address the diverse requirements of the market and provide solutions tailored to the unique challenges of each client. Whether the project demands sleek modern performance, or heavy-duty durability, aesthetics, superior insulation our comprehensive product line ensures that there is always a perfect fit.",
                    ContentAr = "مع أكثر من 30 ملف تعريف متميز في مجموعتنا، نحن قادرون على تلبية المتطلبات المتنوعة للسوق وتوفير حلول مصممة خصيصًا للتحديات الفريدة لكل عميل. سواء كان المشروع يتطلب أداءً حديثًا أنيقًا، أو متانة للخدمة الشاقة، أو جماليات، أو عزلًا فائقًا، فإن خط إنتاجنا الشامل يضمن أن هناك دائمًا حلًا مثاليًا.",
                    ImagePath = "~/images/home/trophy.png",
                    Order = 2,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 3,
                    HomePageId = 3, // Home3
                    TitleEn = "Largest network of suppliers",
                    TitleAr = "أكبر شبكة موردين",
                    ContentEn = "With over 50 suppliers spread across the country, EMAPEN has become the profile extensive distribution network not only of choice for the local market. This ensures fast and reliable access to our products but also reflects the strong trust and partnerships we have built within the industry.",
                    ContentAr = "مع أكثر من 50 موردًا منتشرين في جميع أنحاء البلاد، أصبحت EMAPEN الخيار الأول للسوق المحلي بفضل شبكة التوزيع الواسعة. وهذا يضمن الوصول السريع والموثوق إلى منتجاتنا ويعكس أيضًا الثقة والشراكات القوية التي بنيناها داخل الصناعة.",
                    ImagePath = "~/images/home/trophy.png",
                    Order = 3,
                    IsActive = true
                }
            );

            var aboutPageId = 1;

            modelBuilder.Entity<AboutPage>().HasData(new AboutPage
            {
                Id= aboutPageId,
                PageKey = "About",
                TitleEn = "Since its establishment in 2015, EMAPEN has consistently demonstrated the reliability of its profiles across a multitude of projects, thereby solidifying its standing as one of the leading manufacturers of UPVC in Egypt.",
                TitleAr = "منذ تأسيسها عام 2015، أثبتت شركة إيمابن بشكلٍ متواصل موثوقية قطاعاتها في مئات المشاريع المتنوعة، مما رسّخ مكانتها كواحدة من أبرز الشركات الرائدة في تصنيع أنظمة الـuPVC في مصر. وقد جاءت هذه الريادة نتيجة التزام الشركة الدائم بالجودة، والابتكار في التصميم، والاستمرارية في تحسين الأداء لتلبية احتياجات السوق المتغيرة ومواكبة أحدث التطورات في مجال صناعة النوافذ والأبواب.",
                ContentEn = "EMAPEN boasts the largest network of distributors in the country, proving itself as the profile of choice for many construction projects. Additionally, the company features Egypt's largest extrusion facility, equipped with seven production lines and a capacity of 500 metric tons per month.",
                ContentAr = "تفتخر إيمابن بامتلاكها أكبر شبكة موزعين على مستوى الجمهورية، مما جعلها الخيار الأول والمفضل في العديد من مشاريع البناء الكبرى. ويأتي هذا الانتشار الواسع كدليل على الثقة التي اكتسبتها الشركة بين شركائها في السوق المحلي، بفضل التزامها بالمواعيد وجودة منتجاتها العالية. كما تضم الشركة أكبر منشأة بثق في مصر، مزوّدة بسبعة خطوط إنتاج تعمل بطاقة تصل إلى 500 طن متري شهريًا، مما يمكّنها من تلبية الطلبات المتزايدة بكفاءة واستقرار في الإنتاج.",
                SubtitleEn = "EMAPEN's profiles conform to the ISO 9001:2015 standard, ensuring consistent manufacturing processes and product performance.",
                SubtitleAr = "تُصنّع قطاعات إيمابن وفقًا لمواصفات ISO 9001:2015 العالمية، مما يضمن التزام الشركة بأعلى معايير الجودة في جميع مراحل التصنيع والإدارة. ويُترجم هذا الالتزام إلى عمليات إنتاج دقيقة ومنتجات ذات أداء ثابت وموثوق، تعكس رؤية إيمابن في توفير حلول متكاملة تجمع بين المتانة، والكفاءة، والجمال في آنٍ واحد.",
                IsActive = true
            });

            modelBuilder.Entity<AboutSection>().HasData(
                new AboutSection
                {
                    Id=1,
                    AboutPageId = aboutPageId,
                    SectionType = AboutSectionType.Mission,
                    Order = 1,
                    TitleEn = "Our mission",
                    TitleAr = "مهمتنا",
                    ContentEn = "is to provide uPVC profiles of high quality and durability that satisfy international standards and meets customer demands through advanced machinery and necessary equipment managed by qualified professionals.",
                    ContentAr = "هي تقديم قطاعات uPVC عالية الجودة والمتانة، مطابقة للمعايير الدولية، وتلبي احتياجات العملاء في مختلف الأسواق. نحرص على تحقيق ذلك من خلال استخدام أحدث خطوط الإنتاج والمعدات المتطورة، بإشراف فريق من المهندسين والفنيين ذوي الكفاءة العالية، لضمان تحقيق أعلى مستويات الدقة والأداء في كل منتج يحمل اسم إيمابن."
                },
                new AboutSection
                {
                    Id = 2,
                    AboutPageId = aboutPageId,
                    SectionType = AboutSectionType.Vision,
                    Order = 2,
                    TitleEn = "Our vision",
                    TitleAr = "رؤيتنا",
                    ContentEn = "is to become the most reliable and trusted supplier of uPVC profiles in both domestic and export markets.",
                    ContentAr = "أن نكون المورد الأكثر موثوقية واعتمادًا لقطاعات uPVC في الأسواق المحلية وأسواق التصدير، من خلال ترسيخ اسم إيمابن كرمز للجودة والالتزام والابتكار في هذه الصناعة. نسعى إلى توسيع نطاق أعمالنا إقليميًا ودوليًا، وتعزيز مكانتنا كشريك موثوق يوفّر حلول نوافذ وأبواب متكاملة تلبي أعلى المعايير الفنية والجمالية، بما يضمن رضا العملاء واستدامة الثقة في منتجاتنا."
                }
            );



            // Seed default company info
            modelBuilder.Entity<CompanyInfo>().HasData(
                new CompanyInfo
                {
                    Id = 1,
                    NameEn = "EMAPEN",
                    NameAr = "إيمابن",
                    Phone = "15726",
                    Mobile = "+201069946220",
                    Email = "info@emapen.net",
                    AddressEn = "6th of October City Industrial Zone 1, Land no.238 - Giza, Egypt",
                    AddressAr = "مدينة 6 أكتوبر، المنطقة الصناعية 1، قطعة رقم 238 - الجيزة، مصر",
                    DescriptionEn = "Leading provider of uPVC windows and doors solutions",
                    DescriptionAr = "المزود الرائد لحلول النوافذ والأبواب uPVC",
                    SloganEn = "EMAPEN uPVC systems for doors and windows",
                    SloganAr = "أنظمة EMAPEN لـ uPVC للأبواب والنوافذ",
                    CopyrightTextEn = "EMAPEN all rights reserved",
                    CopyrightTextAr = "إيمابن جميع الحقوق محفوظة",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed default social media
            modelBuilder.Entity<SocialMedia>().HasData(
                new SocialMedia
                {
                    Id = 1,
                    Platform = "Facebook",
                    Url = "https://www.facebook.com/share/1Bhcz5o4dt/",
                    IconClass = "bi bi-facebook",
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 2,
                    Platform = "Twitter",
                    Url = "https://twitter.com/emapen",
                    IconClass = "bi bi-twitter",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 3,
                    Platform = "Instagram",
                    Url = "https://www.instagram.com/emapen.upvc.egypt?igsh=eG9uMGR5bmttaXZv",
                    IconClass = "bi bi-instagram",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 4,
                    Platform = "LinkedIn",
                    Url = "https://www.linkedin.com/company/emapen-for-upvc-profile/",
                    IconClass = "bi bi-linkedin",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 5,
                    Platform = "WhatsApp",
                    Url = "https://wa.me/201069946220",
                    IconClass = "bi bi-whatsapp",
                    DisplayOrder = 5,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed categories for contact form
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    NameEn = "Window manufacturer (Egypt)",
                    NameAr = "مصنع نوافذ",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    NameEn = "Supplier",
                    NameAr = "موزع",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    NameEn = "Architect",
                    NameAr = "مهندس",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 4,
                    NameEn = "Home owner",
                    NameAr = "صاحب منزل",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 5,
                    NameEn = "other",
                    NameAr = "آخر",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed contact page content
            modelBuilder.Entity<ContactPage>().HasData(
                new ContactPage
                {
                    Id = 1,
                    TitleEn = "Contact Us",
                    TitleAr = "اتصل بنا",
                    SubtitleEn = "Whether you have any questions regarding our products or you're considering making an order,",
                    SubtitleAr = "سواء كانت لديك أي استفسارات حول منتجاتنا أو كنت تفكر في تقديم طلب،",
                    SubtitleHighlightEn = "feel free to contact us.",
                    SubtitleHighlightAr = "لا تتردد في الاتصال بنا.",
                    ContentEn = "Window manufacturer, Supplier, Architect, Home owner, no matter what your category is,",
                    ContentAr = "مصنع نوافذ، موزع، مهندس معماري، صاحب منزل، مهما كانت فئتك،",
                    ContentHighlightEn = "we would be glad to support you.",
                    ContentHighlightAr = "يسعدنا دعمك.",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
