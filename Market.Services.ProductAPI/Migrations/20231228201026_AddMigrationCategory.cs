using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2069), "SYSTEM", "", 0, "Appetizer", new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2080), "SYSTEM" },
                    { 2L, new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2111), "SYSTEM", "", 0, "Dessert", new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2112), "SYSTEM" },
                    { 3L, new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2121), "SYSTEM", "", 0, "Entree", new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2122), "SYSTEM" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2136), new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2137) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2155), new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2155) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2166), new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2166) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2176), new DateTime(2023, 12, 28, 15, 10, 26, 71, DateTimeKind.Local).AddTicks(2177) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8310), new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8322) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8349), new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8359), new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8359) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8367), new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8367) });
        }
    }
}
