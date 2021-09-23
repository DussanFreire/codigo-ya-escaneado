using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAlbergue.Migrations
{
    public partial class previousnext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "next",
                table: "pets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "previous",
                table: "pets",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "notice",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "next",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "previous",
                table: "pets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "notice",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
