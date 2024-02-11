namespace Weather.Services.CartApi.Dtos.CartHeaders;

public class CartUpdateHeaderStatusDto
{
    public string CartHeaderId { get; set; } = default!;
    public string Status { get; set; } = default!;
}
