﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webshop.Persistence;

namespace Webshop.Persistence.Migrations
{
    [DbContext(typeof(WebshopContext))]
    partial class WebshopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Webshop.Domain.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("AccountId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnName("AddressId");

                    b.Property<int>("AccountId");

                    b.Property<string>("City");

                    b.Property<int?>("CountryId");

                    b.Property<string>("PostNumber");

                    b.Property<string>("Street");

                    b.HasKey("AddressId");

                    b.HasIndex("CountryId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnName("CartId");

                    b.Property<int>("AccountId");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryId");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CountryId");

                    b.Property<short>("IsShippable");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("ShippingCost");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartFk");

                    b.Property<int>("ItemFk");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartFk");

                    b.HasIndex("ItemFk");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderItemId");

                    b.Property<int>("ItemFk");

                    b.Property<int>("OrderFk");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemFk");

                    b.HasIndex("OrderFk");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InvoiceId");

                    b.Property<decimal>("Amount");

                    b.Property<string>("Currency");

                    b.Property<string>("ExInvoiceId");

                    b.Property<short>("IsPaid");

                    b.Property<int>("OrderId");

                    b.HasKey("InvoiceId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ItemId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Stock");

                    b.Property<decimal>("Volume");

                    b.Property<decimal>("Weight");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderId");

                    b.Property<int>("AccountId");

                    b.Property<short>("IsFinalized");

                    b.Property<short>("IsShipped");

                    b.Property<decimal>("ShippingCost");

                    b.Property<decimal>("TotalCost");

                    b.Property<string>("TrackingId");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Address", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Account", "Account")
                        .WithOne("Address")
                        .HasForeignKey("Webshop.Domain.Entities.Address", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webshop.Domain.Entities.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Cart", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Account", "Account")
                        .WithOne("Cart")
                        .HasForeignKey("Webshop.Domain.Entities.Cart", "CartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.CartItem", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webshop.Domain.Entities.Item", "Item")
                        .WithMany("CartItems")
                        .HasForeignKey("ItemFk")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.OrderItem", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webshop.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderFk")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Order", "Order")
                        .WithOne("Invoice")
                        .HasForeignKey("Webshop.Domain.Entities.Invoice", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Item", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Order", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
