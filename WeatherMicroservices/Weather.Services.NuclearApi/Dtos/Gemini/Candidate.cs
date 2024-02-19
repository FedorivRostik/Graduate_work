namespace Weather.Services.NuclearApi.Dtos.Gemini;

public class Candidate
{
    public Content? Content { get; set; }
    public List<SafetyRating>? SafetyRatings { get; set; }
}