using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Weather.Services.CartApi.Utilities.Constants;
using System.ComponentModel;

namespace Weather.Services.CartApi.Models;

public class CartHeader
{
    [Key]
    public Guid CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }

    public string? Status { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    [NotMapped]
    public double Discount { get; set; }
    [NotMapped]
    public double CartTotal { get; set; }

    public IEnumerable<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

   public void UpdateShippingInfo(string address, string email, string phone)
    {
        Address = address;
        Email = email;
        Phone = phone;
    }
}
