using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RareGameStore.Data.Migrations
{
    public partial class CartUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "GameCarts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameCartID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameCarts_ApplicationUserID",
                table: "GameCarts",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCarts_AspNetUsers_ApplicationUserID",
                table: "GameCarts",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCarts_AspNetUsers_ApplicationUserID",
                table: "GameCarts");

            migrationBuilder.DropIndex(
                name: "IX_GameCarts_ApplicationUserID",
                table: "GameCarts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "GameCarts");

            migrationBuilder.DropColumn(
                name: "GameCartID",
                table: "AspNetUsers");
        }
    }
}
