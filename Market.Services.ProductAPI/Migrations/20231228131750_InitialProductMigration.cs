using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, "Appetizer", new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8310), "SYSTEM", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://placehold.co/603x403", 0, "Pisco Sour", 25.0, new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8322), "SYSTEM" },
                    { 2L, "Appetizer", new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8349), "SYSTEM", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://placehold.co/602x402", 0, "Anticuchos", 20.989999999999998, new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8350), "SYSTEM" },
                    { 3L, "Dessert", new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8359), "SYSTEM", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://placehold.co/601x401", 0, "Suspiro a la Limeña", 12.99, new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8359), "SYSTEM" },
                    { 4L, "Entree", new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8367), "SYSTEM", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://placehold.co/600x400", 0, "Causa al Olivo", 35.0, new DateTime(2023, 12, 28, 8, 17, 50, 359, DateTimeKind.Local).AddTicks(8367), "SYSTEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
