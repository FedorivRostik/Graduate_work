using System.Net;
using System.Text;
using Weather.Services.NuclearApi.Dtos;

namespace Weather.Services.ProductApi.Extensions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(
           RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string json = System.Text.Json.JsonSerializer.Serialize(new ResponseDto
            {
                IsSuccess = false,
                Message = ex.Message.ToString()+ '\n' + ex?.InnerException?.ToString()
            });

            // Convert the JSON string to a byte array
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            // Set the byte array as the response body
            await context.Response.Body.WriteAsync(byteArray, 0, byteArray.Length);
        }
    }
}
