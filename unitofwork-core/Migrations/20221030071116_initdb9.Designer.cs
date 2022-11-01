﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using unitofwork_core.Data;

#nullable disable

namespace unitofwork_core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221030071116_initdb9")]
    partial class initdb9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("unitofwork_core.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.HistoryOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ToStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("HistoryOrder", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("NumberPackage")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,0)");

                    b.Property<Guid?>("ShipperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("StartLatitude")
                        .HasColumnType("float");

                    b.Property<double>("StartLongitude")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ShipperId");

                    b.HasIndex("ShopId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.OrderRouting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoutingIndex")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ToLatitude")
                        .HasColumnType("float");

                    b.Property<double>("ToLongitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderRouting", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderRoutingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderRoutingId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Shipper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<double>("DestinationLatitude")
                        .HasColumnType("float");

                    b.Property<double>("DestinationLongitude")
                        .HasColumnType("float");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("HomeLatitude")
                        .HasColumnType("float");

                    b.Property<double>("HomeLongitude")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Shipper", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Shop", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BalanceWallet")
                        .HasColumnType("decimal(10,0)");

                    b.Property<decimal>("CoinExchange")
                        .HasColumnType("decimal(10,0)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(10,0)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ShipperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShipperId");

                    b.HasIndex("ShopId");

                    b.ToTable("Wallet", (string)null);
                });

            modelBuilder.Entity("unitofwork_core.Entities.HistoryOrder", b =>
                {
                    b.HasOne("unitofwork_core.Entities.Order", "Order")
                        .WithMany("HistoryOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Order", b =>
                {
                    b.HasOne("unitofwork_core.Entities.Shipper", "Shipper")
                        .WithMany("Orders")
                        .HasForeignKey("ShipperId");

                    b.HasOne("unitofwork_core.Entities.Shop", "Shop")
                        .WithMany("Orders")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipper");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("unitofwork_core.Entities.OrderRouting", b =>
                {
                    b.HasOne("unitofwork_core.Entities.Order", "Order")
                        .WithMany("OrderRoutings")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Product", b =>
                {
                    b.HasOne("unitofwork_core.Entities.OrderRouting", "OrderRouting")
                        .WithMany("Products")
                        .HasForeignKey("OrderRoutingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderRouting");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Transaction", b =>
                {
                    b.HasOne("unitofwork_core.Entities.Order", "Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId");

                    b.HasOne("unitofwork_core.Entities.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Wallet", b =>
                {
                    b.HasOne("unitofwork_core.Entities.Shipper", "Shipper")
                        .WithMany("Wallets")
                        .HasForeignKey("ShipperId");

                    b.HasOne("unitofwork_core.Entities.Shop", "Shop")
                        .WithMany("Wallets")
                        .HasForeignKey("ShopId");

                    b.Navigation("Shipper");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Order", b =>
                {
                    b.Navigation("HistoryOrders");

                    b.Navigation("OrderRoutings");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("unitofwork_core.Entities.OrderRouting", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Shipper", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Shop", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("unitofwork_core.Entities.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
