using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Repository.Dtat.Migrations
{
    public partial class Document_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "path",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "path",
                table: "Documents");
        }
    }
}
