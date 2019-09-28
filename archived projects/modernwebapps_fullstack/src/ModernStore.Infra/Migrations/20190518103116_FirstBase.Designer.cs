﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModernStore.Infra.Contexts;

namespace ModernStore.Infra.Migrations
{
    [DbContext(typeof(ModernStoreDataContext))]
    [Migration("20190518103116_FirstBase")]
    partial class FirstBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedIn");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedIn");

                    b.Property<int>("UserId");

                    b.HasKey("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedIn");

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("DeliveryFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<int>("Status");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedIn");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedIn");

                    b.Property<int>("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedIn");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedIn");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("QuantityOnHand");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedIn");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedIn");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedIn");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Number")
                                .HasColumnName("DocumentNumber")
                                .HasMaxLength(20);

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Document")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Document", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("EmailAddress")
                                .HasColumnName("EmailAddress")
                                .HasMaxLength(60);

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Email")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Email", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .HasColumnName("FirstName")
                                .HasMaxLength(60);

                            b1.Property<string>("LastName")
                                .HasColumnName("LastName")
                                .HasMaxLength(60);

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Name")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Name", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Customer", "Customer")
                        .WithOne()
                        .HasForeignKey("ModernStore.Domain.Entities.Order", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ModernStore.Domain.Entities.Product", "Product")
                        .WithMany("OrderItemsFromDB")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
