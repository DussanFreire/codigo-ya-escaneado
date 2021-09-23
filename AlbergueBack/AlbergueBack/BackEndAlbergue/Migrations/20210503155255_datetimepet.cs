using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAlbergue.Migrations
{
    public partial class datetimepet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "age",
                table: "pets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "pets",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "pets",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(55) CHARACTER SET utf8mb4",
                oldMaxLength: 55,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "age",
                table: "pets",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "pets",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "pets",
                type: "varchar(55) CHARACTER SET utf8mb4",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
