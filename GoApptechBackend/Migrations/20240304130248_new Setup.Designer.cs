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
    [Migration("20240304130248_new Setup")]
    partial class newSetup
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

                    b.Property<string>("RankTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequiredPoints")
                        .HasColumnType("int");

                    b.HasKey("EmployeeRankID");

                    b.ToTable("EmployeeRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeeResult", b =>
                {
                    b.Property<int>("EmployeeResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeResultID"));

                    b.Property<int>("FK_PersonID")
                        .HasColumnType("int");

                    b.Property<int>("FK_QuizID")
                        .HasColumnType("int");

                    b.Property<DateTime>("QuizDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeResultID");

                    b.HasIndex("FK_PersonID");

                    b.HasIndex("FK_QuizID");

                    b.ToTable("EmployeeResults");
                });

            modelBuilder.Entity("GoApptechBackend.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("EmpPoints")
                        .HasColumnType("int");

                    b.Property<int>("FK_EmployeeRankID")
                        .HasColumnType("int");

                    b.Property<int>("FK_PingPongRankID")
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("YearsInPratice")
                        .HasColumnType("float");

                    b.HasKey("PersonID");

                    b.HasIndex("FK_EmployeeRankID");

                    b.HasIndex("FK_PingPongRankID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongRank", b =>
                {
                    b.Property<int>("PingPongRankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PingPongRankID"));

                    b.Property<string>("RankTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequiredWinnings")
                        .HasColumnType("int");

                    b.HasKey("PingPongRankID");

                    b.ToTable("PingPongRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongResults", b =>
                {
                    b.Property<int>("PingPongResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PingPongResultID"));

                    b.Property<int>("FK_PersonID")
                        .HasColumnType("int");

                    b.Property<int>("FK_PersonIDPoints")
                        .HasColumnType("int");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OpponentPoints")
                        .HasColumnType("int");

                    b.Property<bool>("WonMatch")
                        .HasColumnType("bit");

                    b.HasKey("PingPongResultID");

                    b.HasIndex("FK_PersonID");

                    b.ToTable("PingPongResults");
                });

            modelBuilder.Entity("GoApptechBackend.Models.Quiz", b =>
                {
                    b.Property<int>("QuizID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizID"));

                    b.Property<string>("AltOne")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("AltThree")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("AltTwo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("QuizHeading")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("QuizID");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("GoApptechBackend.Models.EmployeeResult", b =>
                {
                    b.HasOne("GoApptechBackend.Models.Person", "Persons")
                        .WithMany()
                        .HasForeignKey("FK_PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoApptechBackend.Models.Quiz", "Quizzes")
                        .WithMany()
                        .HasForeignKey("FK_QuizID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persons");

                    b.Navigation("Quizzes");
                });

            modelBuilder.Entity("GoApptechBackend.Models.Person", b =>
                {
                    b.HasOne("GoApptechBackend.Models.EmployeeRank", "EmployeeRanks")
                        .WithMany()
                        .HasForeignKey("FK_EmployeeRankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoApptechBackend.Models.PingPongRank", "PingPongRanks")
                        .WithMany()
                        .HasForeignKey("FK_PingPongRankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeRanks");

                    b.Navigation("PingPongRanks");
                });

            modelBuilder.Entity("GoApptechBackend.Models.PingPongResults", b =>
                {
                    b.HasOne("GoApptechBackend.Models.Person", "Persons")
                        .WithMany()
                        .HasForeignKey("FK_PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}