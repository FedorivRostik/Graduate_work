using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;

namespace Weather.Services.CartApi.Dtos.Carts;

public class CartResponseDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public IEnumerable<CartDetailsDto> CartDetails { get; set; } = Enumerable.Empty<CartDetailsDto>();
}
