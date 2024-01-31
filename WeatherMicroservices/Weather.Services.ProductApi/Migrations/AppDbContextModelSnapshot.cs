﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weather.Services.ProductApi.Data;

#nullable disable

namespace Weather.Services.ProductApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Weather.Services.ProductApi.Models.Genre", b =>
                {
                    b.Property<Guid>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GenreId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Name = "Drinks"
                        });
                });

            modelBuilder.Entity("Weather.Services.ProductApi.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId");

                    b.HasIndex("GenreId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Products", t =>
                        {
                            t.HasCheckConstraint("CK_Amount_GreaterThanZero", "Amount > 0");

                            t.HasCheckConstraint("CK_Discount_GreaterThanZero", "Discount > 0");

                            t.HasCheckConstraint("CK_Price_GreaterThanZero", "Price > 0");
                        });

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("1ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Amount = 100,
                            GenreId = new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Name = "Cola",
                            Price = 0.89m,
                            Slug = "cola"
                        },
                        new
                        {
                            ProductId = new Guid("2ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Amount = 110,
                            GenreId = new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Name = "Pepsi",
                            Price = 0.49m,
                            Slug = "pepsi"
                        },
                        new
                        {
                            ProductId = new Guid("3ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Amount = 120,
                            GenreId = new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                            Name = "Fanta",
                            Price = 0.59m,
                            Slug = "fanta"
                        });
                });

            modelBuilder.Entity("Weather.Services.ProductApi.Models.Product", b =>
                {
                    b.HasOne("Weather.Services.ProductApi.Models.Genre", "Genre")
                        .WithMany("Products")
                        .HasForeignKey("GenreId");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Weather.Services.ProductApi.Models.Genre", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
