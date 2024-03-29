﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationProductAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1157), new Guid("00000000-0000-0000-0000-000000000000"), "", 0, "Appetizer", new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1157), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1209), new Guid("00000000-0000-0000-0000-000000000000"), "", 0, "Dessert", new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1209), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1222), new Guid("00000000-0000-0000-0000-000000000000"), "", 0, "Entree", new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1222), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "ProductCategoryName", "ProductImageUrl", "ProductPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1326), new Guid("00000000-0000-0000-0000-000000000000"), "El Pisco Sour es la bebida nacional peruana y un símbolo como tal del estilo de vida sudamericano. El “Pisco Sour” es un cóctel de Perú hecho a base de Pisco, que se sirve como aperitivo. Por la acidez característica de su limón es refrescante y vigorizante, y alegra cualquier reunión.", 0, "Pisco Sour", "Appetizer", "https://elcomercio.pe/resizer/_qzltzlrDcohFgUmpZ4DCbGX0Ss=/1200x800/smart/filters:format(jpeg):quality(75)/cloudfront-us-east-1.images.arcpublishing.com/elcomercio/66DKHZCFLVBE3EY2IYWUGLEOIE.png", 25.0, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1326), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1344), new Guid("00000000-0000-0000-0000-000000000000"), "El anticucho es un tipo de brocheta de origen peruano,​ que posteriormente se volvió popular en algunos países sudamericanos con diferentes variaciones. Consiste en carne y otros alimentos que se asan ensartados en un pincho.", 0, "Anticuchos", "Appetizer", "https://www.comedera.com/wp-content/uploads/2022/03/Anticucho-shutterstock_185287433.jpg", 20.989999999999998, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1358), new Guid("00000000-0000-0000-0000-000000000000"), "El suspiro limeño, suspiro de limeña o suspiro a la limeña es un postre tradicional peruano cuyo nombre hace referencia a su capital, Lima: “suspiro de Lima”. El lento proceso de cocción de esta receta da como resultado una base de natillas doradas, suave y sedosa, similar al caramelo, que luego se corona con un merengue de licor cremoso y ligero.", 0, "Suspiro a la Limeña", "Dessert", "https://assets.elgourmet.com/wp-content/uploads/2023/03/cover_siu6kem1v7_eg-lidg-platos-suspiro-limeno-hi-02-1024x683.jpg", 12.99, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1358), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4L, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1370), new Guid("00000000-0000-0000-0000-000000000000"), "La causa es uno de los platos más famosos de la comida peruana. Y esta causa limeña con pulpo al olivo es de las más coloridas y deliciosas. Las causas puedes rellenarlas de pollo, atún o camarones mezclados simplemente con mayonesa.", 0, "Causa al Olivo", "Entree", "https://media-cdn.tripadvisor.com/media/photo-s/1d/6b/de/5a/causa-peruana-con-pulpo.jpg", 35.0, new DateTime(2023, 12, 29, 20, 38, 11, 799, DateTimeKind.Local).AddTicks(1370), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
