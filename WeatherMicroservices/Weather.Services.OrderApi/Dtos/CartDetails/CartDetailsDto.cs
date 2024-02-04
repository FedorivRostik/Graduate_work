using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Dtos.Products;

namespace Weather.Services.CartApi.Dtos.CartDetails;

public class CartDetailsDto
{
    public string? CartDetailsId { get; set; } = default!;
    public string? CartHeaderId { get; set; } = default!;
    public CartHeaderDto? CartHeader { get; set; }
    public string ProductId { get; set; } = default!;
    public ProductDto? ProductDto { get; set; }
    public int Count { get; set; }
}
