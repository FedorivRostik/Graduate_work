namespace Weather.Services.ProductApi.Data.Repositories.Interfaces;

public interface IPictureRepository
{
    Task DeleteAsync(string imageLink);
    Task<string> UploadAsync(string image64, string alias);
    Task<Stream> GetLinkAsync(string alias);
}
