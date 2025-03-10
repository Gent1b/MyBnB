using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.API.Migrations
{
    /// <inheritdoc />
    public partial class StaysTableWithAppUserForeignKeyv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Stays",
                columns: table => new
                {
                    StayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stays", x => x.StayId);
                    table.ForeignKey(
                        name: "FK_Stays_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02b0a70b-39c6-41b6-a5fa-b8afd05a334b", "1", "Admin", "Admin" },
                    { "0a538dc7-86c4-454b-85e2-6a35078a210c", "2", "User", "User" },
                    { "793eb13c-759b-4754-89f4-86e0dd519a03", "3", "HR", "HR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stays_ApplicationUserId",
                table: "Stays",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stays");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02b0a70b-39c6-41b6-a5fa-b8afd05a334b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a538dc7-86c4-454b-85e2-6a35078a210c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "793eb13c-759b-4754-89f4-86e0dd519a03");

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
    }
}
