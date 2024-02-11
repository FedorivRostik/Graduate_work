using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Weather.Services.CartApi.Dtos;
using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Dtos.Carts;
using Weather.Services.CartApi.Dtos.Extensions;
using Weather.Services.CartApi.Dtos.LiqPay;
using Weather.Services.CartApi.Services.Interfaces;
using Weather.Services.CartApi.Utilities.Helpers;

namespace Weather.Services.CartApi.Controllers;
[Route("api/carts")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly LiqPayHelper _liqPayHelper;
    private ResponseDto ResponseDto { get; set; } = new();
    public CartController(
        ICartService cartService,
        LiqPayHelper liqPayHelper)
    {
        _cartService = cartService;
        _liqPayHelper = liqPayHelper;
    }

    [Authorize]
    [HttpPost("cartUpsert")]
    public async Task<ActionResult<ResponseDto>> CartUpsertAsync([FromBody] CartAddDto cartDto)
    {
        var result = await _cartService.UpsertCartAsync(cartDto);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpGet("getCart/{userId}")]
    public async Task<ActionResult<CartResponseDto>> GetUserCartAsync([FromRoute] string userId)
    {
        var result = await _cartService.GetCartAsync(userId);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpPost("updateDetailsCount")]
    public async Task<ActionResult<CartResponseDto>> UpdateDetailsCountAsync([FromBody] CartUpdateDetailsDto cartUpdateDetailsDto)
    {
        var result = await _cartService.UpdateDetailsAsync(cartUpdateDetailsDto);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpPost("deleteDetails")]
    public async Task<ActionResult<CartResponseDto>> DeleteDetailsCountAsync([FromBody] CartDeleteDetailsDto cartUpdateDetailsDto)
    {
        var result = await _cartService.DeleteDetailsAsync(cartUpdateDetailsDto.CartDetailsId!);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpPost("updateStatus")]
    public async Task<ActionResult<CartResponseDto>> UpdateStatusAsync([FromBody] CartUpdateHeaderStatusDto cartUpdateHeaderStatusDto)
    {
        var result = await _cartService.UpdateCartHeaderStatusAsync(cartUpdateHeaderStatusDto);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpGet("userOrders")]
    public async Task<ActionResult<CartResponseDto>> GetUserOrders()
    {
       var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var result = await _cartService.GetUserOrders(userId);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpPost("liqpayData")]
    public IActionResult GetLiqPayData([FromBody] LiqPayCreateDto liqPayCreateDto)
    {
        return Ok(ResponseDto.SetResult(_liqPayHelper.GetLiqPayModel(Convert.ToString(liqPayCreateDto.OrderId), liqPayCreateDto.Amount)));
    }

    [Authorize]
    [HttpGet("GetCartHeader/{headerId}")]
    public async Task<IActionResult> GetCartHeader([FromRoute] string headerId)
    {
        var result = await _cartService.GetCartHeaderAsync(headerId);
        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize]
    [HttpPost("cartUpdateShippmentInfo")]
    public async Task<IActionResult >CartUpdateShippmentInfoAsync([FromBody] HeaderUpdateShippmentInfoDto cartUpdateShippmentInfoDto)
    {
        var result = await _cartService.CartUpdateShippmentInfoAsync(cartUpdateShippmentInfoDto);
        return Ok(ResponseDto.SetResult(result));
    }
}
