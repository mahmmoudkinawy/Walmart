namespace Walmart.Products.Api.Controllers;

[Route("api/products")]
[ApiController]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ??
            throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productRepository.GetProductsAsync();

        return Ok(_mapper.Map<IReadOnlyList<ProductResponse>>(products));
    }

    [HttpGet("{productId}", Name = "GetProductById")]
    [Authorize]
    public async Task<IActionResult> GetProductById(
        [FromRoute] Guid productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductResponse>(product));
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] ProductForCreateRequest request)
    {
        var product = _mapper.Map<ProductEntity>(request);

        await _productRepository.AddProduct(product);

        return CreatedAtRoute(nameof(GetProductById),
            new { productId = product.Id },
            _mapper.Map<ProductResponse>(product));
    }

    [HttpPut("{productId}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid productId,
        [FromBody] ProductForUpdateRequest request)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        _mapper.Map(request, product);
        await _productRepository.UpdateProduct();

        return NoContent();
    }

    [HttpDelete("{productId}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> RemoveProduct(
        [FromRoute] Guid productId)
    {
        // I know that I can avoid the next check.

        if (!await _productRepository.ProductExistsAsync(productId))
        {
            return NotFound();
        }

        var product = await _productRepository.GetProductByIdAsync(productId);

        await _productRepository.RemoveProduct(product);

        return NoContent();
    }

}
