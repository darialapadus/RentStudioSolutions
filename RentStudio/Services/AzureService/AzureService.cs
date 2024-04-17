using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RentStudio.Configurations;

namespace RentStudio.Services.AzureService
{
    public class AzureService
    {
        private readonly CloudBlobClient _blobClient;
        private readonly string _containerName;

        public AzureService(IOptions<AzureSettings> azureSettings)
        {
            var storageAccount = CloudStorageAccount.Parse(azureSettings.Value.ConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
            _containerName = azureSettings.Value.ContainerName;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string imageName)
        {
            var container = _blobClient.GetContainerReference(_containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(imageName);
            await blob.UploadFromStreamAsync(imageStream);

            return blob.Uri.ToString();
        }

        public async Task<Stream> GetImageAsync(string imageName)
        {
            var container = _blobClient.GetContainerReference(_containerName);
            var blob = container.GetBlockBlobReference(imageName);

            var memoryStream = new MemoryStream();
            await blob.DownloadToStreamAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
