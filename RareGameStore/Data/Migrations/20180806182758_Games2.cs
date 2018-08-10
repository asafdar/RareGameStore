using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RareGameStore.Data.Migrations
{
    public partial class Games2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "Games");
        }
    }
}
