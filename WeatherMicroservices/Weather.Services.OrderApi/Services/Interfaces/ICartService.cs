using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Dtos.Carts;

namespace Weather.Services.CartApi.Services.Interfaces;

public interface ICartService
{
    Task<bool> UpsertCartAsync(CartAddDto cartAddDto);
    Task<CartResponseDto> GetCartAsync(string userId);
    Task<CartUpdateDetailsDto> UpdateDetailsAsync(CartUpdateDetailsDto cartUpdateDetailsDto);
    Task<bool> UpdateCartHeaderStatusAsync(CartUpdateHeaderStatusDto cartUpdateHeaderStatusDto);
    Task<IEnumerable<CartResponseDto>> GetUserOrders(string userId);
    Task<bool> DeleteDetailsAsync(string cartDetailsId);
    Task<bool> CartUpdateShippmentInfoAsync(HeaderUpdateShippmentInfoDto cartUpdateShippmentInfoDto);
    Task<CartHeaderDto> GetCartHeaderAsync(string headerId);
    Task<bool> CheckPayedCartStatusAsync(string cartHeaderId);
}
