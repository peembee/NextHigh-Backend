﻿// <auto-generated />
using System;
using GoApptechBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoApptechBackend.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231205124542_firstSetup")]
    partial class firstSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoApptechBackend.Models.EmployeeRank", b =>
                {
                    b.Property<int>("EmployeeRankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeRankID"));

                    b.Property<string>("EmployeeRankRankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequiredPoints")
                        .HasColumnType("int");

                    b.HasKey("EmployeeRankID");

                    b.ToTable("EmployeeRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpPoints")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("LossesInPingPong")
                        .HasColumnType("int");

                    b.Property<int>("PongPoints")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("WinningsInPingPong")
                        .HasColumnType("int");

                    b.Property<double>("YearsInPratice")
                        .HasColumnType("float");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongRank", b =>
                {
                    b.Property<int>("PingPongRankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PingPongRankID"));

                    b.Property<string>("PingPongRankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequiredPoints")
                        .HasColumnType("int");

                    b.HasKey("PingPongRankID");

                    b.ToTable("PingPongRanks");
                });
#pragma warning restore 612, 618
        }
    }
}
