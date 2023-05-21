namespace Walmart.Products.Api.Contracts.Requests;
public sealed class ProductForUpdateRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}
