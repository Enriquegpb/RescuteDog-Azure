using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace RecuteDog.Services
{
    public class ServiceBlobRescuteDog
    {
        private BlobServiceClient client;

        public ServiceBlobRescuteDog(BlobServiceClient client)
        {
            this.client = client;
        }
        //public async Task<List<BlobModel>> GetBlobsAsync(string containerName)
        //{
        //    BlobContainerClient containerClient =
        //        this.client.GetBlobContainerClient(containerName);
        //    List<BlobModel> blobModels = new List<BlobModel>();
        //    await foreach (BlobItem item in containerClient.GetBlobsAsync())
        //    {
        //        BlobClient blobClient =
        //            containerClient.GetBlobClient(item.Name);
        //        BlobModel model = new BlobModel();
        //        model.Nombre = item.Name;
        //        model.Contenedor = containerName;
        //        model.Url = blobClient.Uri.AbsoluteUri;
        //        blobModels.Add(model);
        //    }
        //    return blobModels;
        //}

        public async Task<string> GetBlobUriAsync(string container, string blobName)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            var response = await containerClient.GetPropertiesAsync();
            var properties = response.Value;

            if (properties.PublicAccess == PublicAccessType.None)
            {
                Uri imageUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddSeconds(3600));
                return imageUri.ToString();
            }

            return blobClient.Uri.AbsoluteUri.ToString();
        }

        public async Task UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient =
                this.client.GetBlobContainerClient(containerName);
            await containerClient.UploadBlobAsync(blobName, stream);
        }

        public async Task<bool> BlobExistsAsync(string containerName,string blobName)
        {
            BlobContainerClient container = this.client.GetBlobContainerClient(containerName);
            BlobClient blob = container.GetBlobClient(blobName);
            return await blob.ExistsAsync();
        }


       

    }
}
