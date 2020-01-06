﻿// <auto-generated />
using System;
using FutureOfMedia.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FutureOfMedia.Infra.Migrations
{
    [DbContext(typeof(FutureOfMediaContext))]
    [Migration("20190513094513_FirstBase")]
    partial class FirstBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FutureOfMedia.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedIn");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("EmailVisible");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("PhoneVisible");

                    b.Property<string>("ProfilePictureUrl")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedIn");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FutureOfMedia.Domain.Entities.User", b =>
                {
                    b.OwnsOne("FutureOfMedia.Domain.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .HasColumnName("FirstName")
                                .HasMaxLength(200);

                            b1.Property<string>("LastName")
                                .HasColumnName("LastName")
                                .HasMaxLength(200);

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.HasOne("FutureOfMedia.Domain.Entities.User")
                                .WithOne("Name")
                                .HasForeignKey("FutureOfMedia.Domain.ValueObjects.Name", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
