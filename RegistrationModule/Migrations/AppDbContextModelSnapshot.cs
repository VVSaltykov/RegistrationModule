﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationModule;

#nullable disable

namespace RegistrationModule.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RegistrationModule.Models.File", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Path");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("RegistrationModule.Models.Hash", b =>
                {
                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("HashSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Password");

                    b.ToTable("Hashes");
                });

            modelBuilder.Entity("RegistrationModule.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SaltPassword")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SaltPassword");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RegistrationModule.Models.User", b =>
                {
                    b.HasOne("RegistrationModule.Models.Hash", "Salt")
                        .WithMany()
                        .HasForeignKey("SaltPassword");

                    b.Navigation("Salt");
                });
#pragma warning restore 612, 618
        }
    }
}
