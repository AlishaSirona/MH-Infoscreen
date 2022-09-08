﻿// <auto-generated />
using System;
using DbInfoscreenLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbInfoscreenLibrary.Migrations
{
    [DbContext(typeof(DbInfoscreenContext))]
    [Migration("20220908065528_RenameOrder")]
    partial class RenameOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
#pragma warning restore 612, 618
        }
    }
}
