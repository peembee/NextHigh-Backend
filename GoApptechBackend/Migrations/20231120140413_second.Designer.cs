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
    [Migration("20231120140413_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoApptechBackend.Models.EmployeePoints", b =>
                {
                    b.Property<int>("EmployeePointsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeePointsID"));

                    b.Property<int>("FK_EmployeeRankID")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("EmployeePointsID");

                    b.HasIndex("FK_EmployeeRankID");

                    b.ToTable("EmployeePoints");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeeRank", b =>
                {
                    b.Property<int>("EmployeeRankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeRankID"));

                    b.Property<string>("EmployeeRankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<int>("FK_EmployeePointsID")
                        .HasColumnType("int");

                    b.Property<int>("FK_PingPongPointsID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
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

                    b.HasIndex("FK_EmployeePointsID");

                    b.HasIndex("FK_PingPongPointsID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongPoints", b =>
                {
                    b.Property<int>("PingPongPointsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PingPongPointsID"));

                    b.Property<int>("FK_PongRankID")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("PingPongPointsID");

                    b.HasIndex("FK_PongRankID");

                    b.ToTable("PingPongPoints");
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

                    b.HasKey("PingPongRankID");

                    b.ToTable("PingPongRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeePoints", b =>
                {
                    b.HasOne("GoApptechBackend.Models.EmployeeRank", "EmployeeRanks")
                        .WithMany("EmployeePoints")
                        .HasForeignKey("FK_EmployeeRankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.Person", b =>
                {
                    b.HasOne("GoApptechBackend.Models.EmployeePoints", "EmployeePoints")
                        .WithMany("Persons")
                        .HasForeignKey("FK_EmployeePointsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoApptechBackend.Models.PingPongPoints", "PingPongPoints")
                        .WithMany("Persons")
                        .HasForeignKey("FK_PingPongPointsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeePoints");

                    b.Navigation("PingPongPoints");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongPoints", b =>
                {
                    b.HasOne("GoApptechBackend.Models.PingPongRank", "PingPongRanks")
                        .WithMany("PingPongPoints")
                        .HasForeignKey("FK_PongRankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PingPongRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeePoints", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeeRank", b =>
                {
                    b.Navigation("EmployeePoints");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongPoints", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongRank", b =>
                {
                    b.Navigation("PingPongPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
