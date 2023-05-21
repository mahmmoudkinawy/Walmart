namespace Walmart.UI.Controllers;
public sealed class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService ??
            throw new ArgumentNullException(nameof(productService));
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();

        return View(products);
    }

    public IActionResult CreateProduct() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductForCreateViewModel product)
    {
        if (ModelState.IsValid)
        {
            await _productService.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> EditProduct([FromQuery] Guid productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProduct(product);

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            await _productService.RemoveProduct(product.Id);

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }
}
