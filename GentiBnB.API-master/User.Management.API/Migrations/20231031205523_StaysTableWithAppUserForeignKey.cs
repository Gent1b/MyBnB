using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.API.Migrations
{
    /// <inheritdoc />
    public partial class StaysTableWithAppUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "802dde7f-5711-4487-b4b2-0b393b7058ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5720a7e-9085-47dd-8277-e3a052d4236a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f18ecded-af94-4520-bc5c-47366a2344a8");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "802dde7f-5711-4487-b4b2-0b393b7058ce", "1", "Admin", "Admin" },
                    { "b5720a7e-9085-47dd-8277-e3a052d4236a", "3", "HR", "HR" },
                    { "f18ecded-af94-4520-bc5c-47366a2344a8", "2", "User", "User" }
                });
        }
    }
}
