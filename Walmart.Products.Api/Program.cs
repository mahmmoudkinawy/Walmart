using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddFluentValidation(_ => _.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("WalmartProductsApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "WalmartProductsApiBearerAuth" }
                    }, new List<string>() }
            });

});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ApiBaseUrls:IdentityIdp:Url"];
        options.Audience = builder.Configuration["ApiBaseUrls:IdentityIdp:Audience"];
        options.TokenValidationParameters = new()
        {
            RoleClaimType = "role",
            ValidTypes = new[] { "at+jwt" }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>
    {
        policy
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, "Admin");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await dbContext.Database.MigrateAsync();
    await Seed.SeedProducts(dbContext);
}
catch (Exception ex)
{
    logger.LogError(ex, "Product Api - An error occurred while applying pending migrations.");
}

app.Run();
