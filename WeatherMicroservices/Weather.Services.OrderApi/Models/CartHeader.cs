using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Weather.Services.CartApi.Models;

public class CartHeader
{
    [Key]
    public Guid CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    
    [NotMapped]
    public double Discount { get; set; }
    [NotMapped]
    public double CartTotal { get; set; }

    public IEnumerable<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
