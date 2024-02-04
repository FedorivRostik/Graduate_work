using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Services.CartApi.Dto;
using Weather.Services.CartApi.Dtos;
using Weather.Services.CartApi.Dtos.Extensions;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Services.Interfaces;

namespace Weather.Services.CartApi.Controllers;
[Route("api/carts")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private ResponseDto ResponseDto { get; set; } = new();
    public CartController(
        ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("CartUpsert")]
    public async Task<ResponseDto> CartUpsert(CartAddDto cartDto)
    {
        var result = await _cartService.UpsertCart(cartDto);
        return ResponseDto.SetResult(result);
    }
}
