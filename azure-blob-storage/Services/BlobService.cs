using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using azure_blob_storage.Model;
using Microsoft.AspNetCore.StaticFiles;
using BlobInfo = azure_blob_storage.Model.BlobInfo;

namespace azure_blob_storage.Services
{
    public class BlobService : IBlobService
    {
        private const string blobContainerName = "";
        private readonly BlobServiceClient client;

        public BlobService(BlobServiceClient client)
        {
            this.client = client;
        }
        public async Task DeleteBlobAsync(string blobName)
        {
            var container = client.GetBlobContainerClient(blobContainerName);
            var blobClient = container.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<BlobInfo> GetBlobAsync(string blobName)
        {
            var container = client.GetBlobContainerClient(blobContainerName);
            var blobClient = container.GetBlobClient(blobName);
            var downloadInfo = await blobClient.DownloadStreamingAsync();

            return new BlobInfo(downloadInfo.Value.Content, downloadInfo.Value.Details.ContentType);
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var container = client.GetBlobContainerClient(blobContainerName);
            var returnResult = new List<string>();
            await foreach (var item in container.GetBlobsAsync())
            {
                returnResult.Add(item.Name);
            }

            return returnResult;
        }

        public async Task UploadContentBlobAsync(string content, string fileName)
        {
            var container = client.GetBlobContainerClient(blobContainerName);
            var blobClient = container.GetBlobClient(fileName); // not existing yet, but we create reference to it

            var bytes = Encoding.UTF8.GetBytes(content);
            
            using var stream = new MemoryStream(bytes);
            await blobClient.UploadAsync(stream, httpHeaders: new BlobHttpHeaders { ContentType = GetContentType(fileName)});
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            var container = client.GetBlobContainerClient(blobContainerName);
            var blobClient = container.GetBlobClient(fileName); // not existing yet, but we create reference to it

            await blobClient.UploadAsync(filePath, httpHeaders: new BlobHttpHeaders{ ContentType = GetContentType(filePath)});
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if(!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }

}