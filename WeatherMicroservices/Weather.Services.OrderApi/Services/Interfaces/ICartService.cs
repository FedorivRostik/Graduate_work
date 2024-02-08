using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.Carts;

namespace Weather.Services.CartApi.Services.Interfaces;

public interface ICartService
{
    Task<bool> UpsertCartAsync(CartAddDto cartAddDto);
    Task<CartResponseDto> GetCartAsync(string userId);
    Task<CartUpdateDetailsDto> UpdateDetailsAsync(CartUpdateDetailsDto cartUpdateDetailsDto);
    Task<bool> DeleteDetailsAsync(string cartDetailsId);
}
