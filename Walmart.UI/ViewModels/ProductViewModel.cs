﻿namespace Walmart.UI.ViewModels;
public sealed class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }

    [Range(1, 100)]
    public int Count { get; set; } = 1;
}
