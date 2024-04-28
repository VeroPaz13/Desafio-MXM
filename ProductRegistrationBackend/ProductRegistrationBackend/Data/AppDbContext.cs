using Microsoft.EntityFrameworkCore;
using ProductRegistrationBackend.Models;


namespace ProductRegistrationBackend.Data;
public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ProductsModel> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=sqlite.db");
    }
}
