using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.API.Migrations
{
    /// <inheritdoc />
    public partial class StaysTableWithAppUserForeignKeyv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "160e2310-64e7-4993-bf00-6b90d72dcca1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528882ae-428a-4b13-9070-2bd860bea14d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8419b72a-3123-42b4-90a6-24e091d3f23c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26575cda-1536-4f26-96d3-d2da2e146ce2", "2", "User", "User" },
                    { "d9f7d3e2-9a6e-4213-9e85-ec616c597c83", "1", "Admin", "Admin" },
                    { "f08751ab-2fed-4c8e-a276-53e339012487", "3", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26575cda-1536-4f26-96d3-d2da2e146ce2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9f7d3e2-9a6e-4213-9e85-ec616c597c83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f08751ab-2fed-4c8e-a276-53e339012487");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "160e2310-64e7-4993-bf00-6b90d72dcca1", "3", "HR", "HR" },
                    { "528882ae-428a-4b13-9070-2bd860bea14d", "2", "User", "User" },
                    { "8419b72a-3123-42b4-90a6-24e091d3f23c", "1", "Admin", "Admin" }
                });
        }
    }
}
