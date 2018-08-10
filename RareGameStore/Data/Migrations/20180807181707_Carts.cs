using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RareGameStore.Data.Migrations
{
    public partial class Carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlatformName",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameCarts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCarts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GameCartProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    GameCartID = table.Column<int>(nullable: false),
                    GameID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCartProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameCartProducts_GameCarts_GameCartID",
                        column: x => x.GameCartID,
                        principalTable: "GameCarts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCartProducts_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCartProducts_GameCartID",
                table: "GameCartProducts",
                column: "GameCartID");

            migrationBuilder.CreateIndex(
                name: "IX_GameCartProducts_GameID",
                table: "GameCartProducts",
                column: "GameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCartProducts");

            migrationBuilder.DropTable(
                name: "GameCarts");

            migrationBuilder.DropColumn(
                name: "PlatformName",
                table: "Games");
        }
    }
}
