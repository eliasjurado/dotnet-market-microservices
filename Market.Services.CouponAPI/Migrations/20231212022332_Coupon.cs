using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class Coupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponDisccountAmount = table.Column<double>(type: "float", nullable: false),
                    CouponMinAmmount = table.Column<double>(type: "float", nullable: false),
                    CouponStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CouponEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<string>(type: "nvarchar(1)", nullable: false),
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
                columns: new[] { "CouponId", "CouponCode", "CouponDisccountAmount", "CouponEndDate", "CouponMinAmmount", "CouponName", "CouponStartDate", "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "15OFF", 15.0, new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), 200.0, "15% OFF - Christmas Disccount", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 11, 21, 23, 31, 699, DateTimeKind.Local).AddTicks(4504), "SYSTEM", "N", new DateTime(2023, 12, 11, 21, 23, 31, 699, DateTimeKind.Local).AddTicks(4517), "SYSTEM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");
        }
    }
}
