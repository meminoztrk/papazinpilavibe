﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLayer.Repository;

#nullable disable

namespace NLayer.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231010131849_initial15")]
    partial class initial15
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NLayer.Core.Models.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeoDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeoKeywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Youtube")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("About");
                });

            modelBuilder.Entity("NLayer.Core.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessLicense")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessProps")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessServices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasCourier")
                        .HasColumnType("bit");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MapIframe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinHeader")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Process")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<string>("Sa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Su")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Th")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.Property<string>("We")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("UserId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("BusinessComment");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessFAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessFAQ");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessImage");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessSubComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessCommentId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("BusinessSubComment");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessUserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessCommentId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("BusinessUserImage");
                });

            modelBuilder.Entity("NLayer.Core.Models.FavoriteBusiness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteBusiness");
                });

            modelBuilder.Entity("NLayer.Core.Models.FavoriteComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessCommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessCommentId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteComment");
                });

            modelBuilder.Entity("NLayer.Core.Models.OwnerRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Process")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("OwnerRequest");
                });

            modelBuilder.Entity("NLayer.Core.Models.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("MahalleID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MergedArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SehirIlceMahalleAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UstID")
                        .HasColumnType("int");

                    b.Property<string>("maxlatitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("maxlongitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("minlatitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("minlongitude")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("NLayer.Core.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("IsBusinessReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ReportText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessCommentId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("NLayer.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleCredential")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBusiness")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGoogle")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUser")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("NLayer.Core.Models.Business", b =>
                {
                    b.HasOne("NLayer.Core.Models.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany("Businesses")
                        .HasForeignKey("UserId");

                    b.Navigation("Province");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessComment", b =>
                {
                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany("BusinessComments")
                        .HasForeignKey("BusinessId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany("BusinessComments")
                        .HasForeignKey("UserId");

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessFAQ", b =>
                {
                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId");

                    b.Navigation("Business");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessImage", b =>
                {
                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany("BusinessImages")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessSubComment", b =>
                {
                    b.HasOne("NLayer.Core.Models.BusinessComment", "BusinessComment")
                        .WithMany("BusinessSubComments")
                        .HasForeignKey("BusinessCommentId");

                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Business");

                    b.Navigation("BusinessComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessUserImage", b =>
                {
                    b.HasOne("NLayer.Core.Models.BusinessComment", "BusinessComment")
                        .WithMany("BusinessUserImages")
                        .HasForeignKey("BusinessCommentId");

                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany("BusinessUserImages")
                        .HasForeignKey("UserId");

                    b.Navigation("Business");

                    b.Navigation("BusinessComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.FavoriteBusiness", b =>
                {
                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany("FavoriteBusinesses")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.FavoriteComment", b =>
                {
                    b.HasOne("NLayer.Core.Models.BusinessComment", "BusinessComment")
                        .WithMany("FavoriteComments")
                        .HasForeignKey("BusinessCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.OwnerRequest", b =>
                {
                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.Report", b =>
                {
                    b.HasOne("NLayer.Core.Models.BusinessComment", "BusinessComment")
                        .WithMany()
                        .HasForeignKey("BusinessCommentId");

                    b.HasOne("NLayer.Core.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId");

                    b.HasOne("NLayer.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("BusinessComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NLayer.Core.Models.User", b =>
                {
                    b.HasOne("NLayer.Core.Models.Province", "Province")
                        .WithMany("Users")
                        .HasForeignKey("ProvinceId");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("NLayer.Core.Models.Business", b =>
                {
                    b.Navigation("BusinessComments");

                    b.Navigation("BusinessImages");

                    b.Navigation("FavoriteBusinesses");
                });

            modelBuilder.Entity("NLayer.Core.Models.BusinessComment", b =>
                {
                    b.Navigation("BusinessSubComments");

                    b.Navigation("BusinessUserImages");

                    b.Navigation("FavoriteComments");
                });

            modelBuilder.Entity("NLayer.Core.Models.Province", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("NLayer.Core.Models.User", b =>
                {
                    b.Navigation("BusinessComments");

                    b.Navigation("BusinessUserImages");

                    b.Navigation("Businesses");
                });
#pragma warning restore 612, 618
        }
    }
}
