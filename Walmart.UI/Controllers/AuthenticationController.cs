namespace Walmart.UI.Controllers;

[Authorize]
public sealed class AuthenticationController : Controller
{
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignOutAsync(
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult LoginCallback() => RedirectToAction("Index", "Products"); 
}
