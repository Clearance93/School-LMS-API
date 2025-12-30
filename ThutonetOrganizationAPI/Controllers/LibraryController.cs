using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School.Settings;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> _Logger;
        private readonly ILibraryInterfaceService _LibraryService;
        public LibraryController(ILogger<LibraryController> logger,
                                ILibraryInterfaceService liraryService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _LibraryService = liraryService ?? throw new ArgumentNullException(nameof(liraryService));
        }

        [HttpDelete("deleteLibraryItem/{id}")]
        public async Task<IActionResult> DeleteLibraryItemAsync(Guid id)
        {
            try
            {
                var result = await _LibraryService.DeleteABookItemAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while deleting library item with ID: {LibraryId}", id);

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("getAllLibraryItems")]
        public async Task<IActionResult> GetLibraryItem()
        {
            try
            {
                var books = await _LibraryService.GetAllLibraryItemsAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while retrieving all library items.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("getLibraryItemById/{id}")]
        public async Task<IActionResult> GetLibraryItemByIdAsync(Guid id)
        {
            try
            {
                var book = await _LibraryService.GetABookItemByIdAsync(id);

                return Ok(book);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while retrieving library item with ID: {LibraryId}", id);

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("addLibraryItem")]
        public async Task<IActionResult> AddLibraryItemAsync([FromBody] LibraryItemsDto library)
        {
            try
            {
                var result = await _LibraryService.AddABookAsync(library);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while adding a new library item.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("updateLibraryItem/{id}")]
        public async Task<IActionResult> UpdateLibraryItemAsync(Guid id, [FromBody] LibraryItemsDto library)
        {
            try
            {
                var result = await _LibraryService.UpdateABookAsync(id, library);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while updating library item with ID: {LibraryId}", id);

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
