using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weather.Services.ProductApi.Dtos;
using Weather.Services.ProductApi.Dtos.Extensions;
using Weather.Services.ProductApi.Dtos.Products;
using Weather.Services.ProductApi.Services.Interfaces;
using Weather.Services.ProductApi.Utilities.Constants;

namespace Weather.Services.ProductApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductControllerApi : ControllerBase
{
    private readonly IProductService _productService;
    private ResponseDto ResponseDto { get; set; } = new();

    public ProductControllerApi(
        IProductService productService
        )
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateDto productDto)
    {
        var result = await _productService.AddProductAsync(productDto);

        return Ok(ResponseDto.SetResult(result));
    }

    [Authorize(Roles = AppRoles.AdminRole)]
    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDto productDto)
    {
        var result = await _productService.UpdateProductAsync(productDto);

        return Ok(ResponseDto.SetResult(result));
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await _productService.GetAllProductAsync();

        return Ok(ResponseDto.SetResult(result));
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetByIdProductAsync([FromRoute] Guid id)
    {
        var result = await _productService.GetByIdProductAsync(id);

        return Ok(ResponseDto.SetResult(result));
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlugProductAsync([FromRoute] string slug)
    {
        var result = await _productService.GetBySlugProductAsync(slug);

        return Ok(ResponseDto.SetResult(result));
    }
}
