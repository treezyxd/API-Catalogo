using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
    }
}