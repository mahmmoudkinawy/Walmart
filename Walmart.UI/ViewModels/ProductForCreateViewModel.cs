namespace Walmart.UI.ViewModels;
public sealed class ProductForCreateViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1,10000)]
    public decimal Price { get; set; }

    [Required]
    [MinLength(30)]
    [MaxLength(30000)]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Category Name")]
    [MinLength(2)]
    [MaxLength(150)]
    public string CategoryName { get; set; }

    [Required]
    [Display(Name = "Image Url")]
    [Url]
    public string ImageUrl { get; set; }
}