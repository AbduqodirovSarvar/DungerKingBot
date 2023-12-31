﻿// <auto-generated />
using System;
using Dunger.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dunger.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dunger.Domain.Entities.BlockedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsUnBlocked")
                        .HasColumnType("boolean");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserTelegramId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserTelegramId");

                    b.ToTable("BlockedUsers");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Deliver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<DateTime>("JoinedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<string>("VehicleColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.HasIndex("VehicleId");

                    b.HasIndex("VehicleNumber")
                        .IsUnique();

                    b.ToTable("Delivers");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.DeliverPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DeliverId")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DeliverPhotos");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TelegramId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Filial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LocationUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Filials");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.FilialPhotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FilialId")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FilialId");

                    b.ToTable("FilialPhotos");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3259),
                            Name = "uz"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3261),
                            Name = "en"
                        },
                        new
                        {
                            Id = 3,
                            CreatedTime = new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3263),
                            Name = "ru"
                        });
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("FilialId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("FilialId");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.MenuPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuPhotos");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ClosedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DeliverId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeliveredTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FilialId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("LocationUrl")
                        .HasColumnType("text");

                    b.Property<double?>("Reyting")
                        .HasColumnType("double precision");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalSumms")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("DeliverId");

                    b.HasIndex("FilialId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("TelegramId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.OrderMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersMenus");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Summs")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.User", b =>
                {
                    b.Property<long>("TelegramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TelegramId"));

                    b.Property<DateTime>("CreatedTate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("TelegramId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.BlockedUser", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserTelegramId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Deliver", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.DeliverPhoto", "DeliverPhoto")
                        .WithOne("Deliver")
                        .HasForeignKey("Dunger.Domain.Entities.Deliver", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dunger.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("Delivers")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliverPhoto");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("TelegramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.FilialPhotos", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Filial", "Filial")
                        .WithMany("Photos")
                        .HasForeignKey("FilialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filial");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Menu", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Filial", null)
                        .WithMany("Menus")
                        .HasForeignKey("FilialId");

                    b.HasOne("Dunger.Domain.Entities.MenuPhoto", "Photo")
                        .WithOne()
                        .HasForeignKey("Dunger.Domain.Entities.Menu", "PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.MenuPhoto", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Order", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Deliver", "Deliver")
                        .WithMany("Orders")
                        .HasForeignKey("DeliverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dunger.Domain.Entities.Filial", "Filial")
                        .WithMany("Orders")
                        .HasForeignKey("FilialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dunger.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dunger.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("TelegramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliver");

                    b.Navigation("Filial");

                    b.Navigation("Language");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.OrderMenu", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dunger.Domain.Entities.Order", "Order")
                        .WithMany("Menus")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Payment", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.User", b =>
                {
                    b.HasOne("Dunger.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Deliver", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.DeliverPhoto", b =>
                {
                    b.Navigation("Deliver");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Filial", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Orders");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Order", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dunger.Domain.Entities.Vehicle", b =>
                {
                    b.Navigation("Delivers");
                });
#pragma warning restore 612, 618
        }
    }
}
