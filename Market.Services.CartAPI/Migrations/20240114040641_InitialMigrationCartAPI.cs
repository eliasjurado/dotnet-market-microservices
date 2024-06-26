﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Services.CartAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationCartAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartHeader",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartHeaderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<long>(type: "bigint", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponDisccountAmount = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartHeader", x => new { x.CreatedBy, x.CartHeaderId });
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartHeaderId = table.Column<long>(type: "bigint", nullable: false),
                    CartDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => new { x.CreatedBy, x.CartHeaderId, x.CartDetailId });
                    table.ForeignKey(
                        name: "FK_CartDetail_CartHeader_CreatedBy_CartHeaderId",
                        columns: x => new { x.CreatedBy, x.CartHeaderId },
                        principalTable: "CartHeader",
                        principalColumns: new[] { "CreatedBy", "CartHeaderId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartHeader_CartHeaderId",
                table: "CartHeader",
                column: "CartHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartHeader_CreatedBy",
                table: "CartHeader",
                column: "CreatedBy",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropTable(
                name: "CartHeader");
        }
    }
}
