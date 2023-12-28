using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCouponMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DisccountAmount = table.Column<double>(type: "float", nullable: false),
                    MinAmmount = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "DisccountAmount", "EndDate", "IsDeleted", "MinAmmount", "Name", "StartDate", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1L, "15OFF", new DateTime(2023, 12, 28, 7, 52, 59, 809, DateTimeKind.Local).AddTicks(123), "SYSTEM", "Special Disccount for Christmas", 15.0, new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), 0, 200.0, "15% OFF - Christmas Disccount", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 28, 7, 52, 59, 809, DateTimeKind.Local).AddTicks(134), "SYSTEM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");
        }
    }
}
