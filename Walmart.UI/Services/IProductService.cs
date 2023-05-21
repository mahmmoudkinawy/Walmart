namespace Walmart.UI.Services;
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetProductsAsync();
    Task<ProductViewModel> GetProductByIdAsync(Guid productId);
    Task AddProduct(ProductForCreateViewModel product);
    Task UpdateProduct(ProductViewModel product);
    Task RemoveProduct(Guid productId);
}
