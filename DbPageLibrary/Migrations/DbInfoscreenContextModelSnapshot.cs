﻿// <auto-generated />
using System;
using DbInfoscreenLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbInfoscreenLibrary.Migrations
{
    [DbContext(typeof(DbInfoscreenContext))]
    partial class DbInfoscreenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DbInfoscreenLibrary.Pages", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Duration")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("Order")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("DbInfoscreenLibrary.VersandDaten", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Verladen")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("Date")
                        .IsUnique();

                    b.ToTable("VersandDaten");
                });
#pragma warning restore 612, 618
        }
    }
}
