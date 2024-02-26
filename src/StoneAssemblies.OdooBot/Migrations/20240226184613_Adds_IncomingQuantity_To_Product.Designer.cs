﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoneAssemblies.OdooBot.Services;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240226184613_Adds_IncomingQuantity_To_Product")]
    partial class Adds_IncomingQuantity_To_Product
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<long?>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("Size", "ExternalId")
                        .IsUnique()
                        .HasFilter("ExternalId IS NOT NULL");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("InStockQuantity")
                        .HasColumnType("REAL");

                    b.Property<double>("IncomingQuantity")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("QuantityUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("StandardPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Image", b =>
                {
                    b.HasOne("StoneAssemblies.OdooBot.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Product", b =>
                {
                    b.HasOne("StoneAssemblies.OdooBot.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StoneAssemblies.OdooBot.Entities.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
