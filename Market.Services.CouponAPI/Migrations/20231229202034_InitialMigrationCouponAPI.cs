using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationCouponAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CouponDisccountAmount = table.Column<double>(type: "float", nullable: false),
                    CouponMinAmmount = table.Column<double>(type: "float", nullable: false),
                    CouponStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CouponEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Coupon", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "CouponId", "CouponCode", "CouponDisccountAmount", "CouponEndDate", "CouponMinAmmount", "CouponStartDate", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, "50OFF", 50.0, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200.0, new DateTime(2023, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 29, 15, 20, 33, 908, DateTimeKind.Local).AddTicks(5405), "SYSTEM", "Special Disccount for New Year", 0, "50% OFF - New Year Disccount", new DateTime(2023, 12, 29, 15, 20, 33, 908, DateTimeKind.Local).AddTicks(5419), "SYSTEM" },
                    { 2L, "15OFF", 15.0, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.0, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 29, 15, 20, 33, 908, DateTimeKind.Local).AddTicks(5459), "SYSTEM", "Special Disccount for Carnival", 0, "15% OFF - Carnival Disccount", new DateTime(2023, 12, 29, 15, 20, 33, 908, DateTimeKind.Local).AddTicks(5459), "SYSTEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");
        }
    }
}
