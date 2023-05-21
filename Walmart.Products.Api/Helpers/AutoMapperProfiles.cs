namespace Walmart.Products.Api.Helpers;
public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<ProductForCreateRequest, ProductEntity>();
        CreateMap<ProductForUpdateRequest,ProductEntity>();
    }
}
