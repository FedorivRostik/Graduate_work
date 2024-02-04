using Store.Services.CartApi.Dto;

namespace Weather.Services.CartApi.Services.Interfaces;

public interface ICartService
{
    Task<bool> UpsertCart(CartAddDto cartAddDto);
}
