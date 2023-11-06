﻿// <auto-generated />
using System;
using LIDOM.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LIDOM.Migrations
{
    [DbContext(typeof(LidomDBContext))]
    partial class LidomDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LIDOM.Models.Calendar", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Home")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_FirstTeam")
                        .HasColumnType("int");

                    b.Property<int>("Id_SecondTeam")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id_FirstTeam");

                    b.HasIndex("Id_SecondTeam");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("LIDOM.Models.LidomTeam", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Home")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LidomTeams");
                });

            modelBuilder.Entity("LIDOM.Models.Stadistic", b =>
                {
                    b.Property<int>("Id_Calendar")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("Id_Team")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id_Calendar", "Id_Team");

                    b.HasIndex("Id_Team");

                    b.ToTable("Stadistics");
                });

            modelBuilder.Entity("LIDOM.Models.Calendar", b =>
                {
                    b.HasOne("LIDOM.Models.LidomTeam", "LidomFirstTeam")
                        .WithMany()
                        .HasForeignKey("Id_FirstTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIDOM.Models.LidomTeam", "LidomSecondTeam")
                        .WithMany()
                        .HasForeignKey("Id_SecondTeam")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LidomFirstTeam");

                    b.Navigation("LidomSecondTeam");
                });

            modelBuilder.Entity("LIDOM.Models.Stadistic", b =>
                {
                    b.HasOne("LIDOM.Models.Calendar", "Calendar")
                        .WithMany()
                        .HasForeignKey("Id_Calendar")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIDOM.Models.LidomTeam", "LidomTeam")
                        .WithMany()
                        .HasForeignKey("Id_Team")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("LidomTeam");
                });
#pragma warning restore 612, 618
        }
    }
}