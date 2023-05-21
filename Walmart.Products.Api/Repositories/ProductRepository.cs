namespace Walmart.Products.Api.Repositories;
public sealed class ProductRepository : IProductRepository
{
    private readonly ProductsDbContext _context;

    public ProductRepository(ProductsDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task AddProduct(ProductEntity product)
    {
        ArgumentNullException.ThrowIfNull(nameof(product));

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductEntity> GetProductByIdAsync(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }

        return await _context.Products.FindAsync(productId);
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<bool> ProductExistsAsync(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }

        return await _context.Products.AnyAsync(p => p.Id == productId);
    }

    public async Task RemoveProduct(ProductEntity product)
    {
        ArgumentNullException.ThrowIfNull(nameof(product));

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct()
    {
        // No code for update as _mapper.map() will mark the entity as modified.

        await _context.SaveChangesAsync();
    }

}
