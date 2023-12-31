﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dunger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliverPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeliverId = table.Column<int>(type: "integer", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LocationUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilialPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilialId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilialPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilialPhotos_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    TelegramId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.TelegramId);
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    VehicleColor = table.Column<string>(type: "text", nullable: false),
                    VehicleNumber = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
                    JoinedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivers_DeliverPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "DeliverPhotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlockedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    UserTelegramId = table.Column<long>(type: "bigint", nullable: true),
                    IsUnBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedUsers_Users_UserTelegramId",
                        column: x => x.UserTelegramId,
                        principalTable: "Users",
                        principalColumn: "TelegramId");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_TelegramId",
                        column: x => x.TelegramId,
                        principalTable: "Users",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LocationUrl = table.Column<string>(type: "text", nullable: true),
                    DeliverId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    FilialId = table.Column<int>(type: "integer", nullable: false),
                    Reyting = table.Column<double>(type: "double precision", nullable: true),
                    TotalSumms = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Delivers_DeliverId",
                        column: x => x.DeliverId,
                        principalTable: "Delivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_TelegramId",
                        column: x => x.TelegramId,
                        principalTable: "Users",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Summs = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    FilialId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Menus_MenuPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "MenuPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdersMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersMenus_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreatedTime", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3259), "uz" },
                    { 2, new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3261), "en" },
                    { 3, new DateTime(2023, 9, 2, 2, 52, 48, 974, DateTimeKind.Utc).AddTicks(3263), "ru" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUsers_UserTelegramId",
                table: "BlockedUsers",
                column: "UserTelegramId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_Phone",
                table: "Delivers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_PhotoId",
                table: "Delivers",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_VehicleId",
                table: "Delivers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_VehicleNumber",
                table: "Delivers",
                column: "VehicleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TelegramId",
                table: "Feedbacks",
                column: "TelegramId");

            migrationBuilder.CreateIndex(
                name: "IX_FilialPhotos_FilialId",
                table: "FilialPhotos",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Filials_Name",
                table: "Filials",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuPhotos_MenuId",
                table: "MenuPhotos",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FilialId",
                table: "Menus",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PhotoId",
                table: "Menus",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliverId",
                table: "Orders",
                column: "DeliverId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FilialId",
                table: "Orders",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LanguageId",
                table: "Orders",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TelegramId",
                table: "Orders",
                column: "TelegramId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersMenus_MenuId",
                table: "OrdersMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersMenus_OrderId",
                table: "OrdersMenus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Name",
                table: "Vehicles",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPhotos_Menus_MenuId",
                table: "MenuPhotos",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Filials_FilialId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPhotos_Menus_MenuId",
                table: "MenuPhotos");

            migrationBuilder.DropTable(
                name: "BlockedUsers");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FilialPhotos");

            migrationBuilder.DropTable(
                name: "OrdersMenus");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Delivers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DeliverPhotos");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Filials");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "MenuPhotos");
        }
    }
}
