using System;
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
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(5955), "SYSTEM", "", 0, "Appetizer", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(5966), "SYSTEM" },
                    { 2L, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(5993), "SYSTEM", "", 0, "Dessert", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(5993), "SYSTEM" },
                    { 3L, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6001), "SYSTEM", "", 0, "Entree", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6001), "SYSTEM" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, "Appetizer", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6016), "SYSTEM", "El Pisco Sour es la bebida nacional peruana y un símbolo como tal del estilo de vida sudamericano. El “Pisco Sour” es un cóctel de Perú hecho a base de Pisco, que se sirve como aperitivo. Por la acidez característica de su limón es refrescante y vigorizante, y alegra cualquier reunión.", "https://elcomercio.pe/resizer/_qzltzlrDcohFgUmpZ4DCbGX0Ss=/1200x800/smart/filters:format(jpeg):quality(75)/cloudfront-us-east-1.images.arcpublishing.com/elcomercio/66DKHZCFLVBE3EY2IYWUGLEOIE.png", 0, "Pisco Sour", 25.0, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6017), "SYSTEM" },
                    { 2L, "Appetizer", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6030), "SYSTEM", "El anticucho es un tipo de brocheta de origen peruano,​ que posteriormente se volvió popular en algunos países sudamericanos con diferentes variaciones. Consiste en carne y otros alimentos que se asan ensartados en un pincho.", "https://www.comedera.com/wp-content/uploads/2022/03/Anticucho-shutterstock_185287433.jpg", 0, "Anticuchos", 20.989999999999998, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6030), "SYSTEM" },
                    { 3L, "Dessert", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6039), "SYSTEM", "El suspiro limeño, suspiro de limeña o suspiro a la limeña es un postre tradicional peruano cuyo nombre hace referencia a su capital, Lima: “suspiro de Lima”. El lento proceso de cocción de esta receta da como resultado una base de natillas doradas, suave y sedosa, similar al caramelo, que luego se corona con un merengue de licor cremoso y ligero.", "https://assets.elgourmet.com/wp-content/uploads/2023/03/cover_siu6kem1v7_eg-lidg-platos-suspiro-limeno-hi-02-1024x683.jpg", 0, "Suspiro a la Limeña", 12.99, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6039), "SYSTEM" },
                    { 4L, "Entree", new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6048), "SYSTEM", "La causa es uno de los platos más famosos de la comida peruana. Y esta causa limeña con pulpo al olivo es de las más coloridas y deliciosas. Las causas puedes rellenarlas de pollo, atún o camarones mezclados simplemente con mayonesa.", "https://media-cdn.tripadvisor.com/media/photo-s/1d/6b/de/5a/causa-peruana-con-pulpo.jpg", 0, "Causa al Olivo", 35.0, new DateTime(2023, 12, 29, 12, 42, 51, 206, DateTimeKind.Local).AddTicks(6049), "SYSTEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
