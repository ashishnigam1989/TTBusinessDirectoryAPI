using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class BusinessDirectoryDBContext : DbContext
    {
        public BusinessDirectoryDBContext()
        {
        }

        public BusinessDirectoryDBContext(DbContextOptions<BusinessDirectoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<FreeListing> FreeListing { get; set; }
        public virtual DbSet<FreeListingDetails> FreeListingDetails { get; set; }
        public virtual DbSet<MenuRolePermission> MenuRolePermission { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserSocialLogins> UserSocialLogins { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SQL8003.site4now.net;Database=db_a9174a_ttbusiness;User Id=db_a9174a_ttbusiness_admin;Password=ttbusiness#2019;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.KeywordsArb).HasMaxLength(70);

                entity.Property(e => e.KeywordsEng).HasMaxLength(70);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Logo).HasMaxLength(255);

                entity.Property(e => e.MetaDescriptionArb).HasMaxLength(160);

                entity.Property(e => e.MetaDescriptionEng).HasMaxLength(160);

                entity.Property(e => e.MetaTitleArb).HasMaxLength(70);

                entity.Property(e => e.MetaTitleEng).HasMaxLength(70);

                entity.Property(e => e.NameArb).HasMaxLength(250);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Slug).HasMaxLength(250);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.MetaDescriptionArb).HasMaxLength(160);

                entity.Property(e => e.MetaDescriptionEng).HasMaxLength(160);

                entity.Property(e => e.MetaTitleArb).HasMaxLength(70);

                entity.Property(e => e.MetaTitleEng).HasMaxLength(70);

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Slug).HasMaxLength(150);

                entity.Property(e => e.Unspsccode)
                    .HasColumnName("UNSPSCCode")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.AppleStoreUrl).HasMaxLength(250);

                entity.Property(e => e.BlackBerryStoreUrl).HasMaxLength(250);

                entity.Property(e => e.BrochureLink).HasMaxLength(150);

                entity.Property(e => e.CouponUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.DomainName).HasMaxLength(150);

                entity.Property(e => e.EstablishmentDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookUrl).HasMaxLength(100);

                entity.Property(e => e.GooglePlaystoreUrl).HasMaxLength(250);

                entity.Property(e => e.GooglePlusUrl).HasMaxLength(100);

                entity.Property(e => e.InstagramUrl).HasMaxLength(100);

                entity.Property(e => e.Iso).HasMaxLength(100);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.LinkedInUrl).HasMaxLength(100);

                entity.Property(e => e.Logo).HasMaxLength(100);

                entity.Property(e => e.MetaTitle).HasMaxLength(250);

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.OfferUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.OverallRating).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pobox)
                    .HasColumnName("POBox")
                    .HasMaxLength(10);

                entity.Property(e => e.PrimaryEmail).HasMaxLength(50);

                entity.Property(e => e.PrimaryFax).HasMaxLength(20);

                entity.Property(e => e.PrimaryGpsLocation).HasMaxLength(50);

                entity.Property(e => e.PrimaryMobile).HasMaxLength(20);

                entity.Property(e => e.PrimaryPhone).HasMaxLength(20);

                entity.Property(e => e.PrimaryWebsite).HasMaxLength(50);

                entity.Property(e => e.ThemeColor).HasMaxLength(50);

                entity.Property(e => e.TradeLicenceNumber).HasMaxLength(50);

                entity.Property(e => e.TwitterUrl).HasMaxLength(100);

                entity.Property(e => e.UniqueName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VerifiedTime).HasColumnType("datetime");

                entity.Property(e => e.WindowsStoreUrl).HasMaxLength(250);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.AreaInSqKm).HasMaxLength(20);

                entity.Property(e => e.Capital).HasMaxLength(30);

                entity.Property(e => e.Continent).HasMaxLength(2);

                entity.Property(e => e.ContinentNameArb).HasMaxLength(15);

                entity.Property(e => e.ContinentNameEng).HasMaxLength(15);

                entity.Property(e => e.CountryCode).HasMaxLength(3);

                entity.Property(e => e.CountryNameArb).HasMaxLength(50);

                entity.Property(e => e.CountryNameEng)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode).HasMaxLength(3);

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.East).HasMaxLength(30);

                entity.Property(e => e.FipsCode).HasMaxLength(2);

                entity.Property(e => e.IsoAlpha3).HasMaxLength(3);

                entity.Property(e => e.IsoNumeric).HasMaxLength(4);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.North).HasMaxLength(30);

                entity.Property(e => e.Population).HasMaxLength(20);

                entity.Property(e => e.South).HasMaxLength(30);

                entity.Property(e => e.West).HasMaxLength(30);
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.DistrictId);

                entity.Property(e => e.CreationTime).HasColumnType("smalldatetime");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FreeListing>(entity =>
            {
                entity.Property(e => e.CompanyAddress)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CompanyPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ContactMobile)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.Iso).HasMaxLength(100);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Logo).HasMaxLength(100);

                entity.Property(e => e.Pobox)
                    .IsRequired()
                    .HasColumnName("POBox")
                    .HasMaxLength(10);

                entity.Property(e => e.PrimaryEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrimaryWebsite).HasMaxLength(50);
            });

            modelBuilder.Entity<FreeListingDetails>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(150);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.RelatedProduct).HasMaxLength(150);
            });

            modelBuilder.Entity<MenuRolePermission>(entity =>
            {
                entity.HasKey(e => e.MenuPermissionId);

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.ComponentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.MenuIcon)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuPath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserSocialLogins>(entity =>
            {
                entity.ToTable("UserSocial_Logins");

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderKey)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.AuthenticationSource).HasMaxLength(64);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EmailConfirmationCode).HasMaxLength(128);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordResetCode).HasMaxLength(328);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
