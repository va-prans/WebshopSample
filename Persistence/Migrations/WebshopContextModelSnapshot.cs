﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Webshop.Domain.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasMaxLength(20);

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("AccountId");

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
                    b.Property<int>("CartId");

                    b.Property<int>("AccountId");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ImageId");

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.HasIndex("ImageId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CountryId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsShippable");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("ShippingCost");

                    b.Property<double>("Tax");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("ItemId");

                    b.HasKey("ImageId");

                    b.HasIndex("ItemId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartFk");

                    b.Property<int?>("CartId");

                    b.Property<int>("ItemFk");

                    b.Property<int?>("ItemId");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ItemId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemFk");

                    b.Property<int?>("ItemId");

                    b.Property<int>("OrderFk");

                    b.Property<int?>("OrderId");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<string>("Currency");

                    b.Property<string>("ExInvoiceId");

                    b.Property<bool>("IsPaid");

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<bool>("IsFinalized");

                    b.Property<bool>("IsShipped");

                    b.Property<decimal>("ShippingCost");

                    b.Property<decimal>("Tax");

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

            modelBuilder.Entity("Webshop.Domain.Entities.Category", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Image", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Item")
                        .WithMany("Images")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.CartItem", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("Webshop.Domain.Entities.Item", "Item")
                        .WithMany("CartItems")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.IntermediaryTables.OrderItem", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId");

                    b.HasOne("Webshop.Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
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
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Webshop.Domain.Entities.Order", b =>
                {
                    b.HasOne("Webshop.Domain.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
