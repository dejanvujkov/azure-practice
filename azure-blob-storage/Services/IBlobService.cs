using System.Collections.Generic;
using System.Threading.Tasks;
using azure_blob_storage.Model;

namespace azure_blob_storage.Services
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string blobName);

        Task<IEnumerable<string>> ListBlobsAsync();

        Task UploadFileBlobAsync(string filePath, string fileName);

        Task UploadContentBlobAsync(string content, string fileName);

        Task DeleteBlobAsync(string blobName);
    }
}