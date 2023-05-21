namespace Walmart.Products.Api.DbContexts;
public static class Seed
{
    public static async Task SeedProducts(ProductsDbContext context)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));

        if (await context.Products.AnyAsync())
        {
            return;
        }

        var products = new Faker<ProductEntity>("en")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.CategoryName, f => f.Commerce.ProductAdjective())
            .RuleFor(p => p.Price, f => f.Random.Decimal(2.1M, 10000M))
            .RuleFor(p => p.ImageUrl, f => f.Person.Avatar)
            .Generate(10);

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
