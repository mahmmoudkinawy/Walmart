﻿namespace Walmart.UI.Controllers;

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

    [Authorize]
    public async Task<IActionResult> Index()
    {
        await LogIdentityInformation();

        var products = await _productService.GetProductsAsync();

        return View(products);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult CreateProduct() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct(ProductForCreateViewModel product)
    {
        if (ModelState.IsValid)
        {
            await _productService.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            await _productService.RemoveProduct(product.Id);

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task LogIdentityInformation()
    {
        var identityToken = await HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        var accessToken = await HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var userClaimsStringBuilder = new StringBuilder();
        foreach (var claim in User.Claims)
        {
            userClaimsStringBuilder.AppendLine(
                $"Claim type: {claim.Type} - Claim value: {claim.Value}");
        }

        _logger.LogInformation($"Identity token & user claims: " +
            $"\n{identityToken} \n {userClaimsStringBuilder}");

        _logger.LogInformation($"Access token: " +
          $"\n{accessToken}");
    }

}
