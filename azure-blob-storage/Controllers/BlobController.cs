using System.Threading.Tasks;
using azure_blob_storage.Model;
using azure_blob_storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace azure_blob_storage.Controllers
{
    [ApiController]
    [Route("blobs")]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService service;

        public BlobController(IBlobService service)
        {
            this.service = service;
        }

        [HttpGet("{blobName}")]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var blob = await service.GetBlobAsync(blobName);
            return Ok(blob);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListBlobs()
        {
            var blobs = await service.ListBlobsAsync();
            return Ok(blobs);
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            await service.UploadFileBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

        [HttpPost("uploadcontent")]
        public async Task<IActionResult> UploadContent([FromBody] UploadContentRequest request)
        {
            await service.UploadContentBlobAsync(request.Content, request.FileName);
            return Ok();
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteBlob(string blobName)
        {
            await service.DeleteBlobAsync(blobName);
            return Ok();
        }
    }
}