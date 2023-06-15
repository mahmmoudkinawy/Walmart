using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Text;

namespace Walmart.UI.Controllers;

[Authorize]
public sealed class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService ??
            throw new ArgumentNullException(nameof(productService));
        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IActionResult> Index()
    {
        var t = User.FindFirstValue(ClaimTypes.GivenName);

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
