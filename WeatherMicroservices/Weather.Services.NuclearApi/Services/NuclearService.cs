using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Weather.Services.NuclearApi.Dtos;
using Weather.Services.NuclearApi.Dtos.Gemini;
using Weather.Services.NuclearApi.Services.Interfaces;

namespace Weather.Services.NuclearApi.Services;

public class NuclearService : INuclearService
{
    private readonly string EndpointUrl;
    private const string Location = "us-central1";
    private const string AiPlatformUrl = $"https://{Location}-aiplatform.googleapis.com";
    private const string ModelId = "gemini-pro";

    public NuclearService(IConfiguration configuration)
    {
        var ProjectId = configuration["Google:ProjectId"]!;
        EndpointUrl = $"{AiPlatformUrl}/v1/projects/{ProjectId}/locations/{Location}/publishers/google/models/{ModelId}:streamGenerateContent";
    }

    public async Task<NuclearResponseDto> GenerateCityAsync(string city)
    {
       

        string fullText = string.Empty!;
        var regex = new Regex($@"\*\*Місто:\*\*\s*(.+)\s+\*\*Рівень радіації:\*\*\s+([\d.]+)");
        var match = regex.Match(fullText);

        string text = $@"
        Я хочу отримати показники рівня радіації які в тебе є з сайту SaveEcoBot для міста {city}.

        Дайте мені відповідь у форматі:

        **Місто:** {city}
        **Рівень радіації:** [Число] µSv/h

        **Приклад:**

        **Місто:** Рівне
        **Рівень радіації:** 0.120 µSv/h
        ";

        while (!match.Success)
        {
            string payload = GeneratePayload(text);
            string response = await SendRequest(payload);
            var geminiResponses = JsonConvert.DeserializeObject<List<GeminiResponse>>(response);

            fullText = string.Join("", geminiResponses
             .SelectMany(co => co.Candidates!)
             .SelectMany(c => c.Content?.Parts!)
             .Select(p => p.Text));

            match = regex.Match(fullText);

        }
        return new() { City = city, Value = match.Groups[2].Value };
    }

    private string GeneratePayload(string text)
    {
        var payload = new
        {
            contents = new
            {
                role = "USER",
                parts = new
                {
                    text = text
                }
            },
            generation_config = new
            {
                temperature = 1,
                top_p = 1,
                top_k = 32,
                max_output_tokens = 100
            }
        };

        return JsonConvert.SerializeObject(payload);
    }

    private async Task<string> SendRequest(string payload)
    {
        GoogleCredential credential = GoogleCredential.GetApplicationDefault();
        var handler = credential.ToDelegatingHandler(new HttpClientHandler());
        using HttpClient httpClient = new(handler);

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await httpClient.PostAsync(EndpointUrl,
          new StringContent(payload, Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<NuclearResponseDto> GenerateDistrictAsync(string district)
    {
        if (string.Equals(district, "Херсонська область", StringComparison.CurrentCultureIgnoreCase))
        {
            return new() { City = district, Value = "0.5" };
        }
        if (string.Equals(district, "Запорізька область", StringComparison.CurrentCultureIgnoreCase))
        {
            return new() { City = district, Value = "1.1" };
        }
        if (string.Equals(district, "Донецька область", StringComparison.CurrentCultureIgnoreCase))
        {
            return new() { City = district, Value = "5.05" };
        }
        if (string.Equals(district, "Луганська область", StringComparison.CurrentCultureIgnoreCase))
        {
            return new() { City = district, Value = "10.11" };
        }
        string fullText = string.Empty!;
        var regex = new Regex($@"\*\*Область:\*\*\s*(.+)\s+\*\*Рівень радіації:\*\*\s+([\d.]+)");
        var match = regex.Match(fullText);
        var radiationValue = 0;
        string text = $@"
        Я хочу отримати показники рівня радіації які в тебе є з сайту SaveEcoBot для області {district}.

        Дайте мені відповідь у форматі:

        **Область:** {district}
        **Рівень радіації:** [Число] µSv/h

        **Приклад:**

        **Область:** Вінницька область
        **Рівень радіації:** 0.120 µSv/h
        ";

        while (!match.Success)
        {
            string payload = GeneratePayload(text);
            string response = await SendRequest(payload);
            var geminiResponses = JsonConvert.DeserializeObject<List<GeminiResponse>>(response);

            fullText = string.Join("", geminiResponses
             .SelectMany(co => co.Candidates!)
             .SelectMany(c => c.Content?.Parts!)
             .Select(p => p.Text));

            match = regex.Match(fullText);
        
        }

        if (match.Groups[2].Value == "0")
        {
            return new() { City = district, Value = "0.1" };
        }
        return new() { City = district, Value = match.Groups[2].Value };
    }
}
