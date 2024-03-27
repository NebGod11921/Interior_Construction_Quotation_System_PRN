﻿// <auto-generated />
using System;
using Infrustructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240326163729_fixroomProductplease")]
    partial class fixroomProductplease
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColourName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuotationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ColorId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductImage", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Domain.Entities.ProductQuotation", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("QuotationId")
                        .HasColumnType("int");

                    b.Property<float?>("ActualPrice")
                        .HasColumnType("real");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "QuotationId");

                    b.HasIndex("QuotationId");

                    b.ToTable("ProductQuotation");
                });

            modelBuilder.Entity("Domain.Entities.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("QuotationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<float?>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<float?>("UnitPrice")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique()
                        .HasFilter("[RoomId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Quotations");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Area")
                        .HasColumnType("real");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoomDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.RoomProduct", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<float?>("ActualPrice")
                        .HasColumnType("real");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomProduct");
                });

            modelBuilder.Entity("Domain.Entities.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoomTypeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.Quotation", "Quotation")
                        .WithMany("Payments")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Domain.Entities.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorId");

                    b.HasOne("Domain.Entities.Material", "Material")
                        .WithMany("Products")
                        .HasForeignKey("MaterialId");

                    b.Navigation("Category");

                    b.Navigation("Color");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("Domain.Entities.Image", "Image")
                        .WithMany("ProductImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.ProductQuotation", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("ProductQuotations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Quotation", "Quotation")
                        .WithMany("ProductQuotations")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("Domain.Entities.Quotation", b =>
                {
                    b.HasOne("Domain.Entities.Room", "Room")
                        .WithOne("Quotation")
                        .HasForeignKey("Domain.Entities.Quotation", "RoomId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Quotations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.HasOne("Domain.Entities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("Domain.Entities.RoomProduct", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("RoomProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Room", "Room")
                        .WithMany("RoomProducts")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("Domain.Entities.Material", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ProductQuotations");

                    b.Navigation("RoomProducts");
                });

            modelBuilder.Entity("Domain.Entities.Quotation", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("ProductQuotations");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Navigation("Quotation");

                    b.Navigation("RoomProducts");
                });

            modelBuilder.Entity("Domain.Entities.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Quotations");
                });
#pragma warning restore 612, 618
        }
    }
}
