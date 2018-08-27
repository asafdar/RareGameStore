using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RareGameStore.Data.Migrations
{
    public partial class checkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateTable(
                name: "GameOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameOrders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GameOrderProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    GameOrderID = table.Column<Guid>(nullable: false),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameOrderProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameOrderProducts_GameOrders_GameOrderID",
                        column: x => x.GameOrderID,
                        principalTable: "GameOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameOrderProducts_GameOrderID",
                table: "GameOrderProducts",
                column: "GameOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameOrderProducts");

            migrationBuilder.DropTable(
                name: "GameOrders");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
