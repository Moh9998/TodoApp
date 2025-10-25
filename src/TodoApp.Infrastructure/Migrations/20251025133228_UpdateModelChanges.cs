using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("935fe1b5-ff74-4e12-bfae-6fff08698f57"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dd4f65b0-ec58-4fad-9638-e7dd640a7ab5"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1d1f9960-6b9a-47cc-86a4-626f5653a373"), null, "User", "USER" },
                    { new Guid("81d0a66a-7054-4bc7-9033-fe6272fb3d5b"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("935fe1b5-ff74-4e12-bfae-6fff08698f57"), null, "User", "USER" },
                    { new Guid("dd4f65b0-ec58-4fad-9638-e7dd640a7ab5"), null, "Admin", "ADMIN" }
                });
        }
    }
}
