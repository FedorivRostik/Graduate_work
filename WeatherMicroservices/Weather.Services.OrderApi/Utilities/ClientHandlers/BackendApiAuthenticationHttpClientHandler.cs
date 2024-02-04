using Microsoft.AspNetCore.Authentication;

namespace Weather.Services.CartApi.Utilities.ClientHandlers;

public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;

    public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token") ?? string.Empty;
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);

    }
}
