using System.ComponentModel.DataAnnotations;
using Weather.Services.ProductApi.Helpers;

namespace Weather.Services.ProductApi.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? CouponCode { get; set; }
    public string? GenreName { get; set; }
    public string? ImageUrl { get; set; }

    //public Product()
    //{
    //    Slug =  string.Join('-', Name.RemoveExtraSpaces().Split(' '));
    //}

}
