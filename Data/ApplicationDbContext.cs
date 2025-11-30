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
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<ProductCertificate> ProductCertificates { get; set; }
        public DbSet<DesignOption> DesignOptions { get; set; }
        public DbSet<ProductDesignOption> ProductDesignOptions { get; set; }
        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<ContactPage> ContactPages { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AboutSection> AboutSections { get; set; }

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

            // Configure Product -> ProductDetails relationship (one-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductDetails)
                .WithOne(pd => pd.Product)
                .HasForeignKey<ProductDetails>(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Many-to-Many relationship: ProductDetails <-> Specification through ProductSpecification
            modelBuilder.Entity<ProductSpecification>()
                .HasKey(ps => ps.Id);

            modelBuilder.Entity<ProductSpecification>()
                .HasOne(ps => ps.ProductDetails)
                .WithMany(pd => pd.Specifications)
                .HasForeignKey(ps => ps.ProductDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSpecification>()
                .HasOne(ps => ps.Specification)
                .WithMany(s => s.ProductSpecifications)
                .HasForeignKey(ps => ps.SpecificationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Many-to-Many relationship: ProductDetails <-> Color through ProductColor
            modelBuilder.Entity<ProductColor>()
                .HasKey(pc => pc.Id);

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.ProductDetails)
                .WithMany(pd => pd.Colors)
                .HasForeignKey(pc => pc.ProductDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ProductCertificate relationships
            modelBuilder.Entity<ProductCertificate>()
                .HasKey(pc => pc.Id);

            modelBuilder.Entity<ProductCertificate>()
                .HasOne(pc => pc.ProductDetails)
                .WithMany(pd => pd.Certificates)
                .HasForeignKey(pc => pc.ProductDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCertificate>()
                .HasOne(pc => pc.Certificate)
                .WithMany(c => c.ProductCertificates)
                .HasForeignKey(pc => pc.CertificateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ProductDesignOption relationships
            modelBuilder.Entity<ProductDesignOption>()
                .HasKey(pdo => pdo.Id);

            modelBuilder.Entity<ProductDesignOption>()
                .HasOne(pdo => pdo.ProductDetails)
                .WithMany(pd => pd.DesignOptions)
                .HasForeignKey(pdo => pdo.ProductDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductDesignOption>()
                .HasOne(pdo => pdo.DesignOption)
                .WithMany(d => d.ProductDesignOptions)
                .HasForeignKey(pdo => pdo.DesignOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ProductDetails -> ProductDesignOption relationship (one-to-many)
            modelBuilder.Entity<ProductDetails>()
                .HasMany(pd => pd.DesignOptions)
                .WithOne(pdo => pdo.ProductDetails)
                .HasForeignKey(pdo => pdo.ProductDetailsId)
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

            // Seed HomePageSection data for Home2 (Features slider)
            modelBuilder.Entity<HomePageSection>().HasData(
                new HomePageSection
                {
                    Id = 4,
                    HomePageId = 2, // Home2
                    TitleEn = "Energy efficiency",
                    TitleAr = "موفر للطاقة",
                    ContentEn = "With a thermal conductivity significantly lower than aluminum, uPVC possesses superior thermal insulation properties. This thermal insulation reduces heat loss during winter and minimizes heat gain during summer, reducing the reliance on artificial heating and cooling systems and making it the material of choice for saving energy and lowering utility bills.",
                    ContentAr = "تمتلك مادة الuPVC خصائص عزل أفضل من الألمونيوم مما يقلل من فقدان الحرارة في فصل الصيف، و بالتالي يقل الإعتماد على أنظمة التدفئة و التبريد الكهربائية وتقل فواتير الكهرباء.",
                    ImagePath = "/images/home2/adv1.png",
                    Order = 1,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 5,
                    HomePageId = 2, // Home2
                    TitleEn = "Sound insulation",
                    TitleAr = "عزل الصوت",
                    ContentEn = "Due to its inherent properties and construction, uPVC frames stand out with unmatched sound insulation features. The relatively high density of uPVC reduces the transmission of sound waves through the material. Moreover, the chambered system and the gasket further enhance the profile's sound insulation properties.",
                    ContentAr = "نظرا لخصائصه و بنيته الفريدة، تتميز قطاعات الuPVC عزل الصوت العالي. الكثافة العالية لدى القطاعات تقلل من إنتقال الموجات الصوتية. بالإضافة إلى ذلك، يعزز نظام الغرف و الكاوتش من خصائص العزل الصوتي.",
                    ImagePath = "/images/home2/adv2.png",
                    Order = 2,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 6,
                    HomePageId = 2, // Home2
                    TitleEn = "Dust proof",
                    TitleAr = "عزل الأتربة",
                    ContentEn = "The material's smooth surface finish reduces the adhesion of dust particles on the surface, not to mention effective sealing mechanisms such as rubber gaskets that greatly enhance the frame's dust resistance.",
                    ContentAr = "يقلل السطح الناعم للمادة من إلتصاق جزئيات الغبار عليها، بالإضافة إلى وجود آليات إغلاق فعالة مثل كاوتش الTPE المستورد الذي يعزز بشكل كبير مقاومة القطاع للغبار.",
                    ImagePath = "/images/home2/adv3.png",
                    Order = 3,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 7,
                    HomePageId = 2, // Home2
                    TitleEn = "Durability",
                    TitleAr = "المتانة",
                    ContentEn = "Unlike aluminum, uPVC is classified as non-reactive material and does not suffer from rust or corrosion over time. This offers uPVC with superior durability and weather resistance, which maintains its structural integrity and aesthetic appearance over time and eliminates the need for repainting or protective coating.",
                    ContentAr = "تصنف الuPVC كمادة غير تفاعلية ولا تتعرض للصدأ او التآكل مع مرور الوقت. و هذا يمنح الuPVC متانة فائقة و مقاومة ممتازة للطقس، مما يحافظ على سلامتها الهيكلية و جماله البصري على مر الزمن، و يقضي على الحاجة إلى إعادة طلاء أو التغطية الوقائية.",
                    ImagePath = "/images/home2/adv4.png",
                    Order = 4,
                    IsActive = true
                },
                new HomePageSection
                {
                    Id = 8,
                    HomePageId = 2, // Home2
                    TitleEn = "Welded joints",
                    TitleAr = "لحام الوصلات",
                    ContentEn = "uPVC windows and doors are assembled by welding, which provides superior insulation and strength compared to mechanical fasteners, in addition to the reduction of maintenance since bolts or screws may loosen or corrode.",
                    ContentAr = "تجمع نوافذ و أبواب الuPVC بواسطة اللحام، مما يوفر لك عزلا قويا و متانة أعلى مقارنة بالمثبتات الميكانيكية، بالإضافة إلى تقليل الصيانة لأن المسامير قد تتعرض للإرتخاء أو الصدأ.",
                    ImagePath = "/images/home2/adv5.png",
                    Order = 5,
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
                    Platform = SocialMediaPlatform.Facebook,
                    Url = "https://www.facebook.com/share/1Bhcz5o4dt/",
                    IconClass = "bi bi-facebook",
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 2,
                    Platform = SocialMediaPlatform.Twitter,
                    Url = "https://twitter.com/emapen",
                    IconClass = "bi bi-twitter",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 3,
                    Platform = SocialMediaPlatform.Instagram,
                    Url = "https://www.instagram.com/emapen.upvc.egypt?igsh=eG9uMGR5bmttaXZv",
                    IconClass = "bi bi-instagram",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 4,
                    Platform = SocialMediaPlatform.LinkedIn,
                    Url = "https://www.linkedin.com/company/emapen-for-upvc-profile/",
                    IconClass = "bi bi-linkedin",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 5,
                    Platform = SocialMediaPlatform.WhatsApp,
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

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    NameEn = "Ema-42s",
                    NameAr = "EMA-42S",
                    SubtitleEn = "ECONOMIC SLIDING WINDOW SYSTEM",
                    SubtitleAr = "نظام الجرار الإقتصادي",
                    DescriptionEn = "The economic alternative to EMA60S, dedicated for smaller sliding windows and smaller budgets while still maintaining the exceptional quality you need for your windows.",
                    DescriptionAr = "البديل الإقتصادي لنظام EMA-60S مخصص للنوافذ الأصغر السعر والأوفر، مع الحفاظ على العزل و المتانة التي تحتاجها لنوافذك.",
                    ImagePath = "/images/product/EMA-42S.png",
                    BrochurePath = "/files/42s-brochure.pdf",
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 2,
                    NameEn = "EMA-60",
                    NameAr = "EMA-60",
                    SubtitleEn = "THE DEFINITIVE CASEMENT WINDOW\nAND DOOR SYSTEM",
                    SubtitleAr = "نظام المفصلي",
                    DescriptionEn = "Your profile of choice for ensuring maximum ventilation through windows. Moreover, the casement's tight seal provides it with a superior sound insulation compared to its counterparts.",
                    DescriptionAr = "الإختيار الأمثل لضمان أقصى تهوئة من خلال النوافذ. بالإضافة الى ذلك فإن الإغلاق المحكم للقطاع يوفر عزلا صوتيا أعلى من الأنظمة الاخرى.",
                    ImagePath = "/images/product/EMA-60.png",
                    BrochurePath = "/files/60-brochure.pdf",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 3,
                    NameEn = "EMA-60S",
                    NameAr = "EMA-60S",
                    SubtitleEn = "STANDARD SLIDING WINDOW SYSTEM",
                    SubtitleAr = "نظام الجرار",
                    DescriptionEn = "The default option for your sliding windows, ideal when opting for wide unobstructed views. Moreover, the horizontal movement of the sliding systems makes them more suitable for limited interior spaces.",
                    DescriptionAr = "الإختيار النموذجي لنوافذ الجرار الخاصة بك، ملائم للفتحات الكبيرة التي تتيح لك الإستمتاع بالمنظر الخارجي. علاوة على ذلك، فإن الحركة الأفقية لأنظمة الجرار تجعلها أكثر ملائمة للمساحات الداخلية المحدودة",
                    ImagePath = "/images/product/EMA-60s.png",
                    BrochurePath = "/files/60s-brochure.pdf",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 4,
                    NameEn = "EMA-STYLE",
                    NameAr = "EMA-STYLE",
                    SubtitleEn = "PREMIUM SLIDING WINDOW AND\nDOOR SYSTEM",
                    SubtitleAr = "نظام الجرار المميز",
                    DescriptionEn = "EMAPEN's new and refined sliding system, introducing a sliding door sash of exceptional quality and a 60mm built-in bar. The system also boasts the highest profile thickness in the suite, giving you the highest durability and insulation.",
                    DescriptionAr = "نظام الجرار الجديد و المتطور من EMAPEN, الذي يقدم ضلفة لباب جرار بجودة استثنائية وحلق ببار ٦ سم. يتميز النظام أيضا بأعلى سمك خارجي في مجموعتنا، مما يوفر أقصى درجات المتانة و العزل.",
                    ImagePath = "/images/product/EMA-STYLE.png",
                    BrochurePath = "/files/Style-brochure.pdf",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Product Details
            modelBuilder.Entity<ProductDetails>().HasData(
                new ProductDetails
                {
                    Id = 1,
                    ProductId = 1, // EMA-42S
                    DetailHeroImagePath = "/images/product/Ema-42s-hero.png",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ProductDetails
                {
                    Id = 2,
                    ProductId = 2, // EMA-60
                    DetailHeroImagePath = "/images/product/EMA-60-hero.png",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ProductDetails
                {
                    Id = 3,
                    ProductId = 3, // EMA-60S
                    DetailHeroImagePath = "/images/product/EMA-60S-hero.png",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ProductDetails
                {
                    Id = 4,
                    ProductId = 4, // EMA-STYLE
                    DetailHeroImagePath = "/images/product/EMA-STYLE-hero.png",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Shared Specifications (unique specifications used across products)
            modelBuilder.Entity<Specification>().HasData(
                new Specification { Id = 1, NameEn = "42mm sash profile width", NameAr = "عرض ضلفة 42 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 2, NameEn = "47mm sash profile width", NameAr = "عرض ضلفة 47 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 3, NameEn = "60mm sash profile width", NameAr = "عرض ضلفة 60 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 4, NameEn = "60mm frame profile width", NameAr = "عرض إطار 60 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 5, NameEn = "90mm frame profile width", NameAr = "عرض إطار 90 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 6, NameEn = "110mm frame profile width", NameAr = "عرض إطار 110 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 7, NameEn = "126mm frame profile width", NameAr = "عرض إطار 126 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 8, NameEn = "2.3mm profile thickness", NameAr = "سُمك القطاع 2.3 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 9, NameEn = "2.5mm profile thickness", NameAr = "سُمك القطاع 2.5 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 10, NameEn = "2.6mm profile thickness", NameAr = "سُمك القطاع 2.6 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 11, NameEn = "4-6mm single glazing option", NameAr = "خيار زجاج أحادي 4-6 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 12, NameEn = "24mm double glazing option", NameAr = "خيار زجاج مزدوج 24 مم", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 13, NameEn = "Uf value of 1.1 W/m²K", NameAr = "قيمة Uf 1.1 واط/م²ك", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 14, NameEn = "2 chambered system enhancing profile durability and insulation.", NameAr = "نظام غرفتين يعزز متانة القطاع والعزل.", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 15, NameEn = "3 chambered system enhancing profile durability and insulation.", NameAr = "نظام 3 غرف يعزز متانة القطاع والعزل.", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 16, NameEn = "4 chambered system enhancing profile durability and insulation.", NameAr = "نظام 4 غرف يعزز متانة القطاع والعزل.", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 17, NameEn = "One type of steel reinforcement and gasket reducing storage costs.", NameAr = "نوع واحد من التسليح الفولاذي والجوان مما يقلل تكاليف التخزين.", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Specification { Id = 18, NameEn = "TPE gasket available in black, white and gray.", NameAr = "جوان TPE متوفر بالألوان الأسود والأبيض والرمادي.", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Product-Specification relationships (Many-to-Many junction table)
            modelBuilder.Entity<ProductSpecification>().HasData(
                // EMA-42S Specifications (ProductDetailsId=1)
                new ProductSpecification { Id = 1, ProductDetailsId = 1, SpecificationId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 42mm sash
                new ProductSpecification { Id = 2, ProductDetailsId = 1, SpecificationId = 5, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 90mm frame
                new ProductSpecification { Id = 3, ProductDetailsId = 1, SpecificationId = 8, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 2.3mm thickness
                new ProductSpecification { Id = 4, ProductDetailsId = 1, SpecificationId = 11, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Single glazing
                new ProductSpecification { Id = 5, ProductDetailsId = 1, SpecificationId = 13, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Uf value
                new ProductSpecification { Id = 6, ProductDetailsId = 1, SpecificationId = 14, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 2 chambered
                new ProductSpecification { Id = 7, ProductDetailsId = 1, SpecificationId = 17, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Steel reinforcement
                new ProductSpecification { Id = 8, ProductDetailsId = 1, SpecificationId = 18, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // TPE gasket
                
                // EMA-60 Specifications (ProductDetailsId=2)
                new ProductSpecification { Id = 9, ProductDetailsId = 2, SpecificationId = 3, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 60mm sash
                new ProductSpecification { Id = 10, ProductDetailsId = 2, SpecificationId = 4, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 60mm frame
                new ProductSpecification { Id = 11, ProductDetailsId = 2, SpecificationId = 9, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 2.5mm thickness
                new ProductSpecification { Id = 12, ProductDetailsId = 2, SpecificationId = 11, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Single glazing
                new ProductSpecification { Id = 13, ProductDetailsId = 2, SpecificationId = 12, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Double glazing
                new ProductSpecification { Id = 14, ProductDetailsId = 2, SpecificationId = 13, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Uf value
                new ProductSpecification { Id = 15, ProductDetailsId = 2, SpecificationId = 16, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 4 chambered
                new ProductSpecification { Id = 16, ProductDetailsId = 2, SpecificationId = 17, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Steel reinforcement
                new ProductSpecification { Id = 17, ProductDetailsId = 2, SpecificationId = 18, DisplayOrder = 9, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // TPE gasket
                
                // EMA-60S Specifications (ProductDetailsId=3)
                new ProductSpecification { Id = 18, ProductDetailsId = 3, SpecificationId = 3, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 60mm sash
                new ProductSpecification { Id = 19, ProductDetailsId = 3, SpecificationId = 7, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 126mm frame
                new ProductSpecification { Id = 20, ProductDetailsId = 3, SpecificationId = 8, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 2.3mm thickness
                new ProductSpecification { Id = 21, ProductDetailsId = 3, SpecificationId = 11, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Single glazing
                new ProductSpecification { Id = 22, ProductDetailsId = 3, SpecificationId = 12, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Double glazing
                new ProductSpecification { Id = 23, ProductDetailsId = 3, SpecificationId = 13, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Uf value
                new ProductSpecification { Id = 24, ProductDetailsId = 3, SpecificationId = 15, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 3 chambered
                new ProductSpecification { Id = 25, ProductDetailsId = 3, SpecificationId = 17, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Steel reinforcement
                new ProductSpecification { Id = 26, ProductDetailsId = 3, SpecificationId = 18, DisplayOrder = 9, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // TPE gasket
                
                // EMA-STYLE Specifications (ProductDetailsId=4)
                new ProductSpecification { Id = 27, ProductDetailsId = 4, SpecificationId = 2, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 47mm sash
                new ProductSpecification { Id = 28, ProductDetailsId = 4, SpecificationId = 6, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // 110mm frame
                new ProductSpecification { Id = 29, ProductDetailsId = 4, SpecificationId = 10, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 2.6mm thickness
                new ProductSpecification { Id = 30, ProductDetailsId = 4, SpecificationId = 11, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Single glazing
                new ProductSpecification { Id = 31, ProductDetailsId = 4, SpecificationId = 12, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Double glazing
                new ProductSpecification { Id = 32, ProductDetailsId = 4, SpecificationId = 13, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Uf value
                new ProductSpecification { Id = 33, ProductDetailsId = 4, SpecificationId = 14, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // 2 chambered
                new ProductSpecification { Id = 34, ProductDetailsId = 4, SpecificationId = 17, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }, // Steel reinforcement
                new ProductSpecification { Id = 35, ProductDetailsId = 4, SpecificationId = 18, DisplayOrder = 9, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }  // TPE gasket
            );

            // Seed Shared Colors (available colors used across products)
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, NameEn = "White", NameAr = "أبيض", CssClass = "white", HexCode = "#FFFFFF", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 2, NameEn = "Grey", NameAr = "رمادي", CssClass = "grey", HexCode = "#D9D9D9", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 3, NameEn = "Off white", NameAr = "أبيض فاتح", CssClass = "off-white", HexCode = "#FFFFB3", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 4, NameEn = "Beige", NameAr = "بيج", CssClass = "beige", HexCode = "#F5F5DC", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 5, NameEn = "Brown", NameAr = "بني", CssClass = "brown", HexCode = "#8B4513", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 6, NameEn = "Dark Grey", NameAr = "رمادي غامق", CssClass = "dark-grey", HexCode = "#505050", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 7, NameEn = "Light Grey", NameAr = "رمادي فاتح", CssClass = "light-grey", HexCode = "#E8E8E8", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Color { Id = 8, NameEn = "Dark Brown", NameAr = "بني غامق", CssClass = "dark-brown", HexCode = "#4A2511", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Product-Color relationships (Many-to-Many junction table)
            modelBuilder.Entity<ProductColor>().HasData(
                // EMA-42S Colors (4 colors)
                new ProductColor { Id = 1, ProductDetailsId = 1, ColorId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // White
                new ProductColor { Id = 2, ProductDetailsId = 1, ColorId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Grey
                new ProductColor { Id = 3, ProductDetailsId = 1, ColorId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Off white
                new ProductColor { Id = 4, ProductDetailsId = 1, ColorId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Beige
                
                // EMA-60 Colors (4 colors)
                new ProductColor { Id = 5, ProductDetailsId = 2, ColorId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // White
                new ProductColor { Id = 6, ProductDetailsId = 2, ColorId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Grey
                new ProductColor { Id = 7, ProductDetailsId = 2, ColorId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Off white
                new ProductColor { Id = 8, ProductDetailsId = 2, ColorId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Beige
                
                // EMA-60S Colors (4 colors)
                new ProductColor { Id = 9, ProductDetailsId = 3, ColorId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },   // White
                new ProductColor { Id = 10, ProductDetailsId = 3, ColorId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Grey
                new ProductColor { Id = 11, ProductDetailsId = 3, ColorId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Off white
                new ProductColor { Id = 12, ProductDetailsId = 3, ColorId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Beige
                
                // EMA-STYLE Colors (8 colors - all available colors)
                new ProductColor { Id = 13, ProductDetailsId = 4, ColorId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // White
                new ProductColor { Id = 14, ProductDetailsId = 4, ColorId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Grey
                new ProductColor { Id = 15, ProductDetailsId = 4, ColorId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Off white
                new ProductColor { Id = 16, ProductDetailsId = 4, ColorId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Beige
                new ProductColor { Id = 17, ProductDetailsId = 4, ColorId = 5, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Brown
                new ProductColor { Id = 18, ProductDetailsId = 4, ColorId = 6, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Dark Grey
                new ProductColor { Id = 19, ProductDetailsId = 4, ColorId = 7, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // Light Grey
                new ProductColor { Id = 20, ProductDetailsId = 4, ColorId = 8, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }   // Dark Brown
            );

            // Seed Shared Certificates (certificates used across products)
            modelBuilder.Entity<Certificate>().HasData(
                new Certificate { Id = 1, NameEn = "ISO", NameAr = "ايزو", ImagePath = "/images/product/iso.png", AltText = "ISO", Width = 40, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Certificate { Id = 2, NameEn = "EOS", NameAr = "المواصفات المصرية", ImagePath = "/images/product/eos.png", AltText = "EOS", Width = 40, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Certificate { Id = 3, NameEn = "CER", NameAr = "شهادة", ImagePath = "/images/product/cer.png", AltText = "CER", Width = 40, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Certificate { Id = 4, NameEn = "NR", NameAr = "NR", ImagePath = "/images/product/NR.png", AltText = "NR", Width = 40, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Product-Certificate relationships (Many-to-Many junction table)
            modelBuilder.Entity<ProductCertificate>().HasData(
                // EMA-42S Certificates (all 4)
                new ProductCertificate { Id = 1, ProductDetailsId = 1, CertificateId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // ISO
                new ProductCertificate { Id = 2, ProductDetailsId = 1, CertificateId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // EOS
                new ProductCertificate { Id = 3, ProductDetailsId = 1, CertificateId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // CER
                new ProductCertificate { Id = 4, ProductDetailsId = 1, CertificateId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // NR
                
                // EMA-60 Certificates (all 4)
                new ProductCertificate { Id = 5, ProductDetailsId = 2, CertificateId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // ISO
                new ProductCertificate { Id = 6, ProductDetailsId = 2, CertificateId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // EOS
                new ProductCertificate { Id = 7, ProductDetailsId = 2, CertificateId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // CER
                new ProductCertificate { Id = 8, ProductDetailsId = 2, CertificateId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // NR
                
                // EMA-60S Certificates (all 4)
                new ProductCertificate { Id = 9, ProductDetailsId = 3, CertificateId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },   // ISO
                new ProductCertificate { Id = 10, ProductDetailsId = 3, CertificateId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // EOS
                new ProductCertificate { Id = 11, ProductDetailsId = 3, CertificateId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // CER
                new ProductCertificate { Id = 12, ProductDetailsId = 3, CertificateId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // NR
                
                // EMA-STYLE Certificates (all 4)
                new ProductCertificate { Id = 13, ProductDetailsId = 4, CertificateId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // ISO
                new ProductCertificate { Id = 14, ProductDetailsId = 4, CertificateId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // EOS
                new ProductCertificate { Id = 15, ProductDetailsId = 4, CertificateId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },  // CER
                new ProductCertificate { Id = 16, ProductDetailsId = 4, CertificateId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }   // NR
            );

            // Seed Design Options (shared images)
            modelBuilder.Entity<DesignOption>().HasData(
                // Unique design options with images only
                new DesignOption { Id = 1, ImagePath = "/images/product/win1.png", AltText = "Single panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 2, ImagePath = "/images/product/win2.png", AltText = "Two panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 3, ImagePath = "/images/product/win3.png", AltText = "Three panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 4, ImagePath = "/images/product/win4.png", AltText = "Four panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 5, ImagePath = "/images/product/win5.png", AltText = "Five panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 6, ImagePath = "/images/product/win6.png", AltText = "Six panel design", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
              
                new DesignOption { Id = 7, ImagePath = "/images/product/win8.png", AltText = "EMA-60 design 1", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 8, ImagePath = "/images/product/win9.png", AltText = "EMA-60 design 2", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 9, ImagePath = "/images/product/win10.png", AltText = "EMA-60 design 3", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 10, ImagePath = "/images/product/win11.png", AltText = "EMA-60 design 4", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 11, ImagePath = "/images/product/win12.png", AltText = "EMA-60 design 5", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 12, ImagePath = "/images/product/win13.png", AltText = "EMA-60 design 6", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 13, ImagePath = "/images/product/win14.png", AltText = "EMA-60 design 7", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 14, ImagePath = "/images/product/win7.png", AltText = "EMA-60 design 8", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

                new DesignOption { Id = 15, ImagePath = "/images/product/win1.png", AltText = "EMA-60S design 1", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 16, ImagePath = "/images/product/win2.png", AltText = "EMA-60S design 2", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 17, ImagePath = "/images/product/win3.png", AltText = "EMA-60S design 3", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 18, ImagePath = "/images/product/win4.png", AltText = "EMA-60S design 4", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 19, ImagePath = "/images/product/win5.png", AltText = "EMA-60S design 5", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 20, ImagePath = "/images/product/win6.png", AltText = "EMA-60S design 6", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 21, ImagePath = "/images/product/win1.png", AltText = "EMA-STYLE design 1", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 22, ImagePath = "/images/product/win2.png", AltText = "EMA-STYLE design 2", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 23, ImagePath = "/images/product/win3.png", AltText = "EMA-STYLE design 3", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 24, ImagePath = "/images/product/win4.png", AltText = "EMA-STYLE design 4", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 25, ImagePath = "/images/product/win5.png", AltText = "EMA-STYLE design 5", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new DesignOption { Id = 26, ImagePath = "/images/product/win6.png", AltText = "EMA-STYLE design 6", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed ProductDesignOption relationships (junction table)
            modelBuilder.Entity<ProductDesignOption>().HasData(
                // EMA-42S Design Options (6 options)
                new ProductDesignOption { Id = 1, ProductDetailsId = 1, DesignOptionId = 1, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 2, ProductDetailsId = 1, DesignOptionId = 2, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 3, ProductDetailsId = 1, DesignOptionId = 3, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 4, ProductDetailsId = 1, DesignOptionId = 4, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 5, ProductDetailsId = 1, DesignOptionId = 5, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 6, ProductDetailsId = 1, DesignOptionId = 6, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                
                // EMA-60 Design Options (8 options)
                new ProductDesignOption { Id = 7, ProductDetailsId = 2, DesignOptionId = 7, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 8, ProductDetailsId = 2, DesignOptionId = 8, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 9, ProductDetailsId = 2, DesignOptionId = 9, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 10, ProductDetailsId = 2, DesignOptionId = 10, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 11, ProductDetailsId = 2, DesignOptionId = 11, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 12, ProductDetailsId = 2, DesignOptionId = 12, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 13, ProductDetailsId = 2, DesignOptionId = 13, DisplayOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 14, ProductDetailsId = 2, DesignOptionId = 14, DisplayOrder = 8, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                
                // EMA-60S Design Options (6 options)
                new ProductDesignOption { Id = 15, ProductDetailsId = 3, DesignOptionId = 15, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 16, ProductDetailsId = 3, DesignOptionId = 16, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 17, ProductDetailsId = 3, DesignOptionId = 17, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 18, ProductDetailsId = 3, DesignOptionId = 18, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 19, ProductDetailsId = 3, DesignOptionId = 19, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 20, ProductDetailsId = 3, DesignOptionId = 20, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                
                // EMA-STYLE Design Options (6 options)
                new ProductDesignOption { Id = 21, ProductDetailsId = 4, DesignOptionId = 21, DisplayOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 22, ProductDetailsId = 4, DesignOptionId = 22, DisplayOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 23, ProductDetailsId = 4, DesignOptionId = 23, DisplayOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 24, ProductDetailsId = 4, DesignOptionId = 24, DisplayOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 25, ProductDetailsId = 4, DesignOptionId = 25, DisplayOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new ProductDesignOption { Id = 26, ProductDetailsId = 4, DesignOptionId = 26, DisplayOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
