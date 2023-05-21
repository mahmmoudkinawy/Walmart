namespace Walmart.Products.Api.Validators;
public sealed class ProductForCreateValidator : AbstractValidator<ProductForCreateRequest>
{
    public ProductForCreateValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(300);

        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(10000);

        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .MinimumLength(10)
            .MaximumLength(3000);

        RuleFor(p => p.CategoryName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(150);

        RuleFor(p => p.ImageUrl)
            .NotEmpty()
            .NotNull()
            .Must(imageUrl => 
                            Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute) &&
                            (imageUrl.StartsWith("http://") || imageUrl.StartsWith("https://")))
            .WithMessage("Invalid Image Url link.");
    }
}
