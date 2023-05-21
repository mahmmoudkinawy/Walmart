namespace Walmart.Products.Api.DbContexts;
public sealed class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    { }

    public DbSet<ProductEntity> Products { get; set; }
}
