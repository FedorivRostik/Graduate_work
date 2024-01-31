namespace Weather.Services.ProductApi.Models;

public class Genre
{
    public Guid GenreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}
