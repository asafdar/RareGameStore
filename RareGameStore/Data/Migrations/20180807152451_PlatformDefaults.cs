using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RareGameStore.Data.Migrations
{
    public partial class PlatformDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Platform",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Platform",
                nullable: true,
                defaultValueSql: "GetDate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "Platform",
                nullable: true,
                defaultValueSql: "GetDate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Platform");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "Platform");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Platform",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
