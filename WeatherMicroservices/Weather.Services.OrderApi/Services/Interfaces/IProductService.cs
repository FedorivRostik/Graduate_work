using Weather.Services.CartApi.Dtos.Products;

namespace Weather.Services.CartApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}
