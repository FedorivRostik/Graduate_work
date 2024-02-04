using Newtonsoft.Json;
using Weather.Services.CartApi.Dtos;
using Weather.Services.CartApi.Dtos.Products;
using Weather.Services.CartApi.Services.Interfaces;

namespace Weather.Services.CartApi.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var client = _httpClientFactory.CreateClient("Product");

        var response = await client.GetAsync($"/api/products");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

        if (resp!.IsSuccess)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.ResultObj)!)!;
        }

        throw new ArgumentException("not found");
    }
}