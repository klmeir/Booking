﻿// <auto-generated />
using System;
using Booking.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240709234554_EntityRoomAddColumnMaxGuests")]
    partial class EntityRoomAddColumnMaxGuests
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Booking.Domain.Entities.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Commission")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CheckInDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("CheckOutDate")
                        .HasColumnType("date");

                    b.Property<string>("EmergencyContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuestCount")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<int>("RoomType")
                        .HasColumnType("int");

                    b.Property<decimal>("Taxes")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Guest", b =>
                {
                    b.HasOne("Booking.Domain.Entities.Reservation", null)
                        .WithMany("Guests")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Booking.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("Booking.Domain.Entities.Hotel", null)
                        .WithMany("Reservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Booking.Domain.Entities.Room", null)
                        .WithMany("Reservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Booking.Domain.Entities.Room", b =>
                {
                    b.HasOne("Booking.Domain.Entities.Hotel", null)
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Booking.Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Reservation", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Room", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
