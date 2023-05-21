namespace Walmart.Products.Api.Repositories;
public interface IProductRepository
{
    Task<IReadOnlyList<ProductEntity>> GetProductsAsync();
    Task<ProductEntity> GetProductByIdAsync(Guid productId);
    Task<bool> ProductExistsAsync(Guid productId);
    Task AddProduct(ProductEntity product);
    Task RemoveProduct(ProductEntity product);
    Task UpdateProduct();
}
