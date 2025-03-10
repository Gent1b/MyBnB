using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.API.Migrations
{
    /// <inheritdoc />
    public partial class CustomApplicationUserPropertiesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d74abf0-50e6-4710-87dd-a069062a528c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0da90dfe-95f9-4e27-ad84-4e1a1c2c2d5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "800b09e1-97b7-4566-9544-f2e78768c2d1");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d74abf0-50e6-4710-87dd-a069062a528c", "1", "Admin", "Admin" },
                    { "0da90dfe-95f9-4e27-ad84-4e1a1c2c2d5b", "3", "HR", "HR" },
                    { "800b09e1-97b7-4566-9544-f2e78768c2d1", "2", "User", "User" }
                });
        }
    }
}
