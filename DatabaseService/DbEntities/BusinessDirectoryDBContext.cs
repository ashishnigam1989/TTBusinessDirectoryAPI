﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<BrandCategory> BrandCategory { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryIndustry> CategoryIndustry { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyAddress> CompanyAddress { get; set; }
        public virtual DbSet<CompanyAwards> CompanyAwards { get; set; }
        public virtual DbSet<CompanyBanners> CompanyBanners { get; set; }
        public virtual DbSet<CompanyBrand> CompanyBrand { get; set; }
        public virtual DbSet<CompanyCategory> CompanyCategory { get; set; }
        public virtual DbSet<CompanyDynamicMenu> CompanyDynamicMenu { get; set; }
        public virtual DbSet<CompanyEvents> CompanyEvents { get; set; }
        public virtual DbSet<CompanyGalleryAttachment> CompanyGalleryAttachment { get; set; }
        public virtual DbSet<CompanyLinks> CompanyLinks { get; set; }
        public virtual DbSet<CompanyNewsArticle> CompanyNewsArticle { get; set; }
        public virtual DbSet<CompanyOffers> CompanyOffers { get; set; }
        public virtual DbSet<CompanyPackage> CompanyPackage { get; set; }
        public virtual DbSet<CompanyProduct> CompanyProduct { get; set; }
        public virtual DbSet<CompanyReviewLike> CompanyReviewLike { get; set; }
        public virtual DbSet<CompanyService> CompanyService { get; set; }
        public virtual DbSet<CompanyTags> CompanyTags { get; set; }
        public virtual DbSet<CompanyTeams> CompanyTeams { get; set; }
        public virtual DbSet<CompanyUsers> CompanyUsers { get; set; }
        public virtual DbSet<CompanyVideos> CompanyVideos { get; set; }
        public virtual DbSet<CompanyVouchers> CompanyVouchers { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CountryCode> CountryCode { get; set; }
        public virtual DbSet<Designations> Designations { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<EventPassType> EventPassType { get; set; }
        public virtual DbSet<EventRegion> EventRegion { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<FreeListing> FreeListing { get; set; }
        public virtual DbSet<FreeListingDetails> FreeListingDetails { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<InsightType> InsightType { get; set; }
        public virtual DbSet<Insights> Insights { get; set; }
        public virtual DbSet<MenuRolePermission> MenuRolePermission { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserSocialLogins> UserSocialLogins { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<SearchDBModel> SearchModel { get; set; }
        public virtual DbSet<SearchPageDBModel> SearchPageModel { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=185.182.184.243;Database=BusinessDirectoryDB;User Id=businessdir;Password=BusinessDir@123;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.NameEng)
                    .HasName("NonClusteredIndex-NameEng");

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

            modelBuilder.Entity<BrandCategory>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.NameEng)
                    .HasName("NonClusteredIndex-NameEng");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(256)
                    .IsUnicode(false);

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

            modelBuilder.Entity<CategoryIndustry>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.CountryId)
                    .HasName("NonClusteredIndex-CountryId");

                entity.HasIndex(e => e.NameEng)
                    .HasName("NonClusteredIndex-NameEng");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.AppleStoreUrl).HasMaxLength(250);

                entity.Property(e => e.BlackBerryStoreUrl).HasMaxLength(250);

                entity.Property(e => e.BrochureLink).HasMaxLength(150);

                entity.Property(e => e.CountryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CouponUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.DomainName).HasMaxLength(150);

                entity.Property(e => e.EmployeeNum).HasMaxLength(20);

                entity.Property(e => e.EstablishmentDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookUrl).HasMaxLength(100);

                entity.Property(e => e.FoundedYear).HasMaxLength(20);

                entity.Property(e => e.FounderName).HasMaxLength(100);

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

                entity.Property(e => e.UniqueName).HasMaxLength(50);

                entity.Property(e => e.VerifiedTime).HasColumnType("datetime");

                entity.Property(e => e.WindowsStoreUrl).HasMaxLength(250);
            });

            modelBuilder.Entity<CompanyAddress>(entity =>
            {
                entity.Property(e => e.AddressDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.GoogleLocation).IsUnicode(false);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyAwards>(entity =>
            {
                entity.Property(e => e.AwardDesc).IsRequired();

                entity.Property(e => e.AwardTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyBanners>(entity =>
            {
                entity.Property(e => e.ArabicUrl).HasColumnName("ArabicURL");

                entity.Property(e => e.BannerExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.BannerStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EnglishUrl).HasColumnName("EnglishURL");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyBrand>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyCategory>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyDynamicMenu>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyEvents>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EventDesc).IsRequired();

                entity.Property(e => e.EventLocationUrl)
                    .HasColumnName("EventLocationURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EventTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventUrl)
                    .HasColumnName("EventURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyGalleryAttachment>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyLinks>(entity =>
            {
                entity.Property(e => e.ArabicUrl)
                    .HasColumnName("ArabicURL")
                    .HasMaxLength(250);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EnglishUrl)
                    .HasColumnName("EnglishURL")
                    .HasMaxLength(250);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.LinkNameArb).HasMaxLength(250);

                entity.Property(e => e.LinkNameEng).HasMaxLength(250);

                entity.Property(e => e.Target).HasMaxLength(50);
            });

            modelBuilder.Entity<CompanyNewsArticle>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.NewsDesc).IsRequired();

                entity.Property(e => e.NewsTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NewsUrl).HasColumnName("NewsURL");
            });

            modelBuilder.Entity<CompanyOffers>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(150);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.OfferDisplayDate).HasColumnType("datetime");

                entity.Property(e => e.OfferEndDate).HasColumnType("datetime");

                entity.Property(e => e.OfferNameArb).HasMaxLength(150);

                entity.Property(e => e.OfferNameEng).HasMaxLength(150);

                entity.Property(e => e.OfferShortDescriptionArb).HasMaxLength(250);

                entity.Property(e => e.OfferShortDescriptionEng).HasMaxLength(250);

                entity.Property(e => e.OfferStartDate).HasColumnType("datetime");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CompanyPackage>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyProduct>(entity =>
            {
                entity.HasIndex(e => e.CompanyId)
                    .HasName("NonClusteredIndex-CompanyId");

                entity.HasIndex(e => e.NameEng)
                    .HasName("NonClusteredIndex-NameEng");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(150);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.OfferEndDate).HasColumnType("datetime");

                entity.Property(e => e.OfferStartDate).HasColumnType("datetime");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PartNumber).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortDescriptionArb).HasMaxLength(250);

                entity.Property(e => e.ShortDescriptionEng).HasMaxLength(250);

                entity.Property(e => e.WarrantyArb).HasMaxLength(150);

                entity.Property(e => e.WarrantyEng).HasMaxLength(150);
            });

            modelBuilder.Entity<CompanyReviewLike>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyService>(entity =>
            {
                entity.HasIndex(e => e.CompanyId)
                    .HasName("NonClusteredIndex-CompanyId");

                entity.HasIndex(e => e.NameEng)
                    .HasName("NonClusteredIndex-NameEng");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(150);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.OfferEndDate).HasColumnType("datetime");

                entity.Property(e => e.OfferStartDate).HasColumnType("datetime");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortDescriptionArb).HasMaxLength(250);

                entity.Property(e => e.ShortDescriptionEng).HasMaxLength(250);
            });

            modelBuilder.Entity<CompanyTags>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.TagName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyTeams>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyUsers>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CompanyVideos>(entity =>
            {
                entity.Property(e => e.ArabicUrl)
                    .HasColumnName("ArabicURL")
                    .HasMaxLength(250);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EnglishUrl)
                    .HasColumnName("EnglishURL")
                    .HasMaxLength(250);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.VideoNameArb).HasMaxLength(250);

                entity.Property(e => e.VideoNameEng).HasMaxLength(250);
            });

            modelBuilder.Entity<CompanyVouchers>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VoucherDisplayDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherEndDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherNameArb).HasMaxLength(150);

                entity.Property(e => e.VoucherNameEng).HasMaxLength(150);

                entity.Property(e => e.VoucherShortDescriptionArb).HasMaxLength(250);

                entity.Property(e => e.VoucherShortDescriptionEng).HasMaxLength(250);

                entity.Property(e => e.VoucherStartDate).HasColumnType("datetime");
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

            modelBuilder.Entity<CountryCode>(entity =>
            {
                entity.Property(e => e.CodeIcon)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CodeName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Designations>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.DesignationDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
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

            modelBuilder.Entity<EventPassType>(entity =>
            {
                entity.HasKey(e => e.PassTypeId);

                entity.Property(e => e.PassTypeId).ValueGeneratedNever();

                entity.Property(e => e.PassTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.Property(e => e.RegionId).ValueGeneratedNever();

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.Property(e => e.EventTypeId).ValueGeneratedNever();

                entity.Property(e => e.EventTypeDesc)
                    .IsRequired()
                    .HasMaxLength(50)
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

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.FounderName).HasMaxLength(100);

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

                entity.Property(e => e.RelatedService).HasMaxLength(150);
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.MetaDescriptionArb).HasMaxLength(160);

                entity.Property(e => e.MetaDescriptionEng).HasMaxLength(160);

                entity.Property(e => e.MetaTitleArb).HasMaxLength(70);

                entity.Property(e => e.MetaTitleEng).HasMaxLength(70);

                entity.Property(e => e.NameArb).HasMaxLength(150);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<InsightType>(entity =>
            {
                entity.Property(e => e.InsightTypeId).ValueGeneratedNever();

                entity.Property(e => e.InsigtTypeDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insights>(entity =>
            {
                entity.HasKey(e => e.InsightId);

                entity.Property(e => e.InsightId).HasColumnName("InsightID");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventAddress)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EventInfo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventLocationUrl)
                    .HasColumnName("EventLocationURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EventUrl)
                    .HasColumnName("EventURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InsertedOn).HasColumnType("smalldatetime");

                entity.Property(e => e.InsightContent)
                    .HasColumnName("Insight_Content")
                    .IsUnicode(false);

                entity.Property(e => e.InsightImage)
                    .HasColumnName("Insight_Image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsightTitle)
                    .IsRequired()
                    .HasColumnName("Insight_Title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MainLevelId).HasColumnName("Main_LevelID");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("smalldatetime");
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

                entity.Property(e => e.CountryCode).HasMaxLength(20);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(256);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EmailConfirmationCode).HasMaxLength(128);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.PasswordResetCode).HasMaxLength(328);

                modelBuilder.Entity<SearchDBModel>(entity =>
                {
                    entity.HasNoKey();
                });

                modelBuilder.Entity<SearchPageDBModel>(entity =>
                {
                    entity.HasNoKey();
                });

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
