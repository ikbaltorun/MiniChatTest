using Microsoft.EntityFrameworkCore;

namespace MiniChatTest.Models
{
    // Ürün Şablonumuz
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int Stok { get; set; }
        public string Description { get; set; }
        public int Puan { get; set; }
        
    }

    // Veritabanı Bağlantımız
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
