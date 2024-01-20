using Weather.Services.ProductApi.Dtos.Products;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto> AddProductAsync(ProductCreateDto productDto);
    Task<ProductDto> UpdateProductAsync(ProductDto productDto);
    Task<IEnumerable<ProductDto>> GetAllProductAsync();
    Task<ProductDto> GetBySlugProductAsync(string slug);
    Task<ProductDto> GetByIdProductAsync(Guid id);
    Task<ProductDto> DeleteByIdSlugProductAsync(Guid id);

}
