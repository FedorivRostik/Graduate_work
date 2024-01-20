namespace Weather.Services.ProductApi.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; } = true;
    public object? ResultObj { get; set; }
    public string Message { get; set; } = string.Empty;
}
