namespace Store.Services.CartApi.Dto;

public class CartHeaderDto
{
    public string? CartHeaderId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string? CouponCode { get; set; }

    public double? Discount { get; set; }

    public double? CartTotal { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}
