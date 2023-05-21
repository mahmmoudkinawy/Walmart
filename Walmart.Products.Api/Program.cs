var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddFluentValidation(_ => _.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
