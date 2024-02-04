using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Weather.Services.CartApi.Dtos.Products;

namespace Weather.Services.CartApi.Models;

public class CartDetail
{
    [Key]
    public Guid CartDetailsId { get; set; }
    public Guid CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public CartHeader? CartHeader { get; set; }
    public Guid ProductId { get; set; }
    [NotMapped]
    public ProductDto? ProductDto { get; set; }
    public int Count { get; set; }
}
