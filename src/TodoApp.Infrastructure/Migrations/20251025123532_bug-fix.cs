using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bugfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("02ed7ee0-bc7e-48e9-8c2d-7299ccea6f31"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1ec2bc7-2545-49bf-aaf2-0dd178cb5664"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2f3c21c1-7156-44c0-af03-77bc137517c9"), null, "User", "USER" },
                    { new Guid("69de2c3e-9118-4fc1-b463-37a071838d54"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f3c21c1-7156-44c0-af03-77bc137517c9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("69de2c3e-9118-4fc1-b463-37a071838d54"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("02ed7ee0-bc7e-48e9-8c2d-7299ccea6f31"), null, "User", "USER" },
                    { new Guid("a1ec2bc7-2545-49bf-aaf2-0dd178cb5664"), null, "Admin", "ADMIN" }
                });
        }
    }
}
