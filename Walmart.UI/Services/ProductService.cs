namespace Walmart.UI.Services;
public sealed class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task AddProduct(ProductForCreateViewModel request)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(request));

        var httpClient = _httpClientFactory.CreateClient(Constants.ApisBaseRoutes.ProductsApi);
        var response = await httpClient.PostAsJsonAsync("products", request);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            return;
        }
    }

    public async Task<ProductViewModel> GetProductByIdAsync(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }

        var httpClient = _httpClientFactory.CreateClient(Constants.ApisBaseRoutes.ProductsApi);
        var response = await httpClient.GetAsync($"products/{productId}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(content);

            if (product is not null)
            {
                return product;
            }
        }

        return null;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.ApisBaseRoutes.ProductsApi);
        var response = await httpClient.GetAsync("products");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IReadOnlyList<ProductViewModel>>(content);

            return products;
        }

        return Enumerable.Empty<ProductViewModel>();
    }

    public async Task RemoveProduct(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }

        var httpClient = _httpClientFactory.CreateClient(Constants.ApisBaseRoutes.ProductsApi);
        var response = await httpClient.DeleteAsync($"products/{productId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return;
        }
    }

    public async Task UpdateProduct(ProductViewModel product)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(product));

        var httpClient = _httpClientFactory.CreateClient(Constants.ApisBaseRoutes.ProductsApi);
        var response = await httpClient.PutAsJsonAsync($"products/{product.Id}", product);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return;
        }
    }

}
