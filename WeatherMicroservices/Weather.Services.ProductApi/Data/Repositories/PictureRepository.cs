using Azure.Storage.Blobs;
using Weather.Services.ProductApi.Data.Repositories.Interfaces;

namespace Weather.Services.ProductApi.Data.Repositories;

public class PictureRepository : IPictureRepository
{
    private readonly IConfiguration _configuration;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public PictureRepository(
              IConfiguration configuration,
              BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
        _containerName = _configuration["Azure:ContainerName"];
    }
    public async Task DeleteAsync(string imageLink)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);

        if (imageLink.Contains(_containerName))
        {
            imageLink = imageLink.Split(_containerName)[1];
        }

        var blobClient = blobContainer.GetBlobClient(imageLink);

        await blobClient.DeleteAsync();
    }

    public async Task<string> UploadAsync(
           string image64,
           string alias)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
        var imageFormat = GetFileExtension(image64);
        var fileName = $"product-pictures/{alias}.{imageFormat}";

        var blobItems = blobContainer.FindBlobsByTags($"product_alias = '{alias}'");

        if (blobItems.Any())
        {
            await DeleteAsync(blobItems.First().BlobName);
        }

        byte[] byteArray = Convert.FromBase64String(image64.Substring(image64.LastIndexOf(',') + 1));

        using (MemoryStream ms = new(byteArray))
        {
            await blobContainer.UploadBlobAsync(fileName, ms);
        }

        var blobClient = blobContainer.GetBlobClient(fileName);
        var blobTags = new Dictionary<string, string>
        {
            { "product_alias", alias }
        };

        blobClient.SetTags(blobTags);
        fileName = $"{_configuration["Azure:ContainerLink"]}/{_containerName}/{fileName}";

        return fileName;
    }

    private static string GetFileExtension(string base64String)
    {
        int startIndex = "data:image/".Length;

        int endIndex = base64String.IndexOf(";base64");

        return base64String.Substring(startIndex, endIndex - startIndex);
    }

    public async Task<Stream> GetLinkAsync(string alias)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
        using Stream ms = new MemoryStream();

        await foreach (var blobItem in blobContainer.FindBlobsByTagsAsync($"product_alias = '{alias}'"))
        {
            var blobClient = blobContainer.GetBlobClient(blobItem.BlobName);
            return await blobClient.OpenReadAsync();

        }
        await foreach (var blobItem in blobContainer.FindBlobsByTagsAsync($"product_alias = '_default'"))
        {
            var blobClient = blobContainer.GetBlobClient(blobItem.BlobName);
            return await blobClient.OpenReadAsync();

        }
        throw new NullReferenceException("No image with tag; '_default'")!;
    }
}
