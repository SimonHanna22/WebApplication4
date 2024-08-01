using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class blogtablee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BlogDate",
                table: "blogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "ImagesPath",
                table: "blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesPath",
                table: "blogs");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BlogDate",
                table: "blogs",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
