namespace Weather.Services.ProductApi.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public int? Discount { get; set; }
    public string? Description { get; set; }
    public string? CouponCode { get; set; }
    public string? ImageUrl { get; set; }

    public Guid? GenreId { get; set; }
    public Genre? Genre { get; set; }
}
