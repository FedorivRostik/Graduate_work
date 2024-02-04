using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;

namespace Weather.Services.CartApi.Dtos.Carts;

public class CartAddDto
{
    public CartAddHeaderDto? CartHeader { get; set; }
    public CartAddDetailsDto? CartDetails { get; set; }
}
