﻿// <auto-generated />
using System;
using Hospital.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hospital.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hospital.Models.Appointments", b =>
                {
                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<int>("Minute")
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Hour", "Minute", "Day", "Year", "Month");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Hospital.Models.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AuditLogType")
                        .HasColumnType("int");

                    b.Property<string>("Changes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WhoChange")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Hospital.Models.Doctors", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Hospital.Models.DoctorsRating", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("UserID", "DoctorID");

                    b.ToTable("DoctorsRatings");
                });

            modelBuilder.Entity("Hospital.Models.Pills", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DosageForm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormFactor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Producer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pills");
                });

            modelBuilder.Entity("Hospital.Models.Roles", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleOrder")
                        .HasColumnType("int");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = new Guid("1ab783cd-9eaf-4397-ad57-4d2a62abf48e"),
                            RoleName = "Client",
                            RoleOrder = 0
                        },
                        new
                        {
                            RoleID = new Guid("4c0f9bb5-fcc4-447d-baac-5d30363f757e"),
                            RoleName = "Doctor",
                            RoleOrder = 1
                        },
                        new
                        {
                            RoleID = new Guid("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578"),
                            RoleName = "Head Doctor",
                            RoleOrder = 2
                        });
                });

            modelBuilder.Entity("Hospital.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserRole")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d269c295-8c9c-4e1d-97af-8f3bafaf6520"),
                            Address = "Chernivtsi",
                            Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            Gender = "Male",
                            Name = "Oleksandr",
                            Password = "P@ssw0rd",
                            Phone = "380951483461",
                            Surname = "Syrotiuk",
                            UserRole = new Guid("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578")
                        },
                        new
                        {
                            Id = new Guid("34ae203d-9185-482a-853a-19533e3ca44a"),
                            Address = "Chernivtsi",
                            Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "client@gmail.com",
                            Gender = "Male",
                            Name = "Pitsul",
                            Password = "P@ssw0rd",
                            Phone = "380668122551",
                            Surname = "Andrii",
                            UserRole = new Guid("4c0f9bb5-fcc4-447d-baac-5d30363f757e")
                        });
                });

            modelBuilder.Entity("Hospital.Models.UserToken", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("ExpireDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserID", "Token");

                    b.ToTable("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}