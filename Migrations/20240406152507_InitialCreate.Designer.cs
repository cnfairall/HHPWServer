﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HHPWServer.Migrations
{
    [DbContext(typeof(HHPWServerDbContext))]
    [Migration("20240406152507_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HHPWServer.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemName = "Loaded Nachos",
                            ItemPrice = 10.00m
                        },
                        new
                        {
                            Id = 2,
                            ItemName = "Buffalo Wings",
                            ItemPrice = 12.00m
                        },
                        new
                        {
                            Id = 3,
                            ItemName = "Teriyaki Wings",
                            ItemPrice = 12.00m
                        },
                        new
                        {
                            Id = 4,
                            ItemName = "Pepperoni Pizza",
                            ItemPrice = 15.00m
                        },
                        new
                        {
                            Id = 5,
                            ItemName = "Bacon Ranch Pizza",
                            ItemPrice = 17.00m
                        });
                });

            modelBuilder.Entity("HHPWServer.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CustEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustEmail = "isotope47@msn.net",
                            CustName = "Bill Blasky",
                            IsClosed = true,
                            OrderTypeId = 1,
                            PhoneNum = "561-893-2219"
                        },
                        new
                        {
                            Id = 2,
                            CustEmail = "popovili@yahoo.com",
                            CustName = "Sarah Nitro",
                            IsClosed = false,
                            OrderTypeId = 2,
                            PhoneNum = "561-338-9466"
                        });
                });

            modelBuilder.Entity("HHPWServer.Models.OrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "in person"
                        },
                        new
                        {
                            Id = 2,
                            Name = "phone"
                        });
                });

            modelBuilder.Entity("HHPWServer.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TipAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderDate = new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OrderId = 1,
                            PaymentTypeId = 1,
                            TipAmount = 10.00m
                        });
                });

            modelBuilder.Entity("HHPWServer.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "cash"
                        },
                        new
                        {
                            Id = 2,
                            Name = "check"
                        },
                        new
                        {
                            Id = 3,
                            Name = "credit"
                        },
                        new
                        {
                            Id = 4,
                            Name = "debit"
                        },
                        new
                        {
                            Id = 5,
                            Name = "mobile pay"
                        });
                });

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.Property<int>("ItemsId")
                        .HasColumnType("integer");

                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.HasKey("ItemsId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("ItemOrder");
                });

            modelBuilder.Entity("HHPWServer.Models.Payment", b =>
                {
                    b.HasOne("HHPWServer.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.HasOne("HHPWServer.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HHPWServer.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
