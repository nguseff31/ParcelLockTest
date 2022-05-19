using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ParcelLockTest.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParcelLock",
                columns: table => new
                {
                    Number = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelLock", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Products = table.Column<List<string>>(type: "text[]", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ParcelLockNumber = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_ParcelLock_ParcelLockNumber",
                        column: x => x.ParcelLockNumber,
                        principalTable: "ParcelLock",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParcelLock",
                columns: new[] { "Number", "Address", "IsActive" },
                values: new object[,]
                {
                    { "1111-111", "Москва, улица Маршала Новикова, 14 к2", true },
                    { "2222-222", "Москва, Маршала Бирюзова, 32", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ParcelLockNumber",
                table: "Order",
                column: "ParcelLockNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ParcelLock");
        }
    }
}
