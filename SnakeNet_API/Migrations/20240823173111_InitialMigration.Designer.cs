﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SnakeNet_API.DAL;



#nullable disable

namespace SnakeNet_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240823173111_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Elimination", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Healthy")
                        .HasColumnType("bit");

                    b.Property<string>("SnakeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SnakeId");

                    b.ToTable("Eliminations");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Enclosure", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Depth")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Lenght")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Enclosures");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.EnclosureReading", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EnclosureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EnclosureSide")
                        .HasColumnType("int");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<int>("Temperature")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnclosureId");

                    b.ToTable("EnclosureReading");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.FeedingRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Feeder")
                        .HasColumnType("int");

                    b.Property<int>("FeederWeight")
                        .HasColumnType("int");

                    b.Property<string>("SnakeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SnakeId");

                    b.ToTable("FeedingRecord");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.GrowthRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Lenght")
                        .HasColumnType("int");

                    b.Property<string>("SnakeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SnakeId");

                    b.ToTable("GrowthRecords");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Snake", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Snakes");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Elimination", b =>
                {
                    b.HasOne("SnakeNet_API.Models.Entities.Snake", "Snake")
                        .WithMany()
                        .HasForeignKey("SnakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snake");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.EnclosureReading", b =>
                {
                    b.HasOne("SnakeNet_API.Models.Entities.Enclosure", "Enclosure")
                        .WithMany()
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.FeedingRecord", b =>
                {
                    b.HasOne("SnakeNet_API.Models.Entities.Snake", "Snake")
                        .WithMany()
                        .HasForeignKey("SnakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snake");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.GrowthRecord", b =>
                {
                    b.HasOne("SnakeNet_API.Models.Entities.Snake", "Snake")
                        .WithMany()
                        .HasForeignKey("SnakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snake");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Snake", b =>
                {
                    b.HasOne("SnakeNet_API.Models.Entities.Enclosure", "Enclosure")
                        .WithOne("Snake")
                        .HasForeignKey("SnakeNet_API.Models.Entities.Snake", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("SnakeNet_API.Models.Entities.Enclosure", b =>
                {
                    b.Navigation("Snake")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
