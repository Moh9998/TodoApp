using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1d1f9960-6b9a-47cc-86a4-626f5653a373"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("81d0a66a-7054-4bc7-9033-fe6272fb3d5b"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3d319abd-bc7e-46d1-bcd6-5a0c61e582b0"), null, "User", "USER" },
                    { new Guid("98e30fe3-5f7c-4c49-8faf-34d690097bda"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3d319abd-bc7e-46d1-bcd6-5a0c61e582b0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98e30fe3-5f7c-4c49-8faf-34d690097bda"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1d1f9960-6b9a-47cc-86a4-626f5653a373"), null, "User", "USER" },
                    { new Guid("81d0a66a-7054-4bc7-9033-fe6272fb3d5b"), null, "Admin", "ADMIN" }
                });
        }
    }
}
