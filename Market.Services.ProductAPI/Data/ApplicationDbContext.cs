
using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.ProductAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Appetizer"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                Name = "Dessert"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 3,
                Name = "Entree"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Pisco Sour",
                Price = 25,
                Description = "El Pisco Sour es la bebida nacional peruana y un símbolo como tal del estilo de vida sudamericano. El “Pisco Sour” es un cóctel de Perú hecho a base de Pisco, que se sirve como aperitivo. Por la acidez característica de su limón es refrescante y vigorizante, y alegra cualquier reunión.",
                ImageUrl = "https://elcomercio.pe/resizer/_qzltzlrDcohFgUmpZ4DCbGX0Ss=/1200x800/smart/filters:format(jpeg):quality(75)/cloudfront-us-east-1.images.arcpublishing.com/elcomercio/66DKHZCFLVBE3EY2IYWUGLEOIE.png",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Anticuchos",
                Price = 20.99,
                Description = "El anticucho es un tipo de brocheta de origen peruano,​ que posteriormente se volvió popular en algunos países sudamericanos con diferentes variaciones. Consiste en carne y otros alimentos que se asan ensartados en un pincho.",
                ImageUrl = "https://www.comedera.com/wp-content/uploads/2022/03/Anticucho-shutterstock_185287433.jpg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Suspiro a la Limeña",
                Price = 12.99,
                Description = "El suspiro limeño, suspiro de limeña o suspiro a la limeña es un postre tradicional peruano cuyo nombre hace referencia a su capital, Lima: “suspiro de Lima”. El lento proceso de cocción de esta receta da como resultado una base de natillas doradas, suave y sedosa, similar al caramelo, que luego se corona con un merengue de licor cremoso y ligero.",
                ImageUrl = "https://assets.elgourmet.com/wp-content/uploads/2023/03/cover_siu6kem1v7_eg-lidg-platos-suspiro-limeno-hi-02-1024x683.jpg",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Causa al Olivo",
                Price = 35,
                Description = "La causa es uno de los platos más famosos de la comida peruana. Y esta causa limeña con pulpo al olivo es de las más coloridas y deliciosas. Las causas puedes rellenarlas de pollo, atún o camarones mezclados simplemente con mayonesa.",
                ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/1d/6b/de/5a/causa-peruana-con-pulpo.jpg",
                CategoryName = "Entree"
            });

        }
    }
}
