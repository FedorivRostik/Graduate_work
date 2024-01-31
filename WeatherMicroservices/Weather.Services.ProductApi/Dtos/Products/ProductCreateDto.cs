namespace Weather.Services.ProductApi.Dtos.Products;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? CouponCode { get; set; }
    public string? ImageUrl { get; set; }
    public string? GenreId { get; set; }
}
