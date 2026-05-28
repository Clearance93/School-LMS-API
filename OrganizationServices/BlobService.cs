using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace OrganizationServices
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _Configuration;
        private readonly string _BlobConnection;

        public BlobService(IConfiguration configuration)
        {
            _Configuration = configuration;
            _BlobConnection = configuration["AzureBlobSettings:BlobConnection"];
        }

        public async Task<BlobDto> UploadFileTOBlobAsync(IFormFile file, string containerName, string fileName)
        {
            var connectionString = _BlobConnection;

            var blobServerClient = new BlobServiceClient(connectionString);

            var blobContainerClient = blobServerClient.GetBlobContainerClient(containerName);

            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            var blobUrl = blobClient.Uri.ToString();

            return new BlobDto
            {
                BlobFileUrl = blobUrl
            };
        }
    }
}
