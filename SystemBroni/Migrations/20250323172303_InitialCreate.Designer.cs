﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SystemBroni.Models;

#nullable disable

namespace SystemBroni.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250323172303_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SystemBroni.Models.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("SystemBroni.Models.TableBooking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("TableBookings");
                });

            modelBuilder.Entity("SystemBroni.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SystemBroni.Models.VipRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("VipRooms");
                });

            modelBuilder.Entity("SystemBroni.Models.VipRoomBooking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VipRoomId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.HasIndex("VipRoomId");

                    b.ToTable("VipRoomBookings");
                });

            modelBuilder.Entity("SystemBroni.Models.TableBooking", b =>
                {
                    b.HasOne("SystemBroni.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("SystemBroni.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemBroni.Models.User", null)
                        .WithMany("TableBookings")
                        .HasForeignKey("UserId1");

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SystemBroni.Models.VipRoomBooking", b =>
                {
                    b.HasOne("SystemBroni.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemBroni.Models.User", null)
                        .WithMany("VipRoomBookings")
                        .HasForeignKey("UserId1");

                    b.HasOne("SystemBroni.Models.VipRoom", "VipRoom")
                        .WithMany()
                        .HasForeignKey("VipRoomId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VipRoom");
                });

            modelBuilder.Entity("SystemBroni.Models.User", b =>
                {
                    b.Navigation("TableBookings");

                    b.Navigation("VipRoomBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
