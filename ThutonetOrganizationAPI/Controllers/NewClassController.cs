using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewClassController : ControllerBase
    {
        private readonly INewClassServicesInterface _NewClass;

        public NewClassController(INewClassServicesInterface newClass)
        {
            _NewClass = newClass ?? throw new ArgumentNullException(nameof(newClass));
        }

        [HttpPost("createClass")]
        public async Task<IActionResult> CreateNewClass(NewClassDto dto)
        {
            try
            {
                var newClass = await _NewClass.CreateNewClassAsync(dto);

                return Ok(newClass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllClasses/{organizationId}/{teacherId}")]
        public async Task<IActionResult> GetAllClassesCreatedByTeacher(Guid organizationId, Guid teacherId)
        {
            try
            {
                var allClasses = await _NewClass.GetAllNewCreatedClassesAsync(organizationId, teacherId);

                return Ok(allClasses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut("updateClass/{id}")]
        public async Task<IActionResult> UpdatingClass(Guid id, NewClassDto dto)
        {
            try
            {
                var updateClass = await _NewClass.UpdateClassAsync(id, dto);

                return Ok(updateClass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
