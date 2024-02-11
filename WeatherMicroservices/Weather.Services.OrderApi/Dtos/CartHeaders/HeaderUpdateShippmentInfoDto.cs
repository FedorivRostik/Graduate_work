namespace Weather.Services.CartApi.Dtos.CartHeaders;

public class HeaderUpdateShippmentInfoDto
{
    public string CartHeaderId { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
}
