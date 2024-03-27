﻿// <auto-generated />
using System;
using CarRental.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240303143654_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("CarRental.Domain.Entities.Circulations.Circulation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Color")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Color2")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpeditionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InsuranceID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SomatonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Circulations", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Common.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Prices", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Insurances.Insurance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpeditionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Insurances", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Persons.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Reservations.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SupplementsId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SupplementsId");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Somatons.Somaton", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpeditionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Somatons", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Supplements.Supplement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PriceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.ToTable("Supplements", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CirculationId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Color")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Color2")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PriceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CirculationId");

                    b.HasIndex("PriceId");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Persons.Client", b =>
                {
                    b.HasBaseType("CarRental.Domain.Entities.Persons.Person");

                    b.Property<Guid>("Reservation")
                        .HasColumnType("TEXT");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Persons.Users", b =>
                {
                    b.HasBaseType("CarRental.Domain.Entities.Persons.Person");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Car", b =>
                {
                    b.HasBaseType("CarRental.Domain.Entities.Vehicles.Vehicle");

                    b.Property<bool>("HasAirConditioning")
                        .HasColumnType("INTEGER");

                    b.ToTable("Cars", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Motorcycle", b =>
                {
                    b.HasBaseType("CarRental.Domain.Entities.Vehicles.Vehicle");

                    b.ToTable("Motorcycles", (string)null);
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Reservations.Reservation", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Supplements.Supplement", "Supplements")
                        .WithMany()
                        .HasForeignKey("SupplementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplements");
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Supplements.Supplement", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Common.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Vehicle", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Circulations.Circulation", "Circulation")
                        .WithMany()
                        .HasForeignKey("CirculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Domain.Entities.Common.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Circulation");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Persons.Client", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Persons.Person", null)
                        .WithOne()
                        .HasForeignKey("CarRental.Domain.Entities.Persons.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Persons.Users", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Persons.Person", null)
                        .WithOne()
                        .HasForeignKey("CarRental.Domain.Entities.Persons.Users", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Car", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Vehicles.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarRental.Domain.Entities.Vehicles.Car", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental.Domain.Entities.Vehicles.Motorcycle", b =>
                {
                    b.HasOne("CarRental.Domain.Entities.Vehicles.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarRental.Domain.Entities.Vehicles.Motorcycle", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
