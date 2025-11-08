using Microsoft.EntityFrameworkCore;
using UPVC.Models;

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
        public DbSet<Product> Products { get; set; }
        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<ContactPage> ContactPages { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

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
                    SubtitleAr = "كفاءة الطاقة",
                    ContentEn = "With a thermal conductivity significantly lower than aluminum, upvc possesses superior thermal insulation properties.",
                    ContentAr = "مع موصلية حرارية أقل بكثير من الألومنيوم، يمتلك uPVC خصائص عزل حراري فائقة.",
                    IsActive = true
                },
                new HomePage
                {
                    Id = 3,
                    PageKey = "Home3",
                    TitleEn = "Largest in Egypt",
                    TitleAr = "الأكبر في مصر",
                    SubtitleEn = "Market Leader",
                    SubtitleAr = "الرائد في السوق",
                    ContentEn = "Largest extrusion facility, largest number of profiles, largest network of suppliers.",
                    ContentAr = "أكبر منشأة بثق، أكبر عدد من الملفات الشخصية، أكبر شبكة من الموردين.",
                    IsActive = true
                },
                new HomePage
                {
                    Id = 4,
                    PageKey = "Home4",
                    TitleEn = "Quality Assurance",
                    TitleAr = "ضمان الجودة",
                    SubtitleEn = "International Standards",
                    SubtitleAr = "المعايير الدولية",
                    ContentEn = "Our products meet the highest international quality standards.",
                    ContentAr = "منتجاتنا تلبي أعلى معايير الجودة الدولية.",
                    IsActive = true
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
                    Url = "https://facebook.com/emapen",
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
                    Url = "https://instagram.com/emapen",
                    IconClass = "bi bi-instagram",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 4,
                    Platform = "LinkedIn",
                    Url = "https://linkedin.com/company/emapen",
                    IconClass = "bi bi-linkedin",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SocialMedia
                {
                    Id = 5,
                    Platform = "WhatsApp",
                    Url = "https://wa.me/201000000000",
                    IconClass = "bi bi-whatsapp",
                    DisplayOrder = 5,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
