using Microsoft.AspNetCore.Http;
using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IBlobService 
    {
        Task<BlobDto> UploadFileTOBlobAsync(IFormFile file, string containerName, string fileName);
    }
}
