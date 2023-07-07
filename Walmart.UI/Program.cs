var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpClient(Constants.ApisBaseRoutes.ProductsApi, opts =>
{
    opts.BaseAddress = new Uri(builder.Configuration["ApiBaseUrls:ProductsApi"]);
    opts.DefaultRequestHeaders.Clear();
    opts.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddUserAccessTokenHandler();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ApiBaseUrls:IdentityIdp:Url"];
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ClientId = builder.Configuration["ApiBaseUrls:IdentityIdp:ClientId"];
        options.ClientSecret = builder.Configuration["ApiBaseUrls:IdentityIdp:ClientSecret"];
        options.ResponseType = builder.Configuration["ApiBaseUrls:IdentityIdp:ResponseType"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.Scope.Add("roles");
        options.Scope.Add("walmartproductsapi.fullaccess");
        options.ClaimActions.MapJsonKey("role", "role");
        options.ClaimActions.Remove("uid");
        options.ClaimActions.DeleteClaims(new[] { "idp", "sid", "amr", "local" });
        options.TokenValidationParameters = new()
        {
            NameClaimType = "given_name",
            RoleClaimType = "role"
        };
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
