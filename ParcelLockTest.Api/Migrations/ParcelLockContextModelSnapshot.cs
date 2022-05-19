﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParcelLockTest;
using ParcelLockTest.Api.Data;

#nullable disable

namespace ParcelLockTest.Api.Migrations
{
    [DbContext(typeof(ParcelLockContext))]
    partial class ParcelLockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ParcelLockTest.Order.OrderDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParcelLockNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<List<string>>("Products")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParcelLockNumber");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ParcelLockTest.ParcelLock.ParcelLockDto", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Number");

                    b.ToTable("ParcelLock");

                    b.HasData(
                        new
                        {
                            Number = "1111-111",
                            Address = "Москва, улица Маршала Новикова, 14 к2",
                            IsActive = true
                        },
                        new
                        {
                            Number = "2222-222",
                            Address = "Москва, Маршала Бирюзова, 32",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("ParcelLockTest.Order.OrderDto", b =>
                {
                    b.HasOne("ParcelLockTest.ParcelLock.ParcelLockDto", "ParcelLock")
                        .WithMany()
                        .HasForeignKey("ParcelLockNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParcelLock");
                });
#pragma warning restore 612, 618
        }
    }
}
