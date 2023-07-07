namespace Walmart.UI.Controllers;
public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService ??
            throw new ArgumentNullException(nameof(productService));
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();

        return View(products);
    }

    [Authorize]
    public async Task<IActionResult> Details(Guid productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);

        if (product is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
